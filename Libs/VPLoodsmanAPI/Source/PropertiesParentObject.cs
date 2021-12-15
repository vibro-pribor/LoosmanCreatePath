using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPLoodsmanAPI
{
	/// <summary>
	/// Содержит свойства родительского объекта в Лоцмане.
	/// </summary>
	public class PropertiesParentObject
	{
		/// <summary>
		/// Получает или задаёт идентификатор версии родительского объекта.
		/// </summary>
		public int IDVersion { get; set; }

		/// <summary>
		/// Получает или задаёт идентификатор экземпляра связи между исходным объектом и родителем.
		/// </summary>
		public int IDLink { get; set; }

		/// <summary>
		/// Получает или задаёт идентификатор типа родительского объекта.
		/// </summary>
		public int IDType { get; set; }

		/// <summary>
		/// Получает или задаёт ключевой атрибут родительского объекта.
		/// </summary>
		public string KeyAttribute { get; set; }

		/// <summary>
		/// Получает или задаёт номер версии родительского объекта.
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// Получает или задаёт идентификатор текущего состояния родительского объекта.
		/// </summary>
		public int IDState { get; set; }

		/// <summary>
		/// Получает или задаёт идентификатор типа связи между исходным объектом и родителем.
		/// </summary>
		public int IDLinkType { get; set; }

		/// <summary>
		/// Получает или задаёт идентификатор чекаута, в котором блокирован родительский объект.
		/// </summary>
		public int IDCheckOut { get; set; }

		/// <summary>
		/// Получает или задаёт уровень доступа к родительскому объекту.
		/// </summary>
		public AccessLevel AccessLevelObject { get; set; }
	}
}
