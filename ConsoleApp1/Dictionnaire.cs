using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Dictionnaire
    {
        //champs
        private string[] tabmotsfichier1 = new string[28]; //taille28 car le fichier possede 28 lignes
        private string[] tabmotsfichier2 = new string[14]; //taille 14 car le tableau contient que les mots et pas les chiffres
        private string[][] tabmotsfichier3 = new string[14][]; //tableau final du dictionnaire (tableau en escalier)

        //constructeurs
        public Dictionnaire (string filename)
        {
            //création du tableau 1, où chaque case est constitué d'un ensemble de mot qui possède le même nombre de lettre ou d'un chiffre
            //chaque case est constitué d'une ligne de mot de même taille
            StreamReader r = null;
            try
            {
                r = new StreamReader(filename);
                string line = "";
                int i = 0;
                while ((line = r.ReadLine()) != null)
                {
                    tabmotsfichier1[i] = line;
                    i++;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if(r != null)
                {
                    r.Close();
                }
            }

            //creation du tableau2, ici on retire les cases où il y a des numéros (cad les cases de valeur (n*2)+1)
            //on obtient un tableau plus propre, facile à gérer pour la suite
            for (int i = 0; i < tabmotsfichier2.Length; i++)
            {
                tabmotsfichier2[i] = tabmotsfichier1[(2*i) + 1];//pour prendre les cases où il  y a des numéros
            }

            //création du tableau 3, ici on créer le tableau final
            //c'est un tableau en escalier où ou chaque case contient un tableau de mot de même taille
            for (int i = 0; i < tabmotsfichier2.Length; i++)//parcour le 2ème tableau
            {
                string[] tabfichiertemp = tabmotsfichier2[i].Split(' ');//créer un tableau temporaire dans lequel est stocké un tableau de mot de même taille (permet le split, et d'avoir la taille du tableau)
                tabmotsfichier3[i] = new string[tabfichiertemp.Length];//création des cases vides de "tableau de tableau"
                for (int j = 0; j < tabfichiertemp.Length; j++)
                {
                    tabmotsfichier3[i][j] = tabfichiertemp[j];//on remplit les cases de tableau de tableau à l'aide du tableau temporaire
                }                                             //le j parcour les cases de tableau de tableau et aussi celles du tableau temporaire
            }
     

        }

        //propriété
        //pas de propriétés requisent car on n'utilise aucun champs de cette classe en dehors de la classe (en public)


        //méthodes
        /// <summary>
        /// permet d'afficher le tableau en escalier 
        /// </summary>
        /// <returns>un string qui représente le dictionnnaire</returns>
        public string TooooString()
        {
            string mot = "";
            for (int i = 0; i < tabmotsfichier3.Length; i++)
            {
                mot = mot + "Element (" + (i+2) + ") : ";//permet d'afficher le nombre de lettre des mots qui s'afficherront, i+2 car le tableau commence au lettre de taille 2 et pas 0
                for (int j = 0; j < tabmotsfichier3[i].Length; j++)
                {
                    mot = mot + tabmotsfichier3[i][j] + " ";
                }
                mot = mot + "\n";//retoure a la ligne car on passe au tableau de lettre de taille supérieur
            }
            return mot;
        }

        /// <summary>
        /// j'ai créer cette version "void" de la fonction string car elle est plus rapide a compiler (donc plus pratique pour voir si ma lecture de fichier est bonne)
        /// </summary>
        public void Afficher()
        {
            for (int i = 0; i < tabmotsfichier3.Length; i++)
            {
                Console.WriteLine("Element (" + i + ") : ");
                for (int j = 0; j < tabmotsfichier3[i].Length; j++)
                {
                    Console.Write(tabmotsfichier3[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// cete méthode me permet d'obtenir la taille d'un tableau de mot de même taille
        /// il me permet d'obtenir le "fin" pour pouvoir commencer ma recherche dico recursive
        /// </summary>
        /// <param name="mot">ici on rentre le mot que l'on veut tester dans la recherche dico, afin d'avoir sa taille</param>
        /// <returns>retourne le nombre de mot de la même taille que celui passer en parametre</returns>
        public int Fin(string mot)
        {
            int taillemot = mot.Length;
            int fin = 0;
            if (taillemot <= 2)//car le jeu ne prend pas en compteles mots de taille 2 et en dessous
            {
                fin = -1;
            }
            else
            {
                fin = tabmotsfichier3[taillemot - 2].Length;//ici on récupère la taille du tableau de tableau, donc le nombre de mot qu'il y a de la mêeme taille
            }                                               //[taillemot-2] car le tableau commence au mot de taille 2 et pas 0
            return fin;
        }

        /// <summary>
        /// effectue la recherche dico
        /// vérifie d'abord si le mot fait bien plus de 2 lettres
        /// effectue la recherche en 4 cas possible
        /// 2 cas de sortie de la boucle recursive (succès ou echec)
        /// 2 cas où l'on relance la recherche dico (si le mot cherché est au dessus ou en dessous du milieu)
        /// </summary>
        /// <param name="debut">vaut toujours 0</param>
        /// <param name="fin">taille du tableau de mot qui contient les mots de même taille que le mot passé en parametre</param>
        /// <param name="mot">mot que l'on veut tester, voir s'il est dans le dictionnaire</param>
        /// <returns></returns>
        public bool RechDichoRecursif(int debut, int fin, string mot)
        {
            int tailledumot = mot.Length; //pour savoir dans quelle case du tableau en escalier on se situe
            bool reponse = false;
            //cas où le mot ne peut pas etre pris en compte en vue de sa taille
            if (tailledumot <= 2)
            {
                reponse = false;
            }
            else
            {
                mot = mot.ToUpper();//car le fichier de tableau est en majuscule
                int milieu = (debut + fin) / 2;//pour determiner le milieu 
                int resultat = string.Compare(mot, tabmotsfichier3[tailledumot - 2][milieu]);//permet de comparer la "l'ordre alphabétique" entre le mot d'entrer et celui du milieu
                //cas pour sortir de la boucle                                               //[tailledumot-2] car fichier commence au mot de taille 2 et pas 0, aussi cela correspond a la taille du mot
                if (fin < debut)                                                             
                {
                    reponse = false;
                }
                else
                {
                    // si le resultat est superieur a 0, le mot se trouve dans la partie inferieur au milieu, la borne superieur devient le milieu
                    if (resultat > 0)
                    {
                        milieu = milieu + 1; //on change le valeur du milieu car on sait que celle ci n'est pas egale au mot
                        debut = milieu;
                        return RechDichoRecursif(debut, fin, mot);//on relance la recherche mais avec la borne supérieur changé
                    }
                    // si le resultat est inferieur a 0, le mot se trouve dans la partie superieur au milieu, la borne inferieur devient le milieu
                    if (resultat < 0)
                    {
                        milieu = milieu - 1;
                        fin = milieu;
                        return RechDichoRecursif(debut, fin, mot);//on relance la recherche mais avec la borne inférieur changé
                    }
                    //si le resultat est egale a zero, alors les mots comparés sont égaux, la reponse devient vraies, on sort de la boucle récursive
                    if (resultat == 0)
                    {
                        reponse = true;
                    }
                }
       
            }
            return reponse;

        }
    }
}

