var test_name = "ACR 20s GearVR vs VivePro (Nokia)";
var sessions = [
  {
    name: "Path 1 (GearVR->VivePro)",
    runs: [
      {name: "Training (GearVR)", tag: 'training', device: 'gearvr_1', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "GearVR", tag: 'E_gearvr_1st', device: 'gearvr_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'},
      {name: "VivePro", tag: 'E_vivepro_2nd', device: 'htc_vive_pro_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'}
    ]
  },
  {
    name: "Path 2 (VivePro->GearVR)",
    runs: [
      {name: "Training (VivePro)", tag: 'training', device: 'htc_vive_pro_1', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "VivePro", tag: 'E_vivepro_1st', device: 'htc_vive_pro_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'},
      {name: "GearVR", tag: 'E_gearvr_2nd', device: 'gearvr_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'}
    ]
  }
];
var devices = ['gearvr_1', 'htc_vive_pro_1'];
