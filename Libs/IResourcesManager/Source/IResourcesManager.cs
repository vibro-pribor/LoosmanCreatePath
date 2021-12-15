using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using NLog;


namespace VP.Resources.IResourcesManager
{
	public interface IResourcesManager
	{

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов изображения
		/// </summary>
		/// <param name="p_NameImage">Имя изображения</param>
		/// <param name="width">Ширина изображеия</param>
		/// <param name="height">Высота изображения</param>
		/// <returns>Полученное изображение</returns>
		Bitmap GetImageFromRecources(string p_NameImage, int p_Width, int p_Height);

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов изображения
		/// </summary>
		/// <param name="p_NameImage">Имя изображения</param>        
		/// <returns>Полученное изображение</returns>
		Bitmap GetImageFromRecources(string p_NameImage);

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов изображения
		/// </summary>
		/// <param name="p_Path">Путь до ресурса обычно это  строка вида пространство имён.Resources.
		/// Пример:
		///  Пространство имён VP.Device
		/// то строка должна быть  VP.Device.Resources.
		/// </param>
		/// <param name="p_NameImage">Имя изображения</param>        
		/// <returns>Полученное изображение</returns>
		Bitmap GetImageFromRecources(string p_Path, string p_NameImage);

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов изображения
		/// </summary>
		/// <param name="p_Path">Путь до ресурса обычно это  строка вида пространство имён.Resources.
		/// Пример:
		///  Пространство имён VP.Device
		/// то строка должна быть   VP.Device.Resources.
		/// </param>
		/// <param name="p_NameImage">Имя изображения</param>
		/// <param name="width">Ширина изображеия</param>
		/// <param name="height">Высота изображения</param>
		/// <returns>Полученное изображение</returns>
		Bitmap GetImageFromRecources(string p_Path, string p_NameImage, int p_Width, int p_Height);

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов 
		/// </summary>
		/// <param name="p_NameImage">Имя ресурса</param>        
		/// <returns>Буферированный поток</returns>
		BufferedStream GetStreamFromRecources(string p_NameImage);

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов 
		/// </summary>
		/// <param name="p_Path">Путь до ресурса обычно это  строка вида пространство имён.Resources.
		/// Пример:
		///  Пространство имён VP.Device
		/// то строка должна быть  VP.Device.Resources.
		/// </param>
		/// <param name="p_NameImage">Имя ресурса</param>        
		/// <returns>Буферированный поток</returns>
		BufferedStream GetStreamFromRecources(string p_Path, string p_NameImage);

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов  иконку
		/// </summary>
		/// <param name="p_NameImage">Имя ресурса</param>       
		/// <param name="p_Size">Размер  иконки</param>
		/// <returns>Буферированный поток</returns>
		Icon GetAppIconFromRecources(string p_NameImage, Size p_Size);

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов  иконку
		/// </summary>
		/// <param name="p_Path">Путь до ресурса обычно это  строка вида пространство имён.Resources.
		/// Пример:
		/// Пространство имён VP.Device
		/// то строка должна быть  VP.Device.Resources.
		/// </param>
		/// <param name="p_NameImage">Имя ресурса</param>       
		/// <param name="p_Size">Размер  иконки</param>
		/// <returns>Буферированный поток</returns>
		Icon GetAppIconFromRecources(string p_Path, string p_NameImage, Size p_Size);

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов иконку 
		/// </summary>
		/// <param name="p_NameImage">Имя ресурса</param>        
		/// <returns>Буферированный поток</returns>
		Icon GetAppIconFromRecources(string p_NameImage);

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов иконку 
		/// </summary>
		/// <param name="p_NameImage">Имя ресурса</param>        
		/// <returns>Буферированный поток</returns>
		Icon GetVPIconFromRecources();

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов иконку 
		/// </summary>
		/// <param name="p_Path">Путь до ресурса обычно это  строка вида пространство имён.Resources.
		/// Пример:
		///  Пространство имён VP.Device
		/// то строка должна быть VP.Device.Resources.
		/// </param>
		/// <param name="p_NameImage">Имя ресурса</param>        
		/// <returns>Буферированный поток</returns>
		Icon GetAppIconFromRecources(string p_Path, string p_NameImage);

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов 
		/// </summary>
		/// <param name="p_NameImage">Имя ресурса</param>        
		/// <returns>Буферированный поток байт</returns>
		byte[] GetByteFromRecources(string p_NameImage);

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов 
		/// </summary>
		/// <param name="p_Path">Путь до ресурса обычно это  строка вида пространство имён.Resources.
		/// Пример:
		///  Пространство имён VP.Device
		/// то строка должна быть  VP.Device.Resources.
		/// </param>
		/// <param name="p_NameImage">Имя ресурса</param>        
		/// <returns>Буферированный поток байт</returns>
		byte[] GetByteFromRecources(string p_Path, string p_NameImage);

		/// <summary>
		/// Возвращает строку в кодировке utf-8
		/// </summary>
		/// <param name="p_NameFile">Имя ресурса</param>
		/// <returns>Строка</returns>
		string GetStringFromResources(string p_NameFile);

		/// <summary>
		/// Возвращает строку в кодировке utf-8
		/// </summary>
		/// <param name="p_Path">Путь до ресурса обычно это  строка вида пространство имён.Resources.
		/// Пример:
		///  Пространство имён VP.Device
		/// то строка должна быть  VP.Device.Resources.
		/// </param>
		/// <param name="p_NameFile">Имя ресурса</param>
		/// <returns>Строка</returns>
		string GetStringFromResources(string p_Path, string p_NameFile);


	}

	public class StaticFactory
	{
		/// <summary>
		/// Логирование
		/// </summary>
		private static readonly Logger m_Logger = LogManager.GetCurrentClassLogger();

		private static IResourcesManager instance;
		private static object lockObj = new object();

		public static IResourcesManager Instance
		{
			get
			{
				lock (lockObj) {
					if (instance == null) {
						try {
							// Для не зашифрованной библиотеки	
							string keyPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "VP.Resources.UnplugResourcesManager.dll");

							Assembly ass = Assembly.LoadFile(keyPath);
							Type type = ass.GetType("VP.Resources.UnplugResourcesManager.UnplugResourcesManager");
							instance = (IResourcesManager)Activator.CreateInstance(type);
						}
						catch (Exception exc) {
							m_Logger.Error(exc.GetType().Name + ": " + exc.Message + " " + exc.StackTrace);
							throw new System.DllNotFoundException("Error initializing components");
						}
					}
				}
				return instance;
			}
		}
	}
}