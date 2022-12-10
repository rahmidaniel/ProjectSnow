using System;
using System.IO;
using UnityEngine;

namespace _Scripts.Utility.Serialization
{
    public class SaveFileHandler
    {
        private readonly string _path;
        private readonly string _filename;

        public SaveFileHandler(string filename, string path)
        {
            _filename = filename;
            _path = path;
        }

        public GameData LoadData()
        {
            GameData gameData = null;
            
            var location = Path.Combine(_path, _filename);
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(_path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_path) ?? string.Empty);
                }

                using var fileStream = new FileStream(location, FileMode.Open);
                using var streamReader = new StreamReader(fileStream);
                var data = streamReader.ReadToEnd();

                gameData = JsonUtility.FromJson<GameData>(data);
                if (gameData.dead) return null; // Dead saves shouldn't pop up
            }
            catch (FileNotFoundException ignore)
            {
                Debug.Log("Save file doesn't exist");
                return gameData;
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred when trying to save.\n" + e);
            }
            

            return gameData;
        }

        public void SaveData(GameData gameData)
        {
            var location = Path.Combine(_path, _filename);
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