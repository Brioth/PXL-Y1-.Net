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
using System.Windows.Threading;

namespace Groepswerk
{
    /* --ZombieSpel--
     * 
     * 
     * Timers:
     * spelTijdTimer: hoe lang het spel duurt op basis van gewonnen seconden
     * spawnerSpeler/spawnerComputer: Hoe snel er nieuwe humans spawnen
     * animationTimer: timer bewegingen
     * minuutTimer: elke minuut increased de spawnsnelheid van de computer om het spel moeilijker te maken
     * vijfSecondenTimer: Skill 2 en 4 duren 5 seconden
     * skill6Timer: skill 6 kan je 2x 5 seconden, 2x 4 seconden en 2x 3 seconden gebruiker
     * Author: Carmen Celen
     * Date: 24/04/2015
     */
    public partial class ZombieSpel : Page
    {
        //Lokale variabelen
        private ZombieSpelSpeler speler;
        private ZombieSpelComputer computer;
        private DispatcherTimer spelTijdTimer, spawnerSpeler, spawnerComputer, animationTimer, minuutTimer, vijfSecondenTimer, skill6Timer;
        private Gebruiker actieveGebruiker;
        private Point puntSpeler, puntComputer;
        private int skill1Aantal = 5;
        private int skill6Aantal = 2;
        private int skill6Tijd = 5;

        //Constructors
        public ZombieSpel(Gebruiker actieveGebruiker)
        {
            InitializeComponent();
          
            this.actieveGebruiker = actieveGebruiker;
            speler = new ZombieSpelSpeler();
            computer = new ZombieSpelComputer();

            spelTijdTimer = new DispatcherTimer();
            spelTijdTimer.Interval = TimeSpan.FromSeconds(this.actieveGebruiker.GameTijdSec);
            spelTijdTimer.Tick += EindeSpel_Tick;
            spelTijdTimer.Start();

            minuutTimer = new DispatcherTimer();
            minuutTimer.Interval = TimeSpan.FromMinutes(1);
            minuutTimer.Tick += MinuutTimer_Tick;
            minuutTimer.Start();

            spawnerSpeler = new DispatcherTimer();
            spawnerSpeler.Interval = TimeSpan.FromSeconds(1);
            spawnerSpeler.Tick += SpawnSpeler_Tick;
            spawnerSpeler.Start();

            spawnerComputer = new DispatcherTimer();
            spawnerComputer.Interval = TimeSpan.FromMilliseconds(3000);
            spawnerComputer.Tick += SpawnComputer_Tick;
            spawnerComputer.Start();

            animationTimer = new DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(50);
            animationTimer.Tick += Animation_Tick;
            animationTimer.Start();

            vijfSecondenTimer = new DispatcherTimer();
            vijfSecondenTimer.Interval = TimeSpan.FromSeconds(5);
            vijfSecondenTimer.Stop();

            skill6Timer = new DispatcherTimer();
            skill6Timer.Tick += Skill6_Tick;
            skill6Timer.Stop();
        }

        //Events
        private void MinuutTimer_Tick(object sender, EventArgs e)
        {
            int nieuweTijd = Convert.ToInt32(spawnerComputer.Interval.TotalMilliseconds) - 500;
            spawnerComputer.Interval = TimeSpan.FromMilliseconds(nieuweTijd);
        }

        private void Animation_Tick(object sender, EventArgs e)
        {
            speler.Beweeg(spelCanvas);
            computer.Beweeg(spelCanvas);
            speler.CheckHit(computer.HumansComputer, computer.ZombiesComputer);
            computer.CheckHit(speler.HumansSpeler, speler.ZombiesSpeler);
            speler.MaakVrij(spelCanvas);
            computer.MaakVrij(spelCanvas);
        }

        private void SpawnComputer_Tick(object sender, EventArgs e)
        {
            if (computer.HumansComputer.Count < 100)
            {
                ZombieSpelHuman humanComputer = new ZombieSpelHuman(puntComputer, "#13737C");
                computer.HumansComputer.Add(humanComputer);
                humanComputer.DisplayOn(spelCanvas);
            }
        }

        private void SpawnSpeler_Tick(object sender, EventArgs e)
        {
            if (speler.HumansSpeler.Count < 100)
            {
                ZombieSpelHuman humanSpeler = new ZombieSpelHuman(puntSpeler, "#CB2611");
                speler.HumansSpeler.Add(humanSpeler);
                humanSpeler.DisplayOn(spelCanvas);
            }

        }

