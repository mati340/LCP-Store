
//$(document).ready(function () {
//    $('#delete-form').submit(function (event) {
//        event.preventDefault();
//        var item = $('input#trash').val;
//        $.ajax({
//            url: "/Carts/Delete", // מיקום הקובץ אליו תשלח הבקשה
//            type: "POST",
//            data: {
//                id: item
//            }, // איזה מידע לשלוח לקובץ
//            success: function (callback) { // ברגע שהבקשה נשלחה והצליחה , קבלה של נתונים חזרה - callback
//                alert("yayy");
//                console.log(callback);
//            },
//            error: function (callback) {
//                alert("noo");
//            },
//        });
//    });
//});

$('.trash').on('click', function () {
    var getItemId = parseInt($(this).closest('tr').prop('id'));
    //var getUserName = $('.divName').html();
    $.ajax({
        type: 'POST',
        url: 'CartItems/Delete',
        data: {
            id: getItemId,
        },
        success: function (data) {
            document.getElementById(getItemId).remove();
            $(document.getElementById('total_cost')).text("$" + data + ",00");
        },
        error: function (data) {
            alert("Noo");
            console.log(data);
        },
        complete: function (data) {
        },
    });
});


//$(function () {

//    $('#AddToCart').submit(function () {
//    var p = $('#productId').val();
//    var q = $('#quantity').val(); 

//            // שליחת בקשת Ajax
//            $.ajax({
//                url: "/Carts/AddToCart", // מיקום הקובץ אליו תשלח הבקשה
//                type: "POST",
//                data: {
//                    productId: p,
//                    quantity: q
//                }, // איזה מידע לשלוח לקובץ
//                success: function (callback) { // ברגע שהבקשה נשלחה והצליחה , קבלה של נתונים חזרה - callback
//                    console.log(callback);
//                }
//            });
//            return false;
//        }
        
//    );
//})


//$('a#AddToCartOut').click(function (event) {
//    event.preventDefault()
//    var p = $('#productId').val();

//    // שליחת בקשת Ajax
//    $.ajax({
//        url: "/Carts/AddToCart", // מיקום הקובץ אליו תשלח הבקשה
//        type: "POST",
//        data: {
//            productId: p,
//        }, // איזה מידע לשלוח לקובץ
//        success: function (callback) { // ברגע שהבקשה נשלחה והצליחה , קבלה של נתונים חזרה - callback
//            alert("yayy");
//            console.log(callback);
//        },
//        error: function (callback) {
//            alert("noo");
//        },
//    });

//})