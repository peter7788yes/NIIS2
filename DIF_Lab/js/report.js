var pageSize = 1;
$(function () {
    $('.tHeader').html($('#tHeader').html());
    $('.pageSize').val(function () {
        $(this).html(pageSize);
        pageSize++;
    })
})
