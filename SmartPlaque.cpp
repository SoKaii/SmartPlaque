#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <unistd.h>

using namespace std;


class Liquide
{
private: string nomLiquide;
private: int degreEbullition;
private: double coefficiantEbulition;
private: float temperatureLiquide;


public: Liquide(string l_nomLiquide, int l_degreEbullition, double l_coefficiantEbulition)
{
	nomLiquide = l_nomLiquide;
	degreEbullition = l_degreEbullition;
	coefficiantEbulition = l_coefficiantEbulition;
	temperatureLiquide = 0;
};

public: float get_temperatureLiquide()
{
	return temperatureLiquide;
}

public: string get_nomLiquide()
{
	return nomLiquide;
};

public:int get_coefficientEbulition()
{
	return coefficiantEbulition;
}

public: int get_degreEbullition()
{
	return degreEbullition;
};

public: void set_temperatureDuLiquide(float l_temperature)
{
	temperatureLiquide = l_temperature;
};

public: void set_nom_liquide(string nom)
{
	nomLiquide = nom;
};

public: void afficheur_liquide()
{
	cout << nomLiquide <<  " qui bout a " << degreEbullition << endl;
};

};


class Recipient
{

private: string nomRecipient;
private: int capaciteMax;
private: string matiere;
private: float volumeActuel;
private: Liquide *liquideContenu;

public: Recipient(string r_nomRecipient, int r_capaciteMax, string r_matiere)
{
	matiere = r_matiere;
	nomRecipient = r_nomRecipient;
	capaciteMax = r_capaciteMax;
	volumeActuel = 0;
	liquideContenu = NULL;
};


public:float get_volumeActuel()
{
	return volumeActuel;
};

public:int get_capaciteMax()
{
	return capaciteMax;
};

public: string get_matiereRecipient()
{
	return matiere;
};

public: string get_nomRecipient()
{
	return nomRecipient;
};

public: float get_temperatureLiquideContenu()
{
	return liquideContenu->get_temperatureLiquide();
};

public: string get_nomLiquideContenu()
{
	return liquideContenu->get_nomLiquide();
};

public:float get_coefficientLiquideContenut()
{
	return liquideContenu->get_coefficientEbulition();
};

public:int get_temperaturEbulitionLiquide()
{
	return liquideContenu->get_degreEbullition();
};


public:void set_volumeActuel(float r_volumeActuel)
{
	volumeActuel = r_volumeActuel;
};

public: void set_temperatureLiquideContenu(float temperature)
{
	liquideContenu->set_temperatureDuLiquide(temperature);
};


public: void set_remplir(Liquide liquide, float r_quantite)
{
	liquideContenu = &liquide;
	volumeActuel = r_quantite;
};


public:void set_volumeActuelDeborder()
{
	volumeActuel = capaciteMax;
}


public: void afficheur_recipient()
{
	cout << "C'est une " << nomRecipient << "d''une capacite de " << capaciteMax << endl;
	
	if (volumeActuel == 0)
	{
		cout << "Le recipient est vide" << endl;
	}
	else
	{
		cout << "Le recipient contient" << endl;
		cout << volumeActuel << "cL de " << endl;
		liquideContenu->afficheur_liquide();
	}
};

};


