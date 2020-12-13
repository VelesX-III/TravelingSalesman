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
            const int iterations = 100;
            const double mutationRate = .1;
            Random random = new Random();
            for (int i = 0; i < population; i++)
            {
                individuals.Add(new Circuit());
            }
            individuals = individuals.OrderBy(c => c.Distance).ToList();

            for (int i = 0; i < iterations; i++)
            {
                List<Circuit> children = new List<Circuit>();
                for (int j = 0; j < individuals.Count - 1; j += 2)
                {
                    //Create children from two parents.
                    List<City>[] childPaths = { new List<City>(), new List<City>() };
                    childPaths[0].AddRange(individuals[j].Path.GetRange(0, 27));
                    childPaths[0].AddRange(individuals[j + 1].Path.GetRange(27, 26));
                    childPaths[1].AddRange(individuals[j].Path.GetRange(27, 26));
                    childPaths[1].AddRange(individuals[j + 1].Path.GetRange(0, 27));
                    //Roll for mutation.
                    if (random.NextDouble() < mutationRate)
                    {
                        childPaths[0].Permute();
                    }
                    if (random.NextDouble() < mutationRate)
                    {
                        childPaths[1].Permute();
                    }
                    children.Add(new Circuit() { Path = childPaths[0] });
                    children.Add(new Circuit() { Path = childPaths[1] });
                }
                individuals.AddRange(children);
                individuals = individuals.OrderBy(c => c.Distance).Take(population).ToList();
                Console.WriteLine(individuals.First().Distance);
            }
            individuals.First().PrintPath();
        }
    }
}