        private void BtnSkill1_Click(object sender, RoutedEventArgs e)
        {
            Random richtingRandom = new Random();
            for (int i = 0; i < skill1Aantal; i++)
            {
                double x = richtingRandom.Next(Convert.ToInt32(spelCanvas.ActualWidth));
                double y = richtingRandom.Next(Convert.ToInt32(spelCanvas.ActualHeight));
                Point randomPunt = new Point(x, y);
                ZombieSpelHuman humanSpeler = new ZombieSpelHuman(randomPunt, "#CB2611");
                speler.HumansSpeler.Add(humanSpeler);
                humanSpeler.DisplayOn(spelCanvas);
            }
            if (skill1Aantal > 1)
            {
                skill1Aantal--;
            }
        }

        private void BtnSkill2_Click(object sender, RoutedEventArgs e)
        {
            vijfSecondenTimer.Tick += ResetSkill2_Tick;
            vijfSecondenTimer.Start();
            int nieuweTijd = Convert.ToInt32(spawnerSpeler.Interval.TotalMilliseconds)*10;
            spawnerSpeler.Interval = TimeSpan.FromMilliseconds(nieuweTijd);
        }

        private void ResetSkill2_Tick(object sender, EventArgs e)
        {
            vijfSecondenTimer.Stop();
            int oudeTijd = Convert.ToInt32(spawnerSpeler.Interval.TotalMilliseconds) / 10;
            spawnerSpeler.Interval = TimeSpan.FromMilliseconds(oudeTijd);
        }

        private void BtnSkill3_Click(object sender, RoutedEventArgs e)
        {
            Random generator = new Random();
            int randomIndex = generator.Next(speler.ZombiesSpeler.Count);
            speler.ZombiesSpeler[randomIndex].GeraaktDoorEigen = true;
        }

        private void BtnSkill4_Click(object sender, RoutedEventArgs e)
        {
            vijfSecondenTimer.Tick += ResetSkill4_Tick;
            vijfSecondenTimer.Start();
            int nieuweTijd = Convert.ToInt32(spawnerComputer.Interval.TotalMilliseconds) / 10;
            spawnerComputer.Interval = TimeSpan.FromMilliseconds(nieuweTijd);
        }

        private void ResetSkill4_Tick(object sender, EventArgs e)
        {
            vijfSecondenTimer.Stop();
            int oudeTijd = Convert.ToInt32(spawnerComputer.Interval.TotalMilliseconds) * 10;
            spawnerComputer.Interval = TimeSpan.FromMilliseconds(oudeTijd);
        }

        private void BtnSkill5_Click(object sender, RoutedEventArgs e)
        {
            Random generator = new Random();
            int randomIndex = generator.Next(computer.HumansComputer.Count);
            computer.HumansComputer[randomIndex].Geraakt = true;
        }

        private void BtnSkill6_Click(object sender, RoutedEventArgs e)
        {
            skill6Timer.Interval = TimeSpan.FromSeconds(skill6Tijd);
            skill6Timer.Start();
            spawnerComputer.IsEnabled = false;
        }
        private void Skill6_Tick(object sender, EventArgs e)
        {
            spawnerComputer.IsEnabled = true;
            if (skill6Aantal == 2)
            {
                skill6Aantal--;
            }
            else if (skill6Aantal == 1 )
            {
                skill6Aantal = 2;
                if (skill6Tijd <= 3)
                {
                    skill6Tijd = 0;
                }
                else
                {
                    skill6Tijd--;
                }
            }
        }
        private void EindeSpel_Tick(object sender, EventArgs e)
        {
            //(sender as DispatcherTimer).Stop();
            //spawnerSpeler.IsEnabled = false;
            //spawnerComputer.IsEnabled = false;
            //animationTimer.IsEnabled = false;

            //MessageBox.Show("Tijd is op");
            ////scores weergeven en wegschrijven
            //TijdOp(); //Zet gameTijd op 0


        }
        private void SpelCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            puntSpeler = new Point(spelCanvas.ActualWidth / 2, spelCanvas.ActualHeight - 75);
            puntComputer = new Point(spelCanvas.ActualWidth / 2, 25);
            Canvas.SetLeft(imgSpeler, puntSpeler.X);
            Canvas.SetTop(imgSpeler, puntSpeler.Y);
            Canvas.SetLeft(imgComputer, puntComputer.X);
            Canvas.SetTop(imgComputer, puntComputer.Y);
        }
        //Methods
        private void TijdOp() //Zet gameTijd op 0
        {
            AlleGebruikersLijst lijst = new AlleGebruikersLijst();

            for (int i = 0; i < lijst.Count; i++)
            {
                if (lijst[i].Id == actieveGebruiker.Id)
                {
                    lijst[i].SetGameTijdOp0();
                }
            }
            lijst.SchrijfLijst();
        }

        //Properties

    }
}