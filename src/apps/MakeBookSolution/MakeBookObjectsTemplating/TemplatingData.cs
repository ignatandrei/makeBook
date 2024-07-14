namespace MakeBookObjectsTemplating;
public class TemplatingData
{
    public static ResultsTemplateScriban RenderTemplating<T>(string fileName, T model)
    {
        if(!File.Exists(fileName))
        {
            return new FileNotFoundException(fileName);
        }
        string text = "";
        using (FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {

            using (StreamReader reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
        }
        var template = Template.Parse(text);
        if (template.HasErrors)
        {
            return template.Messages.Select(it=>new ValidationResult(it.Message)).ToArray();
        }
        return template.Render(model, mr=>mr.Name);
    }
}
