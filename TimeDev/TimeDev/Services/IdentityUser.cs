using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TimeDev.Services
{
    public class IdentityUser
    {
        public async Task LoginUser()
        {
            var authResult = await WebAuthenticator.AuthenticateAsync(
        new Uri("https://mysite.com/mobileauth/Microsoft"),
        new Uri("myapp://"));

            var accessToken = authResult?.AccessToken;
        }
    }
}
