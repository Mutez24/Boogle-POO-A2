using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Joueur
    {
        //champs, 3 variable, j'ai pris un tableau de taille 30 car suffisant pour un joueur
        private string nom;
        private int score = 0;
        private string[] tabMotTrouvé = new string [30];

        //constructeurs
        public Joueur (string Nom)
        {
            if (this.nom == "")
            {
                this.nom = "Veuillez rentrer un nom correct";
            }
            else
            {
                this.nom = Nom;
            }
        }

        //propriétés
        /// <summary>
        /// la propriété pour le nom du joueur est nécessaire car on l'utilise pour l'afficher dansle programme du jeu
        /// il doit donc être en get pour y avoir accès mais pas en set (on ne veut pas le modifier)
        /// </summary>
        public string LeNom
        {
            get
            {
                return this.nom;
            }
        }


        //méthodes
        /// <summary>
        /// teste si le mot passé appartient déjà aux mots trouvés par le joueur (dans le tableau de mot trouvé)
        /// </summary>
        /// <param name="mot"> mot que l'on teste</param>
        /// <returns> retourne vraie si le mot appartient déjà aux mots trouvés et faux si l'inverse</returns>
        public bool Contain (string mot)
        {
            bool contain = false;
            for (int i = 0; i < tabMotTrouvé.Length; i++)//boucle qui parcourt le tableau de mot trouvé
            {
                if (tabMotTrouvé[i] == mot)//condition qui compare tous les mots du tableau avec le mot test
                {
                    contain = true;
                }
            }
            return contain;
        }

        /// <summary>
        /// ajoute le mot dans la liste des mots déjà trouvés
        /// ajoute les points au score si le mot n'a pas déja été trouvé (en fonction de la taille du mot)
        /// </summary>
        /// <param name="mot">mot que l'on veut ajouter dans la liste</param>
        public void Add_Mot (string mot)
        {
            int taillemot = mot.Length;
            for (int i = 0; i < this.tabMotTrouvé.Length; i++)
            {
                if ((this.tabMotTrouvé[i] == null) || (this.tabMotTrouvé[i] == ""))//si la case est vide au rang i, alors remplir la case avec le mot trouvé
                {
                    this.tabMotTrouvé[i] = mot;//tous les if ci-dessous permettent l'ajout de point au score en fonction de la taille du mot
                    if (taillemot == 3)
                    {
                        score = score + 2;
                    }
                    if (taillemot == 4)
                    {
                        score = score + 3;
                    }
                    if (taillemot == 5)
                    {
                        score = score + 4;
                    }
                    if (taillemot == 6)
                    {
                        score = score + 5;
                    }
                    if (taillemot >= 7)
                    {
                        score = score + 11;
                    }
                    break;//le break permet de sortir de la boucle for, pour ne pas rajouter le mot dès qu'il y a une case vide
                }
            }
        }

        /// <summary>
        /// affiche le score, le nom et les mots cités par le joueur
        /// création de deux strings, le 1er pour stocké les mots trouvés, le 2ème pour retourner la chaine de mot que verra l'utilisateur
        /// </summary>
        /// <returns>retourne une chaine de caractère</returns>
        public string ToooString()
        {
            string motTableau = "";
            for (int i=0; i < this.tabMotTrouvé.Length; i++)
            {
                motTableau = motTableau + this.tabMotTrouvé[i] + " ";//permet de créer une chaine de mot qui contiendra tous les mots trouvé du joueur
            }
            string mot = "Le score de " + this.nom + " est de " + this.score + " grâce aux mots cités suivants : \n" + motTableau;
            return mot;
        }


    }
}
