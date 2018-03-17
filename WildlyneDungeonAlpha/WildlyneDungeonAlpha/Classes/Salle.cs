using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WildlyneDungeonAlpha
{
    class Salle : ObjetGraphique
    {
        //champs --------------------------------------------
        private Tuile[,] _tableauTuiles;
        private int _largeur;
        private int _hauteur;

        //constructeur
        public Salle(int largeur, int hauteur, String contenu)
        {
            string[] nomR;
            _tableauTuiles = new Tuile[hauteur, largeur];
            _largeur = largeur;
            _hauteur = hauteur;
            for (int i = 0; i < _hauteur; i++)
            {
                for (int j = 0; j < _largeur; j++)
                {
                    int idx = i * _largeur + j;
                    Tuile.TypeTuile type;
                    switch (contenu[idx])
                    {
                        //MUR
                        case 'X':
                            type = Tuile.TypeTuile.MUR;
                            nomR = new string[] { "mur.png" };
                            break;
                        case 'T':
                            type = Tuile.TypeTuile.MUR;
                            nomR = new string[] { "sol.png", "table_basse.png" };
                            break;

                        //COFFRE
                        case 'F':
                            type = Tuile.TypeTuile.COFFRE_FERME;
                            nomR = new string[] { "sol.png", "coffre_fermé.png" };
                            break;
                        case 'O':
                            type = Tuile.TypeTuile.COFFRE_OUVERT;
                            nomR = new string[] { "sol.png", "coffre_ouvert.png" };
                            break;


                        //SOL
                        case 'S':
                            type = Tuile.TypeTuile.SOL;
                            nomR = new string[] { "sol.png", "sang1.png" };
                            break;
                        case 'C':
                            type = Tuile.TypeTuile.SOL;
                            nomR = new string[] { "sol.png", "crane.png" };
                            break;
                        case '.':
                        default:
                            type = Tuile.TypeTuile.SOL;
                            nomR = new string[] { "sol.png" };
                            break;

                    }
                    _tableauTuiles[i, j] = new Tuile(type, j * Tuile.LARGEUR, i * Tuile.HAUTEUR, nomR);
                }
            }


        }


        //méthode -------------------------------------------
        //get - set


        // get hauteur
        public int getH() { return _hauteur; }
        // get largeur
        public int getL() { return _largeur; }

        // set  hauteur
        public void setH(int newVal) { _hauteur = newVal; }
        // set  largeur
        public void setL(int newVal) { _largeur = newVal; }


        //ajoute au canvas.
        public override void ajouteAuCanvas(Canvas canvas)
        {
            foreach (Tuile tuile in _tableauTuiles)
            {
                tuile.ajouteAuCanvas(canvas);
            }
        }


        //enlève au canvas.
        public override void enleveDuCanvas(Canvas canvas)
        {
            foreach (Tuile tuile in _tableauTuiles)
            {
                tuile.enleveDuCanvas(canvas);
            }
        }



        // Met à jour la position dans le canvas

        public override void miseAJourPosition()
        {

            for (int i = 0; i < _largeur; i++)
            {
                for (int j = 0; j < _hauteur; j++)
                {
                    int idx = i * _hauteur + j;
                    _tableauTuiles[i, j].setX(getX() + j * Tuile.LARGEUR);
                    _tableauTuiles[i, j].setY(getY() + i * Tuile.HAUTEUR);
                    _tableauTuiles[i, j].miseAJourPosition();
                }
            }


        }
        /// <summary>
        /// Est ce qu'on peut se déplacer en x,y ?
        /// </summary>
        /// <param name="x">x dans la salle</param>
        /// <param name="y">y dans la salle</param>
        /// <returns></returns>
        public bool estTraversable(int x, int y)
        {
            if (x < 0 || x > _largeur || y < 0 || y > _hauteur)
            {
                return false;
            }
            Tuile destination = _tableauTuiles[y, x];
            return destination.estTraversable();
        }


        /// <summary>
        /// Y'a t'il un coffre fermé en x,y ?
        /// </summary>
        /// <param name="x">x dans la salle</param>
        /// <param name="y">y dans la salle</param>
        /// <returns></returns>
        public bool coffreFermé(int x, int y)
        {
            //faut être dans les limites de la salle
            if (x < 0 || x > _largeur || y < 0 || y > _hauteur)
            {
                return false;
            }

            Tuile destination = _tableauTuiles[y, x];
            return destination.coffreFermé();
        }


        /// <summary>
        /// Ouvre le coffre
        /// </summary>
        /// <param name="cofx">position x du coffre dans la salle</param>
        /// <param name="cofy">position y du coffre dans la salle</param>
        /// <param name="canvas">canvas</param>
        public void ouvrirCoffre(int cofx, int cofy, Canvas canvas)
        {
            // Enlève l'ancienne tuile
            _tableauTuiles[cofy, cofx].changeType(canvas,Tuile.TypeTuile.COFFRE_OUVERT, new string[] { "sol.png", "coffre_ouvert.png" });
        }
    }
}
