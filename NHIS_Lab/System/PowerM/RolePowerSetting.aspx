<%@ Page Language="C#" ViewStateMode="Disabled" AutoEventWireup="true" CodeFile="RolePowerSetting.aspx.cs" Inherits="RolePowerSetting" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
                <% if (AddPower.HasPower) { %>
                      <input type="button" id="addBtn" value="新增" class="btn" ng-click="changeAdd()" />
                <% } %>
           
        </div>

         <div class="formTb">
                <table>
                    <tr>
                       <th scope="row">系統名稱:</th>
                        <td>
                            <select ng-model="VM.selectSystem" ng-change="changeSystem()">
                                 <option value="1" selected="selected">全國性預防接種資訊管理系統</option>
                                 <option value="2">院所版預防接種子系統</option>
                                 <option value="3">離線版預防接種子系統</option>
                                 <option value="4">戶政資料管理子系統</option>
                                 <option value="5">國際預防接種系統</option>
                            </select>
                        </td>
                    </tr>
                    </table>
        </div>

        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb" >
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">角色名稱</th>
                    <th scope="col">所屬層級</th>
                    <th scope="col">角色說明</th>
                    <th scope="col">權限修改</th>
                    <th scope="col">名稱修改</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter" ng-bind='record["RN"]'></td>
                      <td class="aCenter" ng-bind='record["R"]'></td>
                      <td class="aCenter" ng-bind='record["RD"]'></td>
                      
                      <td class="aCenter"><a href="javascript:void(0);"><img src="/images/icon_maintain.png" ng-click="changePower(record)"/></a></td>
                      <td class="aCenter"><a href="javascript:void(0);"><img src="/images/icon_maintain.png" ng-click="changeName(record)" /></a></td>
                </tr>
            </table>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/RolePowerSetting_JS")%>
</asp:Content>




