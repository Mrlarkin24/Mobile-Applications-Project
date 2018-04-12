using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SlidingPuzzle
{
    public sealed partial class MainPage : Page
    {
        #region Global Variables _rows is essential, don't delete it
        int _rows;
        const int _iHeight = 55;
        int _iWidth = 55;
        #endregion

        #region Constructor and set up code
        public MainPage() // constructor
        {
            this.InitializeComponent();
            // i+=1
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //// create another ellipse
            //Ellipse el2 = new Ellipse();
            //el2.Name = "el2";
            //el2.Height = 200;
            //el2.Width = 300;
            //el2.HorizontalAlignment = HorizontalAlignment.Left;
            //el2.VerticalAlignment = VerticalAlignment.Top;
            //el2.Fill = new SolidColorBrush(Colors.Blue);
            //// add it to a collection of children
            //parentGrid.Children.Add(el2);

            //// add the event handler
            //el2.Tapped += ellipse_Tapped;

        }

        // page loaded function
        // navigated to method

        #endregion

        #region Event Handlers for Controls
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // local variable for the sender
            RadioButton current = (RadioButton)sender;
            // get the number to size the chess board with from the tag of the sender
            // create global var to hold this value
            //_rows = (int)current.Tag;
            _rows = Convert.ToInt32(current.Tag);
            // create method to generate the chess board
            createChessBoard();
            setupThePieces();

        }

        private void setupThePieces()
        {

            // check the size of board and decide how many cats, how many mice
            //int numCats = _rows / 2;
            Ellipse cat;
            Grid board = FindName("ChessBoard") as Grid;
            int x = 0;

            // cats = red ellipse, width = 50, height = 50
            for (int i = 0; i <= (_rows - 1); i++)
            {


                for (int j = 0; j <= (_rows - 1); j++)
                {
                    cat = new Ellipse();
                    cat.Name = "cat" + (x + 1).ToString();
                    cat.Height = _iHeight * 0.75;
                    cat.Width = _iWidth * 0.75;
                    cat.HorizontalAlignment = HorizontalAlignment.Center;
                    cat.VerticalAlignment = VerticalAlignment.Center;
                    cat.Fill = new SolidColorBrush(Colors.Red);

                    if (i == 1 && j == 1)
                    {
                        //Used to leave one space empty on the board 
                    }

                    else
                    {
                        cat.SetValue(Grid.RowProperty, i);
                        cat.SetValue(Grid.ColumnProperty, j);

                        cat.Tapped += El1_Tapped;
                        board.Children.Add(cat);
                        x++;
                    }
                }

            }

            // mouse = green ellipse, same width
            // create _rows number of ellipses for cats
            // create one for the mouse
            Ellipse mouse = new Ellipse();
            mouse.Name = "theMouse";
            mouse.Height = _iHeight * 0.75;
            mouse.Width = _iWidth * 0.75;
            mouse.HorizontalAlignment = HorizontalAlignment.Center;
            mouse.VerticalAlignment = VerticalAlignment.Center;
            mouse.Fill = new SolidColorBrush(Colors.Green);
            mouse.SetValue(Grid.RowProperty, 1);
            mouse.SetValue(Grid.ColumnProperty, 1);
            
            board.Children.Add(mouse);

            // add event handler to el1
            foreach (var item in board.Children)
            {
                if (item.GetType() == typeof(Ellipse))
                {

                }

            }


        }

        bool mFirst = false;
        Ellipse moveMe1, moveMe2;
        private void El1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int rHolder, cHolder;
            int mouseRow, mouseColumn;

            Ellipse current = (Ellipse)sender;
            moveMe1 = current;

            moveMe2 = FindName("theMouse") as Ellipse;

            mouseRow = (int)moveMe2.GetValue(Grid.RowProperty);
            mouseColumn = (int)moveMe2.GetValue(Grid.ColumnProperty);

            rHolder = (int)current.GetValue(Grid.RowProperty);
            cHolder = (int)current.GetValue(Grid.ColumnProperty);

            //Check should it be able to move up or down
            if (((rHolder - 1) == mouseRow && cHolder == mouseColumn) || ((rHolder + 1) == mouseRow && cHolder == mouseColumn))
            {
                moveMe1.SetValue(Grid.RowProperty, mouseRow);
                moveMe1.SetValue(Grid.ColumnProperty, mouseColumn);

                moveMe2.SetValue(Grid.RowProperty, rHolder);
                moveMe2.SetValue(Grid.ColumnProperty, cHolder);
            }
            //Check should it be able to move left or right
            else if ((rHolder == mouseRow && (cHolder - 1) == mouseColumn) || (rHolder == mouseRow && (cHolder + 1) == mouseColumn))
            {
                moveMe1.SetValue(Grid.RowProperty, mouseRow);
                moveMe1.SetValue(Grid.ColumnProperty, mouseColumn);

                moveMe2.SetValue(Grid.RowProperty, rHolder);
                moveMe2.SetValue(Grid.ColumnProperty, cHolder);
            }
        }

        private void createChessBoard()
        {
            /*
             * the try catch block will try to remove any existing chess board so that
             * there is only one on the parent grid at any given time.  It will only fail
             * when there is no grid to remove, so the catch "falls through" and the code
             * executes as expected.
             */
            try
            {
                parentGrid.Children.Remove(FindName("ChessBoard") as Grid);
            }
            catch
            {
            }
            // create a grid object
            Grid grdBoard = new Grid();
            // give it a name, size, horizontal alignment, vertical align
            // give it background colour, margin of 5, Grid.row = 1
            grdBoard.Name = "ChessBoard";
            grdBoard.HorizontalAlignment = HorizontalAlignment.Center;
            grdBoard.VerticalAlignment = VerticalAlignment.Top;
            grdBoard.Height = _iHeight * _rows;
            grdBoard.Width = _iWidth * _rows;
            grdBoard.Background = new SolidColorBrush(Colors.Gray);
            grdBoard.Margin = new Thickness(5);
            grdBoard.SetValue(Grid.ColumnProperty, 1);
            grdBoard.SetValue(Grid.RowProperty, 1);

            // add _rows number of row definitions and column definitions
            for (int i = 0; i < _rows; i++)
            {
                grdBoard.RowDefinitions.Add(new RowDefinition());
                grdBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // add the chessboard to the root grid children collection
            parentGrid.Children.Add(grdBoard);

            // add a border object to each cell on the grid
            // to add one border
            // create the border object
            Border brdr; // don't put this in the loop

            int iR, iC;

            for (iR = 0; iR < _rows; iR++) // on each row
            {
                for (iC = 0; iC < _rows; iC++)  // for each col on that row
                {
                    #region Create one border element and add to the grid
                    brdr = new Border();
                    // give it height, width, horizontal & vertical align in centre
                    brdr.Height = _iHeight * 0.98;
                    brdr.Width = _iWidth * 0.98;
                    // sq_R_C, no duplicates here 
                    brdr.Name = iR.ToString() + iC.ToString();
                    brdr.HorizontalAlignment = HorizontalAlignment.Center;
                    brdr.VerticalAlignment = VerticalAlignment.Center;
                    // set the Grid.col, grid.row property
                    brdr.SetValue(Grid.RowProperty, iR);
                    brdr.SetValue(Grid.ColumnProperty, iC);
                    // give it a background colour
                    brdr.Background = new SolidColorBrush(Colors.White);
                    if (0 == (iR + iC) % 2) // bottom left is black on 8
                    {
                        brdr.Background = new SolidColorBrush(Colors.White);
                    }
                    // add it to the chess board children collection
                    grdBoard.Children.Add(brdr);
                    #endregion
                } // end iC
            } // end of iR
        }// end of createChessBoard
        #endregion
    }// end of main
}// end of SlidingPuzzle