using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;



namespace WildlyneDungeonAlpha
{

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {

        // Le timer à 10 ms
        private DispatcherTimer timer;

        // Le jeu
        private Jeu _jeu;
        private Inventaire inventaire = new Inventaire();
       


        
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            
        }


        //----------------------

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Start();

            _jeu = new Jeu(canvas1, tbInventaire,inventaire);
            
        }



        // Evenement récurrent toutes les 10ms
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _jeu.miseAJourPosition();          
        }

        private void Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z)
            {
                _jeu.moveHeroes(0, 1);
            }

            if (e.Key == Key.S)
            {
                _jeu.moveHeroes(0, -1);
            }

            if (e.Key == Key.Q)
            {
                _jeu.moveHeroes(1, 0);
            }
            if (e.Key == Key.D)
            {
                _jeu.moveHeroes(-1, 0);
            }
        }

        private void canvas1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_jeu != null)
            {
                _jeu.resize(Convert.ToInt32(canvas1.ActualWidth), Convert.ToInt32(canvas1.ActualHeight));
            }

        }


        //Vendre l'inventaire
        private void bVendreInventaire_Click(object sender, RoutedEventArgs e)
        {
            //on vend on actualise
            inventaire.vendreInventaire();
            tbInventaire.Text = inventaire.actualiserInventaire();
            //on actualise l'or
            tbOr.Text = "Or : " + Convert.ToString(inventaire.getOr());
        } 


    }
}

