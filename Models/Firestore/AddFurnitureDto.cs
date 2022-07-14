namespace auth.Models
{
    
    public class AddFurnitureDto
    {
        public string CompanyName { get; set; }
        public List<FileDto> Files { get; set; }

        public class FileDto
        {
            public string FileName { get; set; }
            public List<FurniturePieceDto> FurniturePieces { get; set; }

            public class FurniturePieceDto
            {
                public string Name { get; set; }
                public string CompanyName { get; set; }
                public float Price { get; set; }
                public string Room { get; set; }
            }
        }
    }
}