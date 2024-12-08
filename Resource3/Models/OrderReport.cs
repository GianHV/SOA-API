namespace Resource3.Models
{
    public class OrderReport
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal TotalRevenue { get; set; } // sum: all revenue from product_reports
        public decimal TotalCost { get; set; } // sum: all cost from product_reports
        public decimal TotalProfit { get; set; }
    }
}
