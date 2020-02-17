using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopFileImplement.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IBilletLogic, BilletLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductLogic, ProductLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainLogic, MainLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
