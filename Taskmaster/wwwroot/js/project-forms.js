$(document).ready(function () {

    //create-project
    $(".open-create-project-modal").click(function (event) {
        event.preventDefault();

        $(".overlay").show();

        $.ajax({
            url: $(this).attr("href"),
            type: 'GET',
            dataType: 'html',
            success: function (data) {
                $(".modal").html(data).show();
                initializeValidation();
            },
            error: function (xhr, status, error) {
                console.log('An error occurred while loading the create project form.');
            }
        });
    });

    $(document).on('submit', '.create-project-form', function (event) {
        event.preventDefault();

        $(this).find('button[type="submit"]').attr('disabled', 'disabled');

        $.ajax({
            url: $(this).attr("action"),
            type: 'POST',
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                if (data.success === true) {
                    $(".overlay").hide();
                    $(".modal").hide();

                    window.location.href = '/project-task/project/' + data.id;
                }
            },
            error: function (xhr, status, error) {
                if (xhr.getResponseHeader('Content-Type').indexOf('html') !== -1) {
                    $(".modal").html(xhr.responseText).show();
                    initializeValidation();
                } else {
                    $('.create-project-form button[type="submit"]').removeAttr('disabled');
                    console.log('An error occurred while creating the project.');
                }
            }
        });
    });

    //update-project
    $(".open-update-project-modal").click(function (event) {
        event.preventDefault();

        $(".overlay").show();

        $.ajax({
            url: $(this).attr("href"),
            type: 'GET',
            dataType: 'html',
            success: function (data) {
                $(".modal").html(data).show();
                initializeValidation();
            },
            error: function (xhr, status, error) {
                console.log('An error occurred while loading the project update form.');
            }
        });
    });

    $(document).on('submit', '.update-project-form', function (event) {
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
                    initializeValidation();
                } else {
                    $('.update-project-form button[type="submit"]').removeAttr('disabled');
                    console.log('An error occurred while updating the project.');
                }
            }
        });
    });

    //delete-project
    $(".open-delete-project-modal").click(function (event) {
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
                console.log('An error occurred while loading the project delete form.');
            }
        });
    });

    $(document).on('submit', '.delete-project-form', function (event) {
        event.preventDefault();

        $(this).find('button[type="submit"]').attr('disabled', 'disabled');

        $.ajax({
            url: $(this).attr("action"),
            type: 'DELETE',
            data: $(this).serialize(),
            dataType: 'json',
            success: function (data) {
                if (data.success === true) {
                    $(".overlay").hide();
                    $(".modal").hide();

                    window.location.href = '/project-task';
                }
            },
            error: function (xhr, status, error) {
                if (xhr.getResponseHeader('Content-Type').indexOf('html') !== -1) {
                    $(".modal").html(xhr.responseText).show();
                } else {
                    $('.delete-project-form button[type="submit"]').removeAttr('disabled');
                    console.log('An error occurred while deleting the project.');
                }
            }
        });
    });

    function initializeValidation() {
        $(".create-project-form, .update-project-form").validate({
            rules: {
                Name: {
                    required: true,
                    maxlength: 50
                }
            },
            messages: {
                Name: {
                    required: "Введіть назву проєкту",
                    maxlength: "Довжина назви проєкту має бути не більше 50 символів"
                }
            },
            errorPlacement: function (error, element) {
                if (element.attr("name") === "Name") {
                    const nameError = element.siblings(".project-name-error");
                    nameError.text('');
                    error.appendTo(nameError);
                } else {
                    error.insertAfter(element);
                }
            }
        });
    }
});
