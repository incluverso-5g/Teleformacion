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
        VideoPlayer player = GetComponent<VideoPlayer>();
        player.url= Application.streamingAssetsPath+ videofile;
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
