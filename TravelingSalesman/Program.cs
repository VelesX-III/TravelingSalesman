using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesman
{
    class Program
    {
        static void Main(string[] args)
        {
            Circuit circuit = new Circuit();
            circuit.Path.ForEach(x => Console.WriteLine(x.State));
            Console.WriteLine(circuit.Distance);
        }
    }
}
