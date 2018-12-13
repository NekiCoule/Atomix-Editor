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
using System.Xml;
using System.Xml.Linq;

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
            txtTilesetFile.Text = fileDialog.FileName;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            txtTilemapPath.Text = folderDialog.SelectedPath;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Tilemap theTilemap = new Tilemap(int.Parse(txtWidth.Text), int.Parse(txtHeight.Text), int.Parse(txtTileWidth.Text), int.Parse(txtTileHeight.Text), int.Parse(txtMargin.Text), int.Parse(txtPadding.Text), txtTilemapPath.Text, txtTilemapName.Text, txtTilesetFile.Text);

            /*
            System.Windows.MessageBox.Show(
                "largeur map : " + mapWidth +
                "\nhauteur map : " + mapHeight +
                "\nlargeur tile : " + tileWidth +
                "\nhauteur tile : " + tileHeight +
                "\nmargin : " + tileMargin +
                "\npadding : " + tilePadding +
                "\nchemin du fichier : " + tilesetFile +
                "\nnom du fichier : " + tilemapName +
                "\nchemin du tilemap : " + tilemapPath
                );
            */

            if(theTilemap.initDoc())
            {
                MapWindow Map = theTilemap.goToEditor();
                Map.Show();
                TilemapWindow tilemap = new TilemapWindow(Map, theTilemap);                
                tilemap.Show();
                //Map.CreateGrid(mapWidth, mapHeight, tileWidth, tileHeight, tileMargin, tilePadding, tilesetPath);
                this.Close();
            }

            
        }

        
    }
}
