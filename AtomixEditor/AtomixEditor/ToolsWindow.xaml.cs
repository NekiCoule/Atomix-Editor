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
        private MapWindow theMap;
        private Tilemap map;

        public ToolsWindow(MapWindow myMap, Tilemap myTilemap)
        {
            InitializeComponent();
            theMap = myMap;
            map = myTilemap;
        }

        private void BtnResetClick(object sender, RoutedEventArgs e)
        {
            theMap.ResetMap();
        }


        // TODO charger info dans XML
        private void BtnSaveMapClick(object sender, RoutedEventArgs e)
        {
            if (map.saveMap(MapWindow.getMapGrid())) { }
        }

        // TODO recharger image Tileset
        private void BtnLoadTilesetClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
