namespace MakeBookWin;

public partial class frmMakeBook : Form
{
    public frmMakeBook()
    {
        InitializeComponent();
    }
    private void btnHelp_Click(object sender, EventArgs e)
    {
        HelpData.WriteTutorial();
    }
    private void btnExistingFolder_Click(object sender, EventArgs e)
    {
        using DisableEnable disableEnable = new(btnInitFolder, btnExistingFolder);
        string folder= txtFolder.Text;
        if(string.IsNullOrWhiteSpace(folder))
        {
            MessageBox.Show("Please enter folder", "No Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        GenerateFromFolder(folder);
    }

    private void GenerateFromFolder(string folderWithFiles)
    {
        try
        {
            IGeneratorFiles generatorFiles = new GeneratorMarkdown(folderWithFiles);
            GenerateFromFolder generateFromFolder = new(folderWithFiles, generatorFiles);
            var result = generateFromFolder.GenerateNow();
            if(result)
            {
                string folder =Path.Combine( folderWithFiles,".output");
                MessageBox.Show($"Please see files in {folder}", "Generating", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("explorer.exe", folder);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void btnExit_Click(object sender, EventArgs e)
    {
        this.Close();
    }
    private void btnInitFolder_Click(object sender, EventArgs e)
    {
        var res = folderBrowserDialog1.ShowDialog();
        if (res != DialogResult.OK) return;
        var folder = folderBrowserDialog1.SelectedPath;
        if (string.IsNullOrWhiteSpace(folder)) return;
        if (Directory.GetDirectories(folder).Any())
        {
            MessageBox.Show($"Folder {folder} has folders! Please choose empty folder ", "Not Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        if (Directory.GetFiles(folder).Any())
        {
            MessageBox.Show($"Folder {folder} has files! Please choose empty folder ", "Not Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        using DisableEnable disableEnable = new(btnInitFolder, btnExistingFolder);
        InitFromFolder(folder);
        txtFolder.Text = folder;
        GenerateFromFolder(folder);
        //TODO: 2024-08-24 Init folder with files
        //var generator = new GeneratorFiles(folder);
        //var generateFromFolder = new GenerateFromFolder(folder, generator);
        //generateFromFolder.GenerateNow();

    }
    private void InitFromFolder(string folder)
    {
        InitFolderStructure initFolder = new(folder);
        initFolder.InitNow();

    }
}