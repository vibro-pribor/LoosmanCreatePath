using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using VP.Xml.Serialization;
using NLog;

namespace ASCON.Loodsman.CreatePath
{
	/// <summary>
	/// Содержит логику работы окна корректировки создаваемого пути.
	/// </summary>
	public partial class CheckPath : Form
	{
		/// <summary>
		/// Логирование
		/// </summary>
		private static readonly Logger m_Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Получает разделяемые данные бизнес логики и данного окна.
		/// </summary>
		public SharedData SharedDataDlg {get; private set;}

		/// <summary>
		/// Создаёт окно корректировки создаваемого пути с указанными разделяемыми данными бизнес логики и данного окна.
		/// </summary>
		/// <param name="sharedData">Разделяемые данные бизнес логики и создаваемого окна.</param>
		/// <exception cref="System.ArgumentNullException">Параметр sharedData или его свойства имеют значение null.</exception>
		public CheckPath(SharedData sharedData)
		{
			if (sharedData == null || sharedData.SelectSubDirectories == null || sharedData.CurrentPath == null)
				throw new System.ArgumentNullException("Параметр sharedData или его свойства имеют значение null");
			this.SharedDataDlg = sharedData;
			InitializeComponent();
			this.SharedDataDlg.OnChangePathCurrentPath += RefreshPath;
			this.TreeSubDirectories.Nodes.Clear();
			TreeNode tree_root = CreateTreeSubDirectories(this.SharedDataDlg.SelectSubDirectories.Skip(this.SharedDataDlg.CountSkippedSelectSubDirectories).ToList());
			if (tree_root != null)
				this.TreeSubDirectories.Nodes.Add(tree_root);
		}

