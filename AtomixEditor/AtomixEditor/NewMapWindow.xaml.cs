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
using System.Windows.Forms;

namespace AtomixEditor
{
    /// <summary>
    /// Logique d'interaction pour NewMapWindow.xaml
    /// </summary>
    public partial class NewMapWindow : Window
    {
        public NewMapWindow()
        {
            InitializeComponent();            
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            txtTileset.Text = fileDialog.FileName;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            int mapWidth;
            int mapHeight;
            int tileWidth;
            int tileHeight;
            int tileMargin;
            int tilePadding;
            string tilesetPath;

            mapWidth = int.Parse(txtWidth.Text);
            mapHeight = int.Parse(txtHeight.Text);
            tileWidth = int.Parse(txtTileWidth.Text);
            tileHeight = int.Parse(txtTileHeight.Text);
            tileMargin = int.Parse(txtMargin.Text);
            tilePadding= int.Parse(txtPadding.Text);
            tilesetPath = txtTileset.Text;

            /*System.Windows.MessageBox.Show(
                "largeur map : " + mapWidth +
                "\nhauteur map : " + mapHeight +
                "\nlargeur tile : " + tileWidth +
                "\nhauteur tile : " + tileHeight +
                "\nmargin : " + tileMargin +
                "\npadding : " + tilePadding +
                "\nchemin du fichier : " + tilesetPath
                );*/

            MapWindow Map = new MapWindow(mapWidth, mapHeight, tileWidth, tileHeight, tileMargin, tilePadding, tilesetPath);
            Map.Show();
            //Map.CreateGrid(mapWidth, mapHeight, tileWidth, tileHeight, tileMargin, tilePadding, tilesetPath);
            this.Close();

        }
    }
}
