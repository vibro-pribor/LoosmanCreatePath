using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPLoodsmanAPI
{
	/// <summary>
	/// Содержит свойства объекта в Лоцмане.
	/// </summary>
	public class PropertiesObject
	{
		/// <summary>
		/// Получает или задаёт идентификатор версии объекта.
		/// </summary>
		public int IDVersion { get; set; }

		/// <summary>
		/// Получает или задаёт название типа объекта.
		/// </summary>
		public string NameType { get; set; }

		/// <summary>
		/// Получает или задаёт ключевой атрибут объекта.
		/// </summary>
		public string KeyAttribute { get; set; }

		/// <summary>
		/// Получает или задаёт номер версии объекта.
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// Получает или задаёт название текущего состояния объекта.
		/// </summary>
		public string NameState { get; set; }

		/// <summary>
		/// Получает или задаёт признак того, что объект является документом.
		/// </summary>
		public bool IsDocument { get; set; }

		/// <summary>
		/// Получает или задаёт уровень доступа к объекту.
		/// </summary>
		public AccessLevel AccessLevelObject { get; set; }

		/// <summary>
		/// Получает или задаёт уровень блокировки объекта.
		/// </summary>
		public LockLevel LockLevelObject { get; set; }
	}
}
