$(document).ready(function () {

    $('#registrationForm').submit(function (event) {
        // Prevent the form from submitting
        event.preventDefault();

        // Validate the form inputs
        var isValid = validateForm();
      
        if (isValid) {
            // Submit the form if it's valid
            this.submit();
        }
    });
});

// function to validate registartion form
function validateForm() {
    var isFormValid = true;

    // Reset error message
    $('.error').text('');

    //Individual field validation

    validateUsername();
    validateEmail();
    validatePassword();
    matchPassword();
    // Check if any errors exist
    $('.error').each(function () {
        if ($(this).text() !== '') {
            isFormValid = false;
            return false // exit the loop 
        }
    });

    return isFormValid;
}

// function to validate user name
function validateUsername() {
    var username = $('#userName').val();
    var usernameError = $('#userNameError');
    usernameError.text('');

    if (username.length < 3) {
        usernameError.text('Username must be at least 3 characters long.');
    }
}

// function to validate user email
function validateEmail() {
    var email = $('#userEmail').val();
    var emailError = $('#userEmailError');
    emailError.text('');

    if (!email.includes('@')) {
        emailError.text('Invalid email address.');
    }
}

// function to validate user passowrd
function validatePassword() {
    var password = $('#userPassword').val();
    var passwordError = $('#userPasswordError');
    passwordError.text('');

    if (password.length < 6) {
        passwordError.text('Password must be at least 6 characters long.');
    }
}

// function to validate password confirmation
function matchPassword() {
    var password = $('#userPassword').val();
    console.log(password);
    var retypeUserPassword = $('#retypeUserPassword').val();
    var userPasswordMatchError = $('#userPasswordMatchError');
    userPasswordMatchError.text('');

    if (password != retypeUserPassword) {
        userPasswordMatchError.text('Password do not match.');
    }
    
}