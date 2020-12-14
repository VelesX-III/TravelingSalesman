using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TravelingSalesman
{
    /// <summary>
    /// Represents a potential solution to the problem.
    /// </summary>
    public class Circuit
    {
        /// <summary>
        /// The order in which each <see cref="City"/> is visited.
        /// </summary>
        public List<City> Path { get; private set; }
        /// <summary>
        /// Gets the total distance traveled over the <see cref="Path"/>.
        /// </summary>
        public double Distance
        {
            get
            {
                double value = 0;
                for (int i = 0; i < Path.Count; i++)
                {
                    value += Math.Sqrt(Math.Pow(Path[(i + 1) % Path.Count].Latitude - Path[i].Latitude, 2) + Math.Pow(Path[(i + 1) % Path.Count].Longitude - Path[i].Longitude, 2));
                }
                return value;
            }
        }
        /// <summary>
        /// Constructs a new <see cref="Circuit"/> with an empty <see cref="Path"/>.
        /// </summary>
        public Circuit()
        {
            Path = new List<City>();
        }
        /// <summary>
        /// Constructs a new <see cref="Circuit"/> from a collection that can be cast as the <see cref="Path"/>.
        /// </summary>
        /// <param name="path">A collection representing the path.</param>
        public Circuit(IEnumerable<City> path)
        {
            Path = path.ToList();
        }
        /// <summary>
        /// Initializes the <see cref="Circuit"/> with a random <see cref="Path"/>.
        /// </summary>
        /// <returns>A reference to the initialized <see cref="Circuit"/>.</returns>
        public Circuit Initialize()
        {
            Path = JsonConvert.DeserializeObject<List<City>>(File.ReadAllText(@"C:\Users\Administrator\source\repos\TravelingSalesman\TravelingSalesman\Capitals.json")).Permute();
            return this;
        }
        /// <summary>
        /// Prints the path traveled on standard out.
        /// </summary>
        public void PrintPath()
        {
            for (int i = 0; i < Path.Count; i++)
            {
                Console.WriteLine(Path[i].State + "~" + Path[(i + 1) % Path.Count].State);
            }
        }
        /// <summary>
        /// Produces a new <see cref="Circuit"/> whose <see cref="Path"/> is formed from those of its operands.
        /// Every other <see cref="City"/> in the <see cref="Path"/> of the first operand is spliced in order with the remaining cities of the second.
        /// </summary>
        /// <param name="circuitA">The <see cref="Circuit"/> whose every other <see cref="City"/> will be present in the result <see cref="Path"/>.</param>
        /// <param name="circuitB">The <see cref="Circuit"/> whose remaining cities will be inserted in order into the remaining null <see cref="Path"/> indices.</param>
        /// <returns>A <see cref="Circuit"/> with a <see cref="Path"/> that combines the paths of its operands.</returns>
        /// <remarks>This operation is non-Abelian.</remarks>
        public static Circuit operator *(Circuit circuitA, Circuit circuitB)
        {
            if (circuitA.Path.Count == 0 || circuitB.Path.Count == 0 || circuitA.Path.Count != circuitB.Path.Count)
            {
                throw new Exception("Paths incompatible for crossing.");
            }

            City[] cities = new City[circuitA.Path.Count];
            List<City> remainder = circuitB.Path.ToList(); //Seems redundant, but forces copy-initialization.
            for (int i = 0; i < circuitA.Path.Count; i += 2)
            {
                cities[i] = circuitA.Path[i];
                remainder.Remove(circuitA.Path[i]);
            }
            Queue<City> queue = new Queue<City>();
            remainder.ForEach(c => queue.Enqueue(c));
            for (int i = 1; i < cities.Length; i += 2)
            {
                if (cities[i] is null)
                {
                    cities[i] = queue.Dequeue();
                }
            }

            return new Circuit(cities);
        }
    }
    /// <summary>
    /// Represents a capital city as a vertex.
    /// </summary>
    public class City
    {
        /// <summary>
        /// The state in the capital <see cref="City"/> lies in.
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// The latitude coordinate of the <see cref="City"/>.
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// The longitude coordinate of the <see cref="City"/>.
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Represents the <see cref="City"/> as a string.
        /// </summary>
        /// <returns>The <see cref="State"/> as a string.</returns>
        public override string ToString() => State;
    }
}
