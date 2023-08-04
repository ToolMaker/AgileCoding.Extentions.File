AgileCoding.Extensions.File
===========================

Overview
--------

AgileCoding.Extensions.File is a .NET 6.0 library that provides useful extensions for file related operations. This includes validating file existence, checking if a file is in use, creating a file name with a specific extension, and more.

Installation
------------

This library is available as a NuGet package. You can install it using the NuGet package manager console:

bashCopy code

`Install-Package AgileCoding.Extensions.File -Version 2.0.5`

Features
--------

The library provides various functionalities such as:

1.  Check File Existence: The `FileExist` extension method checks if a given file path points to an existing file.
2.  Create File and Delete If Exist: The `CreateFileAndDelteIfExist` extension method creates a new file at the given path and deletes any existing file.
3.  Create Filename With Extension: The `CreateFileNameWithExtention` extension method creates a filename with the given extension.
4.  Get File Hash: The `GetFileHash` extension method calculates and returns the SHA1 hash of a file.
5.  Read All Lines: The `AllLines` extension method reads all the lines of a file.
6.  File Is In Use: The `FileIsInUse` extension method checks if a file is being used by another process.
7.  Shift Directory Back: The `ShiftDirectotyBack` extension method shifts the directory back a specified number of times.

Usage
-----

Here's an example of how to use these features:

```csharp
using AgileCoding.Extentions.Files;
using System.IO;

string filePath = "your/file/path.txt";

// Check if file exists
bool exists = filePath.FileExist();

// Throw exception if file doesn't exist
filePath.ThrowIfFileDoesntExist<FileNotFoundException>("File does not exist");

// Create filename with extension
string filename = "myFile";
string newFilename = filename.CreateFileNameWithExtention("txt");

// Create file and delete if it exists
FileInfo file = filePath.CreateFileAndDelteIfExist();

// Read all lines in the file
IEnumerable<string> lines = file.AllLines();

// And so on...
```

Documentation
-------------

For more detailed information about the usage of this library, please refer to the [official documentation](https://github.com/ToolMaker/AgileCoding.Extentions.File/wiki).

License
-------

This project is licensed under the terms of the MIT license. For more information, see the [LICENSE](https://github.com/ToolMaker/AgileCoding.Extentions.File/blob/main/LICENSE) file.

Contributing
------------

Contributions are welcome! Please see our [contributing guidelines](https://github.com/ToolMaker/AgileCoding.Extentions.File/blob/main/CONTRIBUTING.md) for more details.