using System.Diagnostics;
using System.Xml.Linq;


class Solver {

    private Board Solution;
    public Solver(Board initial) {
        initial.CalculateEvaluatie();

        // Time how long it takes to solve the given sudoku
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        // We will have to vary these parameters to see what works best to get the best result overall.
        initial.Print();
        
        Board consistent = this.NodeConsistency(initial);
        
        CBT cBT = new CBT();
        
        Board antwoord = cBT.CBTAlg(consistent);
        this.Solution = antwoord;
        Console.WriteLine("klaartjs");
        antwoord.Print();
        stopWatch.Stop();




        TimeSpan diff = stopWatch.Elapsed;

        Console.WriteLine("This problem took: " + diff.TotalSeconds + " seconds to complete");
    }

    public Board GetSolution()
    {
        return this.Solution;
    }


    //NodeConsistency makes the initial board nodeconsistent: every node has an updated domain that takes the unswappable origininal numbers into account
    private Board NodeConsistency(Board initial)
    {
        
        foreach (Node node in initial.sudoku)
            if (node.Swappable)   
                {
                
                node.Domain = node.Domain.Except(initial.Rows[node.Row]).ToList();
                node.Domain = node.Domain.Except(initial.Columns[node.Column]).ToList();
                node.Domain = node.Domain.Except(initial.BlocksSet[node.Block]).ToList(); ;
                Console.Write($"{node.Row.ToString()}, ");
                Console.Write(node.Column.ToString());
                DisplaySet(node.Domain);
                }
        return initial;
    }

    //DisplaySet is a small method for debugging, it prints the list 
    public static void DisplaySet(List<int> set)
    {
      
        Console.Write("{");
        foreach (int i in set)
        {
            Console.Write(" {0}", i);
        }
        Console.WriteLine(" }");
        
    }

  

   
}