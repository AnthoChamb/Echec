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
        }

        /// <summary>Mets à jour l'affichage de l'<see cref="Echiquier"/> dans le formulaire</summary>
        /// <param name="echiquier">Représentation en chaine de l'<see cref="Echiquier"/></param>
        public void AfficherEchiquier(string echiquier) {
            throw new NotImplementedException();
        }

        /// <summary>Affiche le message précisé dans la zone de texte</summary>
        /// <param name="message">Message a afficher</param>
        public void AfficherMessage(string message) {
            throw new NotImplementedException();
        }
    }
}
