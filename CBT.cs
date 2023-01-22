
using System.Diagnostics;

class CBT
{

    // public void CBTs(string input)
    // {
    //     Console.WriteLine("kaas");

    // }
    public bool Done = false;
    public Board CBTAlg(Board sudoku, int index = 0) 
    {


        //Deze counter doet het nu alleen voor de eerste waarde voor debugging purposes

        // Console.ReadLine();
        if (index >= sudoku.sudoku.Length)
        {
            Console.WriteLine("we're done here");
            sudoku.Print();
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
        }
       // Debug(sudoku);

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
        return sudoku;
        
    }


    void BackTrack(Board sudoku, int cellIndex = 0)
    {
        // Undo

        
        Node cell = sudoku.sudoku[cellIndex];
        
        if (!cell.Swappable)
        {
            int backTrackCellIndex = cellIndex - 1;
            this.BackTrack(sudoku, backTrackCellIndex);
        }
        this.undoDomainUpdate(sudoku: sudoku, cell: cell);
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
        //cell.DomainCounter = cell.DomainCounter - 1;
       
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
                if (effected.Row == 4 && effected.Column == 6)
                { }
                cell.EffectedCells.Add(effected);
                effected.Domain.Remove(cell.Number);
            }
        }
    }

    bool IsNotEmpty(Board sudoku)
    {
        // Niks mag leeg zijn
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


    // Board FillValuesNodesWithDomain(Board sudoku)
    // {
    //     return new Board();
    // }



    /*
    // knoop consistent maken door middel van de code van node consistency
    nodeConsistency()
    {

    }

    // beginnen met de eerste cel en doorwerken tot nummer 81
    functie CBT()
    {   
        huidigeCel = variabel

        // beginnen met algoritme door cellen te vullen
        if (waarde huidigeCel leeg)
        {
            waarde huidigeCel = waarde uit domein van huidigeCel op basis van domeincounter 
            forwardChecking()
        }
        else
        {
            if (partialSolution())
                ga naar volgende cel (huidigeCel +1)
            else
            {
                maak waarde huidigeCel leeg
                plaats waarde terug in omliggende domeinen (rij kolom en box)
                if (domein counter+1 > grootte van het domein) // oftewel hij overschreidt de grootte
                    zet domein counter op 0 oftewel beginpositie
                    spring een cel terug (huidigeCel -1)
                else
                    domeincounter++ 
            }
        }
    }

    forwardChecking()
    {
        for(in elke rij kolom en box van deze cel, de waarde verwijderen uit het domein)
        {
            if (waarde in domein)
                verwijder waarde uit domein
        }
    }

    bool partialSolution()
    {
        // controleren of een domein leeg is of niet. (mogelijk dit niet de beste locatie hiervoor, maar voor overzicht)
        for(in elke rij kolom en box van deze cel, de waarde verwijderen uit het domein)
        {
            if (domein leeg of geen partiele oplossing) // domein is leeg oftewel er is een fout gemaakt
            {
                return false
            }
            else // domein niet leeg, dus gaat nog goed
            {

            }
        }
        
        return true
    }

    */
}