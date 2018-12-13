using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AtomixEditor
{
    public class Tilemap
    {
        private int mapWidth;
        private int mapHeight;
        private int tileWidth;
        private int tileHeight;
        private int tileMargin;
        private int tilePadding;
        private string tilemapPath;
        private string tilemapName;
        private string tilesetFile;


        public Tilemap()
        {
            mapWidth = 0;
            mapHeight = 0;
            tileWidth = 0;
            tileHeight = 0;
            tileMargin = 0;
            tilePadding = 0;
            tilemapPath = "";
            tilemapName = "";
            tilesetFile = "";
        }

        public Tilemap(int myMapWidth, int myMapHeight, int myTileWidth, int myTileHeight, int myTileMargin, int myTilePadding, string myTilemapPath, string myTilemapName, string myTilesetFile)
        {
            mapWidth = myMapWidth;
            mapHeight = myMapHeight;
            tileWidth = myTileWidth;
            tileHeight = myTileHeight;
            tileMargin = myTileMargin;
            tilePadding = myTilePadding;
            tilemapPath = myTilemapPath;
            tilemapName = myTilemapName;
            tilesetFile = myTilesetFile;
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
                            new XAttribute("name", System.IO.Path.GetFileNameWithoutExtension(tilesetFile)),
                            new XElement("image", tilesetFile)
                        )
                    )

                );
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
                            new XAttribute("name", System.IO.Path.GetFileNameWithoutExtension(tilesetFile)),
                            new XElement("image", tilesetFile)
                        )
                    )

                );
                doc.Save(docInit);
            }
            return true;
        }

        public MapWindow goToEditor()
        {
            MapWindow Map = new MapWindow(mapWidth, mapHeight, tileWidth, tileHeight, tileMargin, tilePadding, tilesetFile, tilemapName, tilemapPath);
            return Map;
        }

        public string GetTilesetPath()
        {
            return tilesetFile;
        }

        public int GetTileWidth()
        {
            return tileWidth;
        }

        public int GetTileHeight()
        {
            return tileHeight;
        }
    }
}
