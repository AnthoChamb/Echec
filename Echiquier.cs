using System;

namespace Echec {
    /// <summary>Classe du plateau d'échec (échiquier)</summary>
    public class Echiquier {
        private readonly Piece[,] plateau;

        /// <summary>Crée un nouveau échiquier avec toutes les pièces à leur position initial</summary>
        public Echiquier() {
            plateau = new Piece[8, 8];

            // Initialisation des pièces noires
            plateau[0, 0] = new Tour(Couleur.NOIR);
            plateau[0, 1] = new Cavalier(Couleur.NOIR);
            plateau[0, 2] = new Fou(Couleur.NOIR);
            plateau[0, 3] = new Reine(Couleur.NOIR);
            plateau[0, 4] = new Roi(Couleur.NOIR);
            plateau[0, 5] = new Fou(Couleur.NOIR);
            plateau[0, 6] = new Cavalier(Couleur.NOIR);
            plateau[0, 7] = new Tour(Couleur.NOIR);
            for (int col = 0; col < 8; col++) plateau[1, col] = new Pion(Couleur.NOIR);

            // Initialisation des cases vides
            for (int ligne = 2; ligne < 6; ligne++)
                for (int col = 0; col < 8; col++)
                    plateau[ligne, col] = null;

            // Initialisation des pièce blanches
            for (int col = 0; col < 8; col++) plateau[6, col] = new Pion(Couleur.BLANC);
            plateau[7, 0] = new Tour(Couleur.BLANC);
            plateau[7, 1] = new Cavalier(Couleur.BLANC);
            plateau[7, 2] = new Fou(Couleur.BLANC);
            plateau[7, 3] = new Reine(Couleur.BLANC);
            plateau[7, 4] = new Roi(Couleur.BLANC);
            plateau[7, 5] = new Fou(Couleur.BLANC);
            plateau[7, 6] = new Cavalier(Couleur.BLANC);
            plateau[7, 7] = new Tour(Couleur.BLANC);
        }

        /// <summary>Évalue si la case de l'échiquier est vide</summary>
        /// <param name="ligne">Indice de la ligne</param>
        /// <param name="col">Indice de la colonne</param>
        /// <returns>Retourne true si la case de l'échiquier ne contient pas une pièce</returns>
        public bool EstVide(byte ligne, byte col) => plateau[ligne, col] == null;

        /// <summary>Obtient si la pièce à la ligne et colonne précisée est flottante</summary>
        /// <param name="ligne">Indice de la ligne</param>
        /// <param name="col">Indice de la colonne</param>
        /// <returns>Retourne true si la pièce est flottante</returns>
        public bool EstFlottante(byte ligne, byte col) => plateau[ligne, col].Flottante;

        /// <summary>Obtient la couleur de la pièce à la ligne et colonne précisée</summary>
        /// <param name="ligne">Indice de la ligne</param>
        /// <param name="col">Indice de la colonne</param>
        /// <returns>Retourne la couleur de la pièce</returns>
        public Couleur CouleurPiece(byte ligne, byte col) => plateau[ligne, col].Couleur;

        /// <summary>Évalue si le déplacement de la source à la destination est possible</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne source</param>
        /// <param name="colSrc">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le déplacement de la source à destination est possible</returns>
        public bool SiDeplacer(byte liSrc, byte liDest, byte colSrc, byte colDest) => plateau[liSrc, colSrc].SiDeplacer(liSrc, liDest, colSrc, colDest);

        /// <summary>Évalue si le déplacement de la source pour manger la destination est possible</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne source</param>
        /// <param name="colSrc">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le déplacement de la source pour manger la destination est possible</returns>
        public bool SiManger(byte liSrc, byte liDest, byte colSrc, byte colDest) => plateau[liSrc, colSrc].SiManger(liSrc, liDest, colSrc, colDest);

        /// <summary>Obtient si la pièce à la ligne et colonne précisée est un pion</summary>
        /// <param name="ligne">Indice de la ligne</param>
        /// <param name="col">Indice de la colonne</param>
        /// <returns>Retourne true si la pièce est un pion</returns>
        public bool EstPion(byte ligne, byte col) => plateau[ligne, col] is Pion;

        /// <summary>Obtient si la pièce à la ligne et colonne précisée est un roi</summary>
        /// <param name="ligne">Indice de la ligne</param>
        /// <param name="col">Indice de la colonne</param>
        /// <returns>Retourne true si la pièce est un roi</returns>
        public bool EstRoi(byte ligne, byte col) => plateau[ligne, col] is Roi;

