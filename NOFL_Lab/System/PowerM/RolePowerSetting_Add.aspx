<%@ Page Language="C#" ViewStateMode="Enabled" AutoEventWireup="true" CodeFile="RolePowerSetting_Add.aspx.cs" Inherits="System_PowerM_RolePowerSetting_Add" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
       <%:Styles.Render("~/bundles/RolePowerSetting_Add_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content">
        <div class="path"></div>
        <div class="formBtn formBtnleft">
             <input type="button" id="lastBtn" value="回上一頁" class="btn" />
        </div>

        <div class="formTb">
           <form id="form1" runat="server" defaultbutton="btnNext">
             <table>
              <tr>
                <th scope="row"><span class="must">*</span>角色名稱：</th>
                <td><asp:TextBox ID="tbName" CssClass="text01" ClientIDMode="Static"  runat="server" /></td>
              </tr>
              <tr>
                <th scope="row">角色說明：</th>
                <td><asp:TextBox ID="tbDesp" CssClass="text01" ClientIDMode="Static"  runat="server" /></td>
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
                 <tr>
                     <td>
                         <asp:Button ID="btnNext" CssClass="btn" runat="server" Text="下一步：設定權限" PostBackUrl="~/System/PowerM/RolePowerSetting_AddPower.aspx" />
                     </td>
                    </tr>
             </table>
          </form>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
       <%:Scripts.Render("~/bundles/RolePowerSetting_Add_JS")%>
</asp:Content>


