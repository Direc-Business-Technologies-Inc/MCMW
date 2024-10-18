using DirecLayer;
using DomainLayer.SAP_DATABASE;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Windows.Forms;


namespace ServiceLayer
{
    public class Orders
    {
        public string ErrMsg { get; set; }

        public bool Post(object model,
                        string posting)
        {
            try
            {
                SboServiceLayer sbo = new SboServiceLayer();
                if (sbo.Login())
                {
                    var sboCred = new SboCredentials();
                    string url = ApiHelper.ServiceURL_Update(sboCred.ServiceLayer);
                    ApiHelper.ServiceLayer.open("POST", $@"{url}{posting}");

                    string json = new JavaScriptSerializer().Serialize(model).ToString();

                    json = JsonHelper.RemoveNullorEmptyJsonValue(json);

                    if (json.Length > 2)
                    {
                        json = json.Substring(1, json.Length - 2);
                    }
                    ApiHelper.ServiceLayer.send(json);

                    string result = JsonHelper.GetJsonValue(ApiHelper.ServiceLayer.responseText, posting.Contains("Orders") || posting.Contains("Quotations") ? "DocEntry" : "Code");
                    if (result != null && result.Contains("error"))
                    {
                        MessageBox.Show(result);
                    }
                    return result != null && result.Contains("error") ? false : true;

                }
                else
                {
                    ErrMsg = sbo.ErrMsg;
                    MessageBox.Show(ErrMsg);
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {posting} - {ex.Message}");
            }
        }

        public bool Patch(List<ORDR> ordr, int docEntry)
        {
            SboServiceLayer sbo = new SboServiceLayer();
            if (sbo.Login())
            {
                var sboCred = new SboCredentials();
                string url = ApiHelper.ServiceURL_Update(sboCred.ServiceLayer);
                ApiHelper.ServiceLayer.open("PATCH", $@"{url}Orders({docEntry})");

                ordr.RemoveAll(item => item == null && item.U_Weight2 != 0);

                string json = new JavaScriptSerializer().Serialize(ordr).ToString();
                json = JsonHelper.RemoveNullorEmptyJsonValue(json);

                if (json.Length > 2)
                {
                    json = json.Substring(1, json.Length - 2);
                }

                ApiHelper.ServiceLayer.setRequestHeader("B1S-ReplaceCollectionsOnPatch", "true");

                ApiHelper.ServiceLayer.send(json);

                string result = JsonHelper.GetJsonValue(ApiHelper.ServiceLayer.responseText, "DocEntry");
                if (result.Contains("error"))
                {
                    MessageBox.Show(result);
                }
                return result.Contains("error") ? false : true;
            }
            else
            {
                ErrMsg = sbo.ErrMsg;
                MessageBox.Show(ErrMsg);
                return false;
            }
        }


        public bool PatchString(string ordr, int docEntry, string transtype)
        {
            SboServiceLayer sbo = new SboServiceLayer();
            if (sbo.Login())
            {
                var sboCred = new SboCredentials();
                string url = ApiHelper.ServiceURL_Update(sboCred.ServiceLayer);
                if (transtype == "SO")
                {
                    ApiHelper.ServiceLayer.open("PATCH", $@"{url}Orders({docEntry})");
                }
                else
                {
                    ApiHelper.ServiceLayer.open("PATCH", $@"{url}Quotations({docEntry})");
                }

                ApiHelper.ServiceLayer.setRequestHeader("B1S-ReplaceCollectionsOnPatch", "true");

                ApiHelper.ServiceLayer.send(ordr);

                string result = JsonHelper.GetJsonValue(ApiHelper.ServiceLayer.responseText, "DocEntry");
                if (result.Contains("error"))
                {
                    MessageBox.Show(result);
                }
                return result.Contains("error") ? false : true;
            }
            else
            {
                ErrMsg = sbo.ErrMsg;
                MessageBox.Show(ErrMsg);
                return false;
            }
        }

        public bool PostString(string json, string table)
        {
            SboServiceLayer sbo = new SboServiceLayer();
            if (sbo.Login())
            {
                var sboCred = new SboCredentials();
                string url = ApiHelper.ServiceURL_Update(sboCred.ServiceLayer);
                ApiHelper.ServiceLayer.open("POST", $@"{url}{table}");

                ApiHelper.ServiceLayer.send(json);

                string result = JsonHelper.GetJsonValue(ApiHelper.ServiceLayer.responseText, "Code");
                if (result.Contains("error"))
                {
                    MessageBox.Show(result);
                }
                return result.Contains("error") ? false : true;
            }
            else
            {
                ErrMsg = sbo.ErrMsg;
                MessageBox.Show(ErrMsg);
                return false;
            }
        }
    }
}