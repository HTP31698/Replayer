using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

[Serializable]
public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 vector3;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health} / {vector3}";
    }
}
public class JsonTest1 : MonoBehaviour
{
    private void Start()
    {
        var obj = new PlayerState
        {
            playerName = "ABC",
            lives = 10,
            health = 10.999f,
            vector3 = Vector3.zero
        };

        var path = Path.Combine(Application.persistentDataPath, "test.json");
        string json = JsonConvert.SerializeObject(obj, 
            Formatting.Indented, new Vector3Converter());
        File.WriteAllText(path, json);

        var json2 = File.ReadAllText(path);
        var obj2 =JsonConvert.DeserializeObject<PlayerState>(json2);
        Debug.Log(obj2);
        
    }
}