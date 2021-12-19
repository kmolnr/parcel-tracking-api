namespace ParcelTracking.EFCore.Models
{
    public class ParcelParcelStatus
    {
        public int ParcelId { get; set; }

        public Parcel Parcel { get; set; }

        public int ParcelStatusId { get; set; }

        public ParcelStatus ParcelStatus { get; set; }
    }
}
