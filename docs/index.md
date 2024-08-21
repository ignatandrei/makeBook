---
author: Andrei Ignat
title: Make a Book from markdown files
---

-   [Introduction of
    MakeBookCli](#introduction-of-makebookcli){#footoc-introduction-of-makebookcli}
    -   [Why ?](#why-){#footoc-why-}
    -   [About](#about){#footoc-about}
    -   [How the help manual it was
        created](#how-the-help-manual-it-was-created){#footoc-how-the-help-manual-it-was-created}
-   [Steps to use](#steps-to-use){#footoc-steps-to-use}
    -   [Installation](#installation){#footoc-installation}
    -   [Usage](#usage){#footoc-usage}
    -   [PDF](#pdf){#footoc-pdf}
-   [Advanced -
    Organization](#advanced---organization){#footoc-advanced---organization}
    -   [Folders](#folders){#footoc-folders}

## Introduction of MakeBookCli

### Why ? {#foowhy-}

Every now and then I have wanted to transform my investigations ,
written as blog posts, into books

Also occured to me that I want to write a book. But each chapter was
self sufficient. And transforming different chapters into a book
required manual labor . That for this software application, that has as
purpose to transform a chapter collection into a book ( i.e. a HTML
document or a Word document or a )

### About

My name is Andrei Ignat .

![Author](./Introduction_Assets/author.jpg "Author")

This software is open source and you can download from
<https://github.com/ignatandrei/makeBook>

### How the help manual it was created

Of course the help manual was created using this software. What is
better than dogfooding ?

If you want to edit, please go to
<https://github.com/ignatandrei/makeBook> and edit src/help files

You can download this help file as
`<a href="./index.html">`{=html}HTML`</a>`{=html} ,
`<a href="./index.docx">`{=html}Word`</a>`{=html} ,
`<a href="./index.pdf">`{=html}PDF`</a>`{=html} or
`<a href="./index.epub">`{=html}EPUB`</a>`{=html}

## Steps to use

### Installation

Download latest version of the software from github

<https://github.com/ignatandrei/makeBook/releases>

You will download an executable file - latest is
<https://github.com/ignatandrei/makeBook/releases/download/v8.2024.821.1823/MakeBookCLI.exe>

Note for Windows Users : Unblock the software prior to execute it.

Now run

    MakeBookCLI i --folder 
    MakeBookCLI gmk --folder

The first command will init the structure.

The second one will start to generate output ( html, doc,epub) from the
markdown files.

### Usage

#### Put title and author

Modify bookData.json file in the .bookSettings folder and change the
author ( obviously , your name ) and title ( obviously , the title of
the book )

#### Put the chapters in the book folder

Modify the documents on the book folder. The program will execute
continuously and generate the html and doc documents . Those can be seen
at the .output folder

### PDF

If you want the pdf , then you should install a PDF Engine . You could
install miktext with

``` json

choco install pandoc
choco install rsvg-convert python miktex
```

Modify in the .bookSettings/bookData.json

``` json

"valueNear": ".pandoc/pandoc.exe",
"value": "%LocalAppData%\\Pandoc\\pandoc.exe"
```

Also modify in .bookSettings/bookData.json the \"make an pdf with
miktext\" value

``` json
"enabled":true,
"redirectOutput":false
```

Close the app ( and the console that you are using )and restart the
application .

## Advanced - Organization

### Folders

There are 4 folders into the project: .output , .bookSettings, .pandoc ,
book

#### .output

This folder will contain the output of the application. The output can
be HTML, DOCX, EPUB, PDF or any other kind pandoc will generate

#### .pandoc

Here will be the pandoc executable with all files needed to generate
documents.

#### .bookSettings

Here will be the settings of the book. The most important file is
bookData.json . This is a sample:

``` json
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

#### book

Here you will put the chapters of the book. Each chapter will be a
markdown file. The order of the chapters will be the order of the files.
However, introduction.md will be the first ( if exists )

I suggest to put each image for a chapter in a separate folder with the
name of the chapter. This will help you to organize the images. ( It is
not necesssary, but it is a good practice )
