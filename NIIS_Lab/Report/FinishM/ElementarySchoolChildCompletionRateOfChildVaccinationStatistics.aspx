<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ElementarySchoolChildCompletionRateOfChildVaccinationStatistics.aspx.cs" Inherits="Report_FinishM_ElementarySchoolChildCompletionRateOfChildVaccinationStatistics" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

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
        <div class="formBtn formBtnleft">
              <input type="button" class="btn" value="預覽列印"  ng-click="goPrint()" />
             <asp:Button Text="匯出xls" ID="btnDownload" CssClass="btn" ClientIDMode="Static" runat="server" OnClick="btnDownload_Click" />
        </div>
       <div class="formTb">
             <table>
                 <tr>
                     <td colspan="2">
                            <input id="tbLocation" type="text" class="text03" ng-click="openOrgs()" />
                            <a href="javascript:void(0);"><img  src="/images/location.png" ng-click="openOrgs()" /></a>
                     </td>
                 </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>入學年度:</th>
                    <td>
                         <input  type="text" class="text03"  />
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>學校名稱:</th>
                    <td>
                        <select>
                              <option value="竹北國小">竹北國小</option>
                        </select>
                    </td>
                  </tr>
                   <tr>
                    <th scope="row"><span class="must">*</span>出生區間:</th>
                    <td>
                         <input id="tbDateStart" class="text02"  onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateStart',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                            ~ 
                         <input id="tbDateEnd" class="text02" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateEnd',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>接種區間:</th>
                    <td>
                         <input id="tbDateStart2" class="text02"  onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateStart2',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                            ~ 
                         <input id="tbDateEnd2" class="text02" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateEnd2',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>

                    </td>
                  </tr>
             </table>
        </div>
      </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/AbnormalVaccinationDetail_JS")%>
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>