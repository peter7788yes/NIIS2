<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUserProfileList.ascx.cs" Inherits="CaseMaintain_ucUserProfileList" %>


<head >
 
  <link href="/css/design.css" rel="stylesheet" type="text/css" />  
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
  
  <title></title>
</head>

 <section class="Content" id ="MyController" ng-app="MyApp" ng-controller="MyController" ng-cloak>
       
      
        <!-- form start-->
  <form>
   

    <div class="formTb formTb3">
      <table>
        <tr>
          <th width="37%" class="aCenter" scope="col">個案資料</th>
          <th width="25%" scope="col" class="aCenter">地址資料</th>
          <th width="30%" scope="col" class="aCenter">母親/父親/聯絡人資料</th>
        </tr>
        <tr>
          <td width="37%"><table>
              <tr>
                <th scope="row" style="width:50px">姓名</th>
                <td><input name="CaseName" id="CaseName" type="text" class="text01"></td>
              </tr>
              <tr>
                <th scope="row">證號</th>
                <td>
                <select id="NumberType">
                <option value="0">身份證號</option>
                 <option value="1">居留證號</option>
                 <option value="2">護照號碼</option> 
                  <option value="3">其他證號</option>
                </select>
                <input name="CaseIdNo" id="CaseIdNo" type="text" style="width:125px"></td>
              </tr>
              <tr>
                <th scope="row">出生日期</th>
                <td style="white-space: nowrap;" ><input name="BirthDateS"    id ="BirthDateS"    type="text" class="text02"><img src="/images/icon_calendar.png"  style="cursor:pointer" onclick="WdatePicker({el:'BirthDateS',dateFmt: 'yyyMMdd',lang:'zh-tw'})"  style ="width:18px">至<input name="BirthDateE"  id ="BirthDateE" type="text" class="text02"  > <img src="/images/icon_calendar.png"  style="cursor:pointer" onclick="WdatePicker({el:'BirthDateE',dateFmt: 'yyyMMdd',lang:'zh-tw'})" style ="width:18px"></td>
              </tr>
              <tr>
                <th scope="row">戶號</th>
                <td><input name="HouseNo" id ="HouseNo" type="text" class="text01"></td>
              </tr>
             
            </table></td>
          <td width="25%"  ><table>
              <tr>
                <th scope="row">地址類別</th>
                <td><select id="SelectAddrKind" class="select" style ="width:120px"> 
                    <option  value="0" >戶籍地</option>
                    <option  value="1" >通訊地</option>
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
                 <!--  order column    -->
                  <input type="hidden" name ="OrderCol" id="OrderCol" value="0" />
                  <input type="hidden" name ="OrderAsc" id="OrderAsc" value="1" />
                    <!--  order column    -->
                  </td>
              </tr>
            </table></td>
          <td width="30%"  ><table>
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
                <td><input name="ContactBirthDate" id="ContactBirthDate" type="text" class="text03"   ><img style="cursor:pointer" src="/images/icon_calendar.png" onclick="WdatePicker({el:'ContactBirthDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})"></td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div>

       <div class="formBtn formBtncenter">
      <input type="submit" name="SearchBtn" id="SearchBtn" ng-click="changePage(1)" value="查詢" class="btn" />
   <input type="button" name="btnClear" id="btnClear"    value="清空" class="btn" />
    </div>

  </form>
  <!-- form end--> 
   
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col" style="width:10px">序號</th>
                     <% if (DisplayMode == UseModule.個案訪查維護)  {  %>
                     <th scope="col" style="width:10px">訪查記錄</th>
                     <%  } %>
                       <% if (DisplayMode == UseModule.登錄預種資料)  {  %>
                     <th scope="col" style="width:10px">預種記錄</th>
                        <%  } %>


                    <th scope="col" style="width:100px" id="thOrderCol_0"  class="thOrderCol">出生日期<div  style="display:inline" id="OrderAscSign_0" class="OrderAscSign"></div></th>
                    <th scope="col" style="width:100px" id="thOrderCol_1"  class="thOrderCol">身分證號<div  style="display:inline" id="OrderAscSign_1" class="OrderAscSign"></div></th>
                    <th scope="col" style="width:100px">姓名</th>
                    <th scope="col" style="width:100px">母親姓名</th>
                    <th scope="col" style="width:100px">母親生日</th>
                    <th scope="col" style="width:100px">母親身分證號</th>
                      <% if (DisplayMode == UseModule.登錄預種資料 || DisplayMode == UseModule.個案基本資料)
                         {  %>
                    <th scope="col" style="width:100px">母親電話(日)</th>
                    <th scope="col" style="width:100px">母親電話(夜)</th>
                    <th scope="col"  >母親行動電話</th>
                         <%  } %>
                    
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' >
                    
                    </td>
                        <% if (DisplayMode == UseModule.個案訪查維護)  {  %>
                  <td class="aCenter" ><a href="javascript:void(0);"><img src="/images/icon_visits.png" ng-click="goVisit(record)" /></a></td>
                     <%  } %>
                      <% if (DisplayMode == UseModule.登錄預種資料)  {  %>
             <td class="aCenter"><a href="javascript:void(0);"><img src="/images/icon_needle.png" ng-click="goInject(record)" /></a></td>
                           <%  } %>

                   <td class="aCenter" >
                     <a href="javascript:void(0);" ng-bind='record["BD"]' ng-click="goDetail(record)" class="ng-binding"></a>
                   
                   
                   </td>
                    <td class="aCenter" ng-bind='record["I"]' >
                   
                    </td>
                   <td class="aCenter" ng-bind='record["N"]'></td>
                   <td class="aCenter" ng-bind='record["MN"]'></td>
                   <td class="aCenter" ng-bind='record["MBD"]'></td>
                   <td class="aCenter" ng-bind='record["MI"]'></td>
                    <% if (DisplayMode == UseModule.登錄預種資料 || DisplayMode == UseModule.個案基本資料)
                         {  %>
                    <td class="aCenter" ng-bind='record["TD"]'></td>
                   <td class="aCenter" ng-bind='record["TN"]'></td>
                   <td class="aCenter" style="word-break: break-all;"  ng-bind='record["MM"]'></td>
                    <%  } %>
                 </tr>
            </table>
        </div>
    </section>




    

    <script src="/js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="/js/ang/angular-1.4.8.min.js" type="text/javascript"></script> 
    <script src="/js/ang/PageM.js" type="text/javascript"></script>
    <script src="/js/ang/TableM.js" type="text/javascript"></script>
     <script src="/js/ang/FilterM.js" type="text/javascript"></script>
    <script src="/js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="/js/date/WdatePicker.js"></script> 
    <script src="/js/other/commUtil.js" type="text/javascript"></script>
    <script src="/CaseMaintain/UserProfileList.js" type="text/javascript"></script>