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

    $('#currency-information').on('change', function (event) {
        const currencyId = event.currentTarget.value;
        if (currencyId === '') {
            $('#currency-detailed-information').hide();
            return;
        }

        $.get(`currencies/${currencyId}`, (data) => {
            $('#symbol').text(data.symbol !== '' ? data.symbol : '-');
            $('#name').text(data.name !== '' ? data.name : '-');
            $('#rate').text(data.rate.rateUsd);
            $('#description').text(data.description !== '' ? data.description : '-');
            $('#currency-detailed-information').show();
        });
    });
});