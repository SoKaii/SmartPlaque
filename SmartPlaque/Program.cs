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


namespace std
{
    class Program

    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)

        {
            // int choixRecipient = 1; Retrait JL du 28 / 05 / 2018
            // int choixLiquide = 1; Retrait JL du 28 / 05 / 2018
            // int choixFeu = 1;  Retrait JL du 28 / 05 / 2018

           SmartPlaque.IHM Saisie;
            Saisie = new SmartPlaque.IHM();
            //   Lancement lancement = new Lancement();                                 Retrait JL du 28/05/2018
            //   lancement.affectationChoix(choixRecipient, choixLiquide, choixFeu);    Retrait JL du 28/05/2018
            //   lancement.versement();                                                 Retrait JL du 28/05/2018
            //    Console.ReadLine();                                                   Retrait JL du 28/05/2018
            // lancement.comparaison(*feu, *liquide, *recipient, *lancement);           Retrait JL du 28/05/2018

            //si la comparaison est bonne le lancement du precessus de chauffe se fais automatiquement

            Console.ReadLine();
        }

    }
}
