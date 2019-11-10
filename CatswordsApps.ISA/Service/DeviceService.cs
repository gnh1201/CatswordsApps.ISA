using CatswordsApps.ISA.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CatswordsApps.ISA.Service
{
    public static class DeviceService
    {
        public class ResponseModel
        {
            public List<DeviceModel> Data { get; set; }
        }

        public class RequestModel
        {
            public List<DeviceModel> Data { get; set; }
        }

        public static List<DeviceModel> Data { get; set; }

        public static void DataIn()
        {
            var client = new RestClient(APIService.GetURL());
            var request = new RestRequest("/", Method.GET);
            request.AddParameter("route", "isa.device");
            IRestResponse response = client.Execute(request);
            JsonDeserializer deserial = new JsonDeserializer();
            ResponseModel res = deserial.Deserialize<ResponseModel>(response);
            Data = res.Data;
        }

        public static Boolean DataOut(List<DeviceModel> rows)
        {
            RequestModel req = new RequestModel();
            req.Data = rows;

            var client = new RestClient(APIService.GetURL("isa.device.up"));
            var request = new RestRequest("/", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(req), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
