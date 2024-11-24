We consider the process of random walk (Brownian motion) in a one-dimensional crystal. Suppose there is a space, which is a finite set of cells N, over which a certain number K of impurity atoms are distributed. At each moment of time, an atom can move to any of two adjacent cells with the same probability threshold p 1≥ р≥0. 
For simplicity, we assume that impurity atoms are reflected at the edges of the crystal (i.e., remain in place). Thus, the total number of impurity particles remains unchanged. We also assume that in the initial state all impurity atoms are concentrated in the leftmost cell. 
The rules for the transition of an impurity particle are as follows:
1.	Each particle simulation thread in each simulation act must generate a random number m in the interval [0, 1]. If m>p, then the transition is to the right, otherwise - to the left.
2.	If the particle is in an extreme position (left or right), then when it tries to leave the crystal, it is ‘reflected’ and remains in place.
Tasks.
1.	You are to write a program that simulates the behaviour of particles in a one-dimensional crystal using multithreaded programming (C#), using a different thread of execution for each impurity atom. The result of the simulation should be a set of ‘snapshots’ characterising the state of distribution of impurity particles in the crystal cells from the beginning to the end of the simulation period (take 1 minute) with a discreteness of 1 second.
2.	Investigate how the granularity of blocking operations (one cell or the entire array of crystal cells) affects the performance of a multithreaded simulation program. 
Procedure:
1.	Study the organisation of parallel processes as threads of execution with shared memory in the chosen programming language. 2) the synchronised construct and its use to provide monopoly access to an object.
- Next, follow the steps of writing a program ():
- The crystal in the program can correspond to an array of integers cells, from 0 to N. The value cells[i] is the number of impurity atoms in the i-th cell of the crystal. 
- Each impurity atom corresponds to a thread of execution, which contains the index of the impurity atom. The work of the thread is to modify the cells array and change its index in an infinite loop. 
- The main method initialises the array by starting the threads and waits 10 seconds, then stops all threads and exits. 
- At the end of the program, you need to recalculate the total number of impurity atoms (it should not change) and display it on the screen.
You are to write 2 versions of this program - one incorrect, which does NOT use data locking, and the other correct, which uses the synchronised modifier. 
The main programs should be called Cells0 and Cells1, respectively. 
In the main classes, a public method getcell(i) can be defined that returns the number of impurity atoms in cell i. 
The parameters of the main method should be N, K and p, respectively, as discussed above.  
