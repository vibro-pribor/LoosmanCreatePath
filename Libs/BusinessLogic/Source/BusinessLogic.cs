using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoodsmanDotNet;
using VPLoodsmanAPI;
using System.IO;
using System.Windows.Forms;
using System.Web;
using VP.Loodsman.PluginSettings;
using VP.Xml.Serialization;
using NLog;

namespace ASCON.Loodsman.CreatePath
{
	/// <summary>
	/// Бизнес логика плагина.
	/// </summary>
    public class BusinessLogic
    {
		/// <summary>
		/// Создаёт объект бизнес логики плагина.
		/// </summary>
		/// <param name="loodsmanPluginCall">Интерфейс, передаваемый в подключаемые модули ЛОЦМАН:PLM.</param>
		/// <param name="loodsmanApplication">Интерфейс приложения ЛОЦМАН:PLM, в котором запущен данный плагин.</param>
		/// <exception cref="System.ArgumentNullException">Параметр loodsmanPluginCall или loodsmanApplication имеет значение null.</exception>
		public BusinessLogic(IPluginCall loodsmanPluginCall, ILoodsmanApplication loodsmanApplication)
		{
			if (loodsmanPluginCall != null && loodsmanApplication != null) {
				this.m_LoodsmanAPI = new LoodsmanAPI(loodsmanPluginCall);
				this.m_IDVersionSelectedObject = loodsmanPluginCall.IdVersion;
				this.m_LoodsmanApplication = loodsmanApplication;
			}
			else
				throw new System.ArgumentNullException("Параметр loodsmanPluginCall или loodsmanApplication имеет значение null");
		}

		/// <summary>
		/// Логирование.
		/// </summary>
		private static readonly Logger m_Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Получает или задаёт идентификатор версии выбранного объекта.
		/// </summary>
		private int m_IDVersionSelectedObject { get; set; }

		/// <summary>
		/// Получает или задаёт объект, содержащий настройки плагина.
		/// </summary>
		private Settings m_PluginSettings { get; set; }

		/// <summary>
		/// Получает или задаёт объект, предоставляющий API для взаимодействия с Лоцманом.
		/// </summary>
		private LoodsmanAPI m_LoodsmanAPI { get; set; }

		/// <summary>
		///  Получает или задаёт интерфейс приложения ЛОЦМАН:PLM, в котором запущен данный плагин.
		/// </summary>
		private ILoodsmanApplication m_LoodsmanApplication { get; set; }

		/// <summary>
		/// Выполняет основную работу плагина.
		/// </summary>
		/// <remarks>Является входной точкой логики плагина.</remarks>
		public void RunModule()
		{
			try {
				this.LoadSettingsPlugin();
				string file_directory_current_user = GetFileDirectoryCurrentUser();
				List<string> list_sub_directory = CreateListSubDirectory();
				string created_directory = CreatePathInOS(file_directory_current_user, list_sub_directory);
				if (!this.CreateDirectoryByPath(created_directory))
					LoodsmanNotify.Show(this.m_LoodsmanApplication, String.Format("Директория «{0}» уже существует!", created_directory), "Директория уже существует", LoodsmanIcon.Exclamation, LoodsmanIcon.Exclamation);
				else
					LoodsmanNotify.Show(this.m_LoodsmanApplication, String.Format("Создана директория «{0}»", created_directory), "Директория создана", LoodsmanIcon.Information, LoodsmanIcon.Information);
			}
			catch (System.ApplicationException) {
				return;
			}
		}

