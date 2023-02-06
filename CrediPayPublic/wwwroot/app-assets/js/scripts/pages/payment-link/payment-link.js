 // global value declaration
var additionalInformationArray = [];
var additionalInformationItems = $(".paymentlink-additional-information");
var paymentLinkType = 0;
var collectPhoneNumber = false;
var subAccountNumber = "";
var subAccountNumberRemove = "";
var subAccountNumberText = "";
var subChargeType = 1;
var subChargeTypeText = "";
var subChargeValue = "";
var image = "";
var subAccountNumberArray = [];
var subAccountNumberOnlyArray = [];
 // jQuery validation function
$(function () {
    "use strict";
    var createPaymentLinkForm = $(".create-new-payment-link"),
        createSubAccountForm = $(".create-sub-account-form"),
        select = $('.select2');

    // select2
    select.each(function () {
        var $this = $(this);
        $this.wrap('<div class="position-relative"></div>');
        $this
            .select2({
                placeholder: 'Select value',
                dropdownParent: $this.parent()
            })
            .change(function () {
                $(this).valid();
            });
    });
    createPaymentLinkForm.validate({
        rules: {
            "paymentlink_name": { required: true },
            "paymentlink_amount": { required: true },
        }
    });
    createSubAccountForm.validate({
        rules: {
            "sub_account_number": { required: true },
            "sub_charge_type": { required: true },
        }
    });
});
// show and hide advance options
$("#hide_advanced_options").click(function () {
    $(".both-advanced-btn").addClass("d-none");
    $("#show_advanced_options").removeClass("d-none");
    $("#advanced_options_section").addClass("d-none");
});
$("#show_advanced_options").click(function () {
    $(".both-advanced-btn").addClass("d-none");
    $("#hide_advanced_options").removeClass("d-none");
    $("#advanced_options_section").removeClass("d-none");
});
 // add sub account function
$("#add_new_sub_account").click(function () {
    
    if (subChargeType == 1)
        subChargeValue = $("#sub_charge_value_percentage").autoNumeric('get');
    else 
        subChargeValue = $("#sub_charge_value_flat").autoNumeric('get');
    if (subChargeValue == "") {
        toastr.error("Charge value can not be empty", 'Oops!', {
            closeButton: true,
            tapToDismiss: false
        });
        return;
    };
    subAccountNumber = $('#sub_account_number').find(':selected').val();
    subAccountNumberText = $('#sub_account_number').find(':selected').text();
    subChargeType = parseInt($('#sub_charge_type').find(':selected').val());
    subChargeTypeText = $('#sub_charge_type').find(':selected').text();
    if ($.inArray(subAccountNumber, subAccountNumberOnlyArray) !== -1) {
        toastr.error("Account already selected", 'Oops!', {
            closeButton: true,
            tapToDismiss: false,
        });
        return;
    }
    var tr = "<tr>" +
        "<td data-id='" + subAccountNumber + "' class='sub-account-number'> " + subAccountNumberText + "</td>" +
        "<td data-id='" + subChargeType + "' class='sub-charge-type'> " + subChargeTypeText + "</td>" +
        "<td class='sub-charge-value'> " + subChargeValue + "</td>" +
        "<td><button data-id='" + subAccountNumber + "' type='button' class='btn btn-flat-danger remove-sub-row'><i data-feather='trash'></i></button></td>" +
        "</tr>";
    $("#add_sub_account_tbody").append(tr);
    feather.replace({ width: 14, height: 14 });
    subAccountNumberArray.push({
        subAccountId: subAccountNumber,
        splitOptions: {
            chargeType: parseInt(subChargeType),
            value: parseFloat(subChargeValue.replace(/,/g, ''))
        }
    });
    subAccountNumberOnlyArray.push(subAccountNumber);
});
 // remove sub account function
$(document).on("click", ".remove-sub-row", function () {
    subAccountNumberRemove = $(this).data("id");
    var removerow = $(this).closest("tr");
    removerow.remove();
    subAccountNumberArray.splice($.inArray(subAccountNumberRemove, subAccountNumberArray), 1);
    subAccountNumberOnlyArray.splice($.inArray(subAccountNumberRemove, subAccountNumberOnlyArray), 1);
});
// next to add sub account modal from payment link modal function
//$(document).on("click", "#next_to_add_sub_account", function () {
//    $("#modalCreatePaymentLink").modal("hide");
//    $("#modalAddSubAccount").modal("show");
//});
 // back to payment link modal from sub account modal function
