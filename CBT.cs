class CBT
{

    public bool isFinished = false;
    public Board CBTAlg(Board sudoku, int index = 0) 
    {
        if (index >= sudoku.sudoku.Length)
        {
            sudoku.Print();
            this.isFinished = true;
            return sudoku;
        }

        Node cell = sudoku.sudoku[index];
       
        if (!cell.Swappable)
        {
            this.CBTAlg(sudoku: sudoku, index: index + 1);
        }

        

        cell.DomainCounter += 1;
        if (cell.Domain != null)
        {
            cell.Number = cell.Domain[cell.DomainCounter];
        
            this.forwardChecking(cell, sudoku);
        if (IsNotEmpty(sudoku))
        {
            //ga door
            this.CBTAlg(sudoku: sudoku, index: index + 1);


        }
        else
        {
            //backtrack
            if(cell.DomainCounter >= (cell.Domain.Count()-1))
            {
                // Backtrack(index--)
                this.BackTrack(sudoku: sudoku, cellIndex: index);
            }
            else
            {
                this.undoDomainUpdate(sudoku: sudoku, cell: cell);
                this.CBTAlg(sudoku: sudoku, index: index);
            }

        }
        
        }
        return sudoku;
        
    }


    void BackTrack(Board sudoku, int cellIndex = 0)
    {        
        Node cell = sudoku.sudoku[cellIndex];
        
        if (!cell.Swappable)
        {
            int backTrackCellIndex = cellIndex - 1;
            this.BackTrack(sudoku, backTrackCellIndex);
        }
        this.undoDomainUpdate(sudoku: sudoku, cell: cell);
        if(!this.isFinished) {
            if(cell.DomainCounter == (cell.Domain.Count()-1))
            {
                int backTrackCellIndex = cellIndex - 1;
                cell.DomainCounter = -1;
                cell.Number = 0;
            
                this.BackTrack(sudoku, backTrackCellIndex);
            }
            else
            {
                this.CBTAlg(sudoku: sudoku, index: cellIndex);
            }
        }
        return;
    }


    private void Debug(Board sudoku)
    {
        Console.ReadLine();
        sudoku.Print();
        this.printDomains(sudoku);
        
    }
    private void printDomains(Board sudoku)
    {
        foreach (Node cell in sudoku.sudoku)
        {
            
            if (cell.Swappable)
            {
                
                Console.Write("DC: " + cell.DomainCounter.ToString());
                Solver.DisplaySet(cell.Domain);
                
            }
        }
    }

    private void undoDomainUpdate(Board sudoku, Node cell)
    {
        foreach (Node effected in cell.EffectedCells)
        {
            if (!effected.Domain.Contains(cell.Number))
            {
                effected.Domain.Add(cell.Number);
                
            }
            effected.Domain.Sort();
        }

        cell.EffectedCells.Clear();
    }
    private void forwardChecking(Node cell, Board sudoku)
    {
       foreach (Node effected in sudoku.RowsSwappable[cell.Row])
        {
            
            if (effected != cell && effected.Domain.Contains(cell.Number) && !cell.EffectedCells.Contains(effected))
            {
                cell.EffectedCells.Add(effected);
                effected.Domain.Remove(cell.Number);
            }
            
        }
        foreach (Node effected in sudoku.ColumnsSwappable[cell.Column])
        {
            if (effected != cell && effected.Domain.Contains(cell.Number) && !cell.EffectedCells.Contains(effected))
            {
                cell.EffectedCells.Add(effected);
                effected.Domain.Remove(cell.Number);
            }
        }
        foreach (Node effected in sudoku.BlocksSwappable[cell.Block])
        {
           
            if (effected != cell && effected.Domain.Contains(cell.Number) && !cell.EffectedCells.Contains(effected))
            {
                cell.EffectedCells.Add(effected);
                effected.Domain.Remove(cell.Number);
            }
        }
    }

    bool IsNotEmpty(Board sudoku)
    {
        Node[] sud = sudoku.sudoku;
        foreach (Node node in sud)
        {
            if (node.Swappable)
            {
                if (node.Domain.Count() == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

}