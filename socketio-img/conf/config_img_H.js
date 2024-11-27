var test_name = "ACR 20s HTC Vive Audio vs Silent (RISE)";
var sessions = [
  {
    name: "Path 1 (Audio->Silent)",
    runs: [
      {name: "Training", tag: 'training', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "Tethered", tag: 'G_vive_audio_1st', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'},
      {name: "Untethered", tag: 'G_vive_silent_2nd', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_noaudio_acr.json'}
    ]
  },
  {
    name: "Path 2 (Silent->Audio)",
    runs: [
      {name: "Training", tag: 'training', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "Untethered", tag: 'G_vive_silent_1st', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_noaudio_acr.json'},
      {name: "Tethered", tag: 'G_vive_audio_2nd', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'}
    ]
  }
];
var devices = ['htc_vive_1', 'htc_vive_2'];
