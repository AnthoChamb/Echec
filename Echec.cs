using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Echec {
    /// <summary>Classe maîtresse du jeu d'échec</summary>
    public class Echec {
        private List<Joueur> joueurs;
        private readonly FormEchec formEchec;

        /// <summary>Crée un jeu d'échec</summary>
        public Echec() {
            OuvrirJoueurs();
            formEchec = new FormEchec(this);
        }

        /// <summary>Démarre l'exécution du jeu d'échec</summary>
        public void Demarrer() {
            formEchec.Show();
        }

        /// <summary>Mets en terme à l'exécution du jeu d'échec</summary>
        public void Fermer() {
            SauvegarderJoueurs();
        }

        /// <summary>Lance une partie avec les joueurs aux indices précisés</summary>
        /// <param name="noir">Indice du joueur des pièces noires</param>
        /// <param name="blanc">Indice du joueur des pièces blanches</param>
        public void LancerPartie(int noir, int blanc) {
            new Partie(this, joueurs[noir], joueurs[blanc]).Demarrer();
        }

        /// <summary>Obtient les statistiques du joueur à l'indice précisé</summary>
        /// <param name="i">Indice du joueur</param>
        /// <returns>Retourne les statistiques du joueur sous forme de chaine</returns>
        public string this[int i] {
            get {
                throw new NotImplementedException();
            }
        }

        /// <summary>Obtient les résultats possible d'une partie entre les joueurs aux indices présisés</summary>
        /// <param name="noir">Indice du joueur des pièces noires</param>
        /// <param name="blanc">Indice du joueur des pièces blanches</param>
        /// <returns>Retourne les résultats possible d'une partie entre les joueurs sous forme de chaine</returns>
        public string ResultatsPossibles(int noir, int blanc) {
            throw new NotImplementedException();
        }

        /// <summary>Ouvre un fichier binaire et y récupère la liste de joueurs. Crée une nouvelle liste de joueurs vide le cas échéant.</summary>
        private void OuvrirJoueurs() {
            try {
                Stream flux = File.Open("joueurs.bin", FileMode.Open);
                BinaryFormatter formatteur = new BinaryFormatter();
                joueurs = (List<Joueur>)formatteur.Deserialize(flux);
                flux.Close();
            } catch (FileNotFoundException) { // Le fichier de joueurs n'existe pas
                joueurs = new List<Joueur>();
            }
        }

        /// <summary>Ouvre un fichier binaire et y écrit la liste de joueurs.</summary>
        private void SauvegarderJoueurs() {
            Stream flux = File.Open("joueurs.bin", FileMode.Create);
            BinaryFormatter formatteur = new BinaryFormatter();
            formatteur.Serialize(flux, joueurs);
            flux.Close();
        }
    }
}
