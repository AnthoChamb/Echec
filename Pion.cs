using System;

namespace Echec {
    /// <summary>Classe du pion au jeu d'échec</summary>
    public class Pion : PieceInit {
        /// <summary>Crée un pion au jeu d'échec de la couleur spécifié</summary>
        /// <param name="couleur">Couleur de la pièce</param>
        public Pion(Couleur couleur) : base('\u2659', couleur, false) { }

        /// <summary>Évalue si le déplacement du pion de la source à la destination est possible</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colSrc">Indice de la colonne source</param>
        /// <param name="colDest">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le déplacement du pion est possible</returns>
        /// <remarks>Cette méthode ne tient pas compte des autres pièces possiblement présentes sur l'<see cref="Echiquier"></see></remarks>
        public override bool SiDeplacer(byte liSrc, byte liDest, byte colSrc, byte colDest) => colSrc == colDest && (Init ? Couleur == Couleur.BLANC ? liDest - liSrc == -1 || liDest - liSrc == -2 : liDest - liSrc == 1 || liDest - liSrc == 2 : Couleur == Couleur.BLANC ? liDest - liSrc == -1 : liDest - liSrc == 1);


        /// <summary>Évalue si le déplacement du pion de la source pour manger celle de la destination est possible</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colSrc">Indice de la colonne source</param>
        /// <param name="colDest">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le déplacement du pion pour manger la destination est possible</returns>
        /// <remarks>Cette méthode ne tient pas compte des autres pièces possiblement présentes sur l'<see cref="Echiquier"/></remarks>
        public override bool SiManger(byte liSrc, byte liDest, byte colSrc, byte colDest) => Couleur == Couleur.BLANC && Math.Abs(liDest - liSrc) == 1 ? colDest - colSrc == -1 : colDest - colSrc == 1;
 
    }
}