<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CodeSetting_Add.aspx.cs" Inherits="System_CodeM_CodeSetting_Add" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
    <section class="Content">
        <div class="path"></div>

         <form id="form1" runat="server" defaultbutton="btnSave" autocomplete="off">
            <div class="formBtn formBtnleft">
                 <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                 <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>

            <div class="formTb">
                 <table>
                  <tr>
                    <th scope="row">代碼：</th>
                    <td>
                        <asp:Literal ID="lblCate" ClientIDMode="Static"  runat="server"  />
                        <input name="i" id="i" type="hidden" value="<%=ID %>" />
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>代碼值:</th>
                    <td><asp:TextBox ID="tbValue" type="number" CssClass="text02" min="0" ClientIDMode="Static"  runat="server"  /></td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>代碼顯示:</th>
                    <td><asp:TextBox ID="tbName" CssClass="text02" ClientIDMode="Static"  runat="server"  /></td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>排序:</th>
                    <td><asp:TextBox ID="tbSort" type="number"  CssClass="text02"  min="0" ClientIDMode="Static"  runat="server"  /></td>
                  </tr>
                 </table>
            </div>
        </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
       <%:Scripts.Render("~/bundles/CodeSetting_Add_JS")%>
</asp:Content>


