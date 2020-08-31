<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modify_SwitchAccountFile.aspx.cs" Inherits="System_SystemSettingsM_SwitchAccountSet_Modify_SwitchAccountFile" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<section class="Content2">
<h2>申請資料</h2>
    <form id="form1" name="form1" runat="server">
        <div class="formTb formTb2">
            <table>
                <tr>    
                <th scope="row"><span class="must">*</span>檔案: </th>
                    <td>
                        <%if(DownloadPower.HasPower){ %>
                        <asp:LinkButton ID="DownloadFile" runat="server"></asp:LinkButton>
                        <br/>
                        <%} %>
                        <%if(UploadPower.HasPower){ %>
                        <asp:FileUpload ID="AccountFile" runat="server" CssClass="text02"/>
                        <%} %>
                    </td>
                </tr>
                <tr>     
                <th scope="row">說明: </th>
                    <td>
                        <asp:TextBox ID="AccountFileDesc" TextMode="MultiLine" Columns="60" Rows="15" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formBtn">
            <%if(ModifyPower.HasPower){ %>
            <asp:Button ID="Save" Text="儲存" CssClass="btn" runat="server" OnClick="Save_Click"/>
            <%} %>
            <asp:Button ID="Cancel" Text="取消" CssClass="btn" runat="server" OnClick="Cancel_Click"/>
        </div>
    </form>
</section>
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/SwitchAccountSet_JS")%>
</asp:Content>