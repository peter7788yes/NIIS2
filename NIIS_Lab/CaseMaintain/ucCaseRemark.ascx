<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCaseRemark.ascx.cs" Inherits="CaseMaintain_ucCaseRemark" %>
<input id="btnUcReamrkAdd" type="button" value="新增"  class="btn"/><br/>
<input type="hidden" id="NewCaseUserRemarkIDs" name="NewCaseUserRemarkIDs" />
<div id="CaseRemarkList" class="formTb4">

</div>

<script src="/js/jq/jquery-2.1.4.js" type="text/javascript"></script>
<script src="/js/other/commUtil.js" type="text/javascript"></script>
<script type="text/javascript">

$(function () {
    $(document).on("click", "#btnUcReamrkAdd", function (e) {

        OpenWindowWithPostOptions("/CaseMaintain/CaseRemarkContent.aspx", 600, 300, "CaseRemarkContent", { CaseID: '<%=CaseID.ToString() %>', action: 'Add' });

        e.preventDefault();
        return false;

    });

    ReloadRemarkList();
});

function AddRemarkTr(iRemarkID) {

    $("#NewCaseUserRemarkIDs").val($("#NewCaseUserRemarkIDs").val() + "," + iRemarkID);
 
    var RemarkData = {};
    RemarkData["action"] = "GetRemarkTr";
    RemarkData["RemarkID"] = iRemarkID;

    $.ajax({
        cache: false,
        type: "POST",
        url: "/CaseMaintain/ucCaseRemarkOP.aspx",
        data: RemarkData
    })
                .done(function (data) { 
                    var reply = eval(data); 
                    if (reply.RetCode == '1') {
                        $("#Reamrk_TB > tbody").append(reply.Content);

                        //event;

                        $(".ModifyRemark").click(function () {
                            var RemarkID = $(this).attr("id").replace("ModifyRemark_", "");
                            OpenWindowWithPostOptions("/CaseMaintain/CaseRemarkContent.aspx", 600, 300, "CaseRemarkContent", { "RemarkID": RemarkID });


                        });
                        $(".DeleteRemark").click(function () {
                            if (confirm('確定刪除?')) {
                                var RemarkID = $(this).attr("id").replace("DeleteRemark_", "");
                                deleteRemark(RemarkID);
                            }
                        });

                    }


                })
                .fail(function (jqXHR, textStatus) {

                });


};

 function ReloadRemarkList () {
     var RemarkData = {};
     RemarkData["action"] = "GetList";
    RemarkData["c"] = '<%=CaseID.ToString() %>';

    $.ajax({
        cache: false,
        type: "POST",
        url: "/CaseMaintain/ucCaseRemarkOP.aspx",
        data: RemarkData
    })
                .done(function (data) {
                    var reply = eval(data);
                    if (reply.RetCode == '1') {
                        $("#CaseRemarkList").html(reply.Content);

                        //event;

                        $(".ModifyRemark").click(function () {
                            var RemarkID = $(this).attr("id").replace("ModifyRemark_", "");
                            OpenWindowWithPostOptions("/CaseMaintain/CaseRemarkContent.aspx", 600, 300, "CaseRemarkContent",  { "RemarkID": RemarkID });


                        });
                        $(".DeleteRemark").click(function () {
                            if (confirm('確定刪除?')) {
                                var RemarkID = $(this).attr("id").replace("DeleteRemark_", "");
                                deleteRemark(RemarkID);
                            }
                        });

                    }


                })
                .fail(function (jqXHR, textStatus) {

                });


            };
            function deleteRemark( iRemarkID ) {
                var RemarkData = {};
                RemarkData["action"] = "Delete";
                RemarkData["RemarkID"] = iRemarkID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "/CaseMaintain/ucCaseRemarkOP.aspx",
                    data: RemarkData
                })
                .done(function (data) {
                    var reply = eval(data);
                    if (reply.RetCode == '1') {
                        ReloadRemarkList();

                    }
                     

                });

            }

</script>

