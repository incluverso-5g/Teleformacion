using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Oculus.Interaction;
public class TransformLogger : MonoBehaviour
{
    public HandVisual RightHand;
    public HandVisual LeftHand;
    private string filePath;
    private Coroutine loggingCoroutine;
    void Start()
    {
        long epochTime = new System.DateTimeOffset(System.DateTime.UtcNow).ToUnixTimeSeconds();
        filePath = Path.Combine(Application.persistentDataPath, $"TransformLog_{epochTime}.txt");
        loggingCoroutine = StartCoroutine(LogTransforms());
    }

    IEnumerator LogTransforms()
    {
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            while (true)
            {
                string Time = new System.DateTimeOffset(System.DateTime.UtcNow).ToUnixTimeMilliseconds().ToString();
                foreach (Transform t in RightHand.Joints)
                {
                    writer.WriteLine(Time + ", RightHand," + t.name + ", " + t.position.ToString("F8") + "," + t.rotation);
                }
                writer.Flush(); // Ensure data is written to the file
                foreach (Transform t in LeftHand.Joints)
                {
                    writer.WriteLine(Time + ", LeftHand," + t.name + ", " + t.position.ToString("F8") + "," + t.rotation);
                }
                writer.Flush(); // Ensure data is written to the file
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    void OnDestroy()
    {
        if (loggingCoroutine != null)
        {
            StopCoroutine(loggingCoroutine);
        }
    }
}

