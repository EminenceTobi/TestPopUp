var amount = 0;
var email = "";
var firstName = "";
var lastName = "";
var phone = "";
var subAccountNumberArray = [];
var logo = "";
var client = "";
var paymentReference = "";
var key = "";
//var appUrl = "https://pay.credipay.io/"
var appUrl = "https://localhost:7001/"
function call() {
    $.blockUI({
        message: '<img src="/app-assets/images/loading.gif" width="40" />',
        timeout: 0,
        css: {
            backgroundColor: 'transparent',
            border: '0'
        },
        overlayCSS: {
            backgroundColor: '#000',
            opacity: 0.9
        }
    });
    var vars = getUrlVars()["credipaypopcall"];
    if (vars == "" || vars == "undefined") {
        error("Invalid API initialization");
        return
    }
    else {
        var decodedvars = b64DecodeUnicode(vars);
        var jsonvars = JSON.parse(decodedvars);
        client = jsonvars.client;
        if (jsonvars.email == "") {
            callError("Invalid email address");
            return
        }
        if (validateEmail(jsonvars.email) == false) {
            callError("Invalid email address");
            return
        }
        if (jsonvars.ref == "") {
            callError("Invalid payment reference");
            return
        }
        if (isNaN(jsonvars.amount)) {
            callError("Invalid Amount");
            return
        }
        if (jsonvars.amount % 1 != 0) {
            callError("Invalid Amount");
            return
        }
        if (jsonvars.key == "") {
            callError("Invalid API Key");
            return
        }
        if (jsonvars.splitObj != []) {
            for (var item in jsonvars.splitObj) {
                subAccountNumberArray.push({
                    subAccountId: item.subAccountNumber,
                    splitOptions: {
                        chargeType: item.subChargeType,
                        value: item.subChargeValue
                    }
                });

            }
        }
        if (jsonvars.key.indexOf("pk_test") > -1) {
            $('.is-test-divider').removeClass('d-none');
        }
        firstName = jsonvars.firstName;
        lastName = jsonvars.lastName;
        phone = jsonvars.phone;
        amount = jsonvars.amount;
        email = jsonvars.email;
        paymentReference = jsonvars.ref;
        key = jsonvars.key;
        var obj = {};
        obj.amount = jsonvars.amount;
        obj.email = jsonvars.email;
        obj.key = jsonvars.key;
        obj.splitpaymentoptions = subAccountNumberArray;
        var jsonData = JSON.stringify(obj);
        $.ajax({
            type: 'POST',
            contentType: 'application/json charset=utf-8',
            url: '/popup/validatepayment',
            data: jsonData,
            dataType: 'json',
            success: function (response) {
                if (response.isSuccess == true) {
                    $(".customer-email").text(jsonvars.email);
                    $(".transaction-amount").text("NGN " + parseFloat(amount / 100).toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                    checkIfLogoExists(response.value, (exists) => {
                        if (exists) {
                            // Success code
                            $(".merchant-logo").attr("src", response.value);

                        } else {
                            // Fail code
                            $(".merchant-logo").attr("src", "/app-assets/images/credipay.png");
                        }
                    });
                    $.unblockUI();
                }
                else {
                    callError(response.message);
                    $.unblockUI();
                };
            },
            error: function (error) {
                callError("Oops!, Something went wrong, try again.");
                $.unblockUI();
            },
            failure: function (failure) {
                callError("Oops!, Something went wrong, try again.");
                $.unblockUI();
            }
        })
    };
}
function initiatePaymentTransfer() {
    $("#virtual_account_section").block({
        message: '<img src="/app-assets/images/loading.gif" width="30" />',
        timeout: 0,
        css: {
            backgroundColor: 'transparent',
            border: '0'
        },
        overlayCSS: {
            backgroundColor: '#fff',
            opacity: 0.9
        }
    });
    var obj = {};
    obj.amount = amount;
    obj.firstname = firstName;
    obj.lastname = lastName;
    obj.phone = phone;
    obj.paymentreference = paymentReference;
    obj.paymentchannel = "transfer";
    obj.email = email;
    obj.key = key;
    obj.splitpaymentoptions = subAccountNumberArray;
    var jsonData = JSON.stringify(obj);
    $.ajax({
        type: 'POST',
        contentType: 'application/json charset=utf-8',
        url: '/popup/initiatepayment',
        data: jsonData,
        dataType: 'json',
        success: function (response) {
            if (response.isSuccess == true) {
                var responseValue = response.value;
                $("#transfer_account_name").text(responseValue.accountInfo.accountName);
                $("#transfer_bank_name").text(responseValue.accountInfo.bankName);
                $("#transfer_account_number").text(responseValue.accountInfo.accountNumber);
                $('#btn_copy_account_number').data('id', responseValue.accountInfo.accountNumber);
                $(".btn-retry-payment").data('id', paymentReference);
                $(".transaction-amount").text("NGN " + parseFloat(responseValue.amount / 100).toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                feather.replace({ width: 14, height: 14 });
                $("#virtual_account_section").unblock();
                return paymentReference;
            }
            else {
                $("#virtual_account_section").unblock();
                callError(response.message);

            };
        },
        error: function (error) {
            $("#virtual_account_section").unblock();
            callError("Oops!, Something went wrong, try again.");
        },
        failure: function (failure) {
            $("#virtual_account_section").unblock();
            callError("Oops!, Something went wrong, try again.");
        }
    })
}
$(document).ready(function () {
    call();
    $(".transaction-amount").text("NGN " + parseFloat(amount / 100).toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
});
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');

        if ($.inArray(hash[0], vars) > -1) {
            vars[hash[0]] += "," + hash[1];
        }
        else {
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
    }
    return vars;
}
function callError(msg) {
    $(".error-message-text").text(msg);
    $(".general-popup-section").addClass("d-none");
    $(".error-message-section").removeClass("d-none");
}
function validateUniqueRef(ref) {
    return true
}
function loadIframe() {
}
function b64EncodeUnicode(str) {
    // first we use encodeURIComponent to get percent-encoded UTF-8,
    // then we convert the percent encodings into raw bytes which
    // can be fed into btoa.
    return btoa(encodeURIComponent(str).replace(/%([0-9A-F]{2})/g,
        function toSolidBytes(match, p1) {
            return String.fromCharCode('0x' + p1);
        }));
}
function b64DecodeUnicode(str) {
    // Going backwards: from bytestream, to percent-encoding, to original string.
    return decodeURIComponent(atob(str).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
}
function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
function checkIfLogoExists(url, callback) {
    const img = new Image();
    img.src = url;

    if (img.complete) {
        callback(true);
    } else {
        img.onload = () => {
            callback(true);
        };

        img.onerror = () => {
            callback(false);
        };
    }
}