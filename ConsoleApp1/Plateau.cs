using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Plateau
    {
        //champs
        private string[] tabfichierdede1 = new string[16];//tableau qui lit le fichier dé, il a donc une taille de 16, les 16 dés
        private De [,]plateau1 = new De[4,4];//matrice de dé qui est le plateau final

        //constructeur
        public Plateau (string filename)
        {
            //création du premier tableau qui lit le fichier et assimile une ligne (donc un dé) a une case
            StreamReader r = null;
            try
            {
                r = new StreamReader(filename);
                string line;
                int i = 0;
                while ((line = r.ReadLine()) != null)
                {
                    tabfichierdede1[i] = line;
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
                if (r != null)
                {
                    r.Close();
                }
            }




            //création du plateau
            //cette fonction parcour la matrice palteau et lui assimile un Dé a chaque case (a l'aide du tableau de dé)
            //on utlise 3 variable i,j et k, car i et j servent a parcourir la matrice et vont de 0 à 3
            //alors que k sert a parcourir le tableau de 16 cases et va donc de 0 à 15
            int k = 0;
            for (int i = 0; i < plateau1.GetLength(0); i++)
            {
                for (int j = 0; j < plateau1.GetLength(1); j++)
                {
                    plateau1[i, j] = new De(tabfichierdede1[k].Split(';')); //pour créer un dé a chaque case
                    k++;                    //au dessus, on entre un tableau de Dé pour pouvoir créer un dé, on split car sinon c'est une "ligne" de dé et pas un tableau de lettre
                    System.Threading.Thread.Sleep(10);// pour attendre 10 ms pour que la fonction random puisse avoir le temps de se renouveler (car fonctionne avec l'horloge de l'ordinateur)
                }                                     //cela permet de ne pas avoir le même chiffre aléatoire pour tous les dés
            }

        }

        //propriétés
        //pas de propriétés requisent car on n'utilise aucun champs de cette classe en dehors de la classe (en public)

        //methode

        /// <summary>
        /// permet d'afficher la matrice qui repésente le plateau
        /// </summary>
        /// <returns>une chaine de mot qui représente le plateau</returns>
        public string Tostringplateau()
        {
            string mot = "";
            for (int i = 0; i < plateau1.GetLength(0); i++)
            {
                for (int j = 0; j < plateau1.GetLength(1); j++)//permet de parcourir la matrice
                {
                    mot = mot + plateau1[i, j].Facevisible + " ";
                }
                mot = mot + "\n";//permet de revenir a la ligne
            }
            return mot;
        }

       /// <summary>
       /// fonction qui permet de stocker les lettres adjacentes (dans un tableau) a la lettre passée en parametre (a l'aide de ses coordonés)
       /// il y a 9 cas différents
       /// </summary>
       /// <param name="ligne">ligne de la lettre</param>
       /// <param name="colonne">colonne de la lettre</param>
       /// <returns>un tableau qui contient les lettres adjacentes à la lettre rentrée</returns>
        public string[] Adjacent(int ligne,int colonne)
        {
            string[] adjacent = null;
            //pour vérifier l'entourage de la case (0,0)
            if ((ligne == 0)&&(colonne == 0))
            {
                int k = 0;
                for (int i = 0; i <=1; i++)
                {
                    for (int j = 0; j <=1; j++)
                    {
                        if((i != 0) && (j != 0))
                        {
                            adjacent[k] = plateau1[i, j].Facevisible;
                            k++;
                        }
                    }
                }
            }
            //pour vérifier l'entourage de la case (0,3)
            if ((ligne == 0)&&(colonne == 3))
            {
                int k = 0;
                for (int i = 0; i <= 1; i++)
                {
                    for (int j = 2; j <= 3; j++)
                    {
                        if ((i != 0) && (j != 3))
                        {
                            adjacent[k] = plateau1[i, j].Facevisible;
                        }
                    }
                }
            }
            //pour vérifier l'entourage de a case (3,3)
            if ((ligne == 3) && (colonne == 3))
            {
                int k = 0;
                for (int i = 2; i <= 3; i++)
                {
                    for (int j = 2; j <= 3; j++)
                    {
                        if ((i != 3) && (j != 3))
                        {
                            adjacent[k] = plateau1[i, j].Facevisible;
                        }
                    }
                }
            }
            //pour vérifier l'entourage de la case (3,0)
            if ((ligne == 3) && (colonne == 0))
            {
                int k = 0;
                for (int i = 2; i <= 3; i++)
                {
                    for (int j = 0; j <= 1; j++)
                    {
                        if ((i != 3) && (j != 0))
                        {
                            adjacent[k] = plateau1[i, j].Facevisible;
                        }
                    }
                }
            }
            //pour vérifier l'entouarge des case (1,0) et (2,0)
            if (((ligne == 1) && (colonne == 0)) || ((ligne == 2) && (colonne == 0)))
            {
                int k = 0;
                for (int i = (ligne - 1); i <= (ligne + 1); i++)
                {
                    for (int j = colonne; j <= (colonne + 1); j++)
                    {
                        if ((i != ligne) && (j != colonne))
                        {
                            adjacent[k] = plateau1[i, j].Facevisible;
                        }
                    }
                }
            }
            //pour vérifier l'entourage des cases (0,1) et (0,2)
            if (((ligne == 0) && (colonne == 1)) || ((ligne == 0) && (colonne == 2)))
            {
                int k = 0;
                for (int i = (ligne); i <= (ligne + 1); i++)
                {
                    for (int j = (colonne-1); j <= (colonne + 1); j++)
                    {
                        if ((i != ligne) && (j != colonne))
                        {
                            adjacent[k] = plateau1[i, j].Facevisible;
                        }
                    }
                }
                return adjacent;
            }
            //pour vérifier l'entourage des cases (1,3) et (2,3)
            if (((ligne == 1) && (colonne == 3)) || ((ligne == 2) && (colonne == 3)))
            {
                int k = 0;
                for (int i = (ligne - 1); i <= (ligne + 1); i++)
                {
                    for (int j = (colonne-1); j <= colonne; j++)
                    {
                        if ((i != ligne) && (j != colonne))
                        {
                            adjacent[k] = plateau1[i, j].Facevisible;
                        }
                    }
                }
            }
            //pour vérifier l'entourage des cases (3,1) et (3,2)
            if (((ligne == 1) && (colonne == 0)) || ((ligne == 2) && (colonne == 0)))
            {
                int k = 0;
                for (int i = (ligne - 1); i <= ligne; i++)
                {
                    for (int j = (colonne-1); j <= (colonne + 1); j++)
                    {
                        if ((i != ligne) && (j != colonne))
                        {
                            adjacent[k] = plateau1[i, j].Facevisible;
                        }
                    }
                }
            }
            //pour vérifier l'entourage des 4 cases du milieu
            if (((ligne == 1) || (ligne == 2)) && ((colonne == 1) || (colonne == 2)))
            {
                int k = 0;
                for (int i = (ligne - 1); i <= (ligne + 1); i++)
                {
                    for (int j = (colonne-1); j <= (colonne + 1); j++)
                    {
                        if ((i != ligne) && (j != colonne))
                        {
                            adjacent[k] = plateau1[i, j].Facevisible;
                        }
                    }
                }
            }
            return adjacent;
        }

        /// <summary>
        /// booléen qui permet de vérifier si la lettre [i+1] est dans l'entourage de la lettre [i]
        /// </summary>
        /// <param name="lettre">lettre [i+1] suivant la lettre[i]</param>
        /// <param name="adjacent">tableau de lettre adjacent a la lettre [i]</param>
        /// <returns>true si la lettre [i+1] est dans l'entourage de la lettre [i], false si non</returns>
        public bool LaLettreEstDansLEntourage(string lettre, string[] adjacent)
        {
            bool resultat = false;
            for (int i = 0; i < adjacent.Length; i++)//parcour le tableau de lettre adjacent
            {
                if (lettre == adjacent[i])//si la condition n'est pas vérifiée le resultat reste faux, sinon il devient vraie
                {
                    resultat = true;
                }
            }
            return resultat;
        }

        /// <summary>
        /// permet de transformer un mot en tableau de char
        /// </summary>
        /// <param name="mot">mot que l'on veut transformer</param>
        /// <returns>tableau de char (du mot passé en parametre)</returns>
        public char[] TransformerStringToChar (string mot)
        {
            char[] tab = mot.ToCharArray();//fonction qui permet la transformation
            return tab;
        }

        /// <summary>
        /// permet de vérifier si le mot respecte les conditions du plateau
        /// je n'ai pas réussi a finir cette fonction, c'est pour ca qu'elle est en commentaire, pour ne pas géner la compliation de mon programme
        /// 
        /// </summary>
        /// <param name="mot">mot que l'on veut vérifier</param>
        /// <returns>vraie si il respect les règles, faux sinon</returns>
        //public bool Test_Plateau(char[] mot)
        //{
        //    bool test = false;
        //    int compteur = 0;
        //    for (int i = 0; i < mot.Length; i++)
        //    {

        //        if ()
        //        {
        //            compteur++;
        //        }
        //    }
        //    if (compteur == mot.Length)
        //    {
        //        test = true;
        //    }
        //    return test;
        //}

    }
}
