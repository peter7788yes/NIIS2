<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ParentChildList.aspx.cs" Inherits="CaseMaintain_ParentChildList" %>
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
    
 <section  id="MyController" class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
        <!-- form start-->
  <form runat ="server" id="form1">
  

    <div class="formTb  ">
      <table> 
      <tr>
                <th scope="row">證號</th>
                <td><input name="CaseIdNo" id="CaseIdNo" type="text"class="text02" >
                
                  <a href="#" title="上傳身份證號檔案" click="javascript:void(0);"><img src="/images/icon_upload.png" alt="上傳身份證號檔案" class="UploadIdNoFiles" /></a>
                  
                </td>
              </tr> 
                <tr>
                <th scope="row"><span class="must">*</span>查詢原因</th>
                <td> 
                  <asp:TextBox ID="tbSearchReason" runat="server" CssClass="SearchReason text02"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSearchReason" runat="server" ControlToValidate="tbSearchReason" ForeColor="Red" ErrorMessage="*必填"></asp:RequiredFieldValidator>
                </td>
              </tr>
      </table>
    </div>

       <div class="formBtn formBtncenter">
         <input type="button" name="SearchBtn" id="SearchBtn"   value="查詢" class="btn" />
   
    </div>

  </form>
  <!-- form end--> 
   
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr> 
                    <th scope="col">序號</th> 
                    <th scope="col">身分證號</th>
                    <th scope="col">子女數量</th> 
                    <th scope="col">子女年次</th> 
                      <th scope="col">明細</th> 
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' >
                    
                    </td>
                     <td class="aCenter" ng-bind='record["ParentID"]'></td>
                    <td class="aCenter" ng-bind='record["ChildCount"]' ></td>
                   <td class="aCenter" ng-bind='record["ChildYear"]'></td> 
                <td class="aCenter" ><a ng-click='goDetail(record)'><img src="../images/icon_details.png" /></a></td>  
           
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
    <script src="ParentChildList.js" type="text/javascript"></script>
</asp:Content>

