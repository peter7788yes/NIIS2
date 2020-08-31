<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckLog.ascx.cs" Inherits="UC_CheckLog" %>
<div class="list01">
            <ul id="myUl" style="display:none;">
              <li id="createrLi">
                  <span>建立者：</span><label id="creater"></label>
              </li>
            </ul>
</div>
<script>
    var ToShortTaiwanTime = function (date, minutesOffset) {
        minutesOffset = minutesOffset || 0;
        if (date == null || date == "" || date == undefined || date == '0001-01-01T00:00:00')
            return "";
        var d = new Date(date);
        d.setMinutes(d.getMinutes() + minutesOffset);
        return (d.getFullYear() - 1911) + ("0" + (d.getMonth() + 1).toString()).slice(-2) + ("0" + d.getDate().toString()).slice(-2) + " " + ("0" + d.getHours().toString()).slice(-2) + ":" + ("0" + d.getMinutes().toString()).slice(-2) + ":" + ("0" + d.getSeconds().toString()).slice(-2);
    };
    var getLog = function () {
                var postData = {};
                postData['o'] = "<%=jsonString%>";
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "/Ashx/CheckLogOP.ashx",
                    data: postData
                })
               .done(function (data) {
                   if (data.chk) {
                   
                       $("#creater").text(data.msg1.UN == null ? "" : data.msg1.UN + "-" + data.msg1.ON + " " + ToShortTaiwanTime(data.msg1.CD, -480));
                       $.each(data.msg2, function (index, item) {
                           var data = "<li><span>異動者：</span>";
                           data += "<label>" + item.UN + "-" + item.ON + " " + ToShortTaiwanTime(item.CD, -480) + "</label>";
                           data +=  "</li>";
                           $("#myUl").append(data);
                       });
                       $('#myUl').show();
                   }
               });
    };
    setTimeout(function () {
        
        if (jQuery) {
            getLog();
        } else {
            var url = "/js/jq/jquery-2.1.4.js";
            $.getScript(url, function () {

                getLog();
            });

        }
    }, 1000);
</script>