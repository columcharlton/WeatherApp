
//<p>Click the button to get your coordinates.</p>

//    <button onclick="getLocation()">Try It</button>

//    <p id="demo"></p>


//    <script>
//        var x = document.getElementById("demo");

//        function getLocation() {
//            if (navigator.geolocation) {
//            navigator.geolocation.getCurrentPosition(showPosition, showError);
//        } else {
//            x.innerHTML = "Geolocation is not supported by this browser.";
//        }
//    }

//        function showPosition(position) {
//            x.innerHTML = "Latitude: " + position.coords.latitude +
//            "<br>Longitude: " + position.coords.longitude;

//        }

//        function showError(error) {
//            switch (error.code) {
//                case error.PERMISSION_DENIED:
//            x.innerHTML = "User denied the request for Geolocation."
//            break;
//        case error.POSITION_UNAVAILABLE:
//            x.innerHTML = "Location information is unavailable."
//            break;
//        case error.TIMEOUT:
//            x.innerHTML = "The request to get user location timed out."
//            break;
//        case error.UNKNOWN_ERROR:
//            x.innerHTML = "An unknown error occurred."
//            break;
//    }
//}


//    </script>*@
//< !--
//< script >

//    function success(pos) {
//        var crd = pos.coords;

//        console.log('Your current position is:');
//        console.log(`Latitude : ${//crd.latitude}`);
//            console.log(`Longitude: ${//crd.longitude}`);
//    }


//    navigator.geolocation.getCurrentPosition(success);

//</script>
//    -->
//@*<script>

//        function success(pos) {
//            var crd = pos.coords;

//            console.log('Your current position is:');
//            console.log(`Latitude : ${ crd.latitude }`);
//            console.log(`Longitude: ${ crd.longitude }`);


//            var model = new Object();
//            model.Latitude = crd.latitude;
//            model.Longitude = crd.longitude;

//            alert(model.Latitude);

//            jQuery.ajax({
//                type: "POST",
//                url: "@Url.Action("Index")",
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                data: JSON.stringify({ co: model }),
//                success: function (data) {
//                    alert(data);
//                },
//                failure: function (errMsg) {
//                    alert(errMsg);
//                }
//            });
//        }


//        navigator.geolocation.getCurrentPosition(success);

//    </script>*@

//<!--
//<script src="~/Scripts/jquery-1.10.2.min.js"></script>

//<script>

//    function submitForm() {

//        navigator.geolocation.getCurrentPosition(success);


//        var model = new Object();
//        model.Latitude = 260;
//        model.Longitude = 240;

//        alert(model.Latitude);

//        jQuery.ajax({
//            type: "POST",
//            url: "Url.Action("Coordinates")",
//            dataType: "json",
//            contentType: "application/json; charset=utf-8",
//            data: JSON.stringify({ co: model }),
//            success: function (data) {
//                alert(data);
//            },
//            failure: function (errMsg) {
//                alert(errMsg);
//            }
//        });
//    }
//</script>

//<input type="button" value="Click" onclick="submitForm()" />
//    -->
//<!--
//    <script src="~/Scripts/jquery-1.10.2.min.js"></script>

//    <script>
//        function submitForm() {

//            var model = new Object();
//            model.Latitude = 120;
//            model.Longitude = 240;


//            jQuery.ajax({
//                type: "POST",
//                url: "Url.Action("Coordinates")",
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                data: JSON.stringify({ co: model }),
//                success: function (data) {
//                    alert(data);
//                },
//                failure: function (errMsg) {
//                    alert(errMsg);
//                }
//            });
//        }
//    </script>

//    <input type="button" value="Click" onclick="submitForm()" />
//    -->