using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AtomixEditor
{
    public partial class MapWindow : Window
    {
        private static Grid myGrid;
        private Tilemap map;
        private bool isLeftMouseDown;
        private bool isRightMouseDown;
        private Image selectedTile;
        private Int32Rect selectedRect;

        // Map window constructor (load map)
        public MapWindow(string tilemapPath)
        {
            InitializeComponent();
            Left = 0;
            Top = 0;
        }

        // Map window constructor (new map)
        public MapWindow(Tilemap myTilemap)
        {
            InitializeComponent();

            map = myTilemap; 
            
            int windowWidth = myTilemap.getMapWidth() * myTilemap.getTileWidth();
            int windowHeight = myTilemap.getMapHeight() * myTilemap.getTileHeight();

            // Define window size (add some pixels for width and height for the grid to fit the screen)
            this.Width = windowWidth + 15;
            this.Height = windowHeight + 35;

            myGrid = new Grid
            {
                Width = windowWidth,
                Height = windowHeight,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                ShowGridLines = true
            };

            // Create the grid columns
            for (int i = 0; i < map.getMapWidth(); i++)
            {
                ColumnDefinition Column = new ColumnDefinition
                {
                    Width = new GridLength(map.getTileWidth(), GridUnitType.Pixel)
                };
                myGrid.ColumnDefinitions.Add(Column);
            }
            // Create the grid rows
            for (int j = 0; j < map.getMapHeight(); j++)
            {
                RowDefinition Row = new RowDefinition
                {
                    Height = new GridLength(map.getTileHeight(), GridUnitType.Pixel)
                };
                myGrid.RowDefinitions.Add(Row);
            }



            // Add grid to window
            this.Content = myGrid;

            // update location of the window
            this.Left = 0;
            this.Top = (SystemParameters.WorkArea.Bottom - this.Height) / 2;
            


        }

        

        /// <summary>
        /// Events
        /// </summary>

        // Click in map window     

        // If click down, we will call DrawTile on mouse move
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isLeftMouseDown = true;            
        }

        // If click release we draw a last tile and disable draw
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DrawTile();
            isLeftMouseDown = false;
        }

        // If click down, we will call ERaseTile on mouse move
        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            isRightMouseDown = true;
            EraseTile();
        }

        // If click release we erase a last tile and disable erase
        private void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            EraseTile();
            isRightMouseDown = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {    
            if (isLeftMouseDown)
            {
                DrawTile();
            }
            if (isRightMouseDown)
            {
                EraseTile();
            }
        }

        // draws a tile depending on mouse location and tile selected in tileset
        private void DrawTile()
        {
            bool draw = true;

            GetTilePosition(out int tileX, out int tileY);

            // Try, to prevent crash if no tile selected from tileset 
            try
            {
                Image img = new Image
                {
                    Width = map.getTileWidth(),
                    Height = map.getTileHeight()
                };

                // Get current folder
                string directory = System.IO.Directory.GetCurrentDirectory();

                // Create a bitmad with selected tiled
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();            
                logo.UriSource = new Uri(GetSelectedTile().Source.ToString(), UriKind.Absolute); 
                logo.SourceRect = GetSelectedRect();
                logo.EndInit();

                img.Source = logo;
                img.Name = GetSelectedTile().Name;

                Grid.SetColumn(img, tileX);
                Grid.SetRow(img, tileY);

                // Run through all images already in the grid
                foreach (UIElement tile in myGrid.Children)
                {
                    // if same location we do nothing
                    if (Grid.GetRow(tile) == tileY && Grid.GetColumn(tile) == tileX)
                    {
                        draw = false;
                        break;
                    }
                }

                // if nothing yet in the grid at desired location
                if (draw)
                {
                    myGrid.Children.Add(img);
                }
            }
            catch
            {
                isLeftMouseDown = false;
                MessageBox.Show(this, "Veuillez d'abord choisir une tile du tileset");
            }
        }

        // Erase desired tile on map
        private void EraseTile()
        {
            GetTilePosition(out int tileX, out int tileY);

            foreach (UIElement control in myGrid.Children)
            {
                if (Grid.GetRow(control) == tileY && Grid.GetColumn(control) == tileX)
                {
                    myGrid.Children.Remove(control);
                    break;
                }
            }
        }

        private void GetTilePosition(out int tileX, out int tileY)
        {
            int posX;
            int posY;

            // Retrieve mouse position
            posX = (int)Mouse.GetPosition(this).X;
            posY = (int)Mouse.GetPosition(this).Y;

            // To find which tile is clicked we divide mouse position by tile size
            tileX = posX / map.getTileWidth();
            tileY = posY / map.getTileHeight();            
        }

        // Erase all
        public void ResetMap()
        {            
            myGrid.Children.Clear();
        }

        public void SetSelectedTile(Image img)
        {
            selectedTile = img;
        }

        public Image GetSelectedTile()
        {
            return selectedTile;
        }

        public void SetSelectedRect(Int32Rect rect)
        {
            selectedRect = rect;
        }

        public Int32Rect GetSelectedRect()
        {
            return selectedRect;
        }

        public static Grid getMapGrid()
        {
            return myGrid;
        }


        // Close all windows, save possible before close
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult answer;

            answer = MessageBox.Show("Sauvegarder avant de quitter ?", "Attention !", MessageBoxButton.YesNoCancel);

            if (answer == MessageBoxResult.Yes)
            {
                if (map.saveMap(getMapGrid())) { MessageBox.Show("Map enregitrée !"); }
                Environment.Exit(0);
            }
            else if (answer == MessageBoxResult.No)
            {
                Environment.Exit(0);
            }
            else if (answer == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
