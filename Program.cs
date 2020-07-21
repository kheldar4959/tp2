using ProjetLinq.BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TP2
{
    class Program
    {

        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }
        static void Main(string[] args)
        {
            InitialiserDatas();


            //Affiche les titres de tous les livres triés par ordre alphabétique
            Console.WriteLine(" \n Collection des livres triés par ordre alphabétique");
            var bibliothèque = ListeLivres.OrderBy(l => l.Titre);
            foreach (var livre in bibliothèque)
            {
                Console.WriteLine($" \n Titre : {livre.Titre}");
            };


            //Affiche le titre du livre avec le plus de pages
            var maxiLivre = ListeLivres.OrderByDescending(l => l.NbPages).FirstOrDefault();
            Console.WriteLine($" \n Le titre du livre avec le plus de pages : { maxiLivre.Titre}");

            //Affiche les auteurs et la liste de leurs livres
            Console.WriteLine(" \n Livres disponibles triés par auteur");

            var livresparAuteur = ListeLivres.GroupBy(l => l.Auteur);
            foreach (var livres in livresparAuteur)
            {
                Console.WriteLine($" \n Auteur : {livres.Key.Prenom} {livres.Key.Nom} \n\n Titre associé : \n ");
                foreach (var livre in livres)
                {
                    Console.WriteLine($" {livre.Titre}");
                }
            }

            //Affiche l’auteur ayant écrit le plus de livres         
            var topAuteur = ListeLivres.GroupBy(l => l.Auteur).OrderByDescending(g => g.Count()).FirstOrDefault().Key;
            Console.WriteLine($" \n\n L’auteur ayant écrit le plus de livres : { topAuteur.Nom} { topAuteur.Prenom}");


            //Affiche l’auteur ayant écrit le moins de livres
            var flopAuteur = ListeLivres.GroupBy(l => l.Auteur).OrderBy(g => g.Count()).FirstOrDefault().Key;
            Console.WriteLine($" \n L’auteur ayant écrit le moins de livres : { flopAuteur.Nom} { flopAuteur.Prenom}");


            //Affiche les auteurs dont le prénom commence par G
            Console.WriteLine($" \n\n Les auteurs ayant pour 1ère lettre du prénom G :");
            var auteurG = ListeAuteurs.Where(a => a.Nom.StartsWith("G")).Select(a => a.Prenom);
            foreach (var auteur in auteurG)
            {
                Console.WriteLine($" \n { auteur}"); ;
            }

            //Affiche le nombre moyen de pages par livre par auteur
            var nombrePageMoyen = ListeLivres.Average(l => l.NbPages);
            var nombrePageMoyenParAuteur = ListeLivres.OrderBy(l => l.Auteur);
            Console.WriteLine("\n\n Nombre de Page moyen par auteur : \n");
            foreach (var item in livresparAuteur)
            {
                Console.WriteLine($" {item.Key.Prenom} {item.Key.Nom} moyennes des pages={item.Average(l => l.NbPages)} \n");
            }

            //Affiche la liste des livres dont le nombre de pages est supérieurà la moyenne
            var nombrePageMoyen2 = ListeLivres.Average(l => l.NbPages);
            Console.WriteLine($" \n\n Le nombre moyen sur l'ensemble des livres est de : {nombrePageMoyen2} \n");
            var supérieurMoyenne = ListeLivres.Where(nb => nb.NbPages > nombrePageMoyen2);
            Console.WriteLine($" Bouquin king size : \n ");
            foreach (var nbPages in supérieurMoyenne)
            {
                Console.WriteLine($" \n {nbPages.Titre} ");
            }

            //Combien ont gagné les auteurs en moyenne
            var moyenne = ListeAuteurs.Average(a => a.Factures.Sum(f => f.Montant));
            Console.WriteLine("Combien ont gagné les auteurs en moyenne");
            Console.WriteLine(moyenne);


            Console.ReadKey();
        }
        //Méthodes non fonctionnelles :

        //pour afficher les auteurs et la liste de leurs livres
        /*var listeParAuteur = ListeLivres.OrderBy(l => l.Auteur);
         foreach (var livre in listeParAuteur)
         {
             Console.WriteLine($" \n Auteur : {livre.Auteur}");
             foreach (var livres in listeParAuteur)
             {
                 Console.WriteLine($" \n Titre : {livre.Titre}");
             }
         };*/

        //Affiche l’auteur ayant écrit le plus de livres
        //Livre topAuteur = ListeLivres.GroupBy(l => l.Auteur).OrderByDescending(l => l.Auteur).FirstOrDefault();
        //Console.WriteLine(topAuteur);
    }

}

