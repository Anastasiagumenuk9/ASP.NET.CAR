window.onload = function() {
    var button = document.getElementById("but");
    var div = document.getElementById("intro");
    var logout = document.getElementById("log-out-message");
    
    button.onclick = function() {

        if(div.style.background != "black") {
            document.getElementById("inner").hidden = true;
            div.style.background = "black";
            div.style.height = "100vh";
            div.style.width = "100%";
            document.getElementById("login-form").hidden = true; 
            document.getElementById('all-log').style.background = "black"; 
        }
        else {
            div.style.background = "transparent";
            document.getElementById('inner').hidden = false;
            div.style.height = "auto";
            div.style.width = "100%";
            document.getElementById('nav').style.background = "transparent";
            document.getElementById("login-form").hidden = false;
            document.getElementById('all-log').style.background = "linear-gradient(rgb(207, 207, 207), rgb(255, 216, 206))";
        }
    };
};

$(function () {
    $("#log-off").click(function (e) {
        if (confirm("Are you sure?")) {
            console.log('Form is submitting');
            $("#log-off").submit();
        } else {
            console.log('User clicked no.');
        }
    });
});






