using System;
using System.Windows.Forms;

namespace Echec {
    /// <summary>Formulaire de dialogue du choix de promotion d'un pion</summary>
    public partial class FormPromotion : Form {
        private string promotion;

        /// <summary>Crée un formulaire de choix de promotion</summary>
        public FormPromotion() {
            InitializeComponent();
        }

        /// <summary>Obtient le choix de promotion de l'utilisateur</summary>
        public string Promotion { get => promotion; }

        #region Gestionnaire d'événements

        /// <summary>Gestionnaire d'événement du boutton de promotion</summary>
        /// <param name="sender">Objet à l'origine de de l'événement</param>
        /// <param name="e">Paramètres de l'événement</param>
        private void btnChoix_Click(object sender, EventArgs e) {
            if (sender is Button btn) {
                promotion = btn.Text;
                DialogResult = DialogResult.Yes;
                Close();
            }
        }

        /// <summary>Gestionnaire d'événement de la fermeture du formulaire</summary>
        /// <param name="sender">Objet à l'origine de de l'événement</param>
        /// <param name="e">Paramètres de l'événement</param>
        private void FormPromotion_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing && promotion == null) {
                MessageBox.Show("Veuillez faire un choix de promotion du pion", "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
        }

        #endregion
    }
}
