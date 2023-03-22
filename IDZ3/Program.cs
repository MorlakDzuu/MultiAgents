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
        object locker = new();
        BaseAgent baseAgent = AgentFabric.BaseAgentCreate( "Base", "test" );
        Thread.Sleep( 1000 );
        Console.WriteLine( "\n\n\nMain\n\n\n" );

        baseAgent.StopWorking();
        Console.WriteLine( "\n\n\nMain lock\n\n\n" );
        Thread.Sleep( 3000 );

        baseAgent.StartWorking();
        Console.WriteLine( "\n\n\nMain release\n\n\n" );
        Thread.Sleep( 2000 );

        baseAgent.StopWorking();
        Console.WriteLine( "\n\n\nMain lock\n\n\n" );
        Thread.Sleep( 3000 );

        baseAgent.StartWorking();
        Console.WriteLine( "\n\n\nMain release\n\n\n" );
        Thread.Sleep( 2000 );

        baseAgent.SelfDestruct();
    }

    // This thread procedure performs the task.
    static void ThreadProc( Object stateInfo )
    {
        // No state object was passed to QueueUserWorkItem, so stateInfo is null.
        Console.WriteLine( "Hello from the thread pool." );
    }
}
// The example displays output like the following:
//       Main thread does some work, then sleeps.
//       Hello from the thread pool.
//       Main thread exits.