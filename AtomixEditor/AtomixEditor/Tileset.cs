using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomixEditor
{
    public class Tileset
    {
        private int elementWidth;
        private int elementHeight;
        private int elementMargin;
        private int elementSpacing;
        private string tilesetFile;

        public Tileset()
        {
            elementWidth = 0;
            elementHeight = 0;
            elementMargin = 0;
            elementSpacing = 0;
            tilesetFile = "";
        }

        public Tileset(int myElementWidth, int myElementHeight, int myElementMargin, int myElementSpacing, string myTilesetFile)
        {
            elementWidth = myElementWidth;
            elementHeight = myElementHeight;
            elementMargin = myElementMargin;
            elementSpacing = myElementSpacing;
            tilesetFile = myTilesetFile;
        }

        public int getElementWidth()
        {
            return elementWidth;
        }

        public int getElementHeight()
        {
            return elementHeight;
        }

        public int getElementMargin()
        {
            return elementMargin;
        }

        public int getElementSpacing()
        {
            return elementSpacing;
        }

        public string getTilesetFile()
        {
            return tilesetFile;
        }

        public void setFilePath(string path)
        {
            tilesetFile = path;
        }
}
}
