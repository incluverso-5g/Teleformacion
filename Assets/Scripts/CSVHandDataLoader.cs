using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVHandDataLoader : MonoBehaviour
{
    public string filePath = "hand_data.csv";
    private Dictionary<string, GameObject> handObjects = new Dictionary<string, GameObject>();
    
    void Start()
    {
        LoadHandObjects();
        StartCoroutine(LoadCSVAndApplyTransforms());
    }

    void LoadHandObjects()
    {
        foreach (Transform child in transform)
        {
            handObjects[child.name] = child.gameObject;
        }
    }

    IEnumerator LoadCSVAndApplyTransforms()
    {
        string fullPath = Path.Combine(Application.streamingAssetsPath, filePath);
        if (!File.Exists(fullPath))
        {
            Debug.LogError("CSV file not found: " + fullPath);
            yield break;
        }

        string[] lines = File.ReadAllLines(fullPath);
        float previousTimestamp = -1f;

        foreach (string line in lines)
        {
            string[] data = line.Split(',');
            if (data.Length < 6) continue;

            long timestamp = long.Parse(data[0]);
            string hand = data[1];
            string objectName = data[2].Trim();

            Vector3 position = ParseVector3(data[3]);
            Quaternion rotation = ParseQuaternion(data[4]);

            if (handObjects.TryGetValue(objectName, out GameObject obj))
            {
                obj.transform.localPosition = position;
                obj.transform.localRotation = rotation;
            }

            if (previousTimestamp > 0 && previousTimestamp != timestamp)
            {
                yield return new WaitForSeconds(0.02f); // Simulate frame delay
            }
            previousTimestamp = timestamp;
        }
    }

    Vector3 ParseVector3(string vectorString)
    {
        vectorString = vectorString.Replace("(", "").Replace(")", "");
        string[] values = vectorString.Split(' ');
        return new Vector3(
            float.Parse(values[0]),
            float.Parse(values[1]),
            float.Parse(values[2])
        );
    }

    Quaternion ParseQuaternion(string quaternionString)
    {
        quaternionString = quaternionString.Replace("(", "").Replace(")", "");
        string[] values = quaternionString.Split(' ');
        return new Quaternion(
            float.Parse(values[0]),
            float.Parse(values[1]),
            float.Parse(values[2]),
            float.Parse(values[3])
        );
    }
}
