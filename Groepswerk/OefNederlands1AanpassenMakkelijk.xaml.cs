﻿using System;
using System.Collections.Generic;
using System.IO;
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
    //author:Thomas Cox
    //Date: 19/04/2015
    public partial class OefNederlands1AanpassenMakkelijk : Page
    {
        private OefeningLijst lijstOefeningen;
        private Oefening selectedOefening;
        private string bestand = "OefNederlands1Makkelijk.txt";
        public OefNederlands1AanpassenMakkelijk(Gebruiker actieveGebruiker)
        {
            InitializeComponent();

            lijstOefeningen = new OefeningLijst("makkelijk");
            OpgaveSelecteren.ItemsSource = lijstOefeningen;
            OpgaveSelecteren.SelectedIndex = 0;

        }
        private void OpgaveSelecteren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OpgaveSelecteren.SelectedIndex != -1)
            {
                selectedOefening = (Oefening)OpgaveSelecteren.SelectedItem;
                opgaveBox.Text = selectedOefening.opgave;
                oplossing1Box.Text = selectedOefening.oplossing1;
                oplossing2Box.Text = selectedOefening.oplossing2;
                oplossing3Box.Text = selectedOefening.oplossing3;
                correcteOplossingBox.Text = selectedOefening.correcteOplossing;
                juisteAntwoordCompleetBox.Text = selectedOefening.juisteAntwoordCompleet;
                
            }

        }

        private void AanpasKnop_Click(object sender, RoutedEventArgs e)
        {
            if ((opgaveBox.Text.Contains(';')) || (oplossing1Box.Text.Contains(';')) || (oplossing2Box.Text.Contains(';')) || (oplossing3Box.Text.Contains(';')))
            {
                MessageBox.Show("Gelieve geen ';' in uw zinnen te zetten.");
            }//end if
            else
            {
            Oefening nieuwItem = new Oefening(opgaveBox.Text, oplossing1Box.Text, oplossing2Box.Text, oplossing3Box.Text, correcteOplossingBox.Text, juisteAntwoordCompleetBox.Text);
            lijstOefeningen.Add(nieuwItem);
            lijstOefeningen.Remove(selectedOefening);
            lijstOefeningen.SchrijfLijstTaal(bestand);
            UpdateLijst();
            }//end else
        }

        private void UpdateLijst()
        {
            lijstOefeningen = new OefeningLijst("Makkelijk");
            OpgaveSelecteren.ItemsSource = lijstOefeningen;
            OpgaveSelecteren.SelectedIndex = -1;
            OpgaveSelecteren.SelectedIndex = 0;
        }

        private void verwijderKnop_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult stoppen = MessageBox.Show("Bent u zeker dat u dit wilt verwijderen ?", "Stop", MessageBoxButton.YesNo);
            switch (stoppen)
            {
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    lijstOefeningen.Remove(selectedOefening);
                    lijstOefeningen.SchrijfLijstTaal(bestand);
                    UpdateLijst();
                    break;
                default:
                    break;

            }

        }

        private void toevoegKnop_Click(object sender, RoutedEventArgs e)
        {
            Oefening nieuwOefening = new Oefening(opgaveBox.Text, oplossing1Box.Text, oplossing2Box.Text, oplossing3Box.Text, correcteOplossingBox.Text, juisteAntwoordCompleetBox.Text);
            lijstOefeningen.Add(nieuwOefening);
            lijstOefeningen.SchrijfLijstTaal(bestand);
            UpdateLijst();
        }
}
}