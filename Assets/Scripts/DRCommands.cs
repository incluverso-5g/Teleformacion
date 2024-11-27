using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SocketIOClient;
using SocketIOClient.Transport;
using SocketIO.Core;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnityEngine.Video;
using Unity.VisualScripting;

public class DRCommand
{
    [JsonPropertyName("remote_uri")]
    public string Uri { get; set; } = null;

    [JsonPropertyName("video_format")]
    public string VideoFormat { get; set; } = "video360";

    [JsonPropertyName("angle")]
    public string Angle { get; set; } = "0";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("audio_volume")]
    public string AudioVolume { get; set; } = "";

    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = "";

    [JsonPropertyName("playlist")]
    public string Playlist { get; set; } = "";

}

public class DRCommands : MonoBehaviour, ISbspController
{
    private SocketIOClient.SocketIO client; // Fully qualify the type to avoid namespace conflict
    private float volume;
    private const string NO_REMOTE_URI = "__empty__";
    public string ini_file = "player.ini";
    //public HeadsetTracker tracker;
    public VideoLoader player;
    public VideoPlayer playerPlayer;
    //public GameObject screen;
    
    // They will load from config file, so they are private now
    string device = "PABLO";
    string socketio_uri = "http://127.0.0.1:3000"; //"ws://127.0.0.1:3000/socket.io/?EIO=4&transport=websocket";
    string socketio_eio = "3";

    string influxdb_uri = "udp://127.0.0.1:8089";
    string influxdb_local_dir = "";


    string uri = NO_REMOTE_URI; //It was "", but I don't understand why (Pablo - sep23)
    string video_format = "";
    int content_angle = 0;
    string video_status = "-";
    double audioVolume = 1.0F;
    string title = "";
    string user_id = "X";
    string playlist = "X";


    bool loadNewContent = false;
    bool playContent = false;
    bool pauseContent = false;
    bool reset=false;
    bool near=false;
    bool far=false; 
    bool right=false;
    bool left=false; 
    bool sphereDecrease=false; 
    bool sphereIcrease=false; 
    bool speedDown=false; 
    bool speedUp=false; 
    bool VolumeDown=false; 
    bool volumeUp=false;
    bool up = false;
    bool down = false;
    bool minusten=false;
    bool plusten=false;
    bool resetSpeed=false;

    private bool isLoading = false;

    bool isConnected = false; // Flag to track connection status




    static DRCommands Instance;

    public string GetUri()
    {
        return uri;
    }

    public string GetDevice()
    {
        return device;
    }

    public double GetAudioVolume()
    {
        return audioVolume;
    }

    public string GetVideoFormat()
    {
        return video_format;
    }

