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
    /// Logique d'interaction pour ToolsWindow.xaml
    /// </summary>
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
            if (MessageBox.Show("Votre map sera réinitialisée, êtes-vous sûr ?", "Attention !", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                mapWindow.ResetMap();
            }                
        }

        // Saves data into XML
        private void btnSaveMapClick(object sender, RoutedEventArgs e)
        {
            if (map.saveMap(MapWindow.getMapGrid())) { }
        }

        // TODO recharger image Tileset
        private void btnLoadTilesetClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnExitClick(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void wdwTools_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
