<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchoolCode_Add.aspx.cs" Inherits="System_CodeM_SchoolCode_Add"  MasterPageFile="~/MasterPage/MasterPage.master" %>
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
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>

         <form id="form1" runat="server" defaultbutton="btnSave" autocomplete="off">
            <div class="formBtn formBtnleft">
                 <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                 <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>

             <div class="formTb">
                <table>
                    <tr>
                       <th scope="row"><span class="must">*</span>學校代碼:</th>
                        <td>
                            <asp:TextBox ID="tbCode" CssClass="text02" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>學校名稱:</th>
                        <td>
                             <asp:TextBox ID="tbName" CssClass="text02" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                      <th scope="row"><span class="must">*</span>地址:</th>
                        <td>
                            <select id="SelectCounty" name="SelectCounty" ng-model="VM.SelectCounty" ng-change="SelectCountyChange()">
                                 <option value="0">縣市</option>
                                 <option ng-repeat="option in VM.CountyAry" ng-selected="{{option.I==VM.SelectCounty}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>

                            <select id="SelectTown" name="SelectTown" ng-model="VM.SelectTown"   ng-change="SelectTownChange()">
                                   <option value="0">鄉鎮</option>
                                   <option ng-repeat="option in VM.TownAry" ng-selected="{{option.I==VM.SelectTown}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>

                            <select id="SelectVillage" name="SelectVillage" ng-model="VM.SelectVillage"  >
                                   <option value="0">村里</option>
                                   <option ng-repeat="option in VM.VillageAry" ng-selected="{{option.I==VM.SelectVillage}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>

                            <asp:TextBox ID="tbAddress" CssClass="text02" ClientIDMode="Static" runat="server" />
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
                             <asp:DropDownList ID="ddEnState" CssClass="text02"  ClientIDMode="Static"  runat="server" />
                         </td>
                    </tr>
                </table>
            </div>
        </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
        <script>
            var countyJson  =<%=CountyJson%>;
            var townJson  =<%=TownJson%>;
            var villageJson  =<%=VillageJson%>; 
            var UP =<%=UpdatePower.HasPower ? 1 :0%>;
            var stateAry =<%=StateListAry %>;
        </script>
       <%:Scripts.Render("~/bundles/SchoolCode_Add_JS")%>
</asp:Content>



