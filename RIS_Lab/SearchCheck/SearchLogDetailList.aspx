<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SearchLogDetailList.aspx.cs" Inherits="SearchCheck_SearchLogDetailList" %>
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
  <form>
  

    <div class="formTb" style="display :none">
      <table>
              <tr>
                <th scope="col">姓名</th>
                <td><input name="UserName" id="UserName" type="text" class="text04"></td>
              </tr> 
              <tr>
                <th scope="row">查詢日期</th>
                <td><input name="SearchDateS"    id ="SearchDateS" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd', lang: 'zh-tw' })" type="text" class="text04"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'SearchDateS',dateFmt: 'yyyy/MM/dd',lang:'zh-tw'})"></a> 至 <input name="SearchDateE"  id ="SearchDateE" type="text" class="text04" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd', lang: 'zh-tw' })"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'SearchDateE',dateFmt: 'yyyy/MM/dd',lang:'zh-tw'})" ></a></td>
              </tr> 
                    <tr>
                <th scope="row">單位</th>
                <td>  
                <input ng-model="VM.location" id="tbLocation" type="text" class="text02" ng-click="openOrgs()" />
                        <input ng-model="VM.locationID" id="hfLocationID" type="hidden"  />
                        <img  src="/images/location.png" ng-click="openOrgs()" />
                        <a href="javascript:void(0);" id="refreshBtn" ng-click="refresh()"></a>
                  
                  
                  </td>
              </tr>
               
      </table>
    </div>

       <div class="formBtn formBtncenter" style="display:none">
      <input type="button" name="send" id="SearchBtn" ng-click="changePage()" value="查詢" class="btn" />
  
    </div>

  </form>
  <div class="formTb formTb2 formTb3" >
    <table >
 <tr> <th scope="row">單位 : </th><td>{{  TM.data[0].RoleName }}</td>
 <th scope="row">姓名 : </th><td>{{  TM.data[0].UserName }}</td>
  <th scope="row">帳號 : </th><td>{{  TM.data[0].LoginName }}</td>
  </tr>  </table>
  </div>
  <!-- form end-->  
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col" style="width:5%">序號</th>
                    
                    <th scope="col">時間</th>  
 <th scope="col">條件</th>  
 <th scope="col">查詢資料總筆數</th>
 <th scope="col">實際觀看總筆數</th>

  <th scope="col">查詢原因</th>    </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' >
                    
                    </td> 
              <td class="aCenter" ng-bind='record["SearchDate"]'></td> 
                <td class="aCenter" ng-bind='record["SearchConditions"]'></td> 

                <td class="aCenter" ng-bind='record["SearchResult"]'></td> 
                 <td class="aCenter" ng-bind='record["SumCount"]'></td> 
                  <td class="aCenter" ng-bind='record["SearchReason"]'></td>  
                </tr>
            </table>
        </div>

         <div class="formBtn formBtncenter"  >
      <input type="button"  id="btnReturn" value="確定" class="btn" />
  
    </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">

 

    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>

    <script src="SearchLogDetailList.js" type="text/javascript"></script>
</asp:Content>

