using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    class Program
    {
        static void Main()
        {
            SuperBowl testing = new SuperBowl("15 - Jan - 67", "I", 61946, "Bart Starr", "Vince Lombardi", "Green Bay Packers", 35, "Len Dawson", "Hank Stram", "Kansas City Chiefs", 10, "Bart Starr", "Memorial Coliseum", "Los Angeles", "California");

            Console.WriteLine(testing.Date);

            Console.ReadLine();

            testing.Date = "BOIIII";

            Console.WriteLine(testing.Date);

            Console.ReadLine();
        }
    }
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
