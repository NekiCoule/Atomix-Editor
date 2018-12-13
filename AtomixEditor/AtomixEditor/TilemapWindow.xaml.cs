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
    /// Logique d'interaction pour TilemapWindow.xaml
    /// </summary>
    public partial class TilemapWindow : Window
    {
        private Tilemap theTilemap;
        private MapWindow theMap;

        public TilemapWindow(MapWindow map, Tilemap tilemap)
        {
            InitializeComponent();           

            theTilemap = tilemap;
            theMap = map;

            // load tileset into the editor right-side window
            string directory = System.IO.Directory.GetCurrentDirectory();            

            BitmapImage tileset = new BitmapImage();
            tileset.BeginInit();
            tileset.UriSource = new Uri(theTilemap.GetTilesetPath(), UriKind.Absolute);     
            tileset.EndInit();

            Image img = new Image
            {
                Width = tileset.PixelWidth,
                Height = tileset.PixelHeight
            };    

            img.Source = tileset;

            this.tilesetGrid.Children.Add(img);

            this.Height = tileset.PixelHeight + 35;
            this.Width = tileset.PixelWidth + 15;
            this.Left = SystemParameters.WorkArea.Width - this.Width;
            this.Top = (SystemParameters.WorkArea.Bottom - this.Height) / 2;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetTilePosition(out int tileX, out int tileY);

            BitmapImage tile = new BitmapImage();
            tile.BeginInit();
            tile.UriSource = new Uri(theTilemap.GetTilesetPath(), UriKind.Absolute);
            tile.SourceRect = new Int32Rect(tileX*32, tileY*32, 32, 32);
            tile.EndInit();

            Image img = new Image
            {
                Width = tile.PixelWidth,
                Height = tile.PixelHeight
            };

            img.Source = tile;
            img.Name = "X" + tileX + "Y" + tileY;

            theMap.SetSelectedTile(img);
            theMap.SetSelectedRect(tile.SourceRect);
        }

        private void GetTilePosition(out int tileX, out int tileY)
        {
            int posX;
            int posY;

            // Retrieve mouse position
            posX = (int)Mouse.GetPosition(this).X;
            posY = (int)Mouse.GetPosition(this).Y;

            // To find which tile is clicked we divide mouse position by tile size
            tileX = posX / theTilemap.GetTileWidth();
            tileY = posY / theTilemap.GetTileHeight();
        }
    }
}
