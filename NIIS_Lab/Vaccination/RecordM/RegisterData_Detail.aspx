<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterData_Detail.aspx.cs" Inherits="VaccinationM_RegisterData_Detail"  MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/CaseMaintain/ucCaseRemark.ascx" TagPrefix="uc1" TagName="ucCaseRemark" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-clock>
        <div class="path"></div>
          <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off">
            <div class="formBtn formBtnleft">
              <input type="button" id="lastBtn" value="上一頁" class="btn" />
            </div>
    <%--<div id="divFunc" class="function">
          <ul>
              <li><a href="javascript:(void)" ng-click="goPrint()" class="print">列印</a></li>
          </ul>
    </div>--%>
    <div class="formTb formTb3">
      <table>
        <tr>
          <td width="33%"><table>
              <tr>
                <th scope="row">姓名：</th>
                <td><a href="javacript:void(0)" <%--ng-bind="VM.u.CN"--%>><%:VM.ChName %></a></td>
              </tr>
              <tr>
                <th scope="row">身分證號：</th>
                <td <%--ng-bind="VM.u.IN"--%>><%=VM.IdNo %></td>
              </tr>
              <tr>
                <th scope="row">出生日期：</th>
                <td <%--ng-bind="VM.u.BD | SimpleTaiwanDate"--%>><%:VM.BirthDate.ToShortTaiwanDate() %></td>
              </tr>
              <tr>
                <th scope="row">性別</th>
                <td <%-- ng-bind="VM.u.G"--%>><%:VM.GenderString %></td>
              </tr>
            </table></td>
          <td width="33%">
              <table>
                  <tr>
                    <th scope="row">戶號：</th>
                    <td <%--ng-bind="VM.u.HN"--%>><%:VM.HouseNo %></td>
                  </tr>
                  <tr>
                    <th scope="row">所屬轄區：</th>
                    <td><%:VM.RegionName %></td>
                  </tr>
                  <tr>
                    <th scope="row">身分別：</th>
                    <td><%=CapacityString %></td>
                  </tr>
                  <tr>
                    <th scope="row">語言：</th>
                     <td <%--ng-bind="VM.u.L"--%>><%:VM.Language %></td>
                  </tr>
            </table>
          </td>
          <td width="33%"><table>
              <tr>
                <th scope="row">母親姓名：</th>
                <td <%--ng-bind="VM.u.MN"--%>><%:VM.MotherName %></td>
              </tr>
              <tr>
                <th scope="row">母親身分證號：</th>
                <td <%--ng-bind="VM.u.MI"--%>><%:VM.MontherIdNo %></td>
              </tr>
              <tr>
                <th scope="row">母親出生日期：</th>
                <td <%--ng-bind="VM.u.MD | SimpleTaiwanDate"--%>><%:VM.MotherBirthDate.ToShortTaiwanDate() %></td>
              </tr>
            </table></td>
        </tr>
        <tr>
          <td colspan="3"><table>
              <tr>
                <th scope="row">戶籍地址：</th>
                <td <%--ng-bind="VM.u.RA"--%>><%:VM.RAddress %></td>
              </tr>
              <tr>
                <th scope="row">通訊地址：</th>
                <td><%:VM.CAddress %></td>
              </tr>
            </table></td>
        </tr>
        <tr>
          <td colspan="3"><table>
              <tr>
                <th scope="row">大便卡篩檢：</th>
                <td>
                    <span ng-repeat="record in TM.scAry track by $index" ng-cloak>
                        <span class="replaceA" ng-click="openStool(record['I'])">{{record['CD'] | SimpleTaiwanDate}} - {{record['SC']}}</span>
                        <br/>
                    </span>
                   <span class="replaceA" ng-click="openStool(0)">+新增篩選結果</span>
                  <%--  <label for="rb1">
                    <input name="rbGroup"  id="rb1" type="radio" value="1" ng-model="VM.u.SC" ng-change="goStool(1)"/>
                    正常</label>
                  <label for="rb2">
                    <input name="rbGroup" id="rb2" type="radio" value="2" ng-model="VM.u.SC"  ng-change="goStool(2)"/>
                    不正常</label>
                  <label for="rb3">
                    <input name="rbGroup" id="rb3" type="radio" value="3" ng-model="VM.u.SC"  ng-change="goStool(3)"/>
                    不確定或不知道</label></td>--%>
              </tr>
              <tr>
                <th scope="row">流感註記：</th>
                <td>
                    <table>
                        <tr>
                            <td style="width:180px">
                                 <label for="cbEver" >
                                    <input id="cbEver"  type="checkbox" value="1" disabled="disabled" />
                                    曾經接種流感疫苗
                                </label>  
                            </td>
                              <td>
                                    <div ng-repeat="record in TM.fnAry track by $index" ng-cloak>
                                    接種日期:<span ng-bind="record['ID'] | SimpleTaiwanDate"></span>
                                    </div>
                             </td>
                        </tr>
                    </table>
                </td>
              </tr>
              <tr>
                <th scope="row">紙本黃卡更新紀錄：</th>
                <td>
                  <label id="NoYellowCardUpdateRecord" style="display:none;"  ng-show="!VM.u.HY">未更換黃卡</label>
                  <label id="YellowCardUpdateRecord" style="display:none;"  ng-show="VM.u.HY"><span ng-bind="VM.u.AD | SimpleTaiwanDate"></span> (登錄者: <span ng-bind="VM.u.YUN"></span> - <span ng-bind="VM.u.YO"></span>)</label>
                  <input type="button" value="新增申請記錄" class="btn" ng-click="goAddYellowCard()"/>
                  <br/>
                  <span class="wordred">（請確認個案紙本黃卡是否為最新版）</span></td>
              </tr>
              <tr>
                <th scope="row">個案備註：</th>
                <td><label id="CaseUserRemark" ng-bind="VM.u.CR"></label>
                  <%--<input type="button" value="新增個案備註" class="btn"/  ng-click="goAddRemark()"/>--%>
                        <uc1:ucCaseRemark runat="server" id="ucCaseRemark1" />
                </td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div>
  </form>
  <div class="explain  button_floatleft"><span><img style="cursor:pointer" ng-click="goAddVaccine()" src="/images/icon_choose2.png" />說明：</span>
    <ul>
      <li class="yellowbg">逾期施打</li>
      <li class="pinkbg">提早施打</li>
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
                        <img style="cursor:pointer" ng-show='record["RID"]' ng-click="goChooseCate(record)" src="/images/icon_choose.png" />
                        <img style="cursor:pointer" ng-show='!record["RID"]' ng-click="goChooseCate(record)" src="/images/icon_choose2.png" />
                   </td>
                   <td class="aCenter">
                        <img style="cursor:pointer" ng-show='record["RID"]' ng-click="goDetail(record)" src="/images/icon_detailed.png" />
                   </td>
        </tr>
    </table>
  </div>
    </section>
    <script>
       var  ON ="<%:OrgName%>";
       var  CC =<%:CaseUserID%>;
       var  uJson =<%=uJson%>;
       var  tbAry =<%=tbAry%>;
       var  scAry =<%=scAry%>;
       var  fnAry =<%=fnAry%>;
    </script>
</asp:Content>
 