using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WildlyneDungeonAlpha
{
    abstract class ObjetGraphique
    {
        //champs --------------------------------------------


        //abscisse et ordonnée dans le canvas
        private int _posx;
        private int _posy;
        //ajouter dans le canvas ?
        private bool _estDansLeCanvas;



        //méthode -------------------------------------------

        //ajoute au canvas.
        public abstract void ajouteAuCanvas(Canvas canvas);
        //on ne fait rien ici, on vera surement dans les classes filles
        

        //enlève au canvas.
        public abstract void enleveDuCanvas(Canvas canvas);
        //on ne fait rien ici, on vera surement dans les classes filles
       
        
        // Met à jour la position dans le canvas

        public abstract void miseAJourPosition();
        //on ne fait rien ici, on vera surement dans les classes filles


        //GET&SET -------------------------------------------
        // get X
        public int getX() { return _posx; }
        // set  X
        public void setX(int newVal) { _posx = newVal; }

        // get Y
        public int getY() { return _posy; }
        // set Y
        public void setY(int newVal) { _posy = newVal; }

        // get _estDansLeCanvas
        public bool estDansLeCanvas() { return _estDansLeCanvas; }
        // set _estDansLeCanvas
        public void setDansLeCanvas(bool newVal) { _estDansLeCanvas = newVal; }
        //---------------------------------------------------

    }
}
