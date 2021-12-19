namespace ParcelTracking.EFCore.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public List<ParcelItem> ParcelItems { get; set; }
    }
}
