using System;

namespace JobDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Job job1 = new Job();
            job1.jobNumber = 0192084;
            job1.cname = "Danny";
            job1.jDescription = "lol";

            Console.WriteLine(job1.cname);
        }
    }


    class Job
    {
        public int jobnumber { get; set; }
        public string cname { get; set; }
        public string jdescription { get; set; }
        public double estimatedhours;
        private double price;

        public Job(int aJobNumber, string aCName, string aJDescription, double aEstimatedHours)
        {
            jobnumber = aJobNumber;
            cname = aCName;
            jdescription = aJDescription;
            estimatedhours = aEstimatedHours;

            Console.WriteLine("Creating Job");
        }

        public bool Equals()
        {
            if (jobnumber)
            {
                return true;
            }
            return false;
        }
    }
}



