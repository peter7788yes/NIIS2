<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QnAData.aspx.cs" Inherits="System_FrequentlyAskedQuestionM_QnAData_QnAData" MasterPageFile="~/MasterPage/MasterPage.master"%>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <div id="QnADataApp" ng-controller="QnADataController" ng-cloak>
        <form name="form1" id="form1" >
            <%if(NewPower.HasPower){ %>
            <div class="formBtn formBtnleft">
                <input class="btn" type="submit" name="send" ng-click="TransferNew()" value="新增問答"/>
                <input class="btn" type="submit" name="send" ng-click="TransferType()" value="問答類別管理"/>
            </div>
            <%} %>
            <%if(SearchPower.HasPower){ %>
            <div class="formTb">
                <table>
                  <tr>
                    <th scope="row">問題類別: </th>
                    <td>
                        <select ng-model="VM.QuestionType">
                        <option ng-repeat="option in VM.Type" value="{{option.EnumValue}}" ng-bind="option.EnumName"></option>
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
                      <th scope="row">發佈日期：</th>
                      <td>
                        <input id="PublishedStarDate" ng-model="VM.PublishedStarDate" type="text" onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" onchange="SetDate()" />
                        <a href="#"><img onclick="WdatePicker({el:'PublishedStarDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"></a>至
                        <input id="PublishedEndDate" ng-model="VM.PublishedEndDate" type="text" onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})"  onchange="SetDate()" />
                        <a href="#"><img onclick="WdatePicker({el:'PublishedEndDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"></a>
                      </td>
                  </tr>
                  <tr>
                    <th scope="row">狀態: </th>
                        <td>
                            <select ng-model="VM.PublishedStatus">
                            <option ng-repeat="option in VM.Status" value="{{option.EV}}" ng-bind="option.EN"></option>
                            </select>
                        </td>
                  </tr>
                </table>
            </div>
            <div class="formBtn">
                <input class="btn" type="submit" name="send" ng-click="Search(1)" value="查詢"/>
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
                    <th scope="col" width="10%">發佈日期</th>
                    <th scope="col" width="20%">問題類別</th>
                    <th scope="col" width="10%">狀態</th>
                    <th scope="col" width="40%">問題</th>
                    <th scope="col" width="10%">點閱次數</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" ng-bind='TransformROCDate(record.c11)' ></td>
                    <td class="aCenter" ng-bind='GetQuestionType(record)'></td>
                    <td class="aCenter" ng-bind='GetStatus(record)'></td>
                    <td class="aCenter"><a href="javascript:void(0);" ng-bind='record["c4"]' ng-click="TransferModiy(record)"></a></td>
                    <td class="aCenter" ng-bind='record["c10"]'></td>
                </tr>
            </table>
         </div>
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
        var StatusData = <%=PublishedStatus %>;
        var TypeData = <%=QnAType %>;
     </script>
     <script src="/js/date/WdatePicker.js"></script>
     <%:Scripts.Render("~/bundles/QnAData_JS")%>
</asp:Content>