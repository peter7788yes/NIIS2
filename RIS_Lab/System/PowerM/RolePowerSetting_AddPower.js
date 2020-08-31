$(function () {
    var myLi = "";
    var lastNode = {};
    $.each(data, function (index, item) {
        //var th_or_td_start = "<td>";
        //var th_or_td_end = "</td>";

        myTr = "";
        if (item.PID == 0) {
            myTr = '<tr id="x' + item.ID + '" haschildrens="0" class="pinkcolor" level="0" pid="' + item.PID + '">' + '<td>' + item.ModuleName + '</td></tr>';
            //myTr = '<tr id="x' + item.ID + '" level="0" >' + th_or_td_start + item.ModuleName + th_or_td_end +'</tr>';
            $('#tableRoot tr:last').after(myTr);
            var $itemNode = $("#x" + item.ID);
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="2" class="changeParent"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="2"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="3"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="4"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="5"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="6"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="7"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="8"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="9"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="10"></td>');
            for (var i = 2; i <= 10; i++) {
                $itemNode.append('<td class="aCenter"><input type="checkbox" value="' + i + '"></td>');
            }
            $itemNode.append('<td class="aCenter"><input type="button" value="全選" class="allBtn btn" /><input type="button" value="全不選" class="disAllBtn btn"/></td>');

        }
        else {



            var $parent = $("#x" + item.PID);
            var haschildrens = parseInt($parent.attr("haschildrens"));
            var $parentNext={};
            if (haschildrens > 0) {
                $parentNext=$parent.next();
                for (var i = 1; i <= haschildrens-1; i++)
                {
                    $parentNext = $parentNext.next();
                }
                //console.log($parentNext[0]);
            }
            else {
                $parentNext = $parent;
            }


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
            if (level >= 2)
                blank += "&nbsp;&nbsp;&nbsp;";
            
            var myClass="";
            if (level == 1)
                myClass="yellowcolor"
            $parentNext.after('<tr class="' + myClass + '" id="x' + item.ID + '" haschildrens="0" level="' + level + '" pid="' + item.PID + '">' + '<td>' + blank + item.ModuleName + '</td></tr>');
            $("#x" + item.PID).attr("haschildrens", haschildrens+1);

            //$("#x" + item.PID).after('<tr id="x' + item.ID + '"  level="' + level + '" pid="' + item.PID + '">' + '<td>' + blank + item.ModuleName + '</td></tr>');
              
            var $itemNode = $("#x" + item.ID);
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="2" class="changeParent"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="2"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="3"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="4"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="5"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="6"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="7"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="8"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="9"></td>');
            //$itemNode.append('<td class="aCenter"><input type="checkbox" value="10"></td>');
            for (var i = 2; i <= 10; i++) {
                $itemNode.append('<td class="aCenter"><input type="checkbox" value="' + i + '"></td>');
            }
            $itemNode.append('<td class="aCenter"><input type="button" value="全選" class="allBtn btn" /><input type="button" value="全不選"/ class="disAllBtn btn"></td>');
        }

        lastNode = item;
    });

    var level0_IndexAry = [];
    $.each($("tr[level='0']"), function (index, item) {
        level0_IndexAry.push($(item).index());
        //console.log(level0_IndexAry);
    });
    level0_IndexAry.push($("#tableRoot>tbody>tr").length - 1);
    //console.log(level0_IndexAry);
    $(document).on("change", "tr[level='0']>td>input:checkbox", function (e) {
        var isChecked = $(this).prop('checked');
        var trIndex = $(this).parent("td").parent('tr').index();
        var aryIndex = level0_IndexAry.indexOf(trIndex);
        var index1 = trIndex + 1;
        var index2 = level0_IndexAry[aryIndex + 1] - 1;

        if ((aryIndex + 1) == level0_IndexAry.length - 1)
            index2 = level0_IndexAry[level0_IndexAry.length - 1];
        var checkedValue = $(this).val();
        //console.log(index1);
        //console.log(index2);
        //console.log(checkedValue);
        var $trs = $("#tableRoot>tbody>tr");
        for (var i = index1; i <= index2; i++) {
            //console.log($("#tableRoot>tbody>tr").get(i));
            var tr = $trs.get(i);
            //console.log(tr);
            $(tr).find('input:checkbox[value="' + checkedValue + '"]').prop('checked', isChecked);
        }
       
        e.preventDefault();
        return false;
    });

    var level1_IndexAry = [];
    $.each($("tr[level='0'],tr[level='1']"), function (index, item) {
        level1_IndexAry.push($(item).index());
    });
    level1_IndexAry.push($("#tableRoot>tbody>tr").length - 1);
    //console.log(level0_IndexAry);
    $(document).on("change", "tr[level='1']>td>input:checkbox", function (e) {
        var isChecked = $(this).prop('checked');
        var trIndex = $(this).parent("td").parent('tr').index();
        var aryIndex = level1_IndexAry.indexOf(trIndex);
        var index1 = trIndex + 1;
        var index2 = level1_IndexAry[aryIndex + 1] - 1;
        //console.log(level1_IndexAry[aryIndex + 1] - 1);
        if ((aryIndex + 1) == level1_IndexAry.length - 1) {
            index2 = level1_IndexAry[level1_IndexAry.length - 1];
        }
        //console.log(index2);
        var checkedValue = $(this).val();
        //console.log(index1);
        //console.log(index2);
        //console.log(checkedValue);
        var $trs = $("#tableRoot>tbody>tr");
        for (var i = index1; i <= index2; i++) {
            //console.log($("#tableRoot>tbody>tr").get(i));
            var tr = $trs.get(i);
            //console.log(tr);
            $(tr).find('input:checkbox[value="' + checkedValue + '"]').prop('checked', isChecked);
        }

        //console.log(99999);
        var $tr = $(this).closest("tr");
        var pid = $tr.attr('pid');
        var $parent = $('#x' + pid);
        var value = $(this).val();
        var trIndexB = $parent.index();
        var aryIndexB = level0_IndexAry.indexOf(trIndexB);
        var index1B = trIndexB + 1;
        var index2B = level0_IndexAry[aryIndexB + 1] - 1;

        if ((aryIndexB + 1) == level0_IndexAry.length - 1)
            index2B = level0_IndexAry[level0_IndexAry.length - 1];

        var $trsB = $("#tableRoot>tbody>tr");
        var checkedCountB = 0;
        for (var i = index1B; i <= index2B; i++) {
            //console.log($("#tableRoot>tbody>tr").get(i));
            var tr = $trsB.get(i);
            //console.log(tr);

            $.each($(tr).find('input:checkbox[value="2"]'), function (index, item) {
                if ($(item).prop("checked") == true) {
                    checkedCountB++;
                }
            });
        }

        if (value == "2") {

            if (checkedCountB == 0) {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", false);
            }
            else {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", true);
            }
        }
        else {
            if ((checkedCountB - 1) != (index2B - index1B)) {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", false);
            }
            else
            {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", true);
            }
        }

        e.preventDefault();
        return false;
    });

    $(document).on("click", ".allBtn", function (e) {

       
        //$(this).parents("tr:first").find(":checkbox").prop("checked", true);

        var $tr =$(this).closest("tr");
        var level =$tr.attr("level");
        //console.log(level);
        var $cbs = $tr.find("input:checkbox");
        switch(level)    
        {
            case "0":
                $.each($cbs, function (index, item) {
                    $(item).prop("checked", false);
                    $(item).trigger("click");
                    $(item).prop("checked", true);
                });
                break;
            case "1":
                $.each($cbs, function (index, item) {
                     $(item).prop("checked",false);
                     $(item).trigger("click");
                     $(item).prop("checked",true);
                });

               
                var pid = $tr.attr('pid');
                var $parent = $('#x' + pid);
                var trIndexB = $parent.index();
                var aryIndexB = level0_IndexAry.indexOf(trIndexB);
                var index1B = trIndexB + 1;
                var index2B = level0_IndexAry[aryIndexB + 1] - 1;

                if ((aryIndexB + 1) == level0_IndexAry.length - 1)
                    index2B = level0_IndexAry[level0_IndexAry.length - 1];
                //console.log(index1B);
                //console.log(index2B);
                var $trsB = $("#tableRoot>tbody>tr");

                for (var x = 2; x <= 10; x++)
                {
                        //console.log(x);
                        var checkedCountB = 0;
                        for (var i = index1B; i <= index2B; i++) {
                            //console.log($("#tableRoot>tbody>tr").get(i));
                            var tr = $trsB.get(i);
                            //console.log(tr);

                            $.each($(tr).find('input:checkbox[value="'+x+'"]'), function (index, item) {
                                if ($(item).prop("checked") == true) {
                                    checkedCountB++;
                                }
                            });
                        }
                     

                       
                        if (x.toString() == "2") {
                            if (checkedCountB == 0) {
                                $parent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                            }
                            else {
                                $parent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                            }
                        }
                        else {
                            if ((checkedCountB - 1) == (index2B - index1B)) {
                                $parent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                            }
                            else
                            {
                                $parent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                            }
                        }
                       
                }

                break;
            default:
                $cbs.prop("checked", false);
                $cbs.trigger("click");
                $cbs.prop("checked", true);


                var pidA = $tr.attr('pid');
                var $parent = $('#x' + pidA);
                var trIndexA = $parent.index();
                var aryIndexA = level1_IndexAry.indexOf(trIndexA);
                var index1A = trIndexA + 1;
                var index2A = level1_IndexAry[aryIndexA + 1] - 1;

                if ((aryIndexA + 1) == level1_IndexAry.length - 1)
                    index2A= level1_IndexAry[level1_IndexAry.length - 1];
                var $trsA = $("#tableRoot>tbody>tr");

                for (var x = 2; x <= 10; x++) {
                    //console.log(x);
                    var checkedCountA = 0;
                    for (var i = index1A; i <= index2A; i++) {
                        //console.log($("#tableRoot>tbody>tr").get(i));
                        var tr = $trsA.get(i);
                        //console.log(tr);

                        $.each($(tr).find('input:checkbox[value="' + x + '"]'), function (index, item) {
                            if ($(item).prop("checked") == true) {
                                checkedCountA++;
                            }
                        });
                    }



                    if (x.toString() == "2") {
                        if (checkedCountA == 0) {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                        }
                        else {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                        }
                    }
                    else {
                        if ((checkedCountA - 1) == (index2A - index1A)) {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                        }
                        else {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                        }
                    }

                }

                var pidB = $parent.attr('pid');
                var $grandParent = $('#x' + pidB);
                var trIndexB = $grandParent.index();
                var aryIndexB = level0_IndexAry.indexOf(trIndexB);
                var index1B = trIndexB + 1;
                var index2B = level0_IndexAry[aryIndexB + 1] - 1;

                if ((aryIndexB + 1) == level0_IndexAry.length - 1)
                    index2B = level0_IndexAry[level0_IndexAry.length - 1];
                //console.log(index1B);
                //console.log(index2B);
                var $trsB = $("#tableRoot>tbody>tr");

                for (var x = 2; x <= 10; x++) {
                    //console.log(x);
                    var checkedCountB = 0;
                    for (var i = index1B; i <= index2B; i++) {
                        //console.log($("#tableRoot>tbody>tr").get(i));
                        var tr = $trsB.get(i);
                        //console.log(tr);

                        $.each($(tr).find('input:checkbox[value="' + x + '"]'), function (index, item) {
                            if ($(item).prop("checked") == true) {
                                checkedCountB++;
                            }
                        });
                    }

                    //console.log(checkedCountB-1);
                    //console.log(index2B - index1B);

                    if (x.toString() == "2") {
                        if (checkedCountB == 0) {
                            $grandParent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                        }
                        else {
                            $grandParent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                        }
                    }
                    else {
                        if ((checkedCountB - 1) == (index2B - index1B)) {
                            $grandParent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                        }
                        else {
                            $grandParent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                        }
                    }

                }
                break;
        }
       
       
        e.preventDefault();
        return false;
    });

    $(document).on("click", ".disAllBtn", function (e) {

        //$(this).parents("tr:first").find(":checkbox").prop("checked", false);

        var $tr = $(this).closest("tr");
        var level = $tr.attr("level");
        //console.log(level);
        var $cbs = $tr.find("input:checkbox");
        switch (level) {
            case "0":
                $.each($cbs, function (index, item) {
                    $(item).prop("checked", true);
                    $(item).trigger("click");
                    $(item).prop("checked", false);
                });
                break;
            case "1":
                $.each($cbs, function (index, item) {
                    $(item).prop("checked", true);
                    $(item).trigger("click");
                    $(item).prop("checked", false);
                });

                var pid = $tr.attr('pid');
                var $parent = $('#x' + pid);
                var trIndexB = $parent.index();
                var aryIndexB = level0_IndexAry.indexOf(trIndexB);
                var index1B = trIndexB + 1;
                var index2B = level0_IndexAry[aryIndexB + 1] - 1;

                if ((aryIndexB + 1) == level0_IndexAry.length - 1)
                    index2B = level0_IndexAry[level0_IndexAry.length - 1];
                //console.log(index1B);
                //console.log(index2B);
                var $trsB = $("#tableRoot>tbody>tr");

                for (var x = 2; x <= 10; x++) {
                    //console.log(x);
                    var checkedCountB = 0;
                    for (var i = index1B; i <= index2B; i++) {
                        //console.log($("#tableRoot>tbody>tr").get(i));
                        var tr = $trsB.get(i);
                        //console.log(tr);

                        $.each($(tr).find('input:checkbox[value="' + x + '"]'), function (index, item) {
                            if ($(item).prop("checked") == true) {
                                checkedCountB++;
                            }
                        });
                    }



                    if (x.toString() == "2") {
                        if (checkedCountB == 0) {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                        }
                        else {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                        }
                    }
                    else {
                        if ((checkedCountB - 1) == (index2B - index1B)) {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                        }
                        else {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                        }
                    }

                }
                break;
            default:
                $cbs.prop("checked", true);
                $cbs.trigger("click");
                $cbs.prop("checked", false);

                var pidA = $tr.attr('pid');
                var $parent = $('#x' + pidA);
                var trIndexA = $parent.index();
                var aryIndexA = level1_IndexAry.indexOf(trIndexA);
                var index1A = trIndexA + 1;
                var index2A = level1_IndexAry[aryIndexA + 1] - 1;

                if ((aryIndexA + 1) == level1_IndexAry.length - 1)
                    index2A = level1_IndexAry[level1_IndexAry.length - 1];
                var $trsA = $("#tableRoot>tbody>tr");

                for (var x = 2; x <= 10; x++) {
                    //console.log(x);
                    var checkedCountA = 0;
                    for (var i = index1A; i <= index2A; i++) {
                        //console.log($("#tableRoot>tbody>tr").get(i));
                        var tr = $trsA.get(i);
                        //console.log(tr);

                        $.each($(tr).find('input:checkbox[value="' + x + '"]'), function (index, item) {
                            if ($(item).prop("checked") == true) {
                                checkedCountA++;
                            }
                        });
                    }



                    if (x.toString() == "2") {
                        if (checkedCountA == 0) {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                        }
                        else {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                        }
                    }
                    else {
                        if ((checkedCountA - 1) == (index2A - index1A)) {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                        }
                        else {
                            $parent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                        }
                    }

                }

                var pidB = $parent.attr('pid');
                var $grandParent = $('#x' + pidB);
                var trIndexB = $grandParent.index();
                var aryIndexB = level0_IndexAry.indexOf(trIndexB);
                var index1B = trIndexB + 1;
                var index2B = level0_IndexAry[aryIndexB + 1] - 1;

                if ((aryIndexB + 1) == level0_IndexAry.length - 1)
                    index2B = level0_IndexAry[level0_IndexAry.length - 1];
                //console.log(index1B);
                //console.log(index2B);
                var $trsB = $("#tableRoot>tbody>tr");

                for (var x = 2; x <= 10; x++) {
                    //console.log(x);
                    var checkedCountB = 0;
                    for (var i = index1B; i <= index2B; i++) {
                        //console.log($("#tableRoot>tbody>tr").get(i));
                        var tr = $trsB.get(i);
                        //console.log(tr);

                        $.each($(tr).find('input:checkbox[value="' + x + '"]'), function (index, item) {
                            if ($(item).prop("checked") == true) {
                                checkedCountB++;
                            }
                        });
                    }

                    //console.log(checkedCountB - 1);
                    //console.log(index2B - index1B);

                    if (x.toString() == "2") {
                        if (checkedCountB == 0) {
                            $grandParent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                        }
                        else {
                            $grandParent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                        }
                    }
                    else {
                        if ((checkedCountB - 1) == (index2B - index1B)) {
                            $grandParent.find("input:checkbox[value='" + x + "']").prop("checked", true);
                        }
                        else {
                            $grandParent.find("input:checkbox[value='" + x + "']").prop("checked", false);
                        }
                    }

                }
                break;
        }

       
        e.preventDefault();
        return false;
    });

    $(document).on("change", "tr[level='2']>td>input:checkbox", function (e) {
        var isChecked = $(this).prop('checked');
        var $tr = $(this).closest("tr");
        var pid = $tr.attr('pid');
        var value = $(this).val();
        var $parent = $('#x' + pid);
        var $grandParent = $('#x' + $parent.attr("pid"));
        var $brothers = $("tr[level='2'][pid='"+pid+"']>td>input:checkbox[value='" + value + "']");
        if (isChecked == true)
        {
            var allChecked = true;
            //console.log(brothers);
            $.each($brothers, function (index, item) {
                if($(item).prop("checked")==false)
                {
                    allChecked = false;
                }
            });
            if(allChecked==true || value=="2")
            {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", true);
                if(value=="2")
                    $grandParent.find("input:checkbox[value='" + value + "']").prop("checked", true);
            }
        }
        else
        {
            
            if (value == "2") {
                //console.log("allUnChecked");
                var allUnChecked = true;
                //console.log(brothers);
                $.each($parent, function (index, item) {
                    if ($(item).prop("checked") == true) {
                        allUnChecked = false;
                    }
                });
                //console.log(allUnChecked);
                if (allUnChecked == true && value!="2") {
                    $parent.find("input:checkbox[value='" + value + "']").prop("checked", false);
                }
            }
            else {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", false);
            }
        }

        var trIndexA = $parent.index();
        var aryIndexA = level1_IndexAry.indexOf(trIndexA);
        var index1A = trIndexA + 1;
        var index2A = level1_IndexAry[aryIndexA + 1] - 1;

        if ((aryIndexA + 1) == level0_IndexAry.length - 1)
            index2A = level1_IndexAry[level0_IndexAry.length - 1];

        var $trsA = $("#tableRoot>tbody>tr");
        var checkedCountA = 0;
        for (var i = index1A; i <= index2A; i++) {
            //console.log($("#tableRoot>tbody>tr").get(i));
            var tr = $trsA.get(i);
            //console.log(tr);

            $.each($(tr).find('input:checkbox[value="2"]'), function (index, item) {
                if ($(item).prop("checked") == true) {
                    checkedCountA++;
                }
            });
        }

        if (value == "2") {
            if (checkedCountA == 0) {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", false);
            }
            else {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", true);
            }
        }
        else {
            if ((checkedCountA - 1) != (index2A - index1A)) {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", false);
            }
            else
            {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", true);
            }
        }

        var trIndexB = $grandParent.index();
        var aryIndexB = level0_IndexAry.indexOf(trIndexB);
        var index1B = trIndexB + 1;
        var index2B = level0_IndexAry[aryIndexB + 1] - 1;

        if ((aryIndexB + 1) == level0_IndexAry.length - 1)
            index2B = level0_IndexAry[level0_IndexAry.length - 1];
        
        var $trsB = $("#tableRoot>tbody>tr");
        var checkedCountB=0;
        for (var i = index1B; i <= index2B; i++) {
            //console.log($("#tableRoot>tbody>tr").get(i));
            var tr = $trsB.get(i);
            //console.log(tr);

            $.each($(tr).find('input:checkbox[value="2"]'), function (index, item) {
                if ($(item).prop("checked") == true) {
                    checkedCountB++;
                }
            });
        }

        if (value == "2") {

            if (checkedCountB == 0) {
                $grandParent.find("input:checkbox[value='" + value + "']").prop("checked", false);
            }
            else {
                $grandParent.find("input:checkbox[value='" + value + "']").prop("checked", true);
            }
        }
        else
        {
            if ((checkedCountB-1) != (index2B - index1B )) {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", false);
            }
            else {
                $parent.find("input:checkbox[value='" + value + "']").prop("checked", true);
            }
        }
    });


    //$(document).on("change", ".changeParent", function (e) {
    //    var $parent = $(this).closest("tr");

    //    if ($(this).prop("checked")) {

    //        var level = parseInt($parent.attr("level"));
    //        $parent.find(":checkbox:first").prop("checked", true);

    //        level = level - 1;

    //        var avoidLoopFlag = 10;
    //        while (level > 0 && avoidLoopFlag > 0) {
    //            $parent = $("#x" + $parent.attr("pid"))
    //            level = parseInt($parent.attr("level"));
    //            if (level == 0) {
    //                avoidLoopFlag = 0;
    //            }
    //            $parent.find(":checkbox:first").prop("checked", true);

    //            avoidLoopFlag = avoidLoopFlag - 1;
    //        }

    //    }

    //    e.preventDefault();
    //    return false;
    //});

    $(document).ajaxStart(function () {
        $("#loading").show();
    }).ajaxComplete(function () {
        $("#loading").hide();
    });

    $(document).on("click", "#btnSave", function (e) {
        var $trs = $("#tableRoot>tbody>tr:not(:first)");

        var postIds = [];
        $.each($trs, function (index, item) {
            var robj = {};
            robj.mId = parseInt($(item).attr("id").replace(/x/g, ""));
            robj.ps = "";
            //console.log(mId);
            var $cbs = $(item).find("input:checkbox");
            var ps = [];
            for (var i = 0; i < 10; i++) {
                ps.push(0);
            }
            ps[0] = 1;

            $.each($cbs, function (index, cb) {
                var value = parseInt($(cb).val());
                var isChecked = $(cb).prop("checked");
                if (isChecked == true) {
                    ps[value - 1] = 1;
                }
            });
            robj.ps = ps.reverse().toString().replace(/,/g, "");
            postIds.push(robj);
        });

        document.querySelector("#hfV").value = JSON.stringify(postIds);

        //var postData = {};
        //postData["r"] = RI;
        //postData["v"] = JSON.stringify(postIds);
        ////ajax
        //$.ajax({
        //    cache: false,
        //    type: "POST",
        //    url: "/System/PowerM/RolePowerSetting_AddPowerSaveOP.aspx",
        //    data: postData
        //})
        //.done(function (data) {
        //    if (data.chk > 0) {
        //        alert("儲存成功");
        //        sessionStorage.removeItem("tbName");
        //        sessionStorage.removeItem("tbDesp");
        //        sessionStorage.removeItem("rbLevel");
        //        location.href = "/System/PowerM/RolePowerSetting.aspx";
        //    }
        //    else {
        //        alert("儲存失敗");
        //    }
        //})
        //.fail(function (jqXHR, textStatus) {

        //});

        //e.preventDefault();
        //return false;
    });

    $(document).on("click", "#lastBtn", function (e) {
        //location.href = "/System/PowerM/RolePowerSetting_Add.aspx";
        var keys = [];
        var values = [];
        keys[0] = "sc";
        values[0] = CI;
        doPOST("/System/PowerM/RolePowerSetting_Add.aspx",keys,values)
        e.preventDefault();
        return false;
    });


    var doPOST = function (url, keys, values) {
        keys = keys || [];
        values = values || [];
        var html = "";
        html += "<html><head></head><body><form id='formid' method='post' action='" + url + "' autocomplete='off'>";
        if (keys && values && (keys.length == values.length))
            for (var i = 0; i < keys.length; i++)
                html += "<input type='hidden' name='" + keys[i] + "' value='" + values[i] + "'/>";
        html += "</form><script type='text/javascript'>document.getElementById(\"formid\").submit()</script></body></html>";
        //console.log(html);
        document.write(html);
    };
 
});