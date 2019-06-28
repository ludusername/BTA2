const search = document.getElementById("search");
const matchList = document.getElementById("match-list");
let suggestedCitiesElements = [];

var requestedCity;

console.log(search);
const searchCity = async searchText =>  {
   
    if(searchText.length >= 3){
        //console.log(searchText);
        axios.get('https://autocomplete.geocoder.api.here.com/6.2/suggest.json', {
            params:{
                app_id:'BSAfVlleJC9nUT6TISDl',
                app_code: 'HwwUz06BMMZj77AnGZugrA',
                query: searchText
                   
            }
        }).then((res) => {
                let suggestions = res.data.suggestions;
               
                
                let matches = suggestions.filter((suggestion) => {
                    
                    return suggestion.matchLevel === 'city';
                
            });
            console.log(matches);

            
            const outputHtml = matches.map(match => `
                <div class='card card-body suggest-city mb-2' idLocation=${match.locationId} cityName='${match.address.city}' countryName= '${match.address.country}'>
                        <h4> ${match.label}</h4>
                    <small> Postal code: ${match.address.postalCode}</small>
                </div>
         `).join(' ');


            matchList.innerHTML = outputHtml;
            console.log("2 " + outputHtml);
         suggestedCitiesElements = document.getElementsByClassName('suggest-city');
         for(let suggestedCity of suggestedCitiesElements){
            suggestedCity.addEventListener('click', suggestedCityClick);
         }
         
        }).catch((error) => {
            console.log(error)
        })

        
    }

}

search.addEventListener('input', () => searchCity(search.value));

function suggestedCityClick(event) {
    let idLocation = event.currentTarget.getAttribute('idLocation');
    let cityName = event.currentTarget.getAttribute('cityName');
    let countryName = event.currentTarget.getAttribute('countryName');
    console.log("countryName " + countryName);
    search.value = cityName;
    matchList.innerHTML = "";
    console.log(idLocation);
    console.log("1" + cityName);
    axios.get('https://geocoder.api.here.com/6.2/geocode.json', {
        params: {
            app_id: 'BSAfVlleJC9nUT6TISDl',
            app_code: 'HwwUz06BMMZj77AnGZugrA',
            locationId: idLocation
        }
    })
        .then(res => {
            var result = res.data.Response.View[0].Result[0];
            //console.log(result);
            let lat = result.Location.DisplayPosition.Latitude;
            let lng = result.Location.DisplayPosition.Longitude;
            //let country = result.Location.Address.Country;
            //console.log("country " + country);

            moveMapToLocation(lat, lng);

            let cityModel = { CountryName: countryName, CityName: cityName, Lat: lat, Lng: lng };

            console.log(cityModel);


            axios.post('/Account/GetCityInfo', cityModel).then((res) => {
                console.log("Uspesno poslat");
            }).catch((err) => {
                console.log(err);
            });


        })

}


/*https://geocoder.api.here.com/6.2/geocode.xml
?app_id={YOUR_APP_ID}
&app_code={YOUR_APP_CODE}
&searchtext=425+W+Randolph+Chicago
*/