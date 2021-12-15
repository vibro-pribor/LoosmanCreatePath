namespace ASCON.Loodsman.CreatePath
{
	partial class CheckPath
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckPath));
			this.ButtonOK = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.TreeSubDirectories = new System.Windows.Forms.TreeView();
			this.LabelCurrentPath = new System.Windows.Forms.Label();
			this.CurrentPath = new System.Windows.Forms.Label();
			this.LabelCurrentNodeText = new System.Windows.Forms.Label();
			this.LabelNewNodeText = new System.Windows.Forms.Label();
			this.CurrentNodeText = new System.Windows.Forms.TextBox();
			this.NewNodeText = new System.Windows.Forms.TextBox();
			this.ButtonUpdateNoteText = new System.Windows.Forms.Button();
			this.LabelInfo = new System.Windows.Forms.Label();
			this.LabelIconInfo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ButtonOK
			// 
			this.ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ButtonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ButtonOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ButtonOK.Location = new System.Drawing.Point(18, 422);
			this.ButtonOK.Name = "ButtonOK";
			this.ButtonOK.Size = new System.Drawing.Size(91, 69);
			this.ButtonOK.TabIndex = 2;
			this.ButtonOK.Text = "Создать";
			this.ButtonOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.ButtonOK.UseVisualStyleBackColor = true;
			this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ButtonCancel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ButtonCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ButtonCancel.Location = new System.Drawing.Point(998, 422);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(91, 69);
			this.ButtonCancel.TabIndex = 3;
			this.ButtonCancel.Text = "Отмена";
			this.ButtonCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.ButtonCancel.UseVisualStyleBackColor = true;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// TreeSubDirectories
			// 
			this.TreeSubDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TreeSubDirectories.CheckBoxes = true;
			this.TreeSubDirectories.Cursor = System.Windows.Forms.Cursors.Default;
			this.TreeSubDirectories.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.TreeSubDirectories.ImeMode = System.Windows.Forms.ImeMode.On;
			this.TreeSubDirectories.Location = new System.Drawing.Point(15, 126);
			this.TreeSubDirectories.Name = "TreeSubDirectories";
			this.TreeSubDirectories.Size = new System.Drawing.Size(785, 167);
			this.TreeSubDirectories.TabIndex = 4;
			this.TreeSubDirectories.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeSubDirectories_AfterCheck);
			this.TreeSubDirectories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeSubDirectories_AfterSelect);
			// 
			// LabelCurrentPath
			// 
			this.LabelCurrentPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LabelCurrentPath.AutoSize = true;
			this.LabelCurrentPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.LabelCurrentPath.Location = new System.Drawing.Point(15, 305);
			this.LabelCurrentPath.Name = "LabelCurrentPath";
			this.LabelCurrentPath.Size = new System.Drawing.Size(152, 16);
			this.LabelCurrentPath.TabIndex = 7;
			this.LabelCurrentPath.Text = "Будет создан путь:";
			// 
			// CurrentPath
			// 
			this.CurrentPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CurrentPath.AutoEllipsis = true;
			this.CurrentPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.CurrentPath.Location = new System.Drawing.Point(15, 329);
			this.CurrentPath.Name = "CurrentPath";
			this.CurrentPath.Size = new System.Drawing.Size(1070, 90);
			this.CurrentPath.TabIndex = 8;
			this.CurrentPath.Text = "Текущий путь";
			// 
			// LabelCurrentNodeText
			// 
			this.LabelCurrentNodeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LabelCurrentNodeText.AutoSize = true;
			this.LabelCurrentNodeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.LabelCurrentNodeText.Location = new System.Drawing.Point(808, 126);
			this.LabelCurrentNodeText.Name = "LabelCurrentNodeText";
			this.LabelCurrentNodeText.Size = new System.Drawing.Size(281, 16);
			this.LabelCurrentNodeText.TabIndex = 9;
			this.LabelCurrentNodeText.Text = "Текущее название выбранного элемента";
			// 
			// LabelNewNodeText
			// 
			this.LabelNewNodeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LabelNewNodeText.AutoSize = true;
			this.LabelNewNodeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.LabelNewNodeText.Location = new System.Drawing.Point(808, 170);
			this.LabelNewNodeText.Name = "LabelNewNodeText";
			this.LabelNewNodeText.Size = new System.Drawing.Size(269, 16);
			this.LabelNewNodeText.TabIndex = 10;
			this.LabelNewNodeText.Text = "Новое название  выбранного элемента";
			// 
			// CurrentNodeText
			// 
			this.CurrentNodeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CurrentNodeText.Enabled = false;
			this.CurrentNodeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.CurrentNodeText.Location = new System.Drawing.Point(811, 145);
			this.CurrentNodeText.Name = "CurrentNodeText";
			this.CurrentNodeText.Size = new System.Drawing.Size(275, 22);
			this.CurrentNodeText.TabIndex = 11;
			// 
			// NewNodeText
			// 
			this.NewNodeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.NewNodeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.NewNodeText.Location = new System.Drawing.Point(811, 189);
			this.NewNodeText.Name = "NewNodeText";
			this.NewNodeText.Size = new System.Drawing.Size(275, 22);
			this.NewNodeText.TabIndex = 12;
			this.NewNodeText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewNodeText_KeyPress);
			// 
			// ButtonUpdateNoteText
			// 
			this.ButtonUpdateNoteText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonUpdateNoteText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ButtonUpdateNoteText.ForeColor = System.Drawing.Color.Black;
			this.ButtonUpdateNoteText.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ButtonUpdateNoteText.Location = new System.Drawing.Point(887, 217);
			this.ButtonUpdateNoteText.Name = "ButtonUpdateNoteText";
			this.ButtonUpdateNoteText.Size = new System.Drawing.Size(122, 78);
			this.ButtonUpdateNoteText.TabIndex = 13;
			this.ButtonUpdateNoteText.Text = "Переименовать";
			this.ButtonUpdateNoteText.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.ButtonUpdateNoteText.UseVisualStyleBackColor = true;
			this.ButtonUpdateNoteText.Click += new System.EventHandler(this.ButtonUpdateNoteText_Click);
			// 
			// LabelInfo
			// 
			this.LabelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LabelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.LabelInfo.Location = new System.Drawing.Point(93, 13);
			this.LabelInfo.Name = "LabelInfo";
			this.LabelInfo.Size = new System.Drawing.Size(990, 110);
			this.LabelInfo.TabIndex = 17;
			this.LabelInfo.Text = resources.GetString("LabelInfo.Text");
			// 
			// LabelIconInfo
			// 
			this.LabelIconInfo.Location = new System.Drawing.Point(15, 13);
			this.LabelIconInfo.Name = "LabelIconInfo";
			this.LabelIconInfo.Size = new System.Drawing.Size(74, 71);
			this.LabelIconInfo.TabIndex = 18;
			// 
			// CheckPath
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1109, 500);
			this.Controls.Add(this.LabelIconInfo);
			this.Controls.Add(this.LabelInfo);
			this.Controls.Add(this.ButtonUpdateNoteText);
			this.Controls.Add(this.NewNodeText);
			this.Controls.Add(this.CurrentNodeText);
			this.Controls.Add(this.LabelNewNodeText);
			this.Controls.Add(this.LabelCurrentNodeText);
			this.Controls.Add(this.CurrentPath);
			this.Controls.Add(this.LabelCurrentPath);
			this.Controls.Add(this.TreeSubDirectories);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(1125, 538);
			this.Name = "CheckPath";
			this.Text = "Корректировка пути";
			this.Load += new System.EventHandler(this.CheckPath_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ButtonOK;
		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.TreeView TreeSubDirectories;
		private System.Windows.Forms.Label LabelCurrentPath;
		private System.Windows.Forms.Label CurrentPath;
		private System.Windows.Forms.Label LabelCurrentNodeText;
		private System.Windows.Forms.Label LabelNewNodeText;
		private System.Windows.Forms.TextBox CurrentNodeText;
		private System.Windows.Forms.TextBox NewNodeText;
		private System.Windows.Forms.Button ButtonUpdateNoteText;
		private System.Windows.Forms.Label LabelInfo;
		private System.Windows.Forms.Label LabelIconInfo;

	}
}