using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using LoodsmanDotNet;
using DataProvider;
using VPLoodsmanAPI.Properties;

namespace VPLoodsmanAPI
{
	/// <summary>
	/// Уровень доступа.
	/// </summary>
	public enum AccessLevel { 
		/// <summary>
		/// Только чтение.
		/// </summary>
		ReadOnly = 1, 
		/// <summary>
		/// Чтение/запись.
		/// </summary>
		ReadWrite = 2, 
		/// <summary>
		/// Полный доступ.
		/// </summary>
		FullAccess = 3 
	};

	/// <summary>
	/// Уровень блокировки объекта.
	/// </summary>
	public enum LockLevel { 
		/// <summary>
		/// Объект не блокирован.
		/// </summary>
		NotLocked = 0, 
		/// <summary>
		/// Объект блокирован текущим пользователем.
		/// </summary>
		LockedCurrentUser = 1, 
		/// <summary>
		/// Объект блокирован другим пользователем.
		/// </summary>
		LockedAnotherUser = 2 
	};

	/// <summary>
	/// API для взаимодействия с Лоцманом.
	/// </summary>
	public class LoodsmanAPI
	{
		/// <summary>
		/// Получает или задаёт интерфейс, передаваемый в подключаемые модули ЛОЦМАН:PLM.
		/// </summary>
		private IPluginCall m_PluginCall { get; set; }

		/// <summary>
		/// Создаёт объект, предоставляющий API для взаимодействия с Лоцманом.
		/// </summary>
		/// <param name="loodsmanPluginCall">Интерфейс, передаваемый в подключаемые модули ЛОЦМАН:PLM.</param>
		/// <exception cref="System.ArgumentNullException">Параметр loodsmanObject имеет значение null.</exception>
		public LoodsmanAPI(IPluginCall loodsmanPluginCall)
		{
			if (loodsmanPluginCall != null)
				this.m_PluginCall = loodsmanPluginCall;
			else
				throw new System.ArgumentNullException("Параметр loodsmanObject имеет значение null");
		}

		/// <summary>
		/// Получает информацию о текущем пользователе Лоцмана.
		/// </summary>
		/// <returns>Объект, содержащий информацию о текущем пользователе Лоцмана, или null, если такая информация не была найдена.</returns>
		public InfoAboutCurrentUser GetInfoAboutCurrentUser()
		{
			InfoAboutCurrentUser info_current_user = null;
			// Получение данных о текущем пользователе из Лоцмана
			var current_user = m_PluginCall.GetDataSet("GetInfoAboutCurrentUser", new object[] { }) as IDataSet;
			if (current_user.RecordCount != 0) {
				info_current_user = new InfoAboutCurrentUser();
				info_current_user.Name = current_user.FieldValue[Resources.Name].ToString();

				info_current_user.Fullname = GetStringValueByNullParameter(current_user.FieldValue[Resources.Fullname]);
				info_current_user.Email = GetStringValueByNullParameter(current_user.FieldValue[Resources.Email]);
				info_current_user.WorkDirectory = GetStringValueByNullParameter(current_user.FieldValue[Resources.UserDirectory]);
				info_current_user.FileDirectory = GetStringValueByNullParameter(current_user.FieldValue[Resources.FileDirectory]);
			}

			return info_current_user;
		}

		/// <summary>
		/// Получает информацию о родителях указанного объекта.
		/// </summary>
		/// <param name="idVersionChildObject">Идентификатор версии объекта, информацию о родителе которого необходимо получить.</param>
		/// <returns>Список объектов, содержащих информацию о родителях указанного объекта.</returns>
		/// <exception cref="System.ArgumentException">Один и более параметров, возвращаемых Лоцманом, имеют не корректный тип или значение.</exception>
		public List<PropertiesParentObject> GetParentObject(int idVersionChildObject)
		{
			List<PropertiesParentObject> list_parents_object = new List<PropertiesParentObject>();
			PropertiesParentObject current_parent_object;
			// Получить родительские объекты из Лоцмана
			var parents_object = m_PluginCall.GetDataSet("GetLObjs", new object[] { idVersionChildObject, true }) as IDataSet;
			for (int i = 0; i < parents_object.RecordCount; ++i) {
				current_parent_object = new PropertiesParentObject();
				current_parent_object.IDVersion = (int)parents_object.FieldValue[Resources.IDVersion];
				current_parent_object.IDLink = (int)parents_object.FieldValue[Resources.IDLink];
				current_parent_object.IDType = (int)parents_object.FieldValue[Resources.IDType];
				current_parent_object.KeyAttribute = parents_object.FieldValue[Resources.KeyAttribute].ToString();
				current_parent_object.Version = GetStringValueByNullParameter(parents_object.FieldValue[Resources.Version]);
				current_parent_object.IDState = (int)parents_object.FieldValue[Resources.IDState];
				current_parent_object.IDLinkType = (int)parents_object.FieldValue[Resources.IDLinkType];
				current_parent_object.IDCheckOut = GetIDCheckOut(parents_object.FieldValue[Resources.IDCheckOut]);
				current_parent_object.AccessLevelObject = GetAccessLevelById((int)parents_object.FieldValue[Resources.AccessLevel]);

				list_parents_object.Add(current_parent_object);
				parents_object.Next();
			}

			return list_parents_object;
		}

