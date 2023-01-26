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
        
        this.Solution = result;
        stopWatch.Stop();

        TimeSpan diff = stopWatch.Elapsed;
        this.Runtime = diff.TotalSeconds; 
        Console.WriteLine("This problem took: " + diff.TotalSeconds + " seconds to complete. The following board is the solution: ");
        result.Print();
    }

    public Board GetSolution()
    {
        return this.Solution;
    }

    public double GetRunTime()
    {
        return this.Runtime;
    }

    //Implementatie is nog niet optimaal, misschien delen weer terugvertalen naar een hashset voor meer snelheid?
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