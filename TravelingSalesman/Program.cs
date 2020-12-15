using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesman
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Circuit> individuals = new List<Circuit>();
            const int population = 100;
            const int iterations = 10000;
            const double mutationRate = .2;
            Random random = new Random();
            for (int i = 0; i < population; i++)
            {
                individuals.Add(new Circuit().Initialize());
            }
            individuals = individuals.OrderBy(c => c.Distance).ToList();

            for (int i = 0; i < iterations; i++)
            {
                List<Circuit> children = new List<Circuit>();
                for (int j = 0; j < population; j++)
                {
                    //Create children from two parents.
                    Circuit child = individuals[j] * individuals[(population - 1) - j];
                    //Roll for mutation.
                    if (random.NextDouble() < mutationRate)
                    {
                        int upperBound = random.Next(0, child.Path.Count);
                        int lowerBound = random.Next(0, upperBound);
                        child.Path = child.Path.PermutePart(lowerBound, upperBound);
                    }
                    children.Add(child);
                }
                individuals.AddRange(children);
                individuals = individuals.OrderBy(c => c.Distance).Take(population).ToList();
                Console.WriteLine(individuals.First().Distance);
            }
            individuals.First().PrintPath();
        }
    }
}
