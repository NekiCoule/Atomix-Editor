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

        public ToolsWindow(MapWindow myMap)
        {
            InitializeComponent();

            theMap = myMap;
        }

        private void BtnResetClick(object sender, RoutedEventArgs e)
        {
            theMap.ResetMap();
        }


        // TODO charger info dans XML
        private void BtnSaveMapClick(object sender, RoutedEventArgs e)
        {

        }

        // TODO recharger image Tileset
        private void BtnLoadTilesetClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
