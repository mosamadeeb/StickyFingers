namespace StickyFingers
{
    partial class MainForm
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
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.xfbin1Box = new System.Windows.Forms.TextBox();
            this.xfbin2Box = new System.Windows.Forms.TextBox();
            this.xfbin1Browse = new System.Windows.Forms.Button();
            this.xfbin1Label = new System.Windows.Forms.Label();
            this.xfbin2Label = new System.Windows.Forms.Label();
            this.xfbin2Browse = new System.Windows.Forms.Button();
            this.openXfbin1Dialog = new System.Windows.Forms.OpenFileDialog();
            this.openXfbin2Dialog = new System.Windows.Forms.OpenFileDialog();
            this.mesh1Box = new System.Windows.Forms.ListBox();
            this.mesh2Box = new System.Windows.Forms.ListBox();
            this.group1Label = new System.Windows.Forms.Label();
            this.group1CountLabel = new System.Windows.Forms.Label();
            this.group2CountLabel = new System.Windows.Forms.Label();
            this.group2Label = new System.Windows.Forms.Label();
            this.m1IndexLabel = new System.Windows.Forms.Label();
            this.m2IndexLabel = new System.Windows.Forms.Label();
            this.mesh1IndexLabel = new System.Windows.Forms.Label();
            this.mesh2IndexLabel = new System.Windows.Forms.Label();
            this.format1Label = new System.Windows.Forms.Label();
            this.format2Label = new System.Windows.Forms.Label();
            this.formatByte2Label = new System.Windows.Forms.Label();
            this.formatByte1Label = new System.Windows.Forms.Label();
            this.material1Label = new System.Windows.Forms.Label();
            this.material2Label = new System.Windows.Forms.Label();
            this.mat1Label = new System.Windows.Forms.Label();
            this.mat2Label = new System.Windows.Forms.Label();
            this.exportNud1 = new System.Windows.Forms.Button();
            this.exportNud2 = new System.Windows.Forms.Button();
            this.replaceButton = new System.Windows.Forms.Button();
            this.groupsLabel1 = new System.Windows.Forms.Label();
            this.groups1Label = new System.Windows.Forms.Label();
            this.groupsLabel2 = new System.Windows.Forms.Label();
            this.groups2Label = new System.Windows.Forms.Label();
            this.groupsBox = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupsBoxLabel = new System.Windows.Forms.Label();
            this.bones1Button = new System.Windows.Forms.Button();
            this.openBones1Dialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // xfbin1Box
            // 
            this.xfbin1Box.Location = new System.Drawing.Point(12, 48);
            this.xfbin1Box.Name = "xfbin1Box";
            this.xfbin1Box.Size = new System.Drawing.Size(579, 22);
            this.xfbin1Box.TabIndex = 0;
            // 
            // xfbin2Box
            // 
            this.xfbin2Box.Location = new System.Drawing.Point(12, 233);
            this.xfbin2Box.Name = "xfbin2Box";
            this.xfbin2Box.Size = new System.Drawing.Size(579, 22);
            this.xfbin2Box.TabIndex = 1;
            // 
            // xfbin1Browse
            // 
            this.xfbin1Browse.Location = new System.Drawing.Point(628, 39);
            this.xfbin1Browse.Name = "xfbin1Browse";
            this.xfbin1Browse.Size = new System.Drawing.Size(119, 40);
            this.xfbin1Browse.TabIndex = 2;
            this.xfbin1Browse.Text = "Browse";
            this.xfbin1Browse.UseVisualStyleBackColor = true;
            this.xfbin1Browse.Click += new System.EventHandler(this.Xfbin1Browse_Click);
            // 
            // xfbin1Label
            // 
            this.xfbin1Label.AutoSize = true;
            this.xfbin1Label.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.xfbin1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xfbin1Label.Location = new System.Drawing.Point(12, 9);
            this.xfbin1Label.Name = "xfbin1Label";
            this.xfbin1Label.Size = new System.Drawing.Size(158, 17);
            this.xfbin1Label.TabIndex = 4;
            this.xfbin1Label.Text = "Xfbin 1 (Main Model)";
            // 
            // xfbin2Label
            // 
            this.xfbin2Label.AutoSize = true;
            this.xfbin2Label.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.xfbin2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xfbin2Label.Location = new System.Drawing.Point(12, 194);
            this.xfbin2Label.Name = "xfbin2Label";
            this.xfbin2Label.Size = new System.Drawing.Size(232, 17);
            this.xfbin2Label.TabIndex = 5;
            this.xfbin2Label.Text = "Xfbin 2 / Nud file (Extra Model)";
            // 
            // xfbin2Browse
            // 
            this.xfbin2Browse.Location = new System.Drawing.Point(628, 224);
            this.xfbin2Browse.Name = "xfbin2Browse";
            this.xfbin2Browse.Size = new System.Drawing.Size(119, 40);
            this.xfbin2Browse.TabIndex = 6;
            this.xfbin2Browse.Text = "Browse";
            this.xfbin2Browse.UseVisualStyleBackColor = true;
            this.xfbin2Browse.Click += new System.EventHandler(this.Xfbin2Browse_Click);
            // 
            // openXfbin1Dialog
            // 
            this.openXfbin1Dialog.DefaultExt = "xfbin";
            this.openXfbin1Dialog.Filter = "XFBIN files|*.xfbin";
            this.openXfbin1Dialog.Title = "Open Main Xfbin";
            // 
            // openXfbin2Dialog
            // 
            this.openXfbin2Dialog.DefaultExt = "xfbin";
            this.openXfbin2Dialog.Filter = "XFBIN files, NUD files|*.xfbin;*.nud";
            this.openXfbin2Dialog.Title = "Open Extra Xfbin/Nud";
            // 
            // mesh1Box
            // 
            this.mesh1Box.FormattingEnabled = true;
            this.mesh1Box.ItemHeight = 16;
            this.mesh1Box.Location = new System.Drawing.Point(11, 79);
            this.mesh1Box.Margin = new System.Windows.Forms.Padding(2);
            this.mesh1Box.Name = "mesh1Box";
            this.mesh1Box.Size = new System.Drawing.Size(209, 100);
            this.mesh1Box.TabIndex = 23;
            this.mesh1Box.SelectedIndexChanged += new System.EventHandler(this.Mesh1Box_SelectedIndexChanged);
            // 
            // mesh2Box
            // 
            this.mesh2Box.FormattingEnabled = true;
            this.mesh2Box.ItemHeight = 16;
            this.mesh2Box.Location = new System.Drawing.Point(11, 263);
            this.mesh2Box.Margin = new System.Windows.Forms.Padding(2);
            this.mesh2Box.Name = "mesh2Box";
            this.mesh2Box.Size = new System.Drawing.Size(209, 100);
            this.mesh2Box.TabIndex = 24;
            this.mesh2Box.SelectedIndexChanged += new System.EventHandler(this.Mesh2Box_SelectedIndexChanged);
            // 
            // group1Label
            // 
            this.group1Label.AutoSize = true;
            this.group1Label.BackColor = System.Drawing.SystemColors.Control;
            this.group1Label.Location = new System.Drawing.Point(727, 122);
            this.group1Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.group1Label.Name = "group1Label";
            this.group1Label.Size = new System.Drawing.Size(20, 17);
            this.group1Label.TabIndex = 39;
            this.group1Label.Text = "   ";
            // 
            // group1CountLabel
            // 
            this.group1CountLabel.AutoSize = true;
            this.group1CountLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.group1CountLabel.Location = new System.Drawing.Point(630, 122);
            this.group1CountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.group1CountLabel.Name = "group1CountLabel";
            this.group1CountLabel.Size = new System.Drawing.Size(87, 17);
            this.group1CountLabel.TabIndex = 40;
            this.group1CountLabel.Text = "# of Groups:";
            // 
            // group2CountLabel
            // 
            this.group2CountLabel.AutoSize = true;
            this.group2CountLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.group2CountLabel.Location = new System.Drawing.Point(630, 307);
            this.group2CountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.group2CountLabel.Name = "group2CountLabel";
            this.group2CountLabel.Size = new System.Drawing.Size(87, 17);
            this.group2CountLabel.TabIndex = 41;
            this.group2CountLabel.Text = "# of Groups:";
            // 
            // group2Label
            // 
            this.group2Label.AutoSize = true;
            this.group2Label.BackColor = System.Drawing.SystemColors.Control;
            this.group2Label.Location = new System.Drawing.Point(727, 307);
            this.group2Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.group2Label.Name = "group2Label";
            this.group2Label.Size = new System.Drawing.Size(20, 17);
            this.group2Label.TabIndex = 42;
            this.group2Label.Text = "   ";
            // 
            // m1IndexLabel
            // 
            this.m1IndexLabel.AutoSize = true;
            this.m1IndexLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m1IndexLabel.Location = new System.Drawing.Point(630, 95);
            this.m1IndexLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m1IndexLabel.Name = "m1IndexLabel";
            this.m1IndexLabel.Size = new System.Drawing.Size(83, 17);
            this.m1IndexLabel.TabIndex = 43;
            this.m1IndexLabel.Text = "Mesh Index:";
            // 
            // m2IndexLabel
            // 
            this.m2IndexLabel.AutoSize = true;
            this.m2IndexLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m2IndexLabel.Location = new System.Drawing.Point(630, 279);
            this.m2IndexLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m2IndexLabel.Name = "m2IndexLabel";
            this.m2IndexLabel.Size = new System.Drawing.Size(83, 17);
            this.m2IndexLabel.TabIndex = 44;
            this.m2IndexLabel.Text = "Mesh Index:";
            // 
            // mesh1IndexLabel
            // 
            this.mesh1IndexLabel.AutoSize = true;
            this.mesh1IndexLabel.BackColor = System.Drawing.SystemColors.Control;
            this.mesh1IndexLabel.Location = new System.Drawing.Point(727, 95);
            this.mesh1IndexLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mesh1IndexLabel.Name = "mesh1IndexLabel";
            this.mesh1IndexLabel.Size = new System.Drawing.Size(20, 17);
            this.mesh1IndexLabel.TabIndex = 45;
            this.mesh1IndexLabel.Text = "   ";
            // 
            // mesh2IndexLabel
            // 
            this.mesh2IndexLabel.AutoSize = true;
            this.mesh2IndexLabel.BackColor = System.Drawing.SystemColors.Control;
            this.mesh2IndexLabel.Location = new System.Drawing.Point(727, 279);
            this.mesh2IndexLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mesh2IndexLabel.Name = "mesh2IndexLabel";
            this.mesh2IndexLabel.Size = new System.Drawing.Size(20, 17);
            this.mesh2IndexLabel.TabIndex = 46;
            this.mesh2IndexLabel.Text = "   ";
            // 
            // format1Label
            // 
            this.format1Label.AutoSize = true;
            this.format1Label.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.format1Label.Location = new System.Drawing.Point(630, 199);
            this.format1Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.format1Label.Name = "format1Label";
            this.format1Label.Size = new System.Drawing.Size(56, 17);
            this.format1Label.TabIndex = 47;
            this.format1Label.Text = "Format:";
            // 
            // format2Label
            // 
            this.format2Label.AutoSize = true;
            this.format2Label.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.format2Label.Location = new System.Drawing.Point(630, 385);
            this.format2Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.format2Label.Name = "format2Label";
            this.format2Label.Size = new System.Drawing.Size(56, 17);
            this.format2Label.TabIndex = 48;
            this.format2Label.Text = "Format:";
            // 
            // formatByte2Label
            // 
            this.formatByte2Label.AutoSize = true;
            this.formatByte2Label.BackColor = System.Drawing.SystemColors.Control;
            this.formatByte2Label.Location = new System.Drawing.Point(697, 385);
            this.formatByte2Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.formatByte2Label.Name = "formatByte2Label";
            this.formatByte2Label.Size = new System.Drawing.Size(20, 17);
            this.formatByte2Label.TabIndex = 49;
            this.formatByte2Label.Text = "   ";
            // 
            // formatByte1Label
            // 
            this.formatByte1Label.AutoSize = true;
            this.formatByte1Label.BackColor = System.Drawing.SystemColors.Control;
            this.formatByte1Label.Location = new System.Drawing.Point(697, 199);
            this.formatByte1Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.formatByte1Label.Name = "formatByte1Label";
            this.formatByte1Label.Size = new System.Drawing.Size(20, 17);
            this.formatByte1Label.TabIndex = 50;
            this.formatByte1Label.Text = "   ";
            // 
            // material1Label
            // 
            this.material1Label.AutoSize = true;
            this.material1Label.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.material1Label.Location = new System.Drawing.Point(630, 173);
            this.material1Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.material1Label.Name = "material1Label";
            this.material1Label.Size = new System.Drawing.Size(62, 17);
            this.material1Label.TabIndex = 51;
            this.material1Label.Text = "Material:";
            // 
            // material2Label
            // 
            this.material2Label.AutoSize = true;
            this.material2Label.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.material2Label.Location = new System.Drawing.Point(630, 359);
            this.material2Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.material2Label.Name = "material2Label";
            this.material2Label.Size = new System.Drawing.Size(62, 17);
            this.material2Label.TabIndex = 52;
            this.material2Label.Text = "Material:";
            // 
            // mat1Label
            // 
            this.mat1Label.AutoSize = true;
            this.mat1Label.BackColor = System.Drawing.SystemColors.Control;
            this.mat1Label.Location = new System.Drawing.Point(697, 173);
            this.mat1Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mat1Label.Name = "mat1Label";
            this.mat1Label.Size = new System.Drawing.Size(20, 17);
            this.mat1Label.TabIndex = 53;
            this.mat1Label.Text = "   ";
            // 
            // mat2Label
            // 
            this.mat2Label.AutoSize = true;
            this.mat2Label.BackColor = System.Drawing.SystemColors.Control;
            this.mat2Label.Location = new System.Drawing.Point(697, 359);
            this.mat2Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mat2Label.Name = "mat2Label";
            this.mat2Label.Size = new System.Drawing.Size(20, 17);
            this.mat2Label.TabIndex = 54;
            this.mat2Label.Text = "   ";
            // 
            // exportNud1
            // 
            this.exportNud1.Enabled = false;
            this.exportNud1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.542858F);
            this.exportNud1.Location = new System.Drawing.Point(499, 88);
            this.exportNud1.Margin = new System.Windows.Forms.Padding(2);
            this.exportNud1.Name = "exportNud1";
            this.exportNud1.Size = new System.Drawing.Size(92, 31);
            this.exportNud1.TabIndex = 55;
            this.exportNud1.Text = "Export .nud";
            this.exportNud1.UseVisualStyleBackColor = true;
            this.exportNud1.Click += new System.EventHandler(this.ExportNud1_Click);
            // 
            // exportNud2
            // 
            this.exportNud2.Enabled = false;
            this.exportNud2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.542858F);
            this.exportNud2.Location = new System.Drawing.Point(499, 272);
            this.exportNud2.Margin = new System.Windows.Forms.Padding(2);
            this.exportNud2.Name = "exportNud2";
            this.exportNud2.Size = new System.Drawing.Size(92, 31);
            this.exportNud2.TabIndex = 56;
            this.exportNud2.Text = "Export .nud";
            this.exportNud2.UseVisualStyleBackColor = true;
            this.exportNud2.Click += new System.EventHandler(this.ExportNud2_Click);
            // 
            // replaceButton
            // 
            this.replaceButton.Enabled = false;
            this.replaceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.542858F);
            this.replaceButton.Location = new System.Drawing.Point(252, 394);
            this.replaceButton.Margin = new System.Windows.Forms.Padding(2);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(117, 45);
            this.replaceButton.TabIndex = 57;
            this.replaceButton.Text = "Replace selected mesh";
            this.replaceButton.UseVisualStyleBackColor = true;
            this.replaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // groupsLabel1
            // 
            this.groupsLabel1.AutoSize = true;
            this.groupsLabel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupsLabel1.Location = new System.Drawing.Point(630, 148);
            this.groupsLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.groupsLabel1.Name = "groupsLabel1";
            this.groupsLabel1.Size = new System.Drawing.Size(59, 17);
            this.groupsLabel1.TabIndex = 59;
            this.groupsLabel1.Text = "Groups:";
            // 
            // groups1Label
            // 
            this.groups1Label.AutoSize = true;
            this.groups1Label.BackColor = System.Drawing.SystemColors.Control;
            this.groups1Label.Location = new System.Drawing.Point(693, 148);
            this.groups1Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.groups1Label.Name = "groups1Label";
            this.groups1Label.Size = new System.Drawing.Size(20, 17);
            this.groups1Label.TabIndex = 58;
            this.groups1Label.Text = "   ";
            this.groups1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupsLabel2
            // 
            this.groupsLabel2.AutoSize = true;
            this.groupsLabel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupsLabel2.Location = new System.Drawing.Point(630, 332);
            this.groupsLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.groupsLabel2.Name = "groupsLabel2";
            this.groupsLabel2.Size = new System.Drawing.Size(59, 17);
            this.groupsLabel2.TabIndex = 61;
            this.groupsLabel2.Text = "Groups:";
            // 
            // groups2Label
            // 
            this.groups2Label.AutoSize = true;
            this.groups2Label.BackColor = System.Drawing.SystemColors.Control;
            this.groups2Label.Location = new System.Drawing.Point(693, 332);
            this.groups2Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.groups2Label.Name = "groups2Label";
            this.groups2Label.Size = new System.Drawing.Size(20, 17);
            this.groups2Label.TabIndex = 60;
            this.groups2Label.Text = "   ";
            this.groups2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupsBox
            // 
            this.groupsBox.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.groupsBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.groupsBox.HideSelection = false;
            this.groupsBox.Location = new System.Drawing.Point(326, 76);
            this.groupsBox.Name = "groupsBox";
            this.groupsBox.Size = new System.Drawing.Size(153, 135);
            this.groupsBox.TabIndex = 62;
            this.groupsBox.UseCompatibleStateImageBehavior = false;
            this.groupsBox.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Byte";
            this.columnHeader2.Width = 44;
            // 
            // groupsBoxLabel
            // 
            this.groupsBoxLabel.AutoSize = true;
            this.groupsBoxLabel.Location = new System.Drawing.Point(225, 79);
            this.groupsBoxLabel.Name = "groupsBoxLabel";
            this.groupsBoxLabel.Size = new System.Drawing.Size(95, 17);
            this.groupsBoxLabel.TabIndex = 63;
            this.groupsBoxLabel.Text = "Xfbin Groups:";
            // 
            // bones1Button
            // 
            this.bones1Button.Enabled = false;
            this.bones1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.542858F);
            this.bones1Button.Location = new System.Drawing.Point(499, 134);
            this.bones1Button.Margin = new System.Windows.Forms.Padding(2);
            this.bones1Button.Name = "bones1Button";
            this.bones1Button.Size = new System.Drawing.Size(92, 31);
            this.bones1Button.TabIndex = 64;
            this.bones1Button.Text = "Load bones";
            this.bones1Button.UseVisualStyleBackColor = true;
            this.bones1Button.Click += new System.EventHandler(this.bones1Button_Click);
            // 
            // openBones1Dialog
            // 
            this.openBones1Dialog.DefaultExt = "xfbin";
            this.openBones1Dialog.Filter = "XFBIN files|*.xfbin";
            this.openBones1Dialog.Title = "Open \"mot1\" Animations File";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(820, 450);
            this.Controls.Add(this.bones1Button);
            this.Controls.Add(this.groupsBoxLabel);
            this.Controls.Add(this.groupsBox);
            this.Controls.Add(this.groupsLabel2);
            this.Controls.Add(this.groups2Label);
            this.Controls.Add(this.groupsLabel1);
            this.Controls.Add(this.groups1Label);
            this.Controls.Add(this.replaceButton);
            this.Controls.Add(this.exportNud2);
            this.Controls.Add(this.exportNud1);
            this.Controls.Add(this.mat2Label);
            this.Controls.Add(this.mat1Label);
            this.Controls.Add(this.material2Label);
            this.Controls.Add(this.material1Label);
            this.Controls.Add(this.formatByte1Label);
            this.Controls.Add(this.formatByte2Label);
            this.Controls.Add(this.format2Label);
            this.Controls.Add(this.format1Label);
            this.Controls.Add(this.mesh2IndexLabel);
            this.Controls.Add(this.mesh1IndexLabel);
            this.Controls.Add(this.m2IndexLabel);
            this.Controls.Add(this.m1IndexLabel);
            this.Controls.Add(this.group2Label);
            this.Controls.Add(this.group2CountLabel);
            this.Controls.Add(this.group1CountLabel);
            this.Controls.Add(this.group1Label);
            this.Controls.Add(this.mesh2Box);
            this.Controls.Add(this.mesh1Box);
            this.Controls.Add(this.xfbin2Browse);
            this.Controls.Add(this.xfbin2Label);
            this.Controls.Add(this.xfbin1Label);
            this.Controls.Add(this.xfbin1Browse);
            this.Controls.Add(this.xfbin2Box);
            this.Controls.Add(this.xfbin1Box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Sticky Fingers - Mesh Replacer for JoJo ASB/EoH";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox xfbin1Box;
        private System.Windows.Forms.TextBox xfbin2Box;
        private System.Windows.Forms.Button xfbin1Browse;
        private System.Windows.Forms.Label xfbin1Label;
        private System.Windows.Forms.Label xfbin2Label;
        private System.Windows.Forms.Button xfbin2Browse;
        private System.Windows.Forms.OpenFileDialog openXfbin1Dialog;
        private System.Windows.Forms.OpenFileDialog openXfbin2Dialog;
        private System.Windows.Forms.ListBox mesh1Box;
        private System.Windows.Forms.ListBox mesh2Box;
        private System.Windows.Forms.Label group1Label;
        private System.Windows.Forms.Label group1CountLabel;
        private System.Windows.Forms.Label group2CountLabel;
        private System.Windows.Forms.Label group2Label;
        private System.Windows.Forms.Label m1IndexLabel;
        private System.Windows.Forms.Label m2IndexLabel;
        private System.Windows.Forms.Label mesh1IndexLabel;
        private System.Windows.Forms.Label mesh2IndexLabel;
        private System.Windows.Forms.Label format1Label;
        private System.Windows.Forms.Label format2Label;
        private System.Windows.Forms.Label formatByte2Label;
        private System.Windows.Forms.Label formatByte1Label;
        private System.Windows.Forms.Label material1Label;
        private System.Windows.Forms.Label material2Label;
        private System.Windows.Forms.Label mat1Label;
        private System.Windows.Forms.Label mat2Label;
        private System.Windows.Forms.Button exportNud1;
        private System.Windows.Forms.Button exportNud2;
        private System.Windows.Forms.Button replaceButton;
        private System.Windows.Forms.Label groupsLabel1;
        private System.Windows.Forms.Label groups1Label;
        private System.Windows.Forms.Label groupsLabel2;
        private System.Windows.Forms.Label groups2Label;
        private System.Windows.Forms.ListView groupsBox;
        private System.Windows.Forms.Label groupsBoxLabel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button bones1Button;
        private System.Windows.Forms.OpenFileDialog openBones1Dialog;
    }
}

