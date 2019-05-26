using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MathWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        // Bloc principal ici, lorsque l'utilisateur clique sur le boutton calculer.
        private void CalculerButton_Click(object sender, RoutedEventArgs e)
        {
            // Validation des donnees
            bool isValid = true;
            double varA = 0, varB = 0;
            try
            {
               varA = System.Convert.ToDouble(A.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Nombre A invalide.");
                isValid = false;
            }
            try
            {
                varB = System.Convert.ToDouble(B.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Nombre B invalide.");
                isValid = false;
            }

            if (varA < 0 || varA > 11)
            {
                MessageBox.Show("Nombre A doit etre entre 0 et 11.");
                isValid = false;
            }
            if (varB < 0 || varB > 11)
            {
                MessageBox.Show("Nombre B doit etre entre 0 et 11.");
                isValid = false;
            }

            if (Choix.SelectedIndex == -1)
            {
                MessageBox.Show("SVP, faire un choix de formule.");
                isValid = false;
            }


            // Le tout est valide, on peux commencer nos calculs!!
            if (isValid)
            {
                int y_max = 0;
                // On determine y_max selon la formule selectionnee.

                // f1
                if (Choix.SelectedIndex == 0)
                {
                    y_max = 5;
                }
                // f2
                if (Choix.SelectedIndex == 1)
                {
                    y_max = 5;
                }
                // f3
                if (Choix.SelectedIndex == 2)
                {
                    y_max = 12;
                }
                // f4
                if (Choix.SelectedIndex == 3)
                {
                    y_max = 10;
                }
                // f5
                if (Choix.SelectedIndex == 4)
                {
                    y_max = 4;
                }

                // x_max sera toujours 11.
                int AireDuRectangue = 11 * y_max;

                int NombreDeTirs = 10000;

                // Boucle principale
                var random = new Random(); // Declaration hors de la boucle pour un vrai random
                int NombreDeTirsSuccess = 0;
                for (int i = 0; i < NombreDeTirs; i++)
                {
                    // Determiner les coordonees du tir
                    double tir_x, tir_y;
                    tir_x = GetRandomDouble(random, 0, 11);
                    tir_y = GetRandomDouble(random, 0, y_max);

                    // Si le tir_y est plus petit OU egal au resultat de la fonction, le tir est un success
                    // !! TO DO: Determiner quelle fonction on doit utiliser selon le combobox CHOIX !!
                    if (tir_y <= f5(tir_x))
                    {
                        NombreDeTirsSuccess++;
                    }
                }

                // Determiner le PourcentageTirsSuccess (P)
                double P = ((double)NombreDeTirsSuccess / (double)NombreDeTirs);
                RepPi.Text = P.ToString();

                // Determiner ME et Intervale Confiance

                double zA2 = 1.96; // 95% / 2 = 47.5% -> Table = 1.96

                double ME = zA2 * Math.Sqrt((P * (1 - P)) / NombreDeTirs);
            
                double IntervaleConfianceMin = P - ME;
                double IntervaleConfianceMax = P + ME;

                RepInterval.Text = Math.Round(IntervaleConfianceMin, 4) + " ≤ π ≤ " + Math.Round(IntervaleConfianceMax, 4);

                // Determiner l'aire de la fonction
                double AireSousLaCourbeMin = (AireDuRectangue * IntervaleConfianceMin);
                double AireSousLaCourbeMax = (AireDuRectangue * IntervaleConfianceMax);
                RepResultat.Text = Math.Round(AireSousLaCourbeMin, 4) + " ≤ X ≤ " + Math.Round(AireSousLaCourbeMax, 4);






            }


        }

        double GetRandomDouble(Random random, double min, double max)
        {
            return min + (random.NextDouble() * (max - min));
        }

        private double f5(double x)
        {
            return Math.Cos(x) + 3;
        }

    }
}
