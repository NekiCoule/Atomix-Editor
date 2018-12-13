using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AtomixEditor
{
    class Tilemap
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

        public MapWindow goToEditor()
        {
            MapWindow Map = new MapWindow(mapWidth, mapHeight, tileWidth, tileHeight, tileset.getElementMargin(), tileset.getElementSpacing(), tileset.getTilesetFile(), tilemapName, tilemapPath);
            return Map;
        }
    }
}
