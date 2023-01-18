using System.Diagnostics;


class Solver {

    public Solver(Board initial) {
        initial.CalculateEvaluatie();

        // Time how long it takes to solve the given sudoku
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        // We will have to vary these parameters to see what works best to get the best result overall.
        initial.Print();
        Board consistent = this.NodeConsistency(initial);
        Board result = this.CBT(consistent);
        stopWatch.Stop();

        TimeSpan diff = stopWatch.Elapsed;

        Console.WriteLine("This problem took: " + diff.TotalSeconds + " seconds to complete");
        Console.WriteLine("The final state, with evaluation value " + result.Evaluation + " being: ");
        result.Print();
    }

    private Board NodeConsistency(Board initial)
    {
        
        foreach (Node node in initial.sudoku)
            if (node.Swappable)   
                {   
                node.Domain.ExceptWith(initial.Rows[node.Row]);
                node.Domain.ExceptWith(initial.Columns[node.Column]);
                node.Domain.ExceptWith(initial.BlocksSet[node.Block]);
                DisplaySet(node.Domain);
                }
        return initial;
    }

    void DisplaySet(HashSet<int> set)
    {
        Console.Write("{");
        foreach (int i in set)
        {
            Console.Write(" {0}", i);
        }
        Console.WriteLine(" }");
    }

    public Board CBT(Board board)
    {
        return board;
    }

   
}