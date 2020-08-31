<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterData_Detail.aspx.cs" Inherits="VaccinationM_RegisterData_Detail"  MasterPageFile="~/MasterPage/MasterPage.master" %>
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
        <tr>
          <td colspan="3"><table>
              <tr>
                <th scope="row">大便卡篩檢：</th>
                <td><label for="rb1">
                    <input name="rbGroup"  id="rb1" type="radio" value="1"/>
                    正常</label>
                  <label for="rb2">
                    <input name="rbGroup" id="rb2" type="radio" value="2"/>
                    不正常</label>
                  <label for="rb3">
                    <input name="rbGroup" id="rb3" type="radio" value="3"/>
                    不確定或不知道</label></td>
              </tr>
              <tr>
                <th scope="row">流感註記：</th>
                <td><label for="cbEver">
                    <input id="cbEver" type="checkbox" value="1"/>
                    曾經接種流感疫苗</label></td>
              </tr>
              <tr>
                <th scope="row">紙本黃卡更新紀錄：</th>
                <td><label id="YellowCardUpdateRecord"></label>
                  <input type="button" value="新增申請記錄" class="btn" ng-click="goAddYellowCard()"/>
                  <br/>
                  <span class="wordred">（請確認個案紙本黃卡是否為最新版）</span></td>
              </tr>
              <tr>
                <th scope="row">個案備註：</th>
                <td><label id="CaseUserRemark">其他:103/2/25 出國6個月 (登錄者: 李小明 - 斗六市衛生所)</label>
                  <input type="button" value="新增個案備註" class="btn"/  ng-click="goAddRemark()"/></td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div>
  </form>

  <div class="explain  button_floatleft"><span><a href="javascript:void(0);" ng-click="goAddVaccine()"><img src="/images/icon_choose2.png" /></a>說明：</span>
    <ul>
      <li class="yellowbg">逾期施打</li>
      <li class="pinkbg">提早施打</li>
     <%-- <li class="redbg">已屆效</li>--%>
      <li>非常規疫苗</li>
    </ul>
      <input type="button" id="Submit1" value="重新計算預定日期" class="btn" />
    <div class=" btnblock2 button_floatright">個案歲數：<%=AgeString %></div>
  </div>
  
  <div class="listTb">
    <table>
      <tr>
        <th scope="col">序號</th>
        <th scope="col">適齡</th>
        <th scope="col">劑別代號</th>
        <th scope="col">預定日期</th>
        <th scope="col">接種日</th>
        <th scope="col">接種單位</th>
        <th scope="col">批號</th>
        <th scope="col">建檔方式</th>
        <th scope="col">建檔日期</th>
        <th scope="col">接種作業</th>
        <th scope="col">詳細資料</th>
      </tr>

         <tr ng-class="getClass(record)" ng-repeat="record in TM.data track by $index">
                   <td class="aCenter" ng-bind='$index+1'></td>
                   <td class="aCenter" ng-bind='record["AE"]'></td>
                   <td class="aCenter" ng-bind='record["SRVC"]'></td>
                   <td class="aCenter" ng-bind='record["AD"] | SimpleTaiwanDate' ></td>
                   <td class="aCenter" ng-bind='record["ID"] | SimpleTaiwanDate'></td>
                   <td class="aCenter" ng-bind='record["ON"]'></td>
                   <td class="aCenter" ng-bind='record["VB"]'></td>
                   <td class="aCenter" ng-bind='record["CT"]'></td>
                   <td class="aCenter" ng-bind='record["CD"] | SimpleTaiwanDate' ></td>
                   <td class="aCenter">
                       <a href="javascript:void(0);" ng-click="goChooseCate(record)">
                           <img ng-show='record["ID"]'  src="/images/icon_choose.png" />
                           <img ng-show='!record["ID"]' src="/images/icon_choose2.png" />
                       </a>
                   </td>
                    <td class="aCenter">
                       <a href="javascript:void(0);" ng-click="goDetail(record)">
                           <img ng-show='record["ID"]'  src="/images/icon_detailed.png" />
                       </a>
                   </td>
        </tr>
    </table>
  </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
   
    <script>
       var  ON ="<%=OrgName%>";
       var  CC =<%=CaseUserID%>;
       var  uJson =<%=uJson%>;
       var  tbAry =<%=tbAry%>;
    </script>
    <%:Scripts.Render("~/bundles/RegisterData_Detail_JS")%>
</asp:Content>


 