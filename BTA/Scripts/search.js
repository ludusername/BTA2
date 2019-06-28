var AUTOCOMPLETION_URL = 'https://geocoder.api.here.com/6.2/geocode.json',
    ajaxRequest = new XMLHttpRequest(),
    query = '';
function autoCompleteListener(textBox, event) {

    if (query != textBox.value) {
        if (textBox.value.length >= 1) {
            var params = '?' +
                'query=' + encodeURIComponent(textBox.value) +   // The search text which is the basis of the query
                '&beginHighlight=' + encodeURIComponent('<mark>') + //  Mark the beginning of the match in a token.
                '&endHighlight=' + encodeURIComponent('</mark>') + //  Mark the end of the match in a token.
                '&maxresults=5' +  // The upper limit the for number of suggestions to be included
                // in the response.  Default is set to 5.
                '&app_id=' + APPLICATION_ID +
                '&app_code=' + APPLICATION_CODE + 
                '&prox=Latitude,Longitude';
            ajaxRequest.open('GET', AUTOCOMPLETION_URL + params);
            ajaxRequest.send();
        }
    }
    query = textBox.value;
}
function onAutoCompleteSuccess() {
    clearOldSuggestions();
    addSuggestionsToPanel(this.response);  // In this context, 'this' means the XMLHttpRequest itself.
    addSuggestionsToMap(this.response);
    //console.log(this.response);
}
function onAutoCompleteFailed() {
    alert('Ooops!');
}

// Attach the event listeners to the XMLHttpRequest object
ajaxRequest.addEventListener("load", onAutoCompleteSuccess);
ajaxRequest.addEventListener("error", onAutoCompleteFailed);
ajaxRequest.responseType = "json";
var mapContainer = document.getElementById('map'),
    suggestionsContainer = document.getElementById('panel');
var APPLICATION_ID = 'mJWnpSHggJaiTdbyrCqK',
    APPLICATION_CODE = '4u2ZRbJ-RLg_XYdccD3dQA';

var platform = new H.service.Platform({
    app_id: APPLICATION_ID,
    app_code: APPLICATION_CODE,
    useCIT: false,
    useHTTPS: true
});
var defaultLayers = platform.createDefaultLayers();
var geocoder = platform.getGeocodingService();
var group = new H.map.Group();

group.addEventListener('tap', function (evt) {
    map.setCenter(evt.target.getPosition());
    openBubble(
        evt.target.getPosition(), evt.target.getData());
}, false);
var map = new H.Map(mapContainer,
    defaultLayers.normal.map, {
        center: { lat: 52.5160, lng: 13.3779 },
        zoom: 3
    });
map.addObject(group);
var behavior = new H.mapevents.Behavior(new H.mapevents.MapEvents(map));
var ui = H.ui.UI.createDefault(map, defaultLayers);
var bubble;
function openBubble(position, text) {
    if (!bubble) {
        bubble = new H.ui.InfoBubble(
            position,
            // The FO property holds the province name.
            { content: '<small>' + text + '</small>' });
        ui.addBubble(bubble);
    } else {
        bubble.setPosition(position);
        bubble.setContent('<small>' + text + '</small>');
        bubble.open();
    }
}
function addSuggestionsToMap(response) {
    var onGeocodeSuccess = function (result) {
        var marker,
            locations = result.Response.View[0].Result,
            i;
        for (i = 0; i < locations.length; i++) {
            marker = new H.map.Marker({
                lat: locations[i].Location.DisplayPosition.Latitude,
                lng: locations[i].Location.DisplayPosition.Longitude
            });
            marker.setData(locations[i].Location.Address.Label);
            group.addObject(marker);
        }

        map.setViewBounds(group.getBounds());
        if (group.getObjects().length < 2) {
            map.setZoom(15);
        }
    },
        onGeocodeError = function (error) {
            alert('Ooops!');
        },
        geocodeByLocationId = function (locationId) {
            geocodingParameters = {
                locationId: locationId
            };

            geocoder.geocode(
                geocodingParameters,
                onGeocodeSuccess,
                onGeocodeError
            );
        }
    response.suggestions.forEach(function (item, index, array) {
        geocodeByLocationId(item.locationId);
    });
    console.log(response);
}



function clearOldSuggestions() {
    group.removeAll();
    if (bubble) {
        bubble.close();
    }
}
function addSuggestionsToPanel(response) {
    var suggestions = document.getElementById('suggestions');
    suggestions.innerHTML = JSON.stringify(response, null, ' ');
}



var content = '<strong style="font-size: large;">' + 'Geocoding Autocomplete' + '</strong></br>';

content += '<br/><input type="text" id="auto-complete" style="margin-left:5%; margin-right:5%; min-width:90%"  onkeyup="return autoCompleteListener(this, event);"><br/>';
content += '<br/><strong>Response:</strong><br/>';
content += '<div style="margin-left:5%; margin-right:5%;"><pre style="max-height:235px"><code  id="suggestions" style="font-size: small;">' + '{}' + '</code></pre></div>';

suggestionsContainer.innerHTML = content;

//console.log(response);