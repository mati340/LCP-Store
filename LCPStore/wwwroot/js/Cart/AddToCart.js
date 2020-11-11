
$('form#AddCartForm').submit(function (event) {
    event.preventDefault()
    var p = $('#productId').val();
    var q = $('#quantity').val();
    // שליחת בקשת Ajax
    $.ajax({
        url: "/Carts/AddToCart", // מיקום הקובץ אליו תשלח הבקשה
        type: "POST",
        data: {
            productId: p,
            quantity: q,
        }, // איזה מידע לשלוח לקובץ
        success: function (callback) { // ברגע שהבקשה נשלחה והצליחה , קבלה של נתונים חזרה - callback
            if (callback == false) {
                location.href = '/Accounts/Login/';
            }
            else
                alert("The product added to cart successfully");
        },
        error: function (callback) {
            alert("Can't add this product to cart");
        },
    });

});


$('a.add-card').on("click", function (event) {
    event.preventDefault()
    var p = parseInt($(this).closest('div').prop('id'));

    // שליחת בקשת Ajax
    $.ajax({
        url: "/Carts/AddToCart", // מיקום הקובץ אליו תשלח הבקשה
        type: "POST",
        data: {
            productId: p,
        }, // איזה מידע לשלוח לקובץ
        success: function (callback) { // ברגע שהבקשה נשלחה והצליחה , קבלה של נתונים חזרה - callback
            if (callback == false) {
                location.href = '/Accounts/Login/';
            }
            else
                alert("The product added to cart successfully");
        },
        error: function (callback) {
            alert("Can't add this product to cart");
        },
    });

});