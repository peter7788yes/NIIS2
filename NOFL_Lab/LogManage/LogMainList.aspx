<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="LogMainList.aspx.cs" Inherits="LogManage_LogMainList" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
  <%:Styles.Render("~/bundles/RolePowerSetting_CSS")%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyClassCP" Runat="Server">
  <%=BodyClass %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
    
 <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
        <!-- form start-->
  <form id="form1" runat="server">
  
    <div class="formTb">
      <table>
        <tr>
          <th scope="row"><span class="must">*</span>檔案日期：</th>
          <td><input name="CreateDateS"    id ="Text1" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd', lang: 'zh-tw' })" type="text" class="text04"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'CreateDateS',dateFmt: 'yyyy/MM/dd',lang:'zh-tw'})"></a> 至 <input name="CreateDateE"  id ="Text2" type="text" class="text04" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd', lang: 'zh-tw' })"><a href="#"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'CreateDateE',dateFmt: 'yyyy/MM/dd',lang:'zh-tw'})" ></a></td>
        </tr>
        <tr>
          <th scope="row"><span class="must">*</span>介接項目：</th>
          <td>
          <div style="width:400px">
           
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
                      <th scope="col" style="width :5%"></th> 
                    <th scope="col">檔案日期</th> 
                    <th scope="col">應收檔案數</th>
                   <th scope="col">實收檔案數</th>
                    <th scope="col">轉檔完成時間</th> 
                    <th scope="col">列表</th>
                  
                    
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' > </td>
                      <td class="aCenter"  ><input type="button" value ="取消" class="UrgeCancle" ng-click="goCancel(record)"   /> </td>
                         <td class="aCenter" ng-bind='record["CD"] | SimpleTaiwanDate' > </td>
                             <td class="aCenter" ng-bind='record["UTN"]'></td>  
                   <td class="aCenter" >
                     <a href="javascript:void(0);" ng-bind='record["UC"] ' ng-click="goDetail(record)" class="ng-binding"></a>
                    </td>
                
                   <td class="aCenter" ng-bind='record["UN"]'></td> 
                   <td class="aCenter" ng-bind='record["USN"]'></td> 
                  
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

    <script src="LogMainList.js" type="text/javascript"></script>
</asp:Content>

