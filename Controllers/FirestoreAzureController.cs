using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using System.Xml;
using auth.Models;
using static auth.Models.AddFurnitureDto;
using static auth.Models.AddFurnitureDto.FileDto;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Google.Cloud.Firestore.V1;
using System.Text.Json;

namespace auth.Controllers
{
    [Route("services")]
    [ApiController]
    public class FirestoreAzureController : Controller
    {
        private FirestoreDb _firestoreDb;
        private readonly IConfiguration _configuration;

        public FirestoreAzureController(IConfiguration configuration)
        {
            _configuration = configuration;
            var jsonString = _configuration.GetValue<string>("FirestoreAuthSecret");
            var builder = new FirestoreClientBuilder {JsonCredentials = jsonString};
            this._firestoreDb = FirestoreDb.Create("lucrare-licenta-proba",  builder.Build());
            
        }

        [HttpPost("add/furniture")]
        public IActionResult AddFurniture(AddFurnitureDto addFurnitureDto)
        {
            DocumentReference companyNameDocumentReference = _firestoreDb.Collection("companies").Document(addFurnitureDto.CompanyName);
            Dictionary<string, object> data = new Dictionary<string, object>(){};
            companyNameDocumentReference.SetAsync(data);

            foreach (FileDto file in addFurnitureDto.Files)
            {
                DocumentReference fileDocumentReference = companyNameDocumentReference.Collection("Files").Document(file.FileName);
                data = new Dictionary<string, object>(){};
                fileDocumentReference.SetAsync(data);

                if (file.FileName.EndsWith(".unity3d"))
                {
                    CollectionReference furniturePiecesCollectionReference = fileDocumentReference.Collection("FurniturePieces");

                    foreach (FurniturePieceDto furniturePieceDto in file.FurniturePieces)
                    {
                        fileDocumentReference.SetAsync(data);
                        var furnitureData = new Dictionary<string, object>() {
                            {"Name", furniturePieceDto.Name},
                            {"CompanyName", furniturePieceDto.CompanyName},
                            {"Price", furniturePieceDto.Price},
                            {"Room", furniturePieceDto.Room}
                        };
                        furniturePiecesCollectionReference.AddAsync(furnitureData);
                    }
                }
            }
            return Ok();
        }

        [HttpGet("get/furniture")]
        public async Task<IActionResult> GetAllData()
        {
            Query allFurnitures = _firestoreDb.Collection("companies");
            QuerySnapshot allFurnituresQuerySnapshot = await allFurnitures.GetSnapshotAsync();
            Dictionary<string, Dictionary<string, List<FurniturePieceDto>>> data = new Dictionary<string, Dictionary<string, List<FurniturePieceDto>>>();
            foreach (DocumentSnapshot documentSnapshot in allFurnituresQuerySnapshot.Documents)
            {
                Query allFiles = _firestoreDb.Collection("companies").Document(documentSnapshot.Id).Collection("Files");
                QuerySnapshot allFilesQuerySnapshot = await allFiles.GetSnapshotAsync();
                Dictionary<string, List<FurniturePieceDto>> fileData = new Dictionary<string, List<FurniturePieceDto>>();

                foreach (DocumentSnapshot fileDocumentSnapshot in allFilesQuerySnapshot.Documents)
                {
                    List<FurniturePieceDto> furnitureList = new List<FurniturePieceDto>();

                    if (fileDocumentSnapshot.Id.EndsWith(".unity3d"))
                    {
                        Query allFurniturePieces = _firestoreDb
                        .Collection("companies")
                        .Document(documentSnapshot.Id)
                        .Collection("Files")
                        .Document(fileDocumentSnapshot.Id)
                        .Collection("FurniturePieces");

                        QuerySnapshot allFurniturePiecesQuerySnapshot = await allFurniturePieces.GetSnapshotAsync();

                        foreach (DocumentSnapshot furnitureDocumentSnapshot in allFurniturePiecesQuerySnapshot.Documents)
                        {
                            Dictionary<string, object> furniturePieces = furnitureDocumentSnapshot.ToDictionary();
                            furnitureList.Add(new FurniturePieceDto
                            {
                                Name = (string)furniturePieces["Name"],
                                CompanyName = (string)furniturePieces["CompanyName"],
                                Price = (float)((double)furniturePieces["Price"]),
                                Room = (string)furniturePieces["Room"],
                            });

                        }
                    }

                    fileData.Add(fileDocumentSnapshot.Id, furnitureList);
                }

                data.Add(documentSnapshot.Id, fileData);
            }
            return Ok(data);
        }


    }
}