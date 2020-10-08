namespace Echec {
    partial class FormPromotion {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPromotion));
            this.labelTitre = new System.Windows.Forms.Label();
            this.btnReine = new System.Windows.Forms.Button();
            this.btnCavalier = new System.Windows.Forms.Button();
            this.btnFou = new System.Windows.Forms.Button();
            this.btnTour = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitre
            // 
            this.labelTitre.AutoSize = true;
            this.labelTitre.Location = new System.Drawing.Point(13, 13);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(172, 13);
            this.labelTitre.TabIndex = 0;
            this.labelTitre.Text = "Sélectionnez la pièce à promouvoir\r\n";
            // 
            // btnReine
            // 
            this.btnReine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReine.Location = new System.Drawing.Point(12, 39);
            this.btnReine.Name = "btnReine";
            this.btnReine.Size = new System.Drawing.Size(190, 23);
            this.btnReine.TabIndex = 1;
            this.btnReine.Text = "Reine";
            this.btnReine.UseVisualStyleBackColor = true;
            this.btnReine.Click += new System.EventHandler(this.btnChoix_Click);
            // 
            // btnCavalier
            // 
            this.btnCavalier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCavalier.Location = new System.Drawing.Point(13, 69);
            this.btnCavalier.Name = "btnCavalier";
            this.btnCavalier.Size = new System.Drawing.Size(189, 23);
            this.btnCavalier.TabIndex = 2;
            this.btnCavalier.Text = "Cavalier";
            this.btnCavalier.UseVisualStyleBackColor = true;
            this.btnCavalier.Click += new System.EventHandler(this.btnChoix_Click);
            // 
            // btnFou
            // 
            this.btnFou.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFou.Location = new System.Drawing.Point(13, 99);
            this.btnFou.Name = "btnFou";
            this.btnFou.Size = new System.Drawing.Size(189, 23);
            this.btnFou.TabIndex = 3;
            this.btnFou.Text = "Fou";
            this.btnFou.UseVisualStyleBackColor = true;
            this.btnFou.Click += new System.EventHandler(this.btnChoix_Click);
            // 
            // btnTour
            // 
            this.btnTour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTour.Location = new System.Drawing.Point(13, 129);
            this.btnTour.Name = "btnTour";
            this.btnTour.Size = new System.Drawing.Size(189, 23);
            this.btnTour.TabIndex = 4;
            this.btnTour.Text = "Tour";
            this.btnTour.UseVisualStyleBackColor = true;
            this.btnTour.Click += new System.EventHandler(this.btnChoix_Click);
            // 
            // FormPromotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 160);
            this.Controls.Add(this.btnTour);
            this.Controls.Add(this.btnFou);
            this.Controls.Add(this.btnCavalier);
            this.Controls.Add(this.btnReine);
            this.Controls.Add(this.labelTitre);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(230, 199);
            this.Name = "FormPromotion";
            this.Text = "Promotion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPromotion_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Button btnReine;
        private System.Windows.Forms.Button btnCavalier;
        private System.Windows.Forms.Button btnFou;
        private System.Windows.Forms.Button btnTour;
    }
}