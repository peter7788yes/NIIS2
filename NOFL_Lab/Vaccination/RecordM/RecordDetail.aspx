<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecordDetail.aspx.cs" Inherits="Vaccination_RecordM_RecordDetail" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">

   <section class="Content3"  ng-app="MyApp" ng-controller="MyController" ng-cloak>

    <h2>接種詳細資料</h2>
    <div class="close">
        <input type="button" id="closeBtn" value="關閉" class="btn" />
    </div>
  
    <div class="formTb formTb2 formTb3">
      <table style="width:auto">
        <tr>
          <th scope="row" class="aRight" >身分證號：</th>
          <td><%=VM.IdNo %></td>
          <th scope="row" class="aRight" >姓名：</th>
          <td><%=VM.ChName %></td>
          <th scope="row" class="aRight">性別：</th>
          <td><%=VM.GenderString %></td>
          <th scope="row" class="aRight" >出生日期：</th>
          <td><%=VM.BirthDate.ToShortTaiwanDate() %></td>
        </tr>
        <tr>
        <th scope="row" class="aRight" >疫苗資料：</th>
          <td colspan="7"><%=AgeEnglish %> - <%=VaccineCode %> - <%=AppointmentDate %></td>
        </tr>
      </table>
    </div>
    <div class="title">接種紀錄</div>
    <div class="listTb">
    <table>
  <tr>
    <th scope="col">接種日</th>
    <th scope="col">接種單位</th>
    <th scope="col">批號</th>
    <th scope="col">建檔方式</th>
    <th scope="col">建檔日期</th>
    <th scope="col">建檔單位(人員)</th>
    <th scope="col">異動日期</th>
    <th scope="col">補種</th>
    <th scope="col">補登/補種原因</th>
    <th scope="col">刪除</th>
  </tr>
  <tr>
    <td class="aCenter"><a href="#">0990122</a></td>
    <td>吳婦產科診所</td>
    <td class="aCenter">BG039704</td>
    <td>健保上傳</td>
    <td class="aCenter">0990202<br>
09:15:22</td>
    <td>吳婦產科診所</td>
    <td class="aCenter">0990204<br>
11:07:58</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td class="aCenter"><a href="#"><img src="/images/icon_del01.gif" /></a></td>
  </tr>
  <tr>
    <td class="aCenter"><a href="#">0990122</a></td>
    <td>吳婦產科診所</td>
    <td class="aCenter">BG039704</td>
    <td>健保上傳</td>
    <td class="aCenter">0990202<br/>
09:15:22</td>
    <td>吳婦產科診所</td>
    <td class="aCenter">0990204<br/>
11:07:58</td>
    <td class="aCenter"><img src="/images/icon_tick.png" width="21" height="14"/></td>
    <td>&nbsp;</td>
    <td class="aCenter"><a href="#"><img src="/images/icon_del01.gif" /></a></td>
  </tr>
</table>
</div>
 <div class="title">訪查紀錄</div>
 <div class="listTb">
<table>
  <tr>
    <th scope="col">疫苗別</th>
    <th scope="col">訪查日期</th>
    <th scope="col">訪查單位（人員）</th>
    <th scope="col">訪查方式</th>
    <th scope="col">訪查原因</th>
    <th scope="col">訪查紀錄</th>
    <th scope="col">附件</th>
    <th scope="col">刪除</th>
    </tr>
  <tr>
    <td class="aCenter">HBIG, HepB</td>
    <td class="aCenter"><a href="#">1040104</a></td>
    <td >斗六市衛生所(張無灰)</td>
    <td>電訪</td>
    <td>逾期：原因</td>
    <td>追蹤結果內容</td>
    <td class="aCenter">下載</td>
    <td class="aCenter"><a href="#"><img src="/images/icon_del01.gif" /></a></td>
    </tr>
 <tr>
    <td class="aCenter">HBIG, HepB</td>
    <td class="aCenter"><a href="#">1040104</a></td>
    <td >斗六市衛生所(張無灰)</td>
    <td>電訪</td>
    <td>逾期：原因</td>
    <td>追蹤結果內容</td>
    <td class="aCenter">下載</td>
    <td class="aCenter"><a href="#"><img src="/images/icon_del01.gif" /></a></td>
    </tr>
