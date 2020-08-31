<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BCGRecord.aspx.cs" Inherits="Vaccination_RecordM_BCGRecord" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
  <section class="Content"  ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>

        <div class="formBtn formBtnleft">
                <% if (SearchPower.HasPower) { %>
                      <input type="button" id="searchBtn" value="查詢" ng-click="changePage(1)" class="btn" />
                <% } %>
                <% if (AddPower.HasPower) { %>
                     <input type="button" id="addBtn" value="新增" class="btn" />
                <% } %>
        </div>

       <% if (SearchPower.HasPower) { %>
                     
              
        <div class="formTb">
             <table>
                  <tr>
                    <td colspan="2">
                        <input  id="tbLocation" type="text" class="text03" ng-click="openOrgs()" />
                        <a href="javascript:void(0);"><img  src="/images/location.png" ng-click="openOrgs()" /></a>
                    </td>
                  </tr>
                   <tr>
                    <th scope="row">查詢區間:</th>
                    <td>
                          <input id="tbDateStart" class="text02"  onclick="WdatePicker({ dateFmt: 'yyy年', lang: 'zh-tw' })" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateStart',dateFmt: 'yyy年',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                            ~

                          <input id="tbDateEnd" class="text02" onclick="WdatePicker({ dateFmt: 'yyy年', lang: 'zh-tw' })" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateEnd',dateFmt: 'yyy年',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>

                    </td>
                  </tr>
             </table>
        </div>
        <% } %>

  <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
  <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
    <table>
      <tr>
        <th scope="col">序號</th>
        <th scope="col">單位</th>
        <th scope="col">統計年份</th>
        <th scope="col">季別</th>
        <th scope="col">登錄人員</th>
        <th scope="col">登錄時間</th>
        <th scope="col">瀏覽</th>
      </tr>

      <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter" ng-bind='record["ON"]'></td>
                      <td class="aCenter" ng-bind='record["SY"]'></td>
                      <td class="aCenter" ng-bind='record["YS"]'></td>
                      <td class="aCenter" ng-bind='record["UN"]'></td>
                      <td class="aCenter" ng-bind='record["SD"] | LongTaiwanTime'></td>
                      <td class="aCenter"><a href="javascript:void(0);" ng-click="goDetail(record)"><img src="../../images/icon_browse.png" /></a></td>
      </tr>

    </table>
  </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
     <%:Scripts.Render("~/bundles/BCGRecord_JS")%>
     <script src="../../js/date/WdatePicker.js"></script>
</asp:Content>
