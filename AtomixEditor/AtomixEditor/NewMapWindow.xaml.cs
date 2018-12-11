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

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            txtTileset.Text = fileDialog.FileName;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string mapWidth;
            string mapHeight;
            string tileWidth;
            string tileHeight;
            string tileMargin;
            string tilePadding;
            string tilesetPath;

            mapWidth = txtWidth.Text;
            mapHeight = txtHeight.Text;
            tileWidth = txtTileWidth.Text;
            tileHeight = txtTileHeight.Text;
            tileMargin = txtMargin.Text;
            tilePadding= txtPadding.Text;
            tilesetPath = txtTileset.Text;

            System.Windows.MessageBox.Show(
                "largeur map : " + mapWidth +
                "\nhauteur map : " + mapHeight +
                "\nlargeur tile : " + tileWidth +
                "\nhauteur tile : " + tileHeight +
                "\nmargin : " + tileMargin +
                "\npadding : " + tilePadding +
                "\nchemin du fichier : " + tilesetPath
                );

            MapWindow Map = new MapWindow();
            Map.Show();
            this.Close();

        }
    }
}
