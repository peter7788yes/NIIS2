<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocationSetting_Detail.aspx.cs" Inherits="Vaccination_ParameterM_LocationSetting_Detail" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/CheckLog.ascx" TagPrefix="uc1" TagName="CheckLog" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
     <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
        <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
            <div class="formTb">
                <table>
                    <tr>
                       <th scope="row">醫事機構名稱：</th>
                        <td>
                            <asp:Literal ID="lblName" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">醫事機構代碼：</th>
                        <td>
                             <asp:Literal ID="lblCode" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                      <th scope="row">所屬單位：</th>
                        <td>
                            <asp:TextBox ID="tbOrg" CssClass="text02" Enabled="false" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="hfOrgID" ClientIDMode="Static" runat="server" />
                            <input name="i" value="<%:ID %>" type="hidden" />
                         </td>
                    </tr>
                    <tr>
                      <th scope="row"><span class="must">*</span>院所類別：</th>
                        <td>
                            <asp:DropDownList required="required" ID="ddlAgencyCate" ClientIDMode="Static" runat="server" >
                                      <asp:ListItem Text="請選擇" Value="" />
                                      <asp:ListItem Text="醫學中心" Value="1" />
                                      <asp:ListItem Text="區域醫院" Value="2" />
                                      <asp:ListItem Text="地區醫院" Value="3" />
                                      <asp:ListItem Text="診所" Value="4" />
                            </asp:DropDownList>
                         </td>
                    </tr>
                    <tr>
                      <th scope="row"><span class="must">*</span>院所地址：</th>
                        <td>
                            <select ng-hide="VM.CountyAry.length>0">
                                <option value="<%:County %>"><%:CountyName %></option>
                            </select>
                            <select required="required" style="display:none;" id="SelectCounty" name="SelectCounty" ng-model="VM.SelectCounty" ng-change="SelectCountyChange()" ng-cloak>
                                 <option ng-repeat="option in VM.CountyAry" ng-selected="{{option.I==VM.SelectCounty}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                            <select ng-hide="VM.TownAry.length>0">
                                 <option value="<%:Town %>"><%:TownName %></option>
                            </select>
                            <select required="required" style="display:none;" id="SelectTown" name="SelectTown" ng-model="VM.SelectTown"   ng-change="SelectTownChange()" ng-cloak>
                                   <option ng-repeat="option in VM.TownAry" ng-selected="{{option.I==VM.SelectTown}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                            <select ng-hide="VM.VillageAry.length>0">
                                    <option value="<%:Village %>"><%:VillageName %></option>
                            </select>
                            <select required="required" style="display:none;" id="SelectVillage" name="SelectVillage" ng-model="VM.SelectVillage" ng-cloak>
                                   <option ng-repeat="option in VM.VillageAry" ng-selected="{{option.I==VM.SelectVillage}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                            <asp:TextBox required="required" ID="tbAddress" CssClass="text02" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                       <th scope="row">院所電話：</th>
                        <td>
                              <asp:TextBox pattern="0\d{1,3}" title="格式錯誤" ID="tbTelZone" MaxLength="3" size="3" ClientIDMode="Static" runat="server" />
                              <asp:TextBox pattern="\d{6,10}" title="格式錯誤" ID="tbTel" MaxLength="10"   CssClass="text02" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                     <th scope="row">可施打的疫苗：<span style="color:red">(複選)</span></th>
                        <td>
                             <asp:TextBox ID="tbVaccine" Columns="70" Rows="5" TextMode="MultiLine"  ClientIDMode="Static" runat="server" />
                             <asp:HiddenField ID="hfVaccineIDs"   ClientIDMode="Static" runat="server" />
                             <img style="cursor:pointer" ng-click="openAddVaccine(<%:ID %>);" src="/images/icon_needle.png" />
                         </td>
                    </tr>
                    <tr>
                        <th scope="row">接種時間：</th>
                        <td ng-cloak>
                             <input type="button" value="新增接種時間" ng-click="addDay()" class="btn" />
                             <span ng-repeat="record in VM.DayTime track by $index">
                                 <br/>
                                 星期幾：
                                 <label><input type="checkbox" ng-model="record.d1"/>星期一</label>
                                 <label><input type="checkbox" ng-model="record.d2"/>星期二</label>
                                 <label><input type="checkbox" ng-model="record.d3"/>星期三</label>
                                 <label><input type="checkbox" ng-model="record.d4"/>星期四</label>
                                 <label><input type="checkbox" ng-model="record.d5"/>星期五</label>
                                 <label><input type="checkbox" ng-model="record.d6"/>星期六</label>
                                 <label><input type="checkbox" ng-model="record.d7"/>星期日</label>
                                 <input style="margin-left:10px;" class='btn' ng-show="$index>0" type="button" value="移除" ng-click="removeDay($index)" />
                                 <br/>
                                 <span ng-repeat="time in record.timeAry track by $index">
                                     接種時段：
                                     <input ng-model="time.ss" id="ss{{$parent.$index}}{{$index}}" ng-click="VM.fillssee($parent.$index,$index,$event);" onchange="fillssee(this)" type="text" class="text04" />
                                     <img  style="cursor:pointer" ng-click="clickDayTime(true,$parent.$index,$index);" src="/images/icon_calendar.png"/>
                                     <span class='h10'>~</span>
                                     <input ng-model="time.ee" id="ee{{$parent.$index}}{{$index}}" ng-click="VM.fillssee($parent.$index,$index);" onchange="fillssee(this)" type="text" class="text04" />
                                     <img  style="cursor:pointer" ng-click="clickDayTime(false,$parent.$index,$index);" src="/images/icon_calendar.png"/>
                                     <input ng-show="$index==0" style="margin-left:10px" type="button" value="新增接種時段" ng-click="addTime($parent.$index)" class="btn" />
                                     <img ng-show="$index>0" class='h10 icon_no' style='cursor:pointer;' ng-click="removeTime($parent.$index,$index)"  src="/images/icon_no.png" />
                                     <br/>
                                 <span>
                             </span>
                             <asp:TextBox Visible="false" ID="tbSchedule" Columns="70" Rows="5" TextMode="MultiLine"  ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                        <th scope="row">科別：</th>
                        <td id="td">
                         </td>
                        <td style="display:none;">
                            <input name="did" id ="did" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>預注資料申報方式：</th>
                        <td>
                               <asp:RadioButton GroupName="rbGroup" ID="rb1" Text="系統登錄" ClientIDMode="Static" runat="server" CssClass="radio01" />
                               <asp:RadioButton GroupName="rbGroup" ID="rb2" Text="健保申報" ClientIDMode="Static" runat="server" CssClass="radio01" />
                               <asp:RadioButton GroupName="rbGroup" ID="rb3" Text="檔案匯入" ClientIDMode="Static" runat="server" CssClass="radio01" />
                               <asp:RadioButton GroupName="rbGroup" ID="rb4" Text="HIS介接"  ClientIDMode="Static" runat="server" CssClass="radio01" />
                         </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>單純流感院所：</th>
                        <td>
                               <asp:RadioButton GroupName="rbGroupB" ID="rbB1" Text="是" ClientIDMode="Static" runat="server" CssClass="radio01" />
                               <asp:RadioButton GroupName="rbGroupB" ID="rbB2" Text="否" ClientIDMode="Static" runat="server" CssClass="radio01" />
                         </td>
                    </tr>
                    <tr>
                     <th scope="row"><span class="must">*</span>接種單位狀態：</th>
                        <td ng-cloak>
                                <asp:DropDownList ID="ddlAgState" required="required" ng-model="VM.ddlAgState" CssClass="text02"  ClientIDMode="Static"  runat="server" />
                                <br/>
                                <label ng-show="VM.ddlAgState==2" class="tbDates">
                                    <span class="must">*</span>加入合約日期：
                                    <asp:TextBox ID="tbDateStart" class="text04"  ClientIDMode="Static" runat="server" />
                                    <img style="cursor:pointer" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'tbDateEnd\')}',el:'tbDateStart',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                                </label>
                                <label ng-show="VM.ddlAgState==2" class="tbDates">
                                    <span class="must">*</span>合約到期日期：
                                    <asp:TextBox ID="tbDateEnd" class="text04" ClientIDMode="Static" runat="server" />
                                    <img  style="cursor:pointer" onclick="WdatePicker({minDate:'#F{$dp.$D(\'tbDateStart\')}',el:'tbDateEnd',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                                </label>
                                <label ng-show="VM.ddlAgState==3" class="tbDates">
                                    <span class="must">*</span>解約日期：
                                    <asp:TextBox ID="tbDateStop" class="text04"  ClientIDMode="Static" runat="server" />
                                    <img  style="cursor:pointer" onclick="WdatePicker({el:'tbDateStop',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                                </label>
                                <input name="i2" value="<%=ContractID %>" type="hidden" />
                         </td>
                    </tr>
                     <tr>
                     <th scope="row"><span class="must">*</span>營業狀態：</th>
                        <td>
                                <asp:Literal ID="lblBsState" ClientIDMode="Static"  runat="server" />
                         </td>
                    </tr>
                </table>
            </div>
            <div class="formBtn">
                <% if (UpdatePower.HasPower) { %>
                      <asp:Button CssClass="btn" ID="btnSave" Text="修改儲存" ClientIDMode="Static" runat="server" OnClick="btnSave_Click" />
                <% } %>
                <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>
            <input name="hash" type="hidden" value="<%=GetString("hash") %>"/>
            <input name="daytime" id="daytime" type="hidden" />
        </form>
        <uc1:CheckLog runat="server" ID="uc1" />
    </section>
    <script>
        var county="<%:County%>";
        var town ="<%:Town%>";
        var village ="<%:Village%>";
        var countyJson  =<%=CountyJson%>;
        var townJson  =<%=TownJson%>;
        var villageJson  =<%=VillageJson%>; 
        var UP =<%:UpdatePower.HasPower ? 1 :0%>;
        var stateAry =<%=StateListAry %>;
        var tbOther ="<%:tbOther%>";
        var tbOtherIDs ="<%:tbOtherIDs%>";
        var ddListOutAry =<%=ddListOutAry %>;
        var AgencyState ="<%:AgencyState%>";
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="jsCP" runat="Server">
     <script src="/js/date/WdatePicker.js"></script>
</asp:Content>