		/// <summary>
		/// Создаёт список подкаталогов, которые необходимо создать.
		/// </summary>
		/// <returns>Список, содержащий подкаталоги, которые необходимо создать.</returns>
		/// <remarks>Имена подкаталогов могут содержать недопустимые символы для каталогов в данной операционной системе, т.к. на данном этапе их корректность не проверяется.</remarks>
		private List<string> CreateListSubDirectory()
		{
			List<string> list_sub_directory = new List<string>();
			int id_version_current_object = this.m_IDVersionSelectedObject;
			string current_sub_directory;
			string key_attribute_current_object, version_current_object, attribute_name_current_object;
			PropertiesObject current_object;

			// Пока существует текущий непройденный объект
			while (id_version_current_object != -1) {
				current_object = this.m_LoodsmanAPI.GetPropertiesObject(id_version_current_object);
				if (current_object != null) {
					if (!this.m_PluginSettings.ProcessedTypes.Exists(predicate => predicate.ToLower().Trim() == current_object.NameType.ToLower().Trim())) {
						id_version_current_object = GetIDVersionNextObject(id_version_current_object);
						continue;
					}
					key_attribute_current_object = current_object.KeyAttribute;
					attribute_name_current_object = GeAttributeValueByName(current_object.IDVersion, "Наименование");
					version_current_object = current_object.Version;
					current_sub_directory = NameBuildingSubFolder(key_attribute_current_object, version_current_object, attribute_name_current_object);
					list_sub_directory.Add(current_sub_directory);
				}
				id_version_current_object = GetIDVersionNextObject(id_version_current_object);
			}
			list_sub_directory.Reverse();
			return list_sub_directory;
		}
		/// <summary>
		/// Создаёт имя каталога в операционной системе по каталогу для хранения файлов текущего пользователя и списку требуемых подкаталогов.
		/// </summary>
		/// <param name="fileDirectoryCurrentUser">Имя каталога для хранения файлов текущего пользователя.</param>
		/// <param name="listSubDirectory">Cписок требуемых подкаталогов.</param>
		/// <returns>Имя каталога в операционной системе, составленное из имени каталога для хранения файлов текущего пользователя и списка требуемых подкаталогов.</returns>
		/// <exception cref="System.ApplicationException">Пользователь отменил создание каталога.</exception>
		private string CreatePathInOS(string fileDirectoryCurrentUser, List<string> listSubDirectory)
		{
			string source_path = this.CreatePathInLoodsman(listSubDirectory);
			string replacement_path;
			if (!this.m_PluginSettings.ReplaceablePaths.TryGetValue(HttpUtility.HtmlEncode(source_path), out replacement_path)) {
				SharedData shared_data = new SharedData();
				shared_data.OnChangeSelectSubDirectories += ReCreatePath;
				shared_data.SelectSubDirectories = listSubDirectory;
				CheckPath check_path_dlg = new CheckPath(shared_data);
				if (check_path_dlg.ShowDialog() != DialogResult.OK)
					throw new System.ApplicationException();
				replacement_path = check_path_dlg.SharedDataDlg.CurrentPath;
				this.m_PluginSettings.ReplaceablePaths.Add(HttpUtility.HtmlEncode(source_path), replacement_path);
				this.m_PluginSettings.SaveSettings();
			}
			try {
				return Path.Combine(fileDirectoryCurrentUser, replacement_path);
			}
			catch (ArgumentException)
			{
				m_Logger.Error("Не удалось составить имя директории. Причина: недопустимые символы в пути {0}, сохранённом в словаре для замены создаваемых путей", replacement_path);
				MessageBox.Show("Cловарь для замены создаваемых путей в файле основных настроек плагина повреждён! Обратитесь к администратору.", "Cловарь для замены создаваемых путей повреждён", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				throw new System.ApplicationException();
			}
		}

		/// <summary>
		/// Загружает настройки плагина.
		/// </summary>
		/// <exception cref="System.ApplicationException">Произошла одна или более ошибок при загрузке настроек плагина.</exception>
		private void LoadSettingsPlugin()
		{
			try {
				this.m_PluginSettings = Settings.LoadSettings();
			}
			catch (System.DllNotFoundException exc) {
				m_Logger.Error("Не удалось загрузить настройки плагина. Причина: {0}. Стек вызова: {1}", exc.Message, exc.StackTrace);
				MessageBox.Show("Не удалось загрузить настройки плагина. Возможно файлы плагина были повреждены. Обратитесь к администратору.",
						"Ошибка загрузки настроек плагина", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				throw new System.ApplicationException();
			}
			
		}

		/// <summary>
		/// Получает следующий объект для обработки (родительский).
		/// </summary>
		/// <param name="idVersionCurrenrObject">Идентификатор версии текущего объекта.</param>
		/// <returns>Идентификатор версии следующего обрабатываемого объекта.</returns>
		private int GetIDVersionNextObject(int idVersionCurrenrObject)
		{
			// Получить родительские объекты
			List<PropertiesParentObject> list_parents_current_object = this.m_LoodsmanAPI.GetParentObject(idVersionCurrenrObject);
			// Если они есть
			if (list_parents_current_object.Count != 0) {
				// Первая запись будет соответствовать родителю реального объекта, остальные - ссылке
				// Получение идентификатора версии родителя
				return list_parents_current_object[0].IDVersion;
			}
			else
				return -1;
		}

		/// <summary>
		/// Получает каталог для хранения файлов текущего пользователя.
		/// </summary>
		/// <returns>Имя каталога для хранения файлов текущего пользователя.</returns>
		/// <exception cref="System.ApplicationException">Не удалось получить каталог для хранения файлов текущего пользователя.</exception>
		private string GetFileDirectoryCurrentUser()
		{
			InfoAboutCurrentUser info_current_user = this.m_LoodsmanAPI.GetInfoAboutCurrentUser();
			if (info_current_user != null)
				return info_current_user.FileDirectory;
			else {
				m_Logger.Error("Не удалось получить каталог для хранения файлов текущего пользователя");
				MessageBox.Show("Не удалось получить каталог для хранения файлов текущего пользователя. Возможно он не задан для данного пользователя в настройках программы. Обратитесь к администратору.",
							"Не задан каталог для хранения файлов", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				throw new System.ApplicationException();
			}
		}

		/// <summary>
		/// Получает значение атрибута объекта по его названию.
		/// </summary>
		/// <param name="idVersionObject">Идентификатор версии объекта.</param>
		/// <param name="attributeName">Название атрибута.</param>
		/// <returns>Строковое представление значения атрибута объекта.</returns>
		/// <remarks>Для типов атрибута Image и Text значение атрибута всегда равно пустой строке.</remarks>
		/// <remarks>Если у объекта отсутствует атрибут с таким названием, то будет возвращена пустая строка.</remarks>
		private string GeAttributeValueByName(int idVersionObject, string attributeName)
		{
			List<AttributeObject> list_attributes_current_object = this.m_LoodsmanAPI.GeAllAttributeObject(idVersionObject);
			AttributeObject found_attributes_current_object = list_attributes_current_object.Find(predicate => predicate.Name.ToLower().Trim() == attributeName.ToLower().Trim());
			if (found_attributes_current_object != null)
				return found_attributes_current_object.Value;
			else
				return string.Empty;
		}

		/// <summary>
		/// Составляет имя подкаталога по значению ключевого атрибута, версии и атрибута "Наименование" текущего объекта.
		/// </summary>
		/// <param name="keyAttributeObject">Значение ключевого атрибута текущего объекта.</param>
		/// <param name="versionCurrentObject">Значение версии текущего объекта.</param>
		/// <param name="attributeNameObject">Значение атрибута "Наименование" текущего объекта.</param>
		/// <returns>Составленное имя подкаталога.</returns>
		private string NameBuildingSubFolder(string keyAttributeObject, string versionCurrentObject, string attributeNameObject)
		{
			if (attributeNameObject == string.Empty)
				if (versionCurrentObject == string.Empty)
					return keyAttributeObject;
				else
					return String.Format("{0}, версия {1}", keyAttributeObject, versionCurrentObject);
			else
				if (versionCurrentObject == string.Empty)
					return String.Format("{0} - {1}", keyAttributeObject, attributeNameObject);
				else
					return String.Format("{0} - {1}, версия {2}", keyAttributeObject, attributeNameObject, versionCurrentObject);
		}

		/// <summary>
		/// Обработчик изменения списка выбранных подкаталогов.
		/// </summary>
		/// <param name="sharedData">Разделяемые данные бизнес логики и окна корректировки пути.</param>
		/// <exception cref="System.ApplicationException">Одна из поддиректорий содержит в имени недопустимые символы.</exception>
		public void ReCreatePath(SharedData sharedData)
		{
			string current_path;
			if ((sharedData.CountSkippedSelectSubDirectories = FoundParentPath(sharedData.SelectSubDirectories, out current_path)) == 0)
				current_path = string.Empty;
			List<string> remaining_sub_directories = sharedData.SelectSubDirectories.Skip(sharedData.CountSkippedSelectSubDirectories).ToList();

			string replacement_path = string.Empty;
			try {
				foreach (string sub_directory in remaining_sub_directories) {
					replacement_path = ReplaceReplaceableSymbolsInPath(sub_directory);
					current_path = Path.Combine(current_path, replacement_path);
				}
			}
			catch (ArgumentException) {
				if (replacement_path.Intersect(Path.GetInvalidPathChars()).Any()) {
					m_Logger.Error("Не удалось составить имя директории. Причина: недопустимые символы в имени поддиректории {0}", replacement_path);
					MessageBox.Show(String.Format("Ключевой атрибут одного или нескольких объектов, его версия или атрибут «Наименование» содержат один или несколько недопустимых символов. При этом cловарь заменяемых символов в файле основных настроек плагина не полон! В нём отсутствуют недопустимые символы для директорий: {0}. Обратитесь к администратору.", new String(Path.GetInvalidPathChars())),
						"Недопустимые символы в имени директории", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else {
					m_Logger.Error("Не удалось составить имя директории. Причина: недопустимые символы в ближайшем родительском каталоге {0}, сохранённом в словаре для замены создаваемых путей", current_path);
					MessageBox.Show("Cловарь для замены создаваемых путей в файле основных настроек плагина повреждён! Обратитесь к администратору.", "Cловарь для замены создаваемых путей повреждён", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				throw new System.ApplicationException();
			}
			sharedData.CurrentPath = current_path;
		}

		/// <summary>
		/// Выполняет поиск ближайшего родительского пути, сохранённого в словаре для замены создаваемых путей, к указанному списку требуемых подкаталогов.
		/// </summary>
		/// <param name="listSubDirectory">Cписок требуемых подкаталогов.</param>
		/// <param name="parentPath">Возвращаемый родительский путь, если он найден; в противном случае — значение по умолчанию для типа string. Этот параметр передаётся неинициализированным.</param>
		/// <returns>Количество требуемых подкаталогов от корня, для которых найден ближайший родительский путь.</returns>
		private int FoundParentPath(List<string> listSubDirectory, out string parentPath)
		{
			for (int current_count_skipped_elements = listSubDirectory.Count; current_count_skipped_elements > 0; --current_count_skipped_elements)
				if (this.m_PluginSettings.ReplaceablePaths.TryGetValue(HttpUtility.HtmlEncode(CreatePathInLoodsman(listSubDirectory.Take(current_count_skipped_elements).ToList())), out parentPath))
					return current_count_skipped_elements;

			parentPath = default(string);
			return 0;
		}

		/// <summary>
		/// Создаёт имя каталога в Лоцмане по списку требуемых подкаталогов.
		/// </summary>
		/// <param name="listSubDirectories">Cписок требуемых подкаталогов.</param>
		/// <returns>Имя каталога, составленное из списка требуемых подкаталогов.</returns>
		/// <remarks>Имя каталога может содержать недопустимые символы для каталогов в данной операционной системе. Перед созданием каталога в операционной системе рекомендуется произвести проверку и замену недопустимых символов.</remarks>
		private string CreatePathInLoodsman(List<string> listSubDirectories)
		{
			string current_path = string.Empty;
			foreach (string sub_directory in listSubDirectories)
				current_path = current_path + Path.PathSeparator + sub_directory;
			return current_path;
		}


		/// <summary>
		/// Заменяет в пути символы, которые присутствуют в словаре заменяемых символов.
		/// </summary>
		/// <param name="path">Проверяемый путь.</param>
		/// <returns>Путь с заменёнными символами.</returns>
		private string ReplaceReplaceableSymbolsInPath(string path)
		{
			string  current_path = String.Copy(path);
			foreach (KeyValuePair<string, string> symbol in this.m_PluginSettings.ReplaceableSymbols)
				if (current_path.Contains(symbol.Key))
					current_path = current_path.Replace(symbol.Key, symbol.Value);
			return current_path;
		}

		/// <summary>
		/// Создаёт директорию по указанному пути.
		/// </summary>
		/// <param name="path">Путь, по которому необходимо создать директорию.</param>
		/// <returns>true - если директория была создана, false - если директория по указанному пути уже существует.</returns>
		private bool CreateDirectoryByPath(string path)
		{
			try 
			{
				if (!Directory.Exists(path)) {
					Directory.CreateDirectory(path);
					return true;
				}
				else
					return false;
			}
			catch (System.IO.DirectoryNotFoundException) 
			{
				m_Logger.Error("Не возможно создать директорию {0}. Причина: указанный путь не найден.", path);
				MessageBox.Show(String.Format("Путь {0} не найден. Возможно указан неподключенный диск. Обратитесь к администратору.", path),
					"Путь не найден", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				throw new System.ApplicationException();
			}
			catch (UnauthorizedAccessException) 
			{
				m_Logger.Error("Не возможно создать директорию {0}. Причина: пользователя не достаточно прав для создания данной директории.", path);
				MessageBox.Show(String.Format("Вы не обладаете достаточными правами доступа к директории {0}. Обратитесь к администратору.", path),
					"Отсутствуют необходимые права доступа", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				throw new System.ApplicationException();
			}
			catch (PathTooLongException) 
			{
				m_Logger.Error("Не возможно создать директорию {0}. Причина: длина пути превышает установленное в системе максимальное значение.", path);
				MessageBox.Show(String.Format("Длина пути {0} превышает установленное в системе максимальное значение. Например, для платформ на основе Windows длина пути не должна превышать 248 символов.", path),
					"Превышено количество символов в пути", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				throw new System.ApplicationException();
			}
			catch (NotSupportedException) 
			{
				m_Logger.Error("Не возможно создать директорию {0}. Причина: указанный путь cодержит двоеточие (:), которое не является частью буквы диска.", path);
				MessageBox.Show(String.Format("Путь {0} cодержит двоеточие (:), которое не является частью буквы диска.", path),
					"Недопустимый путь", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				throw new System.ApplicationException();
			}
			
			catch (System.IO.IOException)
			{
				m_Logger.Error("Не возможно создать директорию {0}. Причина: указанный путь cодержит имя файла или не опознанное имя сети", path);
				MessageBox.Show(String.Format("Путь {0} cодержит имя файла или указанное имя сети не опознано. Возможно поврежден файл настроек плагина. Обратитесь к администратору.", path),
					"Путь cодержит имя файла или имя сети не опознано", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				throw new System.ApplicationException();
			}
		}
	}
}
