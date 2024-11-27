var test_name = "ACR 20s HTC VivePro Tethered vs Untethered (Ghent)";
var sessions = [
  {
    name: "Path 1 (Tethered->Untethered)",
    runs: [
      {name: "Training (Tethered)", tag: 'training', device: 'htc_vive_pro', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "Tethered", tag: 'G_vivepro_tethered_1st', device: 'htc_vive_pro', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'},
      {name: "Untethered", tag: 'G_vivepro_untethered_2nd', device: 'htc_vive_pro_1', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'}
    ]
  },
  {
    name: "Path 2 (Untethered->Tethered)",
    runs: [
      {name: "Training (Untethered)", tag: 'training', device: 'htc_vive_pro_untethered', uri: 'http://localhost:3000/playlists/miro360_training_acr.json'},
      {name: "Untethered", tag: 'G_vivepro_untethered_1st', device: 'htc_vive_pro_untethered', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'},
      {name: "Tethered", tag: 'G_vivepro_tethered_2nd', device: 'htc_vive_pro', uri: 'http://localhost:3000/playlists/miro360_20s_8x6_acr.json'}
    ]
  }
];
var devices = ['htc_vive_pro', 'htc_vive_pro_untethered'];
