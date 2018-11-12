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
            this.labelActualPathToExeDescription = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textActualPathToExe
            // 
            this.textActualPathToExe.Location = new System.Drawing.Point(3, 35);
            this.textActualPathToExe.Name = "textActualPathToExe";
            this.textActualPathToExe.Size = new System.Drawing.Size(253, 20);
            this.textActualPathToExe.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(262, 33);
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
            this.labelActualPathToExe.Location = new System.Drawing.Point(3, 10);
            this.labelActualPathToExe.Name = "labelActualPathToExe";
            this.labelActualPathToExe.Size = new System.Drawing.Size(35, 13);
            this.labelActualPathToExe.TabIndex = 2;
            this.labelActualPathToExe.Text = "label1";
            // 
            // labelActualPathToExeDescription
            // 
            this.labelActualPathToExeDescription.AutoSize = true;
            this.labelActualPathToExeDescription.Location = new System.Drawing.Point(3, 68);
            this.labelActualPathToExeDescription.Name = "labelActualPathToExeDescription";
            this.labelActualPathToExeDescription.Size = new System.Drawing.Size(35, 13);
            this.labelActualPathToExeDescription.TabIndex = 3;
            this.labelActualPathToExeDescription.Text = "label1";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(320, 33);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(44, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // GeneralOptionsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelActualPathToExeDescription);
            this.Controls.Add(this.labelActualPathToExe);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.textActualPathToExe);
            this.Name = "GeneralOptionsUserControl";
            this.Size = new System.Drawing.Size(364, 107);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textActualPathToExe;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label labelActualPathToExe;
        private System.Windows.Forms.Label labelActualPathToExeDescription;
        private System.Windows.Forms.Button btnSave;
    }
}
