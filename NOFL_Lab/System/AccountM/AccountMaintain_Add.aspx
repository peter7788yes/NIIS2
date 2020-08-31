<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountMaintain_Add.aspx.cs" Inherits="System_AccountM_AccountMaintain_Add" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <%:Styles.Render("~/bundles/CodeSetting_Add_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content">
        <div class="path"></div>

        <form id="form1" runat="server" defaultbutton="btnSave">
            <div class="formBtn formBtnleft">
                <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>

            <div class="formTb">
                <table>
                    <tr>
                        <th scope="row"><span class="must">*</span>帳號:</th>
                        <td>
                            <asp:TextBox ID="tbAccount" CssClass="text01" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>姓名:</th>
                        <td>
                            <asp:TextBox ID="tbValue" CssClass="text01" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>身分證號:</th>
                        <td>
                            <asp:TextBox ID="tbName" CssClass="text01" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>電話:</th>
                        <td>
                            <asp:TextBox ID="tbSort" CssClass="text01" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>電子信箱:</th>
                        <td>
                            <asp:TextBox ID="TextBox2" CssClass="text01" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>單位:</th>
                        <td>
                            <asp:TextBox ID="tbDept" CssClass="text01" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row">職稱:</th>
                        <td>
                            <asp:TextBox ID="TextBox4" CssClass="text01" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>所屬角色:</th>
                        <td>
                            <asp:TextBox ID="TextBox5" CssClass="text01" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>申請事由:</th>
                        <td>
                            <asp:TextBox ID="TextBox6" CssClass="text01" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row">備註:</th>
                        <td>
                            <asp:TextBox ID="TextBox7" CssClass="text01" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row">申請單:</th>
                        <td>
                            <asp:FileUpload ID="tbFile"  CssClass="text01" ClientIDMode="Static"   runat="server"  /><span style="color:blue;">(格式不限，大小限3M以內)</sapn></td>
                    </tr>
                </table>
            </div>
        </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/CodeSetting_Add_JS")%>
</asp:Content>


