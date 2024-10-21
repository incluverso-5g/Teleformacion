using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoloader : MonoBehaviour
{
    public string videofile;
    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        string VideoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videofile);
        Debug.Log(VideoPath);
        videoPlayer.url = VideoPath;
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
