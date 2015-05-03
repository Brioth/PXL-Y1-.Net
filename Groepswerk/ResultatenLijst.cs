﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groepswerk
{
    //Aangepast op 3/5/2015 door Carmen
    //aparte dinges niet nodig, geef gewoon de txt mee in de constructor, structuur van alle files is toch hetzelfde

    class ResultatenLijst : List<Resultaat>
    {
        public ResultatenLijst(string bestand)
        {
                StreamReader bestandResultaten = File.OpenText(bestand);
                string regel = bestandResultaten.ReadLine();
                char[] scheiding = { ';' };

                while (regel != null)
                {
                        string[] deel = regel.Split(scheiding);

                        Resultaat oefeningenResultaten = new Resultaat(Convert.ToInt32(deel[0]), Convert.ToDateTime(deel[1]), Convert.ToInt32(deel[2]), Convert.ToInt32(deel[3]));
                        this.Add(oefeningenResultaten);
                        regel = bestandResultaten.ReadLine();
                }

                bestandResultaten.Close();
                    
        }
        public void SchrijfLijst(string bestand)
        {
            File.WriteAllText(bestand, String.Empty);
            StreamWriter schrijver = File.AppendText(bestand);
            foreach (Resultaat item in this)
            {
                schrijver.WriteLine(item.SchrijfString());
            }
            schrijver.Close();
        }
    }
}