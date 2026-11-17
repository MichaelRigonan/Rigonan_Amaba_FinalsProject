using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ITAsset_LabTracker
{
    public class AssetRepository
    {
        // Shared path in your Documents folder so both WinForms and Swagger use the same file
        private readonly string _jsonFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "campus_assets.json"
        );

        public void SaveToJson(List<CampusAsset> assets)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(assets, options);
            File.WriteAllText(_jsonFilePath, jsonString);
        }

        public List<CampusAsset> LoadFromJson()
        {
            if (!File.Exists(_jsonFilePath))
                return new List<CampusAsset>(); // Returns an empty list safely if file doesn't exist yet

            string jsonString = File.ReadAllText(_jsonFilePath);
            return JsonSerializer.Deserialize<List<CampusAsset>>(jsonString) ?? new List<CampusAsset>();
        }
    }
}