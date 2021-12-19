using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParcelTracking.API.Services.ParcelTracking;

namespace ParcelTracking.API.Controllers
{
    [Authorize("User")]
    [Route("api/track")]
    public class ParcelTrackingController : Controller
    {
        private readonly IParcelTrackingService parcelTrackingService;

        public ParcelTrackingController(IParcelTrackingService parcelTrackingService)
        {
            this.parcelTrackingService = parcelTrackingService;
        }

        [HttpGet("parcels")]
        public List<string> GetParcels()
        {
            var userName = this.HttpContext.User.Identity?.Name;

            return this.parcelTrackingService.GetParcels(userName).Select(p => p.Code).ToList();
        }

        [HttpGet("parcel")]
        public string GetParcel(string parcelNumber)
        {
            var userName = this.HttpContext.User.Identity?.Name;

            return this.parcelTrackingService.GetParcel(userName, parcelNumber)?.Code;
        }

        [HttpGet("parcel/products")]
        public List<string> GetProducts(string parcelNumber)
        {
            var userName = this.HttpContext.User.Identity?.Name;

            return this.parcelTrackingService.GetProducts(userName, parcelNumber).Select(p => p.Name).ToList();
        }

        [HttpGet("parcel/status/current")]
        public string GetCurrentStatus(string parcelNumber)
        {
            var userName = this.HttpContext.User.Identity?.Name;

            return this.parcelTrackingService.GetCurrentStatus(userName, parcelNumber)?.HungarianDescription;
        }

        [HttpGet("parcel/status/all")]
        public List<string> GetAllStatus(string parcelNumber)
        {
            var userName = this.HttpContext.User.Identity?.Name;

            return this.parcelTrackingService.GetAllStatus(userName, parcelNumber).Select(p => p.HungarianDescription).ToList();
        }
    }
}
