namespace AppFactory.UI
{
    partial class frmSolution
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
            this.txtSolutionVersion = new System.Windows.Forms.TextBox();
            this.txtSolutionName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbPublisher = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSolutionDisplayName = new System.Windows.Forms.TextBox();
            this.btnNewPublisher = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSolutionVersion
            // 
            this.txtSolutionVersion.Location = new System.Drawing.Point(142, 95);
            this.txtSolutionVersion.Margin = new System.Windows.Forms.Padding(2);
            this.txtSolutionVersion.Name = "txtSolutionVersion";
            this.txtSolutionVersion.Size = new System.Drawing.Size(155, 20);
            this.txtSolutionVersion.TabIndex = 40;
            this.txtSolutionVersion.Text = "1.0.0.0";
            // 
            // txtSolutionName
            // 
            this.txtSolutionName.Location = new System.Drawing.Point(142, 72);
            this.txtSolutionName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSolutionName.Name = "txtSolutionName";
            this.txtSolutionName.Size = new System.Drawing.Size(155, 20);
            this.txtSolutionName.TabIndex = 39;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 95);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 38;
            this.label8.Text = "Version:";
            // 
            // cmbPublisher
            // 
            this.cmbPublisher.FormattingEnabled = true;
            this.cmbPublisher.Location = new System.Drawing.Point(88, 47);
            this.cmbPublisher.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPublisher.Name = "cmbPublisher";
            this.cmbPublisher.Size = new System.Drawing.Size(209, 21);
            this.cmbPublisher.TabIndex = 37;
            this.cmbPublisher.SelectedIndexChanged += new System.EventHandler(this.cmbPublisher_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 50);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Herausgeber:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 74);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Anzeigename:";
            // 
            // txtSolutionDisplayName
            // 
            this.txtSolutionDisplayName.Location = new System.Drawing.Point(88, 23);
            this.txtSolutionDisplayName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSolutionDisplayName.Name = "txtSolutionDisplayName";
            this.txtSolutionDisplayName.Size = new System.Drawing.Size(210, 20);
            this.txtSolutionDisplayName.TabIndex = 34;
            this.txtSolutionDisplayName.Leave += new System.EventHandler(this.txtSolutionDisplayName_Leave);
            // 
            // btnNewPublisher
            // 
            this.btnNewPublisher.Location = new System.Drawing.Point(311, 42);
            this.btnNewPublisher.Margin = new System.Windows.Forms.Padding(2);
            this.btnNewPublisher.Name = "btnNewPublisher";
            this.btnNewPublisher.Size = new System.Drawing.Size(55, 28);
            this.btnNewPublisher.TabIndex = 41;
            this.btnNewPublisher.Text = "Neu";
            this.btnNewPublisher.UseVisualStyleBackColor = true;
            this.btnNewPublisher.Click += new System.EventHandler(this.btnNewPublisher_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(295, 128);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 28);
            this.btnSave.TabIndex = 42;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(19, 128);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 28);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmSolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 165);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNewPublisher);
            this.Controls.Add(this.txtSolutionVersion);
            this.Controls.Add(this.txtSolutionName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbPublisher);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSolutionDisplayName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSolution";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Neue Lösung";
            this.Load += new System.EventHandler(this.frmSolution_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSolutionVersion;
        private System.Windows.Forms.TextBox txtSolutionName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbPublisher;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSolutionDisplayName;
        private System.Windows.Forms.Button btnNewPublisher;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}