$(document).ready(function () {

    //overlay-modal
    $(document).on('click', '.overlay, .modal .cancel', function () {
        $(".overlay").hide();
        $(".modal").hide();
    });

    //display-settings
    $(".open-update-display-settings-modal").click(function (event) {
        event.preventDefault();

        $(".overlay").show();

        $.ajax({
            url: $(this).attr("href"),
            type: 'GET',
            dataType: 'html',
            success: function (data) {
                $(".modal").html(data).show();
            },
            error: function (xhr, status, error) {
                console.log('An error occurred while loading the display settings update form.');
            }
        });
    });

    $(document).on('submit', '.update-display-settings-form', function (event) {
        event.preventDefault();

        $(this).find('button[type="submit"]').attr('disabled', 'disabled');

        $.ajax({
            url: $(this).attr("action"),
            type: 'PUT',
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                if (data.success === true) {
                    $(".overlay").hide();
                    $(".modal").hide();

                    location.reload();
                }
            },
            error: function (xhr, status, error) {
                if (xhr.getResponseHeader('Content-Type').indexOf('html') !== -1) {
                    $(".modal").html(xhr.responseText).show();
                } else {
                    $('.update-display-settings-form button[type="submit"]').removeAttr('disabled');
                    console.log('An error occurred while updating the display settings.');
                }
            }
        });
    });
});
