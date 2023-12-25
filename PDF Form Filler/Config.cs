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
            var config = new Config();

            if (!File.Exists(path))
                throw new Exception($"config not found at {Directory.GetCurrentDirectory()}");

            string json = File.ReadAllText(path);
            config = JsonSerializer.Deserialize<Config>(json);
            this.Fields = config.Fields;
        }

        public void Save(string filePath)
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                    WriteIndented = true
                }
            );
            File.WriteAllText(filePath, json);
        }
    }
}