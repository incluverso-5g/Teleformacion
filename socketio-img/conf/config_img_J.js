var test_name = "ACR 10s vs 30s (AGH)";
var sessions = [
  {
    name: "Device 1 (10s->30s)",
    runs: [
      {name: "Training", tag: 'training', device: 'oculus_rift_1', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "ACR 10s", tag: 'J_acr10_1st', device: 'oculus_rift_1', uri: 'http://localhost:3000/playlists/miro360_10s_8x6_acr.json'},
      {name: "ACR 30s", tag: 'J_acr30_2nd', device: 'oculus_rift_1', uri: 'http://localhost:3000/playlists/miro360_30s_8x6_acr.json'}
    ]
  },
  {
    name: "Device 2 (30s->10s)",
    runs: [
      {name: "Training", tag: 'training', device: 'oculus_rift_2', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "ACR 30s", tag: 'J_acr30_1st', device: 'oculus_rift_2', uri: 'http://localhost:3000/playlists/miro360_30s_8x6_acr.json'},
      {name: "ACR 10s", tag: 'J_acr10_2nd', device: 'oculus_rift_2', uri: 'http://localhost:3000/playlists/miro360_10s_8x6_acr.json'}
    ]
  }
];
var devices = ['oculus_rift_1', 'oculus_rift_2'];
