using Microsoft.WindowsAzure.MobileServices;
using SocialLoginAzure.Helpers;
using SocialLoginAzure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AzureService))]
namespace SocialLoginAzure.Services
{
    public class AzureService
    {
        static readonly string AppUrl = "https://socialloginmaratonajunior.azurewebsites.net";

        public MobileServiceClient Client { get; set; }

        public static bool UserAuth { get; set; }

        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);

            if (!string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.AuthToken
                };
            }
        }

        public async Task<bool> LoginAsync()
        {

            Initialize();
            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

                if (user == null)
                {
                    Settings.AuthToken = string.Empty;
                    Settings.UserId = string.Empty;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.Current.MainPage.DisplayAlert("Ops!", "Não conseguimos realizar o seu login, tente novamente", "Ok");
                    });

                    return false;
                }
                else
                {
                    Settings.AuthToken = user.MobileServiceAuthenticationToken;
                    Settings.UserId = user.UserId;
                }
           
          
            return true;
        }
    }
}
