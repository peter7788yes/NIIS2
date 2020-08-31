<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyDataRecord.aspx.cs" Inherits="Vaccination_CertificateM_ApplyDataRecord" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
         <form  id="MyForm" autocomplete="off">
    <div class="formBtn formBtnleft">
      <input type="button" id="lastBtn" value="上一頁" class="btn" />
    </div>
    <div class="formTb formTb3">
      <table>
        <tr>
          <td width="33%"><table>
              <tr>
                <th scope="row">姓名：</th>
                <td><a href="javacript:void(0)" ng-bind="VM.u.CN"><%:VM.ChName %></a></td>
              </tr>
              <tr>
                <th scope="row">身分證號：</th>
                <td ng-bind="VM.u.IN"><%:VM.IdNo %></td>
              </tr>
              <tr>
                <th scope="row">出生日期：</th>
                <td ng-bind="VM.u.BD | SimpleTaiwanDate"><%:VM.BirthDate.ToShortTaiwanDate() %></td>
              </tr>
              <tr>
                <th scope="row">性別</th>
                <td ng-bind="VM.u.G"><%:VM.GenderString %></td>
              </tr>
            </table></td>
          <td width="33%">
              <table>
                 <tr>
                    <th scope="row">戶號：</th>
                    <td ng-bind="VM.u.HN"><%:VM.HouseNo %></td>
                  </tr>
                  <tr>
                    <th scope="row">所屬轄區：</th>
                    <td></td>
                  </tr>
                  <tr>
                    <th scope="row">身分別：</th>
                    <td></td>
                  </tr>
                  <tr>
                    <th scope="row">語言：</th>
                     <td ng-bind="VM.u.L"><%:VM.Language %></td>
                  </tr>
            </table>
          </td>
          <td width="33%"><table>
              <tr>
                <th scope="row">母親姓名：</th>
                <td ng-bind="VM.u.MN"><%:VM.MotherName %></td>
              </tr>
              <tr>
                <th scope="row">母親身分證號：</th>
                <td ng-bind="VM.u.MI"><%:VM.MontherIdNo %></td>
              </tr>
              <tr>
                <th scope="row">母親出生日期：</th>
                <td ng-bind="VM.u.MD | SimpleTaiwanDate"><%:VM.MotherBirthDate.ToShortTaiwanDate()  %></td>
              </tr>
            </table></td>
        </tr>
        <tr>
          <td colspan="3"><table>
              <tr>
                <th scope="row">戶籍地址：</th>
                <td ng-bind="VM.u.RA"><%:VM.ResAddr %></td>
              </tr>
              <tr>
                <th scope="row">通訊地址：</th>
                <td ng-bind="VM.u.CA"><%:VM.CAddress %></td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div>
  </form>
  <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
  <div id="tmBlock" style="display:none;" class="listTb">
    <table>
      <tr>
        <th scope="col">序號</th>
        <th scope="col">申請時間</th>
        <th scope="col">申請人姓名</th>
        <th scope="col">關係</th>
        <th scope="col">承辦單位</th>
        <th scope="col">承辦人員</th>
        <th scope="col">檢附文件</th>
      </tr>
      <tr ng-class="getClass(record)" ng-repeat="record in TM.data track by $index">
                   <td class="aCenter" ng-bind='$index+1'></td>
                   <td class="aCenter" ng-bind='record["AD"] | SimpleTaiwanDate' ></td>
                   <td class="aCenter" ng-bind='record["AN"]'></td>
                   <td class="aCenter" ng-bind='record["UR"]'></td>
                   <td class="aCenter" ng-bind='record["O"]'></td>
                   <td class="aCenter" ng-bind='record["UN"]'></td>
                   <td class="aCenter">
                       <span ng-repeat ="item in record['ary'] track by $index">
                           <span class="replaceA" ng-bind='item["DF"]' ng-click="goDownload(item)"></span>
                       </span>
                   </td>
        </tr>
    </table>
  </div>
    </section>
    <script>
          var uJson =<%=uJson%>;
          var i=<%:CaseUserID%>;
    </script>
</asp:Content>