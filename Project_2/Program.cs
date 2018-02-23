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
            string OUTPUT_FILE_PATH;

            List<SuperBowl> SBList = new List<SuperBowl>();

            getFilePath(out OUTPUT_FILE_PATH);

            ReadFromCSV(ref SBList);

            listAllWinners(SBList, OUTPUT_FILE_PATH);

            top5Attended(SBList, OUTPUT_FILE_PATH);

            top5States(SBList, OUTPUT_FILE_PATH);

            mostMVP(SBList, OUTPUT_FILE_PATH);

            mostWonLost(SBList, OUTPUT_FILE_PATH);

        } // end main method

        static void getFilePath(out string filePath)
        {
            string lineInput;

            Console.WriteLine("Please enter the location for the output file: ");
            lineInput = Path.GetFullPath(Console.ReadLine());

            if (Directory.Exists(lineInput) == false)
            {
                Console.WriteLine("Sorry, your input was not a valid file path");
                filePath = "";
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                filePath = lineInput + @"\SB_Output_File.csv";
            }
            
        }
        static void ReadFromCSV(ref List<SuperBowl> sbList)
        {
            string FILE_PATH = @"Super_Bowl_Project.csv";
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

        static void listAllWinners(List<SuperBowl> sbList, string OUTPUT_FILE_PATH)
        {
            FileStream outFile = new FileStream(OUTPUT_FILE_PATH, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(outFile);

            sw.WriteLine("Superbowl Winners");
            sw.WriteLine("Winning Team,Year,Winning Quarterback,Winning Coach,MVP,Point Difference");

            foreach(SuperBowl sb in sbList)
            {
                sw.WriteLine($"{sb.WTeam},{sb.Date},{sb.WQB},{sb.WCoach},{sb.MVP1},{sb.WPoints - sb.LPoints}");
            }

            sw.Close();
            outFile.Close();
        }

        static void top5Attended(List<SuperBowl> sbList, string OUTPUT_FILE_PATH)
        {
            FileStream outFile = new FileStream(OUTPUT_FILE_PATH, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(outFile);

            sw.WriteLine("\nTop 5 Most Attended Superbowls");
            sw.WriteLine("Rank,Year,Winning Team,Losing Team,City,State,Stadium");
            var mostAttended =
                from sb in sbList
                orderby sb.Attendance descending
                select sb;

            int x = 0;
            foreach (SuperBowl sb in mostAttended)
            {
                x += 1;

                sw.WriteLine($"{x},{sb.Date},{sb.WTeam},{sb.LTeam},{sb.City},{sb.State},{sb.Stadium}");

                if (x == 5) { break; }
            }

            sw.Close();
            outFile.Close();
        }

        static void top5States(List<SuperBowl> sbList, string OUTPUT_FILE_PATH)
        {
            FileStream outFile = new FileStream(OUTPUT_FILE_PATH, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(outFile);

            sw.WriteLine("\n\nList of States hosting the most superbowls");
            sw.WriteLine("Number hosted,City,State,Stadium");

            var byState =
                from sb in sbList
                group sb by sb.State into sbGroup
                orderby sbGroup.Count() descending
                select sbGroup;

            foreach(IGrouping<string, SuperBowl> sbG in byState)
            {
                sw.WriteLine($"{sbG.Count()},{sbG.First().City},{sbG.Key},{sbG.First().Stadium}");
            }

            sw.Close();
            outFile.Close();
        }

        static void mostMVP(List<SuperBowl> sbList, string OUTPUT_FILE_PATH)
        {
            FileStream outFile = new FileStream(OUTPUT_FILE_PATH, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(outFile);

            sw.WriteLine("\n\nList of Players who were MVP more than twice");
            sw.WriteLine("Times won,Name,Winning Team,Losing Team");

            var mvps =
                from sb in sbList
                group sb by sb.MVP1 into MVPlayer
                orderby MVPlayer.Count() descending
                where MVPlayer.Count() > 2
                select MVPlayer;

            foreach(IGrouping<string, SuperBowl> mvp in mvps)
            {
                sw.Write($"{mvp.Count()},{mvp.First().MVP1}");
                foreach(SuperBowl sb in mvp)
                {
                    string xRow = "";
                    if(sb.Date != mvp.First().Date) { xRow += ","; }

                    xRow += $",{sb.WTeam},{sb.LTeam}";

                    sw.WriteLine(xRow);
                }
            }

            sw.Close();
            outFile.Close();
        } // end mostMVP

        static void mostWonLost(List<SuperBowl> sbList, string OUTPUT_FILE_PATH)
        {
            FileStream outFile = new FileStream(OUTPUT_FILE_PATH, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(outFile);

            sw.WriteLine("\n\n,Won the most SuperBowls,Lost the most SuperBowls");

            List<string> maxVal(List<string> list)
            {
                var mostOften =
                    from sb in list
                    group sb by sb into top
                    orderby top.Count() descending
                    select top;

                List<string> max = new List<string>();
                foreach(IGrouping<string,string> boi in mostOften)
                {
                    if(boi.Count() == mostOften.First().Count())
                    {
                        max.Add(boi.Key);
                    }
                }


                return max;
            }

            List<string> wCoaches = new List<string>();
            List<string> lCoaches = new List<string>();
            List<string> wTeams = new List<string>();
            List<string> lTeams = new List<string>();

            foreach (SuperBowl sb in sbList)
            {
                wCoaches.Add(sb.WCoach);
                lCoaches.Add(sb.LCoach);
                wTeams.Add(sb.WTeam);
                lTeams.Add(sb.LTeam);
            }

            List<string> mWonC = maxVal(wCoaches);
            List<string> mLostC = maxVal(lCoaches);
            List<string> mWonT = maxVal(wTeams);
            List<string> mLostT = maxVal(lTeams);

            string xRow = "Coaches,";

            for(int x = 0; x < Math.Max(mWonC.Count,mLostC.Count); x++)
            {
                if(x < mWonC.Count) { xRow += $"{mWonC[x]}"; }
                
                if(x < mLostC.Count) { xRow += $",{mLostC[x]}"; }

                sw.WriteLine(xRow);

                xRow = ",";
            }

            xRow = "Teams,";

            for (int x = 0; x < Math.Max(mWonT.Count, mLostT.Count); x++)
            {
                if (x < mWonT.Count) { xRow += $"{mWonT[x]}"; }

                if (x < mLostT.Count) { xRow += $",{mLostT[x]}"; }

                sw.WriteLine(xRow);

                xRow = ",";
            }

            sw.Close();
            outFile.Close();
        } // end mostWonLost

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
