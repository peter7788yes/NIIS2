<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="LogDetail.aspx.cs" Inherits="LogManage_LogDetail" %>
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
 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">

<section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
       <div class="formBtn formBtnleft" ><input type="button" name="send" id="btnBack"   value="回上頁" class="btn btnBack" /></div>
         
        <!-- form start-->
  <form id="form1" runat="server">
  <br/>
    <div class="formTb formTb3">
  <asp:Literal ID="ltTable" runat="server"></asp:Literal>
  </div>
  </form>
  <!-- form end--> 
     <div class="formBtn formBtncenter" ><input type="button" name="send" id="btnBack2"   value="回上頁" class="btn btnBack" /></div>
      
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

    <script src="LogDetail.js" type="text/javascript"></script>
</asp:Content>

