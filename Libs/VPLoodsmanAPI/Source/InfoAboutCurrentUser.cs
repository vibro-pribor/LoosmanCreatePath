using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPLoodsmanAPI
{
	/// <summary>
	/// Содержит информацию о пользователе.
	/// </summary>
	public class InfoAboutCurrentUser
	{
		/// <summary>
		/// Получает или задаёт имя пользователя.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Получает или задаёт полное имя пользователя.
		/// </summary>
		public string Fullname { get; set; }

		/// <summary>
		/// Получает или задаёт адрес электронной почты пользователя.
		/// </summary>
		public string Email{ get; set; }

		/// <summary>
		/// Получает или задаёт рабочую папку для проектов пользователя.
		/// </summary>
		public string WorkDirectory { get; set; }

		/// <summary>
		/// Получает или задаёт папку для хранения файлов пользователя.
		/// </summary>
		public string FileDirectory { get; set; }

	}
}
