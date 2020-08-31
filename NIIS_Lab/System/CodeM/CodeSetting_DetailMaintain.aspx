<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CodeSetting_DetailMaintain.aspx.cs" Inherits="System_CodeM_CodeSetting_DetailMaintain"  MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content">
        <div class="path"></div>
         <form id="MyForm" ClientIDMode="Static" runat="server"  autocomplete="off">
            <div class="formBtn formBtnright">
                 <asp:Button CssClass="btn" ID="btnRemove" Text="刪除" runat="server" OnClick="btnRemove_Click" />
            </div>
            <div class="formTb">
                 <table>
                  <tr>
                    <th scope="row">代碼：</th>
                    <td>
                        <asp:Literal ID="lblCate" ClientIDMode="Static"  runat="server"  />
                        <input name="i" id="i" type="hidden" value="<%=ID %>" />
                        <input name="ci" id="ci" type="hidden" value="<%=CateID %>" />
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">代碼值:</th>
                    <td>
                        <asp:Literal ID="lblValue" ClientIDMode="Static"  runat="server"  />
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>代碼顯示:</th>
                    <td><asp:TextBox ID="tbName" CssClass="text02" ClientIDMode="Static"  runat="server"  /></td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>狀態:</th>
                    <td>
                        <asp:RadioButton GroupName="rbS" ID="rb1" ClientIDMode="Static" Text="啟用" runat="server" CssClass="radio01" />
                        <asp:RadioButton GroupName="rbS" ID="rb2" ClientIDMode="Static" Text="停用" runat="server" CssClass="radio01" />
                    </td>
                  </tr>
                 </table>
            </div>
            <div class="formBtn">
                 <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                 <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>
            <input name="hash" type="hidden" value="<%=GetString("hash") %>"/>
        </form>
    </section>
</asp:Content>


