<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginConfirm_VaccineIn.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineIn_LoginConfirm_VaccineIn" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
                        <asp:Literal ID="VaccineID" runat="server"></asp:Literal>
                        -<asp:Literal ID="BatchType" runat="server"></asp:Literal>
                    </td>
                    <th scope="row">
                        批號：</th>
                    <td >
                        <asp:Literal ID="BatchID" runat="server"></asp:Literal>
                        -<asp:Literal ID="FormDrug" runat="server"></asp:Literal>
                    </td>
                  </tr>
                </table>
            </div>    
            <div class="formTb formTb2">
                <table>
                    <tr>
                        <th scope="row"><span class="must">*</span>撥入日期：</th>
                        <td colspan="3">
                            <asp:TextBox ID="DealDate" CssClass="text02" runat="server" ></asp:TextBox>
                            <a href="#"><asp:Image ID="DealDateImg" ImageUrl="/images/icon_calendar.png" runat="server"></asp:Image></a>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>撥入類型：</th>
                        <td colspan="3">
                            <asp:DropDownList ID="DealType" runat="server" >
                            </asp:DropDownList>
                            <asp:TextBox ID="DealHospitalName" ClientIDMode="Static" CssClass="text02" Visible="false" runat="server" Enabled="False"></asp:TextBox>
                            <asp:HiddenField ID="DealHospitalID" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">備註：</th>
                        <td colspan="3">
                            <asp:TextBox ID="Remark" runat="server" CssClass="text03"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>撥入數量：</th>
                        <td >
                            <asp:TextBox ID="Num" runat="server" CssClass="text02"></asp:TextBox>
                        </td>
                        <th scope="row"><span class="must">*</span>運送期間溫度：最高：</th>
                        <td >
                            <asp:TextBox ID="TempHigh" runat="server" CssClass="text02"></asp:TextBox>°C
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">冷凍片指數：</th>
                        <td >
                            <asp:DropDownList ID="FroIdx" runat="server">
                            </asp:DropDownList>
                        </td>
                        <th scope="row"><span class="must">*</span>運送期間溫度：最低：</th>
                        <td >
                            <asp:TextBox ID="TempLow" runat="server" CssClass="text02"></asp:TextBox>°C
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">監視片原片指數：</th>
                        <td >
                            <asp:DropDownList ID="OriFroIdx" runat="server">
                            </asp:DropDownList>
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
                        <td>
                            <asp:DropDownList ID="MonIdx" runat="server">
                            </asp:DropDownList>
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
     <script src="/js/date/WdatePicker.js"></script>
     <%:Scripts.Render("~/bundles/VaccineIn_JS")%>
</asp:Content> 
