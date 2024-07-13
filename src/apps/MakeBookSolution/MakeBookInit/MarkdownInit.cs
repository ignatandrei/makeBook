namespace MakeBookInit;

public partial class MarkdownInit
{
    [EmbedResourceCSharp.FolderEmbed("../../../structure/markdown/.bookSettings/")]
    public static partial System.ReadOnlySpan<byte> GetResouceBookSettings(System.ReadOnlySpan<char> path);

    [EmbedResourceCSharp.FolderEmbed("../../../structure/markdown/.settings/")]
    public static partial System.ReadOnlySpan<byte> GetResouceSettings(System.ReadOnlySpan<char> path);

    [EmbedResourceCSharp.FolderEmbed("../../../structure/markdown/.output/")]
    public static partial System.ReadOnlySpan<byte> GetResouceOutput(System.ReadOnlySpan<char> path);

    [EmbedResourceCSharp.FolderEmbed("../../../structure/markdown/book/")]
    public static partial System.ReadOnlySpan<byte> GetResouceBook(System.ReadOnlySpan<char> path);

    [EmbedResourceCSharp.FolderEmbed("../../../structure/markdown/","*.html",SearchOption.TopDirectoryOnly)]
    public static partial System.ReadOnlySpan<byte> GetResouceRoot(System.ReadOnlySpan<char> path);

    [EmbedResourceCSharp.FolderEmbed("../../../structure/markdown/.pandoc/", "*.*t*", SearchOption.TopDirectoryOnly)]
    public static partial System.ReadOnlySpan<byte> GetResoucePandoc(System.ReadOnlySpan<char> path);

    public static byte[] GetPandocZip => ThisAssembly.Resources.pandoc.GetBytes();
}
