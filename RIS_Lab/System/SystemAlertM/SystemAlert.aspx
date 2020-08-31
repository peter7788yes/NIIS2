<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemAlert.aspx.cs" Inherits="System_SystemAlertM_SystemAlert" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" Runat="Server" >
     <%=BodyClass %>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <div id="SystemAlertApp" ng-controller="SystemAlertController" ng-cloak>
        <form name="form1" id="form1" >
            <%if(SearchPower.HasPower){ %>
            <div class="formTb">
                <table>
                  <tr>
                    <th scope="row">所屬單位:</th>
                    <td>
                        <input ng-init="VM.OrgName='<%=OrgName%>'" id="OrgName" ng-model="VM.OrgName" name="OrgName" type="text" value="" class="text03" ng-click="openOrgs()" readonly/>
                        <input ng-init="VM.OrgID='<%=OrgID%>'" id="OrgID" ng-model="VM.OrgID" name="OrgID" type="hidden" />
                        <img  src="/images/location.png" ng-click="openOrgs()" />
                        <a href="javascript:void(0);" id="refreshBtn" ng-click="refresh()"></a>    
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">提醒類別:</th>
                    <td>
                        <select ng-model="VM.SelectAlertType">
                        <option ng-repeat="option in VM.AlertType" value="{{option.EV}}" ng-bind="option.EN"></option>
                        </select>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>日期區間: </th>
                        <td>
                            <input id="StartDate" ng-model="VM.StartDate" type="text" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" onchange="SetDate()" />
                            <a href="#"><img onclick="WdatePicker({el:'StartDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"></a>~
                            <input id="EndDate" ng-model="VM.EndDate" type="text" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })"  onchange="SetDate()" />
                            <a href="#"><img onclick="WdatePicker({el:'EndDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"></a>
                        </td>
                  </tr>
                </table>
            </div>
            <div class="formBtn">
                <input class="btn" type="submit" name="send" ng-click="Search(1)" value="查詢"/>
            </div>
            <%} %>
         </form>
         <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
         </section>
         <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <th scope="col" width="10%">序號</th>
                    <th scope="col" width="15%">日期</th>
                    <th scope="col" width="15%">類別</th>
                    <th scope="col" width="60%">內容</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" ng-bind='record["c2"]'></td>
                    <td class="aCenter" ng-bind='record["c4"]'></td>
                    <td><a href="javascript:void(0);" ng-bind='record["c5"]' ng-click="TransferMessage(record)"></a></td>
                </tr>
            </table>
         </div>
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
        var AlertTypeData = <%=AlertType%>;
        var OrgID = <%=OrgID%>;
     </script>
     <script src="/js/date/WdatePicker.js"></script>
     <%:Scripts.Render("~/bundles/SystemAlert_JS")%>
</asp:Content>