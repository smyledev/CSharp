using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IPInfoController : ControllerBase
    {
        private readonly IPInfoDbContext _dbContext;

        public IPInfoController(IPInfoDbContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetIPInfo")]
        public string Get(string ip) {
            string gettingInfo = new WebClient().DownloadString("https://ipinfo.io/" + ip + "/geo");

            List<IPInfo> IPsDB = _dbContext.IPsInfo.ToList();

            var foundedIP = from IPDB in IPsDB
                            where IPDB.Ip == ip
                            select IPDB.Ip;

            if (foundedIP.Count() == 0) {
                IPInfo ipWithInfo = new IPInfo();
                string[] lines = gettingInfo.Split('\n');
                string lineFormatted, key, value;
                string[] dataOfLine;
                foreach (string line in lines) {
                    lineFormatted = line.Replace("\\", "");
                    dataOfLine = lineFormatted.Split(':');
                    if (dataOfLine.Count() > 1) {
                        key = dataOfLine[0].Replace("\"", "").Replace(" ", "");
                        value = dataOfLine[1].Replace("\"", "").Replace(" ", "");
                        
                        if (value[value.Count() - 1] == ',')
                            value = value.Substring(0, value.Count() - 1);

                        if (key == "ip") ipWithInfo.Ip = value;
                        else if (key == "city") ipWithInfo.City = value;
                        else if (key == "region") ipWithInfo.Region = value;
                        else if (key == "country") ipWithInfo.Country = value;
                        else if (key == "loc") ipWithInfo.Loc = value;
                        else if (key == "org") ipWithInfo.Org = value;
                        else if (key == "postal") ipWithInfo.Postal = value;
                        else if (key == "timezone") ipWithInfo.Timezone = value;
                    }
                }

                if (IPsDB.Count() == 0)
                    ipWithInfo.Id = 1;
                else
                    ipWithInfo.Id = IPsDB.Max(user => user.Id) + 1;

                _dbContext.IPsInfo.Add(ipWithInfo);
                _dbContext.SaveChanges();
            }

            return gettingInfo;
        }
    }
}
