using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Echec {
    /// <summary>Classe d'une partie du jeu d'échec</summary>
    public class Partie {
        private readonly Dictionary<Couleur, Joueur> joueurs;
        private readonly FormPartie formPartie;
        private readonly Echiquier echiquier;
        private Couleur actif;
        private byte coups;
        private readonly List<string> posistions;

        /// <summary>Crée une partie d'échec</summary>
        /// <param name="noir">Joueur avec les pièces noires</param>
        /// <param name="blanc">Joueur avec les pièces blanches</param>
        public Partie(Joueur noir, Joueur blanc) {
            joueurs = new Dictionary<Couleur, Joueur> {
                { Couleur.NOIR, noir },
                { Couleur.BLANC, blanc }
            };

            echiquier = new Echiquier();
            echiquier.InitialisationPieces();
            formPartie = new FormPartie(this);

            actif = Couleur.NOIR;
            coups = 0;
            posistions = new List<string>() { 
                echiquier.ToString() 
            };
        }

        #region Déroulement de la parie

        /// <summary>Affiche <see cref="FormPartie"/></summary>
        public void Demarrer() {
            formPartie.AfficherEchiquier(echiquier.ToString());
            formPartie.Message = "C'est à " + joueurs[actif].Nom + " de jouer";
            formPartie.ShowDialog();
        }

        /// <summary>Capitulation du joueur actif</summary>
        public void Capituler() {
            formPartie.Message = "Abandon de " + joueurs[actif].Nom;
            Victoire((Couleur)((byte)Couleur.BLANC + (byte)Couleur.NOIR - (byte)actif));
        }

        /// <summary>Effectue la fin de la parite par la victoire d'un joueur</summary>
        /// <param name="vainqueur">Couleur du joueur vainqueur</param>
        private void Victoire(Couleur vainqueur) {
            foreach (Joueur joueur in joueurs.Values)
                joueur.Jouees++;

            joueurs[vainqueur].Victoires++;

            Couleur perdant = (Couleur)((byte)Couleur.BLANC + (byte)Couleur.NOIR - (byte)vainqueur);

            FormPartie.AfficherBoiteDialogue("Victoire de " + joueurs[vainqueur].Nom + "\n"
                + joueurs[vainqueur] + " -> " + (joueurs[vainqueur] + joueurs[perdant]) + "\n"
                + joueurs[perdant] + " -> " + (joueurs[perdant] - joueurs[vainqueur]) + "\nRetour au menu principal.", "Victoire");

            // Ajuster pointage
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

                // Promotion d'un pion
                if (echiquier.EstPion(liDest, colDest) && liDest == (actif == Couleur.BLANC ? 0 : 7)) {
                    FormPromotion promotion = new FormPromotion();
                    if (promotion.ShowDialog() == DialogResult.Yes)
                        echiquier.Promotion(liDest, colDest, promotion.Promotion);
                }

                ApresCoup(liDest, colDest);

            // Roque
            } else if (liSrc == liDest && liSrc == (actif == Couleur.NOIR ? 0 : 7) && (colDest == 2 && echiquier.SiRoquable(actif, true) || colDest == 6 && echiquier.SiRoquable(actif, false))) {
                Echiquier clone = (Echiquier)echiquier.Clone();
                clone.JouerRoque(actif, colDest == 2);

                if (Menaces(clone.PositionRoi(actif), clone).Count > 0)
                    formPartie.Message = "Coup illégal. Votre roi se retrouverait en échec";
                else {
                    echiquier.JouerRoque(actif, colDest == 2);
                    ApresCoup(liDest, colDest);
                }

            // Manger en passant
            } else if (liDest == (actif == Couleur.BLANC ? 2 : 5) && echiquier.EstPion(liSrc, colSrc) && echiquier.SiManger(liSrc, liDest, colSrc, colDest) && posistions.Last()[(actif == Couleur.BLANC ? 1 : 6) * 7 + colDest] != ' ' && echiquier.EstPion((byte)(actif == Couleur.BLANC ? 3 : 4), colDest)) {
                Echiquier clone = (Echiquier)echiquier.Clone();
                clone.JouerEnPassant(liSrc, liDest, colSrc, colDest);

                if (Menaces(clone.PositionRoi(actif), clone).Count > 0)
                    formPartie.Message = "Coup illégal. Votre roi se retrouverait en échec";
                else {
                    echiquier.JouerEnPassant(liSrc, liDest, colSrc, colDest);
                    ApresCoup(liDest, colDest);
                }
            }
        }

        /// <summary>Gère les action nécessaire après un coup</summary>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne de destination</param>
        private void ApresCoup(byte liDest, byte colDest) {
            string chaine = echiquier.ToString(); // Représentation en chaine de l'échiquier
            formPartie.AfficherEchiquier(echiquier.ToString());

            actif = (Couleur)((byte)Couleur.BLANC + (byte)Couleur.NOIR - (byte)actif); // Truc de Patrick pour inverser
            formPartie.Message = "C'est à " + joueurs[actif].Nom + " de jouer";

            // Règle des 50 coups
            if (echiquier.EstPion(liDest, colDest) || posistions.Last()[liDest * 8 + colDest] != ' ')
                coups = 0;
            else if (++coups == 50) {
                FormPartie.AfficherBoiteDialogue("Partie nulle car les 50 derniers coups consécutifs ont été joués par chacun des joueurs sans le mouvement d'aucun pion et sans aucune prise de pièce. Retour au menu principal.", "Partie nulle");
                formPartie.Close();
            }
            
            posistions.Add(chaine);

            // Troisième répétition d'une même position
            if (posistions.GroupBy(position => position).Where(group => group.Count() == 3).Count() > 0) {
                FormPartie.AfficherBoiteDialogue("Partie nulle car la même position a été répétée trois fois. Retour au menu principal.", "Partie nulle");
                formPartie.Close();
            }

            // Échec et mat
            List<(byte, byte)> menaces = Menaces(echiquier.PositionRoi(actif), echiquier);
            if (menaces.Count > 0) {
                if (Mat(menaces)) {
                    formPartie.Message = "Échec et mat";
                    Victoire((Couleur)((byte)Couleur.BLANC + (byte)Couleur.NOIR - (byte)actif));
                } else
                    formPartie.Message = "Échec. " + formPartie.Message;

            // Pat
            } else if (Pat()) {
                formPartie.Message = "Pat. " + formPartie.Message;
                FormPartie.AfficherBoiteDialogue("Partie nulle car le joueur au trait est en situation de pat. Retour au menu principal.", "Partie nulle");
                formPartie.Close();
            }

        }

        /// <summary>Évalue si il est possible de jouer le coup de la source à la destination et avise l'utilisateur des erreurs.</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne source</param>
        /// <param name="colSrc">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le coup est possible</returns>
        private bool CoupValide(byte liSrc, byte liDest, byte colSrc, byte colDest) {
            if (echiquier.EstVide(liSrc, colSrc)) {
                formPartie.Message = "Coup invalide. Veuillez sélectioner une pièce source";
                return false;
            } 

            if (echiquier.CouleurPiece(liSrc, colSrc) != actif) {
                formPartie.Message = "Coup invalide. La pièce source ne vous appartient pas";
                return false;
            }

            if (!echiquier.EstVide(liDest, colDest)) {
                if (echiquier.CouleurPiece(liDest, colDest) == actif) {
                    formPartie.Message = "Coup invalide. Impossible de manger sa propre pièce";
                    return false;
                } else if (!echiquier.SiManger(liSrc ,liDest, colSrc, colDest)) {
                    formPartie.Message = "Coup invalide. Cette pièce ne peut effectuer ce déplacement";
                    return false;
                }
            } else if (!echiquier.SiDeplacer(liSrc, liDest, colSrc, colDest)) {
                formPartie.Message = "Coup invalide. Cette pièce ne peut effectuer ce déplacement";
                return false;
            }

            if (!echiquier.EstFlottante(liSrc, colSrc) && !echiquier.CheminLibre(liSrc, liDest, colSrc, colDest)) {
                formPartie.Message = "Coup invalide. Une pièce se trouve dans le chemin de la pièce";
                return false;
            }

            Echiquier clone = (Echiquier)echiquier.Clone();
            clone.JouerCoup(liSrc, liDest, colSrc, colDest);

            if (Menaces(clone.PositionRoi(actif), clone).Count > 0) {
                formPartie.Message = "Coup illégal. Votre roi se retrouverait en échec";
                return false;
            }

            return true;
        }

        #endregion

        #region Vérifications d'échec

        /// <summary>Obtient une liste des pièces menaçant le roi du joueur actif se situant à l'emplacement précisé</summary>
        /// <param name="roi">Couple de valeur indiquant l'emplacement du roi</param>
        /// <param name="echiq"><see cref="Echiquier"/> à évalué</param>
        /// <returns>Retourne une liste des pièces menaçcant le roi se situant èa l'emplacement précisé</returns>
        private List<(byte ligne, byte col)> Menaces((byte ligne, byte col) roi, Echiquier echiq) {
            List<(byte ligne, byte col)> menaces = new List<(byte ligne, byte col)>(); 

            for (byte ligne = 0; ligne < 8; ligne++)
                for (byte col = 0; col < 8; col++)
                    if (!echiq.EstRoi(ligne, col) && !echiq.EstVide(ligne, col) && echiq.CouleurPiece(ligne, col) != actif && echiq.SiManger(ligne, roi.ligne, col, roi.col) && (echiq.EstFlottante(ligne, col) || echiq.CheminLibre(ligne, roi.ligne, col, roi.col)))
                        menaces.Add((ligne, col));

            return menaces;
        }

        /// <summary>Évalue si le joueur actif est matté</summary>
        /// <param name="menaces">Liste des pièces menaçant le roi du joueur actif</param>
        /// <returns>Retourne true si le joueur actif est matté</returns>
        private bool Mat(List<(byte ligne, byte col)> menaces) {
            (byte ligne, byte col) roi = echiquier.PositionRoi(actif);
            
            if (menaces.Count == 1) {
                // Tente de capturer la pièce menaçante
                for (byte ligne = 0; ligne < 8; ligne++)
                    for (byte col = 0; col < 8; col++)
                        if (PossedePiece(ligne, col) && echiquier.SiManger(ligne, menaces[0].ligne, col, menaces[0].col) && (echiquier.EstFlottante(ligne, col) || echiquier.CheminLibre(ligne, menaces[0].ligne, col, menaces[0].col)))
                            return false;

                // Tente d'interposer une pièce entre le roi et la pièce menaçante
                if (!echiquier.EstFlottante(menaces[0].ligne, menaces[0].col)) {
                    if (menaces[0].ligne == roi.ligne) {
                        // Déplacement horizontal
                        sbyte increment = (sbyte)(menaces[0].col > roi.col ? -1 : 1);
                        for (byte col = (byte)(menaces[0].col + increment); col != roi.col; col = (byte)(col + increment))
                            if (Interposer(menaces[0].ligne, col))
                                return false;

                    } else if (menaces[0].col == roi.col) {
                        // Déplacement vertical
                        sbyte increment = (sbyte)(menaces[0].ligne > roi.ligne ? -1 : 1);
                        for (byte ligne = (byte)(menaces[0].ligne + increment); ligne != roi.ligne; ligne = (byte)(ligne + increment))
                            if (Interposer(ligne, menaces[0].col))
                                return false;

                    } else {
                        // Déplacement diagonal
                        sbyte incrementLi = (sbyte)(menaces[0].ligne > roi.ligne ? -1 : 1);
                        sbyte incrementCol = (sbyte)(menaces[0].col > roi.col ? -1 : 1);
                        byte col = (byte)(menaces[0].col + incrementCol);
                        for (byte ligne = (byte)(menaces[0].ligne + incrementLi); ligne != roi.ligne && col != roi.col; ligne = (byte)(ligne + incrementLi))
                            if (Interposer(ligne, col))
                                return false;
                            else
                                col = (byte)(col + incrementCol);
                    }
                }
            }

            // Tente de bouger le roi sans se mettre en échec
            if (BougerRoi(roi, 1, 0) || BougerRoi(roi, -1, 0) || BougerRoi(roi, 0, 1) || BougerRoi(roi, 0, -1) || BougerRoi(roi, 1, -1) || BougerRoi(roi, 1, 1) || BougerRoi(roi, -1, 1) || BougerRoi(roi, -1, -1))
                return false;

            return true;
        }

        /// <summary>Évalue si le joueur actif est en situation de pat</summary>
        /// <returns>Retourne true si le joueur actif est en situation de pat</returns>
        private bool Pat() {
            for (byte liSrc = 0; liSrc < 8; liSrc++)
                for (byte colSrc = 0; colSrc < 8; colSrc++)
                    if (PossedePiece(liSrc, colSrc))
                        for (byte liDest = 0; liDest < 8; liDest++)
                            for (byte colDest = 0; colDest < 8; colDest++)
                                if (echiquier.EstVide(liDest, colDest) ?
                                    echiquier.SiDeplacer(liSrc, liDest, colSrc, colDest) :
                                    echiquier.CouleurPiece(liDest, colDest) != actif && echiquier.SiManger(liSrc, liDest, colSrc, colDest) &&
                                    echiquier.EstFlottante(liSrc, colSrc) || echiquier.CheminLibre(liSrc, liDest, colSrc, colDest)) {

                                    Echiquier clone = (Echiquier)echiquier.Clone();
                                    clone.JouerCoup(liSrc, liDest, colSrc, colDest);

                                    if (Menaces(clone.PositionRoi(actif), clone).Count == 0)
                                        return false;
                                }
            return true;
        }

        #endregion

        #region Vérifications divers

        /// <summary>Évalue si la ligne et colonne précisée contient une pièce possédée par le joueur actif.</summary>
        /// <param name="ligne">Indice de la ligne</param>
        /// <param name="col">Indice de la colonne</param>
        /// <returns>Retourne true si le joueur possède une pièce à cet emplacement</returns>
        private bool PossedePiece(byte ligne, byte col) => !echiquier.EstVide(ligne, col) && echiquier.CouleurPiece(ligne, col) == actif;

        /// <summary>Évalue si il est possible de déplacer le roi du joueur actif sans se mettre en échec</summary>
        /// <param name="roi">Couple de valeur indiquant l'emplacement du roi</param>
        /// <param name="ligne">Modificateur de ligne de l'emplacement du roi</param>
        /// <param name="col">Modificateur de colonne de l'emplacement du roi</param>
        /// <returns>Retourne true si il est possible de déplacer le roi du joueur actif sans se mettre en échec</returns>
        private bool BougerRoi((byte ligne, byte col) roi, sbyte ligne, sbyte col) => roi.ligne + ligne >= 0 && roi.ligne + ligne < 8 && roi.col + col >= 0 && roi.col + col < 8 && (echiquier.EstVide((byte)(roi.ligne + ligne), (byte)(roi.col + col)) || echiquier.CouleurPiece((byte)(roi.ligne + ligne), (byte)(roi.col + col)) != actif) && Menaces(((byte)(roi.ligne + ligne), (byte)(roi.col + col)), echiquier).Count == 0;

        /// <summary>Évalue si le joueur actif possède une pièce pouvant s'interposer à la ligne et colonne précisée</summary>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le déplacement d'une pièce à cet emplacement est possible</returns>
        private bool Interposer(byte liDest, byte colDest) {
            for (byte ligne = 0; ligne < 8; ligne++)
                for (byte col = 0; col < 8; col++)
                    if (PossedePiece(ligne, col) && !echiquier.EstRoi(ligne, col) && echiquier.SiDeplacer(ligne, liDest, col, colDest) && (echiquier.EstFlottante(ligne, col) || echiquier.CheminLibre(ligne, liDest, col, colDest)))
                        return true;

            return false;
        }

        #endregion
    }
}
