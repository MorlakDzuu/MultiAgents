using IDZ3.Agents.Base;
using IDZ3.Services.AgentFabric;
using System;
using System.Threading;

public class Program
{
    public static void Main()
    {
        // Queue the task.
        //ThreadPool.QueueUserWorkItem( ThreadProc );
        //Console.WriteLine( "Main thread does some work, then sleeps." );
        //Thread.Sleep( 1000 );

        //Console.WriteLine( "Main thread exits." );
        BaseAgent AdminAgent = AgentFabric.AdminAgentCreate();
        Thread.Sleep( 5000 );
        Console.WriteLine( "\n\n\nMain\n\n\n" );
    }
}
// The example displays output like the following:
//       Main thread does some work, then sleeps.
//       Hello from the thread pool.
//       Main thread exits.