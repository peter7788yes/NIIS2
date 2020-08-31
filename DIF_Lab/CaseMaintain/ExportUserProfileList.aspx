<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ExportUserProfileList.aspx.cs" Inherits="CaseMaintain_ExportUserProfileList" %>
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
    
 <section id="MyController" class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
        <!-- form start-->
  <form id="form1" runat ="server" >
  
    <div class="formTb   formTb3">
      <table>
        <tr>
          <td>
          <table>
              <tr>
                <th scope="col">姓名</th>
                <td><input name="CaseName" id="CaseName" type="text" class="text04"></td>
              </tr>
              <tr>
                <th scope="row">證號</th>
                <td><input name="CaseIdNo" id="CaseIdNo" type="text"class="text02" >
                
                <a href="#" title="上傳身份證號檔案" click="javascript:void(0);"><img src="/images/icon_upload.png" alt="上傳身份證號檔案" class="UploadIdNoFiles" />
                
                </a>
                
                </td>
              </tr>
              <tr>
                <th scope="row">出生日期</th>
                <td><input name="BirthDateS"    id ="BirthDateS" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" type="text" class="text04"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'BirthDateS',dateFmt: 'yyyMMdd',lang:'zh-tw'})"></a> 至 <input name="BirthDateE"  id ="BirthDateE" type="text" class="text04" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'BirthDateE',dateFmt: 'yyyMMdd',lang:'zh-tw'})" ></a></td>
              </tr> 
                
              <tr>
                <th scope="row">戶籍縣市鄉鎮</th>
                <td> 
                  
                  <select id="SelectCounty" ng-model="VM.SelectCounty"  ng-change="SelectCountyChange()">
                            <option ng-repeat="option in VM.CountyAry" value="{{option.I}}" ng-bind="option.N"></option>
                  </select>
                     
                  <select id="SelectTown" ng-model="VM.SelectTown"  >
                            <option ng-repeat="option in VM.TownAry" value="{{option.I}}" ng-bind="option.N"></option>
                  </select>
                  
                  
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
 </td>
        </tr>
        <tr>
          <td>
		  <table>
              <tr>
                <th scope="row">遮蔽欄位</th>
                <td><label for="">
                    <input name="" type="checkbox" value="">
                    姓名</label>
                  <label for="">
                    <input name="" type="checkbox" value="">
                    身分證號</label>
                  <label for="">
                    <input name="" type="checkbox" value="">
                    出生日期</label>
                  <label for="">
                    <input name="" type="checkbox" value="">
                    戶籍地址</label></td>
              </tr>
            </table>
			</td>
        </tr>
        <tr>
          <td><div class="formTb4">
              <div class="title">報表欄位</div>
               
              <asp:CheckBoxList ID="cblReportFields" runat="server"  RepeatColumns="5" RepeatLayout="Table" RepeatDirection="Horizontal" ></asp:CheckBoxList>
             
            </div></td>
        </tr>
      </table>
    </div>

       <div class="formBtn formBtncenter">
          <input type="button" name="send" id="SearchBtn"   value="查詢" class="btn" style="display:none" />
      <asp:Button ID="btnExport" runat="server" Text="匯出" CssClass ="btn" 
               onclick="btnExport_Click"></asp:Button>
    </div>

  </form>
  <!-- form end--> 
   
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col">序號</th>
                     <th scope="col">姓名</th>
                    <th scope="col">身分證號</th>
                    <th scope="col">出生日期</th>
                    <th scope="col">戶籍地址</th> 
                    <th scope="col">生親姓名</th>
                    <th scope="col">生父姓名</th> 
                    <th scope="col">瀏覽</th>  
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' >
                    
                    </td>
                     <td class="aCenter" ng-bind='record["Name"]'></td>
                    <td class="aCenter" ng-bind='record["IDNO"]' >  </td>
                  
                   <td class="aCenter" ng-bind='record["BirthDate"]'></td>
                   <td class="aCenter" ng-bind='record["ResAddr"]  '></td>
                   <td class="aCenter" ng-bind='record["MotherName"]'></td>
                    <td class="aCenter" ng-bind='record["FatherName"]'></td>
            <td class="aCenter" ><a ng-click='goDetail(record)'><img src="../images/icon_details.png" /></a></td>  
                </tr>
            </table>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">

 

    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
       <script src="../js/ang/angular-1.4.8.min.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>

    <script src="ExportUserProfileList.js" type="text/javascript"></script>
</asp:Content>

