using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    VideoPlayer videoPlayer;
    public string videofile;

    public int statusReportPeriod = 2;

    public bool automaticStart = true;

    private ISbspController sbspController = null;

    private IEnumerator periodicUpdateCoroutine = null;

    protected bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        string VideoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videofile);
        videoPlayer.loopPointReached += OnEndReached;
        videoPlayer.errorReceived += OnError;
        Debug.Log(VideoPath);
        if (automaticStart)
            SetupInputPlaybin("video360", VideoPath);
    }

    // Update is called once per frame
    void Update()
    {
        //if(videoPlayer.isPrepared) videoPlayer.Play();
    }

    public void SetupInputPlaybin(string format, string uri, ISbspController sbspController=null) {

        if(started) {
            Debug.LogWarning("SetupAndPlay called on started renderer. Ignore");
            return;
        }

        if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri uriResult) )
        {
            // If it is not an uri, we assume it is a file on StreamingAssets
            uri = System.IO.Path.Combine(Application.streamingAssetsPath, uri);
        }
        Debug.Log($"Playing uri: {uri}");
        videoPlayer.url = uri;
        videoPlayer.Prepare();
        videoPlayer.Play();
        this.sbspController = sbspController;

        periodicUpdateCoroutine = PeriodicUpdate();
        StartCoroutine(periodicUpdateCoroutine);

        started = true;

    }

    public void StopAndClean() {
        if(periodicUpdateCoroutine != null)
            StopCoroutine(periodicUpdateCoroutine);
        periodicUpdateCoroutine = null;
        videoPlayer.Stop();
        started = false;
    }

    public void SetRotation(float angle) {

    }

    public void SetVolume(double volume) {
    }


    private IEnumerator PeriodicUpdate()
    {
        while(sbspController != null && statusReportPeriod > 0)
        {
            if (started) {
                double seconds = videoPlayer.time;
                int minutes = (int)(seconds / 60);
                int remainingSeconds = (int)(seconds % 60);
                string pos =  $"{minutes:D2}:{remainingSeconds:D2}";
                sbspController.UpdateStreamStatus(pos, false);
            }
            else {
                sbspController.UpdateStreamStatus("stopped", false);
            }
            yield return new WaitForSeconds(statusReportPeriod);
        }
        yield return null;
    }

    void OnEndReached(VideoPlayer vp)
    {
        if(sbspController != null)
            sbspController.UpdateStreamStatus("finished", true);
    }

    void OnError(VideoPlayer vp, string message)
    {
        if(sbspController != null)
            sbspController.UpdateStreamStatus(message, true);
    }

    

}
