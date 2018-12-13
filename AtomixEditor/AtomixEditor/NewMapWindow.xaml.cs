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

        // To choose a tileset
        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            txtTilesetFile.Text = fileDialog.FileName;
        }

        // To select tilemap save folder
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            txtTilemapPath.Text = folderDialog.SelectedPath;
        }

        // Validates user inputs, creates a tilemap and open editor windows (map & tileset)
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if(txtWidth.Text != "" && txtHeight.Text != "" && txtTileWidth.Text != "" &&
            txtTileHeight.Text != "" && txtTilemapPath.Text != "" && txtTilemapName.Text != "" &&
            txtElementWidth.Text != "" && txtElementHeight.Text != "" && txtMargin.Text != "" && 
            txtSpacing.Text != "" && txtTilesetFile.Text != "")
            {
                Tilemap theTilemap = new Tilemap
                (
                    int.Parse(txtWidth.Text), int.Parse(txtHeight.Text), int.Parse(txtTileWidth.Text),
                    int.Parse(txtTileHeight.Text), txtTilemapPath.Text, txtTilemapName.Text,
                    int.Parse(txtElementWidth.Text), int.Parse(txtElementHeight.Text),
                    int.Parse(txtMargin.Text), int.Parse(txtSpacing.Text), txtTilesetFile.Text
                );

                if (theTilemap.initDoc())
                {
                    MapWindow Map = theTilemap.goToEditor();
                    Map.Show();
                    TilemapWindow tilemap = new TilemapWindow(Map, theTilemap.GetTileset());
                    tilemap.Show();
                    ToolsWindow tools = new ToolsWindow(Map);
                    tools.Show();
                    this.Close();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Merci de renseigner tous les champs");
            }
        }        
    }
}
