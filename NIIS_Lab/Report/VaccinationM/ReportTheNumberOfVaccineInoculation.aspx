<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportTheNumberOfVaccineInoculation.aspx.cs" Inherits="Report_VaccinationM_ReportTheNumberOfVaccineInoculation" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
                    <th scope="row"><span class="must">*</span>出生日期:</th>
                    <td colspan="5">
                         <input id="tbDateStart" class="text02"  onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateStart',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                            ~ 
                         <input id="tbDateEnd" class="text02" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateEnd',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                    </td>
                  </tr>
                    <tr>
                         <th scope="row"><span class="must">*</span>戶籍地址:</th>
                        <td>
                            <select id="SelectCounty" ng-model="VM.SelectCounty"  ng-change="SelectCountyChange()">
                                 <option ng-repeat="option in VM.CountyAry" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                            <select id="SelectTown" ng-model="VM.SelectTown">
                                   <option ng-repeat="option in VM.TownAry" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                         </td>
                    </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>接種日期:</th>
                    <td colspan="5">
                         <input id="tbDateStart2" class="text02"  onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateStart2',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                            ~ 
                         <input id="tbDateEnd2" class="text02" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" />
                         <a href="javascript:void(0);"><img onclick="WdatePicker({el:'tbDateEnd2',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>接種日與出生日相差月數:</th>
                    <td colspan="5">
                        <input type="number" />
                    </td>
                  </tr>
                  <tr>
                     <th scope="row"><span class="must">*</span>接種地點:</th>
                     <td colspan="5">
                            <textarea cols="70" rows="5"   id="tbLocation">
                            </textarea>
                            <a href="javascript:void(0);"><img  src="/images/location.png" ng-click="openOrgs()" /></a>
                            <span style="color:crimson">複選</span>
                     </td>
                 </tr>
                   <tr>
                    <th scope="row"><span class="must">*</span>疫苗:</th>
                    <td style="width:80px;">
                        <select style="width:150px"></select>
                    </td>
                    <th scope="row"><span class="must">*</span>批號:</th>
                    <td colspan="3">
                        <div style="float:left">
                        <select multiple="multiple" size="5" style="width:150px;">
                            <option>test1</option>
                            <option>test2</option>
                            <option>test3</option>
                            <option>test4</option>
                            <option>test5</option>
                        </select>
                        </div>
                        <div>
                            <br/>
                             <span style="color:crimson">複選</span><br/>
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
                    <th scope="row"><span class="must">*</span>報表種類:</th>
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
     <%:Scripts.Render("~/bundles/ReportTheNumberOfVaccineInoculation_JS")%>
</asp:Content>