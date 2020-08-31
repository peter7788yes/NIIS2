<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AbnormalVaccinationDetail.aspx.cs" Inherits="Report_VaccinationM_AbnormalVaccinationDetail" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
                    <th scope="row"><span class="must">*</span>異常類型:</th>
                    <td>
                        <select>
                              <option value="同一劑別之異常紀錄">同一劑別之異常紀錄</option>
                              <option value="活性疫苗接種日期間隔小於28天">活性疫苗接種日期間隔小於28天</option>
                              <option value="疫苗規範間隔不足者">疫苗規範間隔不足者</option>
                        </select>
                    </td>
                  </tr>
                 <tr>
                    <th scope="row"><span class="must">*</span>報表格式:</th>
                    <td colspan="5"> 
                        <label><input  type="radio"  name="rd" />統計表</label>
                        <label><input  type="radio"  name="rd" />明細表</label>
                    </td>
                  </tr>
             </table>
        </div>
      </form>
      <div class="list01" style="color:blue">
        <ul>
          <li><span>報表格式：</span>同一劑別的異常紀錄：相同疫苗劑別下, 接種日期 / 疫苗批號 / 接種地點任一不相同者；以及接種日早於出生日
                                    活性疫苗接種日期間隔小於28天：個案二劑活性疫苗接種日期間隔不足28天
                                    疫苗規範間隔不足者：同一疫苗的各劑間的接種日期間隔天數小於接種規範者</li>
        </ul>
      </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/AbnormalVaccinationDetail_JS")%>
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>