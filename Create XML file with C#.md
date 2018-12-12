```Csharp

public class Tilemap
{
    public string height;
    public string length;
    public string tileWidth;
    public string tileLength;
    public string tilesetPath;
    public string path;

    public Tilemap(string myHeight, string myLength, string myTileWidth, string myTileLength, string myTilesetPath, string myPath)
    {
      height = myHeight;
      length = myLength;
      tileWidth = myTileWidth;
      tileLength = myTileLength;
      tilesetPath = myTilesetPath;
      path = myPath;
    }  
}

protected void btnValidate_Click(object sender, EventArgs e)
{
  Tilemap newTilemap = new Tilemap()

  StringBuilder sb = new StringBuilder();
    XmlWriterSettings xws = new XmlWriterSettings();
    xws.OmitXmlDeclaration = true;
    xws.Indent = true;

    using (XmlWriter xw = XmlWriter.Create(newTilemap.path, xws))
    {
      XDocument doc = new XDocument(
        new XElement("Child",
          new XElement("GrandChild", "another content")
        )
      );
      doc.Save(xw);
    }


}

```
