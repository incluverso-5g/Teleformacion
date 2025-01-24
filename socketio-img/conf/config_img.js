var test_name = "20 vs 30 (AGH)";
var sessions = [
  {
    name: "VideoPlayer Control",
    runs: [
      {name: "play", tag: 'CARLOS', device: 'CARLOS', uri: 'play'},
      {name: "pause", tag: 'CARLOS', device: 'CARLOS', uri: 'pause'},
	  {name: "+10 s", tag: 'CARLOS', device: 'CARLOS', uri: '10'},
	  {name: "-10 s", tag: 'CARLOS', device: 'CARLOS', uri: '-10'},
      {name: "volumeup", tag: 'CARLOS', device: 'CARLOS', uri: 'volumeup'},
	  {name: "volumedown", tag: 'CARLOS', device: 'CARLOS', uri: 'volumedown'},
	  {name: "speedup", tag: 'CARLOS', device: 'CARLOS', uri: 'speedup'},
	  {name: "speedown", tag: 'CARLOS', device: 'CARLOS', uri: 'speeddown'},
	  {name: "resetspeed", tag: 'CARLOS', device: 'CARLOS', uri: 'resSpeed'},
	  {name: "toogleUI", tag: 'CARLOS', device: 'CARLOS', uri: 'toogleUI'},
	  {name: "Enable Video", tag: 'CARLOS', device: 'CARLOS', uri: 'enableVideo'},
	  {name: "Disable Video", tag: 'CARLOS', device: 'CARLOS', uri: 'disableVideo'}
	  
	  

	  
    ]
  },
  {
    name: "Sphere Control",
    runs: [
	  {name: "Píldora1", tag: 'CARLOS2', device: 'CARLOS', uri: ';FJ23_COCINA_INMERSIVA_1.mp4;'},
	  {name: "Píldora2", tag: 'CARLOS2', device: 'CARLOS', uri: ';FJ23_COCINA_INMERSIVA_2.mp4;'},
	  {name: "Píldora3", tag: 'CARLOS2', device: 'CARLOS', uri: ';FJ23_COCINA_INMERSIVA_3.mp4;'},
	  {name: "Píldora4 (Peso)", tag: 'CARLOS2', device: 'CARLOS', uri: ';FJ23_COCINA_INMERSIVA_05_PESO.mp4;'},
	  {name: "Píldora6 (Horno sin fotos)", tag: 'CARLOS2', device: 'CARLOS', uri: ';FJ23_COCINA_INMERSIVA_06_HORNO.mp4;'},
	  {name: "Píldora6 (Horno con fotos)", tag: 'CARLOS2', device: 'CARLOS', uri: ';Secuencia_06_HORNO_CON_FOTOS.mp4;'},
	  {name: "Píldora7 (Cucharadas)", tag: 'CARLOS2', device: 'CARLOS', uri: ';Secuencia_07_CUCHARADAS.mp4;'},	  
	  {name: "Píldora6 (Horno sin fotos)", tag: 'CARLOS2', device: 'CARLOS', uri: ';FJ23_COCINA_INMERSIVA_06_HORNO.mp4;'},
	  {name: "Píldora6 (Horno con fotos)", tag: 'CARLOS2', device: 'CARLOS', uri: ';Secuencia_06_HORNO_CON_FOTOS.mp4;'},
	  {name: "Píldora7 (Cucharadas)", tag: 'CARLOS2', device: 'CARLOS', uri: ';Secuencia_07_CUCHARADAS.mp4;'},
	  	  {name: "Enable Video", tag: 'CARLOS', device: 'CARLOS', uri: 'enableVideo'},
	  {name: "Disable Video", tag: 'CARLOS', device: 'CARLOS', uri: 'disableVideo'}
	  
    ]
  },
  {
    name: "Video Selection",
    runs: [
	  {name: "SpherePercentajeUp", tag: 'CARLOS', device: 'CARLOS', uri: 'SphereIncr'},
	  {name: "SpherePercentajeDown", tag: 'CARLOS', device: 'CARLOS', uri: 'SphereDecr'},
	  {name: "Left", tag: 'CARLOS', device: 'CARLOS', uri: 'left'},
	  {name: "Right", tag: 'CARLOS', device: 'CARLOS', uri: 'right'},
	  {name: "Far", tag: 'CARLOS', device: 'CARLOS', uri: 'far'},
	  {name: "Near", tag: 'CARLOS', device: 'CARLOS', uri: 'near'},
	  {name: "Up", tag: 'CARLOS', device: 'CARLOS', uri: 'above'},
	  {name: "Down", tag: 'CARLOS', device: 'CARLOS', uri: 'under'},
	  {name: "Reset", tag: 'CARLOS', device: 'CARLOS', uri: 'reset'},
	  {name: "toogleButtons", tag: 'CARLOS3', device: 'CARLOS', uri: 'toogleButtons'},
	  	  {name: "Enable Video", tag: 'CARLOS', device: 'CARLOS', uri: 'enableVideo'},
	  {name: "Disable Video", tag: 'CARLOS', device: 'CARLOS', uri: 'disableVideo'}
	  
    ]
  }
  
];
var devices = ['CARLOS', 'CARLOS','CARLOS3'];
