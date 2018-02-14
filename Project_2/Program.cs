using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    class Program
    {
        static void Main()
        {
            List<SuperBowl> SBList = new List<SuperBowl>();

            ReadFromCSV(ref SBList);
        } // end main method

        static void ReadFromCSV(ref List<SuperBowl> sbList)
        {
            const string FILE_PATH = @"C:\Users\sipkenj\Documents\Visual Studio 2017\Projects\Project_2\Super_Bowl_Project.csv";
            FileStream dataFile = new FileStream(FILE_PATH, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(dataFile);
            string[] currentLine = sr.ReadLine().Split(',');

            while (sr.EndOfStream == false)
            {
                currentLine = sr.ReadLine().Split(',');

                sbList.Add(new SuperBowl(currentLine[0], currentLine[1], Convert.ToInt32(currentLine[2]), currentLine[3], currentLine[4], currentLine[5], Convert.ToInt32(currentLine[6]), currentLine[7], currentLine[8], currentLine[9], 
                                         Convert.ToInt32(currentLine[10]), currentLine[11], currentLine[12], currentLine[13], currentLine[14] ));
            } // end reading from csv file

            sr.Close();
            dataFile.Close();
        } // end ReadFromCSV method

    } // end program

    class SuperBowl
    {
        private string date, number, wQB, wCoach, wTeam, lQB, lCoach, lTeam, MVP, stadium, city, state;
        private int attendance, wPoints, lPoints;


        public SuperBowl(string date, string number, int attendance, string wQB, string wCoach, string wTeam, int wPoints, string lQB, string lCoach, string lTeam, int lPoints, string MVP, string stadium, string city, string state)
        {
            this.Date = date;
            this.Number = number;
            this.Attendance = attendance;
            this.WQB = wQB;
            this.WCoach = wCoach;
            this.WTeam = wTeam;
            this.wPoints = wPoints;
            this.LQB = lQB;
            this.LCoach = lCoach;
            this.LTeam = lTeam;
            this.lPoints = lPoints;
            this.MVP1 = MVP;
            this.Stadium = stadium;
            this.City = city;
            this.State = state;
        } //end constructor

        public string Date { get => date; set => date = value; }
        public string Number { get => number; set => number = value; }
        public string WQB { get => wQB; set => wQB = value; }
        public string WCoach { get => wCoach; set => wCoach = value; }
        public string WTeam { get => wTeam; set => wTeam = value; }
        public string LQB { get => lQB; set => lQB = value; }
        public string LCoach { get => lCoach; set => lCoach = value; }
        public string LTeam { get => lTeam; set => lTeam = value; }
        public string MVP1 { get => MVP; set => MVP = value; }
        public string Stadium { get => stadium; set => stadium = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public int Attendance { get => attendance; set => attendance = value; }
        public int WPoints { get => wPoints; set => wPoints = value; }
        public int LPoints { get => lPoints; set => lPoints = value; }
    } // end class SuperBowl
}
