
//jQuery.noConflict();
$(document).ready(function () {

    $("#searchBox").autocomplete({
        source: "/Products/SearchAuto",
        minLength: 2,
        //data: {},
        //change: function (item) {
        //    console.log(data);
        //    $('#results').tmpl(item.label).appendTo('#tbody');
        //},
        select: function (event, ui) {
            location.href = '/Products/ProductDetails/' + ui.item.id;
        }
    });

    $('#loader').hide();
    $(document).ajaxStart(function () {
        $('#tbody').empty();
        $('#loader').show();
    }).ajaxStop(function () {
        $('#loader').hide();
    });
});
