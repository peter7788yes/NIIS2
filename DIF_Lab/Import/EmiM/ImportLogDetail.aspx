<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ImportLogDetail.aspx.cs" Inherits="Import_EmiM_ImportLogDetail" %>

<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
    <%=HeadScript %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ctCP" Runat="Server">
    <div class="path"></div>
    <section ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <form id="form1">
            <div class="formBtn formBtnright">
                <input class="btn" type="button" name="send" onclick="history.back()" value="回上一頁"/><!--location.href='ImportList.aspx'-->
            </div>
            <div class="formTb formTb3">
                工作人員：<%=UserName %><br />
                處理資料：總筆數共<%=TotalCnt %>筆 - 成功<%=SuccessCnt %>筆、重複<%=RepeatCnt %>筆、異常<%=ErrorCnt %>筆
                <input type="hidden" value="<%=MasterID%>" id="MasterID" />
            </div>
        </form>
        <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        </section>
        <div class="listTb" style="display: none;">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">資料序號</th>
                    <th scope="col">身分證號</th>
                    <th scope="col">出生日期</th>
                    <th scope="col">出境日期</th>
                    <th scope="col">姓名</th>
                    <th scope="col">狀態</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["SID"]'></td>
                    <td class="aCenter" ng-bind='record["Seq"]'></td>
                    <td class="aCenter" ng-bind='record["CaseID"]'></td>
                    <td class="aCenter" ng-bind='record["Birthday"]'></td>
                    <td class="aCenter" ng-bind='record["EmiDate"]'></td>
                    <td class="aCenter" ng-bind='record["UserName"]'></td>
                    <td class="aCenter" ng-bind='record["strStatus"]'></td>
                </tr>
            </table>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jsCP" Runat="Server">
    <script src="/js/ang/angular-1.4.8.min.js" type="text/javascript"></script>
    <script src="/js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="/js/sys/menuPath.js" type="text/javascript"></script>
    <script src="/js/ang/PageM.js" type="text/javascript"></script>
    <script src="/js/ang/TableM.js" type="text/javascript"></script>
    <script src="/js/ang/FilterM.js" type="text/javascript"></script>
    <script src="ImportLogDetail.js" type="text/javascript"></script>
</asp:Content>

