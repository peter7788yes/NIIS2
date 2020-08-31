angular.module("FilterM", [])
.filter("LongTaiwanDate", function () {
    return function (date,hasBlank) {
        var d = new Date(date);
        if (hasBlank){
            return "民國 " + (d.getFullYear() - 1911) + " 年 " + (d.getMonth() + 1) + " 月 " + d.getDate() + " 日";
        }
        return "民國" + (d.getFullYear() - 1911) + "年" + (d.getMonth() + 1) + "月" + d.getDate() + "日";
    };
})
.filter("ShortTaiwanDate", function () {
    return function (date) {
        var d = new Date(date);
        return (d.getFullYear() - 1911) + "/" + (d.getMonth() + 1) + "/" + d.getDate();
    };
});



