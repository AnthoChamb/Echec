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
    /// <summary>Formulaire de dialogue du choix de promotion d'un pion</summary>
    public partial class FormPromotion : Form {
        private string promotion;

        /// <summary>Crée un formulaire de choix de promotion</summary>
        public FormPromotion() {
            InitializeComponent();
        }

        /// <summary>Obtient le choix de promotion de l'utilisateur</summary>
        public string Promotion { get => promotion; }

        private void btnChoix_Click(object sender, EventArgs e) {
            if (sender is Button btn) {
                promotion = btn.Text;
                DialogResult = DialogResult.Yes;
                Close();
            }
        }
    }
}