    public int GetAngle()
    {
        return content_angle;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetUserID()
    {
        return user_id;
    }

    public string GetPlaylist()
    {
        return playlist;
    }

    private void ReadConfigFile()
    {
        INIParser ini = new INIParser();
        try
        {
            Debug.Log(String.Format("Reading configuration file: " +ini_file + " On path: " + Application.persistentDataPath));
            ini.Open(Application.persistentDataPath + "/" + ini_file);
            device = ini.ReadValue("SocketIO", "device", device);
            socketio_uri = ini.ReadValue("SocketIO", "uri", socketio_uri);
            socketio_eio = ini.ReadValue("SocketIO", "EIO", socketio_eio);
            Debug.Log("using server uri: '" + socketio_uri+"' device: '" + device + "'");
            influxdb_uri = ini.ReadValue("InfluxDB", "uri", influxdb_uri);
            influxdb_local_dir = ini.ReadValue("InfluxDB", "local_dir", influxdb_local_dir);
            Debug.Log("InfluxDB uri: " + influxdb_uri + " local_dir: " + influxdb_local_dir);

        }
        catch (Exception e)
        {
            Debug.Log(String.Format("ReadConfigFile exception: " + e.Message));
        }
        finally
        {
            ini.Close();
        }
    }

    private void Awake()
    {
        if(!this.enabled) {
            Debug.Log("If not enabled from the beginning, this object does not work");
            return;
        }
        // Make object singleton
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Protect from being destroyed when switching scenes

        DontDestroyOnLoad(this.gameObject);

        ReadConfigFile();  
        
        //if(tracker != null)
        //    tracker.Configure(influxdb_uri, influxdb_local_dir);

        SocketIOOptions options = new SocketIOOptions();
        if(socketio_eio == "3")
            options.EIO = EngineIO.V3;
        else if(socketio_eio == "4")
            options.EIO = EngineIO.V4;
        options.Transport = TransportProtocol.WebSocket;
        client = new SocketIOClient.SocketIO(socketio_uri, options);
        Debug.Log("using uri " + socketio_uri);

        // Setup event handlers
        client.OnConnected += async (sender, e) =>
        {
            Debug.Log("Connected to server");
            isConnected = true;
            await Join(device);
            ReportStatus("client is ready");
        };

        client.On("dr_command", OnDrCommand);

        //Debug.Log("Disabling automatic start from video player!!");
        //player.automaticStart = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(String.Format("Starting VR Player"));

        // Connect to the server
        //client.ConnectAsync().GetAwaiter().GetResult(); // FIXME -- this should be handled separately, as it may take LONG""
        //Task.Run(() => client.ConnectAsync().GetAwaiter().GetResult());
        Task.Run(Connect);
        //player.gameObject.SetActive(false);
        
    }

    public async Task Connect() {
        await client.ConnectAsync();
        Debug.Log($"Client connected to {socketio_uri}");
    }

    

    public async Task Join(string device)
    {
        Debug.Log("Joining group: " + device);
        var data = new Dictionary<string, string>
        {
            { "group", device }
        };
        await client.EmitAsync("join", data);
    }


    // Update is called once per frame
    void Update()
    {
        if (loadNewContent && !isLoading)
        {
            loadNewContent = false;
            //StartCoroutine(LoadNewContentLocal());
            LoadNewContentImmediate();
        }
        if(pauseContent)
        {
            player.PausePlayer();
            pauseContent = false;
        }
        if(playContent)
        {
            player.playPlayer();
            playContent = false;
        }
        if (volumeUp)
        {
            player.VolumeUp();
            volumeUp = false;
        }
        if (VolumeDown)
        {
            player.VolumeDown();
            VolumeDown = false;
        }
        if (speedUp)
        {
            player.SpeedUp();
            speedUp = false;
        }
        if (speedDown)
        {
            player.SpeedDown();
            speedDown = false;
        }
        if (sphereIcrease)
        {
            player.SphereIncrease();
            sphereIcrease = false;
        }
        if (sphereDecrease)
        {   
            player.SphereDecrease();
            sphereDecrease = false;
        }
        if (left)
        {
            player.Left();
            left = false;
        }
        if (right)
        {
            player.Right();
            right = false;
        }
        if (far)
        {
            player.Further();
            far = false;
        }
        if (near)
        {
            player.Closer();
            near = false;
        }
        if (up)
        {
            player.Upper();
            up = false;
        }
        if (down)
        {
            player.Below();
            down = false;
        }
        if (plusten)
        {
            player.plusTenSeconds();
            plusten = false;
        }
        if (minusten)
        {
            player.minusTenSeconds();
            minusten = false;
        }
        if (reset)
        {
            player.SphereReset();
            reset = false;
        }
        if (resetSpeed)
        {
            player.SpeedReset();
            resetSpeed = false;
        }

    }

    public void OnDrCommand(SocketIOResponse response)
    {
        //volume = player.videoPlayer.GetDirectAudioVolume(0);
        Debug.Log($"OnDrCommand: {response}");
        string check = response.ToString();
        if (response.ToString().Contains("pause")) {
            Debug.Log("TryingtoPause");
            pauseContent = true;
            ReportStatus("pause");
        }
        if (response.ToString().Contains("play")) { 
            
            Debug.Log("TryingtoPlay");
            playContent = true;
            ReportStatus("play");
        }
        //if (response.ToString().Contains("volumedown")) GetComponent<VideoPlayer>().SetDirectAudioVolume(0, Mathf.Min(1, volume * 1.1f));

        if (response.ToString().Contains("10")) {plusten= true; }
        if (response.ToString().Contains("-10")) {minusten= true; }
        if (response.ToString().Contains("speedup")) {speedUp = true; }
        if (response.ToString().Contains("speeddown")) { speedDown= true; }
        if (response.ToString().Contains("SphereIncr")) {sphereIcrease = true; }
        if (response.ToString().Contains("SphereDecr")) {sphereDecrease = true; }
        if (response.ToString().Contains("left")) {left = true; }
        if (response.ToString().Contains("right")) { right= true; }
        if (response.ToString().Contains("far")) {far = true; }
        if (response.ToString().Contains("near")) {near = true; }
        if(response.ToString().Contains("above")) { up = true; }
        if (response.ToString().Contains("under")) { down = true; }
        if (response.ToString().Contains("reset")) { reset= true; }
        if (response.ToString().Contains("resSpeed")) { resetSpeed = true; }
        if (response.ToString().Contains("volumeup")) { volumeUp = true; }
        if (response.ToString().Contains("volumedown")) { VolumeDown = true; }
        try
        {
            DRCommand command =  response.GetValue<DRCommand>();

             // Use the deserialized data

            if(command.Uri == null) {
                Debug.Log("dr_command: check");
                ReportStatus("status check");
                return;
            }
            uri = command.Uri;
            video_format = command.VideoFormat;
            content_angle = int.Parse(command.Angle);
            title = command.Name;
            user_id = command.UserId;
            playlist = command.Playlist;

            if (command.AudioVolume != "") {
                audioVolume = Convert.ToInt32(command.AudioVolume) / 100F;
                if (audioVolume > 1.0F || audioVolume < 0) {
                    Debug.Log(String.Format("recevied bad audio volume '" + command.AudioVolume + "'"));
                    audioVolume = 1.0F;
                }
                else
                {
                    Debug.Log(String.Format("Setting audio volume '" + command.AudioVolume + "'"));
                }
            }

            Debug.Log(String.Format("dr_command: {0} -- {1} -- {2} -- {3} -- {4}", uri, video_format, title, playlist, audioVolume));

            loadNewContent = true;
            ReportStatus("ok");

        }
        catch (Exception ex)
        {
            Debug.LogError($"Error deserializing CmdMessage: {ex.Message}");
        }

    }

    void LoadNewContentImmediate() 
    {
        player.StopAndClean();
        video_status = "-";
        if (!string.IsNullOrEmpty(uri) && !uri.Equals(NO_REMOTE_URI))
        {
            Debug.Log("Starting content: " + uri);
            player.SetupInputPlaybin(video_format, uri, this);
            player.SetRotation(content_angle);
            //player.SetVolume(audioVolume);
        }
        else {
            Debug.Log("No content to start: " + uri);
        }

    }

    IEnumerator LoadNewContentLocal()
    {
        isLoading = true;

        Debug.Log("Showing menu");
        player.StopAndClean();
        //player.gameObject.SetActive(false);
        //screen.SetActive(true);
        //if(tracker != null)
        //    tracker.VideoStopped();
        yield return new WaitForSeconds(3);

        video_status = "-";

        // Should we ensure that we do not have to stop it again?

        Debug.Log("Starting content");

        if (!string.IsNullOrEmpty(uri) && !uri.Equals(NO_REMOTE_URI))
        {
            Debug.Log("Starting content: " + uri);
            //player.gameObject.SetActive(true);
            player.SetupInputPlaybin(video_format, uri, this);
            player.SetRotation(content_angle);
            //player.SetVolume(audioVolume);
            
            //screen.SetActive(false); // This is a hack! Really!
            //if(tracker != null)
            //    tracker.VideoStarted(device, title, user_id, playlist);
        }
        else {
            Debug.Log("No content to start: " + uri);
        }

        isLoading = false;

    } 

    public async void ReportStatus(string message)
    {
        var data = new Dictionary<string, object>
        {
            { "group", device },
            { "time", DateTime.Now.ToString("h:mm:ss tt") },
            { "local_mix", 0.0f },
            { "remote_uri", uri },
            { "video_status", video_status },
            { "cell_info", "N/A" },
            { "message", message }
        };
        Debug.Log(String.Format("dr_status: {0} -- {1} -- {2} -- {3}", device, uri, video_status, message));
        if(isConnected) // We may miss first OnApplicationPause call on application start -- let's ignore it
            await client.EmitAsync("dr_status", data); 
        else
            Debug.LogWarning("Missed dr_status notification to server");
    }

    public void UpdateVideoStatus(string status, bool finished)
    {
        UpdateStreamStatus(status, finished);
    }

    
    public void UpdateStreamStatus(string status, bool finished)
    {
        Debug.Log("status (" + status + ")  finished: " + finished);
        video_status = status;
        if(finished)
        {
            uri = NO_REMOTE_URI;
            video_format = "";
            title = "";
            loadNewContent = true;
        }
        ReportStatus("status update");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application closed");
        ReportStatus("Application closed");
    }

     public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("App pause");
            ReportStatus("App pause");
        }
        else {
            Debug.Log("App resume");
            ReportStatus("App resume");
        }
    }


    private async void TerminateSocket()
    {
        // Disconnect and dispose of the socket
        await client.DisconnectAsync();
        client.Dispose();
        client = null;
        Debug.Log($"Client disconnected from {socketio_uri}");
    }

    void OnDestroy() {
        if(client != null) {
            TerminateSocket();
        }
    }

}
