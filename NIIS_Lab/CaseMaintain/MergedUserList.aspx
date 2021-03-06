﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MergedUserList.aspx.cs" Inherits="CaseMaintain_MergedUserList" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
  <link href="/css/design.css" rel="stylesheet" type="text/css" />  
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyClassCP" Runat="Server">
  <%=BodyClass %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
    
 <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      <p></p>
        <!-- form start-->
  <form>
  

    <div class="formTb">
      <table> 
              <tr>
                <th scope="row">所屬單位</th>
                <td>  
                <input ng-model="VM.location" id="tbLocation" type="text" class="text02" ng-click="openOrgs()" />
                        <input ng-model="VM.locationID" id="hfLocationID" type="hidden"  />
                        <img  src="/images/location.png" ng-click="openOrgs()" />
                        <a href="javascript:void(0);" id="refreshBtn" ng-click="refresh()"></a>
                  
                   <div class="formBtn" style="display:inline">
      <input type="submit" name="send" id="SearchBtn" ng-click="changePage()" value="查詢" class="btn" />
  
    </div><div class="button_floatright" id="LastUpdateTime" style="display:inline"> 
     </div> 
                  </td>
              </tr> 
      </table>
    </div>

      

  </form>
  <!-- form end--> 
   
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col">序號</th>
               
                    <th scope="col">個案身份證字號</th>
                    <th scope="col">個案姓名</th>
                         <th scope="col">個案出生日期</th>
                    <th scope="col">聯絡人姓名</th>
                    <th scope="col">聯絡人電話(日)</th>
                    <th scope="col">聯絡人電話(夜)</th>
                    <th scope="col">聯絡人行動電話</th>
                    <th scope="col">維護</th>
             
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' >
                    
                    </td>
                       
                   
                    <td class="aCenter" ng-bind='record["I"]' >
                   
                    </td>
                   <td class="aCenter" ng-bind='record["N"]'></td>
<td class="aCenter"  ng-bind='record["BD"]' >< 
                   
                   
                   </td>


                   <td class="aCenter" ng-bind='record["MN"]'></td>
                    <td class="aCenter" ng-bind='record["TD"]'></td>
                   <td class="aCenter" ng-bind='record["TN"]'></td>
                   <td class="aCenter" style="word-break: break-all;" ng-bind='record["MM"]'></td>
                    <td class="aCenter"><a href="javascript:void(0);"><img src="/images/icon_maintain.png" ng-click="goDetail(record)" /></a></td>
        
                </tr>
            </table>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">

 <script type="text/javascript">
     var UserOrg = {};
     UserOrg["text"] = '<%=OrgName %>';
     UserOrg["id"] = '<%=OrgID %>';
 </script>


    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>
      <script src="../js/other/commUtil.js" type="text/javascript"></script>
    <script src="MergedUserList.js" type="text/javascript"></script>
</asp:Content>

