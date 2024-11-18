using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoloader : MonoBehaviour
{
    VideoPlayer videoPlayer;
    public string videofile;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        string VideoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videofile);
        Debug.Log(VideoPath);
        videoPlayer.url = VideoPath;
        videoPlayer.Prepare();
        //videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //if(videoPlayer.isPrepared) videoPlayer.Play();
    }
}
