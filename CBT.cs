
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