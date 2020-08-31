<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="VisitCaseList.aspx.cs" Inherits="CaseVisit_VisitCaseList" %>

<%@ Register src="uc_CaseDataForVisit.ascx" tagname="uc_CaseDataForVisit" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
<link href="../css/design.css" rel="stylesheet" type="text/css" /> 

 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
      
  
    <form id="form1" runat="server">
      
  
 <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak> 
  <!--路徑 start -->
  <div class="path"></div>
   
    <uc1:uc_CaseDataForVisit ID="uc_CaseDataForVisit1" runat="server" />
   
  <!--路徑 end --> 
  <!-- form start-->

  <!-- form end--> 
 <div class="button_floatleft">
 <% if (bAdd)
    { %>
    <input type="button" name="AddBtn"  id="AddBtn" value="新增記錄" class="btn" />

    <%} %>
  </div>
  <!-- page start -->
   <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
    
  <!-- page end --> 
   
  <!--表格 start -->
  <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
  <table>
  <tr>
    <th scope="col">序號</th>
    <th scope="col">訪查日期</th>
    <th scope="col">訪查人員</th>
    <th scope="col">訪查方式</th>
    <th scope="col">提早/逾期疫苗別</th>
    <th scope="col">原因</th>
    <th scope="col">訪查結果</th> 
  </tr> 

  <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' >
                    
                    </td>
                       
                   <td class="aCenter" >
                     <a href="javascript:void(0);" ng-bind='record["VD"] | SimpleTaiwanDate ' ng-click="goDetail(record)" class="ng-binding"></a>
                   
                   
                   </td>
                    <td class="aCenter" ng-bind='record["VAN"]' > </td>
                   <td class="aCenter" ng-bind='record["VTN"]'></td>
                       <td class="aCenter" ng-bind='record["VC"]'></td>
                       <td class="aCenter" ng-bind='record["VRN"]'></td>
                          <td class="aCenter" ng-bind='record["RN"]'></td>
                 </tr>
  </table> 
  </div>
  <!--表格 end --> 
</section>






    </xxxxelmt>






    </form>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">


    <script>
         var CaseID=<%=iCaseID.ToString()%>;  
     </script>


    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>
    <script src="VisitCaseList.js" type="text/javascript"></script>




</asp:Content>

