angular.module("FilterM", [])
.filter("LongTaiwanDate", function () {
    return function (date, hasBlank) {
        if (date == null || date == "" || date == undefined || date == '0001-01-01T00:00:00')
            return "";
        var d = new Date(date);
        if (hasBlank) {
            return "民國 " + (d.getFullYear() - 1911) + " 年 " + (d.getMonth() + 1) + " 月 " + d.getDate() + " 日";
        }
        return "民國" + (d.getFullYear() - 1911) + "年" + (d.getMonth() + 1) + "月" + d.getDate() + "日";
    };
})
.filter("ShortTaiwanDate", function () {
    return function (date) {
        if (date == null || date == "" || date == undefined || date == '0001-01-01T00:00:00')
            return "";
        var d = new Date(date);
        return (d.getFullYear() - 1911) + "/" + (d.getMonth() + 1) + "/" + d.getDate();
    };
})
.filter("LongTaiwanTime", function () {
    return function (date) {
        if (date == null || date == "" || date == undefined || date == '0001-01-01T00:00:00')
            return "";
        var d = new Date(date);
        return (d.getFullYear() - 1911) + "/" + (d.getMonth() + 1) + "/" + d.getDate() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();
    };
})
.filter("SimpleTaiwanDate", function () {
    return function (date) {
        if (date == null || date == "" || date == undefined || date == '0001-01-01T00:00:00')
            return "";
        var d = new Date(date);
        return (d.getFullYear() - 1911) + ("0" + (d.getMonth() + 1).toString()).slice(-2) + ("0" + d.getDate().toString()).slice(-2);
    };
});




