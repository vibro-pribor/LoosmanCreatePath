using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoodsmanDotNet;

namespace VPLoodsmanAPI
{
	/// <summary>
	/// Значок оповещения в Лоцмане.
	/// </summary>
	public enum LoodsmanIcon
	{
		/// <summary>
		/// Значок выводиться не будет.
		/// </summary>
		NoIcon,
		/// <summary>
		/// Будет выведен логотип Лоцмана.
		/// </summary>
		LoodsmanLogo,
		/// <summary>
		/// Будет выведен значок, состоящий из белого знака "X" на круге красного цвета.
		/// </summary>
		Error,
		/// <summary>
		/// Будет выведен значок, состоящий из буквы i в нижнем регистре, помещенной в кружок.
		/// </summary>
		Information,
		/// <summary>
		/// Будет выведен значок, состоящий из вопросительного знака в кружке.
		/// </summary>
		Question,
		/// <summary>
		/// Будет выведен значок, состоящий из восклицательного знака в желтом треугольнике.
		/// </summary>
		Exclamation
	};

	/// <summary>
	/// Отображает оповещение с текстом для пользователя в приложении ЛОЦМАН:PLM. LoodsmanNotify может содержать текст и символы, с помощью которых информируется и инструктируется пользователь.
	/// </summary>
	public static class LoodsmanNotify
	{
		/// <summary>
		/// Отображает в заданном приложении ЛОЦМАН:PLM оповещение, содержащее заданный текст, заголовок, а также значки в сообщении и трее.
		/// </summary>
		/// <param name="owner">Интерфейс приложения ЛОЦМАН:PLM, в котором необходимо вывести данное оповещение.</param>
		/// <param name="text">Текст, отображаемый в оповещении.</param>
		/// <param name="caption">Текст для отображения в строке заголовка оповещения.</param>
		/// <param name="mainIcon">Значок, отображаемый в сообщении.</param>
		/// <param name="trayIcon">Значок, отображаемый в трее.</param>
		public static void Show(ILoodsmanApplication owner, string text, string caption, LoodsmanIcon mainIcon, LoodsmanIcon trayIcon)
		{
			int notify_kind = GetIDNotifyKindByIcons(mainIcon, trayIcon);
			owner.NotifyUser(caption, text, notify_kind, 0);
		}

		/// <summary>
		/// Получает идентификатор типа оповещения в ЛОЦМАН:PLM по его значкам в сообщении и трее.
		/// </summary>
		/// <param name="mainIcon">Значок, отображаемый в сообщении.</param>
		/// <param name="trayIcon">Значок, отображаемый в трее.</param>
		/// <returns>Идентификатор типа оповещения соответствующий указанным значкам в сообщении и трее.</returns>
		/// <exception cref="System.ArgumentException">Указанное сочетание значков сообщения и трея не существует.</exception>
		private static int GetIDNotifyKindByIcons(LoodsmanIcon mainIcon, LoodsmanIcon trayIcon)
		{
			if (mainIcon != LoodsmanIcon.NoIcon && mainIcon != trayIcon)
				throw new System.ArgumentException(String.Format("Сочетания значков сообщения {0} и трея {1} не существует", Enum.GetName(typeof(LoodsmanIcon), mainIcon), Enum.GetName(typeof(LoodsmanIcon), trayIcon)));

			switch (mainIcon) {
				case LoodsmanIcon.NoIcon:
					switch (trayIcon) {
						case LoodsmanIcon.NoIcon: return 0;
						case LoodsmanIcon.Error: return 7;
						case LoodsmanIcon.Question: return 6;
						case LoodsmanIcon.Exclamation: return 8;
						default: throw new System.ArgumentException(String.Format("Сочетания значков сообщения {0} и трея {1} не существует", Enum.GetName(typeof(LoodsmanIcon), mainIcon), Enum.GetName(typeof(LoodsmanIcon), trayIcon)));
					}
				case LoodsmanIcon.LoodsmanLogo: return 1;
				case LoodsmanIcon.Error: return 2;
				case LoodsmanIcon.Information: return 3;
				case LoodsmanIcon.Question: return 4;
				case LoodsmanIcon.Exclamation: return 5;
				default: throw new System.ArgumentException(String.Format("Сочетания значков сообщения {0} и трея {1} не существует", Enum.GetName(typeof(LoodsmanIcon), mainIcon), Enum.GetName(typeof(LoodsmanIcon), trayIcon)));

			}
		}
	}
}
