namespace Proyecto_final_2ndo_semestre_c_sharp
{
    partial class Form3
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
            this.pbScan = new System.Windows.Forms.PictureBox();
            this.btnScanne = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cboDevice = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbScan)).BeginInit();
            this.SuspendLayout();
            // 
            // pbScan
            // 
            this.pbScan.Location = new System.Drawing.Point(12, 38);
            this.pbScan.Name = "pbScan";
            this.pbScan.Size = new System.Drawing.Size(624, 390);
            this.pbScan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbScan.TabIndex = 0;
            this.pbScan.TabStop = false;
            // 
            // btnScanne
            // 
            this.btnScanne.Location = new System.Drawing.Point(655, 68);
            this.btnScanne.Name = "btnScanne";
            this.btnScanne.Size = new System.Drawing.Size(75, 23);
            this.btnScanne.TabIndex = 1;
            this.btnScanne.Text = "Scan";
            this.btnScanne.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(655, 278);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // cboDevice
            // 
            this.cboDevice.FormattingEnabled = true;
            this.cboDevice.Location = new System.Drawing.Point(107, 11);
            this.cboDevice.Name = "cboDevice";
            this.cboDevice.Size = new System.Drawing.Size(377, 21);
            this.cboDevice.TabIndex = 3;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cboDevice);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnScanne);
            this.Controls.Add(this.pbScan);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbScan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbScan;
        private System.Windows.Forms.Button btnScanne;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cboDevice;
    }
}