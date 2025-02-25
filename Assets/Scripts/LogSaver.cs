using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class LogSaver : MonoBehaviour {
    string LogUrl;
    string VideoUrl;
    string state = "Idle";
    string segment;
	public string UserId;
    public string NSession;
    public string Mano;
    public string tiempo_votacion;
    public Camera UserCamera;
    public string Event = "none";
    long duration;
    long start;

   public void SetLogState(string newstate)
    {
        state = newstate;
    }

    public void SetLogEvent(string newEvent)
    {
        Event = newEvent;
    }
    // Use this for initialization
    void Start () {
       // Randomizer Rand = GetComponent<Randomizer>();
       // expCntrl = GetComponent<ExperimentController>();
        if (LogUrl == null)
        {

#if UNITY_ANDROID && !UNITY_EDITOR
        LogUrl = Application.persistentDataPath + "/Result_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
#else
            LogUrl = Application.persistentDataPath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
#endif
            Debug.Log(LogUrl);
        }


        if (!File.Exists(LogUrl))
        {
            // Create a file to write to.
            
            using (StreamWriter sw = File.CreateText(LogUrl))
            {
                sw.WriteLine("UserID," + UserId + ",NSession," + NSession + ",ManoE4," + Mano + ",TiempoVotacion," + tiempo_votacion);
            }
        }
        /*segment =Rand.segment.ToString();
        duration = Rand.secuencias[0].duration;
        start = Rand.secuencias[0].start;*/
        //StartCoroutine(updateGaze());
    }


    // Update is called once per frame
    void Update () {
        Vector3 cabeza = UserCamera.transform.eulerAngles;
        Vector3 position = UserCamera.transform.position;
        string UnixTime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds().ToString();
        //state = ExperimentController.;
        //state = (GetComponent<VideoPlayer>().isPlaying ? "Sync" : "IDLE");
        using (StreamWriter sw = File.AppendText(LogUrl))
        {
            if (Event.Contains("Votado"))
            {
                Event = Event.Replace("Votado", "");
                sw.WriteLine(UnixTime + ",STATE," + state + ",EVENT," + "Votado," + "questionnaire," + "SSCQE" + Event);
            }
            else 
            {
                sw.WriteLine(UnixTime + ",STATE," + state + ",EVENT," + Event + ",LOOK_AT," + cabeza.x + "," + cabeza.y + "," + cabeza.z + ",POSITION," + position.x + "," + position.y + "," + position.z);
            }
        }
        //(UnixTime + ",STATE," + state + ",EVENT,"+ Event " + "questionnaire," + "SSCQE" + "," + points.ToString()

        Event = "none";
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application has ended after " + Time.time + " seconds");
    }





    /*void PrepareDone(VideoPlayer vp)
{


    string in_seq_method = "";
    if (( in_seq_method = Rand.secuencias[0].in_seq_method) != null){
        Secuencias in_seq = Rand.secuencias[0];
        if (in_seq_method == "sscqe")
        {
           GameObject.Find("Script").GetComponent<SSCQ>().enabled = true; SSQC to be developed
        }else if (in_seq_method == "ssdqe")
        {
            GameObject.Find("Script").GetComponent<Exit>().SetTimerOn(in_seq.ssdqe_duration, in_seq.ssdqe_start, in_seq.ssdqe_period, in_seq.ssdqe_total_number);
            GameObject.Find("Plane").SetActive(false);
        } else GameObject.Find("Plane").SetActive(false);
    }else GameObject.Find("Plane").SetActive(false);
    if (GameObject.Find("PlayListObject").GetComponent<Randomizer>().secuencias[0].stereo == "TopDown")
        skyboxmaterial.SetFloat("_Layout", 2.0f);

    else
        skyboxmaterial.SetFloat("_Layout", 0.0f);
    GetComponent<VideoPlayer>().Play();
}
public void WriteSSCQ(int points)
{
    using (StreamWriter sw = File.AppendText(LogUrl))
    {
        sw.WriteLine((DateTime.Now.TimeOfDay.TotalMilliseconds * 1000000).ToString() + "," + segment + "," + VideoUrl + "," + "questionnaire," + "SSCQE" + "," + points.ToString());

    }
}*/









}




