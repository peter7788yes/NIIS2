<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VaccineOverdueEarlyVaccinationStatistics.aspx.cs" Inherits="Report_VaccinationM_VaccineOverdueEarlyVaccinationStatistics" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
                            <a href="javascript:void(0);" id="refreshBtn" ng-click="refresh()"></a>
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
                    <th scope="row"><span class="must">*</span>訪查日期:</th>
                    <td>
                         <input id="tbDateStart2" class="text02"  onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateStart2',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                            ~ 
                         <input id="tbDateEnd2" class="text02" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateEnd2',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>疫苗:</th>
                    <td>
                        <div style="float:left">
                        <select  size="5" style="width:250px;">
                            <option>rHepB1 (B型肝炎遺傳工程疫苗)</option>
                            <option>rHepB2 (B型肝炎遺傳工程疫苗)</option>
                            <option>rHepB3 (B型肝炎遺傳工程疫苗)</option>
                            <option>BCG (卡介苗)</option>
                            <option>Tdap-IPV (減量破傷風白喉非細胞性百日核不活化小兒麻痺混合疫苗)</option>
                        </select>
                        </div>
                        <div>
                            <br/><br/>
                             <input  type="button" value="全選" class="btn" /><br/>
                             <input  type="button" value="取消選取"  class="btn"  />
                        </div>
                    </td>
                    
                  </tr>
                 <tr>
                    <th scope="row"><span class="must">*</span>異常類別:</th>
                    <td>
                        <select>
                              <option value="逾期">逾期</option>
                              <option value="提早">提早</option>
                        </select>
                    </td>
                    
                  </tr>
                 <tr>
                    <th scope="row"><span class="must">*</span>報表類型:</th>
                    <td colspan="5"> 
                        <label><input  type="radio"  name="rd" />統計表</label>
                        <label><input  type="radio"  name="rd" />明細表</label>
                    </td>
                  </tr>
             </table>
        </div>
        </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/VaccineOverdueEarlyVaccinationStatistics_JS")%>
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>