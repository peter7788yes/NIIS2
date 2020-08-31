<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentReRecord.aspx.cs" Inherits="Vaccination_RecordM_StudentReRecord" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/UC_OpenSelectSingleOrg.ascx" TagPrefix="uc1" TagName="UC_OpenSelectSingleOrg" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
  <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
           <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
        <div class="formBtn formBtnleft">
               <% if (AddPower.HasPower) { %>
                    <input type="button" id="addBtn" value="新增" class="btn" />
               <% } %>
               <% if (UploadPower.HasPower) { %>
                    <input type="button" id="uploadBtn" value="上傳匯入" class="btn" />
               <% } %>
        </div>
        <div class="formTb">
             <table>
                  <tr>
                    <th scope="row">所屬單位：</th>
                    <td>
                        <uc1:UC_OpenSelectSingleOrg runat="server" ID="UC_OpenSelectSingleOrg" />
                    </td>
                  </tr>
                   <tr>
                    <th scope="row">學校名稱:</th>
                    <td>
                       <select  ng-model="VM.selectSchool"  >
                                   <option value="">全部</option>
                                   <option ng-repeat="option in VM.sAry" value="{{option.I}}" ng-bind="option.N"></option>
                        </select>
                    </td>
                  </tr>
                   <tr>
                    <th scope="row">入學區間:</th>
                    <td>
                         <input id="tbDateStart" type="text" class="text02"   />
                         <img style="cursor:pointer" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'tbDateEnd\')}',el:'tbDateStart',dateFmt: 'yyy年',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                         <span class="h10">~</span>
                         <input id="tbDateEnd" type="text" class="text02" />
                         <img style="cursor:pointer" onclick="WdatePicker({minDate:'#F{$dp.$D(\'tbDateStart\')}',el:'tbDateEnd',dateFmt: 'yyy年',lang:'zh-tw'})" src="/images/icon_calendar.png"/>

                    </td>
                  </tr>
                  <tr>
                    <th scope="row">學生級別:</th>
                    <td>
                        <label for="L1"><input id="L1" type="checkbox" value="1" />新生</label>
                        <label for="L2"><input id="L2" type="checkbox" value="2" />二年級</label>
                    </td>
                  </tr>
             </table>
        </div>
        <div class="formBtn">
                <% if (SearchPower.HasPower){ %>
                    <input type="button" id="searchBtn" value="查詢" ng-click="changePage(1)" class="btn" />
                <% } %>
                <input type="button" id="clearBtn" value="清空" ng-click="doReset()" class="btn" />
        </div>
        </form>
    <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
    <div id="tmBlock" style="display:none;" class="listTb">
    <table>
      <tr>
        <th scope="col">序號</th>
        <th scope="col">學校</th>
        <th scope="col">入學年度</th>
        <th scope="col">學生級別</th>  
        <th scope="col">登錄人員</th>
        <th scope="col">登錄時間</th>
        <th scope="col">瀏覽</th>
      </tr>

       <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter" ng-bind='record["SN"]'></td>
                      <td class="aCenter" ng-bind='record["AY"]'></td>
                      <td class="aCenter" ng-bind='record["SY"]'></td>
                      <td class="aCenter" ng-bind='record["UN"]'></td>
                      <td class="aCenter" ng-bind='record["SD"] | SimpleTaiwanDate'></td>
                      <td class="aCenter"><img style="cursor:pointer" ng-click="goDetail(record)" src="/images/icon_browse.png" /></td>
        </tr>
    </table>
  </div>
    </section>
    <script>
        var defaultOrgID="<%=DefaultOrgID %>";
        var defaultOrgName="<%=DefaultOrgName %>";
        var sAry =<%=sAry%>;
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="jsCP" runat="Server">
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>
