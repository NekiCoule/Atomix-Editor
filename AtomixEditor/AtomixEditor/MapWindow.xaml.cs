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
        private string mapTilesetPath;

        // Map window constructor
        public MapWindow(int Width, int Height, int tileWidth, int tileHeight, int tileMargin, int tilePadding, string tilesetPath)
        {
            InitializeComponent();

            mapWidth = Width;
            mapHeight = Height;
            mapTileWidth = tileWidth;
            mapTileHeight = tileHeight;
            mapTileMargin = tileMargin;
            mapTilePadding = tilePadding;
            mapTilesetPath = tilesetPath;

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
        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            int posX;
            int posY;
            int tileX;
            int tileY;

            // Retrieve mouse position
            posX = (int)Mouse.GetPosition(this).X;
            posY = (int)Mouse.GetPosition(this).Y;

            // To find which tile is clicked we divide mouse position by tile size
            tileX = posX / GetMapTileWidth();
            tileY = posY / GetMapTileHeight();

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

            myGrid.Children.Add(img);
            
            //MessageBox.Show("position souris : X="+ posX + " Y="+ posY + " case "+ tileX + "-"+ tileY);
        }
    }
}
