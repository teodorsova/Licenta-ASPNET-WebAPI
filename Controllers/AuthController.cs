using auth.Data;
using auth.Helpers;
using auth.Models;
using Azure.Storage.Blobs;
using BCrypt.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Google.Cloud.Firestore;

namespace auth.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly ISubscriptionRepo _subscriptionRepo;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        public AuthController(IUserRepo userRepo, ISubscriptionRepo subscriptionRepo, IJwtService jwtService, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _subscriptionRepo = subscriptionRepo;
            _jwtService = jwtService;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public IActionResult Register(UserModelRegisterDto dto)
        {


            var user = new UserModel
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Email = dto.Email,
                PhoneNo = dto.PhoneNo,
                Role = dto.Role,
                CompanyName = (dto.CompanyName == null || dto.CompanyName == "" ? "" : dto.CompanyName)
            };


            return Created("Success", _userRepo.CreateUser(user));
        }

        [HttpPost("login")]
        public IActionResult Login(UserModelLoginDto dto)
        {
            var user = _userRepo.GetByEmail(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return BadRequest(new { message = "Invalid Credentials" });

            var jwt = _jwtService.Generate(user);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "Success"
            });
        }

        [HttpGet("user")]
        public IActionResult CheckUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user = _userRepo.GetById(userId);

                var userDto = new UserModelGetDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNo = user.PhoneNo,
                    CompanyName = user.CompanyName
                };

                return Ok(userDto);
            }
            catch (Exception e)
            {
                return NoContent();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });
            Console.WriteLine(Response);
            return Ok(new { message = "success" });
        }

        [HttpPost("update")]
        public IActionResult Update(UserModelUpdateDto userModel)
        {
            var user = _userRepo.GetById(userModel.Id);
            if (user == null)
            {
                return NotFound();
            }
            else if (!BCrypt.Net.BCrypt.Verify(userModel.Password, user.Password))
            {
                return Unauthorized();
            }

            user.Email = userModel.Email;
            user.FirstName = userModel.FirstName;
            user.CompanyName = userModel.CompanyName;
            user.LastName = userModel.LastName;
            user.PhoneNo = userModel.PhoneNo;

            _userRepo.UpdateUser();

            return Ok("Success");

        }

        [HttpPost("subscription/create")]
        public IActionResult CreateSubscription(SubscriptionCreateModel subscriptionModel)
        {
            var existingSubscription = _subscriptionRepo.GetByUserId(new UserModelIdDto { Id = subscriptionModel.Id });
            var furnitureCap = 0;
            switch (subscriptionModel.Type)
            {
                case "Bronze":
                    {
                        furnitureCap = 100;
                        break;
                    }
                case "Silver":
                    {
                        furnitureCap = 350;
                        break;
                    }
                case "Gold":
                    {
                        furnitureCap = 1200;
                        break;
                    }
                case "Platinum":
                    {
                        furnitureCap = 2000;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            var subscription = new SubscriptionModel
            {
                Company = _userRepo.GetById(subscriptionModel.Id),
                Type = subscriptionModel.Type,
                Price = subscriptionModel.Price,
                FurnitureCap = furnitureCap,
                OccupiedCap = subscriptionModel.OccupiedCap
            };

            if (existingSubscription != null)
            {
                return UpdateSubscription(subscription);
            }
            else
            {
                return Created("Success", _subscriptionRepo.CreateSubscription(subscription));
            }
        }


        [HttpPost("subscription/get")]
        public IActionResult GetSubscription(UserModelIdDto user)
        {
            var subscription = _subscriptionRepo.GetByUserId(user);
            var subscriptionGetDto = new SubscriptionGetDto
            {
                Id = subscription.Id,
                CompanyId = user.Id,
                Type = subscription.Type,
                Price = subscription.Price,
                OccupiedCap = subscription.OccupiedCap,
                FurnitureCap = subscription.FurnitureCap
            };

            return Ok(subscriptionGetDto);
        }

        [HttpDelete("subscription/delete")]
        public IActionResult DeleteSubscription(UserModelIdDto model)
        {
            _subscriptionRepo.DeleteSubscription(model);
            return Ok();
        }

        [HttpPost("subscription/update")]
        public IActionResult UpdateSubscription(SubscriptionModel model)
        {
            var subscription = _subscriptionRepo.GetByUserId(new UserModelIdDto { Id = model.Company.Id });
            if (subscription == null)
            {
                return NotFound();
            }

            subscription.Price = model.Price;
            subscription.Type = model.Type;
            subscription.OccupiedCap = model.OccupiedCap;
            subscription.FurnitureCap = model.FurnitureCap;
            _subscriptionRepo.UpdateSubscription();
            return Ok();
        }
        [HttpGet("azure/get/config")]
        public async Task<IActionResult> GetConfigFile()
        {
            CloudBlockBlob blockBlob;
            await using (MemoryStream memoryStream = new MemoryStream())
            {
                string fileName = "config.xml";
                string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerName"));
                blockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                await blockBlob.DownloadToStreamAsync(memoryStream);
            }
            Stream blobStream = blockBlob.OpenReadAsync().Result;
            return File(blobStream, blockBlob.Properties.ContentType, blockBlob.Name);

        }

        [HttpPost("azure/get/package")]
        public async Task<IActionResult> GetAssetBundleFile(PackageGetDto packageGetDto)
        {
            CloudBlockBlob blockBlob;
            try{
            await using (MemoryStream memoryStream = new MemoryStream())
            {
                string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerName"));
                blockBlob = cloudBlobContainer.GetBlockBlobReference(packageGetDto.FileName);
                await blockBlob.DownloadToStreamAsync(memoryStream);
            }
            Stream blobStream = blockBlob.OpenReadAsync().Result;
            return File(blobStream, blockBlob.Properties.ContentType, blockBlob.Name);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return BadRequest();

        }
        [DisableRequestSizeLimit]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [HttpPost("azure/post/upload")]
        public async Task<IActionResult> UploadFile(IFormFile body)
        {
            
            string systemFileName = body.FileName;
            string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);

            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerName"));
  
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);
            await using (var data = body.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(data);
            }
            return Ok("File Uploaded Successfully");
        }

    }
}