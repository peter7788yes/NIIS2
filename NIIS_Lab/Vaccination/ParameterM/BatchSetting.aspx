<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchSetting.aspx.cs" Inherits="Vaccination_ParameterM_BatchSetting" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/UC_OpenSelectSingleOrg.ascx" TagPrefix="uc1" TagName="UC_OpenSelectSingleOrg" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
  <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
         <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
        <div class="formTb">
             <table>
                  <tr>
                    <th scope="row"><span class="must">*</span>所屬單位：</th>
                    <td colspan="2">
                        <uc1:UC_OpenSelectSingleOrg runat="server" ID="UC_OpenSelectSingleOrg" />
                        <%--<input  id="tbLocation" type="text" class="text03" ng-click="openOrgs()"  />
                        <img style="cursor:pointer"  ng-click="openOrgs()" src="/images/location.png"  />--%>
                    </td>
                  </tr>
                   <tr>
                    <th scope="row">疫苗名稱：</th>
                    <td>
                       <select id="selectCheck" ng-model="VM.selectCheck" ng-change="changeSelect()">
                            <option value="0">請選擇</option>
                            <option ng-repeat="option in VM.VaccineSelect" value="{{option.I}}" ng-bind="option.EV"></option>
                        </select>
                    </td>
                  </tr>
             </table>
        </div>
      </form>
        <div id="dv" style="display:none;">
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
              <tr  ng-repeat="record in TM.tbData1 track by $index"  ng-class="record['DBVID'] > 0 ?'bluecolor':''">
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
        </div>
    </section>
</asp:Content>
