
namespace RandomizzatoreClient
{
    partial class FrmMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetReal = new System.Windows.Forms.Button();
            this.picDadi = new System.Windows.Forms.PictureBox();
            this.lblRandom = new System.Windows.Forms.Label();
            this.txtRandom = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picDadi)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMax
            // 
            this.txtMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMax.Location = new System.Drawing.Point(20, 126);
            this.txtMax.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(201, 39);
            this.txtMax.TabIndex = 1;
            this.txtMax.Text = "6";
            // 
            // txtMin
            // 
            this.txtMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMin.Location = new System.Drawing.Point(20, 51);
            this.txtMin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(201, 39);
            this.txtMin.TabIndex = 2;
            this.txtMin.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Minimo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Massimo";
            // 
            // btnGetReal
            // 
            this.btnGetReal.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetReal.Location = new System.Drawing.Point(251, 19);
            this.btnGetReal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGetReal.Name = "btnGetReal";
            this.btnGetReal.Size = new System.Drawing.Size(201, 142);
            this.btnGetReal.TabIndex = 5;
            this.btnGetReal.Text = "Estrai numero REALE";
            this.btnGetReal.UseVisualStyleBackColor = true;
            this.btnGetReal.Click += new System.EventHandler(this.btnGetReal_Click);
            // 
            // picDadi
            // 
            this.picDadi.Image = global::RandomizzatoreClient.Properties.Resources.dadi_immagine_animata_0092;
            this.picDadi.Location = new System.Drawing.Point(251, 230);
            this.picDadi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picDadi.Name = "picDadi";
            this.picDadi.Size = new System.Drawing.Size(229, 170);
            this.picDadi.TabIndex = 6;
            this.picDadi.TabStop = false;
            this.picDadi.Visible = false;
            // 
            // lblRandom
            // 
            this.lblRandom.AutoSize = true;
            this.lblRandom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRandom.Location = new System.Drawing.Point(241, 286);
            this.lblRandom.Name = "lblRandom";
            this.lblRandom.Size = new System.Drawing.Size(217, 32);
            this.lblRandom.TabIndex = 8;
            this.lblRandom.Text = "Numero estratto";
            // 
            // txtRandom
            // 
            this.txtRandom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRandom.Location = new System.Drawing.Point(251, 318);
            this.txtRandom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRandom.Name = "txtRandom";
            this.txtRandom.Size = new System.Drawing.Size(201, 39);
            this.txtRandom.TabIndex = 7;
            this.txtRandom.Text = "1";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 429);
            this.Controls.Add(this.lblRandom);
            this.Controls.Add(this.txtRandom);
            this.Controls.Add(this.picDadi);
            this.Controls.Add(this.btnGetReal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMin);
            this.Controls.Add(this.txtMax);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmMain";
            this.Text = "Randomizzatore CLIENT";
            ((System.ComponentModel.ISupportInitialize)(this.picDadi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetReal;
        private System.Windows.Forms.PictureBox picDadi;
        private System.Windows.Forms.Label lblRandom;
        private System.Windows.Forms.TextBox txtRandom;
        private System.Windows.Forms.Timer timer1;
    }
}

