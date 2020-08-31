<%@ Page Language="C#" ViewStateMode="Enabled" AutoEventWireup="true" CodeFile="RolePowerSetting_AddPower.aspx.cs" Inherits="System_PowerM_RolePowerSetting_AddPower" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ PreviousPageType VirtualPath="~/System/PowerM/RolePowerSetting_Add.aspx" %> 
<%@ Reference VirtualPath="~/System/PowerM/RolePowerSetting_Add.aspx" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content">
        <div class="path"></div>
        <form id="MyForm" runat="server" ClientIDMode="Static" autocomplete="off">
             <div class="formBtn formBtnleft">
                     <asp:Button ID="btnSave" CssClass="btn" runat="server" onclick="btnSave_Click" ClientIDMode="Static" Text="儲存" />
                    <input type="button" id="lastBtn" class="btn"  value="回上一頁" />
             </div>
             <div class="formTb">
                 <table>
                  <tr>
                    <th scope="row">系統名稱：</th>
                    <td><%=RoleCateName %></td>
                  </tr>
                  <tr>
                    <th scope="row">角色名稱：</th>
                    <td><%=RoleName %></td>
                  </tr>
                  <tr>
                    <th scope="row">角色說明：</th>
                    <td><%=RoleDescription %></td>
                  </tr>
                  <tr>
                    <th scope="row">所屬層級：</th>
                    <td><%=RoleLevelName %></td>
                  </tr>
                 </table>
            </div>
             <asp:HiddenField id="hfR"  ClientIDMode="Static" runat="server"/>
             <asp:HiddenField id="hfV"  ClientIDMode="Static" runat="server"/>
        </form>
   
     <div  class="listTb">
         <table id="tableRoot">
             <tr>
                 <th>功能名稱</th>
                 <th>瀏覽</th>
                 <th>新增</th>
                 <th>修改</th>
                 <th>刪除</th>
                 <th>查詢</th>
                 <th>列印</th>
                 <th>上傳</th>
                 <th>下載</th>
                 <th>審核</th>
                 <th>整組設定</th>
             </tr>
         </table>
     </div>
     </section>
     <script>
           var CI =<%=RoleCateID %>;
           var data = <%=MyTreeData %>;
     </script>
</asp:Content>

