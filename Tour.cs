namespace Echec {
    /// <summary>Classe de la pièce de la tour au jeu d'échec</summary>
    public class Tour : PieceInit {
        /// <summary>Crée une pièce de tour au jeu d'échec de la couleur spécifié</summary>
        /// <param name="couleur">Couleur de la pièce</param>
        public Tour(Couleur couleur) : base('\u2656', couleur, false) { }

        /// <summary>Évalue si le déplacement est possible</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colSrc">Indice de la colonne source</param>
        /// <param name="colDest">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le déplacement est possible</returns>
        /// <remarks>Cette méthode ne tient pas compte des autres pièces possiblement présentes sur l'<see cref="Echiquier"></see></remarks>
        public override bool SiDeplacer(byte liSrc, byte liDest, byte colSrc, byte colDest) => SiDeplacerTour(liSrc,liDest,colSrc,colDest);

        /// <summary>Évalue si le déplacement de la tour de la source à la destination est possible</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colSrc">Indice de la colonne source</param>
        /// <param name="colDest">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le déplacement de la tour est possible</returns>
        internal static bool SiDeplacerTour(byte liSrc, byte liDest, byte colSrc, byte colDest) => liSrc == liDest || colSrc == colDest;

    }
}