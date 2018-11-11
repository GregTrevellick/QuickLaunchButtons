namespace QuickLaunch.Fiddler.Options
{
    partial class GeneralOptionsUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textActualPathToExe = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.labelActualPathToExe = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textActualPathToExe
            // 
            this.textActualPathToExe.Location = new System.Drawing.Point(17, 36);
            this.textActualPathToExe.Name = "textActualPathToExe";
            this.textActualPathToExe.Size = new System.Drawing.Size(246, 20);
            this.textActualPathToExe.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(269, 34);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(52, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // labelActualPathToExe
            // 
            this.labelActualPathToExe.AutoSize = true;
            this.labelActualPathToExe.Location = new System.Drawing.Point(17, 17);
            this.labelActualPathToExe.Name = "labelActualPathToExe";
            this.labelActualPathToExe.Size = new System.Drawing.Size(35, 13);
            this.labelActualPathToExe.TabIndex = 2;
            this.labelActualPathToExe.Text = "label1";
            // 
            // GeneralOptionsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelActualPathToExe);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.textActualPathToExe);
            this.Name = "GeneralOptionsUserControl";
            this.Size = new System.Drawing.Size(404, 107);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textActualPathToExe;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label labelActualPathToExe;
    }
}
