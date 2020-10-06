using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Echec {
    /// <summary>Formulaire d'une partie du jeu d'échec</summary>
    public partial class FormPartie : Form {
        private readonly Partie partie;
        private TableLayoutPanelCellPosition? source;

        /// <summary>Crée un formulaire d'une partie du jeu d'échec</summary>
        /// <param name="partie">Instance de <see cref="Partie"/> associé à ce formulaire</param>
        public FormPartie(Partie partie) {
            InitializeComponent();
            this.partie = partie;
            source = null;

            // Ajoute l'event à nos cases de l'échiquier
            foreach (Control ctrl in tableEchiquier.Controls)
                ctrl.Click += case_Click;
        }

        #region Méthodes publiques

        /// <summary>Mets à jour l'affichage de l'<see cref="Echiquier"/> dans le formulaire</summary>
        /// <param name="echiquier">Représentation en chaine de l'<see cref="Echiquier"/></param>
        public void AfficherEchiquier(string echiquier) {
            byte ctrl = 0;
            for (int piece = echiquier.Length - 1; piece >= 0; piece--)
                tableEchiquier.Controls[ctrl++].Text = echiquier[piece].ToString();
        }

        /// <summary>Affiche le message précisé dans la zone de texte</summary>
        /// <param name="message">Message a afficher</param>
        public void AfficherMessage(string message) => labelMessage.Text = message;

        #endregion

        #region Gestionnaire d'événements

        /// <summary>Gestionnaire d'événement d'un clic sur une case</summary>
        /// <param name="sender">Objet à l'origine de l'événement</param>
        /// <param name="e">Paramètres de l'événement</param>
        private void case_Click(object sender, EventArgs e) {
            TableLayoutPanelCellPosition clic = tableEchiquier.GetCellPosition(sender as Control);
            if (source == null) {
                if ((sender as Control).Text != " ") {
                    source = clic;
                    (sender as Control).ForeColor = Color.Blue;
                }
            } else {
                tableEchiquier.GetControlFromPosition(source.Value.Column, source.Value.Row).ForeColor = SystemColors.ControlText;
                partie.JouerCoup((byte)source.Value.Row, (byte)clic.Row, (byte)source.Value.Column, (byte)clic.Column);
                source = null;
            }
        }

        /// <summary>Gestionnaire d'événement d'un clic sur le boutton Capituler</summary>
        /// <param name="sender">Objet à l'origine de l'événement</param>
        /// <param name="e">Paramètres de l'événement</param>
        private void btnCapituler_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Souhaitez-vous vraiment capituler et mettre fin à la partie?", "Capitulation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                partie.Capituler();
        }

        #endregion
    }
}
