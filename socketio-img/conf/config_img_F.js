var test_name = "ACR 20s HTC Vive vs VivePro (UPM)";
var sessions = [
  {
    name: "Path 1 (Vive->VivePro)",
    runs: [
      {name: "Training (Vive)", tag: 'training', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "Vive", tag: 'E_htcvive_1st', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'},
      {name: "VivePro", tag: 'E_vivepro_2nd', device: 'htc_vive_pro_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'}
    ]
  },
  {
    name: "Path 2 (VivePro->Vive)",
    runs: [
      {name: "Training (VivePro)", tag: 'training', device: 'htc_vive_pro_1', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "VivePro", tag: 'E_vivepro_1st', device: 'htc_vive_pro_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'},
      {name: "Vive", tag: 'E_htcvive_2nd', device: 'htc_vive_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'}
    ]
  }
];
var devices = ['htc_vive_1', 'htc_vive_pro_1'];
