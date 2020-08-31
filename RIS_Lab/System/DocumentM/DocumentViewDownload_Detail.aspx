<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentViewDownload_Detail.aspx.cs" Inherits="System_DocumentM_DocumentViewDownload_Detail" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
                <form id="MyForm" autocomplete="off">
                <div class="formBtn formBtnleft">
                    <input type="button" id="lastBtn" value="回上一頁" class="btn" />
                </div>
                <div class="formTb">
                    <table>
                        <tr>
                            <th scope="row">發佈日期：</th>
                            <td>
                                <%:VM.PublishStartDate.ToShortTaiwanDate() %>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">標題：</th>
                            <td>
                                <%:VM.DocTitle %>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">檔案：</th>
                            <td>
                                <div ng-repeat="item in VM.fileList track by $index">
                                    <span class="replaceA" ng-bind='item["DF"]' ng-click="goDownload(item)"></span>
                                    <br/>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">說明：</th>
                            <td>
                                <%:VM.DocDescription %>
                            </td>
                        </tr>
                    </table>
                </div>
         </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
     <script>
        var i =<%:ID%>;
        var fileList = <%=FileList%>;
    </script>
    <%:Scripts.Render("~/bundles/DocumentViewDownload_Detail_JS")%>
</asp:Content>
