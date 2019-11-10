using CatswordsApps.ISA.Model;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatswordsApps.ISA.Service
{
    public static class AssignService
    {
        public class ResponseModel
        {
            public List<AssignModel> Data { get; set; }
        }

        public class RequestModel
        {
            public List<AssignModel> Data { get; set; }
        }

        public static List<AssignModel> Data { get; set; }

        public static string AssignNumber { get; set; }

        public static void DataIn()
        {
            var client = new RestClient(APIService.GetURL());
            var request = new RestRequest("/", Method.GET);
            request.AddParameter("route", "isa.assign");
            request.AddParameter("assign_number", AssignNumber);
            IRestResponse response = client.Execute(request);
            JsonDeserializer deserial = new JsonDeserializer();
            ResponseModel res = deserial.Deserialize<ResponseModel>(response);
            Data = res.Data;
        }
    }
}
