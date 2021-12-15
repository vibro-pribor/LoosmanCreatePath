using System;
using System.Text;
using System.IO;
using System.Drawing;
using VP.Resources.IResourcesManager;

namespace VP.Resources.UnplugResourcesManager
{
	public class UnplugResourcesManager : IResourcesManager.IResourcesManager
	{
		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов изображения
		/// </summary>
		/// <param name="p_NameImage">Имя изображения</param>
		/// <param name="width">Ширина изображеия</param>
		/// <param name="height">Высота изображения</param>
		/// <returns>Полученное изображение</returns>
		public Bitmap GetImageFromRecources(string p_NameImage, int p_Width, int p_Height)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			Stream myStream = myAssembly.GetManifestResourceStream("VP.Resources.UnplugResourcesManager.Resources." + p_NameImage);
			Bitmap image = new Bitmap(myStream);
			return new Bitmap(image, new Size(p_Width, p_Height));
		}


		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов изображения
		/// </summary>
		/// <param name="p_NameImage">Имя изображения</param>       
		/// <returns>Полученное изображение</returns>
		public Bitmap GetImageFromRecources(string p_NameImage)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			Stream myStream = myAssembly.GetManifestResourceStream("VP.Resources.UnplugResourcesManager.Resources." + p_NameImage);
			Bitmap image = new Bitmap(myStream);
			return new Bitmap(image);
		}

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
		public Bitmap GetImageFromRecources(string p_Path, string p_NameImage)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			Stream myStream = myAssembly.GetManifestResourceStream(p_Path + "." + p_NameImage);

			Bitmap image = new Bitmap(myStream);
			return new Bitmap(image);
		}


		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов изображения
		/// </summary>
		/// <param name="p_Path">Путь до ресурса обычно это  строка вида пространство имён.Resources.
		/// Пример:
		///  Пространство имён VP.Device
		/// то строка должна быть  VP.Device.Resources.
		/// </param>
		/// <param name="p_NameImage">Имя изображения</param>
		/// <param name="width">Ширина изображеия</param>
		/// <param name="height">Высота изображения</param>
		/// <returns>Полученное изображение</returns>
		public Bitmap GetImageFromRecources(string p_Path, string p_NameImage, int p_Width, int p_Height)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			Stream myStream = myAssembly.GetManifestResourceStream(p_Path + "." + p_NameImage);
			Bitmap image = new Bitmap(myStream);
			return new Bitmap(image, new Size(p_Width, p_Height));
		}



		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов 
		/// </summary>
		/// <param name="p_Name">Имя ресурса</param>        
		/// <returns>Буферированный поток</returns>
		public BufferedStream GetStreamFromRecources(string p_Name)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			return new BufferedStream(myAssembly.GetManifestResourceStream("VP.Resources.UnplugResourcesManager.Resources." + p_Name));

		}

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
		public BufferedStream GetStreamFromRecources(string p_Path, string p_NameImage)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			return new BufferedStream(myAssembly.GetManifestResourceStream(p_Path + "." + p_NameImage));

		}

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов  иконку
		/// </summary>
		/// <param name="p_NameImage">Имя ресурса</param>       
		/// <param name="p_Size">Размер  иконки</param>
		/// <returns>Буферированный поток</returns>
		public Icon GetAppIconFromRecources(string p_NameImage, Size p_Size)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			return new Icon(new BufferedStream(myAssembly.GetManifestResourceStream("VP.Resources.UnplugResourcesManager.Resources." + p_NameImage)), p_Size);

		}
		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов  иконку
		/// </summary>
		/// <param name="p_Path">Путь до ресурса обычно это  строка вида пространство имён.Resources.
		/// Пример:
		///  Пространство имён VP.Device
		/// то строка должна быть  VP.Device.Resources.
		/// </param>
		/// <param name="p_NameImage">Имя ресурса</param>       
		/// <param name="p_Size">Размер  иконки</param>
		/// <returns>Буферированный поток</returns>
		public Icon GetAppIconFromRecources(string p_Path, string p_NameImage, Size p_Size)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			return new Icon(new BufferedStream(myAssembly.GetManifestResourceStream(p_Path + "." + p_NameImage)), p_Size);

		}

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов иконку 
		/// </summary>
		/// <param name="p_NameImage">Имя ресурса</param>        
		/// <returns>Буферированный поток</returns>
		public Icon GetAppIconFromRecources(string p_NameImage)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			return new Icon(new BufferedStream(myAssembly.GetManifestResourceStream("VP.Resources.UnplugResourcesManager.Resources." + p_NameImage)));

		}


		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов иконку 
		/// </summary>
		/// <param name="p_NameImage">Имя ресурса</param>        
		/// <returns>Буферированный поток</returns>
		public Icon GetVPIconFromRecources()
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			return new Icon(new BufferedStream(myAssembly.GetManifestResourceStream("VP.Resources.UnplugResourcesManager.Resources.3D_logo.ico")));

		}

		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов иконку 
		/// </summary>
		/// <param name="p_Path">Путь до ресурса обычно это  строка вида пространство имён.Resources.
		/// Пример:
		///  Пространство имён VP.Device
		/// то строка должна быть  VP.Device.Resources.
		/// </param>
		/// <param name="p_NameImage">Имя ресурса</param>        
		/// <returns>Буферированный поток</returns>
		public Icon GetAppIconFromRecources(string p_Path, string p_NameImage)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			return new Icon(new BufferedStream(myAssembly.GetManifestResourceStream(p_Path + "." + p_NameImage)));

		}




		/// <summary>
		///  Загрузить из  прекреплённых  ресурсов 
		/// </summary>
		/// <param name="p_NameImage">Имя ресурса</param>        
		/// <returns>Буферированный поток байт</returns>
		public byte[] GetByteFromRecources(string p_NameImage)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			Stream bs = myAssembly.GetManifestResourceStream("VP.Resources.UnplugResourcesManager.Resources." + p_NameImage);
			byte[] array = new byte[bs.Length];
			bs.Read(array, 0, Convert.ToInt32(bs.Length));
			bs.Close();
			return array;
		}

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
		public byte[] GetByteFromRecources(string p_Path, string p_NameImage)
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			Stream bs = myAssembly.GetManifestResourceStream(p_Path + "." + p_NameImage);
			byte[] array = new byte[bs.Length];
			bs.Read(array, 0, Convert.ToInt32(bs.Length));
			bs.Close();
			return array;
		}

		/// <summary>
		/// Возвращает строку в кодировке utf-8
		/// </summary>
		/// <param name="p_NameFile">Имя ресурса</param>
		/// <returns>Строка</returns>
		public string GetStringFromResources(string p_NameFile)
		{
			Encoding enc = Encoding.UTF8;
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			StreamReader reader = new StreamReader(myAssembly.GetManifestResourceStream("VP.Resources.UnplugResourcesManager.Resources." + p_NameFile), enc);
			return reader.ReadToEnd();
		}

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
		public string GetStringFromResources(string p_Path, string p_NameFile)
		{
			Encoding enc = Encoding.UTF8;
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			StreamReader reader = new StreamReader(myAssembly.GetManifestResourceStream(p_Path + "." + p_NameFile), enc);
			return reader.ReadToEnd();
		}


	}
}

