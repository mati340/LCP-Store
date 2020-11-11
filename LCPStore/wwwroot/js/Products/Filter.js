
 $('.site-btn').on('click', function (Model) {
        
     var min = $('#minamount').val();
     var max = $('#maxamount').val();
     var cat = location.href.substr(location.href.lastIndexOf('/') + 1);

     $.ajax({

         url: '/Categories/SearchByPriceAndCategory',
         data:
            {
                minamount: min,
                maxamount: max,
                category: cat
         },
         dataType: "JSON",
         success: function (data) {
             $('#results').tmpl(data).appendTo('#tbody');
         },
         error: function (data) {
                alert("Noo");
                console.log(data);
         },

     });

 });