class Feu
{


private: string marque;
private: string matiere;
private: string modele;
private: int vitesseChauffe;
private: int degreCourant;
private: bool etat;
private: time_t chrono_debut;
private: time_t chrono_actuelle;
private: Recipient * recipient;


public: Feu(string f_marque, string f_matiere, string f_modele, int r_vitesseChauffe)
{
	marque = f_marque;
	matiere = f_matiere;
	modele = f_modele;
	vitesseChauffe = r_vitesseChauffe;
	degreCourant = 0;
	etat = false;
	chrono_debut = 0;
	chrono_actuelle = 0;
	recipient = NULL;
};


public: int get_chrono_debut()
{
	return chrono_debut;
}

public: int get_chrono_actuelle()
{
	return chrono_actuelle;
}

public: int get_degreCourant()
{
	return degreCourant;
}

public: string get_matiereFeu()
{
	return matiere;
}


public: void set_degreCourant(int tempFeu)
{
	degreCourant = degreCourant - tempFeu;
}

public: void set_chronoActuelle()
{
	time_t compteur;
	srand(time(NULL));
	compteur = time(&compteur);
	chrono_actuelle = compteur - chrono_debut;
}


public: void set_PutOnFire(Recipient r_recipient)
{
	recipient = &r_recipient;
}

public: void alumage_feu()
{
	time_t compteur;
	srand(time(NULL));
	compteur = time(&compteur);
	chrono_debut = compteur;
	etat = true;
};

public: void chauffe_feu(int temperature_souhaiter)
{
	time_t compteur;
	srand(time(NULL));
	compteur = time(&compteur);
	chrono_actuelle = compteur;
	int calcul = chrono_actuelle - chrono_debut;
	degreCourant = vitesseChauffe * calcul;
	chrono_actuelle = compteur - chrono_debut;
};

public: void maintenirFeu(float temperature, time_t compteur, Recipient recipient)
{
	//mise en ebulition du liquide

	temperature += recipient.get_coefficientLiquideContenut() / recipient.get_volumeActuel();
	recipient.set_temperatureLiquideContenu(temperature);
	//pour eviter que la temperature depace celle d�bulition
	if (recipient.get_temperatureLiquideContenu() > recipient.get_temperaturEbulitionLiquide())
	{
		temperature = recipient.get_temperaturEbulitionLiquide();
		recipient.set_temperatureLiquideContenu(temperature);
	}
	compteur = time(&compteur);
	sleep(1);
}

public: void evaporationLiquide(time_t f_compteur, float f_diminution, Recipient f_recipient)
{
	//le liquide perdra (coefficient / (coefficient -1) cl par seconde
	f_diminution = f_recipient.get_volumeActuel() - (f_recipient.get_coefficientLiquideContenut() / (f_recipient.get_coefficientLiquideContenut() - 1));

	cout << f_diminution << endl;
	f_recipient.set_volumeActuel(f_diminution);
	//pour eviter que la temperature depace celle d�bulition
	if (f_recipient.get_volumeActuel() < 0)
	{
		f_recipient.set_volumeActuel(0);
	}
	f_compteur = time(&f_compteur);
	sleep(1);
}

public: void affiche_feu()
{
	cout << "Le feu " << marque << modele << endl;
	cout << "\nCe feu est fait en " << matiere << endl;
	cout << "\nCe feu a une vitesse de chauffe de " << vitesseChauffe << endl;
	cout << "\nLe chrono est lance depuis " << chrono_actuelle - chrono_debut << " secondes" << endl;
	cout << "\nLe feu a une temperature actuelle de " << degreCourant << endl;
	if (etat)
	{
		cout << "Le est allume" << endl;
	}
	else
	{
		cout << "Le feu est eteint" << endl;
	}
	if (recipient != NULL)
	{
		cout << "Un recipient est sur le feu" << endl;
		recipient->afficheur_recipient();
	}
	else
	{
		cout << "Le feu est vide" << endl;
	}
};
};


