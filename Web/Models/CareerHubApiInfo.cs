using CareerHub.Client.API;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Web.Models {
    public class CareerHubApiInfo {
        public static string BaseUrl {
            get { return ConfigurationManager.AppSettings["ApiLocation"]; }
        }
        
        private static AsyncLazy<APIInfo> studentsInfo = new AsyncLazy<APIInfo>(() => {
            string baseUrl = CareerHubApiInfo.BaseUrl;
            return APIInfo.GetFromRemote(baseUrl, ApiArea.Students);
        });

        public static async Task<APIInfo> GetStudentsInfo() {
            return await studentsInfo;
        }
    }
}