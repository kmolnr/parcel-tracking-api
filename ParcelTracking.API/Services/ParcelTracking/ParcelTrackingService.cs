using ParcelTracking.EFCore;
using ParcelTracking.EFCore.Models;

namespace ParcelTracking.API.Services.ParcelTracking
{
	public class ParcelTrackingService : IParcelTrackingService
	{
		private readonly ParcelTrackingDbContext context;

		public ParcelTrackingService(ParcelTrackingDbContext context)
		{
			this.context = context;
		}

		public Parcel GetParcel(string userName, string code)
        {
			return this.context.Parcel.SingleOrDefault(p => p.User.Name == userName && p.Code == code);
		}

		public IEnumerable<Parcel> GetParcels(string userName)
		{
			return this.context.Parcel.Where(p => p.User.Name == userName);
		}

		public IEnumerable<Product> GetProducts(string userName, string code)
        {
			return this.context.Product.Where(p => p.ParcelItems.Any(i => i.Parcel.User.Name == userName && i.Parcel.Code == code));
		}

		public ParcelStatus GetCurrentStatus(string userName, string code)
        {
			return this.context.ParcelStatus
				.Where(p => p.ParcelParcelStatus.Any(i => i.Parcel.User.Name == userName && i.Parcel.Code == code))
				.OrderByDescending(p => p.ParcelStatusId)
				.FirstOrDefault();
		}

		public IEnumerable<ParcelStatus> GetAllStatus(string userName, string code)
        {
			return this.context.ParcelStatus
				.Where(p => p.ParcelParcelStatus.Any(i => i.Parcel.User.Name == userName && i.Parcel.Code == code))
				.OrderBy(p => p.ParcelStatusId);
		}
	}
}
