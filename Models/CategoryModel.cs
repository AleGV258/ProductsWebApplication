namespace IGRTEC_WEBApplication_MichellAlejandroGarciaVargas.Models
{
    public class CategoryModel
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? image { get; set; }
        public string? creationAt { get; set; }
        public string? updatedAt { get; set; }
    }
    public class UploadedImageResponse
    {
        public string? originalname { get; set; }
        public string? filename { get; set; }
        public string? location { get; set; }
    }
}
