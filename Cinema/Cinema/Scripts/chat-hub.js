$(document).ready(
    function () {
        //Declare a proxy to reference hub
        var chat = $.connection.chatHub;

        chat.client.broadcastMessage = function (name, message) {
            var encodedName = $('<div />').text(name).html();
            var encodedMessage = $('<div />').text(message).html();
            $('#discussion').append('<li><strong>' + encodedName + '</strong>:&nbsp;&nbsp;' + encodedMessage + '</li>');
        }

        $('#message').focus();

        $.connection.hub.start()
            .done(function () {
                $('#sendmessage').click(function () {
                    chat.server.send($('#displayname').val(), $('#message').val());
                    $('#message').val('').focus();
                });
                $('#show-chat').click(function () {
                    var chatBodyNode = $('.js-chat-body');
                    if (chatBodyNode.hasClass('hidden')) {
                        chatBodyNode.removeClass('hidden');
                    } else {
                        chatBodyNode.addClass('hidden');
                    }
                });
            });
    });