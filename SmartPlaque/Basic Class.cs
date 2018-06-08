using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlaque
{
    class Liquide

    {
        private string nomLiquide;
        private int degreEbullition;
        private double coefficiantEbulition;
        private double temperatureLiquide;

        public Liquide(){}  // Constructeur par défaut laissé vide pour une question de normes

        public Liquide(string l_nomLiquide, int l_degreEbullition, double l_coefficiantEbulition)
        {
            nomLiquide = l_nomLiquide;
            degreEbullition = l_degreEbullition;
            coefficiantEbulition = l_coefficiantEbulition;
            temperatureLiquide = 0;
        }


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


        public void set_temperatureDuLiquide(double l_temperature)
        {
            temperatureLiquide = l_temperature;
        }

        public void set_nom_liquide(string nom)
        {
            nomLiquide = nom;
        }


        public void afficheur_liquide()
        {
            Console.WriteLine(" Nom du liquide : " + nomLiquide + " qui bout  a : " + degreEbullition.ToString() + " degré(s)\n");
        }
    };

    class Recipient
    {
        private string nomRecipient;
        private int capaciteMax;
        private string matiere;
        private double volumeActuel;
        private Liquide liquideContenu;

        public Recipient() { }  // Constructeur par défaut laissé vide pour une question de normes

        public Recipient(string r_nomRecipient, int r_capaciteMax, string r_matiere)
        {
            matiere = r_matiere;
            nomRecipient = r_nomRecipient;
            capaciteMax = r_capaciteMax;
            volumeActuel = 0;
            liquideContenu = null;
        }


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


        public void set_volumeActuel(double r_volumeActuel)
        {
            volumeActuel = r_volumeActuel;
        }

        public void set_temperatureLiquideContenu(double temperature)
        {
            liquideContenu.set_temperatureDuLiquide(temperature);
        }

        public void set_remplir(Liquide liquide, double r_quantite)
        {
            liquideContenu = liquide;
            volumeActuel = r_quantite;
        }

        public void set_volumeActuelDeborder()
        {
            volumeActuel = capaciteMax;
        }


        public void afficheur_recipient()
        {
            Console.WriteLine("C'est un recipient " + nomRecipient + " d\' une capacite de " + capaciteMax.ToString() + " cL, il est fait en " + matiere + "\n");

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

        public Feu(){} // Constructeur par défaut laissé vide pour une question de normes

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

        public int get_chrono_debut()
        {
            return 0; //  chrono_debut;
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

        public void set_PutOnFire(Recipient r_recipient)
        {
            recipient = r_recipient;
            alumage_feu();
        }


        public void alumage_feu()
        {
            compteur = Environment.TickCount;
            chrono_debut = compteur;
            etat = true;
        }

        public void chauffe_feu(int temperature_souhaiter)
        {
            compteur = Environment.TickCount;
            chrono_actuelle = compteur;
            int calcul = chrono_actuelle - chrono_debut;
            degreCourant = vitesseChauffe * calcul;
            chrono_actuelle = compteur - chrono_debut;
        }

        public void maintenirFeu(double temperature)
        {
            temperature += recipient.get_coefficientLiquideContenut() / recipient.get_volumeActuel();
            recipient.set_temperatureLiquideContenu(temperature);

            if (recipient.get_temperatureLiquideContenu() > recipient.get_temperaturEbulitionLiquide())
            {
                temperature = recipient.get_temperaturEbulitionLiquide();
                recipient.set_temperatureLiquideContenu(temperature);
            }

            System.Threading.Thread.Sleep(1000);
        }


        public void affiche_feu()
        {
            Console.WriteLine("Le feu {0} de la marque {0}", modele, marque);
            Console.WriteLine("\nCe feu est fait en {0} ", matiere);
            Console.WriteLine("\nCe feu a une vitesse de chauffe de {0} degres par seconde \n", vitesseChauffe);
            Console.WriteLine("\nLe feu est actuellement a {0} degres \n", degreCourant);

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
   
}
