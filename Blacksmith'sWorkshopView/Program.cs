using Blacksmith_sWorkshopBusinessLogic.BusinessLogics;
using Blacksmith_sWorkshopBusinessLogic.HelperModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopDatebaseImplement.Implements;
using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace Blacksmith_sWorkshopView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            MailLogic.MailConfig(new MailConfig 
            { 
                SmtpClientHost = ConfigurationManager.AppSettings["SmtpClientHost"], 
                SmtpClientPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
                MailLogin = ConfigurationManager.AppSettings["MailLogin"], 
                MailPassword = ConfigurationManager.AppSettings["MailPassword"], 
            });

            // создаем таймер             
            var timer = new System.Threading.Timer(new TimerCallback(MailCheck), 
                new MailCheckInfo {                 
                    PopHost = ConfigurationManager.AppSettings["PopHost"],   
                    PopPort = Convert.ToInt32(ConfigurationManager.AppSettings["PopPort"]),         
                    Logic = container.Resolve<IMessageInfoLogic>()       
                }, 0, 100000); 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IBilletLogic, BilletLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductLogic, ProductLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientLogic, ClientLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementerLogic, ImplementerLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMessageInfoLogic, MessageInfoLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<MainLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
        private static void MailCheck(object obj) 
        { 
            MailLogic.MailCheck((MailCheckInfo)obj); 
        }
    }
}
