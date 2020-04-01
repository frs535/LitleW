using System;
using LitleW.Helpers;
using LitleW.Services;
using System.Collections.Generic;
using LitleW.Models.Base;

namespace LitleW
{
    public class GlobalSetting
    {
        public const string DefaultEndpoint = "https://dev.deskit.ru";
        private readonly IRequestProvider _requestProvider;
        private const string baseUrl = "https://dev.deskit.ru/prommix_vp_full/hs";
        
        public GlobalSetting()
        {
            _requestProvider = new RequestProvider();

            ServiceEndPoint = UriHelper.CombineUri(baseUrl, "Warehouse");
        }

        public static GlobalSetting Instance { get; } = new GlobalSetting();

        public IRequestProvider RequestProvider
        {
            get { return _requestProvider; }
        }

        public bool IsAuthenticated { get; set; }

        public string ServiceEndPoint { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool UseSeries { get; set; }

        public bool UseSpecifications { get; set; }
    }
}
