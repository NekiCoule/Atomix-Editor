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
    /// <summary>
    /// Logique d'interaction pour MapWindow.xaml
    /// </summary>

    public partial class MapWindow : Window
    {
        public Grid myGrid;
        private int mapWidth;
        private int mapHeight;
        private int mapTileWidth;
        private int mapTileHeight;
        private int mapTileMargin;
        private int mapTilePadding;
        private string mapTilesetFile;
        private string mapTilemapName;
        private string mapTilemapPath;
        private bool isLeftMouseDown;
        private bool isRightMouseDown;

        // Map window constructor (load map)
        public MapWindow(string tilemapPath)
        {
            InitializeComponent();

            MessageBox.Show("chemin tilemap : "+ tilemapPath);
        }

        // Map window constructor (new map)
        public MapWindow(int Width, int Height, int tileWidth, int tileHeight, int tileMargin, int tilePadding, string tilesetFile, string tilemapName, string tilemapPath)
        {
            InitializeComponent();

            mapWidth = Width;
            mapHeight = Height;
            mapTileWidth = tileWidth;
            mapTileHeight = tileHeight;
            mapTileMargin = tileMargin;
            mapTilePadding = tilePadding;
            mapTilesetFile = tilesetFile;
            mapTilemapName = tilemapName;
            mapTilemapPath = tilemapPath;

            int windowWidth = mapWidth * mapTileWidth;
            int windowHeight = mapHeight * mapTileHeight;
            

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
            //myGrid.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x90, 0x90));

            // Define the columns
            for (int i = 0; i < mapWidth; i++)
            {
                ColumnDefinition Column = new ColumnDefinition
                {
                    Width = new GridLength(mapTileWidth, GridUnitType.Pixel)
                };
                myGrid.ColumnDefinitions.Add(Column);
            }
            // Define the rows
            for (int j = 0; j < mapHeight; j++)
            {
                RowDefinition Row = new RowDefinition
                {
                    Height = new GridLength(mapTileHeight, GridUnitType.Pixel)
                };
                myGrid.RowDefinitions.Add(Row);
            }

            // Add grid to window
            this.Content = myGrid;
        }      


        /// <summary>
        ///  Getters
        /// </summary>
        private int GetMapTileWidth()
        {
            return this.mapTileWidth;
        }

        private int GetMapTileHeight()
        {
            return this.mapTileHeight;
        }

        /// <summary>
        /// Events
        /// </summary>

        // Click in map window
        

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isLeftMouseDown = true;
            
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DrawTile();
            isLeftMouseDown = false;
        }



        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //isRightMouseDown = true;
            //EraseTile();
        }

        private void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            EraseTile();
            //isRightMouseDown = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {    
            if (isLeftMouseDown)
            {
                DrawTile();
            }
            if (isRightMouseDown)
            {
                //EraseTile();
            }
        }

        private void DrawTile()
        {
            int tileX;
            int tileY;

            GetTilePosition(out tileX, out tileY);

            Image img = new Image
            {
                Width = mapTileWidth,
                Height = mapTileHeight
            };

            // Get current folder
            string directory = System.IO.Directory.GetCurrentDirectory();

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(directory + "/char_kon.jpg", UriKind.Absolute);
            logo.EndInit();

            img.Source = logo;            
            Grid.SetColumn(img, tileX);
            Grid.SetRow(img, tileY);

            if (myGrid.Children.Count == 0)
            {
                myGrid.Children.Add(img);                
            }
            else
            {
                foreach (UIElement tile in myGrid.Children)
                {
                    if (Grid.GetRow(tile) == tileY && Grid.GetColumn(tile) == tileX)
                    {
                        if (((System.Windows.Controls.Image)tile).Source.ToString() != logo.UriSource.ToString())
                        {
                            myGrid.Children.Add(img);
                            break;
                        }
                    }
                    else
                    {
                        myGrid.Children.Add(img);
                        break;
                    }
                }
            }

            



            //MessageBox.Show("position souris : X="+ posX + " Y="+ posY + " case "+ tileX + "-"+ tileY);
        }

        private void EraseTile()
        {
            int tileX;
            int tileY;

            GetTilePosition(out tileX, out tileY);

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
            tileX = posX / GetMapTileWidth();
            tileY = posY / GetMapTileHeight();
        }

        private void ResetTile()
        {
            // Erase all
            myGrid.Children.Clear();
        }
    }
}
