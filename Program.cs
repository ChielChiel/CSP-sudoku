namespace sudoku
{
    class Progrm
    {
        static void Main(string[] args)
        {
            // Formatting for pretty printing the results of the programm
            Console.WriteLine("\x1b[1mCBT sudoku solver\x1b[0m");
            
            string path_to_file;
            if(args.Length > 0)
            {
                path_to_file = args[0];
                Console.WriteLine($"Sudoku's will be read from the file '{path_to_file}'.");  
            }  
            else
            {
                path_to_file = "Sudoku_puzzels_5.txt";
                Console.WriteLine($"No sudoku file specified. Sudoku's will be read from the default file '{path_to_file}'."); 
            }
            
            int[,] all_sudokus = ReadFromFile.ReadTXT(@$"{path_to_file}");
            int number_of_sudokus = all_sudokus.GetLength(0);
            Board sudoku;
            for (int i = 0; i < number_of_sudokus; i++)
            {
                Console.WriteLine($"\n\x1b[1mSudoku number: {i + 1}\x1b[0m");
                // Get the sudoku
                sudoku = new Board(all_sudokus.GetRow(i));
                
                // Instantiate a Solver object with the given sudoku
                Solver solver = new Solver(sudoku);

                // Get the solution of the solver
                Board solution = solver.GetSolution();

                Console.WriteLine("This problem took: " + solver.GetRunTime() + " seconds to complete. The following board is the solution:");
                solution.Print();
            }
        }
    }

    // Reference: https://stackoverflow.com/a/1183086/8902440
    // & https://stackoverflow.com/a/51241629/8902440
    public static class Extension
    {
        public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }
}