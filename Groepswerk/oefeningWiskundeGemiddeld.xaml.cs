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
    /// <summary>
    /// Interaction logic for oefeningWiskundeGemiddeld.xaml
    /// </summary>
    /// 
    //author: Vincent Vandoninck
    //date: 15/04/2015
    public partial class oefeningWiskundeGemiddeld : Page
    {
        private Oefening tempOefening;
        private OefeningLijst lijstOefeningenWiskunde2;
        private string[] tempOpgaveWiskunde2, tempOplossing2;
        private Random oefeningenNummer = new Random();
        private int oefeningenNummerOpslag;
        private IList<int> oefeningenNummerLijst;
        private string[] lijstOpgaves2;
        private int oefeningPunten2;

        public oefeningWiskundeGemiddeld()
        {
            InitializeComponent();

            //bestand maken met opgave en antwoord per lijn
            // in lijst inlezen
            // random getal tss lijst.count
            //opgave van de geselecteerde oefn in opgave boxen zettenù
            // antwoorden opslaan in andere lijst en ermee vergelijken

            // of 2 random getallen tss 10 laten maken en die uitkomst ervan laten berekenen en opslaan in lijst
            // lijst vergelijken met de user input
            //

            lijstOefeningenWiskunde2 = new OefeningLijst("gemiddeld2");
            for (int i = 0; i > lijstOefeningenWiskunde2.Count; i++)
            {
               // lijstOefeningenWiskunde2[i] = lijstOefeningenWiskunde2[i].opgave;

                tempOplossing2[i] = lijstOefeningenWiskunde2[i].oplossing;

            }
            opgaveblock1.Content =  tempOpgaveWiskunde2[0];
            opgaveblock2.Content = tempOpgaveWiskunde2[1];
            opgaveblock3.Content = tempOpgaveWiskunde2[2];
            opgaveblock4.Content = tempOpgaveWiskunde2[3];
            opgaveblock5.Content = tempOpgaveWiskunde2[4];
            opgaveblock6.Content = tempOpgaveWiskunde2[5];
            opgaveblock7.Content = tempOpgaveWiskunde2[6];
            opgaveblock8.Content = tempOpgaveWiskunde2[7];
            opgaveblock9.Content = tempOpgaveWiskunde2[8];
            opgaveblock10.Content = tempOpgaveWiskunde2[9];



            for (int i = 0; i < 10; i++)
            {
                oefeningenNummerOpslag = oefeningenNummer.Next(0, 9);
                if (!(oefeningenNummerLijst.Contains(oefeningenNummerOpslag)))
                {
                    lijstOpgaves2[i] = tempOplossing2[oefeningenNummerOpslag];

                }
                else
                {
                while (oefeningenNummerLijst.Contains(oefeningenNummerOpslag))
                {
                    oefeningenNummerOpslag = oefeningenNummer.Next(0, 9);
                }
                lijstOpgaves2[i] = tempOplossing2[oefeningenNummerOpslag];
                }
            }

            antwoordlabel1.Content = lijstOpgaves2[0];
            antwoordlabel2.Content = lijstOpgaves2[1];
            antwoordlabel3.Content = lijstOpgaves2[2];
            antwoordlabel4.Content = lijstOpgaves2[3];
            antwoordlabel5.Content = lijstOpgaves2[4];
            antwoordlabel6.Content = lijstOpgaves2[5];
            antwoordlabel7.Content = lijstOpgaves2[6];
            antwoordlabel8.Content = lijstOpgaves2[7];
            antwoordlabel9.Content = lijstOpgaves2[8];
            antwoordlabel10.Content = lijstOpgaves2[9];
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject(DataFormats.Text, ((Label)e.Source).Content);

            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
            //slepen
        }

        private void Label_Drop(object sender, DragEventArgs e)
        // hier wordt gedropped
        {
            ((Label)e.Source).Content = (string)e.Data.GetData(DataFormats.Text);
        }

        private void Label_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        // als je boven een drop object staat veranderd je cursor
        {
            if (e.Effects == DragDropEffects.Copy)
            {
                e.UseDefaultCursors = false;
                Mouse.SetCursor(Cursors.Hand);
            }
            else
                e.UseDefaultCursors = true;

            e.Handled = true;
        }





        private void verbeterButton_Click(object sender, RoutedEventArgs e)
        {
            oefeningPunten2 = 0;

            {

                if ((lijstOefeningenWiskunde2[1].correcteOplossing).Equals(dropLabel1.Content))
                {
                    oefeningPunten2++;
                }
                if ((lijstOefeningenWiskunde2[2].correcteOplossing).Equals(dropLabel2.Content))
                {
                    oefeningPunten2++;
                }
                if ((lijstOefeningenWiskunde2[3].correcteOplossing).Equals(dropLabel3.Content))
                {
                    oefeningPunten2++;
                }
                if ((lijstOefeningenWiskunde2[4].correcteOplossing).Equals(dropLabel4.Content))
                {
                    oefeningPunten2++;
                }
                if ((lijstOefeningenWiskunde2[5].correcteOplossing).Equals(dropLabel5.Content))
                {
                    oefeningPunten2++;
                }
                if ((lijstOefeningenWiskunde2[6].correcteOplossing).Equals(dropLabel6.Content))
                {
                    oefeningPunten2++;
                }
                if ((lijstOefeningenWiskunde2[7].correcteOplossing).Equals(dropLabel7.Content))
                {
                    oefeningPunten2++;
                }
                if ((lijstOefeningenWiskunde2[8].correcteOplossing).Equals(dropLabel8.Content))
                {
                    oefeningPunten2++;
                }
                if ((lijstOefeningenWiskunde2[9].correcteOplossing).Equals(dropLabel9.Content))
                {
                    oefeningPunten2++;
                }
                if ((lijstOefeningenWiskunde2[10].correcteOplossing).Equals(dropLabel10.Content))
                {
                    oefeningPunten2++;
                }

                Punten.Text = "u heeft  " + oefeningPunten2 + " behaald.";
                oefeningPunten2++;
            }



        }


    }
}