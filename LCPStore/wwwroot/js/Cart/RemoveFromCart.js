
$('.trash').on('click', function () {
    var getItemId = parseInt($(this).closest('tr').prop('id'));
    $.ajax({
        type: 'POST',
        url: '/CartItems/Delete',
        data: {
            id: getItemId,
        },
        success: function (data) {
            document.getElementById(getItemId).remove();
            $(document.getElementById('total_cost')).text("$" + data + ",00");
        },
        error: function (data) {
            alert("Can't delete this cart item");
            console.log(data);
        },
        complete: function (data) {
        },
    });
});


