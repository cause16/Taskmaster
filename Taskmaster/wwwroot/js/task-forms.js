$(document).ready(function () {

    //create-task
    $(".open-create-task-modal").click(function (event) {
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
                console.log('An error occurred while loading the create task form.');
            }
        });
    });

    $(document).on('submit', '.create-task-form', function (event) {
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

                    location.reload();
                }
            },
            error: function (xhr, status, error) {
                if (xhr.getResponseHeader('Content-Type').indexOf('html') !== -1) {
                    $(".modal").html(xhr.responseText).show();
                    initializeValidation();
                } else {
                    $('.create-task-form button[type="submit"]').removeAttr('disabled');
                    console.log('An error occurred while creating a task.');
                }
            }
        });
    });

    //update-task
    $(".open-update-task-modal").click(function (event) {
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
                console.log('An error occurred while loading the task update form.');
            }
        });
    });

    $(document).on('submit', '.update-task-form', function (event) {
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
                    $('.update-task-form button[type="submit"]').removeAttr('disabled');
                    console.log('An error occurred while updating the task.');
                }
            }
        });
    });

    //delete-task
    $(".open-delete-task-modal").click(function (event) {
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
                console.log('An error occurred while loading the task delete form.');
            }
        });
    });

    $(document).on('submit', '.delete-task-form', function (event) {
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

                    location.reload();
                }
            },
            error: function (xhr, status, error) {
                if (xhr.getResponseHeader('Content-Type').indexOf('html') !== -1) {
                    $(".modal").html(xhr.responseText).show();
                } else {
                    $('.delete-task-form button[type="submit"]').removeAttr('disabled');
                    console.log('An error occurred while deleting the task.');
                }
            }
        });
    });

    //task-is-done

    $(document).on("click", ".task-is-done", function (event) {
        event.preventDefault();

        const clickedElement = this;

        $.get('/project-task/get-anti-forgery-token', function (data) {
            const csrfToken = data.requestToken;

            $.ajax({
                url: $(clickedElement).attr("href"),
                type: 'DELETE',
                dataType: 'json',
                headers: {
                    "RequestVerificationToken": csrfToken
                },
                success: function (data) {
                    if (data.success === true) {
                        location.reload();
                    }
                },
                error: function (xhr, status, error) {
                    console.log('An error occurred while deleting completed task.');
                }
            });
        });
    });

    function initializeValidation() {
        jQuery.validator.addMethod("validYearRange", function (value) {
            if (value === "") {
                return true;
            }

            const endDate = new Date(value);
            const year = endDate.getFullYear();

            return year >= 1 && year <= 9999;
        }, "Недопустимий діапазон для року");

        jQuery.validator.addMethod("dateInFutureOnly", function (value) {
            if (value === "") {
                return true;
            }

            const endDate = new Date(value);
            const currentDate = new Date();

            endDate.setHours(0, 0, 0, 0);
            currentDate.setHours(0, 0, 0, 0);

            if (endDate < currentDate) {
                return false;
            }

            return true;
        }, "Виберіть дату, яка не буде меншою за поточну");

        jQuery.validator.addMethod("timeInFutureOnly", function (value) {
            if (value === "") {
                return true;
            }

            const endDateValue = $("#EndDate").val();

            if (!endDateValue) {
                return true;
            }

            const endDate = new Date(endDateValue);
            const endTime = new Date("1970-01-01 " + value);

            const currentDate = new Date();
            const currentTime = new Date(1970, 0, 1, currentDate.getHours(), currentDate.getMinutes(), currentDate.getSeconds());

            if (endDate.toDateString() == currentDate.toDateString() && endTime < currentTime) {
                return false;
            }

            return true;
        }, "Виберіть час, який не буде меншим, ніж поточний");

        jQuery.validator.addMethod("requireTimeWithDate", function () {
            const endDateValue = $("#EndDate").val();
            const endTimeValue = $("#EndTime").val();

            if (!endDateValue && endTimeValue) {
                return false;
            }

            return true;
        }, "Виберіть дату");

        $(".create-task-form, .update-task-form").validate({
            rules: {
                Name: {
                    required: true,
                    maxlength: 50
                },
                Description: {
                    maxlength: 1000
                },
                Priority: {
                    required: true
                },
                EndDate: {
                    requireTimeWithDate: true,
                    validYearRange: true,
                    dateInFutureOnly: true
                },
                EndTime: {
                    timeInFutureOnly: true
                }
            },
            messages: {
                Name: {
                    required: "Введіть назву задачі",
                    maxlength: "Довжина назви задачі має бути не більше 50 символів"
                },
                Description: {
                    maxlength: "Довжина опису має бути не більше 1000 символів"
                },
                Priority: {
                    required: "Оберіть пріоритет"
                }
            },
            errorPlacement: function (error, element) {
                if (element.attr("name") === "Name") {
                    const nameError = element.siblings(".task-name-error");
                    nameError.text('');
                    error.appendTo(nameError);
                } else if (element.attr("name") === "Description") {
                    const lNameError = element.siblings(".task-description-error");
                    lNameError.text('');
                    error.appendTo(lNameError);
                } else if (element.attr("name") === "Priority") {
                    const emailError = element.siblings(".task-priority-error");
                    emailError.text('');
                    error.appendTo(emailError);
                } else if (element.attr("name") === "EndDate") {
                    const passwordError = element.siblings(".task-date-error");
                    passwordError.text('');
                    error.appendTo(passwordError);
                } else if (element.attr("name") === "EndTime") {
                    const confirmPasswordError = element.siblings(".task-time-error");
                    confirmPasswordError.text('');
                    error.appendTo(confirmPasswordError);
                } else {
                    error.insertAfter(element);
                }
            }
        });
    }
});
