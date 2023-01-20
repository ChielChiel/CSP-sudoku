
using System.Diagnostics;

class CBT
{

    public void CBT()
    {
        Console.WriteLine("kaas");
    }

    public void CBTAlg(Board sudoku, int index = 0) 
    {
        

        //Deze counter doet het nu alleen voor de eerste waarde voor debugging purposes
        for (int i = 0; i < 1; i++)
        {
            Node cell = sudoku.sudoku[i];
            if (!cell.Swappable)
            {
                continue; 
            }

            
            cell.DomainCounter += 1;
            cell.Number = cell.Domain[cell.DomainCounter];
            forwardChecking(cell, sudoku);
            if (IsNotEmpty(sudoku))
            {
                //ga door
                CBTAlg(sudoku: sudoku, index: index + 1);


            }
            else
            {
                //backtrack
                if(cell.DomainCounter >= cell.Domain.Count())
                {
                    // Undo
                    // Backtrack(index--)
                }
                else
                {
                    // Undo
                    cell.DomainCounter =+ 1;
                }

            }
            // cell.DomainCounter++;
            //recursief verder

        }
        Debug(sudoku);
    }


    void BackTrack(int cellIndex = 0)
    {
        // Undo
        
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
                Solver.DisplaySet(cell.Domain);
        }
    }
    

    private void forwardChecking(Node cell, Board sudoku)
    {
        foreach (Node effected in sudoku.RowsSwappable[cell.Row])
        {
            effected.Domain.Remove(cell.Number);
        }
        foreach (Node effected in sudoku.ColumnsSwappable[cell.Column])
        {
            effected.Domain.Remove(cell.Number);
        }
        foreach (Node effected in sudoku.BlocksSwappable[cell.Block])
        {
            effected.Domain.Remove(cell.Number);
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