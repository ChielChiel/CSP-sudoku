
class CBT
{

    // public void CBT()
    // {
    //     Console.WriteLine("kaas");
    // }

    public void CBTAlg(Board sudoku) 
    {
        for (int i = 0; i < sudoku.sudoku.Count(); i++)
        {
            Node cell = sudoku.sudoku[i];

            cell.Number = cell.Domain[0];
            
            

        }
    }


    bool IsNotEmpty(Board sudoku)
    {
        // Niks mag leeg zijn
        Node[] sud = sudoku.sudoku;
        foreach (Node node in sud)
        {
            if(node.Domain.Count() == 0)
            {
                return false;
            }
        }
        return true;
    }

    // Board FillValuesNodesWithDomain(Board sudoku)
    // {


    //     return new Board();
    // }



}