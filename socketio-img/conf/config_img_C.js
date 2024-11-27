var test_name = "DCR 10s vs 20s (Roma3)";
var sessions = [
  {
    name: "Device 1 (10s->20s)",
    runs: [
      {name: "Training", tag: 'training', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_training_dcr.json'},
      {name: "DCR 10s", tag: 'C_dcr10_1st', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_10s_5x8_dcr.json'},
      {name: "DCR 20s", tag: 'C_dcr20_2nd', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_20s_5x8_dcr.json'}
    ]
  },
  {
    name: "Device 2 (20s->10s)",
    runs: [
      {name: "Training", tag: 'training', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_training_dcr.json'},
      {name: "DCR 20s", tag: 'C_dcr20_1st', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_20s_5x8_dcr.json'},
      {name: "DCR 10s", tag: 'C_dcr10_2nd', device: 'htc_vive_2', uri: 'http://localhost:3000/playlists/miro360_10s_5x8_dcr.json'}
    ]
  }
];
var devices = ['htc_vive_1', 'htc_vive_2'];
