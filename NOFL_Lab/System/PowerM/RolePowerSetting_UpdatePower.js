$(function () {
    var myLi = "";
    var lastNode = {};
    $.each(data, function (index, item) {
        //var th_or_td_start = "<td>";
        //var th_or_td_end = "</td>";
       
        myTr = "";
        if (item.PID == 0) {
            myTr = '<tr id="x' + item.ID + '" level="0" pid="'+item.PID+'">' + '<td>' + item.ModuleName + '</td></tr>';
            //myTr = '<tr id="x' + item.ID + '" level="0" >' + th_or_td_start + item.ModuleName + th_or_td_end +'</tr>';
            $('#tableRoot tr:last').after(myTr);
            var $itemNode = $("#x" + item.ID);
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="1" class="changeParent"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="2"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="3"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="4"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="5"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="6"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="7"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="8"></td>');
            $itemNode.append('<td class="aCenter"><input type="button" value="全選" class="allBtn" /><input type="button" value="全不選" class="disAllBtn"/></td>');
          
        }
        else {
           
           

            var $parent = $("#x" + item.PID);
            var $parentNext = $("#x" + item.PID).next();
           

            var blank = "";
            var level = parseInt($parent.attr("level")) + 1;
            //console.log(level);

            //if (level < 2) {
            //    th_or_td_start = "<th>";
            //    th_or_td_end = "</th>";
            //}

            for (var i = 0; i <= level; i++) {
                blank += "&nbsp;&nbsp;&nbsp;";
            }

            if ($parentNext.hasClass("trChildren") == false) {
                //console.log(item.ID +"_"+ 1);
                $("#x" + item.PID).after('<tr id="x' + item.ID + '" class="trChildren" level="' + level +'" pid="'+item.PID+'">' + '<td>' + blank + item.ModuleName + '</td></tr>');
                //$("#x" + item.PID).after('<tr id="x' + item.ID + '" class="trChildren" level="' + level + '">' + th_or_td_start + blank + item.ModuleName + th_or_td_end + '</tr>');
            }
            else
            {
                //console.log(item.ID + "_" + 2);
                //console.log(lastNode.ID);
                $("#x" + lastNode.ID).after('<tr id="x' + item.ID + '" class="trChildren" level="' + level +'" pid="'+item.PID+'">'+'<td>' + blank + item.ModuleName + '</td></tr>');
                //$("#x" + lastNode.ID).after('<tr id="x' + item.ID + '" class="trChildren" level="' + level + '">' + th_or_td_start + blank + item.ModuleName + th_or_td_end + '</tr>');
            }
            var $itemNode = $("#x" + item.ID);
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="1" class="changeParent"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="2"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="3"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="4"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="5"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="6"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="7"></td>');
            $itemNode.append('<td class="aCenter"><input type="checkbox" value="8"></td>');
            $itemNode.append('<td class="aCenter"><input type="button" value="全選" class="allBtn" /><input type="button" value="全不選"/ class="disAllBtn"></td>');
        }

        lastNode = item;
    });

    
    $(document).on("click", ".allBtn", function (e) {
        //$(this).parents("tr:first").find(":checkbox").prop("checked", true);
        $(this).closest("tr").find(":checkbox").prop("checked", true);
        e.preventDefault();
        return false;
    });

    $(document).on("click", ".disAllBtn", function (e) {
        //$(this).parents("tr:first").find(":checkbox").prop("checked", false);
        $(this).closest("tr").find(":checkbox").prop("checked", false);
        e.preventDefault();
        return false;
    });

  

    $(document).on("change", ".changeParent", function (e) {
        var $parent = $(this).closest("tr");

        if ($(this).prop("checked")) {
    
            var level = parseInt($parent.attr("level"));
            $parent.find(":checkbox:first").prop("checked", true);

            level = level - 1;

            var avoidLoopFlag = 10;
            while (level > 0 && avoidLoopFlag > 0)
            {
                $parent = $("#x"+$parent.attr("pid"))
                level = parseInt($parent.attr("level"));
                if (level == 0)
                {
                    avoidLoopFlag = 0;
                }
                $parent.find(":checkbox:first").prop("checked", true);

                avoidLoopFlag = avoidLoopFlag - 1;
            }
            
        }
       
        e.preventDefault();
        return false;
    });

    $(document).ajaxStart(function () {
        $("#loading").show();
    }).ajaxComplete(function () {
        $("#loading").hide();
    });

    $(document).on("click", "#saveBtn", function (e) {
        var $allTr = $("#tableRoot tr:not(:first)");
        var total = [];
       
        
        $.each($allTr, function (index, item) {
            var r = {};
            var hasValue = false;
            var allCb = $(item).find(":checkbox");
            $.each(allCb, function (index, item) {
                var value = $(item).prop("checked") ? $(item).val() : 0;
                if(value>0)
                {
                    hasValue = true;
                    r["r" + (index + 1)] = value;
                }
            });
            if (hasValue) {
                r.id = $(item).attr("id").substring(1);
                total.push(r);
            }
        });

        //console.log(JSON.stringify(total));

        //$("#myValue").val(JSON.stringify(total));

            
        

            var url = "/System/PowerM/RolePowerSetting_AddPowerOP.aspx";
            var postData = {};
            postData.v = JSON.stringify(total);
            //ajax
            $.ajax({
                cache: false,
                type: "POST",
                url: url,
                data: postData
            })
            .done(function (data) {
                if (data > 0)
                {
                    alert("儲存成功");
                    sessionStorage.removeItem("tbName");
                    sessionStorage.removeItem("tbDesp");
                    essionStorage.removeItem("rbLevel");
                    location.href = "/System/PowerM/RolePowerSetting.aspx";
                }
                else
                {
                    alert("儲存失敗");
                }
            })
            .fail(function (jqXHR, textStatus) {

            });

        e.preventDefault();
        return false;
    });

    $(document).on("click", "#lastBtn", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });

});