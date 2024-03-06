using System;
using System.Linq;

public class Job
{
    // Properties
    public int JobNumber { get; }
    public string CustomerName { get; }
    public string JobDescription { get; }
    public double EstimatedHours { get; private set; }
    public double Price => EstimatedHours * 45.00;

    // Constructor
    public Job(int jobNumber, string customerName, string jobDescription, double estimatedHours)
    {
        JobNumber = jobNumber;
        CustomerName = customerName;
        JobDescription = jobDescription;
        EstimatedHours = estimatedHours;
    }

    // Methods
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Job other = (Job)obj;
        return JobNumber == other.JobNumber;
    }

    public override int GetHashCode()
    {
        return JobNumber.GetHashCode();
    }

    public override string ToString()
    {
        return $"Job Number: {JobNumber}, Customer Name: {CustomerName}, Job Description: {JobDescription}, Estimated Hours: {EstimatedHours}, Price: {Price:C}";
    }
}

public class RushJob : Job
{
    private const double RushJobPremium = 150.00;

    public RushJob(int jobNumber, string customerName, string jobDescription, double estimatedHours)
        : base(jobNumber, customerName, jobDescription, estimatedHours)
    {
    }

    // New property representing the price with the rush job premium added
    public double RushJobPrice => base.Price + RushJobPremium;

    // No need to override other methods for this example
}

public class JobDemo3
{
    public static void Main(string[] args)
    {
        RushJob[] rushJobs = new RushJob[5];

        for (int i = 0; i < 5; i++)
        {
            bool validJob = false;

            while (!validJob)
            {
                Console.WriteLine($"Enter details for RushJob {i + 1}:");

                Console.Write("Job Number: ");
                int jobNumber = int.Parse(Console.ReadLine());

                // Check for duplicate job numbers
                if (IsDuplicateJobNumber(rushJobs, jobNumber))
                {
                    Console.WriteLine("Job number already exists. Please enter a different job number.");
                    continue;
                }

                Console.Write("Customer Name: ");
                string customerName = Console.ReadLine();

                Console.Write("Job Description: ");
                string jobDescription = Console.ReadLine();

                Console.Write("Estimated Hours: ");
                double estimatedHours = double.Parse(Console.ReadLine());

                rushJobs[i] = new RushJob(jobNumber, customerName, jobDescription, estimatedHours);
                validJob = true;
            }
        }

        Console.WriteLine("\nAll RushJobs:");
        foreach (var rushJob in rushJobs)
        {
            Console.WriteLine(rushJob);
        }

        double totalPrices = rushJobs.Sum(rushJob => rushJob.RushJobPrice);
        Console.WriteLine($"Total Prices: {totalPrices:C}");
    }

    private static bool IsDuplicateJobNumber(RushJob[] rushJobs, int jobNumber)
    {
        foreach (var rushJob in rushJobs)
        {
            if (rushJob != null && rushJob.JobNumber == jobNumber)
            {
                return true;
            }
        }
        return false;
    }
}