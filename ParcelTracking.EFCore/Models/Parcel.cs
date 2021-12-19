namespace ParcelTracking.EFCore.Models
{
    public class Parcel
    {
        public int ParcelId { get; set; }

        public string Code { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<ParcelItem> ParcelItems { get; set; }

        public ICollection<ParcelParcelStatus> ParcelParcelStatus { get; set; }
    }
}
