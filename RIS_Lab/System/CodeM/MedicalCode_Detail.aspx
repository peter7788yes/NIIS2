<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedicalCode_Detail.aspx.cs" Inherits="System_CodeM_MedicalCode_Detail" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <link href="/css/design.min.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
     <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>

            <div class="formBtn formBtnleft">
                <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>

                <div class="formTb">
                <table>
                    <tr>
                        <th scope="row">代碼：</th>
                        <td>
                            <%=VM.AgencyCode %>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">名稱：</th>
                        <td>
                            <%=VM.OrgAgencyName %>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">簡稱：</th>
                        <td>
                             <%=VM.OrgAgencyShortName %>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">開業狀態：</th>
                        <td>
                              <%=VM.BusinessStateString %>
                        </td>
                    </tr>
                </table>

            </div>

            <div class="list01">
                        <ul id="myUl">
                          <li>
                              <span>最後異動日期：</span><label ng-bind='TM.data[0]["CD"] | ShortTaiwanTime:-480'></label>
                          </li>
                        </ul>
            </div>

        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">機構代碼</th>
                    <th scope="col">機構名稱</th>
                    <th scope="col">開業狀態</th>
                    <th scope="col">更新日期</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter" ng-bind='record["AC"]'></td>
                      <td class="aCenter" ng-bind='record["AN"]'></td>
                      <td class="aCenter" ng-bind='record["BS"]'></td>
                      <td class="aCenter" ng-bind='record["CD"] | ShortTaiwanTime:-480'></td>
                </tr>
            </table>
       
</div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var i =<%=ID %>;
        <%--var tbData = <%=tbData%>;--%>
    </script>
     <%:Scripts.Render("~/bundles/MedicalCode_Detail_JS")%>
</asp:Content>
