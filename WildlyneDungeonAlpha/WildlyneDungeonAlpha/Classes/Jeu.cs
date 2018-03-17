using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace WildlyneDungeonAlpha
{
    class Jeu
    {

        //champs --------------------------------------------
        private Salle _salle;
        private Personnage _heros;
        private Canvas _canvas;
        private Inventaire _inventaire;
        private TextBlock _tbInventaire;
        //méthode -------------------------------------------

        public Jeu(Canvas canvas1, TextBlock tbInventaire, Inventaire inventaire)
        {
            //sauvegarde du canvas / inventaire

            _canvas = canvas1;
            _tbInventaire = tbInventaire;
            _inventaire = inventaire;

            //Création Salle
            _salle = new Salle(40, 40,     "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" + // X = mur
                                           "XXXXXXXXXXXXXXXXXXXXXXXF.........FXXXXXX" + // . = sol
                                           "XXXXX.C..OXXXXXXXXXXXXX...........XXXXXX" + // O = coffre ouvert
                                           "XXXXX...S................TTTTTT...XXXXXX" + // F = coffre fermé
                                           "XXXXXXXXXXXXXXXXXXXXXXX..TTTTTT...XXXXXX" + // S = flaque de sang
                                           "XXXXXXXXXXXXXXXXXXXXXXX...........XXXXXX" + // T = table basse
                                           "XXXXXXXXXXXXXXXXXXXXXXX..........fXXXXXX" + // C = crâne
                                           "XXXXXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXT.C.XXXXXXXXXXXX.XXXXXXXXXXXXXXXX" + //
                                           "XXXXXXX....XXXXXXXXXXXX.XXXXXXXXXXXXXXXX" + //
                                           "XXXXXXX.................XXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXX.XXXXXXXXXXXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXX.XXXXXXXXXXXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXX.XXXXXXXXXXXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXX.S....XXXXXXXXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXX......XXXXXXXXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXX.F.S.............XXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXX" + // 
                                           "XXXXXXXXXXXXXXXXXXXXX..S........XXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXXX.........S.XXXXXXXX" + // 
                                           "XXXXXXXXXXXXXXXXXXXXX.S...T.....XXXXXXXX" + // 
                                           "XXXXXXXXXXXXXXXXXXXXX...........XXXXXXXX" + // 
                                           "XXXXXXXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXX" + // 
                                           "XXXXXXXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXX" + // 
                                           "XXXXXXXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXX" + //
                                           "XXXXXXF.....C.............XXXXXXXXXXXXXX" + //
                                           "XXXXXXF....XXXXXXXXX.XXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXF...TXXXXXXXXX.XXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXX.XXXXXXXXXXXXXXXXXXX" + //
                                           "XXXXXXXXXXXXXXXXXXXXXSXXXXXXXXXXXXXXXXXXX");


            // on ajoute la salle dans le canvas
            _salle.setX((Convert.ToInt32(canvas1.ActualWidth) - _salle.getL() * Tuile.LARGEUR) / 2);
            _salle.setY((Convert.ToInt32(canvas1.ActualHeight) - _salle.getH() * Tuile.HAUTEUR) / 2);

            _salle.ajouteAuCanvas(canvas1);
            _salle.miseAJourPosition();


            //Création personnage
            _heros = new Personnage("héros.png", 32, 32, 10, 10, _salle);

            //set/mise à jour position dans le canvas
            _heros.mettreAJourPositionGraphique();
            _heros.ajouteAuCanvas(canvas1);

        }

        public void miseAJourPosition()
        {
            _salle.miseAJourPosition();
            _heros.miseAJourPosition();
        }

        /// <summary>
        /// Déplace le héros de case en case
        /// </summary>
        /// <param name="dx">déplacement horizontal (-1 / 1)</param>
        /// <param name="dy">déplacement vertical (-1 / 1)</param>
        public void moveHeroes(int dx, int dy)
        {
            int testx = _heros.getXSalle() - dx;
            int testy = _heros.getYSalle() - dy;

            //Test tuile.type =! "mur" ?
            if (_salle.estTraversable(testx,testy))
            {


                //si =! mur : déplacement
                _salle.setY(_salle.getY() + dy * Tuile.HAUTEUR);
                _salle.setX(_salle.getX() + dx * Tuile.LARGEUR);
                _heros.setXSalle(testx);
                _heros.setYSalle(testy);
                miseAJourPositionObjets();
                miseAJourPosition();

            }
                //si coffre, faudra faire des truc
            else if (_salle.coffreFermé(testx,testy))
            {
                _salle.ouvrirCoffre(testx, testy, _canvas);
                //on ajoute un objet dans l'invetaire
                _inventaire.addLoot(Loot.createRandom());
                //on les sauvegardes
                _inventaire.sauvegardeFichierTexte(@"E:\Code\WildlyneDungeon\v4 -WildlyneDungeonAlpha\WildlyneDungeonAlpha\WildlyneDungeonAlpha\Inventaire\sauvegarde_inventaire.txt");
                //on actualise l'inventaire :
                _tbInventaire.Text = "Inventaire : \n"+ _inventaire.actualiserInventaire();
            }
        }

        /// <summary>
        /// Nouvelle taille pour la fenetre (le canvas)
        /// </summary>
        /// <param name="width">nouvelle largeur</param>
        /// <param name="height">nouvelle hauteur</param>
        public void resize(int width, int height)
        {
            // On centre la salle sur le héros
            _salle.setX((width / 2) - (_heros.getXSalle() * Tuile.LARGEUR));
            _salle.setY((height / 2) - (_heros.getYSalle() * Tuile.HAUTEUR));
            miseAJourPositionObjets();
            miseAJourPosition();
        }

        /// <summary>
        /// Met à jour la position graphique des objets par rapport à la salle
        /// </summary>
        public void miseAJourPositionObjets()
        {
            _heros.mettreAJourPositionGraphique();
        }
    }
}
