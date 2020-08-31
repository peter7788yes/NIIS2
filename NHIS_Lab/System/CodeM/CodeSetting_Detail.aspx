<%@ Page Language="C#" ViewStateMode="Enabled" AutoEventWireup="true" CodeFile="CodeSetting_Detail.aspx.cs" Inherits="System_CodeM_CodeSetting_Detail" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
       <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
        <div class="formBtn formBtnleft">
            <% if (AddPower.HasPower && VM.CanEdit) { %>
              <input type="button" id="addBtn" value="新增" class="btn" ng-click="goAdd()" />
            <% } %>
             
             <input type="button" id="lastBtn" value="回上一頁" class="btn" />
        </div>
             <div class="formTb formTb2 formTb3">

                        <table>
                            <tr>
                                <th scope="row">代碼:</th>
                                <td>
                                    <%=VM.CodeDescription %>
                                    <input name="i" id="i" type="hidden" value="<%=ID %>" />
                                </td>
                                 <th scope="row">狀態：</th>
                                <td>
                                    <select ng-model="VM.enState" ng-change="changePage(1)">
                                        <option value="0">全部</option>
                                        <option value="1">啟用</option>
                                        <option value="2">停用</option>
                                    </select>
                                </td> 
                            </tr>
                            </table>
                        
            </div>
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">代碼值</th>
                    <th scope="col">代碼值顯示</th>
                    <th scope="col">順序</th>
                    <th scope="col">狀態</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter" ng-bind='record["EV"]'></td>
                      <td class="aCenter" ng-bind='record["EN"]'></td>
                      <td class="aCenter" ng-bind='record["O"]'></td>
                      <td class="aCenter" ng-bind='record["IE"]==1?"啟用":"不啟用"'></td>
                </tr>
            </table>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var I=<%=ID %>;
    </script>
       <%:Scripts.Render("~/bundles/CodeSetting_Detail_JS")%>
</asp:Content>


