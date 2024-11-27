var test_name = "20 vs 30 (AGH)";
var sessions = [
  {
    name: "VideoPlayer Control",
    runs: [
      {name: "play", tag: 'PABLO', device: 'PABLO', uri: 'play'},
      {name: "pause", tag: 'PABLO', device: 'PABLO', uri: 'pause'},
	  {name: "+10 s", tag: 'PABLO', device: 'PABLO', uri: '10'},
	  {name: "-10 s", tag: 'PABLO', device: 'PABLO', uri: '-10'},
      {name: "volumeup", tag: 'PABLO', device: 'PABLO', uri: 'volumeup'},
	  {name: "volumedown", tag: 'PABLO', device: 'PABLO', uri: 'volumedown'},
	  {name: "speedup", tag: 'PABLO', device: 'PABLO', uri: 'speedup'},
	  {name: "speedown", tag: 'PABLO', device: 'PABLO', uri: 'speeddown'},
	  {name: "resetspeed", tag: 'PABLO', device: 'PABLO', uri: 'resSpeed'},
	  {name: "toogleUI", tag: 'PABLO', device: 'PABLO', uri: 'toogleUI'}

	  
    ]
  },
  {
    name: "Sphere Control",
    runs: [
	  {name: "SpherePercentajeUp", tag: 'PABLO2', device: 'PABLO', uri: 'SphereIncr'},
	  {name: "SpherePercentajeDown", tag: 'PABLO2', device: 'PABLO', uri: 'SphereDecr'},
	  {name: "Left", tag: 'PABLO', device: 'PABLO', uri: 'left'},
	  {name: "Right", tag: 'PABLO', device: 'PABLO', uri: 'right'},
	  {name: "Far", tag: 'PABLO', device: 'PABLO', uri: 'far'},
	  {name: "Near", tag: 'PABLO', device: 'PABLO', uri: 'near'},
	  {name: "Up", tag: 'PABLO', device: 'PABLO', uri: 'above'},
	  {name: "Down", tag: 'PABLO', device: 'PABLO', uri: 'under'},
	  {name: "Reset", tag: 'PABLO', device: 'PABLO', uri: 'reset'},
	  {name: "toogleButtons", tag: 'PABLO', device: 'PABLO', uri: 'toogleButtons'}
	  
    ]
  }
  
];
var devices = ['PABLO', 'PABLO'];
