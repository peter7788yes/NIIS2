<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New_VaccineUseData.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineUse_New_VaccineUseData" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="VaccineUseApp" ng-controller="NewVaccineUseDataController" ng-cloak>
        <form name="form1" id="form1" >
            <div class="formBtn formBtnleft">
                <%if(NewPower.HasPower){ %>
                <input class="btn" type="button" name="send" ng-click="TransferNewDataList()" value="儲存"/>
                <%} %>
                <input class="btn" type="button" name="send" ng-click="TransferVaccineUse()" value="回上一頁"/>
            </div>
            <div class="formTb">
              <table>
                <tr>
                  <th scope="row"><span class="must">*</span>領用日期：</th>
                  <td colspan="3">
                      <input ng-init="VM.DealDate=GetDate()" id="DealDate" ng-model="VM.DealDate" type="text" name="DealDate" onclick="WdatePicker({ dateFmt: 'yyyMMdd', maxDate: Date(), lang: 'zh-tw' })" onchange="SetDate()" class="text02" required/>
                      <a href="#"><img onclick="WdatePicker({el:'DealDate',dateFmt: 'yyyMMdd',maxDate: Date(),lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>領用單位：</th>
                  <td colspan="3">
                      <input ng-init="VM.UseOrgName='<%=OrgName%>'" ng-model="VM.UseOrgName" name type="text" class="text03" value="" disabled="disabled"/>
                  </td>
                </tr>
                <tr>
                  <th scope="row">備註：</th>
                  <td colspan="3">
                    <input ng-model="VM.Remark" name="" type="text" class="text03">
                  </td>
                </tr>
              </table>
            </div>
         </form>
    </section>   
</asp:Content>
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script src="/js/date/WdatePicker.js"></script>
     <%:Scripts.Render("~/bundles/VaccineUse_JS")%>
</asp:Content>
