var test_name = "ACR 20s vs 30s (U. Surrey)";
var sessions = [
  {
    name: "Device 1 (20s->30s)",
    runs: [
      {name: "Training", tag: 'training', device: 'oculus_rift_1', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "ACR 20s", tag: 'B_acr20_1st', device: 'oculus_rift_1', uri: 'http://localhost:3000/playlists/miro360_20s_5x8_acr.json'},
      {name: "ACR 30s", tag: 'B_acr30_2nd', device: 'oculus_rift_1', uri: 'http://localhost:3000/playlists/miro360_30s_5x8_acr.json'}
    ]
  },
  {
    name: "Device 2 (30s->20s)",
    runs: [
      {name: "Training", tag: 'training', device: 'oculus_rift_2', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "ACR 30s", tag: 'B_acr30_1st', device: 'oculus_rift_2', uri: 'http://localhost:3000/playlists/miro360_30s_5x8_acr.json'},
      {name: "ACR 20s", tag: 'B_acr20_2nd', device: 'oculus_rift_2', uri: 'http://localhost:3000/playlists/miro360_20s_5x8_acr.json'}
    ]
  }
];
var devices = ['oculus_rift_1', 'oculus_rift_2'];
