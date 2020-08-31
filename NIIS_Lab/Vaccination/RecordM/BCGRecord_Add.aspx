<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BCGRecord_Add.aspx.cs" Inherits="Vaccination_RecordM_BCGRecord_Add"  MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
<section class="Content" ng-app="MyApp" ng-controller="MyController">
   <div class="path"></div>
  <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off">
    <div class="formBtn formBtnleft">
         <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
         <input type="button" id="lastBtn" value="回上一頁" class="btn" />
    </div>
    <div class="formTb  formTb6">
      <table>
        <tr>
          <td colspan="2">
             <asp:TextBox ID="tbOrg" CssClass="text03"  ClientIDMode="Static" runat="server" Enabled="false" />
          </td>
        </tr>
        <tr>
          <th scope="row">統計區間：</th>
          <td>
              <%--<select >
              <option selected="selected" value="<%=nowYear %>"><%=nowYear %>年度</option>
              <option value="<%=nowYear+1 %>"><%=nowYear+1 %>年度</option>
            </select>
            <select >
              <option value="1" selected="selected">第一季</option>
              <option value="2">第二季</option>
              <option value="3">第三季</option>
              <option value="4">第四季</option>
            </select>--%>
              <asp:DropDownList ID="ddlYear" ClientIDMode="Static" runat="server">
                  <%-- <asp:ListItem  Text="<%# nowYear %>年" Value="<%= nowYear %>"></asp:ListItem>
                   <asp:ListItem  Text="<%= nowYear+1  %>年" Value="<%= nowYear+1%>"></asp:ListItem>--%>
              </asp:DropDownList>
               <asp:DropDownList ID="ddlSeason" ClientIDMode="Static" runat="server">
                 <%--   <asp:ListItem Text="第一季" Value="1"></asp:ListItem>
                    <asp:ListItem Text="第二季" Value="2"></asp:ListItem>
                    <asp:ListItem Text="第三季" Value="3"></asp:ListItem>
                    <asp:ListItem Text="第四季" Value="4"></asp:ListItem>--%>
               </asp:DropDownList>
          </td>
        </tr>
      </table>
    </div>
    <div class="formTb  formTb6">
      <table>
        <tr>
          <th scope="row" class="width01">出生人數：</th>
          <td><asp:TextBox ID="tbBirthNumber" type="number" min="0" ng-change="changeAll()" ng-model="VM.tbBirthNumber" ClientIDMode="Static" runat="server" /></td>
        </tr>
      </table>
    </div>
    <div class="formTb  formTb6">
      <table>
        <tr>
          <th  rowspan="2" class="width01">直接接種人數：</th>
          <th scope="row">新生兒：</th>
          <td><asp:TextBox ID="tbKid" type="number" min="0" ng-model="VM.tbKid"  ng-change="changeAll()" ClientIDMode="Static" runat="server" /></td>
        </tr>
        <tr>
          <th scope="row">新生兒：</th>
          <td><asp:TextBox ID="tbBaby" type="number" min="0" ng-model="VM.tbBaby" ng-change="changeAll()" ClientIDMode="Static" runat="server" /></td>
        </tr>
      </table>
    </div>
    <div class="formTb  formTb6">
      <table>
        <tr>
          <th rowspan="3" class="width01">幼兒無疤：</th>
          <th scope="row">測驗數：</th>
          <td><asp:TextBox ID="tbBabyNoScar1" type="number" min="0" ng-model="VM.tbBabyNoScar1" ng-change="changeAll()" ClientIDMode="Static" runat="server" /></td>
        </tr>
        <tr>
          <th scope="row">陽性數：</th>
          <td><asp:TextBox ID="tbBabyNoScar2" type="number" min="0" ng-model="VM.tbBabyNoScar2" ng-change="changeAll()" ClientIDMode="Static" runat="server" /></td>
        </tr>
        <tr>
          <th scope="row">接種數：</th>
          <td><asp:TextBox ID="tbBabyNoScar3" type="number" min="0" ng-model="VM.tbBabyNoScar3"  ng-change="changeAll()" ClientIDMode="Static" runat="server" /></td>
        </tr>
      </table>
    </div>
    <div class="formTb  formTb6">
      <table>
        <tr>
          <th rowspan="3" class="width01">國小一年級<br/>
            學童無疤：</th>
          <th scope="row">測驗數：</th>
          <td> <asp:TextBox ID="tbKidNoScar1" type="number" min="0" ng-model="VM.tbKidNoScar1"  ng-change="changeAll()" ClientIDMode="Static" runat="server" /></td>
        </tr>
        <tr>
          <th scope="row">陽性數：</th>
          <td> <asp:TextBox ID="tbKidNoScar2" type="number" min="0" ng-model="VM.tbKidNoScar2" ng-change="changeAll()" ClientIDMode="Static" runat="server" /></td>
        </tr>
        <tr>
          <th scope="row">接種數：</th>
          <td> <asp:TextBox ID="tbKidNoScar3" type="number" min="0" ng-model="VM.tbKidNoScar3" ng-change="changeAll()" ClientIDMode="Static" runat="server" /></td>
        </tr>
      </table>
    </div>
    <div class="formTb  formTb6">
      <table>
        <tr>
          <th rowspan="3" class="width01">其他無疤：</th>
          <th scope="row">測驗數：</th>
          <td><asp:TextBox ID="tbOtherNoScar1" type="number" min="0" ng-model="VM.tbOtherNoScar1" ng-change="changeAll()"  ClientIDMode="Static" runat="server" /></td>
          <th rowspan="3" class="width01">其他有疤：</th>
          <th scope="row">測驗數：</th>
          <td><asp:TextBox ID="tbOtherHasScar1" type="number" min="0" ng-model="VM.tbOtherHasScar1" ng-change="changeAll()" ClientIDMode="Static" runat="server" /></td>
        </tr>
        <tr>
          <th scope="row">陽性數：</th>
          <td><asp:TextBox ID="tbOtherNoScar2" type="number" min="0" ng-model="VM.tbOtherNoScar2" ng-change="changeAll()" ClientIDMode="Static" runat="server" /></td>
          <th scope="row">陽性數：</th>
          <td><asp:TextBox ID="tbOtherHasScar2" type="number" min="0" ng-model="VM.tbOtherHasScar2" ng-change="changeAll()"  ClientIDMode="Static" runat="server" /></td>
        </tr>
        <tr>
          <th scope="row">接種數：</th>
          <td><asp:TextBox ID="tbOtherNoScar3" type="number" min="0" ng-model="VM.tbOtherNoScar3" ng-change="changeAll()" ClientIDMode="Static" runat="server" /></td>
          <th scope="row">&nbsp;</th>
          <td>&nbsp;</td>
        </tr>
      </table>
    </div>
    <div class="formTb">
    <table>
  <tr>
    <td width="120" rowspan="3" scope="row">總計：</td>
    <th>測驗數：</th>
    <td>
        <asp:TextBox ID="tbAll1"  type="number" min="0"  ng-model="VM.all1" Enabled="false" ClientIDMode="Static" runat="server" />
  </tr>
  <tr>
    <th>陽性數：</th>
    <td><asp:TextBox ID="tbAll2" type="number" min="0"  ng-model="VM.all2"  Enabled="false" ClientIDMode="Static" runat="server" /></td>
  </tr>
  <tr>
    <th>接種數：</th>
    <td><asp:TextBox ID="tbAll13" type="number" min="0"  ng-model="VM.all3" Enabled="false"  ClientIDMode="Static" runat="server" /></td>
  </tr>
</table>
    </div>
  </form>
</section>
</asp:Content>
