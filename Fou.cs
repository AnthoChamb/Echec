using System;

namespace Echec {
    /// <summary>Classe de la pièce du fou au jeu d'échec</summary>
    public class Fou : Piece {
        /// <summary>Crée une pièce de fou au jeu d'échec de la couleur spécifié</summary>
        /// <param name="couleur">Couleur de la pièce</param>
        public Fou(Couleur couleur) : base('\u2657', couleur, false) { }

        /// <summary>Évalue si le déplacement du fou de la source à la destination est possible</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colSrc">Indice de la colonne source</param>
        /// <param name="colDest">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le déplacement du fou est possible</returns>
        /// <remarks>Cette méthode ne tient pas compte des autres pièces possiblement présentes sur l'<see cref="Echiquier"></see></remarks>
        public override bool SiDeplacer(byte liSrc, byte liDest, byte colSrc, byte colDest) {
            throw new NotImplementedException();
        }
    }
}