var i = 1;
function hideBanner() {
    if (i > 3)
        i = 1;
    $(document).ready(function () {
        $("#image").hide("fade", 500);
        setTimeout(showBanner, 500);
    });
}

function showBanner() {
    var s = "../../Images/SideBanner/" + i + ".jpg";
    document.getElementById("image").src = s;
    $("#image").show("fade", 500);
    i++;
}
setInterval(hideBanner, 5000);