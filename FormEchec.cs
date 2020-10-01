using System;
using System.Windows.Forms;

namespace Echec {
    /// <summary>Formulaire maître du jeu d'échec</summary>
    public partial class FormEchec : Form {
        private readonly Echec echec;

        /// <summary>Crée un formulaire d'échec</summary>
        /// <param name="echec">Controlleur <see cref="Echec"/> associé à ce formulaire</param>
        public FormEchec(Echec echec) {
            InitializeComponent();
            this.echec = echec;
        }

        /// <summary>Affiche les informations des joueurs dans la liste</summary>
        /// <param name="joueurs">Représentations en chaine des joueurs</param>
        public void AfficherJoueurs(string[] joueurs) {
            lsvJoueurs.Items.Clear();
            foreach (string joueur in joueurs)
                lsvJoueurs.Items.Add(joueur);
        }

        #region Événements

        /// <summary>Gestionnaire d'événement de la fermeture du formulaire</summary>
        /// <param name="sender">Objet à l'origine de de l'événement</param>
        /// <param name="e">Paramètres de l'événement</param>
        private void FormEchec_FormClosed(object sender, FormClosedEventArgs e) => echec.Fermer();

        /// <summary>Gestionnaire d'événement du changement de la sélection de la liste des joueurs</summary>
        /// <param name="sender">Objet à l'origine de de l'événement</param>
        /// <param name="e">Paramètres de l'événement</param>
        private void lsvJoueurs_SelectedIndexChanged(object sender, EventArgs e) {
            switch (lsvJoueurs.SelectedItems.Count) {
                case 1:
                    labStats.Text = echec[lsvJoueurs.SelectedIndices[0]];
                    break;
                case 2:
                    labStats.Text = echec.ResultatsPossibles(lsvJoueurs.SelectedIndices[0], lsvJoueurs.SelectedIndices[1]);
                    break;
                default:
                    labStats.Text = "Veuillez sélectionner un joueur\npour afficher ses statistiques.";
                    break;
            }

            btnJouer.Enabled = lsvJoueurs.SelectedItems.Count == 2;
        }

        /// <summary>Gestionnaire d'événement d'un clic du boutton jouer</summary>
        /// <param name="sender">Objet à l'origine de de l'événement</param>
        /// <param name="e">Paramètres de l'événement</param>
        private void btnJouer_Click(object sender, EventArgs e) => echec.LancerPartie(lsvJoueurs.SelectedIndices[0], lsvJoueurs.SelectedIndices[1]);

        #endregion
    }
}
