//Dark ski have these symbols for the weather clear-day, clear-night, rain, snow, sleet, wind, fog, cloudy, partly-cloudy-day, or partly-cloudy-night


var container = document.getElementById('Conditions');

var icon = "clear-day"
// var icon = "clear-night"
// var icon = "partly-cloudy-day"
// var icon = "partly-cloudy-night"
// var icon = "rain"
// var icon = "cloudy"
// var icon = "fog"
// var icon = "wind"
// var icon = ""

if (icon == "clear-day") {
    greeting = "clear day";
}
else if (icon == "clear-night") {
    greeting = "clear-night";
}
else if (icon == "partly-cloudy-day") {
    greeting = "partly-cloudy-day";
}
else if (icon == "partly-cloudy-night") {
    greeting = "partly-cloudy-night";
}
else if (icon == "cloudy") {
    greeting = "cloudy";
}
else if (icon == "wind") {
    greeting = "wind";
}
else if (icon == "fog") {
    greeting = "fog";
}
else if (icon == "rain") {
    greeting = "rain";
}

else {
    greeting = "It must be the end of the world then! Run, pray, either way, kiss your ass gooobye!!!! ";
}


console.log(greeting);
