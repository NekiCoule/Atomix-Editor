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

  using (StreamWriter xmlWrite = new StreamWriter(newTilemap.path))
  {
    XmlSerializer myXmlSerializer = new XmlSerializer(newTilemap.GetType());
    myXmlSerializer.Serialize(xmlWrite, newTilemap);
  }


}

```