		/// <summary>
		/// Обработчик клика левой кнопки мыши по кнопке "Создать".
		/// </summary>
		/// <param name="sender">Объект, создавший событие.</param>
		/// <param name="e">Объект, содержащий данные события.</param>
		private void ButtonOK_Click(object sender, EventArgs e)
		{
			try {
				this.SharedDataDlg.SelectSubDirectories = GetListCheckedNode();
			}
			catch (System.ApplicationException) {
				this.Close();
			}
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		/// <summary>
		/// Обработчик клика левой кнопки мыши по кнопке "Отмена".
		/// </summary>
		/// <param name="sender">Объект, создавший событие.</param>
		/// <param name="e">Объект, содержащий данные события.</param>
		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			try {
				this.SharedDataDlg.SelectSubDirectories = this.SharedDataDlg.SelectSubDirectories.Take(this.SharedDataDlg.CountSkippedSelectSubDirectories).ToList();
			}
			catch (System.ApplicationException) {
				this.Close();
			}
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		/// <summary>
		/// Обработчик загрузки окна.
		/// </summary>
		/// <param name="sender">Объект, создавший событие.</param>
		/// <param name="e">Объект, содержащий данные события.</param>
		private void CheckPath_Load(object sender, EventArgs e)
		{
			RefreshPath();
			CheckAllNodes();
			this.ButtonUpdateNoteText.Enabled = false;
			try
			{
				this.ButtonUpdateNoteText.Image = VP.Resources.IResourcesManager.StaticFactory.Instance.GetImageFromRecources("ConfirmationIcon.png", 54, 54);
				this.ButtonOK.Image = VP.Resources.IResourcesManager.StaticFactory.Instance.GetImageFromRecources("AddFolderIcon.png", 54, 54);
				this.ButtonCancel.Image = VP.Resources.IResourcesManager.StaticFactory.Instance.GetImageFromRecources("CancelIcon.png", 48, 48);
				this.LabelIconInfo.Image = VP.Resources.IResourcesManager.StaticFactory.Instance.GetImageFromRecources("InformationIcon.png", 64, 64);
			}
			catch (System.DllNotFoundException exc) {
				m_Logger.Error("Не удалось загрузить ресурсы плагина. Причина: {0}. Стек вызова: {1}", exc.Message, exc.StackTrace);
				MessageBox.Show("Не удалось загрузить ресурсы плагина. Возможно файлы плагина были повреждены. Обратитесь к администратору.",
						"Ошибка загрузки настроек плагина", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				throw new System.ApplicationException();
			}
		}

		/// <summary>
		/// Обработчик изменения текущего создаваемого пути.
		/// </summary>
		public void RefreshPath()
		{
			this.CurrentPath.Text = this.SharedDataDlg.CurrentPath;
		}


		/// <summary>
		/// Обработчик помечания галочкой элемента в дереве.
		/// </summary>
		/// <param name="sender">Объект, создавший событие.</param>
		/// <param name="e">Объект, содержащий данные события.</param>
		private void TreeSubDirectories_AfterCheck(object sender, TreeViewEventArgs e)
		{
			try {
				this.SharedDataDlg.SelectSubDirectories = this.GetListCheckedNode();
			}
			catch (System.ApplicationException) {
				this.Close();
			}
		}

		/// <summary>
		/// Обработчик выделения элемента в дереве.
		/// </summary>
		/// <param name="sender">Объект, создавший событие.</param>
		/// <param name="e">Объект, содержащий данные события.</param>
		private void TreeSubDirectories_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.CurrentNodeText.Text = this.NewNodeText.Text = e.Node.Text;
			this.ButtonUpdateNoteText.Enabled = true;
		}

		/// <summary>
		/// Обработчик нажатия клавиши на клавиатуре в элементе, содержащем новое название элемента дерева.
		/// </summary>
		/// <param name="sender">Объект, создавший событие.</param>
		/// <param name="e">Объект, содержащий данные события.</param>
		private void NewNodeText_KeyPress(object sender, KeyPressEventArgs e)
		{
			switch (e.KeyChar)
			{
				// Клавиша "Enter"
				case '\r': this.ButtonUpdateNoteText.Focus(); break;
				// Клавиша "Esc"
				case (char)27: this.NewNodeText.Text = this.CurrentNodeText.Text;  this.TreeSubDirectories.Focus(); break;
				// Сочетание клавиш "Ctrl" и "Z"
				case (char)26: this.NewNodeText.Text = this.CurrentNodeText.Text;  this.TreeSubDirectories.Focus(); break;
				default: return;
			}

		}

		/// <summary>
		/// Обработчик клика левой кнопки мыши по кнопке "Переименовать".
		/// </summary>
		/// <param name="sender">Объект, создавший событие.</param>
		/// <param name="e">Объект, содержащий данные события.</param>
		private void ButtonUpdateNoteText_Click(object sender, EventArgs e)
		{
			if (this.TreeSubDirectories.SelectedNode != null) {
				this.TreeSubDirectories.SelectedNode.Text = this.CurrentNodeText.Text = this.NewNodeText.Text;
				try {
					this.SharedDataDlg.SelectSubDirectories = this.GetListCheckedNode();
				}
				catch (System.ApplicationException) {
					this.Close();
				}
			}
		}

		/// <summary>
		/// Строит дерево, содержащее создаваемые подкаталоги, отражающее их иерархию.
		/// </summary>
		/// <param name="listSubDirectories">Список, содержащий создаваемые подкаталоги в порядке их вложенности.</param>
		/// <returns>Корень дерева, содержащего создаваемые подкаталоги, отражающего их иерархию.</returns>
		private static TreeNode CreateTreeSubDirectories(List<string> listSubDirectories)
		{
			TreeNode current_note;
			TreeNode last_note = null;
			if (listSubDirectories.Count > 0) {
				last_note = new TreeNode(listSubDirectories[listSubDirectories.Count - 1]);
				for (int i=listSubDirectories.Count - 2; i >= 0; --i) {
					current_note = new TreeNode(listSubDirectories[i]);
					current_note.Nodes.Add(last_note);
					last_note = current_note;
				}
			}
			return last_note;
		}

		/// <summary>
		/// Получает список помеченных галочками элементов в дереве.
		/// </summary>
		/// <returns>Список, содержащий помеченные галочками элементы в дереве.</returns>
		private List<string> GetListCheckedNode()
		{
			List<string> list_checked_node = new List<string>(this.SharedDataDlg.SelectSubDirectories.Take(this.SharedDataDlg.CountSkippedSelectSubDirectories).ToList());
			if (this.TreeSubDirectories.Nodes.Count != 0) {
				TreeNode current_node = this.TreeSubDirectories.Nodes[0];
				while (true) {
					if (current_node.Checked)
						list_checked_node.Add(current_node.Text);
					if (current_node.Nodes.Count != 0)
						current_node = current_node.Nodes[0];
					else
						break;
				}
			}
			return list_checked_node;
		}

		/// <summary>
		/// Помечает галочками все элементы в дереве и разворачивает их.
		/// </summary>
		private void CheckAllNodes()
		{
			if (this.TreeSubDirectories.Nodes.Count != 0) {
				TreeNode current_node = this.TreeSubDirectories.Nodes[0];
				while (true) {
					current_node.Checked = true;
					current_node.Expand();
					if (current_node.Nodes.Count != 0)
						current_node = current_node.Nodes[0];
					else
						break;
				}
			}
		}

	}
}
