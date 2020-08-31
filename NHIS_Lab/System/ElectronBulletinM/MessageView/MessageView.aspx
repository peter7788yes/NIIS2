<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageView.aspx.cs" Inherits="System_MessageViewM_MessageView" MasterPageFile="~/MasterPage/MasterPage.master"%>
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
    <div id="MessageViewApp" ng-controller="MessageViewController" ng-init="Search(1)" ng-cloak>
        <%if(SearchPower.HasPower){ %>
        <div class="choose">
            <label for="">
                <select ng-model="VM.SelectDateStatus" ng-change="Search(1)">
                    <option ng-repeat="option in VM.SearchDate" value="{{option.EV}}" ng-bind="option.EN"></option>
                </select>
                <select ng-model="VM.SelectContentFileStatus" ng-change="Search(1)">
                    <option ng-repeat="option in VM.SearchContentFile" value="{{option.EV}}" ng-bind="option.EN"></option>
                </select>
            </label>
        </div>
        <%} %>
         <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
         </section>
         <div class="listTb">
            <table>
                <tr>
                    <th scope="col" width="10%">序號</th>
                    <th scope="col" width="10%">發佈日期</th>
                    <th scope="col" width="20%">發佈單位</th>
                    <th scope="col" width="50%">主旨</th>
                    <th scope="col" width="10%">附件</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" ng-bind='TransformROCDate(record.c9)' ></td>
                    <td class="aCenter" ng-bind='record["c3"]'></td>
                    <td class="aCenter"><a href="javascript:void(0);" ng-bind='record["c4"]' ng-click="TransferView(record)"></a></td>
                    <td class="aCenter" ng-bind='record["c13"]'></td>
                </tr>
            </table>
         </div>
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
      <script>
          var SearchDateData = <%=SearchDate %>;
          var SearchContentFileData = <%=SearchContentFile %>;
          var OrgData = <%=OrgData %>;
      </script>
     <%:Scripts.Render("~/bundles/MessageView_JS")%>
</asp:Content>