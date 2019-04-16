////< !--I have to press it twice for some reason
//function getReverseGeocodingData(lat, lng) {
//        var latlng = new google.maps.LatLng(lat, lng);
//        // This is making the Geocode request
//        var geocoder = new google.maps.Geocoder();
//        geocoder.geocode({ 'latLng': latlng }, function (results, status) {
//            if (status !== google.maps.GeocoderStatus.OK) {
//                alert(status);
//            }
//            // This is checking to see if the Geoeode Status is OK before proceeding
//            if (status == google.maps.GeocoderStatus.OK) {
//                console.log(results);
//                var address = (results[0].formatted_address);
//            }
//        });
//    }


//function showAddress(position) {
//        x.innerHTML = "Latitude: " + position.coords.latitude +
//            "<br>Longitude: " + position.coords.longitude;
//    }

////@* Now I need to store the result in my model so I can access it in my api controller *@
// <script>
//    type = "text/javascript" src = "https://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452&key=AIzaSyCY4XX-om0CBwXnmF0XbqM2M1kiUcOsZ3Q" >

//        </script>


//        function getReverseGeocodingData(lat, lng) {
//                var latlng = new google.maps.LatLng(lat, lng);
//        // This is making the Geocode request
//        var geocoder = new google.maps.Geocoder();
//                geocoder.geocode({'latLng': latlng }, function (results, status) {
//                    if (status !== google.maps.GeocoderStatus.OK) {
//            alert(status);
//        }
//        // This is checking to see if the Geoeode Status is OK before proceeding
//                    if (status == google.maps.GeocoderStatus.OK) {
//            console.log(results);
//        var address = (results[0].formatted_address);
//    }
//});
//}

