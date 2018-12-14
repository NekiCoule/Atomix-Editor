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
    public partial class TilemapWindow : Window
    {
        private Tileset theTileset;
        private MapWindow theMap;

        public TilemapWindow(MapWindow map, Tileset myTileset)
        {
            InitializeComponent();           
            
            theTileset = myTileset;
            theMap = map;
            
            string directory = System.IO.Directory.GetCurrentDirectory();            

            BitmapImage tileset = new BitmapImage();
            tileset.BeginInit();
            tileset.UriSource = new Uri(theTileset.getTilesetFile(), UriKind.Absolute);     
            tileset.EndInit();

            Image img = new Image
            {
                Width = tileset.PixelWidth,
                Height = tileset.PixelHeight
            };    

            img.Source = tileset;

            // load tileset into the editor right-side window
            this.tilesetGrid.Children.Add(img);

            this.Height = tileset.PixelHeight + 35;
            this.Width = tileset.PixelWidth + 15;
            this.Left = SystemParameters.WorkArea.Width - this.Width;
            this.Top = (SystemParameters.WorkArea.Bottom - this.Height) / 2;
        }

        // on click we create image of 1 tile
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetTilePosition(out int tileX, out int tileY);

            int tileHeight = theTileset.getElementHeight();
            int tileWidth = theTileset.getElementWidth();
            int spacing = theTileset.getElementSpacing();

            BitmapImage tile = new BitmapImage();
            tile.BeginInit();
            tile.UriSource = new Uri(theTileset.getTilesetFile(), UriKind.Absolute);
            tile.SourceRect = new Int32Rect(tileX * tileWidth + ((tileX+1) * spacing), tileY * tileHeight + ((tileY+1) * spacing), tileWidth, tileHeight);
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

            // To find which tile is clicked we divide mouse position by tile size + spacing
            tileX = posX / (theTileset.getElementWidth() + theTileset.getElementSpacing());
            tileY = posY / (theTileset.getElementHeight() + theTileset.getElementSpacing());
        }

        private void wdwTileset_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (MessageBox.Show("Quitter l'éditeur sans sauvegarder ?", "Attention !", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
