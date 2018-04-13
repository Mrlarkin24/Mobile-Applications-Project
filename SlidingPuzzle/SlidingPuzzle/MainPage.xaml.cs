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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SlidingPuzzle
{
    public sealed partial class MainPage : Page
    {
        #region Global Variables _rows is essential, don't delete it
        int _rows;
        const int _iHeight = 100;
        int _iWidth = 100;
        #endregion

        #region Constructor and set up code
        public MainPage() // constructor
        {
            this.InitializeComponent();
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

        //Makes an image object named mouse for later
        Image mouse;
        int moves = 0;
        private void setupThePieces()
        {
            //Makes an image object named cat for later
            Image cat;
            Grid board = FindName("ChessBoard") as Grid;
            int x = 0;

            // Two loops to add images in each space
            for (int i = 0; i < _rows; i++)
            {


                for (int j = 0; j < _rows; j++)
                {
                    if (i == 1 && j == 1)
                    {
                        //Used to leave one space empty on the board

                        //Counter for naming
                        x++;
                    }

                    else
                    {
                        //Call the CreateImage function and adds it to one of the cat objects 
                        cat = CreateImage(x);
                        cat.Name = "cat" + (x + 1).ToString();

                        //Adds image to one of the spaces in the grid and also them to be tapped
                        Grid.SetRow(cat, i);
                        Grid.SetColumn(cat, j);
                        board.Children.Add(cat);
                        cat.Tapped += El1_Tapped;
                        
                        //Counter for naming
                        x++;
                    }
                }

            }

            //Call the CreateImage function and adds it to one of the mouse objects 
            mouse = CreateImage(-1);
            mouse.Name = "Mouse";
            Grid.SetRow(mouse, 1);
            Grid.SetColumn(mouse, 1);
            board.Children.Add(mouse);

            // add event handler to el1
            foreach (var item in board.Children)
            {
                if (item.GetType() == typeof(Image))
                {

                }

            }


        }

        //Source for this part of the code: https://stackoverflow.com/questions/16742262/adding-image-to-grid-c-sharp

        //Creates string with the location of all the images 
        static String ImgName = "ms-appx://SlidingPuzzle/Assets/Image";

        //Used to add images
        private Image CreateImage(int cORm)
        {
            //Adds the full location for the needed image
            String imageName = ImgName + (cORm + 1).ToString() + ".png";

            //Adds image object to temporarily hold the image
            Image section = new Image();

            section.Width = (_iWidth * _rows)* 0.33;
            section.Height = (_iHeight * _rows) * 0.33;
            ImageSource sectionImage = new BitmapImage(new Uri(imageName));
            section.Source = sectionImage;

            return section;
        }

        Image moveMe1, moveMe2;
        private void El1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int rHolder, cHolder;
            int mouseRow, mouseColumn;

            Image current = (Image)sender;
            moveMe1 = current;

            moveMe2 = FindName("Mouse") as Image;

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

                moves++;
                endGame();
            }
            //Check should it be able to move left or right
            else if ((rHolder == mouseRow && (cHolder - 1) == mouseColumn) || (rHolder == mouseRow && (cHolder + 1) == mouseColumn))
            {
                moveMe1.SetValue(Grid.RowProperty, mouseRow);
                moveMe1.SetValue(Grid.ColumnProperty, mouseColumn);

                moveMe2.SetValue(Grid.RowProperty, rHolder);
                moveMe2.SetValue(Grid.ColumnProperty, cHolder);

                moves++;
                endGame();
            }
        }


        private void endGame()
        {
            Image check1 = FindName("cat1") as Image;
            int spaceCheck1 = (int)check1.GetValue(Grid.RowProperty);
            Image check2 = FindName("cat2") as Image;
            int spaceCheck2 = (int)check2.GetValue(Grid.RowProperty);
            Image check3 = FindName("cat3") as Image;
            int spaceCheck3 = (int)check3.GetValue(Grid.RowProperty);
            Image check4 = FindName("cat4") as Image;
            int spaceCheck4 = (int)check4.GetValue(Grid.RowProperty);
            Image check5 = FindName("Mouse") as Image;
            int spaceCheck5 = (int)check5.GetValue(Grid.RowProperty);
            Image check6 = FindName("cat6") as Image;
            int spaceCheck6 = (int)check6.GetValue(Grid.RowProperty);
            Image check7 = FindName("cat7") as Image;
            int spaceCheck7 = (int)check7.GetValue(Grid.RowProperty);
            Image check8 = FindName("cat8") as Image;
            int spaceCheck8 = (int)check8.GetValue(Grid.RowProperty);
            Image check9 = FindName("cat9") as Image;
            int spaceCheck9 = (int)check9.GetValue(Grid.RowProperty);

            if (spaceCheck1 == 0 && spaceCheck2 == 0 && spaceCheck3 == 0 && spaceCheck4 == 1 && spaceCheck5 == 1 && spaceCheck6 == 1 && spaceCheck7 == 2 && spaceCheck8 == 2 && spaceCheck9 == 2)
            {
                spaceCheck1 = (int)check1.GetValue(Grid.ColumnProperty);
                spaceCheck2 = (int)check2.GetValue(Grid.ColumnProperty);
                spaceCheck3 = (int)check3.GetValue(Grid.ColumnProperty);
                spaceCheck4 = (int)check4.GetValue(Grid.ColumnProperty);
                spaceCheck5 = (int)check5.GetValue(Grid.ColumnProperty);
                spaceCheck6 = (int)check6.GetValue(Grid.ColumnProperty);
                spaceCheck7 = (int)check7.GetValue(Grid.ColumnProperty);
                spaceCheck8 = (int)check8.GetValue(Grid.ColumnProperty);
                spaceCheck9 = (int)check9.GetValue(Grid.ColumnProperty);

                if (spaceCheck1 == 0 && spaceCheck2 == 1 && spaceCheck3 == 2 && spaceCheck4 == 0 && spaceCheck5 == 1 && spaceCheck6 == 2 && spaceCheck7 == 0 && spaceCheck8 == 1 && spaceCheck9 == 2)
                {
                    Grid board = FindName("ChessBoard") as Grid;

                    Image done;
                    done = CreateImage(4);
                    done.Name = "Done";

                    done.Width = (_iWidth * _rows) * 0.335;
                    done.Height = (_iHeight * _rows) * 0.335;

                    //Adds image to one of the spaces in the grid and also them to be tapped
                    Grid.SetRow(done, 1);
                    Grid.SetColumn(done, 1);
                    board.Children.Add(done);

                    board.Children.Remove(mouse);
                    check1.Tapped -= El1_Tapped;
                    check1.Width = (_iWidth * _rows) * 0.335;
                    check1.Height = (_iHeight * _rows) * 0.335;

                    check2.Tapped -= El1_Tapped;
                    check2.Width = (_iWidth * _rows) * 0.335;
                    check2.Height = (_iHeight * _rows) * 0.335;

                    check3.Tapped -= El1_Tapped;
                    check3.Width = (_iWidth * _rows) * 0.335;
                    check3.Height = (_iHeight * _rows) * 0.335;

                    check4.Tapped -= El1_Tapped;
                    check4.Width = (_iWidth * _rows) * 0.335;
                    check4.Height = (_iHeight * _rows) * 0.335;

                    check6.Tapped -= El1_Tapped;
                    check6.Width = (_iWidth * _rows) * 0.335;
                    check6.Height = (_iHeight * _rows) * 0.335;

                    check7.Tapped -= El1_Tapped;
                    check7.Width = (_iWidth * _rows) * 0.335;
                    check7.Height = (_iHeight * _rows) * 0.335;

                    check8.Tapped -= El1_Tapped;
                    check8.Width = (_iWidth * _rows) * 0.335;
                    check8.Height = (_iHeight * _rows) * 0.335;

                    check9.Tapped -= El1_Tapped;
                    check9.Width = (_iWidth * _rows) * 0.335;
                    check9.Height = (_iHeight * _rows) * 0.335;
                }
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
                    
                    // add it to the chess board children collection
                    grdBoard.Children.Add(brdr);
                    #endregion
                } // end iC
            } // end of iR
        }// end of createChessBoard
        #endregion
    }// end of main
}// end of SlidingPuzzle