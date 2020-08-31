<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountCheck_Detail.aspx.cs" Inherits="System_AccountM_AccountCheck_Detail" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
    <form style="display:none;" id='formid' method='post' target='_blank' action='AccountCheck_Detail_DownloadOP.aspx' autocomplete="off">
            <input type="hidden" id="i" name="i"  value="<%=FileInfoID %>"/>
    </form>
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>

        <form id="form1" runat="server" defaultbutton="btnSave"  autocomplete="off" >
            <div class="formBtn formBtnleft">
                <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>

            <div class="formTb">
                <table>
                    <tr>
                        <th scope="row"><span class="must">*</span>帳號:</th>
                        <td>
                            <asp:Literal ID="lblAccount"  ClientIDMode="Static" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="cbP" Text="業務承辦人" CssClass="text02" ClientIDMode="Static"  runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>姓名:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbName" CssClass="text02" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>身分證號:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbRID" CssClass="text02" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>電話:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbPhone" CssClass="text02" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>電子信箱:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbEmail" CssClass="text03" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>單位:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbDept" Enabled="false" CssClass="text03" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row">職稱:</th>
                        <td colspan="2">
                            <asp:TextBox ID="tbTitle" CssClass="text02" ClientIDMode="Static" runat="server" /></td>
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
                            <asp:TextBox ID="tbReason"  Columns="70" Rows="5" TextMode="MultiLine"  CssClass="text01" ClientIDMode="Static" runat="server" /></td>
                    </tr>
                    <tr>
                        <th scope="row">申請單:</th>
                        <td colspan="2">
                            <%--<asp:LinkButton ID="lbDownload" OnClick="lbDownload_Click" ClientIDMode="Static" runat="server" />--%>
                            <%--<a href="javascript:void(0);" id="downloadBtn"><%=DisplayFileName %></a>--%>
                            <%--{{VM.fList}}--%>
                           <div id="tmBlock" style="display:none;" ng-repeat="item in VM.fList track by $index">
                                <span class="blank" ng-bind='item["F"]'></span>
                                <span class="blank"></span>
                                <span class="blank">
                                    <a href="javascript:void(0);" ng-click="goDelete(item,$index)"><img src="/images/icon_del01.gif"  alt="刪除"/></a>
                                </span>
                            <br/>
                        </div>
                        <asp:FileUpload ID="tbFile" Multiple="Multiple"  CssClass="text01" ClientIDMode="Static"   runat="server"  />
                        <span class="wordred">(大小限3M以內，請勿傳空白檔案)</span>
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

             <input type="hidden" name ="i" value="<%=ID %>" />
        </form>
    </section>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var i = <%=ID %>;
        var fAry = <%=DisplayFileNamesListJson%>;
        var iAry = <%=FileInfoIDsListJson%>;
    </script>
    <%:Scripts.Render("~/bundles/AccountCheck_Detail_JS")%>
</asp:Content>



