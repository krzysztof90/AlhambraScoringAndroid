using AlhambraScoringAndroid.GamePlay;
using Android.Content;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AlhambraScoringAndroid.Options
{
    public static class Settings
    {
        private static readonly string settingsFileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "results.xml");

        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

        public static List<ResultHistory> GetResults()
        {
            SaveData saveData = null;

            if (File.Exists(settingsFileName))
                using (XmlReader reader = XmlReader.Create(settingsFileName))
                    saveData = (SaveData)serializer.Deserialize(reader);

            return saveData?.Results ?? new List<ResultHistory>();
        }

        public static void SaveResults(List<ResultHistory> results, Context context)
        {
            SaveData saveData = new SaveData()
            {
                Version = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName,
                Results = results
            };

            using (XmlWriter writer = XmlWriter.Create(settingsFileName))
            {
                serializer.Serialize(writer, saveData);
            }
        }
    }
}