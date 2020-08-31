<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfileDetail.aspx.cs" Inherits="CaseMaintain_UserProfileDetail" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
    <link href="/css/design.css" rel="stylesheet" type="text/css" />  
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
  
 <link href="/css/tab.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyClassCP" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">

<section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
       <div class="formBtn formBtnleft" style="display:none"><input type="button" name="send" id="btnBack"   value="回上頁" class="btn btnBack" /></div>
          

        <!-- form start-->
  <form id="form1" runat="server">

   <div class="tab">
      <ul>
        <li class="here"><a href="#" id="a2">基本資料</a>
          <ul>
            <li> 

    <div class="formTb formTb3">
  <asp:Literal ID="ltTable" runat="server"></asp:Literal>
  </div>

     <!--內容 end --> 
            </li>
          </ul>
        </li>  
          <li ><a href="#" id="a1">異動記錄</a>
          <ul>
            <li> 
             <!--內容 end --> 


   <!--表格 start -->

       <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col" width="1%">序號</th>
                             
                    <th scope="col">異動欄位</th>
                    <th scope="col">異動前資料</th>
                    <th scope="col">異動後資料</th>
                    <th scope="col" width="15%">異動時間</th>
                    <th scope="col" width="15%">申請時間</th>   
                    <th scope="col">異動來源</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["SID"]' > </td>
                   <td class="aCenter" ng-bind='record["C"]'></td>
                   <td class="aCenter" ng-bind='record["B"]'></td> 
                   <td class="aCenter" ng-bind='record["A"]'></td>
                    <td class="aCenter" ng-bind='record["MD"]'></td>
                   <td class="aCenter" ng-bind='record["AD"]'></td>
                   <td class="aCenter" ng-bind='record["FN"]'></td>
                </tr>
            </table>
        </div>
           <!--表格 start -->
            

            </li>
          </ul>
        </li>  
    </ul>
     </div >
  </form>
  <!-- form end--> 
     <div class="formBtn formBtncenter" style="display:none" ><input type="button" name="send" id="btnBack2"   value="回上頁" class="btn btnBack" /></div>
      
     </section>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
<script type="text/javascript">
var CaseID =  <%=CaseID.ToString ()%> ;
</script>
    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>
      <script src="../js/other/tab.js" type="text/javascript"></script>
    <script src="UserProfileDetail.js" type="text/javascript"></script>
</asp:Content>

