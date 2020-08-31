<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Return_VaccineOut.aspx.cs" Inherits="Vaccine_StockManagementM_VaccineOut_Return_VaccineOut" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
       <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<section class="Content2">
  <h2>退回原因</h2>
    <section class="Content" ng-app="VaccineOutApp" ng-controller="ReturnVaccineOutDataController" ng-cloak>
        <form id="form1">
            <div class="formTb formTb2">
                <table>
                  <tr>
                  <th scope="row"><span class="must">*</span>退回原因：</th>
                    <td >
                        <select ng-model="VM.VaccReturnSelect"  ng-disabled="true">
                        <option ng-selected="option.EV==VM.VaccReturnSelect" ng-repeat="option in VM.VaccReturn" value="{{option.EV}}" ng-bind="option.EN"></option>
                        </select>
                    </td>
                    <td>
                        <label ng-show="VM.VaccReturnSelect==4?true:false" ng-bind="ReturnOther" ></label>
                    </td>
                  </tr>
                </table>
            </div>
            <div class="formBtn">
               <input type="submit" class="btn" value="關閉" ng-click="CloseWin()">
           </div>
         </form>
    </section>
</section>
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
         var VaccReturnData = <%=VaccReturn %>;
     </script>
     <%:Scripts.Render("~/bundles/VaccineOut_JS")%>
</asp:Content> 

