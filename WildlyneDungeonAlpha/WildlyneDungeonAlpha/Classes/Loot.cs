using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlyneDungeonAlpha
{
    class Loot
    {
        // Champs statiques ----------------------------------





        /// <summary>
        /// bonus carractéristiques :
        /// arme 50%/0%/0%/50%
        /// casque 0%/0%/50%/50%
        /// armure 0%/50%/0%/50%
        /// bouclier 25%/25%/25%/25%
        /// </summary>




        private static string[][] ITEMS = {     
                                             new string[]{ "100","Cuillère en bois","arme", "0", "0", "0", "0" }, //15%
                                             new string[]{ "85","bol en bois","casque", "0", "0", "0", "0" }, //15%
                                             new string[]{ "70","veste en cuir","armure", "0", "0", "0", "0" }, //15%
                                             new string[]{ "55","rondache en bois","bouclier", "0", "0", "0", "0" }, //15%

                                             new string[]{ "40","épée courte en acier","arme", "10", "0", "0", "10" }, //8%
                                             new string[]{ "32","heaume d'acier","casque", "0", "0", "10", "10" }, //8%
                                             new string[]{ "24","cotte de maille","armure", "0", "10", "0", "10" }, //8%
                                             new string[]{ "16","écu d'acier","bouclier", "5", "5", "5", "5" }, //8%

                                             new string[]{ "8","tibia de dragon","arme", "25", "0", "0", "25" }, //2%
                                             new string[]{ "6","tête de dragon","casque", "0", "0", "25", "25" }, //2%
                                             new string[]{ "4","armure draconique","armure", "0", "25", "0", "25" }, //2%
                                             new string[]{ "2","écaille de dragon","bouclier", "20", "20", "20", "20" } //2%
          
                                          };
        private static Random RND = new Random();



        // -------------------- Champ -------------------- //
        //nom objet
        string _nom;
        string _type; //arme, casque,armure,bouclier.

        //les différentes caractéristiques
        int _attaque;
        int _armure;
        int _vie;

        //prix
        int _valeur;



        //constructeur
        public Loot(string nom, string type, int attaque, int armure, int vie, int valeur)
        {
            _type = type;
            _nom = nom;
            _attaque = attaque;
            _armure = armure;
            _vie = vie;
            _valeur = valeur;
        }

        // -------------------- Methode -------------------- //

        /////////////////////////////////////////////////
        //CREATION D'OBJET ALEATOIRE
        /////////////////////////////////////////////////
        public static Loot createRandom()
        {


            //différent nombres aléatoires

            Loot retval = null;

            int d100 = RND.Next(100);
            for (int idx = ITEMS.Length - 1; idx >= 0; idx--)
            {
                if (d100 <= Convert.ToInt32(ITEMS[idx][0]))
                {
                    // Bingo ! on a gagné un objet !
                    string nom = ITEMS[idx][1];
                    string type = ITEMS[idx][2];
                    int attaque = RND.Next(10) + Convert.ToInt32(ITEMS[idx][3]);
                    int armure = RND.Next(10) + Convert.ToInt32(ITEMS[idx][4]);
                    int vie = RND.Next(10) + Convert.ToInt32(ITEMS[idx][5]);
                    int valeur = RND.Next(10) + Convert.ToInt32(ITEMS[idx][6]);
                    //return l'objet
                    retval = new Loot(nom, type, attaque, armure, vie, valeur);
                    break;
                }
            }
            if (retval == null)
            {
                // Objet par défaut
                retval = new Loot("rien", "erreur de proba", 0, 0, 0, 0);
                //c'est quoi l'objet ?
            }


            //Console.WriteLine("Tu as trouvé : " + retval.getNom() + " (" + retval.getType() + ")" + " \n" + "Avec " + retval.getAttaque() + " d'attaque, " + retval.getArmure() + " d'armure, " + retval.getVie() + " de vie. Cette objet à une valeur de " + retval.getValeur());
            return retval;

        }
        /////////////////////////////////////////////////fin




        /////////////////////////////////////////////////
        //tout les getChamps :
        /////////////////////////////////////////////////
        //get nom
        public string getNom()
        {
            string n = _nom;
            return n;

        }
        //get type
        public string getType()
        {
            string n = _type;
            return n;

        }

        //get attaque

        public int getAttaque()
        {
            int n = _attaque;
            return n;

        }

        //get armure

        public int getArmure()
        {
            int n = _armure;
            return n;

        }
        //get vie
        public int getVie()
        {
            int n = _vie;
            return n;

        }
        //get valeur
        public int getValeur()
        {
            int n = _valeur;
            return n;

        }
        /////////////////////////////////////////////////fin




        /////////////////////////////////////////////////
        //sauvegarder sous la forme d'une chaîne de caractères :
        /////////////////////////////////////////////////
        public string sauvegarde()
        {
            return _nom + ";" + _type + ";" + _attaque + ";" + _armure + ";" + _vie + ";" + _valeur;

        }

        /////////////////////////////////////////////////fin


    }
}
