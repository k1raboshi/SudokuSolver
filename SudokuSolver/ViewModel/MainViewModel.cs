using SudokuSolver.Commands;
using SudokuSolver.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SudokuSolver.ViewModel
{

	class MainViewModel
	{
		private const int N = 9;
		public ObservableCollection<SudokuCell> SudokuGrid { get; set; }
        public ICommand SolveSudokuCommand { get; set; }

        public MainViewModel() 
        {
			SudokuGrid = new ObservableCollection<SudokuCell>();
			for(int i = 0; i < N * N; i++)
			{
				SudokuGrid.Add(new SudokuCell(i,0));
			}
            SolveSudokuCommand = new RelayCommand(SolveSudoku, CanSolveSudoku);
		}

		private bool CanSolveSudoku(object obj)
        {
            return true;
        }
		public static bool IsSafe(int[,] sudoku, int rowIndex, int colIndex, int num)
		{
			int smallGridSide = (int)Math.Sqrt(sudoku.GetLength(0));
			int boxStartRow = rowIndex - rowIndex % smallGridSide;
			int boxStartCol = colIndex - colIndex % smallGridSide;

			for (int i = 0; i < sudoku.GetLength(0); i++)
			{
				if (sudoku[rowIndex, i] == num)
					return false;

				if (sudoku[i, colIndex] == num)
					return false;
			}

			for (int i = 0; i < smallGridSide; i++)
			{
				for (int j = 0; j < smallGridSide; j++)
				{
					if (sudoku[boxStartRow + i, boxStartCol + j] == num)
						return false;
				}
			}

			return true;
		}
		public static bool SolveSudoku(int[,] sudoku, int n)
		{
			int row = -1;
			int col = -1;
			bool foundZero = false;

			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					if (sudoku[i, j] == 0)
					{
						row = i;
						col = j;
						foundZero = true;
						break;
					}
				}

				if (foundZero)
				{
					break;
				}
			}

			if (!foundZero) return true;

			for (int i = 1; i < n + 1; i++)
			{
				if (IsSafe(sudoku, row, col, i))
				{
					sudoku[row, col] = i;

					if (SolveSudoku(sudoku, n))
						return true;
					else
						sudoku[row, col] = 0;
				}
			}

			return false;
		}
		private void SolveSudoku(object obj)
        {
			
			var sudokuGridArr = SudokuGrid.ToArray();
			int[] arr = new int[sudokuGridArr.Length];
			for (int i = 0; i < sudokuGridArr.Length; i++)
			{
				arr[i] = sudokuGridArr[i].Value;
			}
			int[,] grid = new int[N, N];

			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
				{
					grid[i, j] = arr[i * N + j];
				}
			}

			SolveSudoku(grid, N);

			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
				{
					SudokuGrid[i * N + j] = new SudokuCell(i * N + 1, grid[i, j]);
				}
			}

		}

    }
}
