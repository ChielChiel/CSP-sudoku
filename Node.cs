class Node
{
    private bool swappable;
    private int number;  
    private int row;
    private int column;
    private int block;
    private HashSet<int> domain;
    public Node()
    { 
    }

    public bool Swappable
    {
        get { return swappable; }
        set { swappable = value; }
    }

    public HashSet<int> Domain
    {
        get { return domain; }
        set { domain = value;  }
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