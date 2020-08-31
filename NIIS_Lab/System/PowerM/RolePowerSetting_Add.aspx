<%@ Page Language="C#" ViewStateMode="Enabled" AutoEventWireup="true" CodeFile="RolePowerSetting_Add.aspx.cs" Inherits="System_PowerM_RolePowerSetting_Add" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content">
        <div class="path"></div>
        <div class="formBtn formBtnleft">
             <input type="button" id="lastBtn" value="回上一頁" class="btn" />
        </div>
           <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
        <div class="formTb">
             <table>
               <tr>
                <th scope="row"><span class="must">*</span>系統名稱：</th>
                <td>
                    <asp:DropDownList required="required" ID="ddlCate" CssClass="text01"  ClientIDMode="Static"  runat="server">
                        <asp:ListItem Value="">請選擇</asp:ListItem>
                    </asp:DropDownList>
                    <input type="hidden" name="hfCateID" id="hfCateID" value="<%=RoleCateID.ToString() %>" />
                    <input type="hidden" name="hfCateName" id="hfCateName" value="<%=RoleCateName %>" />
                </td>
              </tr>
              <tr>
                <th scope="row"><span class="must">*</span>角色名稱：</th>
                <td>
                    <asp:TextBox required="required" ID="tbName" CssClass="text01" ClientIDMode="Static"  runat="server" />
                </td>
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

