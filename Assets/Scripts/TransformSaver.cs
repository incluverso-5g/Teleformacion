using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class TransformData
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public List<TransformData> children = new List<TransformData>();
}

public class TransformSaver : MonoBehaviour
{
    public string fileName = "TransformData.json";
    public bool captureTrigger = false;

    public void SaveTransform()
    {
        
        TransformData data = SaveTransformRecursive(transform);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName), json);
        Debug.Log("Transform saved to: " + Path.Combine(Application.persistentDataPath, fileName));
    }

    public void LoadTransform()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            TransformData data = JsonUtility.FromJson<TransformData>(json);
            LoadTransformRecursive(transform, data);
            Debug.Log("Transform loaded from: " + path);
        }
        else
        {
            Debug.LogError("Save file not found: " + path);
        }
    }

    private TransformData SaveTransformRecursive(Transform t)
    {
        TransformData data = new TransformData
        {
            name = t.name,
            position = t.localPosition,
            rotation = t.localRotation,
            scale = t.localScale
        };

        foreach (Transform child in t)
        {
            data.children.Add(SaveTransformRecursive(child));
        }
        return data;
    }

    private void LoadTransformRecursive(Transform t, TransformData data)
    {
        t.localPosition = data.position;
        t.localRotation = data.rotation;
        t.localScale = data.scale;

        for (int i = 0; i < Mathf.Min(t.childCount, data.children.Count); i++)
        {
            LoadTransformRecursive(t.GetChild(i), data.children[i]);
        }
    }

    void Update() {
        if (captureTrigger){
            SaveTransform();
            captureTrigger= false;
        }
    }
}