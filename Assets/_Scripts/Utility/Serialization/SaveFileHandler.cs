using System;
using System.IO;
using UnityEngine;

namespace _Scripts.Utility.Serialization
{
    public class SaveFileHandler
    {
        private readonly string filename;
        private readonly string path;

        public SaveFileHandler(string filename, string path)
        {
            this.filename = filename;
            this.path = path;
        }

        public GameData LoadData()
        {
            GameData gameData = null;

            var location = Path.Combine(path, filename);
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);

                using var fileStream = new FileStream(location, FileMode.Open);
                using var streamReader = new StreamReader(fileStream);
                var data = streamReader.ReadToEnd();

                gameData = JsonUtility.FromJson<GameData>(data);
                if (gameData.dead) return null; // Dead saves shouldn't pop up
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred when trying to save.\n" + e);
            }


            return gameData;
        }

        public void SaveData(GameData gameData)
        {
            var location = Path.Combine(path, filename);
            try
            {
                var data = JsonUtility.ToJson(gameData, true);

                using var fileStream = new FileStream(location, FileMode.Create);
                using var streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(data);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred when trying to save.\n" + e);
            }
        }
    }
}