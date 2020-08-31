<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ImportLogDetail.aspx.cs" Inherits="Import_ImmiM_ImportLogDetail" %>

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
                <input class="btn" type="button" name="send" onclick="location.href='ImportList.aspx'" value="回上一頁"/>
            </div>
            <div class="formTb formTb3">
                介接日期：<%=ImportDate %> -  介接筆數：<%=DataCnt %>
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
                    <th scope="col">姓名</th>
                    <th scope="col">出生日期</th>
                    <th scope="col">性別</th>
                    <th scope="col">身分證號</th>
                    <th scope="col">護號</th>
                    <th scope="col">證號</th>
                    <th scope="col">入出日期</th>
                    <th scope="col">入出班機</th>
                    <th scope="col">入出港口</th>
                    <th scope="col">起程前往地</th>
                    <th scope="col">地址</th>
                    <th scope="col">海外地址</th>
                    <th scope="col">申請事由</th>
                    <th scope="col">轉入狀態</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["SID"]'></td>
                    <td class="aCenter" ng-bind='record["UserName"]'></td>
                    <td class="aCenter" ng-bind='record["Birthday"]'></td>
                    <td class="aCenter" ng-bind='record["strSex"]'></td>
                    <td class="aCenter" ng-bind='record["CaseID"]'></td>
                    <td class="aCenter" ng-bind='record["PassportNo"]'></td>
                    <td class="aCenter" ng-bind='record["CardNo"]'></td>
                    <td class="aCenter" ng-bind='record["FlightDate"]'></td>
                    <td class="aCenter" ng-bind='record["FlightNo"]'></td>
                    <td class="aCenter" ng-bind='record["Port"]'></td>
                    <td class="aCenter" ng-bind='record["ArrivalLoc"]'></td>
                    <td class="aCenter" ng-bind='record["Address"]'></td>
                    <td class="aCenter" ng-bind='record["OverseasAddr"]'></td>
                    <td class="aCenter" ng-bind='record["Reason"]'></td>
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

