using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec {
    /// <summary>Classe d'une pièce d'échec mémorisant si elle a été déplacée de sa position initial. Cette clase ne peut pas être instanciée</summary>
    public abstract class PieceInit : Piece {
        private bool init;

        /// <summary>Crée une pièce d'échec avec toutes ses propriétés considérée à sa position initial</summary>
        /// <param name="unicode">Caractère unicode représentant la pièce blanche</param>
        /// <param name="couleur">Couleur de la pièce</param>
        /// <param name="flottante">Définit si la pièce peut flotter par dessus les autres pièces lors des déplacements</param>
        /// <exception cref="ArgumentOutOfRangeException">Le caractère unicode de la pièce doit se situer entre U+2654 et U+2659</exception>
        public PieceInit(char unicode, Couleur couleur, bool flottante) : base(unicode, couleur, flottante) => init = false;

        /// <summary>Obtient et définit si la pièce a été déplacée de sa position initial</summary>
        public bool Init { get => init; set => init = value; }
    }
}
