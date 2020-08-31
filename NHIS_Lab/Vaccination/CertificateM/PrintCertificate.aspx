<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCertificate.aspx.cs" Inherits="Vaccination_CertificateM_PrintCertificate" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content2" ContentPlaceHolderID="headJsCP" Runat="Server">
   <%=HeadScript %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cssCP" Runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="bodyClassCP" Runat="Server">
  <%=BodyClass %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ctCP" Runat="Server">
    
 <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
  <form>
 
      <div class="formBtn formBtnright">
      <input type="submit" name="send"  id="AddBtn" value="新增個案" class="btn" />
    </div>

    <div class="formTb formTb3">
      <table>
        <tr>
          <th width="33%" class="aCenter" scope="col">個案資料</th>
          <th width="33%" scope="col" class="aCenter">地址資料</th>
          <th width="33%" scope="col" class="aCenter">母親/父親/聯絡人資料</th>
        </tr>
        <tr>
          <td width="33%"><table>
              <tr>
                <th scope="row">姓名</th>
                <td><input name="CaseName" id="CaseName" type="text" class="text01"></td>
              </tr>
              <tr>
                <th scope="row">證號</th>
                <td><input name="CaseIdNo" id="CaseIdNo" type="text"class="text01" ></td>
              </tr>
              <tr>
                <th scope="row">出生日期</th>
                <td><input name="BirthDateS"    id ="BirthDateS" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" type="text" class="text02"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'BirthDateS',dateFmt: 'yyyMMdd',lang:'zh-tw'})"></a> 至 <input name="BirthDateE"  id ="BirthDateE" type="text" class="text02" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'BirthDateE',dateFmt: 'yyyMMdd',lang:'zh-tw'})" ></a></td>
              </tr>
              <tr>
                <th scope="row">戶號</th>
                <td><input name="HouseNo" id ="HouseNo" type="text" class="text01"></td>
              </tr>
              <tr>
                <th scope="row">轄區</th>
                <td>  
                <input ng-model="VM.location" id="tbLocation" type="text" class="text03" ng-click="openOrgs()" />
                        <input ng-model="VM.locationID" id="hfLocationID" type="hidden"  />
                        <img  src="/images/location.png" ng-click="openOrgs()" />
                        <a href="javascript:void(0);" id="refreshBtn" ng-click="refresh()"></a>
                  
                  
                  </td>
              </tr>
            </table></td>
          <td width="33%"><table>
              <tr>
                <th scope="row">地址類別</th>
                <td><select class="select">
                    <option value="0" selected>全部</option>
                    <option  value="1" >戶籍地</option>
                    <option  value="2" >通訊地</option>
                  </select></td>
              </tr>
              <tr>
                <th scope="row">縣/市</th>
                <td> 
                  
                  <select id="SelectCounty" ng-model="VM.SelectCounty"  ng-change="SelectCountyChange()">
                            <option ng-repeat="option in VM.CountyAry" value="{{option.I}}" ng-bind="option.N"></option>
                  </select>
                    
                  
                  </td>
              </tr>
              <tr>
                <th scope="row">鄉鎮市區</th>
                <td>
                
                  <select id="SelectTown" ng-model="VM.SelectTown"  >
                            <option ng-repeat="option in VM.TownAry" value="{{option.I}}" ng-bind="option.N"></option>
                  </select>
                  
                  
                  </td>
              </tr>
              <tr>
               
                <td colspan="2">
                 
                  
                  </td>
              </tr>
            </table></td>
          <td width="33%"><table>
              <tr>
                <th scope="row">姓名</th>
                <td><input name="ContactName" id="ContactName" type="text"  class="text01"></td>
              </tr>
              <tr>
                <th scope="row">身分證號</th>
                <td><input name="ContactIdNo" id="ContactIdNo" type="text"  class="text01"></td>
              </tr>
              <tr>
                <th scope="row">出生日期</th>
                <td><input name="ContactBirthDate" id="ContactBirthDate" type="text" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" ><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'ContactBirthDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})"></a></td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div>

       <div class="formBtn formBtncenter">
      <input type="submit" name="send" id="SearchBtn" ng-click="changePage()" value="查詢" class="btn" />
  
    </div>

  </form>
  <!-- form end--> 
   
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">檢視黃卡</th>
                    <th scope="col">證明書</th>
                    <th scope="col">出生日期</th>
                    <th scope="col">身分證號</th>
                    <th scope="col">姓名</th>
                    <th scope="col">母親姓名</th>
                    <th scope="col">母親生日</th>
                    <th scope="col">母親身分證號</th>
                    <th scope="col">電話(日)</th>
                    <th scope="col">電話(夜)</th>
                    <th scope="col">行動電話</th>
                    <th scope="col">申請紀錄</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                   <td class="aCenter" ng-bind='record["S"]' ></td>
                   <td class="aCenter"><a href="javascript:void(0);" ng-click="goDetail(record)"><img src="/images/icon_detailed.png" /></a></td>
                   <td class="aCenter"><a href="javascript:void(0" ng-click="goApplyData(record)"><img src="/images/icon_print.png" /></a></td>
                   <td class="aCenter" ><a href="javascript:void(0);" ng-bind='record["BD"] | SimpleTaiwanDate ' ng-click="goDetail(record)" class="ng-binding"></a></td>
                   <td class="aCenter" ng-bind='record["I"]' ></td>
                   <td class="aCenter" ng-bind='record["N"]'></td>
                   <td class="aCenter" ng-bind='record["MN"]'></td>
                   <td class="aCenter" ng-bind='record["MBD"] | SimpleTaiwanDate '></td>
                   <td class="aCenter" ng-bind='record["MI"]'></td>
                   <td class="aCenter" ng-bind='record["TD"]'></td>
                   <td class="aCenter" ng-bind='record["TN"]'></td>
                   <td class="aCenter" ng-bind='record["MM"]'></td>
                   <td class="aCenter"><a href="javascript:void(0);" ng-click="goRecord(record)"><img src="/images/icon_details.png" /> </a>
                   </td>
                </tr>
            </table>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="jsCP" Runat="Server">
    <%:Scripts.Render("~/bundles/PrintCertificate_JS")%>
    <script src="../../js/date/WdatePicker.js"></script>
</asp:Content>


