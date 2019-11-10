using CatswordsApps.ISA.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace CatswordsApps.ISA.Service
{
    public static class BundleService
    {
        public class ResponseModel
        {
            public List<BundleModel> Data { get; set; }
        }

        public class RequestModel
        {
            public List<BundleModel> Data { get; set; }
        }

        public static List<BundleModel> Data { get; set; }

        public static void DataIn()
        {
            var client = new RestClient(APIService.GetURL());
            var request = new RestRequest("/", Method.GET);
            request.AddParameter("route", "isa.bundle");
            IRestResponse response = client.Execute(request);
            JsonDeserializer deserial = new JsonDeserializer();
            ResponseModel res = deserial.Deserialize<ResponseModel>(response);
            Data = res.Data;
        }

        public static Boolean DataOut(List<BundleModel> rows)
        {
            RequestModel req = new RequestModel();
            req.Data = rows;

            var client = new RestClient(APIService.GetURL("isa.bundle.up"));
            var request = new RestRequest("/", Method.POST) {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(req), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
