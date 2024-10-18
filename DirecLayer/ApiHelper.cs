using DirecLayer.Models;
using MSXML2;
using RestSharp;
//using SAPbobsCOM;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace DirecLayer
{
    public class ApiHelper
    {
        #region WebRequest
        public static HttpWebRequest CreateWebRequest(string sURL, string sMethod)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create($"{sURL}");
            webRequest.ContentType = "application/json";
            webRequest.Accept = "application/json";
            webRequest.Method = sMethod;

            if (sURL.StartsWith("https://"))
            {
                webRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            }
            return webRequest;
        }

        public static void SendWebRequest(HttpWebRequest request, StringBuilder json)
        {
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        public static string ResponseWebRequest(HttpWebRequest request)
        {
            var output = "";
            var httpResponse = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                output = streamReader.ReadToEnd();
            }

            return output;
        }
        #endregion

        #region RestClient
        static RestClient client { get; set; }
        static RestRequest request { get; set; }
        static IRestResponse response { get; set; }

        public static string Request(string linkServer,
                                    int method,
                                    string module,
                                    StringBuilder json)
        {
            RestClient client = new RestClient();
            client = restClient(linkServer);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            request = new RestRequest(module, (Method)method);
            request.AddJsonBody(json.ToString());
            response = client.Execute(request);
            return response.Content;
        }


        static RestClient restClient(string linkServer)
        {
            return client == null ? client = new RestClient(linkServer) : client;
        }
        #endregion

        #region XMLHTTP
        public static XMLHTTP60 ServiceLayer { get; set; }
        public static string ServiceURL_Update(string url)
        {
            var output = string.Empty;
            try
            {
                const string httpStr = "http://";
                const string httpsStr = "https://";

                if (!url.StartsWith(httpStr, true, null) &&
                        !url.StartsWith(httpsStr, true, null))
                {
                    output = $"{httpStr}{url}/b1s/v1/";
                }
                else
                {
                    output = $"{url}/b1s/v1/";
                }

                if (ServiceLayer == null)
                { ServiceLayer = new XMLHTTP60(); }
            }
            catch (Exception ex)
            { throw new Exception($"Error : Service Layer Access Return Service URL {ex.Message}"); }
            return output;
        }


        #endregion

        #region DataInterfaceAPI
        //public static Company oCompany { get; set; }
        //public static long lRetCode { get; set; }

        //public static void DocumentHeader(Documents document,
        //                                    DocumentHeaderModel model)
        //{
        //    if (Validation.isNull(model.CardCode))
        //    {
        //        document.CardCode = model.CardCode;
        //    }

        //    if (Validation.isNull(model.CardName))
        //    {
        //        document.CardName = model.CardName;
        //    }

        //    if (Validation.isNull(model.DocDate == null ? "" : model.DocDate.ToString()))
        //    {
        //        document.DocDate = model.DocDate.GetValueOrDefault(DateTime.Now);
        //    }

        //    if (Validation.isNull(model.DocDueDate == null ? "" : model.DocDueDate.ToString()))
        //    {
        //        document.DocDueDate = model.DocDueDate.GetValueOrDefault(DateTime.Now);
        //    }

        //    if (Validation.isNull(model.TaxDate == null ? "" : model.TaxDate.ToString()))
        //    {
        //        document.TaxDate = model.TaxDate.GetValueOrDefault(DateTime.Now);
        //    }

        //    if (Validation.isNull(model.Project))
        //    {
        //        document.Project = model.Project;
        //    }

        //    document.UserFields.Fields.Item("U_Remarks").Value = $"Created by EasySAP | Sales Order : {DateTime.Now} | Powered By : DIREC";

        //}
        #endregion


    }
}
