var player;
var playerExists = false;
var currentPlayerElementId;
var abc;

$(document).ready(function () {
    $('body').on("submit", ".youtube-form", function (event) {
        event.preventDefault();
        if (playerExists) {
            playerExists = false;
            player.destroy()
            var node = document.getElementById(currentPlayerElementId);
            if (node.parentNode) {
                node.parentNode.removeChild(node);
            }
        }
        var values = $(this).serialize();
        var videoId = values.slice(8);
        currentPlayerElementId = "player-" + videoId;
        console.log(values);
        console.log("Hello");
        $.ajax({
            url: '/Home/GetVideo',
            type: 'GET',
            data: $(this).serialize(),
            dataType: 'html',
            success: function (result) {
                console.log('Hi there');
                $('#' + videoId).html(result);
                playerExists = true;
            }
        });
    });
})