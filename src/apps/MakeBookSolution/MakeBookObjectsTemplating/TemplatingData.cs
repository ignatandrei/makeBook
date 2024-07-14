namespace MakeBookObjectsTemplating;

public class TemplatingData
{
    public string RenderTemplating<T>(string fileName, T model)
    {
        return fileName;
    }
}
