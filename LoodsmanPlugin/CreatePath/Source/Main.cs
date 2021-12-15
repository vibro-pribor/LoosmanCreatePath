/// <author creation-date="15 марта 2012 г.">
/// Закиров Артур
/// </author>
/// <summary>
/// Файл реализации экспортных методов плагина ЛОЦМАН
/// </summary>
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;
using DllExport;
using LoodsmanDotNet;
using System.Security.Permissions;
using System.IO;
using NLog;

namespace ASCON.Loodsman.CreatePath
{

    /// <summary>
	/// Класс для выполнения методов плагина в отдельном домене приложений.
	/// </summary>
    public class PluginDomainWorker : MarshalByRefObject
    {
		/// <summary>
		/// Логирование.
		/// </summary>
		private static readonly Logger m_Logger = LogManager.GetCurrentClassLogger();


        public int PgiCheckMenuItemCom(IntPtr stFunction, IntPtr IPC)
        {
            if (IPC != null)
            {
                IPluginCall pc = (IPluginCall)Marshal.GetTypedObjectForIUnknown(IPC, typeof(IPluginCall));
                string funcName = Marshal.PtrToStringAnsi(stFunction);
                if (funcName == "RunModule")
                    if (pc.IdVersion != 0)
                        return 1;
            }
            return 0;
        }

        public void RunModule(IntPtr IPC)
        {
            if (IPC != null)
            {
				try {
					IPluginCall pc = (IPluginCall)Marshal.GetTypedObjectForIUnknown(IPC, typeof(IPluginCall));
					ILoodsmanApplication loodsman_application = (ILoodsmanApplication)Marshal.GetTypedObjectForIUnknown(IPC, typeof(ILoodsmanApplication));

					// Бизнес логика плагина
					BusinessLogic business_logic_plugin = new BusinessLogic(pc, loodsman_application);
					business_logic_plugin.RunModule();
				}
				
				catch (Exception exc) {
					m_Logger.Error(String.Format("Произошла внутреняя ошибка работы плагина. Причина: {0}. Стек вызова: {1}", exc.Message, exc.StackTrace));
					MessageBox.Show("Произошла внутреняя ошибка работы плагина. Обратитесь к разработчику.", "Внутренняя ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
            }
        }

        /// <summary>
        /// Отключить контроль времени существования LifetimeService.
        /// </summary>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public override object InitializeLifetimeService()
        {
            return null;
        }

    }

    /// <summary>
	/// Класс Main - класс, реализующий экспортные методы плагина ЛОЦМАН.
    /// </summary>
    internal static class Main
    {
        private static PluginDomainWorker worker;

        private static AppDomain CreatePluginDomain()
        {
            var execAssembly = Assembly.GetExecutingAssembly();
            Uri assemblyFileUri = new Uri(execAssembly.CodeBase);

            var configFile = assemblyFileUri.LocalPath + ".config";

            var appSetup = new AppDomainSetup()
            {
                ApplicationBase = Path.GetDirectoryName(assemblyFileUri.LocalPath),
                ConfigurationFile = configFile
            };
            AppDomain ad = AppDomain.CreateDomain(execAssembly.GetName().Name,
                null,
                appSetup);

            worker = (PluginDomainWorker)ad.CreateInstanceAndUnwrap(
                execAssembly.FullName,
                typeof(PluginDomainWorker).FullName);

            return ad;
        }

        private static Assembly AssemblyResolve(object sender, ResolveEventArgs e)
        {
            // Определяем текущую папку библиотеки
            Assembly assembly = Assembly.GetExecutingAssembly();
            Uri assemblyFileUri = new Uri(assembly.CodeBase);
            string modulePath = Path.GetDirectoryName(assemblyFileUri.LocalPath);

            string[] nameSplit = e.Name.Split(',');
            string path = Path.Combine(modulePath, nameSplit[0] + ".dll");

            return Assembly.LoadFile(path);
        }

		[DllExport("InitUserDLLCom", CallingConvention.StdCall)]
        public static int InitUserDLLCom(IntPtr value)
        {
            if (value != IntPtr.Zero)
            {
                byte[] menu = Encoding.GetEncoding(1251).GetBytes("Создать путь\u0000");
                byte[] function = Encoding.GetEncoding(1251).GetBytes("RunModule\u0000");

                Marshal.Copy(menu, 0, value, menu.Length);
                Marshal.Copy(function, 0, (IntPtr)((int)value + 255), function.Length);
            }
            return 1;
        }

		[DllExport("PgiCheckMenuItemCom", CallingConvention.StdCall)]
        public static int PgiCheckMenuItemCom(IntPtr stFunction, IntPtr IPC)
        {
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
                AppDomain ad = CreatePluginDomain();
                try
                {
                    AppDomain.CurrentDomain.AssemblyResolve -= AssemblyResolve;

                    return worker.PgiCheckMenuItemCom(stFunction, IPC);
                }
                finally
                {
                    AppDomain.Unload(ad);
                }
            }
			catch (Exception exc) {
				MessageBox.Show(String.Format("Произошла внутреняя ошибка работы плагина. Обратитесь к разработчику. Причина: {0}. Стек вызова: {1}", exc.Message, exc.StackTrace), "Внутренняя ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
            return 0;
        }

        [DllExport("RunModule", CallingConvention.StdCall)]
        public static void RunModule(IntPtr IPC)
        {
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
                AppDomain ad = CreatePluginDomain();
                try
                {
                    AppDomain.CurrentDomain.AssemblyResolve -= AssemblyResolve;

                    worker.RunModule(IPC);
                }
                finally
                {
                    AppDomain.Unload(ad);
                }
            }
			catch (Exception exc) {
				MessageBox.Show(String.Format("Произошла внутреняя ошибка работы плагина. Обратитесь к разработчику. Причина: {0}. Стек вызова: {1}", exc.Message, exc.StackTrace), "Внутренняя ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//return;
			}
        }
    }
}
