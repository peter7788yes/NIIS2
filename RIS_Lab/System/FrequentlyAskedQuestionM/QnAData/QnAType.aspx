<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QnAType.aspx.cs" Inherits="System_FrequentlyAskedQuestionM_QnAData_QnAType" MasterPageFile="~/MasterPage/MasterPage.master"%>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <div id="QnADataApp" ng-controller="QnATypeController" ng-cloak>
        <form name="form1" id="form1" runat="server">
            <div class="formBtn formBtnleft">
                <%if(NewPower.HasPower){ %>
                <input class="btn" type="button" name="send" ng-click="TransferNew()" value="新增"/>
                <%} %>
                <asp:Button CssClass="btn" ID="Return" runat="server" Text="回上一頁" OnClick="Return_Click" />
            </div>
            <%if(SearchPower.HasPower){ %>
            <div class="formTb">
                <table>                    
                  <tr>
                    <th scope="row">狀態: </th>
                        <td>
                            <select ng-model="VM.SelectStatus" ng-change="Search(1)">
                            <option ng-repeat="option in VM.Status" value="{{option.EV}}" ng-bind="option.EN"></option>
                            </select>
                        </td>
                  </tr>
                </table>
            </div>
            <%} %>
         </form>
         <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
         </section>
         <div class="listTb">
            <table>
                <tr>
                    <th scope="col" width="10%">序號</th>
                    <th scope="col" width="60%">類別名稱</th>
                    <th scope="col" width="15%">狀態</th>
                    <th scope="col" width="15%">問答集數量</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aleft"><a href="javascript:void(0);" ng-bind='record["c3"]' ng-click="TransferModiy(record)"></a></td>
                    <td class="aCenter" ng-bind='GetStatus(record)' ></td>
                    <td class="aCenter" ng-bind='record["c12"]'></td>
                </tr>
            </table>
         </div>
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
        <script>
            var StatusData = <%=TypeStatus %>;
        </script>
     <%:Scripts.Render("~/bundles/QnAData_JS")%>
</asp:Content>