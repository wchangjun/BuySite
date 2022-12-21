using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Helper
{
    public class AppInfo
    {
        private IConfiguration _configruration;
        public AppInfo(IConfiguration configruration)
        {
            _configruration = configruration;
        }

        public string BuySite()
        {
            return _configruration.GetConnectionString("BuySite");
        }
        ////連接字串
        //public string BuySiteConnectionStrings() { return configruration["ConnectionStrings:BuySite"]; }
    }
}
