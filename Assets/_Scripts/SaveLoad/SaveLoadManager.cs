using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor.Build.Content;
using UnityEngine;

namespace TopDown.SaveLoad
{
    public class SaveLoadManager : MonoBehaviour
    {
        private string saveFilePath;
        private GameData gameData;
        
        void Awake()
        {
            saveFilePath = Application.persistentDataPath + "ProjNimbus.json";

            gameData = new GameData();
        }

        private void SaveGame()
        {
            gameData.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            
            string json = JsonUtility.ToJson(gameData);
            
            File.WriteAllText(saveFilePath, json);
            
            Debug.Log("Save Successful! Path: " + saveFilePath);
        }

        public void LoadGame()
        {
            if (File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath);
                
                gameData = JsonUtility.FromJson<GameData>(json);
                
                GameObject.FindGameObjectWithTag("Player").transform.position = gameData.playerPosition;
                
                Debug.Log("Load Successful!");
            }
            else
            {
                Debug.Log("Load Failed!");
                //In Middle Process
                gameData = new GameData();
            }
        }

        #region Input
        private void OnSaveGame()
        {
            new Thread(SaveGame).Start();
        }
        private void OnLoadGame()
        {
            LoadGame();
        }
        #endregion
        
    }
}
