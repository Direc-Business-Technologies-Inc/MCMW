using ServiceLayer;
using DomainLayer.CONTEXT;
using DomainLayer.SAO_DATABASE;
using MCWM.Views.Security;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using DomainLayer.SAP_DATABASE;
using DirecLayer;
using MCWM.Helper;

namespace MCWM
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {


                //IUnityContainer unityContainer;

                //unityContainer = new UnityContainer().
                //   RegisterType<ISecurityPresenter, SecurityPresenter>(new ContainerControlledLifetimeManager()).
                //   RegisterType<IfrmLogin, frmLogin>(new ContainerControlledLifetimeManager()).
                //   RegisterType<ILoginService, LoginService>(new ContainerControlledLifetimeManager()).
                //   RegisterType<IfrmMain, frmMain>(new ContainerControlledLifetimeManager()).
                //   RegisterType<IMainPresenter, MainPresenter>(new ContainerControlledLifetimeManager()).
                //   RegisterType<IfrmTravelOrders, frmTravelOrders>(new ContainerControlledLifetimeManager()).
                //   RegisterType<ITravelOrderPresenter, TravelOrderPresenter>(new ContainerControlledLifetimeManager()
                //   );

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //ISecurityPresenter securityPresenter = unityContainer.Resolve<SecurityPresenter>();
                //IfrmLogin form = securityPresenter.GetMainForm();
                //Application.Run((frmLogin)form);

                //string file = $@"{Application.StartupPath}\Data\";
                //string databaseName = $"{AppConfig.AppSettings("SqlDatabase")}.mdf";
                //if (SystemSettings.FolderExist(file) == false)
                //{
                //    file = SystemSettings.PathExist(file);
                //}

                //int i = MsSqlAccess.CreateMsSqlAttachmentDatabase($"{file}{databaseName}");

                Users users = new Users
                {
                    Username = "admin",
                    Password = "1234",
                    CreateDate = DateTime.Now
                };

                Series series = new Series
                {
                    WBNo = "0",
                    CreateDate = DateTime.Now
                };

                using (var ctx = new Context())
                {
                    if (!ctx.Users.Any())
                    {
                        ctx.Users.Add(users);
                        ctx.SaveChanges();
                    }

                    if (ctx.Series.Count() == 0)
                    {
                        ctx.Series.Add(series);
                        ctx.SaveChanges();
                    }
                }





                //MsSqlAccess.Execute(MsSqlAccess.ConnectionString("Addon"),
                //                       $"EXEC sp_detach_db '{AppConfig.AppSettings("SqlDatabase")}', 'true'");

                bool isConnected = false;

                bool isDiApi = bool.Parse(AppConfig.AppSettings("IsDIAPI"));

                if (isDiApi)
                {
                    //SboDataInterfaceAPI sboDataInterfaceAPI = new SboDataInterfaceAPI();
                    //isConnected = sboDataInterfaceAPI.Initialize(AppConfig.AppSettings("LicenseServer"),
                    //                                            AppConfig.AppSettings("Server"),
                    //                                            bool.Parse(AppConfig.AppSettings("UseTrusted")),
                    //                                            AppConfig.AppSettings("DbUserName"),
                    //                                            AppConfig.AppSettings("DbPassword"),
                    //                                            AppConfig.AppSettings("DbServerType"));
                }
                else
                {
                    //Check if the you can ping the server
                    if (SystemSettings.PingHost(AppConfig.AppSettings("Server")) == false)
                    {
                        //MessageBox.Show("You are not connected to the server. Local connection is activated.");
                        PublicStatic.IsConnectedToServer = false;
                        Application.Run(new frmLogin());
                    }
                    else
                    {
                        //trying to get certificate for service layer
                        SboServiceLayer sboServiceLayer = new SboServiceLayer();
                        isConnected = sboServiceLayer.Initialize();
                        if (isConnected)
                        {
                            PublicStatic.IsConnectedToServer = true;
                            Application.Run((new frmLogin()));
                        }
                        else
                        {
                            MessageBox.Show("Please accept the certificate to continue.");
                            Application.Exit();
                        }
                    }
                }

                //SboServiceLayer security = new SboServiceLayer();
                //security.Login(out string err);

                #region Ref
                //var model = new List<ORDR>();
                //var lineModel = new List<RDR1>();
                //lineModel.Add(new RDR1
                //{
                //    ItemCode = "R0001",
                //    Quantity = 1
                //});

                //lineModel.Add(new RDR1
                //{
                //    ItemCode = "R0001",
                //    Quantity = 2
                //});

                //model.Add(new ORDR
                //{
                //    CardCode = "IND000000000001",
                //    DocDate = DateTime.Now,
                //    DocDueDate = DateTime.Now,
                //    TaxDate = DateTime.Now,
                //    DocumentLines = lineModel
                //});

                //Orders orders = new Orders();
                //orders.Post(model);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
