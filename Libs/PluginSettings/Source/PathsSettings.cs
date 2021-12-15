using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VP.Xml.Serialization;
using System.IO;
using VP.Resources.UnplugResourcesManager;
using NLog;

namespace VP.Loodsman.PluginSettings
{
	/// <summary>
	/// Настройки путей плагина.
	/// </summary>
	public class PathsSettings
	{
		/// <summary>
		/// Логирование.
		/// </summary>
		private static readonly Logger m_Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Инициализирует новый экземпляр класса PathsSettings.
		/// </summary>
		private PathsSettings() {}

		/// <summary>
		/// Получает или задаёт путь к файлу основных настроек плагина.
		/// </summary>
		public string PathMainSettings { get; set; }

		/// <summary>
		/// Загружает настройки путей плагина.
		/// </summary>
		/// <returns>Объект, содержащий настройки путей плагина.</returns>
		/// <exception cref="System.DllNotFoundException">Произошла одна или более ошибок при загрузке настроек путей плагина по умолчанию.</exception>
		static public PathsSettings LoadPathsSettings()
		{
			string path_file_settings = GetPathFilePathsSettings();
			PathsSettings settings;
			try
			{
				settings = XMLSerialize<PathsSettings>.Deserialize(path_file_settings);
			}
			catch (Exception exc) {
				m_Logger.Info("Невозможно загрузить файл настроек путей плагина по указанному пути. Путь: {0}. Причина: {1}. Будут использованы настройки по умолчанию.", path_file_settings, exc.Message);
				settings = LoadDefaultPathsSettings();
			}
			return settings;
		}
		/// <summary>
		/// Сохраняет настройки путей плагина.
		/// </summary>
		public void SavePathsSettings()
		{
			string path_file_settings = GetPathFilePathsSettings();
			try {
				XMLSerialize<PathsSettings>.Serialize(path_file_settings, this);
			}
			catch (Exception exc)
			{
				m_Logger.Info("Невозможно сохранить файл настроек путей плагина по указанному пути. Путь: {0}. Причина: {1}", path_file_settings, exc.Message);
			}

		}

		/// <summary>
		/// Загружает настройки путей плагина по умолчанию.
		/// </summary>
		/// <returns>Объект, содержащий настройки путей плагина.</returns>
		/// <exception cref="System.DllNotFoundException">Произошла одна или более ошибок при загрузке настроек путей плагина по умолчанию.</exception>
		private static PathsSettings LoadDefaultPathsSettings()
		{
			PathsSettings settings;
			try {
				settings = XMLSerialize<PathsSettings>.Deserialize(VP.Resources.IResourcesManager.StaticFactory.Instance.GetStreamFromRecources("DefaultPathsSettings.vpxml"));
			}
			catch (Exception exc) 
			{
				m_Logger.Error("Невозможно загрузить файл настроек путей плагина по умолчанию. Причина: {0}", exc.Message);
				throw new DllNotFoundException("Невозможно загрузить файл настроек путей плагина по умолчанию", exc);
			}
			settings.PathMainSettings = GetPathFileMainSettings();
			settings.SavePathsSettings();
			return settings;
		}

		/// <summary>
		/// Получает путь, по которому находится файл настроек путей плагина.
		/// </summary>
		/// <returns>Путь, по которому находится файл настроек путей плагина.</returns>
		private static string GetPathFilePathsSettings()
		{
			string path_file_settings = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
			path_file_settings = Path.Combine(path_file_settings, String.Format("Ascon{0}LOODSMAN{0}VPPlugin{0}CreatePath{0}PathsSettings.vpxml",Path.DirectorySeparatorChar));
			return path_file_settings;
		}

		/// <summary>
		/// Получает путь, по которому находится файл основных настроек плагина.
		/// </summary>
		/// <returns>Путь, по которому находится файл основных настроек плагина.</returns>
		private static string GetPathFileMainSettings()
		{
			string path_file_settings = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
			path_file_settings = Path.Combine(path_file_settings, String.Format("Ascon{0}LOODSMAN{0}VPPlugin{0}CreatePath{0}MainSettings.vpxml", Path.DirectorySeparatorChar));
			return path_file_settings;
		}

	}
}
