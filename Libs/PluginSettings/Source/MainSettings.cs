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
	/// Основные настройки плагина.
	/// </summary>
	public class MainSettings
	{
		/// <summary>
		/// Логирование.
		/// </summary>
		private static readonly Logger m_Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Инициализирует новый экземпляр класса MainSettings.
		/// </summary>
		private MainSettings() { }

		/// <summary>
		/// Получает или задаёт обрабатываемые типы объектов Лоцмана.
		/// </summary>
		public List<string> ProcessedTypes { get; set; }

		/// <summary>
		/// Получает или задаёт словарь для замены создаваемых путей.
		/// </summary>
		public SerializableDictionary <string,string> ReplaceablePaths { get; set; }

		/// <summary>
		/// Получает или задаёт словарь для замены символов в создаваемых каталогах.
		/// </summary>
		public SerializableDictionary<string, string> ReplaceableSymbols { get; set; }

		/// <summary>
		/// Загружает основные настройки плагина.
		/// </summary>
		/// <param name="pathMainSettings">Путь к файлу основных настроек плагина.</param>
		/// <returns>Объект, содержащий основные настройки плагина.</returns>
		/// <exception cref="System.DllNotFoundException">Произошла одна или более ошибок при загрузке основных настроек плагина по умолчанию.</exception>
		public static MainSettings LoadMainSettings(string pathMainSettings)
		{
			MainSettings settings;
			try {
				settings = XMLSerialize<MainSettings>.Deserialize(pathMainSettings);
			}
			catch (Exception exc) {
				m_Logger.Info("Невозможно загрузить файл основных настроек плагина по указанному пути. Путь: {0}. Причина: {1}. Будут использованы настройки по умолчанию.", pathMainSettings,exc.Message);
				settings = LoadDefaultMainSettings(pathMainSettings);
			}
			return settings;
		}

		/// <summary>
		/// Сохраняет основные настройки плагина.
		/// </summary>
		/// <param name="pathMainSettings">Путь, по которому будет сохранён файл основных настроек плагина.</param>
		public void SaveMainSettings(string pathMainSettings)
		{
			try {
				XMLSerialize<MainSettings>.Serialize(pathMainSettings, this);
			}
			catch (Exception exc)
			{
				m_Logger.Info("Невозможно сохранить файл основных настроек плагина по указанному пути. Путь: {0}. Причина: {1}.", pathMainSettings, exc.Message);
			}
		}

		/// <summary>
		/// Загружает основные настройки плагина по умолчанию.
		/// </summary>
		/// <param name="pathMainSettings">Путь, по которому будет сохранён файл основных настроек плагина.</param>
		/// <returns>Объект, содержащий основные настройки плагина.</returns>
		/// <exception cref="System.DllNotFoundException">Произошла одна или более ошибок при загрузке основных настроек плагина по умолчанию.</exception>
		private static MainSettings LoadDefaultMainSettings(string pathMainSettings)
		{
			MainSettings settings;
			try {
				settings = XMLSerialize<MainSettings>.Deserialize(VP.Resources.IResourcesManager.StaticFactory.Instance.GetStreamFromRecources("DefaultMainSettings.vpxml"));
			}
			catch (Exception exc) {
				m_Logger.Error("Невозможно загрузить файл основных настроек плагина по умолчанию. Причина: {0}", exc.Message);
				throw new DllNotFoundException("Невозможно загрузить файл основных настроек плагина по умолчанию", exc);
			}
			settings.SaveMainSettings(pathMainSettings);
			return settings;
		}
	}
}
