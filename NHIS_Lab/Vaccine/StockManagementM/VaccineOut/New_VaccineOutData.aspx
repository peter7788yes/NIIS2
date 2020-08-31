<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New_VaccineOutData.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineOut_New_VaccineOutData" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="VaccineOutApp" ng-controller="NewVaccineOutDataController" ng-cloak>
        <form name="form1" id="form1" >
            <div class="formBtn formBtnleft">
                <%if(NewPower.HasPower){ %>
                <input class="btn" type="button" name="send" ng-click="TransferNewDataList()" value="儲存"/>
                <%} %>
                <input class="btn" type="button" name="send" ng-click="TransferVaccineOut()" value="回上一頁"/>
            </div>
            <div class="formTb">
              <table>
                <tr>
                  <th scope="row"><span class="must">*</span>撥出日期：</th>
                  <td colspan="3">
                      <input ng-init="VM.DealDate=GetDate()" id="DealDate" ng-model="VM.DealDate" type="text" name="DealDate" onclick="WdatePicker({ dateFmt: 'yyyMMdd',maxDate: Date() ,lang: 'zh-tw' })" onchange="SetDate()" class="text02" required/>
                      <a href="#"><img onclick="WdatePicker({el:'DealDate',dateFmt: 'yyyMMdd',maxDate: Date(),lang:'zh-tw'})" src="/images/icon_calendar.png"></a>
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>撥出單位：</th>
                  <td colspan="3">
                      <input ng-model="VM.OutOrgName" name type="text" class="text03" value="" disabled="disabled" />
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>撥入單位：</th>
                  <td colspan="3">
                      <input id="InOrgName" ng-model="VM.InOrgName" name="InOrgName" type="text" value="" class="text03" ng-click="openOrgs()" readonly="true" required="required"/>
                      <input id="InOrgID" ng-model="VM.InOrgID" name="InOrgID" type="hidden" required/>
                      <img  src="/images/location.png" ng-click="openOrgs()" />
                      <a href="javascript:void(0);" id="refreshBtn" ng-click="refresh()"></a
                  </td>
                </tr>
                <tr>
                  <th scope="row">備註：</th>
                  <td colspan="3">
                    <input ng-model="VM.Remark" name="" type="text" class="text03">
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>撥出類別：</th>
                  <td>
                      <select ng-model="VM.DealTypeSelect" name="DealTypeSelect" required="required">
                      <option ng-repeat="option in VM.DealType" value="{{option.EV}}" ng-bind="option.EN"></option>
                      </select>
                      <label ng-show="(VM.DealTypeSelect==4)?true:false">
                        <input id="DealHospitalName" ng-model="VM.DealHospitalName" name="DealHospitalName" class="text02" type="text" ng-click="openSelectAgency()" readonly="true" required="required"/>
                        <input id="DealHospitalID" ng-model="VM.DealHospitalID" name="DealHospitalID" type="hidden" required/>
                        <a href="javascript:void(0);" ng-click="openSelectAgency()">
                            <img src="/images/icon_agency.png" />
                        </a>
                        <a href="javascript:void(0);" id="refreshBtn1" ng-click="refresh1()"></a>
                      </label>
                  </td>
                </tr>
              </table>
            </div>
         </form>
    </section>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
         var DealTypeData = <%=DealType %>;
         var OrgData = '<%=OrgName %>';
     </script>
     <script src="/js/date/WdatePicker.js"></script>
     <%:Scripts.Render("~/bundles/VaccineOut_JS")%>
</asp:Content>