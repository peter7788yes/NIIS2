<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentRecord_Detail.aspx.cs" Inherits="Vaccination_RecordM_StudentRecord_Detail"  MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">

<section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
   <div class="path"></div>


  <form id="form1" runat="server" defaultbutton="btnSave">
         <div class="formBtn formBtnleft">
         <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" ng-click="goAdd();" OnClick="btnSave_Click" />
         <input type="button" id="lastBtn" value="回上一頁" class="btn" />
    </div>

         <div class="formTb">
      <table>
        
        <tr>
          <th scope="row">入學年度：</th>
          <td>

             <asp:DropDownList ID="ddlYear" ClientIDMode="Static" runat="server">
                  <%-- <asp:ListItem  Text="<%# nowYear %>年" Value="<%= nowYear %>"></asp:ListItem>
                   <asp:ListItem  Text="<%= nowYear+1  %>年" Value="<%= nowYear+1%>"></asp:ListItem>--%>
              </asp:DropDownList>
             
            
              <asp:DropDownList ID="ddlSchool" ClientIDMode="Static" runat="server">
                  <%-- <asp:ListItem  Text="<%# nowYear %>年" Value="<%= nowYear %>"></asp:ListItem>
                   <asp:ListItem  Text="<%= nowYear+1  %>年" Value="<%= nowYear+1%>"></asp:ListItem>--%>
              </asp:DropDownList>



          </td>
        </tr>
        <tr>
          <th scope="row">學生人數:：</th>
          <td>
               <asp:TextBox ID="tbStudent"  CssClass="text03" ng-model="VM.tbStudent"  ng-change="changeAll()" type="number" min="0" ClientIDMode="Static" runat="server"  />
          </td>
        </tr>
      </table>
    </div>

         <div class="listTb">
    <table>
      <tr>
        <th scope="col">持卡人數</th>
        <th scope="col" >持卡率</th>
      </tr>
      <tr>
        <td > 
            <asp:TextBox ID="tbCard"   CssClass="text03" ng-model="VM.tbCard" ng-change="changePercent()" type="number" min="0" ClientIDMode="Static" runat="server"  /></td>
        <td> 
           <label ng-bind="VM.percent"></label> %
        </td>
      </tr>
    </table>
  </div>

<div class="listTb">
    <table>
      <tr>
        <th scope="col">疫苗種類</th>
        <th scope="col" width="10%">實際補種人數</th>
        <th scope="col" width="10%">接種率</th>
      </tr>

        <tr ng-repeat="record in TM.data track by $index">
                      <td ng-bind="record['EN']"></td>
                      <td class="aCenter"><input type="number" ng-model="record['Number']" ng-change="changePercent2(record)" class="text03" min="0" /></td>
                      <td class="aCenter"><label ng-bind="record['Percent']"></label> %</td>
        </tr>
    </table>
    <input style="display:none;" type="text" name="V" ng-model="VM.Vary"/>
    <input style="display:none;" type="text" name="I" ng-model="VM.Iary"/>
  </div>
  </form>
  
</section>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var tbAry = <%=tbAry%>;
        var tbAry2= <%=tbAry2%>;
        var tbCard=<%=HasYellowCardNumber%>;
        var tbStudent=<%=StudentNumber%>;
    </script>
     <%:Scripts.Render("~/bundles/StudentRecord_Detail_JS")%>
</asp:Content>
