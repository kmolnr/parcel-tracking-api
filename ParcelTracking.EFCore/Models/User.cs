namespace ParcelTracking.EFCore.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public List<Parcel> Parcels { get; set; }
    }
}
