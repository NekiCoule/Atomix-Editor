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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace AtomixEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNewMap_Click(object sender, RoutedEventArgs e)
        {
            NewMapWindow newMap = new NewMapWindow();           
            newMap.Show();
            this.Close();
        }

        private void BtnLoadMap_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            MapWindow Map = new MapWindow(fileDialog.FileName);
            Map.Show();
            TilemapWindow tilemap = new TilemapWindow();
            tilemap.Show();
            this.Close();
        }
    }
}
