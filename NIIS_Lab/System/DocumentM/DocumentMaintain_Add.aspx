<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentMaintain_Add.aspx.cs" Inherits="System_DocumentManagementM_DocumentMaintain_Add" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content">
        <div class="path"></div>
            <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
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
        <div class="formBtn">
            <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server"  OnClick="btnSave_Click" />
            <input type="button" id="lastBtn" value="回上一頁" class="btn" />
        </div>
        <input name="hash" type="hidden" value="<%=GetString("hash") %>"/>
       </form>
    </section>
</asp:Content>


