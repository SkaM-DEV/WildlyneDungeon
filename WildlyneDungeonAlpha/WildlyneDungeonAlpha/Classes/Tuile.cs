using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Media;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WildlyneDungeonAlpha
{
    class Tuile : ObjetGraphique
    {
        //champs --------------------------------------------
        public const int LARGEUR = 32;
        public const int HAUTEUR = 32;


        // Type de tuile


        public enum TypeTuile
        {
            // On peut traverser un sol
            SOL,

            //coffre ouvert et fermé
            COFFRE_FERME,
            COFFRE_OUVERT,


            // On ne peut pas traverser un mur ou déco qu'on peut pas traverser
            MUR,
        }

        //les images de la tuile
        private Image[] images;

        //Type de la tuile
        private TypeTuile _type;


        // Constructeur ------------------------------------------
        public Tuile(TypeTuile type, int posX, int posY, string[] nomRessources)
        {
            _type = type;
            setX(posX);
            setY(posY);
            creeImages(nomRessources);
        }

        /// <summary>
        /// Initialise le tableau des images de la tuile
        /// </summary>
        /// <param name="nomRessources">fichiers contenant les bitmaps de la tuile</param>
        private void creeImages(string[] nomRessources) {
            images = new Image[nomRessources.Length];
            for (int indice = 0; indice < nomRessources.Length; indice++)
            {
                images[indice] = new Image();

                BitmapImage bitmap = new BitmapImage();

                //bitmap depend juste du nom ressource

                //on initialise
                bitmap.BeginInit();
                //on donne le chemin de l'image
                bitmap.UriSource = new Uri("pack://application:,,/ressources/" + nomRessources[indice]);
                //Fin initiaisation
                bitmap.EndInit();
                images[indice].Source = bitmap;
                //on donne la largeur/hauteur a l'image
                images[indice].Width = LARGEUR;
                images[indice].Height = HAUTEUR;
                Canvas.SetZIndex(images[indice], 1000 + indice);
            }
        }


        // s'il n'a pas été déjà ajouté dans le canvas, l'ajoute, sinon ne fait rien
        public override void ajouteAuCanvas(Canvas canvas)
        {
            if (!estDansLeCanvas())
            {
                foreach (Image image in images)
                {
                    canvas.Children.Add(image);
                }
                setDansLeCanvas(true);
                miseAJourPosition();

            }
        }

        // s'il est encore dans le canvas, l'enlève, sinon ne fait rien
        public override void enleveDuCanvas(Canvas canvas)
        {
            if (estDansLeCanvas())
            {
                foreach (Image image in images)
                {
                    canvas.Children.Remove(image);
                }
                setDansLeCanvas(false);
            }
        }
        // Met à jour la position dans le canvas
        public override void miseAJourPosition()
        {
            if (estDansLeCanvas())
            {
                foreach (Image image in images)
                {
                    Canvas.SetLeft(image, getX());
                    Canvas.SetTop(image, getY());
                }
            }
        }


        /// <summary>
        /// Listes des tuiles traversable 
        /// </summary>
        /// <returns>oui on non</returns>
        public bool estTraversable()
        {
            return _type == TypeTuile.SOL;

        }

        /// <summary>
        /// coffre fermer ?
        /// </summary>
        /// <returns>oui on non</returns>
        public bool coffreFermé()
        {
            return _type == TypeTuile.COFFRE_FERME;

        }


        /// <summary>
        /// Change le type de tuile
        /// </summary>
        /// <param name="canvas">canvas contenant la tuile</param>
        /// <param name="typeTuile">nouveau type</param>
        /// <param name="res">nouvelles ressources</param>
        public void changeType(Canvas canvas, TypeTuile typeTuile, string[] res)
        {
            enleveDuCanvas(canvas);
            _type = typeTuile;
            creeImages(res);
            ajouteAuCanvas(canvas);
        }
    }
}











