namespace ParcelTracking.API.Services.Authentication
{
    public interface IAuthenticationService
    {
        User Authenticate(string userName, string password);
    }
}
