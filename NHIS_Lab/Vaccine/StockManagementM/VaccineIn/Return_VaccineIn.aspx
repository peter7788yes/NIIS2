<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Return_VaccineIn.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineIn_Return_VaccineIn" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
       <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <div>
        <form id="form1" runat="server">
            <div class="formTb">
                <table>
                  <tr>
                  <th scope="row"><span class="must">*</span>退回原因：</th>
                    <td >
                        <asp:DropDownList ID="VaccReturn" runat="server" OnSelectedIndexChanged="VaccReturn_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="ReturnOther" runat="server" CssClass="text03" Visible="False"></asp:TextBox>
                    </td>
                  </tr>
                </table>
            </div>
            <div class="formBtn">
                <%if(NewPower.HasPower){ %>
                <asp:Button ID="Save" runat="server" CssClass="btn" Text="儲存" OnClick="Save_Click" />
                <%} %>
                <asp:Button ID="CanCel" runat="server" CssClass="btn" Text="取消" OnClick="CanCel_Click" />
           </div>
         </form>
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/VaccineIn_JS")%>
</asp:Content> 
