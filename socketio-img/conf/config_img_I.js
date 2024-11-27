var test_name = "ACR 20s Miro scoring vs Voice (TU Ilmenau)";
var sessions = [
  {
    name: "Path 1 (Controller->Voice)",
    runs: [
      {name: "Training", tag: 'training', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "Controller", tag: 'G_vive_controller_1st', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'},
      {name: "Voice", tag: 'G_vive_voice_2nd', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_timeout_acr.json'}
    ]
  },
  {
    name: "Path 2 (Voice->Controller)",
    runs: [
      {name: "Training", tag: 'training', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "Voice", tag: 'G_vive_voice_1st', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_timeout_acr.json'},
      {name: "Controller", tag: 'G_vive_controller_2nd', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'}
    ]
  }
];
var devices = ['htc_vive_1', 'htc_vive_2'];
