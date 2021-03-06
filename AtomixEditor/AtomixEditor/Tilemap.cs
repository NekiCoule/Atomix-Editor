using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Media.Imaging;

namespace AtomixEditor
{
    public class Tilemap
    {
        private int mapWidth;
        private int mapHeight;
        private int tileWidth;
        private int tileHeight;
        private string tilemapPath;
        private string tilemapName;
        private Tileset tileset;


        public Tilemap()
        {
            mapWidth = 0;
            mapHeight = 0;
            tileWidth = 0;
            tileHeight = 0;
            tilemapPath = "";
            tilemapName = "";
            tileset = new Tileset();
        }

        public Tilemap(int myMapWidth, int myMapHeight, int myTileWidth, int myTileHeight, string myTilemapPath, string myTilemapName, int myElementWidth, int myElementHeight, int myElementMargin, int myElementSpacing, string myTilesetFile)
        {
            mapWidth = myMapWidth;
            mapHeight = myMapHeight;
            tileWidth = myTileWidth;
            tileHeight = myTileHeight;
            tilemapPath = myTilemapPath;
            tilemapName = myTilemapName;
            tileset = new Tileset(myElementWidth, myElementHeight, myElementMargin, myElementSpacing, myTilesetFile);

        }

        // Destructor
        ~Tilemap(){}



        // Name : initFromXML
        // Parameters: string (the full path of the xml file)
        // Returns : bool
        // Description : Load an XML file and save the values in the current object. The XML file must be well written!!

        public bool initFromXML(string fullPath)
        {
            // Load the document
            XDocument doc = XDocument.Load(fullPath);

            // map node
            XElement eMap = doc.Element("map");
            IEnumerable<XAttribute> mapAtt = 
                from att in eMap.Attributes()
                select att;

            // map attributes
            mapWidth = int.Parse(mapAtt.ElementAt(0).Value);
            mapHeight = int.Parse(mapAtt.ElementAt(1).Value);
            tileWidth = int.Parse(mapAtt.ElementAt(2).Value);
            tileHeight = int.Parse(mapAtt.ElementAt(3).Value);
            tilemapPath = System.IO.Path.GetDirectoryName(fullPath);
            tilemapName = System.IO.Path.GetFileNameWithoutExtension(fullPath);

            // map/tileset node
            XElement eTileset = eMap.Element("tileset");
            mapAtt =
                from att in eTileset.Attributes()
                select att;

            // map/tileset attributes
            tileset = new Tileset(
                int.Parse(mapAtt.ElementAt(1).Value),
                int.Parse(mapAtt.ElementAt(2).Value),
                int.Parse(mapAtt.ElementAt(3).Value),
                int.Parse(mapAtt.ElementAt(4).Value),
                ""
                );

            // map/tilset/image node
            XElement eImage = eTileset.Element("image");
            mapAtt =
                from att in eImage.Attributes()
                select att;

            // map/tilset/image attributes
            tileset.setFilePath(@mapAtt.ElementAt(0).Value);

            return true;
        }


        // Name: initDoc
        // Parameters: None
        // Returns: bool
        // Description: Create an XML doc to serialise the object values. Save to the path already stored. Returns true if everything happened as intended

