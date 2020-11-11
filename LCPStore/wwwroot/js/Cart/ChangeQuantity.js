$(document).ready(function () {
    $("a.dec").on("click",function () {
        var getItemId = parseInt($(this).closest('tr').prop('id'));
        var quantity = parseInt($(this).closest('div').find('input').prop('value'));
        var element = this;
        if (quantity == 0) {
            return;
        }
        $.ajax({
            type: 'POST',
            url: '/Carts/Minus',
            data: {
                id: getItemId,
            },
            success: function (data) {
                $(element).closest('tr').find("td.total-col").find('h4').text("$" + data[0] + ",00");

                $(element).closest('tr').find('input').val("" + (quantity - 1)); 

                $(document.getElementById('total_cost')).text("$" + data[1] + ",00");
            },
            error: function (data) {
                alert("Can't Change this cart item");
                console.log(data);
            },
            complete: function (data) {
            },
        });
    });
});

$(document).ready(function () {
    $("a.inc").on("click",function () {
        var getItemId = parseInt($(this).closest('tr').prop('id'));
        var quantity = parseInt($(this).closest('div').find('input').prop('value'));
        var element = this;

        $.ajax({
            type: 'POST',
            url: '/Carts/Plus',
            data: {
                id: getItemId,
            },
            success: function (data) {
                $(element).closest('tr').find("td.total-col").find('h4').text("$" + data[0] + ",00");

                $(element).closest('tr').find('input').val("" + (quantity + 1)); 

                $(document.getElementById('total_cost')).text("$" + data[1] + ",00");
            },
            error: function (data) {
                alert("Can't Change this cart item");
                console.log(data);
            },
            complete: function (data) {
            },
        });
    });
});

function onChangefun(textbox, prodId) {
    var diff;
    if (textbox.value == "") {
        diff = -parseInt(textbox.oldvalue);
    }
    else {
        if (textbox.oldvalue == "") {
            textbox.oldvalue = 0;
        }
        diff = parseInt(textbox.value) - parseInt(textbox.oldvalue)
    }
    $.ajax({
        type: 'POST',
        url: '/CartItems/Update',
        data: {
            id: prodId,
            quantity: diff,
        },
        success: function (data) {
            $(textbox).closest('tr').find(".total-col").find('h4').text("$" + data[0] + ",00");

            $(document.getElementById('total_cost')).text("$" + data[1] + ",00");
        },
        error: function (data) {
            alert("Can't delete this cart item");
            console.log(data);
        },
        complete: function (data) {
        },
    });
}



    //function minusNumber() {
    //    console.log($(this).closest('.quantity').find('input').val());
    //    $(this).parent().find(".quantity").val(function (i, oldval) {
    //        if (oldval == 0) {
    //            return oldval;
    //        } else {
    //    $(this).closest("tr").find("td .totalPriceProduct").text(function (i, oldval) {
    //        oldval = oldval.split(",");
    //        oldval = oldval[0].substring(1);

    //        var itemPrice = $('#itemPrice').text();
    //        itemPrice = itemPrice.split(",");
    //        itemPrice = itemPrice[0].substring(1);

    //        return '$' + (oldval - itemPrice) + ',00';
    //    });

    //            $('#totalPriceAll').text(function (i, oldval) {
    //    oldval = oldval.split(",");
    //                oldval = oldval[0].substring(1);

    //                var itemPrice = $('#itemPrice').text();
    //                itemPrice = itemPrice.split(",");
    //                itemPrice = itemPrice[0].substring(1);

    //                return '$' + (oldval - itemPrice) + ',00';
    //            });
    //            return oldval-1;
    //        }
    //    });
    //}