</table>
    </div>


    <div id="dvHealth" style="display:none;">

            <div class="title" >評估紀錄</div>
                         <div class="listTb">
                                     <table>
                                          <tr>
                                            <th scope="col">評估日期</th>
                                            <th scope="col">評估者</th>
                                            <th scope="col">評估單位</th>
                                            <th scope="col">一</th>
                                            <th scope="col">二</th>
                                            <th scope="col">三</th>
                                            <th scope="col">四</th>
                                            <th scope="col">五</th>
                                            <th scope="col">六</th>
                                            <th scope="col">七</th>
                                            <th scope="col">八</th>
                                            <th scope="col">其他</th>
                                            <th scope="col">可接種</th>
                                            <th scope="col">刪除</th>
                                            </tr>

                                         <tr ng-show="TM.ApplyHealth.length>0"  ng-repeat="record in TM.ApplyHealth track by $index">
                                                <td class="aCenter"><a href="javascript:void(0);" ng-bind="record['AD'] | SimpleTaiwanDate"></a></td>   
                                                <td class="aCenter" ng-bind="record['N']"></td>
                                              <td class="aCenter" ng-bind="record['ON']"></td>
                                              <td class="aCenter"><img ng-show="record['AS'].indexOf(',1,') >= 0" src="/images/icon_tick.png" width="21" height="14" /></td>
                                              <td class="aCenter"><img ng-show="record['AS'].indexOf(',2,') >= 0" src="/images/icon_tick.png" width="21" height="14" /></td>
                                              <td class="aCenter"><img ng-show="record['AS'].indexOf(',3,') >= 0" src="/images/icon_tick.png" width="21" height="14" /></td>
                                              <td class="aCenter"><img ng-show="record['AS'].indexOf(',4,') >= 0" src="/images/icon_tick.png" width="21" height="14" /></td>
                                              <td class="aCenter"><img ng-show="record['AS'].indexOf(',5,') >= 0" src="/images/icon_tick.png" width="21" height="14" /></td>
                                              <td class="aCenter"><img ng-show="record['AS'].indexOf(',6,') >= 0" src="/images/icon_tick.png" width="21" height="14" /></td>
                                              <td class="aCenter"><img ng-show="record['AS'].indexOf(',7,') >= 0" src="/images/icon_tick.png" width="21" height="14" /></td>
                                              <td class="aCenter"><img ng-show="record['AS'].indexOf(',8,') >= 0" src="/images/icon_tick.png" width="21" height="14" /></td>

                                                <td class="aCenter" ng-bind="record['OS']"></td>
                                                <td class="aCenter"><img ng-show="record['AQ']==true" src="/images/icon_no.png" width="21" height="14" /></td>
                                                <td class="aCenter"><a href="javascript:void(0);"  ng-click="RemoveApplyHealth(record)"><img src="/images/icon_del01.gif" /></a></td>
                                          </tr> 

                                    </table>
            </div>

    </div>

    <div id="dvEffect" style="display:none;">
             <div class="title" >副作用紀錄</div>
             <div class="listTb">
<table>
  <tr>
    <th scope="col">登錄日期</th>
    <th scope="col">登錄者</th>
    <th scope="col">登錄單位</th>
    <th scope="col">症狀最早出現日期</th>
    <th scope="col">接種前3日內，曾就醫吃藥</th>
    <th scope="col">疫苗接種後7日內，發生感染性疾病</th>
    <th scope="col">刪除</th>
    </tr>



    <tr ng-show="TM.ApplyEffect.length>0"  ng-repeat="record in TM.ApplyEffect track by $index">
                                      <td class="aCenter"><a href="javascript:void(0);" ng-bind="record['SD'] | SimpleTaiwanDate"></a></td>   
                                      <td class="aCenter" ng-bind="record['UN']"></td>
                                      <td class="aCenter" ng-bind="record['ON']"></td>
                                      <td class="aCenter" ng-bind="record['ED'] | SimpleTaiwanDate"></td>
                                      <td class="aCenter"><img ng-show="record['AS'].indexOf(',1,') >= 0" src="/images/icon_tick.png" width="21" height="14" /></td>
                                      <td class="aCenter"><img ng-show="record['AS'].indexOf(',2,') >= 0" src="/images/icon_tick.png" width="21" height="14" /></td>
                                      <td class="aCenter"><a href="javascript:void(0);" ng-click="RemoveApplyEffect(record)"><img src="/images/icon_del01.gif" /></a></td>
    </tr> 

 </table>
     </div>
    </div>
  
</section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
     <script>
         var ApplyHealth=<%=ApplyHealthAry%>;
         var ApplyEffect=<%=ApplyEffectAry%>;
     </script>
     <%:Scripts.Render("~/bundles/RecordDetail_JS")%>
</asp:Content>