$(document).on("click", "#back_to_paymentlink_form", function () {
    var removerows = $("#add_sub_account_tbody").html();
    removerows.remove();
    subAccountNumberArray = [];
    $("#modalAddSubAccount").modal("hide");
    $("#modalCreatePaymentLink").modal("show");
});
 // cancel payment link creation function
$(document).on("click", "#cancel_payment_link_creation", function () {
    $("form.create-new-payment-link :input").each(function () {
       $(this).val("");
    });
    $("#modalCreatePaymentLink").modal("hide");
});
 // select payment link type function
$(document).on("click", ".select-payment-link-type", function () {
    paymentLinkType = parseInt($(this).data('type'));
    $("#modalPaymentLinkType").modal("hide");
    $("#modalCreatePaymentLink").modal("show");
});
// select charge type function
$(document).on("change", "#sub_charge_type", function () {
    subChargeType = $("#sub_charge_type").val();
    if (subChargeType == 1) {
       // subChargeValue = $("#sub_charge_value_percentage").autoNumeric('get');
        $(".both-charge-type-input").addClass("d-none");
        $("#sub_charge_value_percentage").removeClass("d-none");
    }
    else {
        //subChargeValue = $("#sub_charge_value_flat").autoNumeric('get');
        $(".both-charge-type-input").addClass("d-none");
        $("#sub_charge_value_flat").removeClass("d-none");
    };
});
 // create payment link function
$(document).on("click", "#btn_create_payment_link", function () {
    var isfilled = $(".create-new-payment-link").valid();
    if (isfilled == false) return;
    var name = $('#paymentlink_name').val();
    var amount = $('#paymentlink_amount').val();
    var description = $('#paymentlink_description').val();
    var alias = $('#paymentlink_alias').val();
    var sendNotificationTo = $('#paymentlink_send_notification_to').val();
    if ($('#collect_phone_number').is(':checked')) {
        collectPhoneNumber = true;
    } else { collectPhoneNumber = false; };
    for (var i = 0; i < additionalInformationItems.length; i++) {
        additionalInformationArray.push($(additionalInformationItems[i]).val());
    }
    $(this).attr('disabled', 'disabled').html('<span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span><span class= "ms-25 align-middle"> Creating...</span >');
    var obj = {};
    obj.paymentlinktype = parseInt(paymentLinkType);
    obj.pagename = name;
    obj.amount = parseFloat(amount.replace(/,/g, ''));
    obj.description = description;
    obj.image = image;
    obj.collectcustomerphone = collectPhoneNumber;
    obj.alias = alias;
    obj.sendnotification = sendNotificationTo;
    obj.additionalinformation = additionalInformationArray.toString();
    obj.splitpaymentoptions = subAccountNumberArray;
    var jsonData = JSON.stringify(obj);
    $.ajax({
        type: 'POST',
        contentType: 'application/json charset=utf-8',
        url: '/paymentlink/createpaymentlink',
        data: jsonData,
        dataType: 'json',
        success: function (response) {
            $("#btn_create_payment_link").text("Create").removeAttr("disabled");

            if (response.status == true) {
                toastr.success(response.message, 'Success!', {
                    closeButton: true,
                    tapToDismiss: false
                });
            }
            else
                toastr.error(response.message, 'Oops!', {
                    closeButton: true,
                    tapToDismiss: false,
                });

        },
        error: function (error) {
            $("#btn_create_payment_link").text("Create").removeAttr("disabled");
            toastr.error('Something went wrong, try again.', 'Oops!', {
                closeButton: true,
                tapToDismiss: false,
            });
        },
        failure: function (failure) {
            $("#btn_create_payment_link").text("Create").removeAttr("disabled");
            toastr.error('Cannot connect to server due to poor internet connection.', 'Oops!', {
                closeButton: true,
                tapToDismiss: false,
            });
        }
    })
});
 // push additional information into array function
function pushAdditionalInformation() {
    additionalInformationItem = $(this).val();
    additionalInformationArray.push(additionalInformationItem);
}