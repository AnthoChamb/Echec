using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }
    }
}
