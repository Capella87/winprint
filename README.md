![.NET Core](https://github.com/tig/winprint/workflows/.NET%20Core/badge.svg)
[![CodeFactor](https://www.codefactor.io/repository/github/tig/winprint/badge?s=93affda0738af869187afe5296914b814511f529)](https://www.codefactor.io/repository/github/tig/winprint)

# winprint 2.0

*winprint - A modern take on the the classic source code printing app from [1988](https://tig.github.io/winprint/about.html).*

Advanced source code and text file printing for PowerShell. The perfect tool for printing source code, web pages, reports generated by legacy systems, documentation, or any text or HTML file. It works interactively or from the command line making it great for single users or whole enterprises.

* Prints source code with syntax highlighting and line numbering for [over 200 programming languages and file formats](https://pygments.org/languages/).
* Prints HTML files.
* Prints "multiple-pages-up" on one piece of paper (saves trees!)
* Complete control over page formatting options, including headers and footers, margins, fonts, page orientation, etc...
* Headers and Footers support detailed file and print information macros with rich date/time formatting.
* Simple and elegant graphical user interface with accurate print preview.
* The most capable PowerShell printing tool enabling printing from the command line.
  * Complete control of printing features with dozens of parameters, including *Intellicode* parameter completion (using `tab` key).
  * Allows **winprint** to be used from other applications or solutions. The **winpprint** PowerShell `Out-WinPrint` CmdLet is a drop-in replacement for `out-printer`.
* Sheet Definitions make it easy to save settings for frequent print jobs.
* Comprehensive logging.
* Cross platform. Even though it's named **win**print, it works on Windows, Linux (command line only; some assembly required), and (not yet tested) Mac OS.

## Documentation

* [Overview](https://tig.github.io/winprint/)
* [Install](https://tig.github.io/winprint/install.html)
* [User's Guide](https://tig.github.io/winprint/users-guide.html)
* [About](https://tig.github.io/winprint/about.html)
* [Support](https://tig.github.io/winprint/support.html)

## Graphical Interface

![winprint 2.0](https://tig.github.io/winprint/winprint2.png)

## PowerShell Command Line Interface

See what version is installed:

```powershell
PS > Out-WinPrint -verbose
VERBOSE: Out-WinPrint 2.0.5.0 - Copyright Kindel Systems, LLC - https://tig.github.io/winprint
```

Print a Powershell profile using the default sheet definition and default printer:

```powershell
Get-Content $profile.CurrentUserAllHosts | winprint -Language powershell
```

```powershell
PS > cat Program.cs | wp -PrinterName PDF -Orientation Portrait -Verbose -Title Program.cs
VERBOSE: Out-WinPrint 2.0.5.0 - Copyright Kindel Systems, LLC - https://tig.github.io/winprint
VERBOSE:     Printer:          PDF
VERBOSE:     Paper Size:       Letter
VERBOSE:     Orientation:      Portrait
VERBOSE:     Sheet Definition: Default 2-Up (0002a500-0000-0000-c000-000000000046)
VERBOSE: Printing sheet 1
VERBOSE: Printing sheet 2
VERBOSE: Printed a total of 2 sheets.
PS >
```

The following all do the same thing:

```powershell
Out-WinPrint -FileName program.cs
wp program.cs
winprint program.cs
cat program.cs | wp -Title "program.cs"
```

Print all `.c` and `.h` files in the current directory to the "HP LaserJet" printer, ensuring the `{Title`} in the header/footers shows the filename. Present verbose output along the way:

```powershell
ls .\* -include ('*.c', '*.h') | foreach { cat $_.FullName | Out-WinPrint -p "HP LaserJet" -title $_.FullName -verbose}
```

## Contributing

I'm open to pull requests. I'll also take donations, preferably in beer or Scotch.
