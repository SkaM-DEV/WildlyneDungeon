using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WildlyneDungeonAlpha
{
    class Inventaire
    {
        // -------------------- Champ -------------------- //

        HashSet<Loot> loots;
        int _or = 0;

        //constructeur 

        public Inventaire()
        {
            loots = new HashSet<Loot>();
        }



        // -------------------- Méthode -------------------- //

        //ajouter un objet dans l'inventaire
        public void addLoot(Loot toAdd)
        {
            loots.Add(toAdd);
        }

        //Sauvegarde des loots dans un fichier .txt
        public void sauvegardeFichierTexte(string nomFichier)
        {
            //Si le fichier.txt n'existe pas
            if (!File.Exists(nomFichier))
            {
                //Création un fichier.txt pour écrire dedans
                using (StreamWriter wsft = File.CreateText(nomFichier))
                {
                    foreach (Loot l in loots)
                    {
                        wsft.WriteLine(l.sauvegarde());
                    }

                }
            }

            else
            {
                using (StreamWriter wsft = File.CreateText(nomFichier))
                {
                    foreach (Loot l in loots)
                    {
                        wsft.WriteLine(l.sauvegarde());
                    }

                }
            }
        }

        //Crée des loots à partir d'une sauvegarde dans un fichier .txt
        public void chargerFichierTexte(string nomFichier)
        {

            //test fichier existe ou pas
            if (!File.Exists(nomFichier))
            {
                Console.WriteLine("Le fichier n'existe pas");
            }
            else
            {
                loots.Clear();
                string[] lines = File.ReadAllLines(nomFichier);
                foreach (string line in lines)
                {
                    Loot l = LootFactory.makeLoot(line);
                    addLoot(l);



                }

            }
        }



        //actualise l'inventaire
        public string actualiserInventaire()
        {
            string lootinvetaire = "";
            foreach (Loot l in loots)
            {

                    //Trop de champ qui ne servent à rien pour l'instant
              //  lootinvetaire += l.getNom() + " (" + l.getType() + ")" + " \n" //
              //  + l.getAttaque() + " attaque " + l.getArmure() + " armure " + l.getVie() + " vie " + l.getValeur() + " de valeur. \n";

                //on va juste afficher le nom et la valeur
                lootinvetaire += l.getNom() + " (" + l.getType() + ")" + ". Prix : " +l.getValeur() + " \n";
            }
            return lootinvetaire;
        }


        //Vendre l'inventaire
        public void vendreInventaire()
        {
            if (loots != null)
            {

                // s = somme des valeurs des loots
                int s=0;
                foreach (Loot l in loots)
                {
                    s += Convert.ToInt32(l.getValeur());
                }
                _or = _or +  s ;

                //on vide et actualise l'inventaire
                loots.Clear();
                actualiserInventaire();
            }
        }


        //get / set "_or"

        public int getOr()
        {
            int or = _or;
            return or;
        }

        public void setOr(int newVal) { _or = newVal; }

    }

}