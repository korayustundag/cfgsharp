# CfgSharp

CfgSharp is a lightweight C# library for handling configuration files. It provides a simple interface to create, read, modify, and delete entries within a configuration file.

## Features

- **Easy to Use**: CfgSharp offers a straightforward API for managing configuration files in C# projects.
- **Add, Update, Delete**: You can easily add, update, or delete configuration entries using simple method calls.
- **Robust Handling**: The library handles file I/O operations robustly, with error handling for better reliability.
- **Minimalistic**: CfgSharp is designed to be lightweight and minimalistic, making it suitable for small to medium-sized projects.

## Installation

To use CfgSharp in your C# project, you can either clone this repository or install it via .NET CLI:

```bash
> dotnet add package CfgSharp --version 1.0.0
```

## Usage

```csharp
using CfgSharp;

// Create a new instance of ConfigFile with the path to your configuration file
ConfigFile cfg = new ConfigFile("path/to/config/file.cfg");

// Add a new entry to the configuration file
cfg.AddEntry("key", "value");

// Retrieve the value of an entry
string value = cfg.GetValue("key");

// Update the value of an entry
cfg.SetValue("key", "new_value");

// Delete an entry from the configuration file
cfg.DeleteEntry("key");
```

## Contributing

Contributions are welcome! If you have any ideas, suggestions, or bug fixes, feel free to open an issue or submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/korayustundag/cfgsharp/blob/main/LICENSE) file for details.
