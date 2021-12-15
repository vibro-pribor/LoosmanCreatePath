using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPLoodsmanAPI
{
	/// <summary>
	/// Содержит информацию об атрибутах объекта в Лоцмане.
	/// </summary>
	public class AttributeObject
	{
		/// <summary>
		/// Получает или задаёт идентификатор значения атрибута.
		/// </summary>
		public int IDValue { get; set; }

		/// <summary>
		/// Получает или задаёт название атрибута.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Получает или задаёт значение атрибута в строковом представлении.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Получает или задаёт идентификатор типа атрибута.
		/// </summary>
		public int IDType { get; set; }

		private AccessLevel m_AccessLevelAttribute;
		/// <summary>
		/// Получает или задаёт уровень доступа к атрибуту.
		/// </summary>
		/// <exception cref="System.ArgumentException">Уровень доступа к атрибуту объекта задан как "Полный доступ".</exception>
		public AccessLevel AccessLevelAttribute {
			get
			{
				return this.m_AccessLevelAttribute;
			}
			set
			{
				if (value != AccessLevel.FullAccess)
					this.m_AccessLevelAttribute = value;
				else
					throw new System.ArgumentException("Уровень доступа к атрибуту объекта не может быть полным.");
			}
		}

		/// <summary>
		/// Получает или задаёт идентификатор единицы измерения, в которой возвращается значение атрибут.
		/// </summary>
		public string IDUnit { get; set; }

		/// <summary>
		/// Получает или задаёт название единицы измерения, в которой возвращается значение атрибута.
		/// </summary>
		public string NameUnit { get; set; }

		/// <summary>
		/// Получает или задаёт идентификатор сущности, которую измерил данный атрибут.
		/// </summary>
		public string IDMeasure { get; set; }

		/// <summary>
		/// Получает или задаёт название сущности, которую измерил данный атрибут.
		/// </summary>
		public string NameMeasure { get; set; }

		/// <summary>
		/// Получает или задаёт признак того, что атрибут является служебным.
		/// </summary>
		public bool IsSystemAttribute { get; set; }

	}
}
