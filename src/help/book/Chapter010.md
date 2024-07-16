# Advanced - Organization

## Folders

There are 4 folders into the project: .output , .bookSettings, .pandoc , book

### .output

This folder will contain the output of the application. The output can be HTML, DOCX, EPUB, PDF or any other kind pandoc will generate

### .pandoc

Here will be the pandoc executable with all files needed to generate documents.

### .bookSettings

Here will be the settings of the book. The most important file is bookData.json . This is a sample:

```json
{
    "book":{
        "title":"The book title",
        "author":"Your name"
    },
    "locations": [
    {
      "name": "pandoc",
      "value": ".pandoc/pandoc.exe",
      "valueIfChocoInstalled": "%LocalAppData%\\Pandoc\\pandoc.exe"
    }

  ],
    "commands": [
      {
        "name": "make a html",
        "value": "-d .settings/pandocHTML.yaml --resource-path book --metadata=title:\"{title}\" --metadata=author:\"{author}\" --title \"{title}\" -o .output/index.html",
        "enabled":true
      },      
    ]
}

```

### book

Here you will put the chapters of the book. Each chapter will be a markdown file. The order of the chapters will be the order of the files.
However, introduction.md will be the first ( if exists )

I suggest to put each image for a chapter in a separate folder with the name of the chapter. This will help you to organize the images.
( It is not necesssary, but it is a good practice )

