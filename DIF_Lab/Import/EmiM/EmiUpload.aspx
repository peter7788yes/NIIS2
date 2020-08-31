<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="EmiUpload.aspx.cs" Inherits="Import_EmiM_EmiUpload" Async="true" %>

<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ctCP" runat="Server">
    <div class="path"></div>
    <form id="form1" runat="server">
        <div class="formBtn formBtnleft">
            <input class="btn" type="button" name="send" onclick="location.href = 'ImportList.aspx'" value="匯入紀錄" />
        </div>
        <div class="formTb">
            <table>
                <tr>
                    <td ><span class="wordred">說明: 檔案限定為TXT，以逗號分隔</span></td>
                </tr>
                <tr>
                    <td>
                        <asp:FileUpload ID="tbFile" CssClass="text01" ClientIDMode="Static" runat="server" />
                        <asp:Button CssClass="btn" ID="btnSave" Text="上傳匯入" runat="server" OnClick="btnSave_Click" />
                </tr>
            </table>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jsCP" runat="Server">
    <script src="/js/sys/menuPath.js" type="text/javascript"></script>
</asp:Content>