        /// <summary>Évalue si le chemin est libre entre la source et la destination</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne source</param>
        /// <param name="colSrc">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le chemin entre la source et la destination ne contient pas une pièce</returns>
        public bool CheminLibre(byte liSrc, byte liDest, byte colSrc, byte colDest) {
            sbyte increment; // Définit l'incrémentation à partir de la source vers la destination
            if (liSrc == liDest) {
                // Déplacement horizontal
                increment = (sbyte)(colSrc > colDest ? -1 : 1);
                for (byte col = colSrc; col != colDest; col = (byte)(col + increment))
                    if (plateau[liSrc, col] != null)
                        return false;

            } else if (colSrc == colDest) {
                // Déplacement vertical
                increment = (sbyte)(liSrc > liDest ? -1 : 1);
                for (byte ligne = liSrc; ligne != liDest; ligne = (byte)(ligne + increment))
                    if (plateau[ligne, colSrc] != null)
                        return false;

            } else {
                // Déplacement diagonal
                increment = (sbyte)(liSrc > liDest ? -1 : 1);
                byte col = colSrc;
                for (byte ligne = liSrc; ligne != liDest; ligne = (byte)(ligne + increment))
                    if (plateau[ligne, col] != null)
                        return false;
                    else
                        col = (byte)(col + increment);
            }

            return true;
        }

        /// <summary>Évalue si le roque est possible pour la couleur et la taille choisie</summary>
        /// <param name="couleur">Couleur a évalué</param>
        /// <param name="grand">Taille du roque. true pour le grand roque, false pour le petit roque</param>
        /// <returns>Retourne true si le roque est possible</returns>
        public bool SiRoquable(Couleur couleur, bool grand) {
            byte ligne = (byte)(couleur == Couleur.NOIR ? 0 : 7); // Ligne où est joué le roque selon la couleur
            byte col = (byte)(grand ? 0 : 7); // Colonne de la tour où est joué le roque
            return plateau[ligne, 4] is Roi roi && SiPieceRoquable(roi, couleur) && plateau[ligne, col] is Tour tour && SiPieceRoquable(tour, couleur) && CheminLibre(ligne, ligne, 4, col);
        }

        /// <summary>Évalue si le roque est possible pour la pièce et la coleur choisie</summary>
        /// <param name="piece">Pièce a évalué</param>
        /// <param name="couleur">Couleur a évalué</param>
        /// <returns></returns>
        private bool SiPieceRoquable(PieceInit piece, Couleur couleur) => piece.Couleur == couleur && piece.Init;

        /// <summary>Joue le coup de la source à la destination</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne source</param>
        /// <param name="colSrc">Indice de la colonne de destination</param>
        /// <remarks>Cette méthode ne tient pas compte de si le coup est valide. Une vérification doit être effectuée avant</remarks>
        public void JouerCoup(byte liSrc, byte liDest, byte colSrc, byte colDest) {
            plateau[liDest, colDest] = plateau[liSrc, colSrc];
            plateau[liSrc, colSrc] = null;

            if (plateau[liDest, colDest] is PieceInit piece)
                piece.Init = false;
        }

        /// <summary>Joue la prise en passant de la source à la destination</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colDest">Indice de la colonne source</param>
        /// <param name="colSrc">Indice de la colonne de destination</param>
        /// <remarks>Cette méthode ne tient pas compte de si le coup est valide. Une vérification doit être effectuée avant</remarks>
        public void JouerEnPassant(byte liSrc, byte liDest, byte colSrc, byte colDest) {
            JouerCoup(liSrc, liDest, colSrc, colDest);
            plateau[liDest + plateau[liDest, colDest].Couleur == Couleur.NOIR ? 1 : -1, colDest] = null;
        }

        /// <summary>Joue le roque pour la couleur et la taille choisie</summary>
        /// <param name="couleur">Couleur du roque a effectué</param>
        /// <param name="grand">Taille du roque. true pour le grand roque, false pour le petit roque</param>
        /// <remarks>Cette méthode ne tient pas compte de si le roque est valide. Une vérification doit être effectuée avant</remarks>
        public void JouerRoque(Couleur couleur, bool grand) {
            byte ligne = (byte)(couleur == Couleur.NOIR ? 0 : 7);
            JouerCoup(ligne, ligne, 4, (byte)(grand ? 2 : 6)); // Déplacement du roi
            JouerCoup(ligne, ligne, (byte)(grand ? 0 : 7), (byte)(grand ? 3 : 5)); // Déplacement de la tour
        }

        /// <summary>Effectue la promotion de la piece à la ligne et colonne précisée en le type choisi</summary>
        /// <param name="ligne">Indice de la ligne</param>
        /// <param name="col">Indice de la colonne</param>
        /// <param name="promotion">Type de la promotion en chaine</param>
        public void Promotion(byte ligne, byte col, string promotion) => plateau[ligne, col] = (Piece)Type.GetType(promotion).GetConstructors()[0].Invoke(new object[] { plateau[ligne, col].Couleur });

        /// <summary>Obtient une représentation en chaine de l'échiquier</summary>
        /// <returns>Retourne une représentation en chaine de l'échiquier</returns>
        public override string ToString() {
            string echiquier = "";
            foreach(Piece piece in plateau)
                echiquier += piece?.ToString() ?? " ";
            return echiquier;
        }
    }
}
