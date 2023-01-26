class Node
{
    private bool swappable;
    private int number;  
    private int row;
    private int column;
    private int block;
    private List<int> domain;
    private List<Node> effectedCells;

    private int domainCounter = -1;

    public Node() { }

    public bool Swappable
    {
        get { return swappable; }
        set { swappable = value; }
    }

    public List<int> Domain
    {
        get { return domain; }
        set { domain = value;  }
    }
    public List<Node> EffectedCells
    {
        get { return effectedCells; }
        set { effectedCells = value; }
    }

    public int DomainCounter
    {
        get { return domainCounter; }
        set { domainCounter = value; }
    }


    public int Number
    {
        get { return number; }
        set { number = value; }
    }
    public int Block
    {
        get { return block; }
        set { block = value; }
    }

    public int Row
    {
        get { return row; }
        set { row = value; }
    }

    public int Column
    {
        get { return column; }
        set { column = value; }
    }


}