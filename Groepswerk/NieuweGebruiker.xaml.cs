﻿using System;
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

namespace Groepswerk
{
    /* --NieuweGebruiker--
     * Nieuwe gebruiker aanmaken
     * Author: Carmen Celen
     * Date: 01/04/2015
     */
    public partial class NieuweGebruiker : Page
    {

        //Lokale variabelen
        private List<string> klaslijst;
        private Gebruiker nieuweGebruiker;
        private AlleGebruikersLijst accountlijst;

        //Constructors
        public NieuweGebruiker()
        {
            InitializeComponent();
            klaslijst = new Klaslijst();
            boxKlas.ItemsSource = klaslijst;
        }

        //Events
        private void btnVoegToe_Click(object sender, RoutedEventArgs e)
        {
            maakGebruiker();
            maakNieuweAccountLijst();
            accountlijst.SchrijfLijst();
            MessageBox.Show(String.Format("{0} {1} is toegevoegd aan {2}", nieuweGebruiker.Type, nieuweGebruiker, nieuweGebruiker.Klas));
        }

        //Methods
        private void maakGebruiker()
        {
            string voornaam = txtbVoornaam.Text;
            string achternaam = txtboxAchternaam.Text;
            string psw = pswBox.Password;
            string klas = Convert.ToString(boxKlas.SelectedItem);
            string type;
            if (klas.Equals("Leerkracht"))
            {
                type = "lk";
            }
            else
            {
                type = "lln";
            }

            nieuweGebruiker = new Gebruiker(type, klas, voornaam, achternaam, psw);
        }
        private void maakNieuweAccountLijst()
        {
            accountlijst = new AlleGebruikersLijst();
            accountlijst.Add(nieuweGebruiker);
        }

        //Properties
    }
}
