using System.Diagnostics;

class Solver {

    private Board Solution;
    private double Runtime;

    public Solver(Board initial) {
        // Time how long it takes to solve the given sudoku
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        Board consistent = this.NodeConsistency(initial);
        CBT cBT = new CBT();
        Board result = cBT.CBTAlg(consistent);
        stopWatch.Stop();
        
        this.Solution = result;
        this.Runtime = stopWatch.Elapsed.TotalSeconds; 
    }

    public Board GetSolution()
    {
        return this.Solution;
    }

    public double GetRunTime()
    {
        return this.Runtime;
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
                // DisplaySet(node.Domain);
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