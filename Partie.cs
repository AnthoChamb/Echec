using System;
using System.Collections.Generic;
using System.Linq;

namespace Echec {
    /// <summary>Classe d'une partie du jeu d'échec</summary>
    public class Partie {
        private readonly Dictionary<Couleur, Joueur> joueurs;
        private readonly Echec echec;
        private readonly FormPartie formPartie;
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
            formPartie = new FormPartie(this);
            formPartie.AfficherEchiquier(echiquier.ToString());

            actif = Couleur.NOIR;
            coups = 0;
            posistions = new List<string>() { 
                echiquier.ToString() 
            };
        }

        #region Déroulement de la parie

        /// <summary>Affiche <see cref="FormPartie"/></summary>
        public void Demarrer() => formPartie.ShowDialog();

        /// <summary>Capitulation du joueur actif</summary>
        public void Capituler() => Victoire((Couleur)((byte)Couleur.BLANC + (byte)Couleur.NOIR - (byte)actif));

        /// <summary>Effectue la fin de la parite par la victoire d'un joueur</summary>
        /// <param name="vainqueur">Couleur du joueur vainqueur</param>
        private void Victoire(Couleur vainqueur) {
            foreach (Joueur joueur in joueurs.Values)
                joueur.Jouees++;

            joueurs[vainqueur].Victoires++;

            // Ajuster pointage
            Couleur perdant = (Couleur)((byte)Couleur.BLANC + (byte)Couleur.NOIR - (byte)vainqueur);
            joueurs[vainqueur].Pointage = joueurs[vainqueur] + joueurs[perdant];
            joueurs[perdant].Pointage = joueurs[perdant] - joueurs[vainqueur];

            formPartie.Close();
        }

        #endregion

        #region Déroulement d'un coup

        /// <summary>Joue le coup de la source à la destination, si possible</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne source</param>
        /// <param name="colSrc">Indice de la colonne de destination</param>
        public void JouerCoup(byte liSrc, byte liDest, byte colSrc, byte colDest) {
            // Coup normal
            if (CoupValide(liSrc, liDest, colSrc, colDest)) {
                echiquier.JouerCoup(liSrc, liDest, colSrc, colDest);
                ApresCoup(liDest, colDest);

            // Roque
            } else if (liSrc == liDest && liSrc == (actif == Couleur.NOIR ? 0 : 7) && (colDest == 2 && echiquier.SiRoquable(actif, true) || colDest == 6 && echiquier.SiRoquable(actif, false))) {
                echiquier.JouerRoque(actif, colDest == 2);
                ApresCoup(liDest, colDest);

            // Manger en passant
            } else if (echiquier.EstPion(liSrc, colSrc) && echiquier.SiManger(liSrc, liDest, colSrc, colDest) && posistions.Last()[(liDest + (actif == Couleur.BLANC ? -1 : 1)) * 7 + colDest] != ' ' && echiquier.EstPion((byte)(liDest + (actif == Couleur.BLANC ? 1 : -1)), colDest)) {
                echiquier.JouerEnPassant(liSrc, liDest, colSrc, colDest);
                ApresCoup(liDest, colDest);
            }
        }

        /// <summary>Gère les action nécessaire après un coup</summary>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne de destination</param>
        private void ApresCoup(byte liDest, byte colDest) {
            string chaine = echiquier.ToString(); // Représentation en chaine de l'échiquier
            formPartie.AfficherEchiquier(echiquier.ToString());

            actif = (Couleur)((byte)Couleur.BLANC + (byte)Couleur.NOIR - (byte)actif); // Truc de Patrick pour inverser
            formPartie.AfficherMessage("C'est à " + joueurs[actif].Nom + " de jouer");

            // Règle des 50 coups
            if (echiquier.EstPion(liDest, colDest) || posistions.Last()[liDest * 7 + colDest] == ' ')
                coups = 0;
            else if (++coups == 50) {
                // TODO: Partie nulle
            }
            
            posistions.Add(chaine);
        }

        /// <summary>Évalue si il est possible de jouer le coup de la source à la destination</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne source</param>
        /// <param name="colSrc">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le coup est possible</returns>
        private bool CoupValide(byte liSrc, byte liDest, byte colSrc, byte colDest) {
            if (echiquier.EstVide(liSrc, colSrc)) {
                formPartie.AfficherMessage("Coup invalide. Veuillez sélectioner une pièce source");
                return false;
            } 

            if (echiquier.CouleurPiece(liSrc, colSrc) != actif) {
                formPartie.AfficherMessage("Coup invalide. La pièce source ne vous appartient pas");
                return false;
            }

            if (!echiquier.EstVide(liDest, colDest)) {
                if (echiquier.CouleurPiece(liDest, colDest) == actif) {
                    formPartie.AfficherMessage("Coup invalide. Impossible de manger sa propre pièce");
                    return false;
                } else if (!echiquier.SiManger(liSrc ,liDest, colSrc, colDest)) {
                    formPartie.AfficherMessage("Coup invalide. Cette pièce ne peut effectuer ce déplacement");
                    return false;
                }
            } else if (!echiquier.SiDeplacer(liSrc, liDest, colSrc, colDest)) {
                formPartie.AfficherMessage("Coup invalide. Cette pièce ne peut effectuer ce déplacement");
                return false;
            }

            if (!echiquier.EstFlottante(liSrc, colSrc) && !echiquier.CheminLibre(liSrc, liDest, colSrc, colDest)) {
                formPartie.AfficherMessage("Coup invalide. Une pièce se trouve dans le chemin de la pièce");
                return false;
            }

            return true;
        }

        #endregion

        #region Vérification d'échec

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

        #endregion
    }
}
