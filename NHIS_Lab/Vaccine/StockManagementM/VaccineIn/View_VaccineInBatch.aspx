﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_VaccineInBatch.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineIn_View_VaccineInBatch" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
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
                    <th scope="row"><span class="must">*</span>撥入日期：</th>
                    <td colspan="3">
                        <asp:Label ID="DealDate" CssClass="text02" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>撥入類型：</th>
                    <td colspan="3">
                        <asp:DropDownList ID="DealType" runat="server"></asp:DropDownList>
                        <asp:Label ID="DealHospitalName" ClientIDMode="Static" CssClass="text02" Visible="false" runat="server"></asp:Label>
                        <asp:HiddenField ID="DealHospitalID" ClientIDMode="Static" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th scope="row">備註：</th>
                    <td colspan="3">
                        <asp:Label ID="Remark" runat="server" CssClass="text03"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>撥入數量：</th>
                    <td >
                        <asp:Label ID="Num" runat="server" CssClass="text02"></asp:Label>(瓶)
                        目前庫存量:<asp:Label ID="Storage" runat="server"></asp:Label>(瓶)
                    </td>
                    <th scope="row"><span class="must">*</span>運送期間溫度：最高：</th>
                    <td >
                        <asp:Label ID="TempHigh" runat="server" CssClass="text02"></asp:Label>°C
                    </td>
                </tr>
                <tr>
                    <th scope="row">冷凍片指數：</th>
                    <td >
                        <asp:DropDownList ID="FroIdx" runat="server"></asp:DropDownList>
                    </td>
                    <th scope="row"><span class="must">*</span>運送期間溫度：最低：</th>
                    <td >
                        <asp:Label ID="TempLow" runat="server" CssClass="text02"></asp:Label>°C
                    </td>
                </tr>
                <tr>
                    <th scope="row">監視片原片指數：</th>
                    <td >
                        <asp:DropDownList ID="OriFroIdx" runat="server"></asp:DropDownList>
                    </td>
                    <%if(DownloadPower.HasPower){ %>
                    <th scope="row">上傳溫度資料檔案：</th>
                    <td >
                        <asp:LinkButton ID="DownloadFile" runat="server"></asp:LinkButton>
                    </td>
                    <%} %>
                </tr>
                <tr>
                    <th scope="row">監視片點收指數：</th>
                    <td >
                        <asp:DropDownList ID="MonIdx" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formBtn">
            <asp:Button ID="Cancel" runat="server" OnClick="Cancel_Click" Text="取消" CssClass="btn" />
        </div>   
    </form>
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/VaccineIn_JS")%>
</asp:Content> 