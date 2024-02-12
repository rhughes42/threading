# Threading in C#
_________________
The `Program.cs` file contains a C# program that creates a large number of tasks and runs them concurrently. Here's a summary of its main components:

**- Main method:** This is the entry point of the program. It starts by initializing a number of tasks to run and a Stopwatch to measure the execution time. It then creates an array of Task objects and a `ConcurrentDictionary `to keep track of how many tasks each thread runs.
**- Task creation:** In a loop, the program creates a number of tasks equal to numberOfTasks. Each task gets its own number and is run on a separate thread. The priority of the thread is set to the highest. The task then either adds a new entry to the threadRuns dictionary (if the thread hasn't run a task before) or increments the count of runs for the thread.
**- Task execution:** The program waits for all tasks to complete using `Task.WaitAll.` It then prints out the total execution time and some statistics about the threads used.
**- Frequency calculation:** The program calculates the frequency distribution of the threads (i.e., the percentage of tasks each thread handled) using the CalculateFrequency method. It then prints out these frequencies.
**- `CalculateFrequency `method:** This method calculates the frequency distribution of a `ConcurrentDictionary`. It sums up all the values in the dictionary, then calculates the frequency of each key and adds it to a new dictionary. The frequencies are rounded to a specified number of decimal places.

The program ends by waiting for the user to press a key before exiting.
