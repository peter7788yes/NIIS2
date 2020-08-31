<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QAView.aspx.cs" Inherits="System_FrequentlyAskedQuestionM_QAView_QAView" MasterPageFile="~/MasterPage/MasterPage.master"%>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" Runat="Server" >
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <div id="QAViewApp" ng-controller="QAViewController" ng-cloak>
        <%if(SearchPower.HasPower){ %>
        <form name="form1" id="form1" >
            <div class="formBtn formBtnleft">
                <input class="btn" type="submit" name="send" ng-click="Search(1)" value="查詢"/>
            </div>
            <div class="formTb">
                <table>
                  <tr>
                    <th scope="row">問題類別: </th>
                    <td>
                        <select ng-model="VM.QuestionType">
                        <option ng-repeat="option in VM.Type" value="{{option.ID}}" ng-bind="option.Name"></option>
                        </select>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">問題: </th>
                    <td>
                        <input ng-model="VM.Question" name="" type="text" value="" class="text03"/>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">日期: </th>
                        <td>
                            <select ng-model="VM.QAViewDateStatus">
                            <option ng-repeat="option in VM.Status" value="{{option.EV}}" ng-bind="option.EN"></option>
                            </select>
                        </td>
                  </tr>
                </table>
            </div>    
         </form>
         <%} %>
         <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
         </section>
         <div class="listTb">
            <table>
                <tr>
                    <th scope="col" width="10%">序號</th>
                    <th scope="col" width="15%">發佈日期</th>
                    <th scope="col" width="20%">問題類別</th>
                    <th scope="col" width="40%">問題</th>
                    <th scope="col" width="15%">點閱次數</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" ng-bind='TransformROCDate(record.c11)' ></td>
                    <td class="aCenter" ng-bind='GetQuestionType(record)'></td>
                    <td class="aCenter"><a href="javascript:void(0);" ng-bind='record["c4"]' ng-click="TransferView(record)"></a></td>
                    <td class="aCenter" ng-bind='record["c10"]'></td>
                </tr>
            </table>
         </div>
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
        <script>
            var StatusData = <%=QAViewDateStatus %>;
            var TypeData = <%=QnAType %>;
        </script>
     <%:Scripts.Render("~/bundles/QAView_JS")%>
</asp:Content>
