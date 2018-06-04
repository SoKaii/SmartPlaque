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
        private string[] tabLiquide = new string[80];
        private string[] tabRecipient = new string[80];
        private string[] tabFeu = new string[80];
        private string temp;
        private int index = 0;
        private int nbrLignes = 0;
        private string connexion_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SampledB.mdf;Integrated Security=True";
        private string tempName;
        private string tempMatiere;

        public DAO()
        {
            try
            {
                SqlConnection MyConnection = new SqlConnection(connexion_string);
                MyConnection.Open();

                SqlCommand cmdLiquide = new SqlCommand("Select * from Liquide");
                cmdLiquide.Connection = MyConnection;
                SqlDataReader readerL = cmdLiquide.ExecuteReader();  // Objet recevant les résultats (curseur de lecture comme la lecture de fichier)

                while (readerL.Read())
                {
                    tempName = String.Format("{0}", readerL[1]);
                    tempName = tempName.Trim();

                    temp = String.Format("{1} {2} {3}", readerL[0], tempName, readerL[2], readerL[3]);
                    tabLiquide[index] = temp;
                    index++;
                }
                index = 0;
                readerL.Close();


                SqlCommand cmdRecipient = new SqlCommand("Select * from Recipient");
                cmdRecipient.Connection = MyConnection;
                SqlDataReader readerR = cmdRecipient.ExecuteReader();  // Objet recevant les résultats (curseur de lecture comme la lecture de fichier)

                while (readerR.Read())
                {
                    tempName = String.Format("{0}", readerR[1]);
                    tempName = tempName.Trim();

                    temp = String.Format("{1} {2} {3}", readerR[0], tempName, readerR[2], readerR[3]);
                    tabRecipient[index] = temp;
                    index++;
                }
                index = 0;
                readerR.Close();


                SqlCommand cmdFeu = new SqlCommand("Select * from Feu");
                cmdFeu.Connection = MyConnection;
                SqlDataReader readerF = cmdFeu.ExecuteReader();  // Objet recevant les résultats (curseur de lecture comme la lecture de fichier)

                while (readerF.Read())
                {
                    tempName = String.Format("{0}", readerF[1]);
                    tempName = tempName.Trim();
                    tempMatiere = String.Format("{0}", readerF[2]);
                    tempMatiere = tempMatiere.Trim();

                    temp = String.Format("{1} {2} {3} {4}", readerF[0], tempName, tempMatiere, readerF[3],readerF[4]);
                    tabFeu[index] = temp;
                    index++;
                }
                index = 0;
                readerF.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RecupViaTxt();
            }
        }

        public void RecupViaTxt()
        {
            string[] p_path = new string[3];
            p_path[0] = "Liquides.txt";
            p_path[1] = "Recipients.txt";
            p_path[2] = "Feux.txt";

            Console.WriteLine("\n TENTATIVE DE RECUPERATION VIA LES FICHIERS LOCAUX\n");

            try
            {
                StreamReader dao = new StreamReader(p_path[0]);

                if (dao != null)
                {
                    temp = dao.ReadLine();

                    while (temp != null)
                    {
                        tabLiquide[index] = temp;
                        temp = dao.ReadLine();
                        nbrLignes++;
                        index++;
                    }
                }
                dao.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            try
            {
                StreamReader dao = new StreamReader(p_path[1]);

                if (dao != null)
                {
                    temp = dao.ReadLine();

                    while (temp != null)
                    {
                        tabRecipient[index] = temp;
                        temp = dao.ReadLine();
                        nbrLignes++;
                        index++;
                    }
                }
                dao.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                StreamReader dao = new StreamReader(p_path[2]);

                if (dao != null)
                {
                    temp = dao.ReadLine();

                    while (temp != null)
                    {
                        tabFeu[index] = temp;
                        temp = dao.ReadLine();
                        nbrLignes++;
                        index++;
                    }
                }
                dao.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string[] getTabLiquide()
        {
            return tabLiquide;
        }

        public string[] getTabRecipient()
        {
            return tabRecipient;
        }

        public string[] getTabFeu()
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