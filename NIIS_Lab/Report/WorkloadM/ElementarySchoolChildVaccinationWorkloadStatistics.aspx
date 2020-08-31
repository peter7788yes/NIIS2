<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ElementarySchoolChildVaccinationWorkloadStatistics.aspx.cs" Inherits="Report_WorkloadM_ElementarySchoolChildVaccinationWorkloadStatistics"  MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/UC_OpenSelectSingleOrg.ascx" TagPrefix="uc1" TagName="UC_OpenSelectSingleOrg" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <link href="/css/design.min.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
   <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
           <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
        <div class="formTb">
             <table>
                 <tr>
                     <th scope="row"><span class="must">*</span>所屬單位:</th>
                     <td colspan="2">
                         <uc1:UC_OpenSelectSingleOrg runat="server" ID="UC_OpenSelectSingleOrg" />
                     </td>
                 </tr>
                   <tr>
                    <th scope="row"><span class="must">*</span>入學年度:</th>
                    <td>
                         <input id="tb" required="required" type="text" class="text02"  />
                    </td>
                  </tr>
                 <tr>
                    <th scope="row"><span class="must">*</span>學校名稱:</th>
                    <td>
                        <select required="required">
                              <option value="竹北國小">竹北國小</option>
                        </select>
                    </td>
                  </tr>
                 <tr>
                    <th scope="row"><span class="must">*</span>出生區間:</th>
                    <td colspan="5"> 
                         <input required="required" id="tbDateStartA" type="text" class="text02" />
                         <img style='cursor:pointer' onclick="WdatePicker({maxDate:'#F{$dp.$D(\'tbDateEndA\')}',el:'tbDateStartA',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                         <span class='h10'>~</span>
                         <input required="required" id="tbDateEndA"  type="text" class="text02" />
                         <img style='cursor:pointer' onclick="WdatePicker({minDate:'#F{$dp.$D(\'tbDateStartA\')}',el:'tbDateEndA',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>接種區間:</th>
                    <td colspan="5"> 
                         <input required="required" id="tbDateStartB"  type="text" class="text02" />
                         <img style='cursor:pointer' onclick="WdatePicker({maxDate:'#F{$dp.$D(\'tbDateEndB\')}',el:'tbDateStartB',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                         <span class='h10'>~</span>
                         <input required="required" id="tbDateEndB"  type="text" class="text02" />
                         <img style='cursor:pointer' onclick="WdatePicker({minDate:'#F{$dp.$D(\'tbDateStartB\')}',el:'tbDateEndB',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                    </td>
                  </tr>
             </table>
        </div>
        <div class="formBtn">
             <input type="button" class="btn" value="預覽列印"  ng-click="goPrint()" />
             <asp:Button Text="匯出xls" ID="btnDownload" CssClass="btn" ClientIDMode="Static" runat="server" OnClick="btnDownload_Click" />
        </div>
      </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/ElementarySchoolChildVaccinationWorkloadStatistics_JS")%>
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>