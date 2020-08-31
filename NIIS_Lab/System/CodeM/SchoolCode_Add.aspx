<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchoolCode_Add.aspx.cs" Inherits="System_CodeM_SchoolCode_Add"  MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
         <form id="MyForm" ClientIDMode="Static" runat="server"  autocomplete="off">
             <div class="formTb">
                <table>
                    <tr>
                       <th scope="row"><span class="must">*</span>學校代碼:</th>
                        <td>
                            <asp:TextBox ID="tbCode" required="required" CssClass="text02" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>學校名稱:</th>
                        <td>
                             <asp:TextBox ID="tbName" required="required" CssClass="text02" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                      <th scope="row"><span class="must">*</span>地址:</th>
                        <td>
                            <select id="SelectCounty" required="required" name="SelectCounty" ng-model="VM.SelectCounty" ng-change="SelectCountyChange()">
                                 <option value="">縣市</option>
                                 <option ng-repeat="option in VM.CountyAry" ng-selected="{{option.I==VM.SelectCounty}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                            <select id="SelectTown" required="required" name="SelectTown" ng-model="VM.SelectTown"   ng-change="SelectTownChange()">
                                   <option value="">鄉鎮</option>
                                   <option ng-repeat="option in VM.TownAry" ng-selected="{{option.I==VM.SelectTown}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                            <select id="SelectVillage" required="required" name="SelectVillage" ng-model="VM.SelectVillage"  >
                                   <option value="">村里</option>
                                   <option ng-repeat="option in VM.VillageAry" ng-selected="{{option.I==VM.SelectVillage}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                            <asp:TextBox ID="tbAddress" required="required" CssClass="text02" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                      <th scope="row">電話:</th>
                        <td>
                            <asp:TextBox ID="tbTel" CssClass="text02" ClientIDMode="Static" runat="server" /><span style="color:#F99">格式: (區號)電話號碼</span>
                         </td>
                    </tr>
                    <tr>
                       <th scope="row"><span class="must">*</span>狀態:</th>
                        <td>
                             <asp:DropDownList ID="ddlEnState" CssClass="text02"  ClientIDMode="Static"  runat="server" />
                         </td>
                    </tr>
                </table>
            </div>
            <div class="formBtn">
                 <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                 <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>
            <input name="hash" type="hidden" value="<%=GetString("hash") %>"/>
        </form>
    </section>
    <script>
            var UP =<%=UpdatePower.HasPower ? 1 :0%>;
            var stateAry =<%=StateListAry %>;
    </script>
</asp:Content>

