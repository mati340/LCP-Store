$(function () {


    var p = $('#productId').val();
    var q = $('#quantity').val(); 


    $('#AddToCart').submit(function () {

            // שליחת בקשת Ajax
            $.ajax({
                url: "/Carts/AddToCart", // מיקום הקובץ אליו תשלח הבקשה
                data: {
                    productId: p,
                    quantity: q
                }, // איזה מידע לשלוח לקובץ
                success: function (callback) { // ברגע שהבקשה נשלחה והצליחה , קבלה של נתונים חזרה - callback
                    console.log(callback);
                }
            });
            return false;
        }
        
    );
})