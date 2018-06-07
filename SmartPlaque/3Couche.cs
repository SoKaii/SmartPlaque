using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;

namespace SmartPlaque
{

    class DAO
    {
        private string[] tabLiquide = new string[80]; // Création d'un tableau de string qui receptionnera les liquides
        private string[] tabRecipient = new string[80]; // Création d'un tableau de string qui receptionnera les recipients
        private string[] tabFeu = new string[80]; // Création d'un tableau de string qui receptionnera les feux
        private string temp; // Création d'un string temporaire pour effectuer le transfert entre la DB et les tableaux 
        private int index = 0; // Création d'un index permettant de naviguer dans les tableaux afin de les remplir 
        private string connexion_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SampledB.mdf;Integrated Security=True"; // Création d'un string permettant d'ouvrir la dB avec des parametres prédéfinis 
        private string tempName; // Création d'un string permettant de trim le nom avant de le stocker dans le tableau 
        private string tempMatiere; // Création d'un string permettant de trim la matiere avant de la stocker dans le tableaus  

        public DAO()
        {
            try
            {
                SqlConnection MyConnection = new SqlConnection(connexion_string); // Ouverture d'une connexion à la dB avec la connexion_string en parametres
                MyConnection.Open(); // Activation de la connexion

                SqlCommand cmdLiquide = new SqlCommand("Select * from Liquide"); // Création de la requete SQL permettant de récuperer tous les liquides stockés dans la dB
                cmdLiquide.Connection = MyConnection; // Lancement de la requete
                SqlDataReader readerL = cmdLiquide.ExecuteReader();  // Récupération des résultats de la requete dans un Reader 

                while (readerL.Read()) // Tant que le reader voit quelque chose 
                {
                    tempName = String.Format("{0}", readerL[1]); // tempName recupere le nom dans le resultat de la requete
                    tempName = tempName.Trim(); // trim de tempName permettant de supprimer les espaces inutiles

                    temp = String.Format("{1} {2} {3}", readerL[0], tempName, readerL[2], readerL[3]); // temp recupere le resultat de la requete
                    tabLiquide[index] = temp; // remplissage du tableau final avec temp 
                    index++; // Incrémentation de l'index permettant de naviguer dans le tableau
                }
                index = 0; // Remise a 0 de l'index afin de pouvoir le réutiliser 
                readerL.Close(); // Fermeture du Reader et donc de la requete 


                SqlCommand cmdRecipient = new SqlCommand("Select * from Recipient"); // Création de la requete SQL permettant de récuperer tous les recipients stockés dans la dB
                cmdRecipient.Connection = MyConnection; // Lancement de la requete
                SqlDataReader readerR = cmdRecipient.ExecuteReader();  // Récupération des résultats de la requete dans un Reader 

                while (readerR.Read()) // Tant que le reader voit quelque chose 
                {
                    tempName = String.Format("{0}", readerR[1]); // tempName recupere le nom dans le resultat de la requete
                    tempName = tempName.Trim(); // trim de tempName permettant de supprimer les espaces inutiles

                    temp = String.Format("{1} {2} {3}", readerR[0], tempName, readerR[2], readerR[3]); // temp recupere le resultat de la requete
                    tabRecipient[index] = temp; // remplissage du tableau final avec temp 
                    index++; // Incrémentation de l'index permettant de naviguer dans le tableau
                }
                index = 0; // Remise a 0 de l'index afin de pouvoir le réutiliser 
                readerR.Close(); // Fermeture du Reader et donc de la requete 


                SqlCommand cmdFeu = new SqlCommand("Select * from Feu"); // Création de la requete SQL permettant de récuperer tous les feux stockés dans la dB
                cmdFeu.Connection = MyConnection; // Lancement de la requete
                SqlDataReader readerF = cmdFeu.ExecuteReader();  // Récupération des résultats de la requete dans un Reader 

                while (readerF.Read()) // Tant que le reader voit quelque chose 
                {
                    tempName = String.Format("{0}", readerF[1]); // tempName recupere le nom dans le resultat de la requete
                    tempName = tempName.Trim(); // trim de tempName permettant de supprimer les espaces inutiles
                    tempMatiere = String.Format("{0}", readerF[2]); // tempMatiere recupere le nom dans le resultat de la requete
                    tempMatiere = tempMatiere.Trim(); // trim de tempMatiere permettant de supprimer les espaces inutiles

                    temp = String.Format("{1} {2} {3} {4}", readerF[0], tempName, tempMatiere, readerF[3],readerF[4]); // temp recupere le resultat de la requete
                    tabFeu[index] = temp; // remplissage du tableau final avec temp 
                    index++; // Incrémentation de l'index permettant de naviguer dans le tableau
                }
                index = 0; // Remise a 0 de l'index afin de pouvoir le réutiliser 
                readerF.Close(); // Fermeture du Reader et donc de la requete 
            }
            catch (Exception e) // Si l'ouverture de la dB est impossible 
            {
                Console.WriteLine(e.Message); // Afficher le message d'erreur renvoyé 
                RecupViaTxt(); // Lancer la récupération via les fichiers locaux 
            }
        }

