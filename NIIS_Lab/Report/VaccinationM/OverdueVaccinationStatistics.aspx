<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OverdueVaccinationStatistics.aspx.cs" Inherits="Report_VaccinationM_OverdueVaccinationStatistics" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
       <form id="form1" runat="server"  autocomplete="off">
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
                    <th scope="row"><span class="must">*</span>出生日期:</th>
                    <td>
                         <input id="tbDateStart" class="text02"  onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateStart',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                            ~ 
                         <input id="tbDateEnd" class="text02" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateEnd',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>劑次:</th>
                    <td>
                        <textarea cols="70" rows="5" ></textarea>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>選項:</th>
                    <td>
                        <table>
                            <tr><th>最後一劑到現在時間</th><td><input type="text"/></td></tr>
                            <tr><th>是否有逾期註記或訪視紀錄</th><td><label><input type="radio" name="rb"/>是</label><label><input type="radio" name="rb"/>否</label></td></tr>
                            <tr><th>已逾期多久無訪視紀錄</th><td><input type="text"/></td></tr>
                       </table>
                    </td>
                  </tr>
                 <tr>
                    <th scope="row"><span class="must">*</span>報表格式:</th>
                    <td colspan="5"> 
                        <label><input  type="radio"  name="rd" id="rd1" value="1"/>統計表</label>
                        <label><input  type="radio"  name="rd" id="rd2" value="2"/>明細表</label>
                    </td>
                  </tr>
             </table>
        </div>
        </form>
      <div class="list01" style="color:blue">
        <ul>
          <li style="color:black">P.S: 若選擇衛生所, 則報表格式提供統計表及明細表. 若選擇衛生局或CDC, 則報表格式僅提供統計表</li>
          <li><span>報表格式：</span>個案最後一次施打紀錄到今天應施打而未施打的疫苗次數明細統計表</li>
        </ul>
      </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/OverdueVaccinationStatistics_JS")%>
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>