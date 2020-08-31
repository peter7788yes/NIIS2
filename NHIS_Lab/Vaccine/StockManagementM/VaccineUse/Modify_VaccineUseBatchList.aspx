<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modify_VaccineUseBatchList.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineUse_Modify_VaccineUseBatchList" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
        <form name="form1" id="form1" runat="server">
            <div class="formTb formTb2">
                <table>
                    <tr>
                        <th scope="row">疫苗：</th>
                        <td >
                            <asp:Label ID="VaccineID" runat="server"></asp:Label>
                            -<asp:Label ID="BatchType" runat="server"></asp:Label>
                        </td>
                        <th scope="row">批號：</th>
                        <td >
                            <asp:Label ID="BatchID" runat="server"></asp:Label>
                            -<asp:Label ID="FormDrug" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>    
            <div class="formTb formTb2">
                <table>
                    <tr>
                        <th scope="row"><span class="must">*</span>領用數量：</th>
                        <td >
                            <asp:TextBox ID="Num" runat="server" CssClass="text02"></asp:TextBox>(瓶)
                            目前庫存量:<asp:Label ID="Storage" runat="server"></asp:Label>(瓶)
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>領用用途：</th>
                        <td >
                            <asp:DropDownList ID="UseType" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="formBtn">
                <%if(NewPower.HasPower){ %>
                <asp:Button ID="Save" runat="server" OnClick="Save_Click" Text="儲存" CssClass="btn" />
                <%} %>
                <asp:Button ID="Cancel" runat="server" OnClick="Cancel_Click" Text="取消" CssClass="btn" />
            </div>   
         </form>
    </div>
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/VaccineUse_JS")%>
</asp:Content> 