        public void RecupViaTxt()
        {
            string[] p_path = new string[3]; // Création d'un tableau qui contiendra les chemins d'accès aux fichiers
            p_path[0] = "Liquides.txt"; // Configuration du tableau 
            p_path[1] = "Recipients.txt"; // Configuration du tableau 
            p_path[2] = "Feux.txt"; // Configuration du tableau 

            Console.WriteLine("\n TENTATIVE DE RECUPERATION VIA LES FICHIERS LOCAUX\n");

            try
            {
                StreamReader dao = new StreamReader(p_path[0]); // Ouverture du fichier via un StreamReader que l'on nomme dao

                if (dao != null) // Si le fichier contient quelque chose
                {
                    temp = dao.ReadLine(); // temp prend la valeur de la première ligne

                    while (temp != null) // Tant que temp contient quelque chose
                    {
                        tabLiquide[index] = temp; // Remplissage du tableau de liquide avec le contenu de Temp 
                        temp = dao.ReadLine(); // temp prend la valeur de la ligne suivante 
                        index++; // Incrémentation de l'index afin de pointer sur la case suivante du tableau 
                    }
                }
                dao.Close(); // Fermeture du fichier
            }
            catch (Exception e) // Si le fichier ne s'ouvre pas / mal 
            {
                Console.WriteLine(e.Message); // Affichage du message d'erreur 
            }


            try
            {
                StreamReader dao = new StreamReader(p_path[1]); // Ouverture du fichier via un StreamReader que l'on nomme dao

                if (dao != null) // Si le fichier contient quelque chose
                {
                    temp = dao.ReadLine(); // temp prend la valeur de la premiere ligne 

                    while (temp != null) // Tant que temp contient quelque chose 
                    {
                        tabRecipient[index] = temp; // Remplissage du contenu de Recipient avec le contenu de Temp 
                        temp = dao.ReadLine(); // temp prend la valeur de la ligne suivante 
                        index++; // Incrémentation de l'index afin de pointer sur la case suivante du tableau 
                    }
                }
                dao.Close(); // Fermeture du fichier 
            }
            catch (Exception e) // Si le fichier ne s'ouvre pas / mal 
            {
                Console.WriteLine(e.Message); // Affichage du message d'erreur 
            }

            try
            {
                StreamReader dao = new StreamReader(p_path[2]); // Ouverture du fichier via un StreamReader que l'on nomme dao

                if (dao != null) // Si le fichier contient quelque chose
                {
                    temp = dao.ReadLine(); // temp prend la valeur de la premiere ligne 

                    while (temp != null) // Tant que temp contient quelque chose 
                    {
                        tabFeu[index] = temp; // Remplissage du contenu de Recipient avec le contenu de Temp 
                        temp = dao.ReadLine(); // temp prend la valeur de la ligne suivante 
                        index++; // Incrémentation de l'index afin de pointer sur la case suivante du tableau 
                    }
                }
                dao.Close(); // Fermeture du fichier 
            }
            catch (Exception e) // Si le fichier ne s'ouvre pas / mal 
            {
                Console.WriteLine(e.Message); // Affichage du message d'erreur 
            }
        }

