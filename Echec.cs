using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Echec {
    /// <summary>Classe maîtresse du jeu d'échec</summary>
    public class Echec {
        private List<Joueur> joueurs;
        private readonly FormEchec formEchec;

        /// <summary>Crée un jeu d'échec</summary>
        public Echec() {
            OuvrirJoueurs();
            formEchec = new FormEchec(this);
            formEchec.AfficherJoueurs(TabJoueurs);
        }

        #region Méthodes publiques

        /// <summary>Démarre l'exécution du jeu d'échec</summary>
        public void Demarrer() => Application.Run(formEchec);

        /// <summary>Mets en terme à l'exécution du jeu d'échec</summary>
        public void Fermer() {
            SauvegarderJoueurs();
        }

        /// <summary>Lance une partie avec les joueurs aux indices précisés</summary>
        /// <param name="noir">Indice du joueur des pièces noires</param>
        /// <param name="blanc">Indice du joueur des pièces blanches</param>
        public void LancerPartie(int noir, int blanc) {
            formEchec.Hide();
            new Partie(this, joueurs[noir], joueurs[blanc]).Demarrer();
            formEchec.AfficherJoueurs(TabJoueurs);
            formEchec.Show();
        }

        /// <summary>Obtient les statistiques du joueur à l'indice précisé</summary>
        /// <param name="i">Indice du joueur</param>
        /// <returns>Retourne les statistiques du joueur sous forme de chaine</returns>
        public string this[int i] { get => "Parties jouées : " + joueurs[i].Jouees + "\nVictoires : " + joueurs[i].Victoires + "\nDéfaites : " + joueurs[i].Defaites + "\nPointage : " + joueurs[i].Pointage; }

        /// <summary>Obtient les résultats possible d'une partie entre les joueurs aux indices présisés</summary>
        /// <param name="noir">Indice du joueur des pièces noires</param>
        /// <param name="blanc">Indice du joueur des pièces blanches</param>
        /// <returns>Retourne les résultats possible d'une partie entre les joueurs sous forme de chaine</returns>
        public string ResultatsPossibles(int noir, int blanc) => "N: (" + (joueurs[noir] - joueurs[blanc]) + ", " + (joueurs[noir] + joueurs[blanc]) + ")\nB: (" + (joueurs[blanc] - joueurs[noir]) + ", " + (joueurs[blanc] + joueurs[noir]) + ")";

        #endregion

        #region Méthodes privées

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

        /// <summary>Tableau des réprésentations en chaines des joueurs en odre de pointage</summary>
        private string[] TabJoueurs {
            get {
                string[] tab = new string[joueurs.Count];
                joueurs.Sort();
                for (int i = 0; i < tab.Length; i++)
                    tab[i] = joueurs[i].ToString();
                return tab;
            }
        }

        #endregion
    }
}
