var test_name = "DCR 20s vs 30s (CWI)";
var sessions = [
  {
    name: "Device 1 (20s->30s)",
    runs: [
      {name: "Training", tag: 'training', device: 'oculus_rift_1', uri: 'http://localhost:3000/playlists/miro360_training_dcr.json'},
      {name: "DCR 20s", tag: 'D_dcr20_1st', device: 'oculus_rift_1', uri: 'http://localhost:3000/playlists/miro360_20s_5x6_dcr.json'},
      {name: "DCR 30s", tag: 'D_dcr30_2nd', device: 'oculus_rift_1', uri: 'http://localhost:3000/playlists/miro360_30s_5x6_dcr.json'}
    ]
  },
  {
    name: "Device 2 (30s->20s)",
    runs: [
      {name: "Training", tag: 'training', device: 'oculus_rift_2', uri: 'http://localhost:3000/playlists/miro360_training_dcr.json'},
      {name: "DCR 30s", tag: 'D_dcr30_1st', device: 'oculus_rift_2', uri: 'http://localhost:3000/playlists/miro360_30s_5x6_dcr.json'},
      {name: "DCR 20s", tag: 'D_dcr20_2nd', device: 'oculus_rift_2', uri: 'http://localhost:3000/playlists/miro360_20s_5x6_dcr.json'}
    ]
  }
];
var devices = ['oculus_rift_1', 'oculus_rift_2'];
