"use strict";

$(document).ready(function () {
    $('#togglePassword').on('click', function (event) {
        toggleEyeLashIcon();
        let input = event.currentTarget.parentElement.previousElementSibling;
        input.type = input.type === 'password' ? 'text' : 'password';
    });

    function toggleEyeLashIcon() {
        const eyeSlashed = 'fa-eye-slash';
        $('#togglePassword').toggleClass(eyeSlashed);
    }
});