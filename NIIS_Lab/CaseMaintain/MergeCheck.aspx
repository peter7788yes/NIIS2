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

<section id="MyController" class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak> 
  <!--路徑 start -->
    <div class="path"></div>
  <!--路徑 end --> 
  <!-- form start-->
  <form>
    <div class="formBtn formBtnleft"> 
    </div>
    <div class="formTb">
      <table>
        <tr> <th scope="row">所屬單位</th>
          <td  >
          
           <input  id="tbLocation" type="text" class="text02 OrgSelect"   />
                        <input   id="hfLocationID" type="hidden"  />
                        <img class="OrgSelect" src="/images/location.png"   />
            
            <input id="hfSearchKind" type="hidden" value="1" />
         <div class="formBtn" style="display:inline">
      <input type="button" id="SearchBtn" name="send" value="查詢" class="btn" />
    </div>
     <div class="button_floatright" id="LastUpdateTime" style="display:inline"> 
     </div> 
            </td>
        </tr>
        <tr style="display:none">
          <th scope="row">出生日期</th>
         <td><input name="BirthDateS"    id ="BirthDateS" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" type="text" class="text04"><a  href="javascript:void(0);"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'BirthDateS',dateFmt: 'yyyMMdd',lang:'zh-tw'})"></a> 至 <input name="BirthDateE"  id ="BirthDateE" type="text" class="text04" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })"><a  href="javascript:void(0);"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'BirthDateE',dateFmt: 'yyyMMdd',lang:'zh-tw'})" ></a>
         
         </td>
        </tr>
      </table>
        
    </div> 
  <div class="button_floatright">
    <input type="button" name="TipMerge"  id="TipMerge"   value="重新整理一下" class="btn" />
  </div>
  <!-- form end-->
  
  <!--function start -->
  <div class="function"  >
    <ul>
      <li><a href="#" class="print">列印</a></li>
      <li><a href="#" id="ExportCSV" class="download">下載(csv)</a></li>
    </ul>
  </div>
  <!--function end --> 
   
    <br/>
      <div class="tab">
      <ul >
        <li class="here"  ><a class="taba" id="li1" href="javascript:void(0);">個案生日相同, 父或母證號相同, 胎別相同</a>
          <ul>
            <li> 
              <!--內容 start -->
                狀況：個案生日相同, 父或母證號相同, 胎別相同 => 請檢查個案同胎次序 
                 
              
  </li> 
  </ul>
        </li>
            <li   ><a id="li3" class="taba" href="javascript:void(0);">個案生日相同, 父或母證號相同</a>
          <ul>
            <li >  <!--內容 start -->
             狀況：個案生日相同, 父或母證號相同=> 請檢查個案胎別  
              <!--內容 start -->
            </li> 
  </ul>
        </li>
           <li   ><a id="li2" class="taba" href="javascript:void(0);">父或母證號相同, 個案胎別相同, 同胎次序相同</a>
          <ul>
            <li >  <!--內容 start -->
              狀況：父或母證號相同, 個案胎別相同, 個案同胎次序相同 => 請檢查個案出生日  
              
            </li> 
  </ul>
        </li>
         
      </ul>

        <div id="ListTable" style="padding-top:60px">
   
   
  <!--表格 start -->
  <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_CommonMulit.html"
  
  
  ></page-nav>
  
     <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak >
    <table>
    <thead>
      <tr>
        <th scope="col">父親或母親證號</th>
        <th scope="col">父親或母親姓名</th>
        <th scope="col">父親或母親生日</th>
        <th scope="col">個案身份證字號</th>
        <th scope="col">個案姓名</th>
        <th scope="col" class="HightLightTh" id="HightLightTh2">個案出生日期</th>
          <th scope="col" class="HightLightTh " id="HightLightTh3">胎別</th>
        <th scope="col" class="HightLightTh redcolor" id="HightLightTh1">同胎次序</th>
         
         <th scope="col">維護</th>
      </tr>
      </thead>
       <tbody ng-repeat="record in TM.data track by $index" >
       <tr  ng-class="record['GID']%2==0? 'yellowcolor' :'' "> 
            <td  class="aCenter"  ng-bind='record["MI"]'>  </td>
            <td    class="aCenter" ng-bind='record["MN"]'></td>
            <td    class="aCenter" ng-bind='record["MBD"]' > </td>
            <td    class="aCenter" ng-bind='record["I"]'  > </td>
                   <td   class="aCenter" ng-bind='record["N"]'></td> 
                   <td  class="aCenter" ng-bind='record["BD"]'></td>
                       <td    class="aCenter" ng-bind='record["BM"]' >  </td>
               <td    class="aCenter" ng-bind='record["BS"]' >  </td>
            <td  class="aCenter" style="vertical-align :middle"><a href="javascript:void(0);"><img src="/images/icon_maintain.png" ng-click="goDetail(record)" /></a></td>
          </tr>
        <tr  ng-class="record['GID']%2==0? 'yellowcolor' :'' " > 
            <td   class="aCenter" ng-bind='record["MI"]' > </td>
            <td   class="aCenter" ng-bind='record["MN"]'></td>
            <td    class="aCenter" ng-bind='record["MBD"]' > </td>
            <td    class="aCenter" ng-bind='record["I99"]' > </td>
                   <td class="aCenter" ng-bind='record["N99"]'></td> 
                   <td  class="aCenter" ng-bind='record["BD99"]'></td>
                        <td    class="aCenter" ng-bind='record["BM99"]' >  </td>
               <td  class="aCenter"   ng-bind='record["BS99"]'> </td>
     <td  class="aCenter" style="vertical-align :middle"><a href="javascript:void(0);"><img src="/images/icon_maintain.png" ng-click="goDetail99(record)" /></a></td>
         
        
          </tr>
            </tbody>
    </table>
  </div>

  <!--表格 end -->

   
   
    <!--內容 start -->
     
     <!--內容 end --> 
   
   </div>
   
    </div>
      </form>
  <!--表格 end -->
</section>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
      <script type="text/javascript">
          var UserOrg = {};
          UserOrg["text"] = '<%=OrgName %>';
          UserOrg["id"] = '<%=OrgID %>'; 
         
       </script>
    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
    <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js" type="text/javascript"></script> 
    <script src="../js/other/tab.js" type="text/javascript"></script>
      <script src="../js/other/commUtil.js" type="text/javascript"></script>
    <script src="MergeCheck.js" type="text/javascript"></script>

    
 

</asp:Content>

