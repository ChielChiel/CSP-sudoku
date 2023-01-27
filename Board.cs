class Board 
{

    public Node[] sudoku;
    public List<List<int>> blocks;
    public List<List<int>> rows;
    public List<List<int>> columns;

    public List<List<Node>> blocksSwappable;
    public List<List<Node>> rowsSwappable;
    public List<List<Node>> columnsSwappable;
    public List<List<int>> blocksSet;


    public List<List<int>> Rows
    {
        get { return rows; }
        set { rows = value; }
    }
    public List<List<int>> Columns
    {
        get { return columns; }
        set { columns = value; }
    }

    public List<List<int>> BlocksSet
    {
        get { return blocksSet; }
        set { blocksSet = value; }
    }

    public List<List<Node>> BlocksSwappable
    {
        get { return blocksSwappable; }
        set { blocksSwappable = value; }
    }
    public List<List<Node>> RowsSwappable
    {
        get { return rowsSwappable; }
        set { rowsSwappable = value; }
    }
    public List<List<Node>> ColumnsSwappable
    {
        get { return columnsSwappable; }
        set { columnsSwappable = value; }
    }


    public Board(int[] sudoku_array) {
        this.sudoku = this.Create_Board(sudoku_array);
        this.UpdateBlocks(onlySwappableNumbers: false);
    }

    // Returns from the flat array arrays with the indexes, sorted in blocks. FI: the numbers  [0, 1, 2, 9, 10, 11, 18, 19, 20] are in block 0
    public void UpdateBlocks(bool onlySwappableNumbers = false)
    {
        this.blocks = new List<List<int>>();
        int numberOfRows = (int)Math.Sqrt(this.sudoku.Length);

        int blockNumber;

        for (int i = 0; i < numberOfRows; i++) 
        {
            this.blocks.Add(new List<int>());
        }
        
        for (int i = 0; i < this.sudoku.Length; i++)
        {

            blockNumber = (int)((this.sudoku[i].Row / 3) * 3) + (int)((this.sudoku[i].Column) / 3);
          
            if (onlySwappableNumbers) {
                if (this.sudoku[i].Swappable) {
                    this.blocks[blockNumber].Add(i);
                }
            } else {
                this.blocks[blockNumber].Add(i);
            }
        }
    }

    // Method used for pretty printing the sudoku array as a proper 9x9 sudoku. Blue color is used to indicate that
    // Nodes that are unswappable.
    public void Print()
    {
        string row;
        int numberOfRows = (int) Math.Sqrt(sudoku.Length);
        for (int i = 0; i < numberOfRows; i++)
        {
            row = "";
            for (int j = 0; j < numberOfRows; j++)
            {
                if ((j + (i * numberOfRows)) % 3 == 0) {
                    row += "|";
                }
                if(sudoku[j+(i*numberOfRows)].Swappable == false) {
                    row += "\x1b[36m" + sudoku[j+(i*numberOfRows)].Number.ToString() + "\x1b[0m ";
                } else {
                    row += sudoku[j+(i*numberOfRows)].Number.ToString() + " ";
                }
                
            }
            if ((i % 3) == 0)
                Console.WriteLine("--------------------");
           
            Console.WriteLine(row);
        }
    }

    // Just creates a starting sudoku.
    public Node[] Create_Board(int[] sudoku_array) {
        Node[] board = new Node[sudoku_array.Length];

        rows = new List<List<int>>();
        columns = new List<List<int>>();
        blocksSet = new List<List<int>>();
        blocksSwappable = new List<List<Node>>();
        rowsSwappable = new List<List<Node>>();
        columnsSwappable = new List<List<Node>>();

        for (int i = 0; i < 9; i++)
        {
            rows.Add(new List<int>());
            columns.Add(new List<int>());
            blocksSet.Add(new List<int>());
            blocksSwappable.Add(new List<Node>());
            rowsSwappable.Add(new List<Node>());
            columnsSwappable.Add(new List<Node>());
        }
        for (int i = 0; i < sudoku_array.Length; i++)
        {
            // Add meta information about a number in the sudoku.
            
            Node cell = new Node();
            cell.Swappable = (sudoku_array[i] == 0);
            cell.Number = sudoku_array[i];
            
            Coordinate positie = GetCoordinate(i);
            cell.Row = positie.Y;
            cell.Column = positie.X;
            int blockNumber = ((int)(positie.Y / 3) * 3) + (int)((positie.X) / 3);
            cell.Block = blockNumber;
            cell.EffectedCells = new List<Node>();
            if (sudoku_array[i] == 0)
            {
                // cell is swappable
                cell.Domain = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9};
                this.blocksSwappable[cell.Block].Add(cell);
                this.rowsSwappable[cell.Row].Add(cell);
                this.columnsSwappable[cell.Column].Add(cell);

            }
            else
            {
                // cell is not swappable
                rows[positie.Y].Add(sudoku_array[i]);
                columns[positie.X].Add(sudoku_array[i]);
                blocksSet[cell.Block].Add(sudoku_array[i]);
               
            }
            this.Rows = rows;
            board[i] = cell;
        }
        
        return board;
    } 


    // Maps the flattened position of sudoku array to a coordinate in the sudoku.
    // Coordinates from (0,0) top-left to (8,8) bottom right
    public Coordinate GetCoordinate(int flat_position) {
        int x = 0;
        int y = 0;
        y = (int)Math.Floor(flat_position / 9.0);
        x = flat_position - (y * 9);
        return new Coordinate(x, y);
    }
}

// Helper class to project a coordinate in our sudoku
class Coordinate {

    // The X coordinate, also the column
    public int X;

    // The Y coordinate, also the row
    public int Y;

    public Coordinate(int x, int y) {
        this.X = x;
        this.Y = y;
    }

    public override string ToString()
    {
        return "X = " + this.X + ", Y = " + this.Y;
    }
}
