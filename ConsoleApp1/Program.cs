using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)//permet de selctionnner l'exercice voulu (en enlevant/remettant les doubles slash)
        {
            //Exotest();
            //Testclassejoueur();
            //Testclassede();
            //TestclasseDictionnaire();
            //TestclassePlateau();
            Jeu();


            Console.ReadKey();

        }

        /// <summary>
        /// Permet de tester si la classe joueur ainsi que ses fonctions compilent
        /// En l'occurence, la fonction contain et ToString
        /// </summary>
        static void Testclassejoueur()
        {
            Joueur J1 = new Joueur("Robert");//permet de créer un joueur
            J1.Add_Mot("salut");//permet de vérifier si la fonction addmot fonctionne
            J1.Add_Mot("bonjours");
            J1.Add_Mot("hello");
            Console.WriteLine(J1.Contain("salut"));//doit afficher true
            Console.WriteLine(J1.Contain("salt"));//doit afficher false
            Console.WriteLine(J1.ToooString());//permet de vérifier si le string marche, et si le score est bon (ici score de 19)
        }

        /// <summary>
        /// Permet de tester si la classe De ainsi que ses fonctions compilent
        /// En l'occurence, la fonction Lance et ToString
        /// </summary>
        static void Testclassede()
        {
            string[] tabde = { "a", "b", "c", "d", "e", "f" };//création d'un tableau de de pour pouvoir ensuite créer une calsse de
            De de1 = new De(tabde);//création de la classe de
            Console.WriteLine(de1.ToooooString());//permet de vérifier que ma classe dé fonctionne (qu'il y a bien une lettre tiré au hasard (fonction Lance) parmis les 6)
        }

        /// <summary>
        /// Permet de tester si la classe dictionnaire ainsi que ses fonctions compilent
        /// En l'occurence, la fonction RechDicoRecursif et ToString
        /// Cela me permet aussi de vérifier que mon tableau en escalier compile
        /// </summary>
        static void TestclasseDictionnaire()
        {
            Dictionnaire dico = new Dictionnaire("MotsPossibles.txt");//me permet de creer ma classe dictionnaire
            //Console.WriteLine(dico.TooooString());//je l'ai mise en commentaire car tres long a compilé, mais fonctionnne
            //dico.Afficher(); //n'est pas nécessaire mais m'a permis de vérifier si mon dictionnaire était bon
            Console.WriteLine(dico.Fin("salut")); //m'a permis de vérifier 2 chose: si il y a le bon nombre de mot a 5 lettres dans mon fichier dictionnaire, et si la fonction fin marche
            Console.WriteLine(dico.RechDichoRecursif(0, dico.Fin("salut"), "salut"));//permet de vérifier la fonction rechdichorecursf, doit retourner true
            Console.WriteLine(dico.RechDichoRecursif(0, dico.Fin("salt"), "salt"));//de même mais cii doit retourner false
        }

        /// <summary>
        /// Permet de tester si la classe Plateau ainsi que ses fonctions compilent
        /// En l'occurence, la fonction afficher et ToString
        /// Cela me permet surtout de vérifier si mon plateau génère bien les Dé aléatoirement
        /// </summary>
        static void TestclassePlateau()
        {
            Plateau P1 = new Plateau("Des.txt");//création du plateau
            Console.WriteLine(P1.Tostringplateau());// me permet de vérifier si mon affichage fonctionnne, et surtout si ce sont bien toutes des faces aléatoires (d'un différent chiffre aléatoire)

        }

        /// <summary>
        /// cela m'a permis de bien me rendre compte du fonctionnement de la fonction Split, mais n'a pas d'interet réel pour le jeu
        /// </summary>
        static void Exotest()
        {
            string mot = "salut mec comment ca va";
            string[] tabtest = mot.Split(' ');
            for (int i = 0; i < tabtest.Length; i++)
            {
                Console.WriteLine(tabtest[i]);
            }
        }

        /// <summary>
        /// permet de lancer le jeu
        /// En l'occurence chaque joueur a le droit de trouver 3 mot par tour, et il y a 2 tours
        /// </summary>
        static void Jeu()
        {
            Console.WriteLine("Bienvenue dans le jeu !!");
            Console.WriteLine("Quel est le nombre de joueur ? ");
            int nbjoueur = Convert.ToInt32(Console.ReadLine());
            Joueur[] tabjoueur = new Joueur[nbjoueur];//le tableau permet la création de plusieur joueur
            for (int i = 0; i < nbjoueur; i++)
            {
                Console.WriteLine("Quel est le nom du joueur " + (i+1));//(i+1) pour ne pas avoir le joueur 0
                string nom = Convert.ToString(Console.ReadLine());
                tabjoueur[i] = new Joueur(nom); //attribut a chaque joueur une classe Joueur
            }
            Dictionnaire dico = new Dictionnaire("MotsPossibles.txt"); //permet de créer le dictionnaire
            int grandtour = 0;
            while (grandtour <= 2)//ici on peut changer le nombre de tour voulue en changeant la durée du while (ici changé le 2), le nombre de tour actuelle est de 3
            {
                for (int i= 0; i < nbjoueur; i++) //ici commence un tour par joueur
                {
                    Console.WriteLine("C'est au tour de " + tabjoueur[i].LeNom  + " de jouer");
                    Plateau P1 = new Plateau("Des.txt");// permet de créér un nouveau plateau a chaque tour
                    Console.WriteLine(P1.Tostringplateau());//permet d'afficher le plateau au joueur
                    for (int essai = 0; essai < 3; essai++)//ici on peut changer le nombre d'essai de mot trouvée en changeant le nombre d'essai (ici 3)
                    {
                        Console.WriteLine("Saisissez un nouveau mot trouvé");
                        string mot = Convert.ToString(Console.ReadLine());
                        int fin = dico.Fin(mot); //pour pouvoir rentrer la fin dans la fonction rechdicorecursif
                        bool Dansledico = dico.RechDichoRecursif(0, fin, mot);
                        bool DejaDit = tabjoueur[i].Contain(mot);
                        if((Dansledico == true) && (DejaDit == false))//on vérifie les conditions (rechdichorecursif et contain) avant d'ajouter le mot dans le tableau de mot trouvé et d'ajouter les points
                        {                                             //il manque au dessus la condition "test_plateau" à vérifier car je n'ai pas réussi à la faire
                            tabjoueur[i].Add_Mot(mot);
                        }
                    }
                    Console.WriteLine(tabjoueur[i].ToooString());//permet d'afficher le score du joueur avec les mots trouvés                    
                }
                grandtour++;//permet de rajouter 1 au grandtour quand tous les joueurs ont effectué leur toursd
            }            
        }
    }
}
