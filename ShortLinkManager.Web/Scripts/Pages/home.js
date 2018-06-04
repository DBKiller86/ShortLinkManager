$(document).ready(function () {
    $("#shortMyLinkForm").submit(

        function (event) {

            // Stop form from submitting normally
            event.preventDefault();
            var $form = $(this);
            var term = $form.find("input[name='s']").val();
            var guestGuid = $("#guestGuid").val();
            var urlPost = $("#urlWebApiPost").val();

            if (urlMatcher(term)) {


                var obj = { Link: term, GuestGuid: guestGuid };

                $.ajax({
                    type: 'POST',
                    url: urlPost,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(obj),
                    success: function (data) {

                        console.log(data);

                        if (data.OperationSucceded) {
                            var lnktext = window.location.protocol + "//" + window.location.host + "/" + data.ShortLink;
                            $("#responseMessage").empty().append("<a href='" + lnktext + "' target='_blank'>" + lnktext + "</a>");
                        }
                        else {
                            $("#responseMessage").empty().append("<p style='color:red'>" + data.ErrorMessage + "</p>");
                        }

                        
                    },
                    error: function (error) {

                        console.log(error);

                        $("#linkTest").empty().append("<p>" + error + "</p>");
                    }
                });
            }
            else
            {
                $("#responseMessage").empty().append("<p style='color:red'>Invalid Link!</p>");
            }

            $('#collapseResult').collapse('show');

        });

    var urlMatcher = function (url) {
        var expression = /[-a-zA-Z0-9@:%_\+.~#?&//=]{2,256}\.[a-z]{2,4}\b(\/[-a-zA-Z0-9@:%_\+.~#?&//=]*)?/gi;
        var regex = new RegExp(expression);

        if (url.match(regex)) {
            return true;
        } else {
            return false;
        }
    }
});
