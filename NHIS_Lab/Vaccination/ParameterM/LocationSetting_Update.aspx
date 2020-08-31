<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocationSetting_Update.aspx.cs" Inherits="Vaccination_ParameterM_LocationSetting_Update" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
     <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>

        <form id="form1" runat="server" defaultbutton="btnSave">
            <div class="formBtn formBtnleft">
                <% if (UpdatePower.HasPower) { %>
                      <asp:Button CssClass="btn" ID="btnSave" Text="修改儲存" ClientIDMode="Static" runat="server" OnClick="btnSave_Click" />
                <% } %>
                <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>

            <div class="formTb">
                <table>
                    <tr>
                       <th scope="row">醫事機構名稱:</th>
                        <td>
                            <asp:Literal ID="lblName" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">醫事機構代碼:</th>
                        <td>
                             <asp:Literal ID="lblCode" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                      <th scope="row">所屬單位:</th>
                        <td>
                            <asp:TextBox ID="tbOrg" CssClass="text02" Enabled="false" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="hfOrgID" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                      <th scope="row">院所地址:</th>
                        <td>
                            <select id="SelectCounty" name="SelectCounty" ng-model="VM.SelectCounty" ng-change="SelectCountyChange()">
                                 <option ng-repeat="option in VM.CountyAry" ng-selected="{{option.I==VM.SelectCounty}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>

                            <select id="SelectTown" name="SelectTown" ng-model="VM.SelectTown"   ng-change="SelectTownChange()">
                                   <option ng-repeat="option in VM.TownAry" ng-selected="{{option.I==VM.SelectTown}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>

                            <select id="SelectVillage" name="SelectVillage" ng-model="VM.SelectVillage"  >
                                   <option ng-repeat="option in VM.VillageAry" ng-selected="{{option.I==VM.SelectVillage}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>

                            <asp:TextBox ID="tbAddress" CssClass="text02" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                       <th scope="row">院所電話:</th>
                        <td>
                              <asp:TextBox ID="tbTel" CssClass="text02" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                     <th scope="row">可施打的疫苗:<span style="color:red">(複選)</span></th>
                        <td>
                             <asp:TextBox ID="tbVaccine" Columns="70" Rows="5" TextMode="MultiLine"  ClientIDMode="Static" runat="server" />
                             <asp:HiddenField ID="hfVaccineIDs"   ClientIDMode="Static" runat="server" />
                             <a href="javascript:void(0);"  ng-click="openAddVaccine(<%=ID %>);"><img  src="/images/icon_needle.png" /></a>
                         </td>
                    </tr>
                    <tr>
                        <th scope="row">接種時間</th>
                        <td>
                             <asp:TextBox ID="tbSchedule" Columns="70" Rows="5" TextMode="MultiLine"  ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                        <th scope="row">科別:</th>
                        <td id="td">
                         </td>
                        <td style="display:none;">
                            <input name="did" id ="did" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">預注資料申報方式:</th>
                        <td>
                               <asp:RadioButton GroupName="rbGroup" ID="rb1" Text="系統登錄" ClientIDMode="Static" runat="server" CssClass="radio01" />
                               <asp:RadioButton GroupName="rbGroup" ID="rb2" Text="健保申報" ClientIDMode="Static" runat="server" CssClass="radio01" />
                               <asp:RadioButton GroupName="rbGroup" ID="rb3" Text="檔案匯入" ClientIDMode="Static" runat="server" CssClass="radio01" />
                         </td>
                    </tr>
                    <tr>
                        <th scope="row">單純流感院所:</th>
                        <td>
                               <asp:RadioButton GroupName="rbGroupB" ID="rbB1" Text="是" ClientIDMode="Static" runat="server" CssClass="radio01" />
                               <asp:RadioButton GroupName="rbGroupB" ID="rbB2" Text="否" ClientIDMode="Static" runat="server" CssClass="radio01" />
                         </td>
                    </tr>
                    <tr>
                     <th scope="row"><span class="must">*</span>接種單位狀態:</th>
                        <td>
                                <asp:DropDownList ID="ddAgState" CssClass="text02"  ClientIDMode="Static"  runat="server" />
                         </td>
                    </tr>
                     <tr>
                     <th scope="row"><span class="must">*</span>營業狀態:</th>
                        <td>
                                <asp:Literal ID="lblBsState"   ClientIDMode="Static"  runat="server" />
                         </td>
                    </tr>
                </table>
            </div>
        </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var county="<%=County%>";
        var town ="<%=Town%>";
        var village ="<%=Village%>";
        var countyJson  =<%=CountyJson%>;
        var townJson  =<%=TownJson%>;
        var villageJson  =<%=VillageJson%>;
        var UP =<%=UpdatePower.HasPower ? 1 :0%>;
        var stateAry =<%=StateListAry %>;
        var tbOther ="<%=tbOther%>";
        var tbOtherIDs ="<%=tbOtherIDs%>";
    </script>
     <%:Scripts.Render("~/bundles/LocationSetting_Update_JS")%>
</asp:Content>


