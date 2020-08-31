<%@ Page Language="C#" ViewStateMode="Enabled" AutoEventWireup="true" CodeFile="RolePowerSetting_Add.aspx.cs" Inherits="System_PowerM_RolePowerSetting_Add" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
        <div class="formBtn formBtnleft">
             <input type="button" id="lastBtn" value="回上一頁" class="btn" />
        </div>
        <form id="MyForm" runat="server" autocomplete="off">
        <div class="formTb">
             <table>
               <tr>
                <th scope="row"><span class="must">*</span>系統名稱：</th>
                <td>
                    <asp:DropDownList ID="ddlCate" CssClass="text01"  ClientIDMode="Static"  runat="server">
                        <asp:ListItem Value="0">請選擇</asp:ListItem>
                        <asp:ListItem Value="1">全國性預防接種資訊管理系統</asp:ListItem>
                        <asp:ListItem Value="2">院所版預防接種子系統</asp:ListItem>
                        <asp:ListItem Value="3">離線版預防接種子系統</asp:ListItem>
                        <asp:ListItem Value="4">戶政資料管理子系統</asp:ListItem>
                        <asp:ListItem Value="5">國際預防接種系統</asp:ListItem>
                    </asp:DropDownList>

                    <input type="hidden" name="hfCateID" id="hfCateID" value="<%=RoleCateID.ToString() %>" />
                    <input type="hidden" name="hfCateName" id="hfCateName" value="<%=RoleCateName %>" />
                </td>
              </tr>
              <tr>
                <th scope="row"><span class="must">*</span>角色名稱：</th>
                <td>
                    <asp:TextBox ID="tbName" CssClass="text01" ClientIDMode="Static"  runat="server" />
                </td>
              </tr>
              <tr>
                <th scope="row">角色說明：</th>
                <td><asp:TextBox ID="tbDesp" CssClass="text01" ClientIDMode="Static"  runat="server" /></td>
              </tr>
              <tr>
                <th scope="row"><span class="must">*</span>所屬層級：</th>
                  <td>
                        <asp:RadioButton GroupName="rbLevel" ID="rb1"  ClientIDMode="Static" Text="中央" runat="server" CssClass="radio01" />
                        <asp:RadioButton GroupName="rbLevel" ID="rb2" ClientIDMode="Static" Text="區管中心" runat="server" CssClass="radio01" />
                        <asp:RadioButton GroupName="rbLevel" ID="rb3" ClientIDMode="Static" Text="局" runat="server" CssClass="radio01" />
                        <asp:RadioButton GroupName="rbLevel" ID="rb4" ClientIDMode="Static" Text="所" runat="server" CssClass="radio01" /> 
                  </td>
              </tr>
                 <tr>
                     <td>
                          <asp:Button ID="btnNext" CssClass="btn" runat="server"  ClientIDMode="Static" Text="下一步：設定權限" PostBackUrl="~/System/PowerM/RolePowerSetting_AddPower.aspx" />
                          <%--<asp:Button ID="btnNext" CssClass="btn" runat="server" Text="下一步：設定權限" onclick="btnNext_Click" />--%>
                     </td>
                    </tr>
             </table>
        </div>
             <asp:HiddenField ID="hash"  ClientIDMode="Static"  runat="server"/>
          </form>

    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
       <%:Scripts.Render("~/bundles/RolePowerSetting_Add_JS")%>
</asp:Content>


