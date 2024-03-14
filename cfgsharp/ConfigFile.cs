using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
#if !NET6_0_OR_GREATER
using System.Linq;
#endif
using System.Text;

namespace CfgSharp
{
    /// <summary>
    /// This class represents a configuration file handler in the CfgSharp namespace.
    /// <para>It provides methods to create, read, modify, and delete entries within a configuration file.</para>
    /// </summary>
    public class ConfigFile
    {
        private readonly string _path;
        private readonly Dictionary<string, string> _cfgs;

        /// <summary>
        /// Initializes a new instance of the ConfigFile class with the specified file path.
        /// <para>If the file does not exist, it creates a new configuration file.</para>
        /// <para>If the file exists, it reads the existing configuration.</para>
        /// </summary>
        /// <param name="path">The path to the configuration file.</param>
        public ConfigFile(string path)
        {
            _cfgs = new Dictionary<string, string>();
            _path = path;
            if (!File.Exists(_path))
            {
                CreateConfigFile();
            }
            else
            {
                ReadConfigFile();
            }
        }

        private void CreateConfigFile()
        {
            try
            {
                using (FileStream fs = new FileStream(_path, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine("# Config File");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating config file: {ex.Message}");
            }
        }


        private void ReadConfigFile()
        {
            try
            {
                string[] lines = File.ReadAllLines(_path);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line) && line.Contains('='))
                    {
                        string[] parts = line.Split('=');
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();
                        _cfgs[key] = value;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading config file: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new entry with the provided key and value to the configuration file.
        /// <para>If an entry with the same key already exists, it does not add the new entry.</para>
        /// </summary>
        /// <param name="key">The key of the entry to add.</param>
        /// <param name="value">The value of the entry to add.</param>
        public void AddEntry(string key, string value)
        {
            if (!_cfgs.ContainsKey(key))
            {
                _cfgs[key] = value;
                SaveChanges();
            }
            else
            {
                Debug.WriteLine("Entry with the same key already exists.");
            }
        }

        /// <summary>
        /// Sets the value of an existing entry with the provided key in the configuration file.
        /// <para>If the entry does not exist, it creates a new entry with the specified key and value.</para>
        /// </summary>
        /// <param name="key">The key of the entry to set.</param>
        /// <param name="value">The new value of the entry.</param>
        public void SetValue(string key, string value)
        {
            _cfgs[key] = value;
            SaveChanges();
        }

        /// <summary>
        /// Retrieves the value of the entry with the specified key from the configuration file.
        /// </summary>
        /// <param name="key">The key of the entry to retrieve.</param>
        /// <returns>The value associated with the specified key. If the entry does <b>not exist</b> returns <b>empty string</b>.</returns>
        public string GetValue(string key)
        {
            if (_cfgs.ContainsKey(key))
                return _cfgs[key];
            else
                return string.Empty;
        }

        /// <summary>
        /// Deletes the entry with the specified key from the configuration file, if it exists.
        /// </summary>
        /// <param name="key">The key of the entry to delete.</param>
        public void DeleteEntry(string key)
        {
            if (_cfgs.ContainsKey(key))
            {
                _cfgs.Remove(key);
                SaveChanges();
            }
        }

        private void SaveChanges()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_path))
                {
                    foreach (KeyValuePair<string, string> entry in _cfgs)
                    {
                        writer.WriteLine($"{entry.Key} = {entry.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error writing to config file: {ex.Message}");
            }
        }
    }
}