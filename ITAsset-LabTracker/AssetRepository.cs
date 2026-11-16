using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ITAsset_LabTracker
{
    public class AssetRepository
    {

        private readonly string _jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "campus_assets.json");

        // Phase 4: JSON Serialization Persistence
        public void SaveToJson(List<CampusAsset> assets)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(assets, options);
            File.WriteAllText(_jsonFilePath, jsonString);
        }

        public List<CampusAsset> LoadFromJson()
        {
            if (!File.Exists(_jsonFilePath))
                throw new FileNotFoundException("No saved JSON file found yet. Save your assets first.");

            string jsonString = File.ReadAllText(_jsonFilePath);
            return JsonSerializer.Deserialize<List<CampusAsset>>(jsonString) ?? new List<CampusAsset>();
        }
    }
}