<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MergeCheck.aspx.cs" Inherits="CaseMaintain_MergeCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
<link href="/css/design.css" rel="stylesheet" type="text/css">
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
 <link href="/css/tab.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
   
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">

<section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak> 
  <!--路徑 start -->
    <div class="path"></div>
  <!--路徑 end --> 
  <!-- form start-->
  <form>
    <div class="formBtn formBtnleft"> 
    </div>
    <div class="formTb">
      <table>
        <tr>
          <td colspan="2">
          
           <input ng-model="VM.location" id="tbLocation" type="text" class="text03" ng-click="openOrgs()" />
                        <input ng-model="VM.locationID" id="hfLocationID" type="hidden"  />
                        <img  src="/images/location.png" ng-click="openOrgs()" />
                        <a href="javascript:void(0);" id="refreshBtn" ng-click="refresh()"></a>
            
            
            </td>
        </tr>
        <tr>
          <th scope="row">出生日期</th>
         <td><input name="BirthDateS"    id ="BirthDateS" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" type="text" class="text02"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'BirthDateS',dateFmt: 'yyyMMdd',lang:'zh-tw'})"></a> 至 <input name="BirthDateE"  id ="BirthDateE" type="text" class="text02" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'BirthDateE',dateFmt: 'yyyMMdd',lang:'zh-tw'})" ></a>
         
         <div class="formBtn" style="display:inline">
      <input type="button" id="SearchBtn" name="send" value="查詢" class="btn" />
    </div>
         </td>
        </tr>
      </table>
        
    </div> 
  <div class="button_floatright">
    <input type="button" name="TipMerge"  id="TipMerge"   value="合併個案" class="btn" />
  </div>
  <!-- form end-->
  
  <!--function start -->
  <div class="function">
    <ul>
      <li><a href="#" class="print">列印</a></li>
      <li><a href="#" id="ExportCSV" class="download">下載(csv)</a></li>
    </ul>
  </div>
  <!--function end --> 
   
    <br/>
      <div class="tab">
      <ul class="tabul">
        <li class="here" ><a href="#" id="li1" ng-click="changePage(1)" >個案生日相同, 父或母證號相同</a>
          <ul>
            <li> 
              <!--內容 start -->
               
  
  <!--表格 start -->
  <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_CommonMulit.html"></page-nav>
  
     <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
    <table>
      <caption>
      狀況：個案生日相同, 父或母證號相同 => 請檢查個案同胎次序
      </caption>
      <tr>
        <th scope="col">父親或母親證號</th>
        <th scope="col">父親或母親姓名</th>
        <th scope="col">父親或母親生日</th>
        <th scope="col">個案身份證字號</th>
        <th scope="col">個案姓名</th>
        <th scope="col">個案出生日期</th>
        <th scope="col" class="redcolor">同胎次序</th>
        
      </tr>
       <tr ng-repeat="record in TM.data track by $index"  > 
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter"  >
            
             <a href="javascript:void(0);" ng-bind='record["MI"]' ng-click="goMotherDetail(record)" class="ng-binding"></a>
            </td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["MN"]'></td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["MBD"] | SimpleTaiwanDate ' > </td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter"  >
              <a href="javascript:void(0);" ng-bind='record["I"]' ng-click="goDetail(record)" class="ng-binding"></a>
                  
                    </td>
                   <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["N"]'></td>
                
                   <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["BD"] | SimpleTaiwanDate '></td>
               <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter"  >
                <a href="javascript:void(0);" ng-bind='record["BS"]' ng-click="goDetail(record)" class="ng-binding"></a>
               </td>
                 </tr>
        
    </table>
  </div>

  <!--表格 end -->


  </li> 
  </ul>
        </li>

           <li ><a href="#"  id="li2"  ng-click="changePage2(1)">父或母證號相同, 個案同胎次序相同</a>
          <ul>
            <li> 
              <!--內容 start -->

  <!--表格 start -->
  <page-nav  ng-model="PM2" on-change-page-d="changePage2(pgIndex)" template-url="/html/ang_template/PageNav_CommonMulit.html"></page-nav>
     <div id="tmBlock2" style="display:none;" class="listTb"  ng-cloak>
    <table>
      <caption>
     父或母證號相同, 個案同胎次序相同 => 請檢查個案出生日
      </caption>
      <tr>
        <th scope="col">父親或母親證號</th>
        <th scope="col">父親或母親姓名</th>
        <th scope="col">父親或母親生日</th>
        <th scope="col">個案身份證字號</th>
        <th scope="col">個案姓名</th>
        <th scope="col" class="redcolor">個案出生日期</th>
        <th scope="col" >同胎次序</th>
        
      </tr>
       <tr ng-repeat="record in TM2.data track by $index"  > 
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter"  >
              <a href="javascript:void(0);" ng-bind='record["MI"]' ng-click="goMotherDetail(record)" class="ng-binding"></a>
            </td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["MN"]'></td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["MBD"] | SimpleTaiwanDate ' > </td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter"  >
              <a href="javascript:void(0);" ng-bind='record["I"]' ng-click="goDetail(record)" class="ng-binding"></a>
                  
                    </td>
                   <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["N"]'></td>
                
                   <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" >
                    <a href="javascript:void(0);" ng-bind='record["BD"] | SimpleTaiwanDate ' ng-click="goDetail(record)" class="ng-binding"></a>
                   
                   </td>
               <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["BS"]'></td>
                 </tr>
        
    </table>
  </div>
  <!--表格 end -->
     <!--內容 end --> 
           
            </li> 
  </ul>
        </li>

           <li  ><a href="#"  id="li3" ng-click="changePage3(1)">個案生日相同, 同胎次序相同, 父或母出生日相同</a>
          <ul>
            <li> 
              <!--內容 start -->
            <!--表格 start -->
  <page-nav  ng-model="PM3" on-change-page-d="changePage3(pgIndex)" template-url="/html/ang_template/PageNav_CommonMulit.html"></page-nav>
     <div id="tmBlock3" style="display:none;" class="listTb"  ng-cloak>
    <table>
      <caption>
     個案生日相同, 同胎次序相同, 父或母出生日相同 => 請檢查個案父/母證號
      </caption>
      <tr>
        <th scope="col" class="redcolor">父親或母親證號</th>
        <th scope="col">父親或母親姓名</th>
        <th scope="col">父親或母親生日</th>
        <th scope="col">個案身份證字號</th>
        <th scope="col">個案姓名</th>
        <th scope="col" >個案出生日期</th>
        <th scope="col" >同胎次序</th>
        
      </tr>
       <tr ng-repeat="record in TM3.data track by $index"  > 
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter"  >
                <a href="javascript:void(0);" ng-bind='record["MI"]' ng-click="goMotherDetail(record)" class="ng-binding"></a>
            </td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["MN"]'></td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["MBD"] | SimpleTaiwanDate ' > </td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter"  >
              <a href="javascript:void(0);" ng-bind='record["I"]' ng-click="goDetail(record)" class="ng-binding"></a>
                  
                    </td>
                   <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["N"]'></td>
                
                   <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["BD"] | SimpleTaiwanDate '></td>
               <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["BS"]'></td>
                 </tr>
        
    </table>
  </div>
  <!--表格 end -->

              <!--內容 end -->
            </li> 
  </ul>
        </li>

           <li ><a href="#" id="li4"  ng-click="changePage4(1)">個案生日相同, 同胎次序相同, 父或母姓名相同</a>
          <ul>
            <li> 
              <!--內容 start -->
                <!--表格 start -->
  <page-nav  ng-model="PM4" on-change-page-d="changePage4(pgIndex)" template-url="/html/ang_template/PageNav_CommonMulit.html"></page-nav>
     <div id="tmBlock4" style="display:none;" class="listTb"  ng-cloak>
    <table>
      <caption>
     個案生日相同, 同胎次序相同, 父或母姓名相同 => 請檢查個案父/母證號
      </caption>
      <tr>
        <th scope="col" class="redcolor">父親或母親證號</th>
        <th scope="col">父親或母親姓名</th>
        <th scope="col">父親或母親生日</th>
        <th scope="col">個案身份證字號</th>
        <th scope="col">個案姓名</th>
        <th scope="col" >個案出生日期</th>
        <th scope="col" >同胎次序</th>
        
      </tr>
       <tr ng-repeat="record in TM4.data track by $index"  > 
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter"  >
              <a href="javascript:void(0);" ng-bind='record["MI"]' ng-click="goMotherDetail(record)" class="ng-binding"></a>
            </td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["MN"]'></td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["MBD"] | SimpleTaiwanDate ' > </td>
            <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter"  >
              <a href="javascript:void(0);" ng-bind='record["I"]' ng-click="goDetail(record)" class="ng-binding"></a>
                  
                    </td>
                   <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["N"]'></td>
                
                   <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["BD"] | SimpleTaiwanDate '></td>
               <td ng-class="record['GID']%2==0? 'aCenter yellowcolor' :'aCenter' "  class="aCenter" ng-bind='record["BS"]'></td>
                 </tr>
        
    </table>
  </div>
  <!--表格 end -->
               <!--內容 end -->
           </li>
          </ul>
        </li>
      </ul>
    </div>
      </form>
  <!--表格 end -->
</section>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">

    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
    <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js" type="text/javascript"></script> 
    <script src="../js/other/tab.js" type="text/javascript"></script>
    <script src="MergeCheck2.js" type="text/javascript"></script>

</asp:Content>

