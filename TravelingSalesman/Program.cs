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
            const int population = 100; //The total population limit and number of initial solutions.
            const int iterations = 100000; //The total number of iterations.
            const int elites = 2; //The total number of most optimal solutions to bypass tournament selection at each iteration.
            const double mutationRate = .7; //The chance of a randomly sized portion of cities being shuffled when producing a child.

            List<Circuit> individuals = new List<Circuit>(); //Contains the population.
            Random random = new Random(); //Used for random number generation.
            Mutex writeAccess = new System.Threading.Mutex(); //Control access to standard output.
            Task.Run(() => { Console.ReadLine(); writeAccess.WaitOne(); individuals.First().PrintPath(); Environment.Exit(0); }); //Listen for early exit sequence.

            for (int i = 0; i < population; i++)
            { individuals.Add(new Circuit().Initialize()); }
            double latestDistance = individuals.Min(i => i.Distance);
            for (int i = 0; i < iterations; i++)
            {
                individuals = individuals.OrderBy(c => c.Distance).ToList(); //This will allow us to match the best with the worst.
                #region User Interaction
                writeAccess.WaitOne();
                Console.WriteLine(individuals.First().Distance);
                writeAccess.ReleaseMutex();
                if (individuals.First().Distance != latestDistance)
                {
                    latestDistance = individuals.First().Distance;
                    Task.Run(() => Console.Beep()); //Beep asynchrnously so you don't slow the program down.
                }
                #endregion User Interaction
                for (int j = 0; j < population; j++)
                {
                    //Create children from two parents. Note the "*" operator.
                    Circuit child = individuals[j] * individuals[(population - 1) - j];
                    //Roll for mutation. Randomly select a contiguous range of elements to permute.
                    if (random.NextDouble() < mutationRate)
                    {
                        int upperBound = random.Next(0, child.Path.Count);
                        int lowerBound = random.Next(0, upperBound);
                        child.Path = child.Path.PermutePart(lowerBound, upperBound);
                    }
                    individuals.Add(child);
                }
                List<Circuit> nextGeneration = new List<Circuit>();
                //Elites bypass and are excluded from selection.
                nextGeneration.AddRange(individuals.OrderBy(i => i.Distance).Take(elites));
                individuals = individuals.OrderBy(i => i.Distance).TakeLast(individuals.Count - elites).ToList();
                //Non-elites participate in tournament selection with replacement.
                for (int j = 0; j < population - elites; j++)
                { nextGeneration.Add(individuals.RandomChoice() - individuals.RandomChoice()); }
                individuals = nextGeneration;
            }
            individuals = individuals.OrderBy(c => c.Distance).ToList();
            Console.WriteLine(individuals.First().Distance);
            individuals.First().PrintPath();
        }
    }
}
