using PDF_Form_Filler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PDF_Form_Filler
{
    public class Config
    {
        [JsonPropertyName("Fields")]
        public List<PdfField> Fields { get; set; } = new List<PdfField>();

        public void Load(string path)
        {
            //var config = new Config();

            //// Load and deserialize settings
            //if (!File.Exists(path))
            //    throw new Exception($"config not found at {Directory.GetCurrentDirectory()}");

            //string json = File.ReadAllText(path);
            //config = JsonSerializer.Deserialize<Config>(json);

            //return config;
            
            
            
            
            
            var config = new Config();

            // Load and deserialize settings
            if (!File.Exists(path))
                throw new Exception($"config not found at {Directory.GetCurrentDirectory()}");

            string json = File.ReadAllText(path);
            config = JsonSerializer.Deserialize<Config>(json);
            this.Fields = config.Fields;
        }

        public void Save(string filePath)
        {
            // Serialize the Config object to JSON
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true // Makes the JSON output more readable with indentation
            });

            // Write the JSON to a file
            File.WriteAllText(filePath, json);
        }

        //public List<KeyValuePair<string, string>> Kunde { get; set; }
        //public List<KeyValuePair<string, string>> Selbst { get; set; }
        //public List<KeyValuePair<string, string>> Textfields { get; set; }
        //public List<KeyValuePair<string, string>> Checkboxes { get; set; }
        //public List<KeyValuePair<string, string>> Groups { get; set; }
    }
}