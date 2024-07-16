# Steps to start

## Installation

Download latest version of the software from github.

Run 
```
MakeBookCLI i --folder 
MakeBookCLI gmk --folder
```
The first command will init the structure.

The second one will start to  

Modify the documents on the book folder. The program will execute continuously and generate the html and doc documents


## Advanced 

If you know pandoc and want to modify the generating 

### Errors 

### PDF

If you want the pdf , then you must run
```
choco install pandoc
choco install rsvg-convert python miktex
```

And restart the application . 

Also modify in .bookSettings/bookData.json the enabled value from false to true 