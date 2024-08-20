

namespace MakeBookWin;

partial class frmMakeBook
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        btnHelp = new Button();
        btnInitFolder = new Button();
        folderBrowserDialog1 = new FolderBrowserDialog();
        btnExistingFolder = new Button();
        txtFolder = new TextBox();
        btnExit = new Button();
        lblFolder = new Label();
        SuspendLayout();
        // 
        // btnHelp
        // 
        btnHelp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnHelp.Location = new Point(585, 34);
        btnHelp.Name = "btnHelp";
        btnHelp.Size = new Size(150, 46);
        btnHelp.TabIndex = 0;
        btnHelp.Text = "&Help";
        btnHelp.UseVisualStyleBackColor = true;
        btnHelp.Click += btnHelp_Click;
        // 
        // btnInitFolder
        // 
        btnInitFolder.Location = new Point(32, 34);
        btnInitFolder.Name = "btnInitFolder";
        btnInitFolder.Size = new Size(198, 46);
        btnInitFolder.TabIndex = 1;
        btnInitFolder.Text = "&Init Book";
        btnInitFolder.UseVisualStyleBackColor = true;
        btnInitFolder.Click += btnInitFolder_Click;
        // 
        // btnExistingFolder
        // 
        btnExistingFolder.Location = new Point(32, 283);
        btnExistingFolder.Name = "btnExistingFolder";
        btnExistingFolder.Size = new Size(186, 46);
        btnExistingFolder.TabIndex = 2;
        btnExistingFolder.Text = "&Generate Book";
        btnExistingFolder.UseVisualStyleBackColor = true;
        btnExistingFolder.Click += btnExistingFolder_Click;
        // 
        // txtFolder
        // 
        txtFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtFolder.Location = new Point(32, 227);
        txtFolder.Name = "txtFolder";
        txtFolder.Size = new Size(478, 39);
        txtFolder.TabIndex = 3;
        // 
        // btnExit
        // 
        btnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnExit.Location = new Point(585, 283);
        btnExit.Name = "btnExit";
        btnExit.Size = new Size(150, 46);
        btnExit.TabIndex = 4;
        btnExit.Text = "E&xit";
        btnExit.UseVisualStyleBackColor = true;
        btnExit.Click += btnExit_Click;
        // 
        // lblFolder
        // 
        lblFolder.AutoSize = true;
        lblFolder.Location = new Point(37, 182);
        lblFolder.Name = "lblFolder";
        lblFolder.Size = new Size(329, 32);
        lblFolder.TabIndex = 5;
        lblFolder.Text = "Folder that contains the book";
        // 
        // frmMakeBook
        // 
        AutoScaleDimensions = new SizeF(13F, 32F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(lblFolder);
        Controls.Add(btnExit);
        Controls.Add(txtFolder);
        Controls.Add(btnExistingFolder);
        Controls.Add(btnInitFolder);
        Controls.Add(btnHelp);
        Name = "frmMakeBook";
        Text = "Make A Book";
        ResumeLayout(false);
        PerformLayout();
    }


    #endregion

    private Button btnHelp;
    private Button btnInitFolder;
    private FolderBrowserDialog folderBrowserDialog1;
    private Button btnExistingFolder;
    private TextBox txtFolder;
    private Button btnExit;
    private Label lblFolder;
}