class Lancement
{

public: Lancement()

{}

public: void comparaison(Feu feu, Liquide liquide, Recipient recipient, Lancement lancement)
{
	if (feu.get_matiereFeu() == recipient.get_matiereRecipient())
	{
		lancement.lancementChauffe(feu, liquide, recipient);
	}
	else
	{
		cout << "Le recipient et le feu ne sont pas compatibles" << endl;
	}
}

public:void lancementChauffe(Feu feu, Liquide liquide, Recipient recipient)
{

	feu.set_PutOnFire(recipient);
	feu.alumage_feu();
	feu.affiche_feu();

	int difference;
	int condition = 0;

	//augmentation de la temperature de la plaque jusqua temperature d'�bulition du liquideContenu
	while (feu.get_degreCourant() < recipient.get_temperaturEbulitionLiquide())
	{
		feu.chauffe_feu(liquide.get_degreEbullition());
		if (condition < feu.get_degreCourant())
		{
			condition = feu.get_degreCourant();
			cout << "La termperature actuelle de la plaque est de " << condition << endl;
		}
	}

	//diminution de la temperature de la plaque si elle est superieur a celle demander
	if (feu.get_degreCourant() > recipient.get_temperaturEbulitionLiquide())
	{
		difference = feu.get_degreCourant() - recipient.get_temperaturEbulitionLiquide();
		cout << "La temperature va baisser de " << difference << endl;
		feu.set_degreCourant(difference);
	}
	cout << "La plaque est a temperature d'ebulition" << endl;
	feu.affiche_feu();

	//mise en ebulition du liquide mais  non fonctionnel
	time_t compteur;
	srand(time(NULL));
	compteur = time(&compteur);
	float temperature = recipient.get_temperatureLiquideContenu();

	//augmente la temperature du liquide jusqua ebulition
	while (recipient.get_temperatureLiquideContenu() < recipient.get_temperaturEbulitionLiquide())
	{
		feu.maintenirFeu(temperature, compteur, recipient);
		temperature = recipient.get_temperatureLiquideContenu();
		cout << "La temperature du liquide est de " << recipient.get_temperatureLiquideContenu() << " degres" endl;
	}

	//evaporation du liquide
	float diminution = recipient.get_volumeActuel() - (recipient.get_coefficientLiquideContenut() / (recipient.get_coefficientLiquideContenut() - 1));
	while (recipient.get_volumeActuel() > 0)
	{
		diminution = recipient.get_volumeActuel();
		feu.evaporationLiquide(compteur, diminution, recipient);
		cout << "Le recipient contient actuellement" << recipient.get_volumeActuel() << " cL" << endl;
	}
}
};


int main()
{

	string matiereRecipient;
	string nom;
	int capaciteMax;

	cout << "Quel est le nom du recipient ? : " << endl;
	cin >> nom;

	cout << "Quelle est sa capacite maximale ? : " << endl;
	cin >> capaciteMax;

	cout << "Quelle est sa matiere ? : " << endl;
	cin >> matiereRecipient;

	Recipient *recipient = new Recipient(nom, capaciteMax, matiereRecipient);
	recipient->afficheur_recipient();

	int degre;
	double coefficientEbulition;

	cout << "Quel est le nom du liquide ? : " << endl;
	cin >> nom;

	cout << "Quel est sa temperature d'ebullition ? : '" << endl;
	cin >> degre;
	cout << "Quel est son coefficient d'ebullition (degre/seconde pour 1 cL) ? : '" << endl;
	
	recipient->set_remplir(*liquide, quantite);
	
	if (recipient->get_capaciteMax()<recipient->get_volumeActuel())
	{
		cout << "Le recipient a	deborde" << endl;
		recipient->set_volumeActuelDeborder();
	}

	recipient->afficheur_recipient();

	int vitesse_chauffe;
	string marque;
	string matiereFeu;
	string modele;

	cout << "Quelle est la marque du feu ? : " << endl;
	cin >> marque;

	cout << "Quelle est la matiere du feu ? : " << endl;
	cin >> matiereFeu;

	cout << "Quel est le modele du feu ? : " << endl;
	cin >> modele;

	cout << "Quelle est la vitesse de chauffe du feu (degres/secondes) ? : " << endl;
	cin >> vitesse_chauffe;

	Feu *feu = new Feu(marque, matiereFeu, modele, vitesse_chauffe);

	
	Lancement *lancement = new Lancement();
	lancement->comparaison(*feu, *liquide, *recipient, *lancement);
	
	return 0;
};

