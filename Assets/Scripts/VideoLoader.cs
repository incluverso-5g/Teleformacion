using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Android;
using Oculus.Interaction;

public class VideoLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videofile;
    public HandMeshUI handmeshui;
    public int statusReportPeriod = 2;
    public GameObject InteractableObjectVR, HandMeshObject, InteractableObjectAR;
    public bool automaticStart = true;
    private GameObject InteractableObject;

    private ISbspController sbspController = null;

    private IEnumerator periodicUpdateCoroutine = null;

    private string moviesPath = "";

    protected bool started = false;
    protected bool enabled  = true;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            GetPermission(Permission.ExternalStorageRead);
#endif
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnEndReached;
        videoPlayer.errorReceived += OnError;
        SetMoviesPath();
        Debug.Log(moviesPath);
        videoPlayer.transform.GetComponent<MeshRenderer>().enabled = false;

        InteractableObject = InteractableObjectVR;
        InteractableObjectAR.SetActive(false); // The default mode is VR so Buttons in AR are disabled
        if (automaticStart)
            SetupInputPlaybin("video360", videofile);
    }

    // Update is called once per frame
    void Update()
    {
        //if(videoPlayer.isPrepared) videoPlayer.Play();
    }
    private static void GetPermission(string permission)
    {
        if (Permission.HasUserAuthorizedPermission(permission))
        {
            Debug.Log("Permission is already granted.");
            return;
        }

        Debug.LogFormat("Requesting permission to {0}.", permission);
        Permission.RequestUserPermission(permission);
    }

    private void SetMoviesPath() {
#if UNITY_ANDROID && !UNITY_EDITOR
        using var envClass = new UnityEngine.AndroidJavaClass("android.os.Environment");  
        using var moviesDir = envClass.CallStatic<UnityEngine.AndroidJavaObject>("getExternalStoragePublicDirectory", "Movies");       
        moviesPath = moviesDir.Call<string>("getAbsolutePath");        
        
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        moviesPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "Movies");  
        
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        moviesPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyVideos));

#else
        Debug.LogWarning("No Movies path found in the system. Using StreamingAssets instead")
        moviesPath = Application.streamingAssetsPath;  
#endif
    }
    public void SetupInputPlaybin(string format, string uri, ISbspController sbspController=null) {

        if(started) {
            Debug.LogWarning("SetupAndPlay called on started renderer. Ignore");
            return;
        }

        if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri uriResult) )
        {
            // If it is not an uri, we assume it is a file on moviesPath
            uri = System.IO.Path.Combine(moviesPath, uri);
        }
        Debug.Log($"Playing uri: {uri}");
        videoPlayer.url = uri;
        videoPlayer.Prepare();
        videoPlayer.Play();
        this.sbspController = sbspController;

        periodicUpdateCoroutine = PeriodicUpdate();
        StartCoroutine(periodicUpdateCoroutine);

        started = true;
        if(enabled)
            videoPlayer.transform.GetComponent<MeshRenderer>().enabled = true; 
    }

    public void StopAndClean() {
        if(periodicUpdateCoroutine != null)
            StopCoroutine(periodicUpdateCoroutine);
        periodicUpdateCoroutine = null;
        videoPlayer.Stop();
        started = false;
        if(enabled)
            videoPlayer.transform.GetComponent<MeshRenderer>().enabled = false; 
    }

    /*
    public void ChangeVideo(string uri)
    {
        videoPlayer.Stop();
        videoPlayer.url = moviesPath + "/" + uri;
        Debug.Log("Trying to load: " + videoPlayer.url);
        videoPlayer.Play();

    }*/

    public double GetPosition() {
        return videoPlayer.time;
    }

    public void setVideoEnable(bool setEnable)
    {
        if (setEnable) { 
            if(started)
                videoPlayer.Play();
            enabled = true;
            videoPlayer.transform.GetComponent<MeshRenderer>().enabled = true; 
        }
        else {
            if(started)
                videoPlayer.Pause();
            enabled = false;
            videoPlayer.transform.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public void SetLoop(bool loop)
    {
        videoPlayer.isLooping = loop;
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
        handmeshui.SetSliderValue(2, (Mathf.Min(handmeshui.GetSliderValue(2) + 0.1f, 0.5f)), false);
    }
    public void Left()
    {

        handmeshui.SetSliderValue(2, Mathf.Max(handmeshui.GetSliderValue(2) - 0.1f, -0.5f), false);
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
        foreach (var sphereCollider in HandMeshObject.GetComponentsInChildren<SphereCollider>())
        {
            sphereCollider.enabled = !enabled;
        }

    }
    public void VRMode()
    {
        InteractableObject.SetActive(false);
        
        handmeshui.SetSliderValue(0, -0.1f, false);
        handmeshui.SetSliderValue(1, 0.0f, false);
        handmeshui.SetSliderValue(2, 0.0f, false);
        handmeshui.SetSliderValue(3, 0.0f, false);
        handmeshui.SetSliderValue(4, 0.0f, false);
        transform.localScale = Vector3.one*36;
        InteractableObject = InteractableObjectVR;

    }
    public void ARMode(Vector3 position,Vector3 eulerangles)
    {
        InteractableObject.SetActive(false);
        
        handmeshui.SetSliderValue(0, 1.0f, false);
        transform.SetPositionAndRotation(position, Quaternion.Euler(eulerangles));
        transform.localScale = Vector3.one * 3.6f;
        InteractableObject = InteractableObjectAR;
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
                if(videoPlayer.isPaused) {
                    sbspController.UpdateStreamStatus("pause", false);
                }
                else {
                    sbspController.UpdateStreamStatus("playing", false);
                /*double seconds = videoPlayer.time;
                int minutes = (int)(seconds / 60);
                int remainingSeconds = (int)(seconds % 60);
                string pos =  $"{minutes:D2}:{remainingSeconds:D2}";
                sbspController.UpdateStreamStatus(pos, false);*/
                }
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
