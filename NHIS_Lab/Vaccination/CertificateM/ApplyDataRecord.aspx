<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyDataRecord.aspx.cs" Inherits="Vaccination_CertificateM_ApplyDataRecord"  MasterPageFile="~/MasterPage/MasterPage.master" %>
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

    <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
         <form>
    <div class="formBtn formBtnleft">
      <input type="button" id="lastBtn" value="上一頁" class="btn" />
    </div>
    <div class="formTb formTb3">
      <table>
        <tr>
          <td width="33%"><table>
              <tr>
                <th scope="row">姓名：</th>
                <td><a href="javacript:void(0)" ng-bind="VM.u.CN"></a></td>
              </tr>
              <tr>
                <th scope="row">身分證號：</th>
                <td ng-bind="VM.u.IN"></td>
              </tr>
              <tr>
                <th scope="row">出生日期：</th>
                <td ng-bind="VM.u.BD | SimpleTaiwanDate"></td>
              </tr>
              <tr>
                <th scope="row">性別</th>
                <td ng-bind="VM.u.G"></td>
              </tr>
            </table></td>
          <td width="33%"><table>
              <tr>
                <th scope="row">戶號：</th>
                <td></td>
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
                <td></td>
              </tr>
            </table></td>
          <td width="33%"><table>
              <tr>
                <th scope="row">母親姓名：</th>
                <td ng-bind="VM.u.MN"></td>
              </tr>
              <tr>
                <th scope="row">母親身分證號：</th>
                <td ng-bind="VM.u.MI"></td>
              </tr>
              <tr>
                <th scope="row">母親出生日期：</th>
                <td ng-bind="VM.u.MD | SimpleTaiwanDate"></td>
              </tr>
            </table></td>
        </tr>
        <tr>
          <td colspan="3"><table>
              <tr>
                <th scope="row">戶籍地址：</th>
                <td ng-bind="VM.u.RA"></td>
              </tr>
              <tr>
                <th scope="row">通訊地址：</th>
                <td ng-bind="VM.u.CA"></td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div>
  </form>

  
   <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
  
  <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
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
                           <a href="javascript:void(0);" ng-bind='item["DF"]' ng-click="goDownload(item)"></a>
                       </span>
                   </td>
        </tr>
    </table>
  </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
      <script>
          var uJson =<%=uJson%>;
          var i=<%=CaseUserID%>;
    </script>
    <%:Scripts.Render("~/bundles/ApplyDataRecord_JS")%>
</asp:Content>