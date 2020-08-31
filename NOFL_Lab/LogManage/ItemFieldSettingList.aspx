<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ItemFieldSettingList.aspx.cs" Inherits="LogManage_ItemFieldSettingList" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
  <%:Styles.Render("~/bundles/RolePowerSetting_CSS")%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyClassCP" Runat="Server">
  <%=BodyClass %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
    
 <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
        <!-- form start-->
  <form>
  
    <div class="formTb">
      <table>
       
        <tr>
          <th scope="row"><span class="must">*</span>介接項目：</th>
          <td>
        

           </td>
        </tr>
        <tr>
          <th scope="row"><span class="must">*</span>介接項目欄位：</th>
          <td>  
          

          </td>
        </tr>
         <tr>
          <th scope="row"><span class="must">*</span>匯入欄位：</th>
          <td>  
          

          </td>
        </tr>
      </table>
    </div>  
           <div class="formBtn formBtncenter" ><input type="submit" name="send" id="SearchBtn" ng-click="changePage()" value="查詢" class="btn" /></div>
         

  </form>
  <!-- form end--> 
   
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr> 
                    <th scope="col" style="width :5%">序號</th>
                      <th scope="col" style="width :5%">取消通知</th> 
                    <th scope="col">設定日期</th> 
                    <th scope="col">通知方式</th>
                   <th scope="col">通知數量</th>
                    <th scope="col">設定人員</th> 
                    <th scope="col">狀態</th>
                  
                    
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' > </td>
                      <td class="aCenter"  ><input type="button" value ="取消" class="UrgeCancle" ng-click="goCancel(record)"   /> </td>
                         <td class="aCenter" ng-bind='record["CD"] | SimpleTaiwanDate' > </td>
                             <td class="aCenter" ng-bind='record["UTN"]'></td>  
                   <td class="aCenter" >
                     <a href="javascript:void(0);" ng-bind='record["UC"] ' ng-click="goDetail(record)" class="ng-binding"></a>
                    </td>
                
                   <td class="aCenter" ng-bind='record["UN"]'></td> 
                   <td class="aCenter" ng-bind='record["USN"]'></td> 
                  
                 </tr>
            </table>
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

    <script src="ItemFieldSettingList.js" type="text/javascript"></script>
</asp:Content>

