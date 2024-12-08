namespace Resource3.Models
{
    public class ProductReport
    {
        public int Id { get; set; }
        public int OrderReportId { get; set; }
        public int ProductId { get; set; }
        public int TotalSold { get; set; }
        public decimal Revenue { get; set; } // total price with product have same id
        public decimal Cost { get; set; } // total cost from product
        public decimal profit { get; set; }

    }
}
