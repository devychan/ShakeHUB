namespace Models
{
    public class Flavors : Variants
    {
        public int id { get; set; }
        public string flavor { get; set; }
        public decimal price { get; set; }
        public int type { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}