using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Globalization;
public class CSVHandDataLoader : MonoBehaviour
{
    public string filePath = "hand_data.csv";
    public Transform RightHand,LeftHand;
    private Dictionary<string, GameObject> RightHandObjects = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> LeftHandObjects = new Dictionary<string, GameObject>();
    void Start()
    {
        LoadHandObjects();
        StartCoroutine(LoadCSVAndApplyTransforms());
    }

    void LoadHandObjects() //Esto hay que modificarlo para que lea LeftHand o RightHand
    {
        if (RightHand != null) { 
        foreach (Transform child in RightHand.GetComponentsInChildren<Transform>())
        {
            RightHandObjects[child.name] = child.gameObject;
            }
        }
        if (LeftHand != null) {
            foreach (Transform child in LeftHand.GetComponentsInChildren<Transform>())
            {
                LeftHandObjects[child.name] = child.gameObject;
            }
        }
    }

    IEnumerator LoadCSVAndApplyTransforms()
    {
        float startTime = Time.realtimeSinceStartup;
        string fullPath = Path.Combine(Application.streamingAssetsPath, filePath);
        if (!File.Exists(fullPath))
        {
            Debug.LogError("CSV file not found: " + fullPath);
            yield break;
        }

        string[] lines = File.ReadAllLines(fullPath);
        long previousTimestamp = -1;

        foreach (string line in lines)
        {
            string linenew = Regex.Replace(line, @",(?=(?:[^()]*\([^()]*\))*[^()]*$)", ";");
            string[] data = linenew.Split(';');
            if (data.Length < 5) continue;

            long timestamp = long.Parse(data[0], CultureInfo.InvariantCulture);
            string hand = data[1].Trim();
            string objectName = data[2].Trim();

            Vector3 NewPosition = ParseVector3(data[3]);
            Quaternion NewRotation = ParseQuaternion(data[4]);
            if (previousTimestamp > 0 && previousTimestamp != timestamp)
            {
                float endTime = Time.realtimeSinceStartup;
                float elapsedTime = endTime - startTime;
                float remainingTime = 0.5f - elapsedTime;

                Debug.Log("Finalized one iteration" + remainingTime);

                if (remainingTime > 0f)
                {
                    yield return new WaitForSecondsRealtime(remainingTime);
                }
                startTime = Time.realtimeSinceStartup;
            }
            previousTimestamp = timestamp;
            if (hand == "RightHand")
            {
                if (RightHandObjects.TryGetValue(objectName, out GameObject RightObj))
                {
                    RightObj.transform.position = NewPosition;
                    RightObj.transform.rotation = NewRotation;
                }

            }
            if (hand == "LeftHand")
            {
                if (LeftHandObjects.TryGetValue(objectName, out GameObject LeftObj))
                {
                    LeftObj.transform.position = NewPosition;
                    LeftObj.transform.rotation = NewRotation;
                }
            }
        }
    }

    Vector3 ParseVector3(string vectorString)
    {
        vectorString = vectorString.Replace("(", "").Replace(")", "");
        string[] values = vectorString.Split(',');
        return new Vector3(
            float.Parse(values[0], CultureInfo.InvariantCulture),
            float.Parse(values[1], CultureInfo.InvariantCulture),
            float.Parse(values[2], CultureInfo.InvariantCulture)
        );
    }

    Quaternion ParseQuaternion(string quaternionString)
    {
        quaternionString = quaternionString.Replace("(", "").Replace(")", "");
        string[] values = quaternionString.Split(',');
        return new Quaternion(
            float.Parse(values[0], CultureInfo.InvariantCulture),
            float.Parse(values[1], CultureInfo.InvariantCulture),
            float.Parse(values[2], CultureInfo.InvariantCulture),
            float.Parse(values[3], CultureInfo.InvariantCulture)
        );
    }
}
