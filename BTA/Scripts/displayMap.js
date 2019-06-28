var platform = new H.service.Platform({
    app_id: 'BSAfVlleJC9nUT6TISDl',
    app_code: 'HwwUz06BMMZj77AnGZugrA'
});

var pixelRatio = window.devicePixelRatio || 1;

var defaultLayers = platform.createDefaultLayers({
    tileSize: pixelRatio === 1 ? 256 : 512,
    ppi: pixelRatio === 1 ? undefined : 320
  });

  var map = new H.Map(document.getElementById('map'),
  defaultLayers.normal.map, {pixelRatio: pixelRatio});

  var behavior = new H.mapevents.Behavior(new H.mapevents.MapEvents(map));

  var ui = H.ui.UI.createDefault(map, defaultLayers);

  function moveMapToLocation(latitude, longitude){
      
      map.setCenter({lat:latitude, lng:longitude});
      map.setZoom(13);
  }





