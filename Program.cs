using System;
using System.Windows.Forms;

namespace Echec {
    /// <summary>Classe d'exécution du programme</summary>
    static class Program {
        /// <summary>Point d'entrée principal de l'application</summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Echec().Demarrer();
        }
    }
}
