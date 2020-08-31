<%@ Page Language="C#" ViewStateMode="Enabled" AutoEventWireup="true" CodeFile="RolePowerSetting_AddPower.aspx.cs" Inherits="System_PowerM_RolePowerSetting_AddPower" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ PreviousPageType VirtualPath="~/System/PowerM/RolePowerSetting_Add.aspx" %> 
<%@ Reference VirtualPath="~/System/PowerM/RolePowerSetting_Add.aspx" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
       <%:Styles.Render("~/bundles/RolePowerSetting_AddPower_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
         <div class="formBtn formBtnleft">
                <input type="button" id="saveBtn" class="btn"  value="儲存" />
                <input type="button" id="lastBtn" class="btn"  value="回上一頁" />
         </div>
         <div class="formTb">
             <table>
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
                <td><%=orgLevelEnumString %></td>
              </tr>
             </table>
        </div>
    </section>
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
                 <th>下載</th>
                 <th>審核</th>
                 <th>整組設定</th>
             </tr>
         </table>
     </div>
        
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
       <script>
         var data = <%=MyTreeData %>;
       </script>
       <%:Scripts.Render("~/bundles/RolePowerSetting_AddPower_JS")%>
</asp:Content>


