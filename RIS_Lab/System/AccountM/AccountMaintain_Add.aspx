<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountMaintain_Add.aspx.cs" Inherits="System_AccountM_AccountMaintain_Add" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <link href="/css/design.min.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content">
        <div class="path"></div>
        <form id="form1" runat="server"  autocomplete="off">
            <div class="formTb">
                <table>
                    <tr>
                        <th scope="row"><span class="must">*</span>帳號:</th>
                        <td>
                            <asp:TextBox ID="tbAccount" CssClass="text02" required="required" ClientIDMode="Static" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="cbP" Text="業務承辦人" CssClass="text02" ClientIDMode="Static"  runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>姓名:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbName" CssClass="text02" required="required" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>身分證號:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbRID" CssClass="text02" required="required" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>電話:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbPhone" CssClass="text02" required="required" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>電子信箱:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbEmail" type="email" CssClass="text03" required="required" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>單位:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbDept" Enabled="false" CssClass="text03" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">職稱:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbTitle" CssClass="text02" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>所屬角色:</th>
                        <td colspan="2">
                             <asp:CheckBoxList ID="cbList"  RepeatColumns="4"  RepeatDirection="Vertical"  ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>申請事由:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbR1"  Columns="70" Rows="5" TextMode="MultiLine"  CssClass="text01" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">備註:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbR2"  Columns="70" Rows="5" TextMode="MultiLine"   CssClass="text01" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">申請單:</th>
                        <td colspan="2">
                           <%-- <asp:FileUpload ID="tbFile"
                                accept="application/pdf,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                                Multiple="Multiple" CssClass="text01" ClientIDMode="Static"   runat="server"  />--%>
                             <asp:FileUpload ID="tbFile"
                                accept="application/pdf,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                                Multiple="Multiple" CssClass="text01" ClientIDMode="Static"   runat="server"  />
                            <span style="color:blue;">(格式不限，大小限3M以內)</span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="formBtn">
                <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>
        </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
     <%:Scripts.Render("~/bundles/AccountMaintain_Add_JS")%>
</asp:Content>


