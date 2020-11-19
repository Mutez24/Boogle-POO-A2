using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class De
    {
        //champs
        private string[] tabDeLettre;//tableau de 6 lettre de dé
        private string lettre;//face visible du dé

        //constructeurs
        /// <summary>
        /// on égalise le tableau passé en parametre a celui présent dans les champs de la classe
        /// on créer a l'aide de la focntion lance une face visible aléatoire parmis celle donné dans le tableau de lettre
        /// </summary>
        /// <param name="Tableaudelettre">tableau de 6 lettre d'un dé</param>
        public De(string[] Tableaudelettre)
        {
            this.tabDeLettre = Tableaudelettre;
            Random r = new Random();
            Lance(r);
            
        }

        //propriétés
        /// <summary>
        /// obligatoire pour la réalisation du plateau
        /// la face visible doit être en public pour pouvoir être afficher dans le plateau
        /// </summary>
        public string Facevisible 
        {
            get
            {
                return this.lettre;
            }
        }

        //méthodes
        public void Lance (Random r)
        {
            int chiffre = r.Next(0, 5);
            //Console.WriteLine(chiffre);//permet de vérifier que le chiffre tiré au hasard correspond a face visible, et permet de vérifier qu'il y a un bien un chiffre tiré au hasard
            this.lettre = this.tabDeLettre[chiffre];//permet de tiré la face visible parmis celle presente dans le tableau de lettre
        }

        /// <summary>
        /// permet de décrire un dé
        /// </summary>
        /// <returns>la chaine de mot qui décrit le dé</returns>
        public string ToooooString()
        {
            string tabmot = "";
            for (int i = 0; i < tabDeLettre.Length; i++)
            {
                tabmot = tabmot + tabDeLettre[i] + " ; ";
            }
            string mot = "La lettre visible est " + this.lettre + ". \n" + "Le dé est composé des lettres suivantes : " + tabmot;
            return mot;
        }


    }
}
