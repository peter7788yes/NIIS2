<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ImportList.aspx.cs" Inherits="Import_EmiM_ImportList" %>

<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
    <%=HeadScript %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
    <link href="../../css/jquery.autocomplete.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ctCP" Runat="Server">
    <div class="path"></div>
    <section ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <form id="form1">
            <div class="formBtn formBtnleft">
                <input class="btn" type="button" name="send" onclick="location.href = 'EmiUpload.aspx'" value="資料匯入" />
            </div>
            <div class="formTb formTb3">
                <table>
                    <tr>
                        <th scope="row"><span class="must">*</span>工作人員</th>
                        <td>
                            <input type="text" id="UserName" name="UserName"/>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>匯入日期</th>
                        <td>
                            <input id="StartDate" ng-model="VM.StartDate" type="text" onclick="WdatePicker({ dateFmt: 'yyyMMdd', maxDate: new Date(), lang: 'zh-tw' })" onchange="SetDate()" />
                            <a href="#"><img onclick="WdatePicker({el:'StartDate',dateFmt: 'yyyMMdd',maxDate: new Date(),lang:'zh-tw'})" src="/images/icon_calendar.png"></a>~
                                        <input id="EndDate" ng-model="VM.EndDate" type="text" onclick="WdatePicker({ dateFmt: 'yyyMMdd', maxDate: new Date(), lang: 'zh-tw' })" onchange="SetDate()" />
                            <a href="#"><img onclick="WdatePicker({el:'EndDate',dateFmt: 'yyyMMdd', maxDate: new Date(),lang:'zh-tw'})" src="/images/icon_calendar.png"></a>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="formBtn">
                <input class="btn" type="submit" name="send" ng-click="Search(1)" value="查詢" />
            </div>
        </form>
        <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        </section>
        <div class="listTb" style="display: none;">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">工作人員</th>
                    <th scope="col">匯入時間</th>
                    <th scope="col">成功</th>
                    <th scope="col">重複</th>
                    <th scope="col">異常</th>
                    <th scope="col">總筆數</th>
                    <th scope="col">異常資料下載</th>
                    <th scope="col">資料清單</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["SID"]'></td>
                    <td class="aCenter" ng-bind='record["UserName"]'></td>
                    <td class="aCenter" ng-bind='record["CreateDate"]'></td>
                    <td class="aCenter" ng-bind='record["SuccessCnt"]'></td>
                    <td class="aCenter" ng-bind='record["RepeatCnt"]'></td>
                    <td class="aCenter" ng-bind='record["ErrorCnt"]'></td>
                    <td class="aCenter" ng-bind='record["TotalCnt"]'></td>
                    <td class="aCenter"><img style="cursor:pointer" ng-click="getError(record)" src="/images/icon_download.png" /></td>
                    <td class="aCenter"><img style="cursor:pointer" ng-click="goDetail(record)" src="/images/icon_maintain.png" /></td>
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
    <script src="/js/date/WdatePicker.js" type="text/javascript"></script>
    <script src="/js/jq/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        var data = <%=GetUserList()%>;
        $("#UserName").autocompleteArray(data);

        function getToday() {
            return '<%=(DateTime.Now.Year-1911).ToString() + DateTime.Now.ToString("MMdd")%>';
        }
    </script>
    <script src="ImportList.js" type="text/javascript"></script>
</asp:Content>

