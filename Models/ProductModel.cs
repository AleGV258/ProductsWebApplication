namespace IGRTEC_WEBApplication_MichellAlejandroGarciaVargas.Models
{
    public class ProductModel
    {
        public int? id { get; set; }
        public string? title { get; set; }
        public decimal? price { get; set; }
        public string? description { get; set; } = string.Empty;
        public Category? category { get; set; }
        public List<string>? images { get; set; } = new List<string>();
    }

    public class Category
    {
        public int id { get; set; }
        public string? name { get; set; } = string.Empty;
        public string? image { get; set; } = string.Empty;
    }
}
