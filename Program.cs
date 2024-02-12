using System.Collections.Concurrent;
using System.Diagnostics;

// Graph Consult Aps https://graphconsult.xyz/

internal class Program {

	private static void Main( string[] args ) {
		int numberOfTasks = 10000; // Number of tasks to start.

		Stopwatch sw = Stopwatch.StartNew( );
		Console.WriteLine( $"Starting {numberOfTasks} tasks on PID {Environment.ProcessId}..." );
		Console.WriteLine( $"Press any key to continue." );
		Console.ReadKey( );

		Task[] tasks = new Task[ numberOfTasks ];
		ConcurrentDictionary<int, int> threadRuns = new( );

		for ( int i = 0; i < numberOfTasks; i++ ) {
			int taskNumber = i;
			tasks[ i ] = Task.Run( ( ) => {
				int Id = Environment.CurrentManagedThreadId;
				// Set the priority of the thread.
				Thread.CurrentThread.Priority = ThreadPriority.Highest;
				Console.WriteLine( $"[{sw.ElapsedMilliseconds}ms]: Task {taskNumber} executing on {Id}." );

				// If the thread has not been run before, add it to the dictionary. Otherwise,
				// increment the count of runs for this thread.
				threadRuns.AddOrUpdate( Id, 1, ( key, oldValue ) => oldValue + 1 );
			} );
		}

		// Wait for all tasks to complete.
		Task.WaitAll( tasks );
		Console.WriteLine( $"All tasks completed after {sw.ElapsedMilliseconds}ms." );

		// Return some simple statistics.
		Console.WriteLine( $"Threads used: {threadRuns.Keys.Count}." );
		foreach ( int key in threadRuns.Keys ) {
			Console.WriteLine( $"Thread {key} executed {threadRuns[ key ]} tasks." );
		}

		// Calculate the frequency distribution of the threads.
		Dictionary<int, double> freq = CalculateFrequency( threadRuns, 2 );
		foreach ( KeyValuePair<int, double> item in freq ) {
			Console.WriteLine( $"Thread {item.Key} handles {item.Value}% of the tasks." );
		}

		// Wait for the user to press a key before exiting.
		Console.WriteLine( "Press any key to exit." );
		Console.ReadKey( );
	}

	/// <summary>
	/// Calculate the frequency distribution of a dictionary.
	/// </summary>
	/// <param name="dict">     </param>
	/// <param name="rounding"> </param>
	/// <returns> A dictionary of values representing their rate of appearance. </returns>
	public static Dictionary<int, double> CalculateFrequency( ConcurrentDictionary<int, int> dict, int rounding ) {
		int totalValues = dict.Values.Sum( );

		Dictionary<int, double> frequencyDistibution = [];
		foreach ( int key in dict.Keys ) {
			// Calculate the frequency of each key and add it to the frequency distribution
			// dictionary.
			double percentage = Math.Round( ( double ) dict[ key ] / totalValues * 100, rounding );
			frequencyDistibution[ key ] = percentage;
		}
		return frequencyDistibution;
	}
}