<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeFile="DocumentMaintain_Update.aspx.cs" Inherits="DocumentManagementM_DocumentMaintain_Update" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

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
    <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
         <form id="form1" runat="server" defaultbutton="btnSave">
        <div class="formBtn formBtnleft">
             <% if (UpdatePower.HasPower) { %>
                <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server"  OnClick="btnSave_Click" />
            <%} %>
            <input type="button" id="lastBtn" value="回上一頁" class="btn" />
        </div>
        <div class="formTb">
            <table>
                <tr>
                    <th scope="row"><span class="must">*</span>發佈日期：</th>
                    <td><asp:Literal ID="lblDate" ClientIDMode="Static"  runat="server"  /></td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>標題：</th>
                    <td>
                        <asp:TextBox ID="tbTitle" CssClass="text01" ClientIDMode="Static"  runat="server"  />
                    </td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>檔案名稱：</th>
                    <td>
                        <%--<%=FilesHtml %>--%>
                        <%--{{VM.fileList}}--%>
                        <div ng-repeat="item in VM.fileList track by $index">
                            <span class="blank"ng-bind='item["DF"]'></span>
                            <span class="blank"></span>
                             <% if (UpdatePower.HasPower) { %>
                                <span class="blank">
                                    <a href="javascript:void(0);" ng-click="goDelete(item,$index)"><img src="/images/icon_del01.gif"  alt="刪除"/></a>
                                </span>
                                <%} %>
                            <br/>
                        </div>
                        <asp:FileUpload ID="tbFile" Multiple="Multiple"  CssClass="text01" ClientIDMode="Static"   runat="server"  />
                        <span class="wordred">(大小限3M以內，請勿傳空白檔案)</span>
                    </td>
                </tr>
                <tr>
                    <th scope="row">說明：</th>
                    <td>
                        <asp:TextBox ID="tbDesp" Rows="5" CssClass="text01" ClientIDMode="Static"   runat="server"  />
                    </td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>上架狀態：</th>
                    <td>
                        <asp:RadioButton GroupName="rbS" ID="rb1" ClientIDMode="Static" Text="上架" runat="server" CssClass="radio01" />
                        <asp:RadioButton GroupName="rbS" ID="rb2" ClientIDMode="Static" Text="下架" runat="server" CssClass="radio01" />
                    </td>
                </tr>
            </table>

        </div>
       </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var i =<%=ID%>;
        var fileList = <%=FileList%>;
    </script>
    <%:Scripts.Render("~/bundles/DocumentMaintain_Update_JS")%>
</asp:Content>