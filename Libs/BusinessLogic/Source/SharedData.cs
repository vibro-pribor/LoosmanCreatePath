using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASCON.Loodsman.CreatePath
{
	/// <summary>
	/// Содержит разделяемые данные бизнес логики и окна корректировки пути.
	/// </summary>
	public class SharedData
	{
		/// <summary>
		/// Делегат для обработчиков событий, сообщающих об изменении списка выбранных подкаталогов.
		/// </summary>
		public delegate void MethodsReCreatePath(SharedData sharedData);

		/// <summary>
		/// Событие, сообщающее об изменении списка выбранных подкаталогов.
		/// </summary>
		public event MethodsReCreatePath OnChangeSelectSubDirectories = delegate { };

		/// <summary>
		/// Делегат для обработчиков событий, сообщающих об изменении текущего создаваемого пути.
		/// </summary>
		public delegate void MethodsChangePathCurrentPath();

		/// <summary>
		/// Событие, сообщающее об изменении текущего создаваемого пути.
		/// </summary>
		public event MethodsChangePathCurrentPath OnChangePathCurrentPath = delegate { };

		/// <summary>
		/// Делегат для обработчиков событий, сообщающих об изменении количества пропускаемых при выводе выбранных подкаталогов.
		/// </summary>
		public delegate void MethodsReShowSelectSubDirectories();

		/// <summary>
		/// Событие, сообщающее об изменении количества пропускаемых при выводе выбранных подкаталогов.
		/// </summary>
		public event MethodsReShowSelectSubDirectories OnChangeCountSkippedSelectSubDirectories = delegate { };

		/// <summary>
		/// Объект блокировки списка выбранных подкаталогов.
		/// </summary>
		private object m_LockerSelectSubDirectories = new object();

		/// <summary>
		/// Список выбранных подкаталогов.
		/// </summary>
		private List<string> m_SelectSubDirectories;

		/// <summary>
		/// Получает или задаёт список выбранных подкаталогов.
		/// </summary>
		public List<string> SelectSubDirectories
		{
			get
			{
				lock (m_LockerSelectSubDirectories) 
				{
					return this.m_SelectSubDirectories;
				}
			}
			set
			{
				lock (m_LockerSelectSubDirectories) 
				{
					this.m_SelectSubDirectories = value;
					this.OnChangeSelectSubDirectories(this);
				}
			}
		}

		/// <summary>
		/// Объект блокировки текущего создаваемого путь.
		/// </summary>
		private object m_LockerCurrentPath = new object();

		/// <summary>
		/// Текущий создаваемый путь.
		/// </summary>
		private string m_CurrentPath;

		/// <summary>
		/// Получает или задаёт текущий создаваемый путь.
		/// </summary>
		public string CurrentPath
		{
			get
			{
				lock (m_LockerCurrentPath) 
				{
					return this.m_CurrentPath;
				}
			}
			set
			{
				lock (m_LockerCurrentPath) 
				{
					this.m_CurrentPath = value;
					this.OnChangePathCurrentPath();
				}
			}
		}

		/// <summary>
		/// Количество пропускаемых при выводе выбранных подкаталогов.
		/// </summary>
		private int m_CountSkippedSelectSubDirectories;

		/// <summary>
		/// Объект блокировки количества пропускаемых при выводе выбранных подкаталогов.
		/// </summary>
		private object m_LockerCountSkippedSelectSubDirectories = new object();


		/// <summary>
		/// Получает или задаёт количество пропускаемых при выводе выбранных подкаталогов.
		/// </summary>
		public int CountSkippedSelectSubDirectories
		{
			get
			{
				lock (m_LockerCountSkippedSelectSubDirectories) {
					return this.m_CountSkippedSelectSubDirectories;
				}
			}
			set
			{
				lock (m_LockerCountSkippedSelectSubDirectories) {
					this.m_CountSkippedSelectSubDirectories = value;
					this.OnChangeCountSkippedSelectSubDirectories();
				}
			}
		}
	}
}
