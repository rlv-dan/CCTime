namespace CCTime
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.labelDate = new System.Windows.Forms.Label();
			this.objectListView = new BrightIdeasSoftware.ObjectListView();
			this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.buttonWeeklyReport = new System.Windows.Forms.Button();
			this.buttonTimerOnOff = new System.Windows.Forms.Button();
			this.buttonNextDate = new System.Windows.Forms.Button();
			this.buttonPrevDate = new System.Windows.Forms.Button();
			this.buttonTaskUp = new System.Windows.Forms.Button();
			this.buttonTaskDown = new System.Windows.Forms.Button();
			this.buttonAlwaysOnTop = new System.Windows.Forms.Button();
			this.buttonTaskDel = new System.Windows.Forms.Button();
			this.buttonTaskAdd = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.objectListView)).BeginInit();
			this.SuspendLayout();
			// 
			// labelDate
			// 
			this.labelDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelDate.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDate.Location = new System.Drawing.Point(52, 10);
			this.labelDate.Name = "labelDate";
			this.labelDate.Size = new System.Drawing.Size(308, 24);
			this.labelDate.TabIndex = 2;
			this.labelDate.Text = "labelDate";
			this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip.SetToolTip(this.labelDate, "Click to select another day");
			this.labelDate.Click += new System.EventHandler(this.labelDate_Click);
			// 
			// objectListView
			// 
			this.objectListView.AllColumns.Add(this.olvColumn1);
			this.objectListView.AllColumns.Add(this.olvColumn6);
			this.objectListView.AllColumns.Add(this.olvColumn2);
			this.objectListView.AllColumns.Add(this.olvColumn3);
			this.objectListView.AllColumns.Add(this.olvColumn4);
			this.objectListView.AllColumns.Add(this.olvColumn5);
			this.objectListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.objectListView.BackColor = System.Drawing.Color.White;
			this.objectListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.objectListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
			this.objectListView.CellEditUseWholeCell = false;
			this.objectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn6,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5});
			this.objectListView.Cursor = System.Windows.Forms.Cursors.Default;
			this.objectListView.ForeColor = System.Drawing.SystemColors.WindowText;
			this.objectListView.FullRowSelect = true;
			this.objectListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.objectListView.Location = new System.Drawing.Point(6, 47);
			this.objectListView.MultiSelect = false;
			this.objectListView.Name = "objectListView";
			this.objectListView.RowHeight = 32;
			this.objectListView.ShowGroups = false;
			this.objectListView.Size = new System.Drawing.Size(400, 399);
			this.objectListView.TabIndex = 5;
			this.objectListView.UseAlternatingBackColors = true;
			this.objectListView.UseCellFormatEvents = true;
			this.objectListView.UseCompatibleStateImageBehavior = false;
			this.objectListView.View = System.Windows.Forms.View.Details;
			this.objectListView.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.objectListView1_CellEditFinished);
			this.objectListView.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.objectListView1_CellEditStarting);
			this.objectListView.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.objectListView1_CellRightClick);
			this.objectListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.objectListView1_KeyUp);
			// 
			// olvColumn1
			// 
			this.olvColumn1.AspectName = "Title";
			this.olvColumn1.CellEditUseWholeCell = true;
			this.olvColumn1.FillsFreeSpace = true;
			this.olvColumn1.Groupable = false;
			// 
			// olvColumn6
			// 
			this.olvColumn6.AspectName = "AutoTick";
			this.olvColumn6.IsEditable = false;
			this.olvColumn6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.olvColumn6.Width = 10;
			// 
			// olvColumn2
			// 
			this.olvColumn2.AspectName = "Minutes";
			this.olvColumn2.CellEditUseWholeCell = true;
			// 
			// olvColumn3
			// 
			this.olvColumn3.AspectName = "Plus15";
			this.olvColumn3.ButtonSize = new System.Drawing.Size(32, 24);
			this.olvColumn3.ButtonSizing = BrightIdeasSoftware.OLVColumn.ButtonSizingMode.FixedBounds;
			this.olvColumn3.IsButton = true;
			this.olvColumn3.IsEditable = false;
			this.olvColumn3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// olvColumn4
			// 
			this.olvColumn4.AspectName = "Plus30";
			this.olvColumn4.ButtonSize = new System.Drawing.Size(32, 24);
			this.olvColumn4.ButtonSizing = BrightIdeasSoftware.OLVColumn.ButtonSizingMode.FixedBounds;
			this.olvColumn4.IsButton = true;
			this.olvColumn4.IsEditable = false;
			this.olvColumn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// olvColumn5
			// 
			this.olvColumn5.AspectName = "Plus60";
			this.olvColumn5.ButtonMaxWidth = 32;
			this.olvColumn5.ButtonSize = new System.Drawing.Size(32, 24);
			this.olvColumn5.ButtonSizing = BrightIdeasSoftware.OLVColumn.ButtonSizingMode.FixedBounds;
			this.olvColumn5.CellVerticalAlignment = System.Drawing.StringAlignment.Center;
			this.olvColumn5.IsButton = true;
			this.olvColumn5.IsEditable = false;
			this.olvColumn5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// notifyIcon
			// 
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "notifyIcon";
			this.notifyIcon.Visible = true;
			this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
			// 
			// buttonWeeklyReport
			// 
			this.buttonWeeklyReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonWeeklyReport.FlatAppearance.BorderSize = 0;
			this.buttonWeeklyReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonWeeklyReport.Image = global::CCTime.Properties.Resources.calendar;
			this.buttonWeeklyReport.Location = new System.Drawing.Point(177, 458);
			this.buttonWeeklyReport.Name = "buttonWeeklyReport";
			this.buttonWeeklyReport.Size = new System.Drawing.Size(34, 34);
			this.buttonWeeklyReport.TabIndex = 18;
			this.toolTip.SetToolTip(this.buttonWeeklyReport, "Generate a weekly report");
			this.buttonWeeklyReport.UseVisualStyleBackColor = true;
			this.buttonWeeklyReport.Click += new System.EventHandler(this.buttonWeeklyReport_Click);
			// 
			// buttonTimerOnOff
			// 
			this.buttonTimerOnOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonTimerOnOff.FlatAppearance.BorderSize = 0;
			this.buttonTimerOnOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTimerOnOff.Image = global::CCTime.Properties.Resources.clock;
			this.buttonTimerOnOff.Location = new System.Drawing.Point(326, 458);
			this.buttonTimerOnOff.Name = "buttonTimerOnOff";
			this.buttonTimerOnOff.Size = new System.Drawing.Size(34, 34);
			this.buttonTimerOnOff.TabIndex = 17;
			this.toolTip.SetToolTip(this.buttonTimerOnOff, "Toggle timer on and off");
			this.buttonTimerOnOff.UseVisualStyleBackColor = true;
			this.buttonTimerOnOff.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonTimerOnOff_MouseUp);
			// 
			// buttonNextDate
			// 
			this.buttonNextDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNextDate.BackColor = System.Drawing.Color.White;
			this.buttonNextDate.FlatAppearance.BorderSize = 0;
			this.buttonNextDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonNextDate.Image = global::CCTime.Properties.Resources.sign_right;
			this.buttonNextDate.Location = new System.Drawing.Point(366, 7);
			this.buttonNextDate.Name = "buttonNextDate";
			this.buttonNextDate.Size = new System.Drawing.Size(34, 34);
			this.buttonNextDate.TabIndex = 16;
			this.toolTip.SetToolTip(this.buttonNextDate, "Next day");
			this.buttonNextDate.UseVisualStyleBackColor = false;
			this.buttonNextDate.Click += new System.EventHandler(this.buttonNextDate_Click);
			// 
			// buttonPrevDate
			// 
			this.buttonPrevDate.FlatAppearance.BorderSize = 0;
			this.buttonPrevDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonPrevDate.Image = global::CCTime.Properties.Resources.sign_left;
			this.buttonPrevDate.Location = new System.Drawing.Point(12, 7);
			this.buttonPrevDate.Name = "buttonPrevDate";
			this.buttonPrevDate.Size = new System.Drawing.Size(34, 34);
			this.buttonPrevDate.TabIndex = 15;
			this.toolTip.SetToolTip(this.buttonPrevDate, "Previous day");
			this.buttonPrevDate.UseVisualStyleBackColor = true;
			this.buttonPrevDate.Click += new System.EventHandler(this.buttonPrevDate_Click);
			// 
			// buttonTaskUp
			// 
			this.buttonTaskUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonTaskUp.FlatAppearance.BorderSize = 0;
			this.buttonTaskUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTaskUp.Image = global::CCTime.Properties.Resources.sign_up;
			this.buttonTaskUp.Location = new System.Drawing.Point(97, 458);
			this.buttonTaskUp.Name = "buttonTaskUp";
			this.buttonTaskUp.Size = new System.Drawing.Size(34, 34);
			this.buttonTaskUp.TabIndex = 11;
			this.toolTip.SetToolTip(this.buttonTaskUp, "Move selected task up one step (Ctrl-Click to move to top)");
			this.buttonTaskUp.UseVisualStyleBackColor = true;
			this.buttonTaskUp.Click += new System.EventHandler(this.buttonTaskUp_Click);
			// 
			// buttonTaskDown
			// 
			this.buttonTaskDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonTaskDown.FlatAppearance.BorderSize = 0;
			this.buttonTaskDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTaskDown.Image = global::CCTime.Properties.Resources.sign_down;
			this.buttonTaskDown.Location = new System.Drawing.Point(137, 458);
			this.buttonTaskDown.Name = "buttonTaskDown";
			this.buttonTaskDown.Size = new System.Drawing.Size(34, 34);
			this.buttonTaskDown.TabIndex = 10;
			this.toolTip.SetToolTip(this.buttonTaskDown, "Move selected task down one step (Ctrl-Click to move to bottom)");
			this.buttonTaskDown.UseVisualStyleBackColor = true;
			this.buttonTaskDown.Click += new System.EventHandler(this.buttonTaskDown_Click);
			// 
			// buttonAlwaysOnTop
			// 
			this.buttonAlwaysOnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAlwaysOnTop.FlatAppearance.BorderSize = 0;
			this.buttonAlwaysOnTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonAlwaysOnTop.Image = global::CCTime.Properties.Resources.pin_gray;
			this.buttonAlwaysOnTop.Location = new System.Drawing.Point(366, 458);
			this.buttonAlwaysOnTop.Name = "buttonAlwaysOnTop";
			this.buttonAlwaysOnTop.Size = new System.Drawing.Size(34, 34);
			this.buttonAlwaysOnTop.TabIndex = 9;
			this.toolTip.SetToolTip(this.buttonAlwaysOnTop, "Pin window on top to prevent it from hiding");
			this.buttonAlwaysOnTop.UseVisualStyleBackColor = true;
			this.buttonAlwaysOnTop.Click += new System.EventHandler(this.buttonAlwaysOnTop_Click);
			// 
			// buttonTaskDel
			// 
			this.buttonTaskDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonTaskDel.FlatAppearance.BorderSize = 0;
			this.buttonTaskDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTaskDel.Image = global::CCTime.Properties.Resources.sign_delete;
			this.buttonTaskDel.Location = new System.Drawing.Point(57, 458);
			this.buttonTaskDel.Name = "buttonTaskDel";
			this.buttonTaskDel.Size = new System.Drawing.Size(34, 34);
			this.buttonTaskDel.TabIndex = 8;
			this.toolTip.SetToolTip(this.buttonTaskDel, "Remove selected task (Ctrl-Click to delete current day, Alt-Click to remove tasks" +
        " with 0 time)");
			this.buttonTaskDel.UseVisualStyleBackColor = true;
			this.buttonTaskDel.Click += new System.EventHandler(this.buttonTaskDel_Click);
			// 
			// buttonTaskAdd
			// 
			this.buttonTaskAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonTaskAdd.FlatAppearance.BorderSize = 0;
			this.buttonTaskAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTaskAdd.Image = global::CCTime.Properties.Resources.sign_add;
			this.buttonTaskAdd.Location = new System.Drawing.Point(17, 458);
			this.buttonTaskAdd.Name = "buttonTaskAdd";
			this.buttonTaskAdd.Size = new System.Drawing.Size(34, 34);
			this.buttonTaskAdd.TabIndex = 7;
			this.toolTip.SetToolTip(this.buttonTaskAdd, "Add a task (Ctrl-Click to add a new day)");
			this.buttonTaskAdd.UseVisualStyleBackColor = true;
			this.buttonTaskAdd.Click += new System.EventHandler(this.buttonTaskAdd_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(412, 498);
			this.Controls.Add(this.buttonWeeklyReport);
			this.Controls.Add(this.buttonTimerOnOff);
			this.Controls.Add(this.buttonNextDate);
			this.Controls.Add(this.buttonPrevDate);
			this.Controls.Add(this.buttonTaskUp);
			this.Controls.Add(this.buttonTaskDown);
			this.Controls.Add(this.buttonAlwaysOnTop);
			this.Controls.Add(this.buttonTaskDel);
			this.Controls.Add(this.buttonTaskAdd);
			this.Controls.Add(this.objectListView);
			this.Controls.Add(this.labelDate);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMain";
			this.Text = "CC Time";
			this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.objectListView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelDate;
		private BrightIdeasSoftware.ObjectListView objectListView;
		private BrightIdeasSoftware.OLVColumn olvColumn1;
		private BrightIdeasSoftware.OLVColumn olvColumn2;
		private BrightIdeasSoftware.OLVColumn olvColumn3;
		private BrightIdeasSoftware.OLVColumn olvColumn4;
		private BrightIdeasSoftware.OLVColumn olvColumn5;
		private System.Windows.Forms.Button buttonTaskAdd;
		private System.Windows.Forms.Button buttonTaskDel;
		private System.Windows.Forms.Button buttonAlwaysOnTop;
		private System.Windows.Forms.Button buttonTaskDown;
		private System.Windows.Forms.Button buttonTaskUp;
		private System.Windows.Forms.Button buttonPrevDate;
		private System.Windows.Forms.Button buttonNextDate;
		private System.Windows.Forms.Button buttonTimerOnOff;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.Button buttonWeeklyReport;
		private System.Windows.Forms.ToolTip toolTip;
		private BrightIdeasSoftware.OLVColumn olvColumn6;
	}
}

