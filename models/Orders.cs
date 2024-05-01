namespace Models
{
    public class Orders
    {
        public int id { get; set; }
        public int shake_id { get; set; }
        public string product { get; set; }
        public int quantity { get; set; }
        public string variant { get; set; }
        public decimal amount { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
    public class AddOrder
    {
        public int shake_id { get; set; }
        public string product { get; set; }
        public int quantity { get; set; }
        public string variant { get; set; }
        public decimal amount { get; set; }
    }
}