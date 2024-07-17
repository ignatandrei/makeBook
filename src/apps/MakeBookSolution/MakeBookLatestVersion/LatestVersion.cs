
namespace MakeBookLatestVersion;

public class LatestVersion
{

    public async Task<ResultsHttp<string>> LatestVersionNumber(string owner, string repo)
    {
        string url = $"https://api.github.com/repos/{owner}/{repo}/releases/latest";

        using (var client = new HttpClient())
        {
            // GitHub API requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent", "request");
            try
            {
                var response = await client.GetStringAsync(url);
                using (JsonDocument doc = JsonDocument.Parse(response))
                {
                    var root = doc.RootElement;
                    var tagName = root.GetProperty("tag_name").GetString();
                    ArgumentNullException.ThrowIfNullOrWhiteSpace(tagName);
                    return tagName;
                }
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}
