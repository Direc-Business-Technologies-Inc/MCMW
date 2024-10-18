using DirecLayer;
using SAPbobsCOM;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;

namespace ServiceLayer
{
    public class SboDataInterfaceAPI
    {
        public string ErrMsg { get; set; }

        public bool Initialize(string licenseServer,
                                string server,
                                bool useTrusted,
                                string dbUsername,
                                string dbPassword,
                                string dbServerType)
        {
            ApiHelper.oCompany = new Company()
            {
                LicenseServer = licenseServer,
                Server = server,
                language = BoSuppLangs.ln_English,
                UseTrusted = useTrusted,
                DbUserName = dbUsername,
                DbPassword = dbPassword,
                DbServerType = (BoDataServerTypes)Enum.Parse(typeof(BoDataServerTypes), dbServerType),
            };
            return true;
        }

        public bool Initialize(string licenseServer,
                                string server,
                                string dbUsername,
                                string dbPassword,
                                string dbServerType)
        {
            ApiHelper.oCompany = new Company()
            {
                LicenseServer = licenseServer,
                Server = server,
                language = BoSuppLangs.ln_English,
                UseTrusted = false,
                DbUserName = dbUsername,
                DbPassword = dbPassword,
                DbServerType = (BoDataServerTypes)Enum.Parse(typeof(BoDataServerTypes), dbServerType),
            };
            return true;
        }

        public bool Initialize(string licenseServer,
                                string server,
                                string dbServerType)
        {
            try
            {
                ApiHelper.oCompany = new Company()
                {
                    LicenseServer = licenseServer,
                    Server = server,
                    language = BoSuppLangs.ln_English,
                    UseTrusted = true,
                    DbServerType = (BoDataServerTypes)Enum.Parse(typeof(BoDataServerTypes), dbServerType),
                };
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool IsConnected()
        {
            return ApiHelper.oCompany != null ? ApiHelper.oCompany.Connected : false;
        }

        public bool Login()
        {
            var output = false;
            try
            {
                var sboCred = new SboCredentials();

                if (IsConnected())
                {
                    ApiHelper.oCompany.CompanyDB = sboCred.CompanyDB;
                    ApiHelper.oCompany.UserName = sboCred.UserName;
                    ApiHelper.oCompany.Password = sboCred.Password;

                    ApiHelper.lRetCode = ApiHelper.oCompany.Connect();
                    if (ApiHelper.lRetCode != 0)
                    { ErrMsg = ApiHelper.oCompany.GetLastErrorDescription(); }
                    else
                    {
                        {
                            ErrMsg = $"You are now connected to {ApiHelper.oCompany.CompanyName}.";
                        }
                        output = true;
                    }
                }
                else
                {
                    ErrMsg = $"Error : Please do not";
                }
            }
            catch (Exception ex)
            {
                ErrMsg = $"Error : Data Interface API Access Return Login {ex.Message}";
            }

            return output;
        }

        public bool Login(out string sMessage)
        {
            var output = false;
            try
            {
                var sboCred = new SboCredentials();

                if (IsConnected())
                {
                    ApiHelper.oCompany.CompanyDB = sboCred.CompanyDB;
                    ApiHelper.oCompany.UserName = sboCred.UserName;
                    ApiHelper.oCompany.Password = sboCred.Password;

                    ApiHelper.lRetCode = ApiHelper.oCompany.Connect();
                    if (ApiHelper.lRetCode != 0)
                    { sMessage = ApiHelper.oCompany.GetLastErrorDescription(); }
                    else
                    {
                        {
                            sMessage = $"You are now connected to {ApiHelper.oCompany.CompanyName}.";
                            ErrMsg = sMessage;
                        }
                        output = true;
                    }
                }
                else
                {
                    sMessage = $"Error : Please do not";
                    ErrMsg = sMessage;
                }
            }
            catch (Exception ex)
            {
                sMessage = $"Error : Data Interface API Access Return Login {ex.Message}";
                ErrMsg = sMessage;
            }

            return output;
        }

        public bool Logout()
        {
            if (IsConnected())
            {
                ApiHelper.oCompany.Disconnect();
                return true;
            }
            else { return false; }
        }
    }
}
