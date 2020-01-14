# winprint
WinPrint 2 - A modern take on the the classic from [1988](https://www.kindel.com/products/winprint/).

WinPrint is the perfect tool for printing source code, web pages, reports generated by legacy systems, documentation, or any text file. It works either interactively or from the command line making it perfect for single users or whole enterprises.

## Features
* Use from the Commandline/Console or GUI.
* Print source code, with syntax highlighting for over 200 langauges.
* Print "multiple-pages-up" on one piece of paper (saves trees!)
* Complete control over all page formatting options, including headers and footers, margins, fonts, page orientation, etc...
* Simple and elegant graphical user interface with accurate print preview.
* Complete command line interface. Allows WinPrint to be used from other applications or solutions.
* Supports Sheet Defintions for frequent print jobs.

## Command Line Interface
Examples:

Print Program.cs in landscape mode:

    winprint --landscape Program.cs

Print all .cs files on a specific printer with a specific paper size:

    winprint --printer "Fabricam 535" --paper-size A4 *.cs

Print the first two pages of Program.cs:

    winprint --from-page 1 --to-page 2 Program.cs

Print Program.cs using the 2 Up sheet defintion:

    winprint --sheet "2 Up" Program.cs

### `-s`, `--sheet` 

Sheet defintion to use for formatting. Use sheet ID or friendly name.

### `-l`, `--landscape`

Force landscape orientation.

  -r, --portrait       (Default: false) Force portrait orientation.

  -p, --printer        Printer name.

  -z, --paper-size     Paper size name.

  -f, --from-page      (Default: 0) Number of first page to print (may be used
                       with --to-page).

  -t, --to-page        (Default: 0) Number of last page to print (may be used
                       with --from-page).

  -c, --count-pages    (Default: false) Exit code is set to numer of pages that
                       would be printed. Use --verbose to diplsay the count.

  -v, --verbose        (Default: false) Verbose console output (log is always
                       verbose).

  -d, --debug          (Default: false) Debug-level console & log output.

  -g, --gui            (Default: false) Show WinPrint GUI (to preview or change
                       sheet settings).

  --help               Display this help screen.

  --version            Display version information.

  <files> (pos. 0)     Required. One or more files to be printed.
