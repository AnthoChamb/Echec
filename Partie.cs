using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec {
    /// <summary>Classe d'une partie du jeu d'échec</summary>
    public class Partie {
        private readonly Dictionary<Couleur, Joueur> joueurs;
        private readonly Echec echec;
        private readonly FormPartie formEchec;
        private readonly Echiquier echiquier;
        private Couleur actif;
        private byte coups;
        private readonly List<string> posistions;

        /// <summary>Crée une partie d'échec</summary>
        /// <param name="echec">Jeu d'échec lié à cette partie</param>
        /// <param name="noir">Joueur avec les pièces noires</param>
        /// <param name="blanc">Joueur avec les pièces blanches</param>
        public Partie(Echec echec, Joueur noir, Joueur blanc) {
            joueurs = new Dictionary<Couleur, Joueur> {
                { Couleur.NOIR, noir },
                { Couleur.BLANC, blanc }
            };

            this.echec = echec;
            echiquier = new Echiquier();
            formEchec = new FormPartie(this);

            actif = Couleur.NOIR;
            coups = 0;
            posistions = new List<string>() { 
                echiquier.ToString() 
            };
        }

        /// <summary>Affiche <see cref="FormPartie"/></summary>
        public void Demarrer() => formEchec.ShowDialog();

        /// <summary>Joue le coup de la source à la destination, si possible</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne source</param>
        /// <param name="colSrc">Indice de la colonne de destination</param>
        public void JouerCoup(byte liSrc, byte liDest, byte colSrc, byte colDest) {
            if (CoupValide(liSrc, liDest, colSrc, colDest)) {
                echiquier.JouerCoup(liSrc, liDest, colSrc, colDest);
            }
        }

        /// <summary>Évalue si il est possible de jouer le coup de la source à la destination</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne source</param>
        /// <param name="colSrc">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le coup est possible</returns>
        private bool CoupValide(byte liSrc, byte liDest, byte colSrc, byte colDest) {
            throw new NotImplementedException();
        }

        /// <summary>Évalue si le joueur actif est en échec</summary>
        /// <returns>Retourne true si le joueur actif est en échec</returns>
        private bool Echec() {
            throw new NotImplementedException();
        }

        /// <summary>Évalue si le joueur actif est matté</summary>
        /// <returns>Retourne true si le joueur actif est matté</returns>
        private bool Mat() {
            throw new NotImplementedException();
        }

        /// <summary>Évalue si le joueur actif est en situation de pat</summary>
        /// <returns>Retourne true si le joueur actif est en situation de pat</returns>
        private bool Pat() {
            throw new NotImplementedException();
        }
    }
}
