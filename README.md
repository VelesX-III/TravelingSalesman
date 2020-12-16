# TravelingSalesman
Contains the source for the final project, a GA solver for the traveling salesman problem.

Upon running the program, the user can exit at any time by hitting the enter key. This will print the most recent solution attained.

The user can control aspects of the GA by modifying any of the below constants:
	const int population = 100; //The total population limit and number of initial solutions.
	const int iterations = 100000; //The total number of iterations.
	const int elites = 10; //The total number of most optimal solutions to bypass tournament selection at each iteration.
	const double mutationRate = .2; //The chance of a randomly sized portion of cities being shuffled.
  
There are two branches to the repository. One accomplishes reproduction from two parents by randomly placing genes in order into a child.
The blanks are filled in with the second parent's genes in order. The other method transplants a contiguous sublist from one parent and surrounds it
with the remaining genes from the second parent in order.
