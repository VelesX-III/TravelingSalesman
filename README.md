# TravelingSalesman
Contains the source for the final project, a GA solver for the traveling salesman problem.

Upon running the program, the user can exit at any time by hitting the enter key. This will print the most recent solution attained. Every time the solution changes from one iteration to the next, the program will beep.

The user can control aspects of the GA by modifying any of the following constants: population (the total population limit and number of initial solutions), iterations (the total number of iterations), elites (the total number of most optimal solutions to bypass tournament selection at each iteration), and mutationRate (the chance of a randomly sized portion of cities being shuffled when producing a child).
  
There are two branches to the repository. One accomplishes reproduction from two parents by randomly placing genes in order into a child. The blanks are filled in with the second parent's genes in order. The other method transplants a contiguous sublist from one parent and surrounds it with the remaining genes from the second parent in order.
