<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentMaintain_Add.aspx.cs" Inherits="System_DocumentManagementM_DocumentMaintain_Add" MasterPageFile="~/MasterPage/MasterPage.master" %>

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
            <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server"  OnClick="btnSave_Click" />
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
    <%:Scripts.Render("~/bundles/DocumentMaintain_Add_JS")%>
</asp:Content>


