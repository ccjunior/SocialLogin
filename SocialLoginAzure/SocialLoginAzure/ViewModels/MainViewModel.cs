using SocialLoginAzure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialLoginAzure.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        private string _tokenId;
        public string Token
        {
            get { return _tokenId; }
            set { SetProperty(ref _tokenId, value); }
        }

        public MainViewModel()
        {
            Title = "Página autenticada";
            UserId = Settings.UserId;
            Token = Settings.AuthToken;
        }
    }
}
