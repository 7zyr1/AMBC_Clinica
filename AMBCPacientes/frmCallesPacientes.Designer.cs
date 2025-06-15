namespace AMBCPacientes
{
    partial class frmCallesPacientes
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
            this.btnPacientes = new System.Windows.Forms.Button();
            this.btnCalles = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblABMC = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPacientes
            // 
            this.btnPacientes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPacientes.Location = new System.Drawing.Point(299, 144);
            this.btnPacientes.Name = "btnPacientes";
            this.btnPacientes.Size = new System.Drawing.Size(177, 57);
            this.btnPacientes.TabIndex = 0;
            this.btnPacientes.Text = "Pacientes";
            this.btnPacientes.UseVisualStyleBackColor = true;
            this.btnPacientes.Click += new System.EventHandler(this.btnPacientes_Click);
            // 
            // btnCalles
            // 
            this.btnCalles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCalles.Location = new System.Drawing.Point(299, 259);
            this.btnCalles.Name = "btnCalles";
            this.btnCalles.Size = new System.Drawing.Size(177, 57);
            this.btnCalles.TabIndex = 1;
            this.btnCalles.Text = "Calles";
            this.btnCalles.UseVisualStyleBackColor = true;
            this.btnCalles.Click += new System.EventHandler(this.btnCalles_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnCalles, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnPacientes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblABMC, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 45);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(776, 345);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblABMC
            // 
            this.lblABMC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblABMC.AutoSize = true;
            this.lblABMC.Location = new System.Drawing.Point(344, 49);
            this.lblABMC.Name = "lblABMC";
            this.lblABMC.Size = new System.Drawing.Size(88, 16);
            this.lblABMC.TabIndex = 2;
            this.lblABMC.Text = "ABMC Clinica";
            // 
            // frmCallesPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmCallesPacientes";
            this.Text = "AMBC Clinica";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPacientes;
        private System.Windows.Forms.Button btnCalles;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblABMC;
    }
}