<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseUserRemark_Add.aspx.cs" Inherits="Vaccination_RecordM_CaseUserRemark_Add" MasterPageFile="~/MasterPage/MasterPage.master" %>
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

    <section class="Content2">
        <h2>新增個案備註</h2>
       <form id="form1" runat="server" defaultbutton="btnSave">
            <div class="formTb formTb2">
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddCate" CssClass="text01"  ClientIDMode="Static"  runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="tbRemark" Columns="75" Rows="6"  TextMode="MultiLine" CssClass="text01" ClientIDMode="Static"  runat="server"  />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="formBtn">
                <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                <input type="button" name="closeBtn" value="取消" class="btn" onclick="window.close();"/>
            </div>
        </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/CaseUserRemark_Add_JS")%>
    <script src="../../js/date/WdatePicker.js"></script>
</asp:Content>

