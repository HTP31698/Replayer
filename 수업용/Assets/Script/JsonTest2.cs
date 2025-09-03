using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public class GameObjDataList
{
    public List<GameObjData> gameObjDatas = new List<GameObjData>();
}
[Serializable]
public class GameObjData
{
    public Vector3 position;
    public Vector3 localscale;
    public Quaternion rotation;
    public Color color;
}

public class JsonTest2 : MonoBehaviour
{
    public static readonly string fileName = "cube.json";
    public static readonly string refileName = "replay.json";

    public static string fileFullPath =>
        Path.Combine(Application.persistentDataPath, fileName);

    public static string refileFullPath =>
        Path.Combine(Application.persistentDataPath, refileName);

    public List<GameObject> targetlist = new List<GameObject>();
    public GameObject newObject;


    public void Save()
    {
        var objs = new GameObjDataList();
        foreach (var target in targetlist)
        {
            var renderer = target.GetComponent<Renderer>();
            var obj = new GameObjData()
            {
                position = target.transform.position,
                localscale = target.transform.localScale,
                rotation = target.transform.rotation,
                color = renderer.material.color
            };
            objs.gameObjDatas.Add(obj);
        }
        var json = JsonConvert.SerializeObject(objs,
            new Vector3Converter(), new QuaternionConverter(), new ColorConverter());
        File.WriteAllText(fileFullPath, json);
    }
    public void Load()
    {
        for (int i = 0; i < targetlist.Count; i++)
        {
            Destroy(targetlist[i]);
        }
        targetlist.Clear();
        var json = File.ReadAllText(fileFullPath);
        var objs = JsonConvert.DeserializeObject<GameObjDataList>
            (json, new Vector3Converter(), new QuaternionConverter(), new ColorConverter());
        for (int i = 0; i < objs.gameObjDatas.Count; i++)
        {
            GameObject go = Instantiate(newObject);
            var obj = objs.gameObjDatas[i];
            go.transform.position = obj.position;
            go.transform.rotation = obj.rotation;
            go.transform.localScale = obj.localscale;
            go.GetComponent<Renderer>().material.color = obj.color;
            targetlist.Add(go);
        }
    }

    public void NewObject()
    {
        Vector3 posrand = new Vector3(UnityEngine.Random.Range(-20, 20),
            UnityEngine.Random.Range(1, 20), UnityEngine.Random.Range(1, 10));

        Vector3 scalerand = new Vector3(UnityEngine.Random.Range(1, 3),
            UnityEngine.Random.Range(1, 3), UnityEngine.Random.Range(1, 3));

        Quaternion quaterand = new Quaternion(UnityEngine.Random.Range(1, 3),
            UnityEngine.Random.Range(1, 3), UnityEngine.Random.Range(1, 3),
            UnityEngine.Random.Range(1, 3));

        Color color = new Color(UnityEngine.Random.Range(0, 256) / 255f,
    UnityEngine.Random.Range(0, 256) / 255f, UnityEngine.Random.Range(0, 256) / 255f,
    UnityEngine.Random.Range(0, 256) / 255f);
        GameObject obj = Instantiate(newObject);
        obj.transform.position = posrand;
        obj.transform.localScale = scalerand;
        obj.transform.rotation = quaterand;
        obj.GetComponent<Renderer>().material.color = color;
        targetlist.Add(obj);
    }

    public void Recording()
    {
        var objs = new GameObjDataList();
        foreach (var target in targetlist)
        {
            var renderer = target.GetComponent<Renderer>();
            var obj = new GameObjData()
            {
                position = target.transform.position,
                localscale = target.transform.localScale,
                rotation = target.transform.rotation,
                color = renderer.material.color
            };
            objs.gameObjDatas.Add(obj);
        }
        var json = JsonConvert.SerializeObject(objs,
            new Vector3Converter(), new QuaternionConverter(), new ColorConverter());
        File.WriteAllText(refileFullPath, json);
    }

    public void Replay()
    {
        for (int i = 0; i < targetlist.Count; i++)
        {
            Destroy(targetlist[i]);
        }
        targetlist.Clear();
        var json = File.ReadAllText(refileFullPath);
        var objs = JsonConvert.DeserializeObject<GameObjDataList>
            (json, new Vector3Converter(), new QuaternionConverter(), new ColorConverter());
        for (int i = 0; i < objs.gameObjDatas.Count; i++)
        {
            GameObject go = Instantiate(newObject);
            var obj = objs.gameObjDatas[i];
            go.transform.position = obj.position;
            go.transform.rotation = obj.rotation;
            go.transform.localScale = obj.localscale;
            go.GetComponent<Renderer>().material.color = obj.color;
            targetlist.Add(go);
        }
    }
}
