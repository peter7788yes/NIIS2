<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RolePowerSetting_Detail.aspx.cs" Inherits="System_PowerM_RolePowerSetting_Update" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
       <link href="/css/design.min.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
   
    <section class="Content">
        <div class="path"></div>
         <form id="MyForm" runat="server"  autocomplete="off"> 
            <div class="formBtn formBtnleft">
                  <% if (UpdatePower.HasPower) { %>
                        <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                    <% } %>
              
                 <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>

            <div class="formTb">
                 <table>
                  <tr>
                    <th scope="row"><span class="must">*</span>角色名稱：</th>
                    <td>
                        <asp:TextBox ID="tbName" CssClass="text01" required="required" ClientIDMode="Static"  runat="server"  />
                        <input type="hidden" id="i" name="i" value="<%=ID %>" />
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">角色說明：</th>
                    <td>
                        <asp:TextBox ID="tbDesp" CssClass="text01" ClientIDMode="Static"  runat="server"  />
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>所屬層級：</th>
                      <td>
                            <asp:RadioButton GroupName="rbLevel" ID="rb1" ClientIDMode="Static" Text="中央" runat="server" CssClass="radio01" />
                            <asp:RadioButton GroupName="rbLevel" ID="rb2" ClientIDMode="Static" Text="區管中心" runat="server" CssClass="radio01" />
                            <asp:RadioButton GroupName="rbLevel" ID="rb3" ClientIDMode="Static" Text="局" runat="server" CssClass="radio01" />
                            <asp:RadioButton GroupName="rbLevel" ID="rb4" ClientIDMode="Static" Text="所" runat="server" CssClass="radio01" /> 
                      </td>
                  </tr>
                 </table>
            </div>
            <input name="hash" type="hidden" value="<%=GetString("hash") %>"/>
        </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
       <script>
            var UP =<%=UpdatePower.HasPower ? 1 :0%>; 
       </script>
     <%:Scripts.Render("~/bundles/RolePowerSetting_Detail_JS")%>
</asp:Content>