		/// <summary>
		/// Получает информацию обо всех атрибутах указанного объекта.
		/// </summary>
		/// <param name="idVersionObject">Идентификатор версии объекта, информацию об атрибутах которого необходимо получить.</param>
		/// <returns>Список объектов, содержащих информацию об атрибутах указанного объекта.</returns>
		/// <exception cref="System.ArgumentException">Один и более параметров, возвращаемых Лоцманом, имеют не корректный тип или значение.</exception>
		/// <remarks>Для типов атрибута Image и Text значение атрибута всегда равно пустой строке.</remarks>
		public List<AttributeObject> GeAllAttributeObject(int idVersionObject)
		{
			List<AttributeObject> list_attributes_object = new List<AttributeObject>();
			AttributeObject current_attribute_object;
			// Получить все атрибуты объекта из Лоцмана
			var attributes =  m_PluginCall.GetDataSet("GetInfoAboutVersion", new object[] { null, null, null, idVersionObject, 3 }) as IDataSet;
			for (int i = 0; i < attributes.RecordCount; ++i) {
				current_attribute_object = new AttributeObject();
				current_attribute_object.IDValue = (int)attributes.FieldValue[Resources.ID];
				current_attribute_object.Name = attributes.FieldValue[Resources.Name].ToString();
				current_attribute_object.Value = GetStringValueByNullParameter(attributes.FieldValue[Resources.Value]);
				current_attribute_object.IDType = (int)attributes.FieldValue[Resources.IDTypeAttribute];
				current_attribute_object.AccessLevelAttribute = GetAccessLevelById((int)attributes.FieldValue[Resources.AccessLevel]);
				current_attribute_object.IDUnit = GetStringValueByNullParameter(attributes.FieldValue[Resources.IDUnit]);
				current_attribute_object.NameUnit = GetStringValueByNullParameter(attributes.FieldValue[Resources.NameUnit]);
				current_attribute_object.IDMeasure = GetStringValueByNullParameter(attributes.FieldValue[Resources.IDMeasure]);
				current_attribute_object.NameMeasure = GetStringValueByNullParameter(attributes.FieldValue[Resources.NameMeasure]);
				current_attribute_object.IsSystemAttribute = GetBoolValueById((int)attributes.FieldValue[Resources.IsSystem]);

				list_attributes_object.Add(current_attribute_object);
				attributes.Next();
			}
			return list_attributes_object;
		}

		/// <summary>
		/// Получает свойства указанного объекта.
		/// </summary>
		/// <param name="idVersionObject">Идентификатор версии объекта, свойства которого необходимо получить.</param>
		/// <returns>Объект, содержащий свойства указанного объекта Лоцмана, или null, если такая информация не была найдена.</returns>
		/// <exception cref="System.ArgumentException">Один и более параметров, возвращаемых Лоцманом, имеют не корректный тип или значение.</exception>
		public PropertiesObject GetPropertiesObject(int idVersionObject)
		{
			PropertiesObject info_properties_object = null;
			// Получение cвойств объекта из Лоцмана
			var properties_object = m_PluginCall.GetDataSet("GetPropObjects", new object[] { idVersionObject, 0 }) as IDataSet;
			if (properties_object.RecordCount != 0) {
				info_properties_object = new PropertiesObject();
				info_properties_object.IDVersion = (int)properties_object.FieldValue[Resources.IDVersion];
				info_properties_object.NameType = properties_object.FieldValue[Resources.NameType].ToString();
				info_properties_object.KeyAttribute = properties_object.FieldValue[Resources.KeyAttribute].ToString();
				info_properties_object.Version = GetStringValueByNullParameter(properties_object.FieldValue[Resources.Version]);
				info_properties_object.NameState = properties_object.FieldValue[Resources.NameState].ToString();
				info_properties_object.IsDocument = GetBoolValueById((int)properties_object.FieldValue[Resources.IsDocument]);
				info_properties_object.AccessLevelObject = GetAccessLevelById((int)properties_object.FieldValue[Resources.AccessLevel]);
				info_properties_object.LockLevelObject = GetLockLevelById((int)properties_object.FieldValue[Resources.LockLevel]);
			}
			return info_properties_object;
		}


