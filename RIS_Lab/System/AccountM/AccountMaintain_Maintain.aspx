<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountMaintain_Maintain.aspx.cs" Inherits="System_AccountM_AccountMaintain_Maintain" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <link href="/css/design.min.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
    <form style="display:none;" id='formid' method='post' target='_blank' action='AccountCheck_Detail_DownloadOP.aspx' autocomplete="off">
            <input type="hidden" id="i" name="i" value="<%:FileInfoID %>"/>
    </form>
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
        <form id="form1" runat="server" autocomplete="off" >
            <input type="hidden" name ="i" value="<%=ID %>" />
            <div class="formBtn formBtnleft">
                <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>
            <div class="formTb">
                <table>
                    <tr>
                        <th scope="row"><span class="must">*</span>帳號:</th>
                        <td>
                            <asp:Literal ID="lblAccount" ClientIDMode="Static" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="cbP" Text="業務承辦人" CssClass="text02" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>姓名:</th>
                        <td colspan="2">
                            <asp:TextBox required="required" MaxLength="10" ID="tbName" CssClass="text02" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>身分證號:</th>
                        <td colspan="2">
                            <asp:TextBox required="required" ID="tbRID" CssClass="text02" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>電話:</th>
                        <td colspan="2">
                            <asp:TextBox required="required" ID="tbPhone" CssClass="text02" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>電子信箱:</th>
                        <td colspan="2">
                            <asp:TextBox required="required" ID="tbEmail" type="email" CssClass="text03" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>單位:</th>
                        <td colspan="2">
                            <asp:TextBox required="required" ID="tbDept" Enabled="false" CssClass="text03" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">職稱:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbTitle" CssClass="text02" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>所屬角色:</th>
                        <td colspan="2">
                            <asp:CheckBoxList ID="cbList"  RepeatColumns="4"  RepeatDirection="Vertical"  ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>申請事由:</th>
                        <td colspan="2">
                            <asp:TextBox required="required" ID="tbReason"  Columns="70" Rows="5" TextMode="MultiLine"  CssClass="text01" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">申請單:</th>
                        <td colspan="2"  id="tmBlock" style="display:none;">
                            <%--<asp:LinkButton ID="lbDownload" OnClick="lbDownload_Click" ClientIDMode="Static" runat="server" />--%>
                            <%--<a href="javascript:void(0);" id="downloadBtn"><%=DisplayFileName %></a>--%>
                            <%--{{VM.fList}}--%>
                           <div  ng-repeat="item in VM.fList track by $index">
                                <span class="blank replaceA" ng-click="goDownload($index)" ng-bind='item["F"]'></span>
                                <span class="blank"></span>
                                <%--<span class="blank">
                                    <img style="cursor:pointer" src="/images/icon_del01.gif" ng-click="goDelete(item,$index)" />
                                </span>--%>
                            <br/>
                        </div>
                        <%--<asp:FileUpload ID="tbFile"  Multiple="Multiple"  CssClass="text01" ClientIDMode="Static"   runat="server"  />--%>
                        <%--<span class="wordred">(大小限3M以內，請勿傳空白檔案)</span>--%>
                         </td>
                    </tr>
                    <tr>
                        <th scope="row">審核狀態:</th>
                        <td colspan="2">
                            <asp:RadioButtonList ID="rbList"  RepeatColumns="50"  RepeatDirection="Vertical"  ClientIDMode="Static" runat="server" >
                                 <asp:ListItem Text="審核通過" Value="2"></asp:ListItem>
                                   <asp:ListItem Text="審核不通過" Value="3"></asp:ListItem>
                             </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">審核說明:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbDesp"  Columns="70" Rows="5" TextMode="MultiLine"   CssClass="text01" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var i = <%:ID %>;
        var fAry = <%=DisplayFileNamesListJson%>;
        var iAry = <%=FileInfoIDsListJson%>;
    </script>
    <%:Scripts.Render("~/bundles/AccountMaintain_Maintain_JS")%>
</asp:Content>



