namespace ExifReader
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            pbFile = new PictureBox();
            propertyGrid1 = new PropertyGrid();
            panel2 = new Panel();
            lblDrag = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbFile).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(pbFile);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(16, 16);
            panel1.Name = "panel1";
            panel1.Size = new Size(378, 601);
            panel1.TabIndex = 0;
            // 
            // pbFile
            // 
            pbFile.BackColor = SystemColors.ControlDarkDark;
            pbFile.BorderStyle = BorderStyle.FixedSingle;
            pbFile.Dock = DockStyle.Fill;
            pbFile.Location = new Point(0, 0);
            pbFile.Name = "pbFile";
            pbFile.Size = new Size(378, 601);
            pbFile.TabIndex = 0;
            pbFile.TabStop = false;
            // 
            // propertyGrid1
            // 
            propertyGrid1.Dock = DockStyle.Fill;
            propertyGrid1.HelpVisible = false;
            propertyGrid1.Location = new Point(394, 116);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new Size(446, 501);
            propertyGrid1.TabIndex = 3;
            propertyGrid1.ToolbarVisible = false;
            propertyGrid1.Visible = false;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(lblDrag);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(394, 16);
            panel2.Name = "panel2";
            panel2.Size = new Size(446, 100);
            panel2.TabIndex = 2;
            // 
            // lblDrag
            // 
            lblDrag.AllowDrop = true;
            lblDrag.Dock = DockStyle.Fill;
            lblDrag.Location = new Point(0, 0);
            lblDrag.Name = "lblDrag";
            lblDrag.Size = new Size(444, 98);
            lblDrag.TabIndex = 0;
            lblDrag.Text = "Drag your PNG file here to read the EXIF data\r\nCurrent file: None";
            lblDrag.TextAlign = ContentAlignment.MiddleCenter;
            lblDrag.DragDrop += lblDrag_DragDrop;
            lblDrag.DragEnter += lblDrag_DragEnter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 633);
            Controls.Add(propertyGrid1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Padding = new Padding(16);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ComfyUI PNG EXIF Reader";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbFile).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pbFile;
        private PropertyGrid propertyGrid1;
        private Panel panel2;
        private Label lblDrag;
    }
}
