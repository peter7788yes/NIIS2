﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="UserContact.aspx.cs" Inherits="CaseMaintain_UserContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
<link href="/css/design.css" rel="stylesheet" type="text/css">
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
<section class="Content2" ng-app="MyApp" ng-controller="MyController" ng-cloak>
  <h2>聯絡人資訊</h2>
  
  <!-- form start-->
  <form id="form1" runat="server">
    <!--表格 end -->
    <hr>
    <div class="formTb formTb2 formTb3">
      <table>
        <tr>
          <th scope="row">姓名：</th>
          <td><asp:Literal ID="ltName" runat="server"></asp:Literal></td>
           <th scope="row" class="aRight">身分證號：</th>
          <td><asp:Literal ID="ltIdNo" runat="server"></asp:Literal></td>
           <th scope="row" class="aRight">出生日期：</th>
          <td><asp:Literal ID="ltBirthDate" runat="server"></asp:Literal></td>
        </tr>
        
        <tr>
          <th scope="row" >聯絡人關係：</th>
          <td colspan="5" >
           <asp:DropDownList ID="ddlRS" runat="server"></asp:DropDownList>
           <asp:RequiredFieldValidator ID="rfvRS" ControlToValidate ="ddlRS" InitialValue ="" runat="server"  ForeColor="Red" ErrorMessage="*必填"></asp:RequiredFieldValidator>
             <asp:CheckBox ID="cbMain" runat="server"></asp:CheckBox>
            為主要連絡人</td>
        </tr>
        <tr>
          <th scope="row">電話(日)：</th>
          <td  colspan="5">
          <asp:TextBox ID="tbTelDayArea"   runat="server" Width="50px"></asp:TextBox>
         <asp:TextBox ID="tbTelDayNo" runat="server"></asp:TextBox>
            分機
            <asp:TextBox ID="tbTelDayExt" runat="server"></asp:TextBox>
            
            </td>
        </tr>
        <tr>
          <th scope="row">電話(夜)：</th>
          <td  colspan="5">
          <asp:TextBox ID="tbTelNightArea" runat="server" Width="50px"></asp:TextBox>
         <asp:TextBox ID="tbTelNightNo" runat="server"></asp:TextBox>
            分機
            <asp:TextBox ID="tbTelNightExt" runat="server"></asp:TextBox>
            
            </td>
        </tr>
        <tr>
          <th scope="row">行動電話：</th>
          <td  colspan="5"> 
            <div id="MobileDIV" class="MobileDIV"  runat="server"  > </div> 
            <div id="MobileSample" style="display:none">
            <input name="tbMobileNo" type="text" class="text02" /><a onclick ="javascript:void(0);" class="DelMobile"><img src="/images/icon_del.png" /></a><a onclick ="javascript:void(0);" class="AddMobile"><img src="/images/icon_increase.png" /></a>
             </div>
          </td>
        </tr>
        <tr>
          <th scope="row">電子郵件：</th>
          <td  colspan="5">
           <div id="EmailDIV" class="EmailDIV"  runat="server"  > </div> 
            <div id="EmailSample" style="display:none">
            <input name="tbEmail" type="text" class="text02" /><a onclick ="javascript:void(0);" class="DelEmail"><img src="/images/icon_del.png" /></a><a onclick ="javascript:void(0);" class="AddEmail"><img src="/images/icon_increase.png" /></a>
             </div>
          </td>
        </tr>
      </table>
    </div>
    <div class="formBtn">
     <asp:Button ID="btnSave" runat="server" Text="確認" CssClass="btn" 
            onclick="btnSave_Click"></asp:Button>
      <input type="button" name="cancel" id="cancel" value="取消" class="btn" />
    </div>
  
  </form>
  <!-- form end--> 
  
</section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">

 
    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script> 
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
<script language="javascript" type ="text/javascript">
 
$(function () {
 

    $(document).on("click", "#cancel", function (e) {
        window.close();
        e.preventDefault();
        return false;
    });

    $(document).on("click", ".DelMobile,.DelEmail", function (e) {
        $(this).parent().html('');
    });
    $(document).on("click", ".AddMobile", function (e) { 
        $(".MobileDIV").append("<div>" + $("#MobileSample").html() + "</div>"); 
    }); 
    $(document).on("click", ".AddEmail", function (e) { 
        $(".EmailDIV").append("<div>" + $("#EmailSample").html() + "</div>"); 
    });


    $(".MobileDIV").append("<div>" + $("#MobileSample").html() + "</div>");
    $(".EmailDIV").append("<div>" + $("#EmailSample").html() + "</div>");

});

angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
         } ]);


</script>


</asp:Content>
