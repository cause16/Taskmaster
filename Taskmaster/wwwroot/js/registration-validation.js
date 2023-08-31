$(document).ready(function () {
    $(".registration-form").validate({
        rules: {
            FirstName: {
                required: true,
                maxlength: 50
            },
            LastName: {
                required: true,
                maxlength: 50
            },
            Email: {
                required: true,
                email: true,
                maxlength: 100
            },
            Password: {
                required: true,
                minlength: 8,
                maxlength: 100
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#Password"
            }
        },
        messages: {
            FirstName: {
                required: "Введіть ім'я",
                maxlength: "Довжина імені має бути не більше 50 символів"
            },
            LastName: {
                required: "Введіть прізвище",
                maxlength: "Довжина прізвища має бути не більше 50 символів"
            },
            Email: {
                required: "Введіть email",
                email: "Введіть коректний email",
                maxlength: "Довжина email має бути не більше 100 символів"
            },
            Password: {
                required: "Введіть пароль",
                minlength: "Довжина паролю має бути не менше 8 символів",
                maxlength: "Довжина паролю має бути не більше 100 символів"
            },
            ConfirmPassword: {
                required: "Введіть пароль",
                equalTo: "Паролі не співпадають"
            }
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") === "FirstName") {
                const fNameError = element.siblings(".fname-error");
                fNameError.text('');
                error.appendTo(fNameError);
            } else if (element.attr("name") === "LastName") {
                const lNameError = element.siblings(".lname-error");
                lNameError.text('');
                error.appendTo(lNameError);
            } else if (element.attr("name") === "Email") {
                const emailError = element.siblings(".email-error");
                emailError.text('');
                error.appendTo(emailError);
            } else if (element.attr("name") === "Password") {
                const passwordError = element.siblings(".password-error");
                passwordError.text('');
                error.appendTo(passwordError);
            } else if (element.attr("name") === "ConfirmPassword") {
                const confirmPasswordError = element.siblings(".confirm-password-error");
                confirmPasswordError.text('');
                error.appendTo(confirmPasswordError);
            } else {
                error.insertAfter(element);
            }
        }
    });
});
