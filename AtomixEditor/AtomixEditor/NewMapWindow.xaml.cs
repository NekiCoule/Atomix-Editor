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
            int mapWidth;
            int mapHeight;
            int tileWidth;
            int tileHeight;
            int tileMargin;
            int tilePadding;
            string tilesetFile;
            string tilemapName;
            string tilemapPath;
            string tilemapFile;

            mapWidth = int.Parse(txtWidth.Text);
            mapHeight = int.Parse(txtHeight.Text);
            tileWidth = int.Parse(txtTileWidth.Text);
            tileHeight = int.Parse(txtTileHeight.Text);
            tileMargin = int.Parse(txtMargin.Text);
            tilePadding= int.Parse(txtPadding.Text);
            tilesetFile = txtTilesetFile.Text;
            tilemapName = txtTilemapName.Text;
            tilemapPath = txtTilemapPath.Text;

            tilemapFile = tilemapPath + @"\" + tilemapName + ".xml";

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




            XmlWriterSettings docSettings = new XmlWriterSettings();
            docSettings.OmitXmlDeclaration = true;
            docSettings.Indent = true;

            using (XmlWriter docInit = XmlWriter.Create(tilemapFile, docSettings))
            {
                XDocument doc = new XDocument(
                    new XElement("map",
                        new XAttribute("width", mapWidth),
                        new XAttribute("height", mapHeight),
                        new XAttribute("tileWidth", tileWidth),
                        new XAttribute("tileHeight", tileHeight),
                        new XElement("tileset", 
                            new XAttribute("name", System.IO.Path.GetFileNameWithoutExtension(tilesetFile)),
                            new XElement("image", tilesetFile)
                        )
                    )
                   
                );
                doc.Save(docInit);
            }

            MapWindow Map = new MapWindow(mapWidth, mapHeight, tileWidth, tileHeight, tileMargin, tilePadding, tilesetFile, tilemapName, tilemapPath);
            Map.Show();
            //Map.CreateGrid(mapWidth, mapHeight, tileWidth, tileHeight, tileMargin, tilePadding, tilesetPath);
            this.Close();
        }

        
    }
}
