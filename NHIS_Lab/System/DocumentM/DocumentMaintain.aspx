<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentMaintain.aspx.cs" Inherits="DocumentManagementM_DocumentMaintain" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
                <% if (SearchPower.HasPower) { %>
                     <input type="button" id="searchBtn" value="查詢" class="btn" ng-click="changePage(1)"  />
                <% } %>
                <% if (AddPower.HasPower) { %>
                     <input type="button" id="addBtn" value="新增" class="btn" />
                <% } %>
            
            
        </div>
        
        <% if (SearchPower.HasPower) { %>
                <div class="formTb">
                    <table>
                        <tr>
                            <th scope="row">標題：</th>
                            <td>
                                <input name="" type="text" class="text01" ng-model="VM.title" /></td>
                        </tr>
                        <tr>
                            <th scope="row">上架狀態：</th>
                            <td>
                                <select ng-model="VM.publishState">
                                    <option value="0">全部</option>
                                    <option value="1">上架</option>
                                    <option value="2">下架</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
   
        <% } %>
        

        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb">
            <table>
                <tr>
                     <%--<th ng-repeat="item in TM.tbHeadArray  track by $index"
                            ng-style="$index == 0  ? {'width':'10%'} : [1,2].indexOf($index)>=0 ? {'width':'15%'} :{}"
                            ng-bind="item"></th>--%>
                    <th scope="col" width="10%">序號</th>
                    <th scope="col" width="15%">發佈日期</th>
                    <th scope="col" width="15%">上架狀態</th>
                    <th scope="col">標題</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter" ng-bind='record["P"] | SimpleTaiwanDate '></td>
                      <td class="aCenter" ng-bind='record["p"]'></td>
                      <td><span class="replaceA" ng-bind='record["D"]' ng-click="goDetail(record)"></span></td>
                </tr>
            </table>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/DocumentMaintain_JS")%>
</asp:Content>

