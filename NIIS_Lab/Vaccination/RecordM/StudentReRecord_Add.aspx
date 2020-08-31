<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentReRecord_Add.aspx.cs" Inherits="Vaccination_RecordM_StudentReRecord_Add" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
<section class="Content" ng-app="MyApp" ng-controller="MyController">
   <div class="path"></div>
  <form id="MyForm" ClientIDMode="Static" runat="server"  autocomplete="off">
     <div class="formTb">
      <table>
        <tr>
          <th scope="row">入學年度：</th>
          <td>
               <asp:DropDownList ID="ddlYear" ClientIDMode="Static" runat="server">
               </asp:DropDownList>
                <select name="ss" ng-model="VM.selectSchool"  >
                                   <option value="0">請選擇學校名稱</option>
                                   <option ng-repeat="option in VM.sAry" value="{{option.I}}" ng-bind="option.N"></option>
                </select>
                <asp:DropDownList ID="ddlST" ClientIDMode="Static" runat="server">
                      <asp:ListItem  Value="1" Text="一年級"></asp:ListItem>
                      <asp:ListItem  Value="2" Text="二年級"></asp:ListItem>
                </asp:DropDownList>
          </td>
        </tr>
      </table>
    </div>
<div class="listTb">
    <table>
      <tr>
        <th scope="col">疫苗種類</th>
        <th scope="col" width="10%">應補種人數</th>
        <th scope="col" width="10%">實際補種人數</th>
        <th scope="col" width="10%">完成率</th>
      </tr>
        <tr ng-repeat="record in TM.data track by $index" repeat-callback="repeatCallback()">
                      <td ng-bind="record['EN']"></td>
                      <td class="aCenter"><input type="number" ng-model="record['sNumber']" ng-change="changePercent2(record)" class="text03" min="0" /></td>
                      <td class="aCenter"><input type="number" ng-model="record['Number']" ng-change="changePercent2(record)" class="text03" min="0" /></td>
                      <td class="aCenter"><label ng-bind="record['Percent']"></label> %</td>
        </tr>
    </table>
    <input style="display:none;" type="text" name="V" ng-model="VM.Vary"/>
    <input style="display:none;" type="text" name="I" ng-model="VM.Iary"/>
    <input style="display:none;" type="text" name="S" ng-model="VM.Sary"/>
  </div>
     <div class="formBtn">
         <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" ng-click="goAdd();" OnClick="btnSave_Click" />
         <input type="button" id="lastBtn" value="回上一頁" class="btn" />
     </div>
  </form>
</section>
    <script>
        var tbAry = <%=tbAry%>;
        var sAry = <%=sAry %>;
    </script>
</asp:Content>