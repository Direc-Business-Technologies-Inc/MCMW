using DirecLayer;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace ServiceLayer
{
    public class SboServiceLayer
    {
        public string ErrMsg { get; set; }
        public bool Initialize()
        {
            var sboCred = new SboCredentials();
            string url = ApiHelper.ServiceURL_Update(sboCred.ServiceLayer);
            ApiHelper.ServiceLayer.open("POST", $"{url}");
            try
            {
                ApiHelper.ServiceLayer.send();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Login()
        {
            var output = false;
            try
            {
                var sboCred = new SboCredentials();
                var model = new List<ILoginModel>();

                model.Add(new LoginModel
                {
                    CompanyDB = sboCred.CompanyDB,
                    UserName = sboCred.UserName,
                    Password = sboCred.Password
                });

                string url = ApiHelper.ServiceURL_Update(sboCred.ServiceLayer);

                ApiHelper.ServiceLayer.open("POST", $@"{url}Login");

                string json = new JavaScriptSerializer().Serialize(model).ToString();

                if (json.Length > 2)
                {
                    json = json.Substring(1, json.Length - 2);
                }
                ApiHelper.ServiceLayer.send(json);

                ErrMsg = JsonHelper.GetJsonValue(ApiHelper.ServiceLayer.responseText, "SessionId");

                output = IsLoginSuccess(ErrMsg);
            }
            catch (Exception ex)
            {
                ErrMsg = $"Error : Service Layer Access Return Login {ex.Message}";
                throw new Exception(ErrMsg);
            }
            return output;
        }

        public bool Login(out string sMessage)
        {
            var output = false;
            try
            {
                var sboCred = new SboCredentials();
                var model = new List<ILoginModel>();

                model.Add(new LoginModel
                {
                    CompanyDB = sboCred.CompanyDB,
                    UserName = sboCred.UserName,
                    Password = sboCred.Password
                });

                string url = ApiHelper.ServiceURL_Update(sboCred.ServiceLayer);

                ApiHelper.ServiceLayer.open("POST", $@"{url}Login");

                string json = new JavaScriptSerializer().Serialize(model).ToString();

                if (json.Length > 2)
                {
                    json = json.Substring(1, json.Length - 2);
                }
                ApiHelper.ServiceLayer.send(json);

                sMessage = JsonHelper.GetJsonValue(ApiHelper.ServiceLayer.responseText, "SessionId");

                output = IsLoginSuccess(sMessage);
            }
            catch (Exception ex)
            {
                sMessage = $"Error : Service Layer Access Return Login {ex.Message}";
                throw new Exception(sMessage);
            }
            return output;
        }

        public bool Logout()
        {
            var output = false;
            try
            {
                var sboCred = new SboCredentials();
                string url = ApiHelper.ServiceURL_Update(sboCred.ServiceLayer);

                ApiHelper.ServiceLayer.open("POST", $@"{url}Logout");
                ApiHelper.ServiceLayer.send();
                output = ApiHelper.ServiceLayer.responseText.Equals("") ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return output;
        }

        bool IsLoginSuccess(string sResponse)
        {
            var output = false;
            try
            {
                if (string.IsNullOrEmpty(sResponse))
                { output = false; }
                else
                {
                    //Logout();
                    output = sResponse.Contains("-");
                }
            }
            catch (Exception ex)
            { throw new Exception($"Error : Service Layer Access Return IsLoginSuccess {ex.Message}"); }

            return output;
        }
    }
}
