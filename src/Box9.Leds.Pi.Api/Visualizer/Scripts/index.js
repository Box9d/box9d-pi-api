$(function () {
    var playback = $.connection.playbackHub;
    playback.client.broadcastMessage = function (name, message) {

        if (name.toLower() === 'blackout') {
            $('#display').empty();
        }

        if (name.toLower() === 'frame') {
            var html = '';
            var pixelsPerRow = message.length / $('#rowcount').val();

            for (var i = 0; i < $('#rowcount').val(); i++) {
                var rowDiv = '<div>';
                for (var j = 0; j < pixelsPerRow; j++) {
                    var color = message[i * pixelsPerRow + j + 1];
                    rowDiv += '<div class=\'pixel\' style=\'background-color:' + color + '\'/>';
                }

                rowDiv += '</div>';
                html += rowDiv;
            }

            $('#display').html(html);
        }
    };
});

$(function () {
    $('#rowcount').bind('input', function () {
        if ($(this).val() === "0") {
            $(this).val("1"); // Do not allow 0 value
        }
    });
});