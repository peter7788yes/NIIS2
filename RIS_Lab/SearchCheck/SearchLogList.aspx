<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SearchLogList.aspx.cs" Inherits="SearchCheck_SearchLogList" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
  <link href="/css/design.css" rel="stylesheet" type="text/css" />  
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
    
 <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
        <!-- form start-->
  <form runat="server" id="form1">
  

    <div class="formTb  ">
      <table>
              <tr>
                <th scope="col">姓名</th>
                <td><input name="UserName" id="UserName" type="text" class="text04"></td>
              </tr> 
              <tr>
                <th scope="row">查詢年月</th>
                <td><input name="SearchDateS" value='<%=StartDate %>'     id ="SearchDateS" onclick="WdatePicker({ dateFmt: 'yyy-MM', lang: 'zh-tw' })" type="text" class="text04"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'SearchDateS',dateFmt: 'yyy-MM',lang:'zh-tw'})"></a> </td>
              </tr> 
               <tr>
                <th scope="row">單位</th>
                <td>  
                 <select id="SelectOrg" ng-model="VM.SelectOrg"   >
                            <option ng-repeat="option in VM.OrgAry" value="{{option.I}}" >{{option.O}}</option>
                  </select>
                </td>
              </tr>
                <tr>
                <th scope="row">類別</th>
                <td>  
                 <select id="SearchKind"  > 
                  <option value ="0">全部</option>
                <option value ="1">批次資料查詢</option>
                <option value ="2">單筆資料查詢</option>
                <option value ="3">批次資料勾稽</option>
                <option value ="4">親子資料勾稽</option>
                 </select>
                </td>
              </tr>
               
      </table>
    </div>

       <div class="formBtn formBtncenter">
      <input type="button" name="SearchBtn" id="SearchBtn" ng-click="changePage()" value="查詢" class="btn" />
  
    </div>

  </form>
  <!-- form end-->  
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col" style="width:5%">序號</th>
                       <th scope="col">年月</th>
                     <th scope="col">單位</th>
                    <th scope="col">姓名</th>
                    <th scope="col">帳號</th>
                     <th scope="col">類別</th>  
                    <th scope="col">筆數</th>  
                      <th scope="col">稽核狀態</th>   
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' > </td>
                       <td class="aCenter" ng-bind='record["YearMonth"]'></td>
                     <td class="aCenter" ng-bind='record["RoleName"]'></td>
                    <td class="aCenter" ng-bind='record["UserName"]' >  </td>
                     <td class="aCenter" ng-bind='record["LoginName"]'></td> 
                   <td class="aCenter" ng-bind='record["SearchKindName"]'></td> 
            <td class="aCenter" ><a ng-click='goDetail(record)'  ng-bind='record["SumCount"]'> </a></td>
              <td class="aCenter"  >
                        
                        <div ng-if='record["AuditID"]==0'>
                        <input type="button" ng-click='goAdd(record)'  value="加入稽核">
                        </div>
                        <div ng-if='record["AuditID"]!=0'>
                           <div ng-if='record["AuditResult"]!=0'>
                            已稽核
                          </div>
                           <div ng-if='record["AuditResult"]==0'>
                              <input type="button" ng-click='goDel(record)'  value="取消稽核">
                          </div>
                        </div>
              </td> 
              
                </tr>
            </table>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
 
 <script>
     var OrgData = <%=OrgData %>;
  
 </script>

    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.8.min.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>

    <script src="SearchLogList.js" type="text/javascript"></script>
</asp:Content>

