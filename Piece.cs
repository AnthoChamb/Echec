using System;

namespace Echec {
    /// <summary>Classe d'une pièce d'échec. Cette clase ne peut pas être instanciée</summary>
    public abstract class Piece {
        private readonly char unicode;
        private readonly Couleur couleur;
        private readonly bool flottante;

        /// <summary>Crée une pièce d'échec avec toutes ses propriétés</summary>
        /// <param name="unicode">Caractère unicode représentant la pièce blanche</param>
        /// <param name="couleur">Couleur de la pièce</param>
        /// <param name="flottante">Définit si la pièce peut flotter par dessus les autres pièces lors des déplacements</param>
        /// <exception cref="ArgumentOutOfRangeException">Le caractère unicode de la pièce doit se situer entre U+2654 et U+2659</exception>
        public Piece(char unicode, Couleur couleur, bool flottante) {
            if (unicode < '\u2654' || unicode > '\u2659')
                throw new ArgumentOutOfRangeException("unicode", "Le caractère unicode de la pièce doit se situer entre U+2654 et U+2659");

            this.unicode = unicode;
            this.couleur = couleur;
            this.flottante = flottante;
        }

        /// <summary>Obtient la couleur de la pièce</summary>
        public Couleur Couleur { get => couleur; }

        /// <summary>Obtient si la pièce peut flotter par dessus les autres pièces lors des déplacements</summary>
        public bool Flottante { get => flottante; }

        /// <summary>Évalue si le déplacement de la pièce de la source à la destination est possible</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colSrc">Indice de la colonne source</param>
        /// <param name="colDest">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le déplacement de la pièce est possible</returns>
        /// <remarks>Cette méthode ne tient pas compte des autres pièces possiblement présentes sur l'<see cref="Echiquier"></see></remarks>
        public abstract bool SiDeplacer(byte liSrc, byte liDest, byte colSrc, byte colDest);

        /// <summary>Évalue si le déplacement de la pièce de la source pour manger celle de la destination est possible</summary>
        /// <param name="liSrc">Indice de la ligne source</param>
        /// <param name="liDest">Indice de la ligne de destination</param>
        /// <param name="colSrc">Indice de la colonne source</param>
        /// <param name="colDest">Indice de la colonne de destination</param>
        /// <returns>Retourne true si le déplacement de la pièce pour manger la destination est possible</returns>
        /// <remarks>Cette méthode ne tient pas compte des autres pièces possiblement présentes sur l'<see cref="Echiquier"/>.
        /// L'implémentation de base de <see cref="SiManger(byte, byte, byte, byte)"/> est le résultat de <see cref="SiDeplacer(byte, byte, byte, byte)"/></remarks>
        virtual public bool SiManger(byte liSrc, byte liDest, byte colSrc, byte colDest) => SiDeplacer(liSrc, liDest, colSrc, colDest);

        /// <summary>Retourne une chaine qui représente la pièce</summary>
        /// <returns>Retourne une chaine qui représente la pièce</returns>
        public override string ToString() => ((char)(unicode + (int)couleur)).ToString();
    }
}