        public string[] getTabLiquide() // Permet de récuperer le tableau de Liquides
        {
            return tabLiquide;
        }

        public string[] getTabRecipient() // Permet de récuperer le tableau de Recipients 
        {
            return tabRecipient;
        }

        public string[] getTabFeu() // Permet de récuperer le tableau de Feux 
        {
            return tabFeu;
        }
    }

    class Lancement

    {
        private Feu a_feu;
        private Liquide a_liquide;
        private Recipient a_recipient;
        private string[] tabLiquide;
        private string[] tabRecipient;
        private string[] tabFeu;


        public Lancement()
        {
            DAO load = new DAO();
            tabRecipient = load.getTabRecipient();
            tabLiquide = load.getTabLiquide();
            tabFeu = load.getTabFeu();
        }

        public Lancement(string[] listeRecipient, string[] listeLiquide, string[] listeFeu)
        {
            DAO load = new DAO();

            tabRecipient = load.getTabRecipient();
            listeRecipient = load.getTabRecipient();

            tabLiquide = load.getTabLiquide();
            listeLiquide = load.getTabLiquide();

            tabFeu = load.getTabFeu();
            listeFeu = load.getTabFeu();
        }

        public void Activation(int p_indiceRecipient, int p_indiceLiquide, int p_indiceFeu)  // Ajout du JL du 28/05/2018
        {
            recupereLiquide(p_indiceLiquide);
            recupereFeu(p_indiceFeu);
            recupereRecipient(p_indiceRecipient);

            comparaison();
        }

        private void comparaison()
        {
            string matiereFeu = a_feu.get_matiereFeu().Trim();
            string matiereRecipient = a_recipient.get_matiereRecipient().Trim();

            if (matiereFeu == matiereRecipient)
            {
                lancementChauffe();
            }
            else
            {
                Console.WriteLine("Le feu et le recipient ne sont pas de meme matiere, lancement impossible");
            }
        }

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

            string[] tabSFeu = new string[4];
            tabSFeu = tabFeu[choixFeu].Split(delimiteur);

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

            a_feu.set_PutOnFire(a_recipient);
            a_feu.affiche_feu();

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

            if (a_feu.get_degreCourant() > a_recipient.get_temperaturEbulitionLiquide())
            {
                difference = a_feu.get_degreCourant() - a_recipient.get_temperaturEbulitionLiquide();
                Console.WriteLine("La temperature va baisser de: {0} \n", difference);
                a_feu.set_degreCourant(difference);
            }
            a_feu.affiche_feu();

            double temperature = a_recipient.get_temperatureLiquideContenu();

            while (a_recipient.get_temperatureLiquideContenu() < a_recipient.get_temperaturEbulitionLiquide())
            {
                a_feu.maintenirFeu(temperature);
                temperature = a_recipient.get_temperatureLiquideContenu();
                Console.WriteLine("La temperature du liquide est de {0} degrés \n", a_recipient.get_temperatureLiquideContenu());
            }

