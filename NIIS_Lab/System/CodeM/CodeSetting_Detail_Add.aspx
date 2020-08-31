<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CodeSetting_Detail_Add.aspx.cs" Inherits="System_CodeM_CodeSetting_Detail_Add" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content">
        <div class="path"></div>
         <form id="MyForm" ClientIDMode="Static" runat="server"  autocomplete="off">
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
                        <input name="ci" id="ci" type="hidden" value="<%=ID %>" />
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>代碼值:</th>
                    <td><asp:TextBox ID="tbValue" required="required" type="number" min="0" max="255" CssClass="text02"  ClientIDMode="Static"  runat="server"  /></td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>代碼顯示:</th>
                    <td><asp:TextBox ID="tbName"  required="required" CssClass="text02" ClientIDMode="Static"  runat="server"  /></td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>排序:</th>
                    <td><asp:TextBox ID="tbSort" required="required" type="number" min="0" max="255"   CssClass="text02"   ClientIDMode="Static"  runat="server"  /></td>
                  </tr>
                 </table>
            </div>
            <input name="hash" type="hidden" value="<%=GetString("hash") %>"/>
        </form>
    </section>
</asp:Content>


