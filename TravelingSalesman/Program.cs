using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TravelingSalesman
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Circuit> individuals = new List<Circuit>();
            const int population = 100; //The total population limit and number of initial solutions.
            const int iterations = 100000; //The total number of iterations.
            const int elites = 10; //The total number of most optimal solutions to bypass tournament selection at each iteration.
            const double mutationRate = .2; //The chance of a randomly sized portion of cities being shuffled.
            Mutex writeAccess = new System.Threading.Mutex();
            Random random = new Random();

            for (int i = 0; i < population; i++) { individuals.Add(new Circuit().Initialize()); }
            double latestDistance = individuals.Max(i => i.Distance);
            Task.Run(() => { Console.ReadLine(); writeAccess.WaitOne(); individuals.First().Path.ForEach(c => Console.WriteLine(c)); Environment.Exit(0); });
            for (int i = 0; i < iterations; i++)
            {
                individuals = individuals.OrderBy(c => c.Distance).ToList(); //This will allow us to match the best with the worst.
                writeAccess.WaitOne(); Console.WriteLine(individuals.First().Distance); writeAccess.ReleaseMutex();
                if (individuals.First().Distance != latestDistance)
                {
                    latestDistance = individuals.First().Distance;
                    Task.Run(() => Console.Beep()); //Beep asynchrnously so you don't slow the program down.
                }
                for (int j = 0; j < population; j++)
                {
                    //Create children from two parents.
                    Circuit child = individuals[j] * individuals[(population - 1) - j];
                    //Roll for mutation.
                    if (random.NextDouble() < mutationRate)
                    {
                        int upperBound = random.Next(0, child.Path.Count); int lowerBound = random.Next(0, upperBound);
                        child.Path = child.Path.PermutePart(lowerBound, upperBound);
                    }
                    individuals.Add(child);
                }
                //Perform a tournament selection with random matching.
                List<Circuit> nextGeneration = new List<Circuit>();
                for (int j = 0; j < population - elites; j++) { nextGeneration.Add(individuals.RandomChoice() - individuals.RandomChoice()); }
                //Automatically include the elites.
                nextGeneration.AddRange(individuals.OrderBy(i => i.Distance).Take(elites));
                individuals = nextGeneration;
            }
            individuals = individuals.OrderBy(c => c.Distance).ToList();
            Console.WriteLine(individuals.First().Distance);
            individuals.First().Path.ForEach(c => Console.WriteLine(c));
        }
    }
}
