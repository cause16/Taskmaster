$(document).ready(function () {
    $(".login-form").validate({
        rules: {
            UserName: {
                required: true,
                maxlength: 100
            },
            Password: {
                required: true,
                minlength: 8,
                maxlength: 100
            }
        },
        messages: {
            UserName: {
                required: "Введіть логін",
                maxlength: "Довжина логіну має бути не більше 100 символів"
            },
            Password: {
                required: "Введіть пароль",
                minlength: "Довжина паролю має бути не менше 8 символів",
                maxlength: "Довжина паролю має бути не більше 100 символів"
            }
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") === "UserName") {
                const usernameError = element.siblings(".username-error");
                usernameError.text('');
                error.appendTo(usernameError);
            } else if (element.attr("name") === "Password") {
                const passwordError = element.siblings(".password-error");
                passwordError.text('');
                error.appendTo(passwordError);
            } else {
                error.insertAfter(element);
            }
        }
    });
});
