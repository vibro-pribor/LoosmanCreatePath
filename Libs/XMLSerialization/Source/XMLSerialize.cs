using System;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Xml.Serialization;
using NLog;

namespace VP.Xml.Serialization
{
	/// <summary>
	/// XML-сериализация объекта
	/// </summary>
	/// <typeparam name="T">Тип объекта</typeparam>
	static public class XMLSerialize<T>
	{
		/// <summary>
		/// Логирование
		/// </summary>
		private static readonly Logger m_Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Сериализация объекта в файл
		/// </summary>
		/// <param name="path">Имя файла куда будет сериализован объект</param>
		/// <param name="obj">Объект</param>
		/// <exception cref="System.IO.IOException">Параметр path включает неверный или недопустимый 
		/// синтаксис имени файла, имени каталога или метки тома. Длина указанного пути, имени файла или 
		/// обоих параметров превышает установленное в системе максимальное значение. 
		/// Указанный путь недопустим (например, он соответствует неподключенному диску).</exception>
		/// <exception cref="System.UnauthorizedAccessException">Отказано в доступе. Файл имеет параметр readonly 
		/// или он скрыт. Операция записи данных не поддерживается на текущей платформе. path определяет 
		/// только каталог. У пользователя отсутствуют необходимые права</exception>
		/// <exception cref="System.ArgumentException">Параметр path является пустой строкой ("") или 
		/// path содержит имя системного устройства или path равно null или path не содержит информацию о каталоге</exception>
		/// <exception cref="System.FormatException">Ошибка при сериализации объекта, связанная с типом 
		/// сериализуемого объекта и наличием у него атрибута сериализации</exception>
		static public void Serialize(string path, T obj)
		{
			// Директория файла
			string dir;
			try {
				// Получить директорию из path
				dir = Path.GetDirectoryName(path);
			}
			// Обработка ошибок
			// Перегенерировать более общее исключение
			catch (System.IO.PathTooLongException exc) {
				throw new System.IO.IOException(exc.Message);
			}
			
			try {
				// Если такая директория не существует на диске, то
				if (!Directory.Exists(dir))
					// Создать ее
					Directory.CreateDirectory(dir);
			}
			// Обработка ошибок
			// Перегенерировать более общее исключение
			catch (System.NotSupportedException exc) {
				throw new System.ArgumentException(exc.Message);
			}
			try {
				// Сериализовать полученный объект в файл path 
				using (TextWriter writer = new StreamWriter(path)) {
					XmlSerializer xml = new XmlSerializer(typeof(T));
					xml.Serialize(writer, obj);
				}
			}
			// Обработка ошибок
			// Перегенерировать более общее исключение
			catch (System.Security.SecurityException exc) {
				throw new System.UnauthorizedAccessException(exc.Message);
			}
			catch (System.InvalidOperationException exc) {
				throw new System.FormatException(exc.Message);
			}

		}

		/// <summary>
		/// Сериализация объекта в поток
		/// </summary>
		/// <param name="stream">Поток куда будет сериализован объект</param>
		/// <param name="obj">Объект</param>
		static public void Serialize(Stream stream, T obj)
		{
			XmlSerializer xml = new XmlSerializer(typeof(T));
			xml.Serialize(stream, obj);
		}

		/// <summary>
		/// Десериализация объекта из файла
		/// </summary>
		/// <param name="path">Имя файла откуда будет десериализован объект</param>
		/// <returns>Десериализованный объект</returns>
		/// <exception cref="System.ArgumentException">Параметр path является пустой строкой ("") или 
		/// path содержит имя системного устройства или path равно null или path содержит только пробелы</exception>
		/// <exception cref="System.IO.IOException">Параметр path включает неверный или недопустимый 
		/// синтаксис имени файла, имени каталога или метки тома. Указанный путь недопустим (например,
		/// он соответствует неподключенному диску). Указанно имя несуществующего файла.</exception>
		///	<exception cref="System.FormatException">Произошла ошибка во время десериализации, связанная 
		///	с несоответствием содержимого файла десериализуемому объекту. Оригинальный
		///	исключение доступно, используя свойство InnerException</exception>
		static public T Deserialize(string path)
		{
			// Целевой объект
			T obj;
			try {
				using (TextReader reader = new StreamReader(path)) {
					XmlSerializer xml = new XmlSerializer(typeof(T));
					obj = (T)xml.Deserialize(reader);
				}
			}
			// Обработка ошибок
			// Перегенерировать более общее исключение
			catch (System.InvalidOperationException exc) {
				throw new System.FormatException(exc.Message);
			}
			return obj;
		}
		/// <summary>
		/// Десериализация объекта из потока
		/// </summary>
		/// <param name="stream">Поток откуда будет десериализован объект</param>
		/// <returns>Десериализованный объект</returns>
		static public T Deserialize(Stream stream)
		{
			XmlSerializer xml = new XmlSerializer(typeof(T));
			return (T)xml.Deserialize(stream);
		}
	}
}
