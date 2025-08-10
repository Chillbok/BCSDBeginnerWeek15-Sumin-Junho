using System.IO;
using UnityEngine;

public static class SaveGame
{
    public static void SaveData(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, "gamedata.json");
        File.WriteAllText(path, json);
    }

    public static GameData LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "gamedata.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);
            return data;
        }
        return null; //저장된 파일이 없을 때
    }
}
