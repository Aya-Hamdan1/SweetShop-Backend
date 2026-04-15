namespace SweetStore.ViewModels.Order
{
    public class DashboardDto
    {
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public string BestProductName { get; set; }
        public int BestProductSales { get; set; }

        public decimal TodaySales { get; set; }
        public List<TopProductDto> TopProducts { get; set; }

    }

    public class TopProductDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
