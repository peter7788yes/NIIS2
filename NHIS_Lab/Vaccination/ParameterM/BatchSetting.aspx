<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchSetting.aspx.cs" Inherits="Vaccination_ParameterM_BatchSetting" MasterPageFile="~/MasterPage/MasterPage.master" %>
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

        <div class="formTb">
             <table>
                  <tr>

                    <td colspan="2">
                        <input  id="tbLocation" type="text" class="text03" ng-click="openOrgs()"  />
                        <img  src="/images/location.png" ng-click="openOrgs()" />
                    </td>
                  </tr>
                   <tr>
                    <th scope="row">疫苗名稱：</th>
                    <td>
                       <select id="selectCheck" ng-model="VM.selectCheck" ng-change="changeSelect()">
                            <option ng-repeat="option in VM.VaccineSelect" value="{{option.I}}" ng-bind="option.V"></option>
                        </select>
                    </td>
                  </tr>
             </table>
        </div>

        <div id="dv" style="display:none;">
          <!--表格 start -->
          <div class="title">疫苗批號<span class="wordred">(僅列出尚有庫存的批號)</span></div>
          <div class="listTb">
             <table>
              <tr>
                <th scope="col">設定</th>
                <th scope="col">批號</th>
                <th scope="col">類別</th>
                <th scope="col">包裝樣式</th>
                <th scope="col">劑量</th>
                <th scope="col">庫存量(劑)</th>
                <th scope="col">庫存量(瓶)</th>
                <th scope="col">有效日期</th>
                <th scope="col">預設批號</th>
              </tr>
              <tr  ng-repeat="record in TM.tbData1 track by $index">
                    <td class="aCenter"><button ng-bind="record['DBVID'] > 0 ?'取消':'設定'" ng-click="goSetting(record)" class="btn"></button></td>
                    <td class="aCenter" ng-bind="record['BN']"></td>
                    <td class="aCenter" ng-bind="record['BC']"></td>
                    <td class="aCenter" ng-bind="record['PS']"></td>
                    <td class="aCenter" ng-bind="record['DP']"></td>
                    <td class="aCenter" ng-bind="record['SV']"></td>
                    <td class="aCenter" ng-bind="record['SB']"></td>
                    <td class="aCenter" ng-bind="record['VD'] | SimpleTaiwanDate "></td>
                    <td class="aCenter" ng-bind="record['DBVID'] > 0 ?'Y':'N'"></td>
              </tr>
            </table>
          </div>
          <!--表格 end --> 
           <!--表格 start -->
          <div class="title">預設批號</div>
          <div class="listTb">
            <table>
              <tr>
                <th scope="col">批號順序</th>
                <th scope="col">批號</th>
                <th scope="col">類別</th>
                <th scope="col">包裝樣式</th>
                <th scope="col">劑量</th>
                <th scope="col">庫存量(劑)</th>
                <th scope="col">庫存量(瓶)</th>
                <th scope="col">有效日期</th>
                <th scope="col">移除設定</th> 
              </tr>
              <tr  ng-repeat="record in TM.tbData2 track by $index">
                    <td class="aCenter" ng-bind="$index+1"></td>
                    <td class="aCenter" ng-bind="record['BN']"></td>
                    <td class="aCenter" ng-bind="record['BC']"></td>
                    <td class="aCenter" ng-bind="record['PS']"></td>
                    <td class="aCenter" ng-bind="record['DP']"></td>
                    <td class="aCenter" ng-bind="record['SV']"></td>
                    <td class="aCenter" ng-bind="record['SB']"></td>
                    <td class="aCenter" ng-bind="record['VD'] | SimpleTaiwanDate "></td>
                    <td class="aCenter"><input type="button" value="移除" class="btn" ng-click="goRemove(record)"/></td>
              </tr> 
            </table>
          </div>
          <!--表格 end --> 
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
     <%:Scripts.Render("~/bundles/BatchSetting_JS")%>
</asp:Content>
