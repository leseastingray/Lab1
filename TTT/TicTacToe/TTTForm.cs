﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TTTForm : Form
    {
        public TTTForm()
        {
            InitializeComponent();
            DisableAllSquares();
            ResetArray();
            ResetSquares();
            EnableAllSquares();
        }

        const string USER_SYMBOL = "X";
        const string COMPUTER_SYMBOL = "O";
        const string EMPTY = "";

        const int SIZE = 5;

        // constants for the 2 diagonals
        const int TOP_LEFT_TO_BOTTOM_RIGHT = 1;
        const int TOP_RIGHT_TO_BOTTOM_LEFT = 2;

        // constants for IsWinner
        const int NONE = -1;
        const int ROW = 1;
        const int COLUMN = 2;
        const int DIAGONAL = 3;

        // Rectangular array to hold Xs and Os
        private string[,] board = new string[5, 5];
        

        // This method takes a row and column as parameters and 
        // returns a reference to a label on the form in that position
        private Label GetSquare(int row, int column)
        {
            int labelNumber = row * SIZE + column + 1;
            return (Label)(this.Controls["label" + labelNumber.ToString()]);
        }

        // This method does the "reverse" process of GetSquare
        // It takes a label on the form as its parameter and
        // returns the row and column of that square as output parameters
        private void GetRowAndColumn(Label l, out int row, out int column)
        {
            int position = int.Parse(l.Name.Substring(5));
            row = (position - 1) / SIZE;
            column = (position - 1) % SIZE;
        }

        // Modify this so it uses the array rather than a square in the UI 
        // This method takes a row (in the range of 0 - 4) and returns true
        // if the row on the form contains 5 Xs or 5 Os.
        // Use it as a model for writing IsColumnWinner
        private bool IsRowWinner(int row)
        {
            string symbol = board [row, 0];

            if (String.Equals(symbol, EMPTY))
            {
                return false;
            }

            for (int col = 1; col < SIZE; col++)
            {
                if (!String.Equals(board[row, col], symbol))
                {
                    return false;
                }
            }
            return true;
        }
        

        private bool IsAnyRowWinner()
        {
            return true;
        }

        private bool IsColumnWinner(int col)
        {
            string symbol = board [0, col];

            if (String.Equals(symbol, EMPTY))
            {
                return false;
            }
            for (int row = 1; row < SIZE; row ++)
                {
                    if (!String.Equals(board [row, col], symbol))
                        return false;
                }
                return true;
        }

        private bool IsAnyColumnWinner()
        {
            return true;
        }

        private bool IsDiagonal1Winner()
        {
            string symbol = board [0, 0];

            if (String.Equals(symbol, EMPTY))
            {
                return false;
            }

            for (int row = 1, col = 1; row < SIZE; row ++, col ++)
            {
                if (!String.Equals(board [row,col], symbol))
                    return false;
            }
            return true;
        }

        private bool IsDiagonal2Winner()
        {
            string symbol = board[0, SIZE - 1];

            if (String.Equals(symbol, EMPTY))
            {
                return false;
            }

            for (int row = 1, col = SIZE - 2; row < SIZE; row++, col--)
            {
                if (!String.Equals(board [row,col], symbol))
                    return false;
            }
            return true;
        }

        private bool IsAnyDiagonalWinner()
        {
            return true;
        }

        private bool IsFull()
        {
            /* for all the rows beginning at 0 and less than SIZE increment by 1
             *     for all the cols beginning at 0 and less than SIZE increment by 1
             *         call GetSquare method
             *         if text of square is empty
             *             return false
             * return true
             */
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    if (String.Equals(square.Text, EMPTY))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // This method determines if any row, column or diagonal on the board is a winner.
        // It returns true or false and the output parameters will contain appropriate values
        // when the method returns true.  See constant definitions at top of form.
        private bool IsWinner(out int whichDimension, out int whichOne)
        {
            // rows
            // for row starting at 0 and less than SIZE and incremented by 1
            for (int row = 0; row < SIZE; row++)
            {
                // if there is a winner in a row
                //    whichDimension = ROW
                //    whichOne = the winning row
                //    return true
                if (IsRowWinner(row))
                {
                    whichDimension = ROW;
                    whichOne = row;
                    return true;
                }
            }
            // columns
            for (int column = 0; column < SIZE; column++)
            {
                if (IsColumnWinner(column))
                {
                    whichDimension = COLUMN;
                    whichOne = column;
                    return true;
                }
            }
            // diagonals
            if (IsDiagonal1Winner())
            {
                whichDimension = DIAGONAL;
                whichOne = TOP_LEFT_TO_BOTTOM_RIGHT;
                return true;
            }
            if (IsDiagonal2Winner())
            {
                whichDimension = DIAGONAL;
                whichOne = TOP_RIGHT_TO_BOTTOM_LEFT;
                return true;
            }
            whichDimension = NONE;
            whichOne = NONE;
            return false;
        }

        // I wrote this method to show you how to call IsWinner
        private bool IsTie()
        {
            int winningDimension, winningValue;
            return (IsFull() && !IsWinner(out winningDimension, out winningValue));
        }

        // This method takes an integer in the range 0 - 4 that represents a column
        // as it's parameter and changes the font color of that cell to red.
        private void HighlightColumn(int col)
        {
            for (int row = 0; row < SIZE; row++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        // This method changes the font color of the top right to bottom left diagonal to red
        // I did this diagonal because it's harder than the other one
        private void HighlightDiagonal2()
        {
            for (int row = 0, col = SIZE - 1; row < SIZE; row++, col--)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        // This method will highlight either diagonal, depending on the parameter that you pass
        private void HighlightDiagonal(int whichDiagonal)
        {
            if (whichDiagonal == TOP_LEFT_TO_BOTTOM_RIGHT)
                HighlightDiagonal1();
            else
                HighlightDiagonal2();

        }

        private void HighlightRow(int row)
        {
            for (int col = 0; col < SIZE; col++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        private void HighlightDiagonal1()
        {
            for (int row = 0, col = 0; row < SIZE; row++, col++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        private void HighlightWinner(string player, int winningDimension, int winningValue)
        /* switch based on winningDimension value
         *     in case ROW
         *         call HighlightRow method
         *         display win in resultLabel text
         *         end case
         *     in case COLUMN
         *         call HighlightColumn method
         *         display win in resultLabel text
         *         end case
         *     in case DIAGONAL
         *         call HighlightDiagonal method
         *         display win in resultLabel text
         *         end case
         */
        {
            switch (winningDimension)
            {
                case ROW:
                    HighlightRow(winningValue);
                    resultLabel.Text = (player + " wins!");
                    break;
                case COLUMN:
                    HighlightColumn(winningValue);
                    resultLabel.Text = (player + " wins!");
                    break;
                case DIAGONAL:
                    HighlightDiagonal(winningValue);
                    resultLabel.Text = (player + " wins!");
                    break;
            }
        }

        private void ResetSquares()
            /* for all the rows beginning at 0 and less than SIZE
             *     for all the columns beginning at 0 and less than SIZE
             *         call GetSquare(row, col) method
             *         clear square label text
             *         color of square label to black
             */
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    square.Text = EMPTY;
                    square.ForeColor = Color.Black;
                }
            }
            // Clear the resultLabel
            resultLabel.Text = EMPTY;

        }
        private void ResetArray()
        {
            // for all the rows beginning at 0 and less than SIZE
            //    for all the cols beginning at 0 and less than SIZE
            //        clear the label text
            //        color of label to black
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    board[row, col] = EMPTY;
                }
            }
        }
        // Modify this so it uses the array rather than the UI to make the move.
        // Setting the text and disabling the square will happen in the
        // SyncArrayAndSquares method
        private void MakeComputerMove()
        {
            /* random generator = new Random()
             * int row = generator.Next(0, 3)
             * do
             *     row = random number btwn 0 and 4
             *     col = random number btwn 0 and 4
             *     square = GetSquare(row, col)
             * while there is something in the square (row, col)
             *  or the square is not empty
             */
            Random rand = new Random();
            int row;
            int col;

            do
            {
                row = rand.Next(0, SIZE);
                col = rand.Next(0, SIZE);
                if (String.Equals(board[row, col], EMPTY))
                {
                    board[row, col] = COMPUTER_SYMBOL;
                    break;
                }
            } while (!String.Equals(board[row, col], EMPTY) && !IsFull());
        }

        // Setting the enabled property changes the look and feel of the cell.
        // Instead, this code removes the event handler from each square.
        // Use it when someone wins or the board is full to prevent clicking a square.
        private void DisableAllSquares()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    DisableSquare(square);
                }
            }
        }

        // Inside the click event handler you have a reference to the label that was clicked
        // Use this method (and pass that label as a parameter) to disable just that one square
        private void DisableSquare(Label square)
        {
            square.Click -= new System.EventHandler(this.label_Click);
        }

        // You'll need this method to allow the user to start a new game
        private void EnableAllSquares()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    square.Click += new System.EventHandler(this.label_Click);
                }
            }
        }

        // This method should set the text property of each square in the UI
        // to the value in the corresponding element of the array and disable
        // the squares that are not empty (you don't have to enable the others
        // because they're enabled by default)
        private void SyncArrayAndSquares()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    square.Text = board[row, col];
                    if (!String.Equals(square.Text, EMPTY))
                    {
                        DisableSquare(square);
                    }
                }
            }
        }
        //* TODO:  Modify this so it uses the array and UI methods properly
        private void label_Click(object sender, EventArgs e)
        {
            int winningDimension = NONE;
            int winningValue = NONE;


            /* Click label
             * Change clicked label text to USER_SYMBOL
             * Disable clicked label
             */
            int row, col;

            Label clickedLabel = (Label)sender;
            DisableSquare(clickedLabel);

            GetRowAndColumn(clickedLabel, out row, out col);
            board[row, col] = USER_SYMBOL;
            SyncArrayAndSquares();
            
            /* If user wins on this move,
             *     disable all squares
             *     call HighlightWinner method
             * Else if (IsFull()) is true
             *     disable all squares
             *     display "It's a tie!" in resultLabel
             * Else
             *     call MakeComputerMove() method
             */
            if (IsWinner(out winningDimension, out winningValue))
            {
                DisableAllSquares();
                HighlightWinner("User", winningDimension, winningValue);
            }
            else if (IsFull())
            {
                DisableAllSquares();
                resultLabel.Text = "It's a tie!";
            }
            else
            {
                MakeComputerMove();
           
                SyncArrayAndSquares();

                /* if IsWinner is true after computer move
                 *    disable all squares
                 *    call HighlightWinner() method
                 * else if IsFull is true
                 *    disable all squares
                 *    display "It's a tie!" in resultLabel text
                 */
                if (IsWinner(out winningDimension, out winningValue))
                {
                    DisableAllSquares();
                    HighlightWinner("Computer", winningDimension, winningValue);
                }
                else if (IsFull())
                {
                    resultLabel.Text = "It's a tie!";
                }
            }
        }

        private void newGameButton_Click(object sender, EventArgs e)
        /* newGameButton click
        * calls DisableAllSquares()
        * calls ResetSquares()
        * calls EnableAllSquares();       
        */
        {
            DisableAllSquares();
            ResetArray();
            ResetSquares();
            EnableAllSquares();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
