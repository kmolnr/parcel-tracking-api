namespace ParcelTracking.EFCore.Models
{
    public class ParcelItem
    {
        public int ParcelItemId { get; set; }

        public int ParcelId { get; set; }

        public Parcel Parcel { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
