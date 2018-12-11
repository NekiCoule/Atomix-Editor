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
    /// Logique d'interaction pour MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        public MapWindow()
        {
            InitializeComponent();            
        }

        public void CreateGrid(int mapWidth, int mapHeight, int tileWidth, int tileHeight, int tileMargin, int tilePadding, string tilesetPath)
        {
            this.Width = (2+mapWidth) * tileWidth;
            this.Height = (2+mapHeight) * tileHeight;

            Grid myGrid = new Grid();
            myGrid.Width = mapWidth*tileWidth;
            myGrid.Height = mapHeight*tileHeight;
            myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            myGrid.VerticalAlignment = VerticalAlignment.Top;
            myGrid.ShowGridLines = true;

            // Define the Columns

            


            for (int i = 0; i < mapWidth; i++)
            {
                ColumnDefinition Column = new ColumnDefinition();
                Column.Width = new GridLength(tileWidth, GridUnitType.Pixel);
                myGrid.ColumnDefinitions.Add(Column);
            }
            for (int j = 0; j < mapHeight; j++)
            {
                RowDefinition Row = new RowDefinition();
                Row.Height = new GridLength(tileHeight, GridUnitType.Pixel);
                myGrid.RowDefinitions.Add(Row);
            }
            //ColumnDefinition colDef1 = new ColumnDefinition();
            //ColumnDefinition colDef2 = new ColumnDefinition();
            //ColumnDefinition colDef3 = new ColumnDefinition();
            //myGrid.ColumnDefinitions.Add(colDef1);
            //myGrid.ColumnDefinitions.Add(colDef2);
            //myGrid.ColumnDefinitions.Add(colDef3);

            // Define the Rows
            //RowDefinition rowDef1 = new RowDefinition();
            //RowDefinition rowDef2 = new RowDefinition();
            //RowDefinition rowDef3 = new RowDefinition();
            //RowDefinition rowDef4 = new RowDefinition();
            //myGrid.RowDefinitions.Add(rowDef1);
            //myGrid.RowDefinitions.Add(rowDef2);
            //myGrid.RowDefinitions.Add(rowDef3);
            //myGrid.RowDefinitions.Add(rowDef4);

            //TextBlock txt1 = new TextBlock();
            //txt1.Text = "2005 Products Shipped";
            //txt1.FontSize = 20;
            //txt1.FontWeight = FontWeights.Bold;
            //Grid.SetColumnSpan(txt1, 3);
            //Grid.SetRow(txt1, 0);

            //myGrid.Children.Add(txt1);

            this.Content = myGrid;

        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(Mouse.GetPosition(this).ToString());
        }
    }
}
