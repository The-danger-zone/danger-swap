"use strict";

$(document).ready(function () {
    $('#toggle-password').on('click', function (event) {
        toggleEyeLashIcon();
        let input = event.currentTarget.parentElement.previousElementSibling;
        input.type = input.type === 'password' ? 'text' : 'password';
    });

    function toggleEyeLashIcon() {
        const eyeSlashed = 'fa-eye-slash';
        $('#toggle-password').toggleClass(eyeSlashed);
    }
});