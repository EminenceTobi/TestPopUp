//--------------------------------------- global variables -------------------------------------------------------------------------------------------
var cardPaymentElement = "<!-- start: form enter card details -->\r\n<section id=\"enter_card_info_section\" class=\"all-card-payment-form\">\r\n    <form id=\"cardPaymentValidation\">\r\n        <div class=\"row\">\r\n            <div class=\"col-12 text-center mb-1\">\r\n                <h4>Pay <span class=\"transaction-amount\"><\/span><\/h4>\r\n            <\/div>\r\n            <div class=\"col-12 text-center\">\r\n                <h6 class=\"text-primary\">Enter Card Details<\/h6>\r\n            <\/div>\r\n            <div class=\"col-12 mb-1\">\r\n                <label class=\"form-label\" for=\"transaction_card_number\">Card Number<\/label>\r\n                <input id=\"transaction_card_number\"\r\n                       name=\"transaction_card_number\"\r\n                       class=\"form-control add-credit-card-mask\"\r\n                       type=\"text\"\r\n                       placeholder=\"0000 0000 0000 0000\" \/>\r\n            <\/div>\r\n            <div class=\"col-6 mb-1\">\r\n                <label class=\"form-label\" for=\"transaction_card_expiry_date\">Expiry Date<\/label>\r\n                <input type=\"text\"\r\n                       id=\"transaction_card_expiry_date\"\r\n                       name=\"transaction_card_expiry_date\"\r\n                       class=\"form-control add-expiry-date-mask\"\r\n                       placeholder=\"MM\/YY\" \/>\r\n            <\/div>\r\n            <div class=\"col-6 mb-1\">\r\n                <label class=\"form-label\" for=\"transaction_card_cvv\">CVV<\/label>\r\n                <input type=\"text\"\r\n                       id=\"transaction_card_cvv\"\r\n                       name=\"transaction_card_cvv\"\r\n                       class=\"form-control add-cvv-code-mask\"\r\n                       maxlength=\"3\"\r\n                       placeholder=\"000\" \/>\r\n            <\/div>\r\n            <div class=\"d-grid w-100 mt-1\">\r\n                <button id=\"btn_confirm_card_info\" type=\"button\" class=\"btn btn-primary btn-lg\">\r\n                    Pay\r\n                <\/button>\r\n            <\/div>\r\n            <div class=\"d-grid w-100 mt-1 text-center\">\r\n                <a href=\"javascript:void()\" class=\"text-decoration-underline fs-4 fw-bolder link-cancel-payment\">\r\n                    Cancel Payment\r\n                <\/a>\r\n            <\/div>\r\n        <\/div>\r\n    <\/form>\r\n<\/section>\r\n<!-- end: form enter card details -->\r\n<!-- start: form enter card pin -->\r\n<section id=\"enter_card_pin_section\" class=\"all-card-payment-form d-none\">\r\n    <form id=\"cardPinValidation\">\r\n        <div class=\"row\">\r\n            <div class=\"col-12 text-center mb-1\">\r\n                <h4>Pay <span class=\"transaction-amount\"><\/span><\/h4>\r\n            <\/div>\r\n            <div class=\"col-12 text-center\">\r\n                <h6 class=\"text-primary\">Enter PIN<\/h6>\r\n            <\/div>\r\n            <div class=\"col-12 mb-1\">\r\n                <div class=\"auth-input-wrapper d-flex align-items-center justify-content-between\">\r\n                    <input type=\"text\"\r\n                           class=\"form-control card-payment-pin-input auth-input height-50 text-center numeral-mask mx-25 mb-1\"\r\n                           maxlength=\"1\"\r\n                           name=\"card_pin\"\r\n                           autofocus=\"\" \/>\r\n\r\n                    <input type=\"text\"\r\n                           class=\"form-control card-payment-pin-input auth-input height-50 text-center numeral-mask mx-25 mb-1\"\r\n                             name=\"card_pin\"\r\n                           maxlength=\"1\" \/>\r\n\r\n                    <input type=\"text\"\r\n                           class=\"form-control card-payment-pin-input auth-input height-50 text-center numeral-mask mx-25 mb-1\"\r\n                             name=\"card_pin\"\r\n                           maxlength=\"1\" \/>\r\n\r\n                    <input type=\"text\"\r\n                           class=\"form-control card-payment-pin-input auth-input height-50 text-center numeral-mask mx-25 mb-1\"\r\n                             name=\"card_pin\"\r\n                           maxlength=\"1\" \/>\r\n                <\/div>\r\n            <\/div>\r\n            <div class=\"d-grid w-100 mt-1\">\r\n                <button type=\"button\" class=\"btn btn-primary btn-lg\" id=\"btn_confirm_transaction_pin\">\r\n                    Complete Payment\r\n                <\/button>\r\n            <\/div>\r\n            <div class=\"d-grid w-100 mt-1 text-center\">\r\n                <a href=\"javascript:void()\" class=\"text-decoration-underline fs-4 fw-bolder link-cancel-payment\">\r\n                    Cancel Payment\r\n                <\/a>\r\n            <\/div>\r\n        <\/div>\r\n    <\/form>\r\n<\/section>\r\n<!-- end: form enter card pin -->\r\n<!-- start:  form otp sent -->\r\n<section id=\"enter_transaction_otp_section\" class=\"all-card-payment-form d-none\">\r\n    <form id=\"paymentOtpValidation\">\r\n        <div class=\"row\">\r\n            <div class=\"col-12 text-center mb-1\">\r\n                <h4>OTP sent to <span class=\"customer-phone\">08100848422<\/span><\/h4>\r\n            <\/div>\r\n            <div class=\"col-12 text-center\">\r\n                <h6 class=\"text-primary\">Enter OTP<\/h6>\r\n            <\/div>\r\n            <div class=\"col-12 mb-1\">\r\n                <input id=\"transaction_otp\"\r\n                       name=\"transaction_otp\"\r\n                       class=\"form-control\"\r\n                       type=\"text\"\r\n                       maxlength=\"6\"\r\n                       placeholder=\"123456\" \/>\r\n            <\/div>\r\n            <div class=\"d-grid w-100 mt-1\">\r\n                <button class=\"btn btn-primary btn-lg\" id=\"btn_confirm_transaction_otp\">\r\n                    Confirm OTP\r\n                <\/button>\r\n            <\/div>\r\n            <div class=\"d-grid w-100 mt-1 text-center\">\r\n                <a href=\"javascript:void()\" class=\"text-decoration-underline fs-4 fw-bolder link-cancel-payment\">\r\n                    Cancel Payment\r\n                <\/a>\r\n            <\/div>\r\n        <\/div>\r\n    <\/form>\r\n<\/section>\r\n<!-- end: form otp sent-->";
var transferPaymentElement = "<!-- start: virtual account detail-->\r\n<section id=\"virtual_account_section\" class=\"all-transfer-payment-form\">\r\n    <div class=\"row\">\r\n        <div class=\"col-12 text-center mb-1\">\r\n            <h4>Transfer <span class=\"transaction-amount\"><\/span><\/h4>\r\n        <\/div>\r\n        <div class=\"col-12 mb-2\" id=\"\">\r\n            <h5 class=\"fw-bolder text-primary d-flex justify-content-center mb-1\" id=\"transfer_account_name\">\r\n               Account Name\r\n            <\/h5>\r\n            <div class=\"bg-light-primary position-relative rounded p-2\">\r\n                <h6 class=\"d-flex align-items-center fw-bolder mb-1\">\r\n                    <span class=\"me-50 text-primary\">Bank Name: <span class=\"text-primary p-1\" id=\"transfer_bank_name\">Test Bank<\/span><\/span>\r\n                <\/h6>\r\n                <h6 class=\"d-flex align-items-center fw-bolder mb-1\">\r\n                    <span class=\"me-50 text-primary\">Account Number: <span class=\"text-primary p-1\" id=\"transfer_account_number\">0000000000<\/span><a id=\"btn_copy_account_number\" onclick=\"copyToClipboard(this)\" data-id=\"0000000000\" href=\"javascript:void()\"><i data-feather=\'copy\'><\/i><\/a><\/span>\r\n                <\/h6>\r\n            <\/div>\r\n        <\/div>\r\n        <div class=\"d-grid w-100 mt-1\">\r\n            <button type=\"button\" id=\"btn_transfer_sent\" class=\"btn btn-primary btn-lg\">\r\n                Transfer Sent\r\n            <\/button>\r\n        <\/div>\r\n        <div class=\"d-grid w-100 mt-1 text-center\">\r\n            <a href=\"javascript:void()\" class=\"text-decoration-underline fs-4 fw-bolder link-cancel-payment\">\r\n                Cancel Payment\r\n            <\/a>\r\n        <\/div>\r\n    <\/div>\r\n<\/section>\r\n<!-- end: virtual account detail-->\r\n<!-- start: transfer confirmation wait-->\r\n<section id=\"transfer_confirmation_wait_section\" class=\"all-transfer-payment-form d-none\">\r\n    <div class=\"row\">\r\n        <div class=\"col-12 text-center mb-1\">\r\n            <h4 class=\"text-primary\">Awaiting Confirmation... Just a sec<\/h4>\r\n        <\/div>\r\n        <div class=\"col-12 mt-2 mb-2\">\r\n            <div class=\"progress progress-bar-primary\" style=\"height:5px\">\r\n                <div class=\"progress-bar progress-bar-striped progress-bar-animated\"\r\n                     role=\"progressbar\"\r\n                     aria-valuenow=\"60\"\r\n                     aria-valuemin=\"60\"\r\n                     aria-valuemax=\"100\"\r\n                     style=\"width: 60%\"><\/div>\r\n            <\/div>\r\n        <\/div>\r\n        <div class=\"col-12 mb-1\">\r\n            <div class=\"bg-light-primary position-relative rounded p-2\">\r\n                <h1 class=\"fw-bolder fs-1 text-primary d-flex justify-content-center\" id=\"transfer_request_time\">02:00<\/h1>\r\n            <\/div>\r\n        <\/div>\r\n        <div class=\"d-grid w-100 mt-1\">\r\n            <a href=\"javascript:void()\" class=\"text-decoration-underline fs-4 fw-bolder link-cancel-payment\">\r\n                Cancel Payment\r\n            <\/a>\r\n        <\/div>\r\n    <\/div>\r\n<\/section>\r\n<!-- end: transfer confirmation wait-->";
//--------------------------------------- desktop - pay with card -------------------------------------------------------------------------------------------
$(document).on("click", ".pay-with-card-trigger-lg", function () {
    $("#desktop_card_payment_section").html(cardPaymentElement);
    $(".both-payment-type-trigger-lg").removeClass("active");
    $(".pay-with-card-trigger-lg").addClass("active");
    $(".both-desktop-payment-section").addClass("d-none");
    $(".general-popup-section").addClass("d-none");
    $("#desktop_card_payment_section").removeClass("d-none");
});
//$(window).on('load', function () {
//    $("#desktop_card_payment_section").html(cardPaymentElement);
//})
//--------------------------------------------------------- desktop - pay with transfer ----------------------------------------------------------------------------
$(document).on("click", ".pay-with-transfer-trigger-lg", function () {
    $("#desktop_transfer_payment_section").html(transferPaymentElement);
    initiatePaymentTransfer();
    $(".both-payment-type-trigger-lg").removeClass("active");
    $(".pay-with-transfer-trigger-lg").addClass("active");
    $(".both-desktop-payment-section").addClass("d-none");
    $(".general-popup-section").addClass("d-none");
    $("#desktop_transfer_payment_section").removeClass("d-none");
});

