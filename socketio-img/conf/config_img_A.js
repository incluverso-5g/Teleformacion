var test_name = "ACR 10s vs 20s (Wuhan University)";
var sessions = [
  {
    name: "Device 1 (10s->20s)",
    runs: [
      {name: "Training", tag: 'training', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "ACR 10s", tag: 'A_acr10_1st', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_10s_8x8_acr.json'},
      {name: "ACR 20s", tag: 'A_acr20_2nd', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x8_acr.json'},
	        {name: "Training", tag: 'training', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "ACR 20s", tag: 'A_acr20_1st', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_20s_8x8_acr.json'},
      {name: "ACR 10s", tag: 'A_acr10_2nd', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_10s_8x8_acr.json'}
    ]
  },
  {
    name: "Device 2 (20s->10s)",
    runs: [
      {name: "Training", tag: 'training', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "ACR 20s", tag: 'A_acr20_1st', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_20s_8x8_acr.json'},
      {name: "ACR 10s", tag: 'A_acr10_2nd', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_10s_8x8_acr.json'},      
	  {name: "Training", tag: 'training', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "ACR 20s", tag: 'A_acr20_1st', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_20s_8x8_acr.json'},
      {name: "ACR 10s", tag: 'A_acr10_2nd', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_10s_8x8_acr.json'}
    ]
  }
];
var devices = ['htc_vive_1', 'htc_vive_2'];
