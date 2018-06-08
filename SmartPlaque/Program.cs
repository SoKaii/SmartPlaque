﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace std
{
<<<<<<< HEAD
    static class Constants
    {
        public const Char ExpActif = '0';
        public const Char Comparaison = '1';
        public const Char feuAllumer = '2';
        public const Char Phase = '3';
        public const Char evaporation = '4';
        public const Char ExpTerminer = '5';
    }

    class DAO
    {
        string[] line = new string[80];
        string temp;
        int index = 0;
        int nbrLignes = 0;

        public DAO(string p_path)
        {
            StreamReader dao = new StreamReader(p_path);

            if (dao != null)
            {
                temp = dao.ReadLine();

                while (temp != null)
                {
                    line[index] = temp;
                    temp = dao.ReadLine();
                    nbrLignes++;
                    index++;
                }
            }
            else
                Console.WriteLine("ERROR 100 : Impossible d'ouvrir le fichier");
            dao.Close();
        }

        public string[] getTab()
        {
            return line;
        }
    }

    class Liquide

    {



        //*** attributs de liquide ***//

        private string nomLiquide;

        private int degreEbullition;

        private double coefficiantEbulition;

        private double temperatureLiquide;





        //*** constructeur de liquide ***//

        public Liquide() { }  // Constructeur par défaut



        public Liquide(string l_nomLiquide, int l_degreEbullition, double l_coefficiantEbulition)

        {

            nomLiquide = l_nomLiquide;

            degreEbullition = l_degreEbullition;

            coefficiantEbulition = l_coefficiantEbulition;

            temperatureLiquide = 0;

        }



        //*** liste des get de liquide ***//



        public double get_temperatureLiquide()

        {

            return temperatureLiquide;

        }



        public string get_nomLiquide()

        {

            return nomLiquide;

        }



        public double get_coefficientEbulition()

        {

            return coefficiantEbulition;

        }



        public int get_degreEbullition()

        {

            return degreEbullition;

        }



        //*** liste des set de liquide ***//



        public void set_temperatureDuLiquide(double l_temperature)

        {

            temperatureLiquide = l_temperature;

        }



        public void set_nom_liquide(string nom)

        {

            nomLiquide = nom;

        }



        //*** afficheur de liquide ***//



        public void afficheur_liquide()

        {

            Console.WriteLine(" Nom du liquide : " + nomLiquide + " qui bouille  a : " + degreEbullition.ToString() + " degré(s)\n");

        }



    };





    class Recipient

    {



        //*** attributs de Recipient ***//



        private string nomRecipient;

        private int capaciteMax;

        private string matiere;

        private double volumeActuel;

        private Liquide liquideContenu;



        //*** constructeurs de Recipient ***//



        public Recipient() { }  // Constructeur par défaut.



        public Recipient(string r_nomRecipient, int r_capaciteMax, string r_matiere)

        {

            matiere = r_matiere;

            nomRecipient = r_nomRecipient;

            capaciteMax = r_capaciteMax;

            volumeActuel = 0;

            liquideContenu = null;

        }



        //*** liste des get de recipient ***//



        public double get_volumeActuel()

        {

            return volumeActuel;

        }



        public int get_capaciteMax()

        {

            return capaciteMax;

        }



        public string get_matiereRecipient()

        {

            return matiere;

        }



        public string get_nomRecipient()

        {

            return nomRecipient;

        }



        //* liste des get du liquideContenu *//



        public double get_temperatureLiquideContenu()

        {

            return liquideContenu.get_temperatureLiquide();

        }



        public string get_nomLiquideContenu()

        {

            return liquideContenu.get_nomLiquide();

        }



        public double get_coefficientLiquideContenut()

        {

            return liquideContenu.get_coefficientEbulition();

        }



        public int get_temperaturEbulitionLiquide()

        {

            return liquideContenu.get_degreEbullition();

        }



        //*** liste des set de recipient ***//



        public void set_volumeActuel(double r_volumeActuel)

        {

            volumeActuel = r_volumeActuel;

        }



        public void set_temperatureLiquideContenu(double temperature)

        {

            liquideContenu.set_temperatureDuLiquide(temperature);

        }



        //* set remplir un recipient d'un liquide *//



        public void set_remplir(Liquide liquide, double r_quantite)

        {

            liquideContenu = liquide;

            volumeActuel = r_quantite;

        }



        //* set en cas de debordement *//



        public void set_volumeActuelDeborder()

        {

            volumeActuel = capaciteMax;

        }



        //*** afficheur de recipient ***//



        public void afficheur_recipient()

        {

            Console.WriteLine("C'est un recipient " + nomRecipient + " d\' une capacité de " + capaciteMax.ToString() + " ,il est fais en " + matiere + "\n");



            if (volumeActuel == 0)

            {

                Console.WriteLine("Le recipient est vide");

            }

            else

            {

                Console.WriteLine("Le recipient contient: " + volumeActuel.ToString() + "cl\n");

                liquideContenu.afficheur_liquide();



            }

        }



    };





    class Feu

    {



        //*** attributs de Feu ***//

        private string marque;
        private string matiere;
        private string modele;
        private int vitesseChauffe;
        private int degreCourant;
        private bool etat;
        private int chrono_debut;
        private int chrono_actuelle;
        private int compteur;
        private Recipient recipient;

        //*** constructeur de Feu ***//

        public Feu() { }


        public Feu(string f_marque, string f_matiere, string f_modele, int r_vitesseChauffe)
        {
            marque = f_marque;
            matiere = f_matiere;
            modele = f_modele;
            vitesseChauffe = r_vitesseChauffe;
            degreCourant = 0;
            etat = false;
            chrono_debut = Environment.TickCount;
            chrono_actuelle = 0;
            recipient = null;
        }


        //*** liste des get de Feu ***//

        public int get_chrono_debut()
        { return 0; //  chrono_debut;

        }

        public int get_chrono_actuelle()
        {

            return 0; // chrono_actuelle;

        }



        public int get_degreCourant()

        {

            return degreCourant;

        }



        public string get_matiereFeu()

        {

            return matiere;

        }



        //*** liste des set de Feu ***//



        public void set_degreCourant(int tempFeu)

        {

            degreCourant = degreCourant - tempFeu;

        }

        public void set_volumeAcutuelFeu(double f_volume)

        {

            recipient.set_volumeActuel(f_volume);

        }





        public void set_chronoActuelle()

        {

            compteur = Environment.TickCount;

            chrono_actuelle = compteur - chrono_debut;

        }



        //* set mettre un recipient sur le feu *//



        public void set_PutOnFire(Recipient r_recipient)

        {

            recipient = r_recipient;

            alumage_feu();

        }



        //* allumage du feu *//



        public void alumage_feu()

        {

            compteur = Environment.TickCount;

            chrono_debut = compteur;

            etat = true;

        }



        //* augmentation de la temperature de la plaque *//



        public void chauffe_feu(int temperature_souhaiter)

        {

            compteur = Environment.TickCount;

            chrono_actuelle = compteur;



            int calcul = chrono_actuelle - chrono_debut;

            degreCourant = vitesseChauffe * calcul;

            chrono_actuelle = compteur - chrono_debut;



        }



        //*** maintient de la temperature jusqua ebulition ***//



        public void maintenirFeu(double temperature)

        {

            //mise en ebulition du liquide



            temperature += recipient.get_coefficientLiquideContenut() / recipient.get_volumeActuel();

            recipient.set_temperatureLiquideContenu(temperature);

            //pour eviter que la temperature depace celle dï¿½bulition

            if (recipient.get_temperatureLiquideContenu() > recipient.get_temperaturEbulitionLiquide())

            {

                temperature = recipient.get_temperaturEbulitionLiquide();

                recipient.set_temperatureLiquideContenu(temperature);

            }

            // compteur = time(&compteur);

            System.Threading.Thread.Sleep(1000);

        }



        //*** afficheur de feu ***/



        public void affiche_feu()

        {

            Console.WriteLine("Le feu {0}-{0}", marque, modele);
            Console.WriteLine("\nCe feu est fait en {0} ", matiere);
            Console.WriteLine("\nCe feu a une vitesse de chauffe de {0} \n", vitesseChauffe);
            //Console.WriteLine("\nLe chrono est lance depuis %g secondes\n", chrono_actuelle - chrono_debut);
            Console.WriteLine("\nLe feu a une temperature actuelle de :{0} \n", degreCourant);

            if (etat)

            {
                Console.WriteLine("Le feu est allume");
            }
            else
            {
                Console.WriteLine("Le feu est eteint \n");
            }
            if (recipient != null)
            {
                Console.WriteLine("Un recipient est sur le feu \n");
                recipient.afficheur_recipient();
            }
            else
            {
                Console.WriteLine("Le feu est vide \n");
            }
        }
    };





    class Lancement

    {
        private Feu a_feu;
        private Liquide a_liquide;
        private Recipient a_recipient;
        private string[] tabLiquide;
        private string[] tabRecipient;
        private string[] tabFeu;
        private string message;
        private char statut;

        //*** Pas dattributs ***//
        //*** Constructeur vide ***//
        //public Lancement() { }
        //*** Constructeur surchargé ***//

        public Lancement()
        {
            string path = "C:\\temp\\f1.txt";   // A reprendre !!!
            DAO daoLiquide = new DAO(path);
            tabLiquide = daoLiquide.getTab();

            path = "C:\\temp\\recipient.txt";
            DAO daoRecipient = new DAO(path);
            tabRecipient = daoRecipient.getTab();

            path = "C:\\temp\\feu.txt";
            DAO daoFeu = new DAO(path);
            tabFeu = daoFeu.getTab();
        }

        public Lancement(string[] listeRecipient, string[] listeLiquide, string[] listeFeu)

        {
            string path = "C:\\temp\\f1.txt";
            DAO daoLiquide = new DAO(path);
            tabLiquide = daoLiquide.getTab();
            listeLiquide = daoLiquide.getTab();

            path = "C:\\temp\\recipient.txt";
            DAO daoRecipient = new DAO(path);
            tabRecipient = daoRecipient.getTab();
            listeRecipient = daoRecipient.getTab();

            path = "C:\\temp\\feu.txt";
            DAO daoFeu = new DAO(path);
            tabFeu = daoFeu.getTab();
            listeFeu = daoFeu.getTab();
        }

        public void Activation(int p_indiceRecipient, int p_indiceLiquide, int p_indiceFeu)  // Ajout du JL du 28/05/2018
        {
            recupereLiquide(p_indiceLiquide);
            recupereFeu(p_indiceFeu);
            recupereRecipient(p_indiceRecipient);
            comparaison();
        }
        //*** comparaison de la matiere du recipient et de celui du feu ***//



        private void comparaison()

        {
            string matiereFeu = a_feu.get_matiereFeu();
            string matiereRecipient = a_recipient.get_matiereRecipient().Trim();

            if (matiereFeu == matiereRecipient)

            {


                lancementChauffe();

            }

            else

            {

                message = "Incompatibilité détectée.";
                statut = Constants.Comparaison;
            }
        }



        //*** tout le processus de chauffe apres la comparaison des materiaux ***//
        private void recupereLiquide(int choixLiquide)
        {
            int i = 0;
            string nom;
            char delimiteur = ';';

            string[] tabSLiquide = tabLiquide[choixLiquide].Split(delimiteur);
            int degre;
            double coefficientEbulition;

            nom = tabSLiquide[0];
            degre = int.Parse(tabSLiquide[1]);
            coefficientEbulition = double.Parse(tabSLiquide[2]);

            Liquide liquide = new Liquide(nom, degre, coefficientEbulition);
            i++;
            a_liquide = liquide;
        }

        public string[] get_tabLiquide()
        {
            return tabLiquide;
        }

        private void recupereFeu(int choixFeu)
        {
            int i = 0;
            int vitesse_chauffe;
            string marque;
            string matiereFeu;
            string modele;
            char delimiteur = ';';

            string[] tabSFeu = tabFeu[choixFeu].Split(delimiteur);

            marque = tabSFeu[0];
            matiereFeu = tabSFeu[1];
            modele = tabSFeu[2];
            vitesse_chauffe = int.Parse(tabSFeu[3]);

            Feu feu = new Feu(marque, matiereFeu, modele, vitesse_chauffe);
            i++;

            a_feu = feu;
        }
        public string[] get_tabFeu()
        {
            return tabFeu;
        }

        private void recupereRecipient(int choixRecipient)
        {
            int i = 0;
            string nom;
            int capaciteMax;
            string matiereRecipient;
            char delimiteur = ';';

            string[] tabSRecipient = tabRecipient[choixRecipient].Split(delimiteur);

            nom = tabSRecipient[0];
            capaciteMax = int.Parse(tabSRecipient[1]);
            matiereRecipient = tabSRecipient[2];

            Recipient recipient = new Recipient(nom, capaciteMax, matiereRecipient);
            i++;

            a_recipient = recipient;
        }

        // getTabRecipient -> camelCase
        // get_tab_recipient

        public string[] get_tabRecipient()
        {
            return tabRecipient;
        }

        public void affectationChoix(int p_choixRecipient, int p_choixLiquide, int p_choixFeu, int p_quantite)
        {
            recupereRecipient(p_choixRecipient);
            recupereLiquide(p_choixLiquide);
            recupereFeu(p_choixFeu);
            versement(p_quantite);
        }
        private void versement(int quantite)
        {
            a_recipient.set_remplir(a_liquide, quantite);
            comparaison();
        }

        public void lancementChauffe()

        {

            int difference;

            int condition = 0;

            //recipient sur feu et allumage du feu



            a_feu.set_PutOnFire(a_recipient);
            a_feu.affiche_feu();
            //augmentation de la temperature de la plaque jusqua temperature d'ï¿½bulition du liquideContenu
            while (a_feu.get_degreCourant() < a_recipient.get_temperaturEbulitionLiquide())
            {
                a_feu.chauffe_feu(a_liquide.get_degreEbullition());
                Console.WriteLine("La temperature actuelle de la plaque est de: {0}\n", condition);
                System.Threading.Thread.Sleep(1000);
                if (condition < a_feu.get_degreCourant())
                {
                    condition = a_feu.get_degreCourant();
                    Console.WriteLine("La temperature actuelle de la plaque est de: {0}\n", condition);
                }
            }

            //diminution de la temperature de la plaque si elle est superieur a celle demander
            if (a_feu.get_degreCourant() > a_recipient.get_temperaturEbulitionLiquide())
            {
                difference = a_feu.get_degreCourant() - a_recipient.get_temperaturEbulitionLiquide();
                Console.WriteLine("La temperature va baisser de: {0} \n", difference);
                a_feu.set_degreCourant(difference);

            }

            a_feu.affiche_feu();
            //mise en ebulition du liquide mais  non fonctionnel
            // time_t compteur;
            // srand(time(null));
            // compteur = time(&compteur);

            double temperature = a_recipient.get_temperatureLiquideContenu();
            //augmente la temperature du liquide jusqua ebulition
            while (a_recipient.get_temperatureLiquideContenu() < a_recipient.get_temperaturEbulitionLiquide())
            {
                a_feu.maintenirFeu(temperature);
                temperature = a_recipient.get_temperatureLiquideContenu();
                Console.WriteLine("La temperature du liquide est de {0} degrés \n", a_recipient.get_temperatureLiquideContenu());
            }


            //evaporation du liquide
            double diminution = a_recipient.get_volumeActuel();
            while (a_recipient.get_volumeActuel() > 0)
            {
                //le liquide perdra (coefficient / (coefficient -1) cl par seconde
                diminution = a_recipient.get_volumeActuel() - (a_recipient.get_coefficientLiquideContenut() / (a_recipient.get_coefficientLiquideContenut() - 1));
                a_recipient.set_volumeActuel(diminution);
                //pour eviter que la temperature depace celle dï¿½bulition
                if (a_recipient.get_volumeActuel() < 0)
                {
                    a_recipient.set_volumeActuel(0);
                }

                //compteur = time(&compteur);
                System.Threading.Thread.Sleep(1000);
                message = "Le recipient contient actuellement" + a_recipient.get_volumeActuel() + "\n";
            }
            Console.ReadLine();

        }
        public string get_message()
        {
            return message;
        }

        public char get_statut()
        {
            return statut;
        }
    };



    //--------------------------------------------------------------------------------------------------------  
    // classe interface homme machine
    class IHM
    {
        private string[] recipients;
        private string[] liquides;
        private string[] feux;
        private Lancement experiance;
        string nomFeuSelectionner;
        string marque;
        string model;
        string nomFeu;
        string[] chainedFeu;
        string nomLiquideSelectionner;
        string[] liquideSelectionner;
        string coefchauf;
        string nomRecipientSelectionner;
        string[] recipientSelectionner;
        int recipnumb;
        int liqpnumb;
        int feunumb;
        int Qte_Liq;
        private string affichage;

        //--------------------------------------------------------------------------------------------------------  

        // constructeur qui permet de creer une nouveau "gateau" avec le moule "IHM"
        public IHM()
        {

            experiance = new Lancement();

            recipients = experiance.get_tabRecipient();
            liquides = experiance.get_tabLiquide();
            feux = experiance.get_tabFeu();

            afficheListeRecipient();
            // demandeFeu();            // ERREUR de désignation méthode - 28/05/2017
            demanderRecipient();        // Ajout JL du 28/05/2018

            afficheListeLiquide();
            demanderLiquide();

            afficheListeFeu();
            demandeFeu();
            demandeVersement();
            // Ajout JL du 28/05/2018: Activation de l'expérience à ce niveau  :) !!!. La question est de savoir à partir de quelle méthode...(fournie par Ewan)
            experiance.affectationChoix(recipnumb, liqpnumb, feunumb, Qte_Liq);

            while (Constants.ExpActif != Constants.ExpTerminer)
            {
                System.Threading.Thread.Sleep(2000);
                affichage = experiance.get_message();
                Console.WriteLine(affichage);
                a_statut = experiance.get_statut();
                switch (a_statut)
                {

                }
            }


        }
        //--------------------------------------------------------------------------------------------------------  
        //création Fonction pour le choix du recipient
        private void demanderRecipient()
        {
            // int i = 0;
            Console.WriteLine();
            Console.WriteLine("Inscrivez le numéro du récipient choisis :\n ");
            recipnumb = Int32.Parse(Console.ReadLine());

            //Console.WriteLine("Vous avez choisi " + recipnumb);  Ajout JL du 28/05/2018
            // recupere le recipient choisi par rapport à son numero
            // recipientSelectionner = recipients[recipnumb].ToString().Split(';'); 
            // Console.WriteLine("Vous avez : " + recipients[recipnumb].ToString()); Ajout JL du 28/05/2018
            recipientSelectionner = recipients[recipnumb].ToString().Split(';');

            // creer une variable qui recupere a la premiere position du tableau le nom du recipient
            nomRecipientSelectionner = recipientSelectionner[0];

            // recipient choisis
            // Console.WriteLine("Vous avez choisi un/une " + recipientSelectionner);  // ERREUR de désignation de variable
            Console.WriteLine("Vous avez choisi un/une " + nomRecipientSelectionner); // Ajout JL du 28/05/2018
        }

        //--------------------------------------------------------------------------------------------------------      
        //Création fonction pour afficher chaque recepient
        private void afficheListeRecipient()
        {
            // creer une variable i qui stocke le nombre de tours
            int i = 0;

            // boucle qui recupere chaque element
            while (recipients[i] != null)
            {
                // creer une variable qui stocke la chaine entiere qui contient les informatins
                string[] chainedRecipients = recipients[i].ToString().Split(';');

                // creer une variable qui recupere a la premiere position du tableau le nom du recipient
                string nomRecipient = chainedRecipients[0];

                //Creation de variable la capmax
                string capaciteMax = chainedRecipients[1];

                //Creation de variable matiere
                string matiereRecipient = chainedRecipients[2];

                // affiche la valeur de la chaine
                Console.WriteLine(i + ") " + nomRecipient + ", de capacité max : " + capaciteMax + ", " + matiereRecipient);

                // on ajoute 1 à i pour chaque fin de tour de boucle
                i = i + 1;
            }

        }
        //--------------------------------------------------------------------------------------------------------  
        //Création fonction pour afficher chaque liquide
        private void afficheListeLiquide()
        {
            // creer une variable i qui stocke le nombre de tours
            int i = 0;

            // boucle qui recupere chaque element
            while (liquides[i] != null)
            {
                // creer une variable qui stocke la chaine entiere qui contient les informatins
                string[] chainedLiquides = liquides[i].ToString().Split(';');

                // creer une variable qui recupere a la premiere position du tableau le nom du recipient
                string nomLiquide = chainedLiquides[0];

                //Creation de variable la tempEbu
                string tempEbu = chainedLiquides[1];

                //Creation de variable coefchauf
                coefchauf = chainedLiquides[2];

                // affiche la valeur de la chaine
                Console.WriteLine(i + ") " + nomLiquide + ", Qui possède une température d'ébullition de : " + tempEbu + ", Coefficient de chauffe : " + coefchauf);

                i = i + 1;
            }

        }

        //--------------------------------------------------------------------------------------------------------  
        //création Fonction pour le choix du Liquide
        private void demanderLiquide()
        {
            Console.WriteLine();
            Console.WriteLine("Inscrivez le numéro du Liquide choisis :\n ");
            liqpnumb = Int32.Parse(Console.ReadLine());

            // recupere le recipient choisis par rapport à son numero
            liquideSelectionner = liquides[liqpnumb].ToString().Split(';');

            // creer une variable qui recupere a la premiere position du tableau le nom du recipient
            nomLiquideSelectionner = liquideSelectionner[0];

            // recipient choisis
            Console.WriteLine("Vous avez choisi de " + nomLiquideSelectionner);
        }
        //--------------------------------------------------------------------------------------------------------  
        //Création fonction pour afficher chaque feu
        private void afficheListeFeu()
        {
            // creer une variable i qui stocke le nombre de tours
            int i = 0;

            // boucle qui recupere chaque element
            while (feux[i] != null)
            {
                // creer une variable qui stocke la chaine entiere qui contient les informatins
                chainedFeu = feux[i].ToString().Split(';');

                // creer une variable qui recupere a la premiere position du tableau le nom du recipient
                nomFeu = chainedFeu[0];

                //Creation de variable la capmax
                model = chainedFeu[1];

                //Creation de variable matiere
                marque = chainedFeu[2];

                // affiche la valeur de la chaine
                Console.WriteLine(i + ") " + nomFeu + ", " + model + ", de la marque :" + marque);

                // on ajoute 1 à i pour chaque fin de tour de boucle
                i = i + 1;
            }

        }

        //--------------------------------------------------------------------------------------------------------
        //création Fonction pour le choix du Feu
        private void demandeFeu()
        {
            int i = 0;
            Console.WriteLine();
            Console.WriteLine("Inscrivez le numéro de la Plaque choisis :\n ");
            feunumb = Int32.Parse(Console.ReadLine());

            // recupere le recipient choisis par rapport à son numero
            string[] feuSelectionner = feux[feunumb].ToString().Split(';');

            // creer une variable qui recupere a la premiere position du tableau le nom du recipient
            nomFeuSelectionner = feuSelectionner[0];
            model = feuSelectionner[1];

            // recipient choisis
            Console.WriteLine("Vous avez choisis une " + model);
        }
        //--------------------------------------------------------------------------------------------------------  

        private void demandeVersement()
        {
            Console.WriteLine();
            Console.WriteLine("Combien de liquide voulez vous versez ? : \n");
            Qte_Liq = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Vous avez versez : {" + Qte_Liq + "} cl ");


        }
      
             
    }
    class Program

=======
class Program
>>>>>>> d65559aca74e6a16262953199088385a0d62117c
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            // int choixRecipient = 1; Retrait JL du 28 / 05 / 2018
            // int choixLiquide = 1; Retrait JL du 28 / 05 / 2018
            // int choixFeu = 1;  Retrait JL du 28 / 05 / 2018

            IHM Saisie;
            Saisie = new IHM();
            //   Lancement lancement = new Lancement();                                 Retrait JL du 28/05/2018
            //   lancement.affectationChoix(choixRecipient, choixLiquide, choixFeu);    Retrait JL du 28/05/2018
            //   lancement.versement();                                                 Retrait JL du 28/05/2018
            //    Console.ReadLine();                                                   Retrait JL du 28/05/2018
            // lancement.comparaison(*feu, *liquide, *recipient, *lancement);           Retrait JL du 28/05/2018

            //si la comparaison est bonne le lancement du precessus de chauffe se fais automatiquement

            Console.ReadLine();
=======
            SmartPlaque.IHM Saisie = new SmartPlaque.IHM();
>>>>>>> d65559aca74e6a16262953199088385a0d62117c
        }

    }
}
    