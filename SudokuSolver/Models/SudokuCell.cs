using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Models
{
    class SudokuCell
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public SudokuCell(int id, int value)
        {
            Id = id;
            Value = value;
        }
    }
}
