using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WildlyneDungeonAlpha
{
    class Personnage : ObjetGraphique
    {
        //champs --------------------------------------------

        //position dans la salle
        private Salle _salle;
        private int _xSalle;
        private int _ySalle;


        // L'image du perso
        private Image image=new Image();

        //constructeur
        public Personnage(String nomRessource, int largeur, int hauteur, int xSalle,int ySalle,Salle salle)
        {

            BitmapImage bitmap = new BitmapImage();
            //on initialise
            bitmap.BeginInit();
            //on donne le chemin de l'image
            bitmap.UriSource = new Uri("pack://application:,,/ressources/" + nomRessource);
            //Fin initiaisation
            bitmap.EndInit();
            
            image.Source = bitmap;
            //on donne la largeur/hauteur a l'image
            image.Width = largeur;
            image.Height = hauteur;
            Canvas.SetZIndex(image,10000);

            _salle = salle;
            _xSalle = xSalle;
            _ySalle = ySalle;
            mettreAJourPositionGraphique();
        }

        public void mettreAJourPositionGraphique()
        {
            // Calcule la position en pixels par rapport à : la position en pixels de la salle et la position dans la salle
            setX(_salle.getX() + _xSalle * Tuile.LARGEUR);
            setY(_salle.getY() + _ySalle * Tuile.HAUTEUR);
        }

        //méthode -------------------------------------------

        // s'il n'a pas été déjà ajouté dans le canvas, l'ajoute, sinon ne fait rien
        public override void ajouteAuCanvas(Canvas canvas)
        {
            if (!estDansLeCanvas())
            {

                canvas.Children.Add(image);
                setDansLeCanvas(true);
                miseAJourPosition();

            }
        }


        // s'il est encore dans le canvas, l'enlève, sinon ne fait rien
        public override void enleveDuCanvas(Canvas canvas)
        {
            if (estDansLeCanvas())
            {
                canvas.Children.Remove(image);
                setDansLeCanvas(false);
            }
        }
        //mise a jour position
        public override void miseAJourPosition()
        {
            if (estDansLeCanvas())
            {
                Canvas.SetLeft(image, getX());
                Canvas.SetTop(image, getY());
            }
        }


        //get xSalle
        public int getXSalle() { return _xSalle; }
        //get ySalle
        public int getYSalle() { return _ySalle; }

        //set xSalle
        public void setXSalle(int newVal) { _xSalle = newVal; }
        //set ySalle
        public void setYSalle(int newVal) { _ySalle = newVal; }

    }
}
