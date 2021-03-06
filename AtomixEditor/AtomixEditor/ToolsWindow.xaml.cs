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
using System.Windows.Shapes;
using System.Windows.Forms;

namespace AtomixEditor
{
    public partial class ToolsWindow : Window
    {
        private MapWindow mapWindow;
        private Tilemap map;

        public ToolsWindow(MapWindow myMapWindow, Tilemap myTilemap)
        {
            InitializeComponent();

            mapWindow = myMapWindow;
            map = myTilemap;
            this.Left = SystemParameters.WorkArea.Width - this.Width;
            this.Top = 0;
        }

        // erase all tiles of the map
        private void btnResetClick(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Votre map sera réinitialisée, êtes-vous sûr ?", "Attention !", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                mapWindow.ResetMap();
            }                
        }

        // Saves data into XML
        private void btnSaveMapClick(object sender, RoutedEventArgs e)
        {
            if (map.saveMap(MapWindow.getMapGrid())) { System.Windows.MessageBox.Show("Map enregitrée !"); }
        }

        // Saves data into XML at selected location
        private void btnSaveAsClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();

            if (map.saveMap(MapWindow.getMapGrid(), folderDialog.SelectedPath)) { System.Windows.MessageBox.Show("Map enregitrée !"); }
        }

        // Exit app
        private void btnExitClick(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        // Close all windows, save possible before close
        private void wdwTools_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult answer;

            answer = System.Windows.MessageBox.Show("Sauvegarder avant de quitter ?", "Attention !", MessageBoxButton.YesNoCancel);

            if (answer == MessageBoxResult.Yes)
            {
                if (map.saveMap(MapWindow.getMapGrid())) { System.Windows.MessageBox.Show("Map enregitrée !"); }
                Environment.Exit(0);
            }
            else if (answer == MessageBoxResult.No)
            {
                Environment.Exit(0);
            }
            else if(answer == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }        
    }
}
