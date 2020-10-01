namespace Echec {
    partial class FormEchec {
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
            this.groupStats = new System.Windows.Forms.GroupBox();
            this.labStats = new System.Windows.Forms.Label();
            this.btnJouer = new System.Windows.Forms.Button();
            this.lsvJoueurs = new System.Windows.Forms.ListView();
            this.groupStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupStats
            // 
            this.groupStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupStats.Controls.Add(this.labStats);
            this.groupStats.Location = new System.Drawing.Point(139, 13);
            this.groupStats.Name = "groupStats";
            this.groupStats.Size = new System.Drawing.Size(162, 94);
            this.groupStats.TabIndex = 2;
            this.groupStats.TabStop = false;
            this.groupStats.Text = "Statistiques";
            // 
            // labStats
            // 
            this.labStats.AutoSize = true;
            this.labStats.Location = new System.Drawing.Point(7, 20);
            this.labStats.Name = "labStats";
            this.labStats.Size = new System.Drawing.Size(140, 26);
            this.labStats.TabIndex = 0;
            this.labStats.Text = "Veuillez choisir un joueur\r\npour afficher ses statistiques";
            // 
            // btnJouer
            // 
            this.btnJouer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJouer.Enabled = false;
            this.btnJouer.Location = new System.Drawing.Point(13, 114);
            this.btnJouer.Name = "btnJouer";
            this.btnJouer.Size = new System.Drawing.Size(288, 23);
            this.btnJouer.TabIndex = 3;
            this.btnJouer.Text = "Jouer";
            this.btnJouer.UseVisualStyleBackColor = true;
            this.btnJouer.Click += new System.EventHandler(this.btnJouer_Click);
            // 
            // lsvJoueurs
            // 
            this.lsvJoueurs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lsvJoueurs.HideSelection = false;
            this.lsvJoueurs.Location = new System.Drawing.Point(13, 13);
            this.lsvJoueurs.Name = "lsvJoueurs";
            this.lsvJoueurs.Size = new System.Drawing.Size(120, 94);
            this.lsvJoueurs.TabIndex = 4;
            this.lsvJoueurs.UseCompatibleStateImageBehavior = false;
            this.lsvJoueurs.View = System.Windows.Forms.View.List;
            this.lsvJoueurs.SelectedIndexChanged += new System.EventHandler(this.lsvJoueurs_SelectedIndexChanged);
            // 
            // FormEchec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 142);
            this.Controls.Add(this.lsvJoueurs);
            this.Controls.Add(this.btnJouer);
            this.Controls.Add(this.groupStats);
            this.MinimumSize = new System.Drawing.Size(334, 181);
            this.Name = "FormEchec";
            this.Text = "Menu principal";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormEchec_FormClosed);
            this.groupStats.ResumeLayout(false);
            this.groupStats.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupStats;
        private System.Windows.Forms.Label labStats;
        private System.Windows.Forms.Button btnJouer;
        private System.Windows.Forms.ListView lsvJoueurs;
    }
}