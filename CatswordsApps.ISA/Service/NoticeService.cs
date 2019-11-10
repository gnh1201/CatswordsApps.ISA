using CatswordsApps.ISA.Model;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CatswordsApps.ISA.Service
{
    public static class NoticeService
    {
        public class ResponseModel
        {
            public List<NoticeModel> Data { get; set; }
        }

        public static List<NoticeModel> Data { get; set; }

        public static void DataIn()
        {
            var client = new RestClient(APIService.GetURL());
            var request = new RestRequest("/", Method.GET);
            request.AddParameter("route", "isa.notice");
            IRestResponse response = client.Execute(request);
            JsonDeserializer deserial = new JsonDeserializer();
            ResponseModel res = deserial.Deserialize<ResponseModel>(response);
            Data = res.Data;
        }
    }
}