//--------------------------------------- mobile - pay with card -------------------------------------------------------------------------------------------

$(document).on("click", ".pay-with-card-trigger-md", function () {
    $("#mobile_card_payment_section").html(cardPaymentElement);
});

//--------------------------------------------------------- mobile - pay with transfer ----------------------------------------------------------------------------
$(document).on("click", ".pay-with-transfer-trigger-md", function () {
    $("#mobile_transfer_payment_section").html(transferPaymentElement);
    initiatePaymentTransfer();
});

//--------------------------------------------------------- hide and show mobile & desktop element with screen size ----------------------------------------------------------------------------
$(document).ready(function () {
    if ($(window).width() >= 480) {
        // card section
        $("#desktop_card_payment_section").html(cardPaymentElement);
        $("#mobile_card_payment_section").html("");
        // transfer
        $("#desktop_transfer_payment_section").html(transferPaymentElement);
        $("#mobile_transfer_payment_section").html("");
    }
    else if ($(window).width() <= 479) {
        // card section
        $("#mobile_card_payment_section").html(cardPaymentElement);
        $("#desktop_card_payment_section").html("");
        // transfer section
        $("#mobile_transfer_payment_section").html(transferPaymentElement);
        $("#desktop_transfer_payment_section").html("");
    }
    else if ($(window).width() <= 959) {
        // card section
        $("#desktop_card_payment_section").html(cardPaymentElement);
        $("#mobile_card_payment_section").html("");
        // card section
        $("#desktop_transfer_payment_section").html(transferPaymentElement);
        $("#mobile_transfer_payment_section").html("");
    }
    // This will fire when document is ready:
    $(window).resize(function () {
        if ($(window).width() >= 480) {
            // card section
            $("#desktop_card_payment_section").html(cardPaymentElement);
            $("#mobile_card_payment_section").html("");
            // transfer
            $("#desktop_transfer_payment_section").html(transferPaymentElement);
            $("#mobile_transfer_payment_section").html("");
        }
        else if ($(window).width() <= 479) {
            // card section
            $("#mobile_card_payment_section").html(cardPaymentElement);
            $("#desktop_card_payment_section").html("");
            // transfer section
            $("#mobile_transfer_payment_section").html(transferPaymentElement);
            $("#desktop_transfer_payment_section").html("");
        }
        else if ($(window).width() <= 959) {
            // card section
            $("#desktop_card_payment_section").html(cardPaymentElement);
            $("#mobile_card_payment_section").html("");
            // card section
            $("#desktop_transfer_payment_section").html(transferPaymentElement);
            $("#mobile_transfer_payment_section").html("");
        }
    }).resize(); // This will simulate a resize to trigger the initial run.
});
//--------------------------------------------------------- pay with card process function ----------------------------------------------------------------------------
var transactionCardNumber = "";
var transactionCardExpiryDate = "";
var transactionCardCvv = "";
var cardPinArray = [];
var cardPinInputItems = $(".card-payment-pin-input");
var transactioncardPin = "";
var transactionOtp = "";
//var paymentReference = "";
//var paymentChannel = "";
// card detail function
$(function () {
    var a = $(".add-credit-card-mask"),
        t = $("#cardPaymentValidation"),
        d = $(".add-expiry-date-mask"),
        n = $(".add-cvv-code-mask");
       "laravel" === $("body").attr("data-framework") && (e = $("body").attr("data-asset-path")),
        a.length &&
        a.each(function () {
            new Cleave($(this), {
                creditCard: !0,
            });
        }),
        d.length &&
        d.each(function () {
            new Cleave($(this), { date: !0, delimiter: "/", datePattern: ["m", "y"] });
        }),
        n.length &&
        n.each(function () {
            new Cleave($(this), { numeral: !0, numeralPositiveOnly: !0 });
        })
});
$(function () {
    "use strict";
    var enterCreditCardForm = $("#cardPaymentValidation");
    enterCreditCardForm.validate({
        rules: {
            "transaction_card_number": { required: true },
            "transaction_card_expiry_date": { required: true },
            "transaction_card_cvv": { required: true },
        }
    });
});
$(document).on("click", "#btn_confirm_card_info", function () {
    var isfilled = $("#cardPaymentValidation").valid();
    if (isfilled == false) return;
    transactionCardNumber = $("#transaction_card_number").val();
    transactionCardExpiryDate = $("#transaction_card_expiry_date").val();
    transactionCardCvv = $("#transaction_card_cvv").val();
    $(".all-card-payment-form").addClass("d-none");
    $("#enter_card_pin_section").removeClass("d-none");
});
// card pin function
$(function () {
    $("#cardPinValidation").validate();
    $("[name=card_pin]").each(function () {
        $(this).rules("add", {
            required: true,
        });
    });
});
$(document).on("click", "#btn_confirm_transaction_pin", function () {
    var isfilled = $("#cardPinValidation").valid();
    if (isfilled == false) return;
    for (var i = 0; i < cardPinInputItems.length; i++) {
        cardPinArray.push($(cardPinInputItems[i]).val());
    }
    transactioncardPin = cardPinArray.join('');
    $(".all-card-payment-form").addClass("d-none");
    $("#enter_card_pin_section").removeClass("d-none");
});
// confirm otp function
$(function () {
    "use strict";
    var paymentOtpForm = $("#paymentOtpValidation");
    paymentOtpForm.validate({
        rules: {
            "transaction_otp": { required: true },
        }
    });
});
$(document).on("click", "#btn_confirm_transaction_otp", function () {
    transactioncardPin = $("#transaction_otp").val();
    $(".all-card-payment-form").addClass("d-none");
    $("#enter_transaction_otp_section").removeClass("d-none");
});
//--------------------------------------------------------- pay with transfer process function ----------------------------------------------------------------------------
function copyToClipboard(d) {
    var copyText = d.getAttribute("data-id")
    navigator.clipboard
        .writeText(copyText)
        .then(() => {
            toastr.success("", "Copied to clipboard!");
        })
        .catch(() => {
            toastr.error("", "Something went wrong!");
        });
}
$(document).on("click", "#btn_transfer_sent", function () {
    var fiveMinutes = 60 * 5,
        display = document.querySelector('#transfer_request_time');
    startTimer(fiveMinutes, display);
    $(".all-transfer-payment-form").addClass("d-none");
    $("#transfer_confirmation_wait_section").removeClass("d-none");
});
function startTimer(duration, display) {
   var timer = duration, minutes, seconds;
    setInterval(function () {
        minutes = parseInt(timer / 60, 10)
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (--timer < 0) {
        }
    }, 1000);
}
//--------------------------------------------------------- close payment process function ----------------------------------------------------------------------------
$(document).on("click", ".link-cancel-payment", function () {
    window.parent.postMessage({ message: "close" }, client);
});
//--------------------------------------------------------- retry payment confirmation process function ----------------------------------------------------------------------------
$(document).on("click", ".btn-retry-payment", function () {
    paymentChannel = "transfer";
    var fiveMinutes = 60 * 5,
        display = document.querySelector('#transfer_request_time');
    startTimer(fiveMinutes, display);
    $(".all-transfer-payment-form").addClass("d-none");
    $("#transfer_confirmation_wait_section").removeClass("d-none");
    var obj = {};
    obj.paymentreference = paymentReference;
    obj.paymentchannel = "transfer";
    var jsonData = JSON.stringify(obj);
    $.ajax({
        type: 'POST',
        contentType: 'application/json charset=utf-8',
        url: '/popup/completepayment',
        data: jsonData,
        dataType: 'json',
        success: function (response) {
            $("#btn_create_payment_link").text("Create").removeAttr("disabled");
            if (response.isSuccess == true) {
                $(".general-popup-section").addClass("d-none");
                $(".transaction-success-section").removeClass("d-none");
            }
            else {
                $(".general-popup-section").addClass("d-none");
                $(".transaction-fail-section").removeClass("d-none");
            }
        },
        error: function (error) {
            $(".general-popup-section").addClass("d-none");
            $(".transaction-fail-section").removeClass("d-none");
        },
        failure: function (failure) {
            $(".general-popup-section").addClass("d-none");
            $(".transaction-fail-section").removeClass("d-none");
        }
    })
});

