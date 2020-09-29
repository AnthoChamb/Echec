using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echec {
    /// <summary>Classe d'un joueur du jeu d'échec</summary>
    [Serializable]
    public class Joueur : IComparable {
        private readonly string nom;
        private int jouees, victoires, pointage;

        public Joueur(string nom) {
            this.nom = nom;
            jouees = victoires = pointage = 0;
        }

        /// <summary>Obtient le nom du joueur</summary>
        public string Nom { get => nom; }

        /// <summary>Obtient et définit le nombre de parties jouées par le joueur</summary>
        public int Jouees { get => jouees; set => jouees = value; }

        /// <summary>Obtient et définit le nombre de parties gagnées par le joueur</summary>
        public int Victoires { get => victoires; set => victoires = value; }

        /// <summary>Obtient le nombre de parties perdues par le joueur</summary>
        public int Defaites { get => jouees - victoires; }

        /// <summary>Obtient et définit le pointage du joueur</summary>
        public int Pointage { get => pointage; set => pointage = value; }

        /// <summary>
        /// Compare l'instance actuelle avec un autre objet du même type et retourne un entier
        /// qui indique si l'instance actuelle précède ou suit un autre objet ou se trouve
        /// à la même position dans l'ordre de tri.
        /// </summary>
        /// <param name="obj">Objet à comparer à cette instance</param>
        /// <returns>Valeur qui indique l'ordre relatif des objets comparés. La valeur de retour a
        /// les significations suivantes : Valeur Signification Inférieure à zéro Cette instance
        /// précède obj dans l'ordre de tri. Zéro Cette instance se produit dans la même
        /// position dans l’ordre de tri en tant que obj. Supérieure à zéro Cette instance
        /// suit obj dans l'ordre de tri.</returns>
        /// <exception cref="ArgumentException">L'objet doit être du même type que cette instance</exception>
        public int CompareTo(object obj) {
            throw new NotImplementedException();
        }

        /// <summary>Obtient le pointage du joueur a après une victoire face au joueur b</summary>
        /// <param name="a">Joueur a</param>
        /// <param name="b">Joueur b</param>
        /// <returns>Retourne le pointage du joueur a après une victoire face au joueur b</returns>
        public static int operator +(Joueur a, Joueur b) {
            throw new NotImplementedException();
        }

        /// <summary>Obtient le pointage du joueur a après une défaite face au joueur b</summary>
        /// <param name="a">Joueur a</param>
        /// <param name="b">Joueur b</param>
        /// <returns>Retourne le pointage du joueur a après une défaite face au joueur b</returns>
        public static int operator -(Joueur a, Joueur b) {
            throw new NotImplementedException();
        }

        /// <summary>Obtient une représentation en chaine du joueur</summary>
        /// <returns>Retourne une représentation en chaine du joueur</returns>
        public override string ToString() => nom + " (" + pointage + ")";
    }
}
