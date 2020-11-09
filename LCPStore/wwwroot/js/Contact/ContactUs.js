
$(document).ready(function () {
    $('#contactForm').submit(function (event) {
        event.preventDefault();
        var name = $("input#Name").val();
        var email = $("input#Email").val();
        var subject = $("input#Subject").val();
        var body = $("textarea#Body").val();

        $.ajax({
            url: "/Contacts/Contact",
            type: "POST",
            data: {
                Name: name,
                Email: email,
                Subject: subject,
                Body: body,
            },
            cache: false,
            success: function (data) {
                // Success message
                $("#success").html("<div class='alert alert-success'>");
                $("#success > .alert-success")
                    .html(
                        "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;"
                    )
                    .append("</button>");
                $("#success > .alert-success").append(
                    "<strong>Your message has been sent. </strong>"
                );
                $("#success > .alert-success").append("</div>");
                //clear all fields
                $("#contactForm").trigger("reset");
            },
            error: function (data) {
                // Fail message
                $("#success").html("<div class='alert alert-danger'>");
                $("#success > .alert-danger")
                    .html(
                        "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;"
                    )
                    .append("</button>");
                $("#success > .alert-danger").append(
                    $("<strong>").text(
                        "Sorry, it seems that our mail server is not responding. Please try again later!"
                    )
                );
                $("#success > .alert-danger").append("</div>");
                //clear all fields
                $("#contactForm").trigger("reset");
            },
        });
    });
});





