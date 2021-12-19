using ParcelTracking.EFCore.Models;

namespace ParcelTracking.API.Services.ParcelTracking
{
	public interface IParcelTrackingService
	{
		Parcel GetParcel(string userName, string code);

		IEnumerable<Parcel> GetParcels(string userName);

		IEnumerable<Product> GetProducts(string userName, string code);

		ParcelStatus GetCurrentStatus(string userName, string code);

		IEnumerable<ParcelStatus> GetAllStatus(string userName, string code);
	}
}
