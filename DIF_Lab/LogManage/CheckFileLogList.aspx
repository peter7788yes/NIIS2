<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="CheckFileLogList.aspx.cs" Inherits="LogManage_CheckFileLogList" %>
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
    
    <style>
    /* in the css file or in a style block */
.angular-with-newlines {
    white-space: pre-wrap;
}
    
    </style>

 <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
        <!-- form start-->
  <form id="form1" runat="server">
     <div class="formBtn formBtnright" style="display:none">
     <a href="/LogManage/LogImportSetting.aspx" target="_blank" class="btn" >介接設定(暫放)</a>
     <input type="button" name="btnExe"  id="btnExe" value="執行轉置" class="btn" />
    </div>
    <div class="formTb">
      <table>
        <tr>
          <th scope="row"><span class="must">*</span>檔案日期：</th>
          <td><input name="CreateDateS" value='<%=StartDate %>'   id ="CreateDateS" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd', lang: 'zh-tw' })" type="text" class="text04"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'CreateDateS',dateFmt: 'yyyy/MM/dd',lang:'zh-tw'})"></a> 至 <input name="CreateDateE"  value='<%=EndDate %>' id ="CreateDateE" type="text" class="text04" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd', lang: 'zh-tw' })"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'CreateDateE',dateFmt: 'yyyy/MM/dd',lang:'zh-tw'})" ></a></td>
        </tr>
           
        <tr>
          <th scope="row"><span class="must">*</span>狀態：</th>
          <td>  
          <select name="LogStatus" id ="LogStatus">
          <option value ="0" selected >全部</option>
          <option value ="1">檔案數異常</option>
          <option value ="2">處理資料筆數異常</option>
          </select>   &nbsp;   
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
                    <th scope="col">檔案日期</th> 
                    <th scope="col">應收檔案數</th>
                   <th scope="col">實收檔案數</th>
                    <th scope="col">檢查時間</th> 
                    <th scope="col">完成時間</th> 
                    <th scope="col">列表</th>
        <th scope="col">摘要</th>
                    
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                     <td class="aCenter" ng-bind='record["S"]'></td> 
                       <td class="aCenter" ng-bind='record["FileDate"]'></td> 
                         <td class="aCenter" ng-bind='record["MustCount"]'></td> 
                          <td class="aCenter" ng-bind='record["ActuallyCount"]'></td> 
                           <td class="aCenter" ng-bind='record["CheckDate"]'></td>  
                               <td class="aCenter" ng-bind='record["FinishDate"]'></td>  
                   <td class="aCenter" ><a ng-click='goDetail(record)'><img src="../images/icon_details.png" /></a></td>  
                   <td >
                   <div  onclick="javascript:$(this).parent().children('.Summary').toggle();" style ="text-align :center "><img  src="../images/icon_browse.png" /></div>
                   <div class="Summary angular-with-newlines" ng-bind='record["TransferMsg"]' style="display:none">
                  
                   </div></td> 
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
    <script src="../js/other/commUtil.js" type="text/javascript"></script>
    <script src="CheckFileLogList.js" type="text/javascript"></script>
</asp:Content>

