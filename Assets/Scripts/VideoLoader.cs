using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videofile;
    public HandMeshUI handmeshui;
    public int statusReportPeriod = 2;
    public GameObject InteractableObject, HandMeshObject;
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
        //videoPlayer.Prepare();
        //videoPlayer.Play();
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
    public void SpeedUp()
    {
        videoPlayer.playbackSpeed = (Mathf.Min(videoPlayer.playbackSpeed + 0.1f, 2.0f));
    }
    public void SpeedDown()
    {
        videoPlayer.playbackSpeed = (Mathf.Max(videoPlayer.playbackSpeed - 0.1f, 0.1f));
    }
    public void SpeedReset()
    {
        videoPlayer.playbackSpeed = 1.0f;
    }
    public void VolumeUp() {
        videoPlayer.SetDirectAudioVolume(0,(Mathf.Min(videoPlayer.GetDirectAudioVolume(0) + 0.1f,1.0f)));
    }
    public void VolumeDown()
    {
        videoPlayer.SetDirectAudioVolume(0, (Mathf.Max(videoPlayer.GetDirectAudioVolume(0) - 0.1f,0.0f)));
    }
    public void PausePlayer()
    {
        videoPlayer.Pause();
    }
    public void playPlayer()
    {
        videoPlayer.Play();
    }
    public void SphereIncrease()
    {
        handmeshui.SetSliderValue(0, (Mathf.Min(handmeshui.GetSliderValue(0) + 0.1f, 1f)), false);
    }
    public void SphereDecrease()
    {

        handmeshui.SetSliderValue(0, Mathf.Max(handmeshui.GetSliderValue(0) - 0.1f, 0.01f), false);
    }
    public void Further()
    {
        handmeshui.SetSliderValue(1, (Mathf.Min(handmeshui.GetSliderValue(1) + 0.1f, 1f)), false);
    }
    public void Closer()
    {

        handmeshui.SetSliderValue(1, Mathf.Max(handmeshui.GetSliderValue(1) - 0.1f, 0.01f), false);
    }

    public void Right()
    {
        handmeshui.SetSliderValue(2, (Mathf.Min(handmeshui.GetSliderValue(2) + 0.1f, 1f)), false);
    }
    public void Left()
    {

        handmeshui.SetSliderValue(2, Mathf.Max(handmeshui.GetSliderValue(2) - 0.1f, 0.01f), false);
    }
    public void Upper()
    {
        handmeshui.SetSliderValue(3, (Mathf.Min(handmeshui.GetSliderValue(3) + 0.1f, 0.5f)), false);
    }
    public void Below()
    {

        handmeshui.SetSliderValue(3, Mathf.Max(handmeshui.GetSliderValue(3) - 0.1f, -0.5f), false);
    }
    public void SphereReset()
    {
        handmeshui.SetSliderValue(4, 1.0f, false);

    }
    public void toogleButtons()
    {
        InteractableObject.SetActive(!InteractableObject.activeSelf);
    }
    public void toogleUI()
    {
        bool enabled = HandMeshObject.GetComponentInChildren<MeshRenderer>().enabled;
        
            foreach (var meshRenderer in HandMeshObject.GetComponentsInChildren<MeshRenderer>())
            {
            meshRenderer.enabled = !enabled;
            }
        
    }
    public void ChangeVideo(string uri)
    {
        
        string newPath=System.IO.Path.Combine("file://sdcard/Movies", uri);
        Debug.Log("Trying to load: " + newPath);
        videoPlayer.Stop();
        
        videoPlayer.url = newPath;
        videoPlayer.Play();

    }

    public void plusTenSeconds() 
    {
        videoPlayer.Pause();
        while (videoPlayer.isPlaying) { }
        videoPlayer.frame = videoPlayer.frame + Mathf.FloorToInt(videoPlayer.frameRate * 10);
        videoPlayer.Prepare();
        videoPlayer.Play();
    }
    public void minusTenSeconds()
    {
        videoPlayer.Pause();
        while (videoPlayer.isPlaying) { }
        videoPlayer.frame = videoPlayer.frame - Mathf.FloorToInt(videoPlayer.frameRate * 10);
        videoPlayer.Prepare();
        videoPlayer.Play();
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
