using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VP.Xml.Serialization;

namespace VP.Loodsman.PluginSettings
{
	/// <summary>
	/// Настройки плагина.
	/// </summary>
	public class Settings
    {
		/// <summary>
		/// Инициализирует новый экземпляр класса Settings.
		/// </summary>
		private Settings()  {}

		/// <summary>
		/// Получает или задаёт путь к файлу основных настроек плагина.
		/// </summary>
		private string m_PathMainSettings { get; set; }

		/// <summary>
		/// Получает или задаёт обрабатываемые типы объектов Лоцмана.
		/// </summary>
		public List<string> ProcessedTypes { get; set; }

		/// <summary>
		/// Получает или задаёт словарь для замены создаваемых путей.
		/// </summary>
		public SerializableDictionary<string, string> ReplaceablePaths { get; set; }

		/// <summary>
		/// Получает или задаёт словарь для замены символов в создаваемых каталогах.
		/// </summary>
		public SerializableDictionary<string, string> ReplaceableSymbols { get; set; }

		/// <summary>
		/// Получает или задаёт объект, содержащий настройки путей плагина.
		/// </summary>
		private PathsSettings m_PathsSettings { get; set; }

		/// <summary>
		/// Получает или задаёт объект, содержащий основные настройки плагина.
		/// </summary>
		private MainSettings m_MainSettings { get; set; }

		/// <summary>
		/// Загружает настройки плагина.
		/// </summary>
		/// <returns>Объект, содержащий настройки плагина.</returns>
		/// <exception cref="System.DllNotFoundException">Произошла одна или более ошибок при загрузке настроек плагина по умолчанию.</exception>
		public static Settings LoadSettings()
		{
			Settings settings = new Settings();

			settings.m_PathsSettings = PathsSettings.LoadPathsSettings();
			settings.m_PathMainSettings = settings.m_PathsSettings.PathMainSettings;
			settings.m_PathsSettings.PathMainSettings = null;

			settings.m_MainSettings = MainSettings.LoadMainSettings(settings.m_PathMainSettings);
			settings.ProcessedTypes = settings.m_MainSettings.ProcessedTypes;
			settings.ReplaceablePaths = settings.m_MainSettings.ReplaceablePaths;
			settings.ReplaceableSymbols = settings.m_MainSettings.ReplaceableSymbols;
			settings.m_MainSettings.ProcessedTypes = null;
			settings.m_MainSettings.ReplaceablePaths = null;
			settings.m_MainSettings.ReplaceableSymbols = null;

			return settings;
		}

		/// <summary>
		/// Сохраняет настройки плагина.
		/// </summary>
		public void SaveSettings()
		{
			m_PathsSettings.PathMainSettings = this.m_PathMainSettings;
			m_PathsSettings.SavePathsSettings();

			m_MainSettings.ProcessedTypes = this.ProcessedTypes;
			m_MainSettings.ReplaceablePaths = this.ReplaceablePaths;
			m_MainSettings.ReplaceableSymbols = this.ReplaceableSymbols;
			m_MainSettings.SaveMainSettings(this.m_PathMainSettings);
		}


    }
}
