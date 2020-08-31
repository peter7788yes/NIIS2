<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemParameters.aspx.cs" Inherits="System_SystemSettingsM_SystemParameters_SystemParameters" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
    <div id="SystemParametersApp" ng-controller="SystemParametersController" ng-cloak>
        <%if(SearchPower.HasPower){ %>
        <form name="form1" id="form1" >
            <div class="formTb formTb6">
                <table>
                  <tr>
                    <td colspan="2">
                        <input id="Para" ng-model="VM.Para" name="Para" type="text" value="" class="text03"/>
                        <div class="formBtn" style="display:inline">
                            <input class="btn" type="submit" name="send" ng-click="Search()" value="查詢參數" />
                        </div>
                    </td>
                  </tr>
                </table>
            </div>
        </form>
        <%} %>
        <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <th scope="col" width="10%">序號</th>
                    <th scope="col" width="20%">參數</th>
                    <th scope="col" width="55%">參數說明</th>
                    <th scope="col" width="15%">參數值</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" ng-bind='record["c3"]'></td>
                    <td class="aCenter" ng-bind='record["c4"]'></td>
                    <%if(ModifyPower.HasPower){ %>
                    <td class="aCenter" >
                        <input ng-model='record["c5"]' type="text" value="" size="10" ng-blur="SaveData(record)" />
                    </td>
                    <%}else{ %>
                    <td class="aCenter" ng-bind='record["c5"]'></td>
                    <%} %>
                </tr>
            </table>
         </div>
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/SystemParameters_JS")%>
</asp:Content>