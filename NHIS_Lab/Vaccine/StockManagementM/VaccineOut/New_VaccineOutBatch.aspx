<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New_VaccineOutBatch.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineOut_New_VaccineOutBatch" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
                        <th scope="row">
                            批號：</th>
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
                        <th scope="row"><span class="must">*</span>撥出數量：</th>
                        <td >
                            <asp:TextBox ID="Num" runat="server" CssClass="text02"></asp:TextBox>(瓶)
                            目前庫存量:<asp:Label ID="Storage" runat="server"></asp:Label>(瓶)
                        </td>
                        <th scope="row"><span class="must">*</span>運送期間溫度：最高：</th>
                        <td >
                            <asp:TextBox ID="TempHigh" runat="server" CssClass="text02"></asp:TextBox>°C
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">冷凍片指數：</th>
                        <td >
                            <asp:DropDownList ID="FroIdx" runat="server"></asp:DropDownList>
                        </td>
                        <th scope="row"><span class="must">*</span>運送期間溫度：最低：</th>
                        <td >
                            <asp:TextBox ID="TempLow" runat="server" CssClass="text02"></asp:TextBox>°C
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">監視片原片指數：</th>
                        <td >
                            <asp:DropDownList ID="OriFroIdx" runat="server" Enabled="False"></asp:DropDownList>
                        </td>
                        <%if(UploadPower.HasPower){ %>
                        <th scope="row">上傳溫度資料檔案：</th>
                        <td >
                            <asp:FileUpload ID="TempFile" runat="server" />
                        </td>
                        <%} %>
                    </tr>
                    <tr>
                        <th scope="row">監視片點收指數：</th>
                        <td >
                            <asp:DropDownList ID="MonIdx" runat="server"></asp:DropDownList>
                        </td>
                        <%if(UploadPower.HasPower){ %>
                        <th scope="row"></th>
                        <td >
                            (檔案大小限3M，檔案格式不限)
                        </td>
                        <%} %>
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
     <%:Scripts.Render("~/bundles/VaccineOut_JS")%>
</asp:Content> 