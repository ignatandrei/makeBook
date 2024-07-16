# Steps to use

## Installation

Download latest version of the software from github
 
https://github.com/ignatandrei/makeBook/releases


Note for Windows Users :  Unblock the software prior to execute it.

Now run

```
MakeBookCLI i --folder 
MakeBookCLI gmk --folder
```

The first command will init the structure.

The second one will start to  generate output ( html, doc,epub) from the markdown files.

## Usage

### Put title and author

Modify bookData.json file in the .bookSettings folder and change the author ( obviously , your name ) and title ( obviously , the title of the book )

### Put the chapters in the book folder

Modify the documents on the book folder. The program will execute continuously and generate the html and doc documents . Those can be seen at the .output folder


## Advanced 


### PDF

If you want the pdf , then you should install a PDF Engine . You could install miktext with
```
choco install pandoc
choco install rsvg-convert python miktex
```

Also modify in .bookSettings/bookData.json the "make an pdf with miktext" value enabled from false to true 

Close the app ( and the console that you are using )and restart the application . 
