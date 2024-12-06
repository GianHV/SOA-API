namespace Resource2.Models
{
    public class OrderDTO
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
    }
    public class OrderPutDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