		/// <summary>
		/// Получает уровень доступа по его идентификатору.
		/// </summary>
		/// <param name="idAccessLevel">Идентификатор уровня доступа.</param>
		/// <returns>Уровень доступа соответствующий указанному идентификатору.</returns>
		/// <exception cref="System.ArgumentException">Идентификатор уровня доступа находится вне диапазоне допустимых значений.</exception>
		public AccessLevel GetAccessLevelById(int idAccessLevel)
		{
			switch (idAccessLevel) {
				case 1: return AccessLevel.ReadOnly;
				case 2: return AccessLevel.ReadWrite;
				case 3: return AccessLevel.FullAccess;
				default: throw new System.ArgumentException(String.Format("Идентификатор уровня доступа idAccessLevel находится вне диапазоне допустимых значений [1-3]. Указанное значение: {0}.", idAccessLevel), "idAccessLevel");
			}
		}

		/// <summary>
		/// Получает уровень блокировки объекта по его идентификатору.
		/// </summary>
		/// <param name="idLockLevel">Идентификатор уровня блокировки объекта.</param>
		/// <returns>Уровень блокировки объекта соответствующий указанному идентификатору.</returns>
		/// <exception cref="System.ArgumentException">Идентификатор уровня блокировки находится вне диапазоне допустимых значений.</exception>
		public LockLevel GetLockLevelById(int idLockLevel)
		{
			switch (idLockLevel) {
				case 0: return LockLevel.NotLocked;
				case 1: return LockLevel.LockedCurrentUser;
				case 2: return LockLevel.LockedAnotherUser;
				default: throw new System.ArgumentException(String.Format("Идентификатор уровня блокировки idLockLevel находится вне диапазоне допустимых значений [0-2]. Указанное значение: {0}.", idLockLevel), "idLockLevel");
			}
		}

		/// <summary>
		/// Получает логическое значение по его идентификатору.
		/// </summary>
		/// <param name="idBoolValue">Идентификатор логического значения.</param>
		/// <returns>Логическое значение соответствующее указанному идентификатору.</returns>
		/// <exception cref="System.ArgumentException">Идентификатор логического значения находится вне диапазоне допустимых значений.</exception>
		public bool GetBoolValueById(int idBoolValue)
		{
			switch (idBoolValue) {
				case 0: return false;
				case 1: return true;
				default: throw new System.ArgumentException(String.Format("Идентификатор логического значения idBoolValue находится вне диапазоне допустимых значений [0-1]. Указанное значение: {0}.", idBoolValue), "idBoolValue");
			}
		}

		/// <summary>
		/// Получает значение указанного параметра в строковом представлении.
		/// </summary>
		/// <param name="valueParameter">Значение параметра.</param>
		/// <returns>Значение указанного параметра в строковом представлении или пустая строка, если указанный параметр равен null.</returns>
		public string GetStringValueByNullParameter(object valueParameter)
		{
			if (valueParameter != null)
				return valueParameter.ToString();
			else
				return string.Empty;
		}

		/// <summary>
		/// Получает идентификатор чекаута, в котором блокирован объект, по указанному значению параметра.
		/// </summary>
		/// <param name="valueParameter">Значение параметра, по которому необходимо получить идентификатор чекаута.</param>
		/// <returns>Идентификатор чекаута, в котором блокирован объект, или -1, если объект не блокирован.</returns>
		/// <exception cref="System.ArgumentException">Не существует способа преобразования указанного значения параметра в int.</exception>
		public int GetIDCheckOut(object valueParameter)
		{
			if (valueParameter != null)
				if (valueParameter is int)
					return (int)valueParameter;
				else
					throw new System.ArgumentException("Не существует способа преобразования указанного значения параметра в int", "valueParameter");
			else
				return -1;
		}

	}

}