        public bool initDoc()
        {
            string tilemapFile = tilemapPath + @"\" + tilemapName + ".xml";

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
                            new XAttribute("name", System.IO.Path.GetFileNameWithoutExtension(tileset.getTilesetFile())),
                            new XAttribute("elementwidth", tileset.getElementWidth()),
                            new XAttribute("elementheight", tileset.getElementHeight()),
                            new XAttribute("margin", tileset.getElementMargin()),
                            new XAttribute("spacing", tileset.getElementSpacing()),
                            new XElement("image", 
                                new XAttribute("source", tileset.getTilesetFile())
                            ) //end image
                        ), //end tileset
                        new XElement("layer",
                            new XAttribute("id", 1),
                            new XAttribute("name", "Layer 1"),
                            new XElement("data")
                        ) //end layer
                    ) //end map

                ); //end of doc
                doc.Save(docInit);
            }
            return true;
        }

        // Name: initDoc
        // Parameters: None
        // Returns: bool
        // Description: Create an XML doc to serialise the object values. Save to a new path and store it in the private values. Returns true if everything happened as intended

        public bool initDoc(string newPath)
        {
            tilemapPath = newPath;
            string tilemapFile = tilemapPath + @"\" + tilemapName + ".xml";

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
                            new XAttribute("name", System.IO.Path.GetFileNameWithoutExtension(tileset.getTilesetFile())),
                            new XAttribute("elementwidth", tileset.getElementWidth()),
                            new XAttribute("elementheight", tileset.getElementHeight()),
                            new XAttribute("margin", tileset.getElementMargin()),
                            new XAttribute("spacing", tileset.getElementSpacing()),
                            new XElement("image",
                                new XAttribute("source", tileset.getTilesetFile())
                            ) //end image
                        ), //end tileset
                        new XElement("layer",
                            new XAttribute("id", 1),
                            new XAttribute("name", "Layer 1"),
                            new XElement("data")
                        ) //end layer
                    ) //end map

                ); //end of doc
                doc.Save(docInit);
            }
            return true;
        }

        public bool saveMap(Grid myGrid)
        {
            string tilemapFile = tilemapPath + @"\" + tilemapName + ".xml";

            initDoc();

            XDocument doc = XDocument.Load(tilemapFile);
            XElement map = doc.Element("map");
            XElement layer = map.Element("layer");
            XElement data = layer.Element("data");

            foreach (UIElement control in myGrid.Children)
            {

                var tile = new XElement("Tile",
                    new XAttribute("row", Grid.GetRow(control)),
                    new XAttribute("col", Grid.GetColumn(control)),
                    new XAttribute("id", ((Image)control).Name.ToString())
                ); //end tile

                data.Add(tile);

            }

            doc.Save(tilemapFile);


            return true;
        }

        public bool saveMap(Grid myGrid, string newPath)
        {
            tilemapPath = newPath;
            string tilemapFile = tilemapPath + @"\" + tilemapName + ".xml";

            initDoc();

            XDocument doc = XDocument.Load(tilemapFile);
            XElement map = doc.Element("map");
            XElement layer = map.Element("layer");
            XElement data = layer.Element("data");

            foreach (UIElement control in myGrid.Children)
            {

                var tile = new XElement("Tile",
                    new XAttribute("row", Grid.GetRow(control)),
                    new XAttribute("col", Grid.GetColumn(control)),
                    new XAttribute("id", ((Image)control).Name.ToString())
                ); //end tile

                data.Add(tile);

            }

            doc.Save(tilemapFile);


            return true;
        }

        public MapWindow goToEditor()
        {
            MapWindow Map = new MapWindow(this);
            return Map;
        }

        public Grid loadTilesFromXML(Grid myGrid)
        {
            string tilemapFile = tilemapPath + @"\" + tilemapName + ".xml";
            int tileX;
            int tileY;
            string tileCoord;

            Grid rGrid = myGrid;

            XDocument doc = XDocument.Load(tilemapFile);
            XElement eMap = doc.Element("map");
            XElement eLayer = eMap.Element("layer");
            XElement eData = eLayer.Element("data");

            // Save a list of <tile /> from the data node
            IEnumerable<XElement> tileNodes = from elem in eData.Elements()
                                            select elem;
            
            // loop in all the tiles inside 
            foreach (XElement tile in tileNodes)
            {
                IEnumerable<XAttribute> tileAtt = from att in tile.Attributes()
                                                 select att;
                // Saves the tile in the grid                
                tileY = int.Parse(tileAtt.ElementAt(0).Value);
                tileX = int.Parse(tileAtt.ElementAt(1).Value);
                tileCoord = tileAtt.ElementAt(2).Value;
                int tileCoordX;
                int tileCoordY;

                if (tileCoord[2].ToString() == "Y")
                {
                    tileCoordX = int.Parse(tileCoord[1].ToString());
                    if (tileCoord.ToString().Length == 5)
                    {
                        tileCoordY = int.Parse(tileCoord[3].ToString() + tileCoord[4].ToString());
                    }
                    else tileCoordY = int.Parse(tileCoord[3].ToString());
                }
                else
                {
                    tileCoordX = int.Parse(tileCoord[1].ToString() + tileCoord[2].ToString());
                    if (tileCoord.ToString().Length == 6)
                    {
                        tileCoordY = int.Parse(tileCoord[4].ToString() + tileCoord[5].ToString());
                    }
                    else tileCoordY = int.Parse(tileCoord[4].ToString());
                }
                

                Image img = new Image
                {
                    Width = getTileWidth(),
                    Height = getTileHeight()
                };

                BitmapImage tileBmp = new BitmapImage();
                tileBmp.BeginInit();
                tileBmp.UriSource = new Uri(getTileset().getTilesetFile().ToString(), UriKind.Absolute);
                tileBmp.SourceRect = new Int32Rect(tileCoordX * tileset.getElementWidth() + ((tileCoordX + 1) * tileset.getElementSpacing()), tileCoordY * tileset.getElementHeight() + ((tileCoordY + 1) * tileset.getElementSpacing()), tileset.getElementWidth(), tileset.getElementHeight());
                tileBmp.EndInit();

                Grid.SetColumn(img, tileX);
                Grid.SetRow(img, tileY);

                img.Source = tileBmp;
                img.Name = tileCoord;

                myGrid.Children.Add(img);

            }


            return rGrid;
        }

        public int getMapWidth()        {   return mapWidth;    }
        public int getMapHeight()       {   return mapHeight;   }
        public string getTilemapPath()  {   return tilemapPath; }
        public string getTilemapName()  {   return tilemapName; }
        public int getTileWidth()       {   return tileWidth;   }
        public int getTileHeight()      {   return tileHeight;  }
        public Tileset getTileset()     {   return tileset;     }
    }
}
