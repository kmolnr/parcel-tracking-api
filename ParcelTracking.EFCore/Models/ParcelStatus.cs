namespace ParcelTracking.EFCore.Models
{
    public class ParcelStatus
    {
        public int ParcelStatusId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string HungarianDescription { get; set; }

        public ICollection<ParcelParcelStatus> ParcelParcelStatus { get; set; }
    }
}