            double diminution = a_recipient.get_volumeActuel();
            while (a_recipient.get_volumeActuel() > 0)
            {
                diminution = a_recipient.get_volumeActuel() - (a_recipient.get_coefficientLiquideContenut() / (a_recipient.get_coefficientLiquideContenut() - 1));
                a_recipient.set_volumeActuel(diminution);

                if (a_recipient.get_volumeActuel() < 0)
                {
                    a_recipient.set_volumeActuel(0);
                }

                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Le recipient contient actuellement {0}  cl \n", a_recipient.get_volumeActuel());
            }
            Console.ReadLine();
        }
    };

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

        public IHM()
        {
            experiance = new Lancement();

            recipients = experiance.get_tabRecipient();
            liquides = experiance.get_tabLiquide();
            feux = experiance.get_tabFeu();

            afficheListeRecipient();
            demanderRecipient();

            afficheListeLiquide();
            demanderLiquide();

            afficheListeFeu();
            demandeFeu();

            demandeVersement();

            experiance.affectationChoix(recipnumb, liqpnumb, feunumb, Qte_Liq);


        }

        private void demanderRecipient()
        {
            Console.WriteLine("Inscrivez le numéro du récipient choisis :\n ");
            recipnumb = Int32.Parse(Console.ReadLine());

            recipientSelectionner = recipients[recipnumb].ToString().Split(';');
            nomRecipientSelectionner = recipientSelectionner[0];

            Console.WriteLine("Vous avez choisi un/une " + nomRecipientSelectionner); // Ajout JL du 28/05/2018
        }

        private void afficheListeRecipient()
        {
            int i = 0;

            while (recipients[i] != null)
            {

                string[] chainedRecipients = recipients[i].ToString().Split(';');
                string nomRecipient = chainedRecipients[0];
                string capaciteMax = chainedRecipients[1];
                string matiereRecipient = chainedRecipients[2];

                Console.WriteLine(i + ") " + nomRecipient + ", de capacité max : " + capaciteMax + ", " + matiereRecipient);

                i = i + 1;
            }
        }

        private void afficheListeLiquide()
        {
            int i = 0;

            while (liquides[i] != null)
            {

                string[] chainedLiquides = liquides[i].ToString().Split(';');
                string nomLiquide = chainedLiquides[0];
                string tempEbu = chainedLiquides[1];

                coefchauf = chainedLiquides[2];

                Console.WriteLine(i + ") " + nomLiquide + ", Qui possède une température d'ébullition de : " + tempEbu + ", Coefficient de chauffe : " + coefchauf);
                i = i + 1;
            }
        }

        private void demanderLiquide()
        {
            Console.WriteLine();
            Console.WriteLine("Inscrivez le numéro du Liquide choisis :\n ");
            liqpnumb = Int32.Parse(Console.ReadLine());

            liquideSelectionner = liquides[liqpnumb].ToString().Split(';');
            nomLiquideSelectionner = liquideSelectionner[0];

            Console.WriteLine("Vous avez choisi de " + nomLiquideSelectionner);
        }

        private void afficheListeFeu()
        {
            int i = 0;

            while (feux[i] != null)
            {

                chainedFeu = feux[i].ToString().Split(';');
                nomFeu = chainedFeu[0];
                model = chainedFeu[1];
                marque = chainedFeu[2];

                Console.WriteLine(i + ") " + nomFeu + ", " + model + ", de la marque :" + marque);

                i = i + 1;
            }
        }

        private void demandeFeu()
        {
            int i = 0;
            Console.WriteLine();
            Console.WriteLine("Inscrivez le numéro de la Plaque choisis :\n ");
            feunumb = Int32.Parse(Console.ReadLine());
            Console.WriteLine("\n\n");

            string[] feuSelectionner = feux[i].ToString().Split(';');
            nomFeuSelectionner = feuSelectionner[0];
            model = feuSelectionner[1];

            Console.WriteLine("Vous avez choisi la plaque qui porte la reference " + nomFeuSelectionner);
        }

        private void demandeVersement()
        {
            Console.WriteLine();
            Console.WriteLine("Combien de liquide voulez vous versez ? : \n");
            Qte_Liq = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Vous avez versez : {" + Qte_Liq + "} cl ");
        }
    }
}