class CBT
{
    //This bool is for the last iteration: if the sudoku is correct and completely filled this becomes true
    public bool isFinished = false;
    
    public Board CBTAlg(Board sudoku, int index = 0) 
    {
        //This check is to check for the last iteration
        if (index >= sudoku.sudoku.Length)
        {
            this.isFinished = true;
            return sudoku;
        }

        Node cell = sudoku.sudoku[index];
        
        //If a cell is given, ea not swappable, we skip the cell
        if (!cell.Swappable)
        {
            this.CBTAlg(sudoku: sudoku, index: index + 1);
        }

        //we instantiate the number of the node/cell based on it's domaincounter
        cell.DomainCounter += 1;
  
        if (cell.Domain != null)
        {
            cell.Number = cell.Domain[cell.DomainCounter];
        
            this.forwardChecking(cell, sudoku);

            //we check whether the forwardchecking does not lead to empty domains. If it does, we backtrack and do DFS on the next available branch 
            if (IsNotEmpty(sudoku))
            {
                this.CBTAlg(sudoku: sudoku, index: index + 1);
            }
            else
            {
                //backtrack consists of undoing the domainupdates (so updating the node.Domain for all the effected nodes) and doing CBT on the next available option
                if(cell.DomainCounter >= (cell.Domain.Count()-1))
                {
                    this.BackTrack(sudoku: sudoku, cellIndex: index);
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
        }
        return sudoku;
        
    }

    //Backtrack is based on the domaincounter and the index of the cell. We take a cell, check whether there are more options in the domain
    //(so whether the current number is not the last number in the domain)
    //and if not, we make another branch and continue with CBT
    //if it is, we go back one cell (based on the index) and do backtrack again
    void BackTrack(Board sudoku, int cellIndex = 0)
    {        
        Node cell = sudoku.sudoku[cellIndex];
        
        if (!cell.Swappable)
        {
            int backTrackCellIndex = cellIndex - 1;
            this.BackTrack(sudoku, backTrackCellIndex);
        }
        
        this.undoDomainUpdate(sudoku: sudoku, cell: cell);
        
        if(!this.isFinished) 
        {
            if(cell.DomainCounter == (cell.Domain.Count() - 1))
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

    //Method that prints the domain and board 
    private void Debug(Board sudoku)
    {
        Console.ReadLine();
        sudoku.Print();
        this.printDomains(sudoku);
        
    }
    
    //This is a method for debugging purposes that is used to print the domains of the cells
    private void printDomains(Board sudoku)
    {
        foreach (Node cell in sudoku.sudoku)
        {
            if (cell.Swappable)
            {
                Solver.DisplaySet(cell.Domain);
            }
        }
    }

    //UndoDomainUpdates is essantially the feedbackforward method, but the other way around. It adds the number to all the domains of the effected cells
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
        //After adding the number to all the domains, we clear the list of effectedcells. Afterwards, we built this list again with forwardchecking.
        cell.EffectedCells.Clear();
    }

    //Forwardchecking removes the initiated number from the domain of the effected cells (in the row, column and the block)
    private void forwardChecking(Node cell, Board sudoku)
    {
        // Each effected node is added to the effectedcells list and the number is removed from the domain 
        // Handle the rows
        this.HandleEffectedCells(cell: cell, effectedList: sudoku.RowsSwappable[cell.Row]);
        
        // Handle the columns
        this.HandleEffectedCells(cell: cell, effectedList: sudoku.ColumnsSwappable[cell.Column]);
        
        // Handle the current block
        this.HandleEffectedCells(cell: cell, effectedList: sudoku.BlocksSwappable[cell.Block]);
    }

    private void HandleEffectedCells(Node cell, List<Node> effectedList)
    {
        foreach (Node effected in effectedList)
        {
            if (effected != cell && effected.Domain.Contains(cell.Number) && !cell.EffectedCells.Contains(effected))
            {
                cell.EffectedCells.Add(effected);
                effected.Domain.Remove(cell.Number);
            }
        }
    }

    //this method checks whether there are no empty domains in the cells on the board
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