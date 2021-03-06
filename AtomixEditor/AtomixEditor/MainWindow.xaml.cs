﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace AtomixEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Create a new tilemap
        private void BtnNewMap_Click(object sender, RoutedEventArgs e)
        {
            NewMapWindow newMap = new NewMapWindow();           
            newMap.Show();
            this.Close();
        }

        // Load an existing tilemap
        private void BtnLoadMap_Click(object sender, RoutedEventArgs e)
        {
            // Choose the tilemap file
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            Tilemap theTilemap = new Tilemap();

            if (theTilemap.initFromXML(fileDialog.FileName))
            {
                MapWindow Map = theTilemap.goToEditor();
                Map.Show();
                TilemapWindow tilemap = new TilemapWindow(Map, theTilemap.getTileset());
                tilemap.Show();
                ToolsWindow tools = new ToolsWindow(Map, theTilemap);
                tools.Show();
                this.Close();
            }
        }
    }
}
