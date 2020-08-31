<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="LogIdFilterList.aspx.cs" Inherits="LogManage_LogIdFilterList" %>
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
    
 <section class="Content" id="MyController" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
        <!-- form start-->
  <form id="form1" runat="server">
  
    <div class="formTb">
      <table>
        <tr>
          <th scope="row"><span class="must">*</span>身分證號：</th>
          <td> 
          <asp:TextBox ID="tbIdNo" CssClass="IdNo" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="rfvtbIdNo" runat="server" ControlToValidate="tbIdNo" ForeColor="Red" ErrorMessage="*必填"></asp:RequiredFieldValidator>
             
           <div class="formBtn  " style="display :inline"  ><input type="button" name="SearchBtn" id="SearchBtn"  value="查詢" class="btn" /></div>
         </td>
        </tr>
        <tr  style ="display:none">
          <th scope="row">介接項目：</th>
          <td>
          <div style="width:480px;word-wrap:break-word;">
           
          <asp:CheckBoxList ID="cblLogItem" runat="server"  RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass ="cblLogItem">
 
 <asp:ListItem Value="1">出生登記</asp:ListItem>
 <asp:ListItem Value="2">死亡登記</asp:ListItem>
 <asp:ListItem Value="3">結婚登記</asp:ListItem>
 <asp:ListItem Value="4">離婚登記</asp:ListItem>
 <asp:ListItem Value="5">原住民身分登記</asp:ListItem>
  <asp:ListItem Value="6">遷入登記</asp:ListItem>
   <asp:ListItem Value="7">遷出登記</asp:ListItem>
    <asp:ListItem Value="8">住址變更</asp:ListItem>
 <asp:ListItem Value="9">初設戶籍</asp:ListItem>
   <asp:ListItem Value="10">統號更正</asp:ListItem>
    <asp:ListItem Value="11">姓名變更</asp:ListItem>
 <asp:ListItem Value="12"> 姓名更正</asp:ListItem>
 <asp:ListItem Value="13"> 撤銷遷入</asp:ListItem>
     <asp:ListItem Value="14">撤銷遷出</asp:ListItem>
    

    </asp:CheckBoxList>
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
                    <th scope="col" style="width :5%">序號</th> 
                    <th scope="col">檔案日期</th> 
                    <th scope="col">檔案名稱</th>
                   <th scope="col">項目</th>  
                    <th scope="col">檢視內容</th>
                  
                    
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                     <td class="aCenter" ng-bind='record["S"]'></td> 
                       <td class="aCenter" ng-bind='record["FileDate"]'></td> 
                         <td class="aCenter" ng-bind='record["FileName"]'></td> 
                          <td class="aCenter" ng-bind='record["LogItemName"]'></td>   
                   <td class="aCenter" ><a ng-click='goDetail(record)'><img src="../images/icon_file01.png" /></a></td>  
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

    <script src="LogIdFilterList.js" type="text/javascript"></script>
</asp:Content>

