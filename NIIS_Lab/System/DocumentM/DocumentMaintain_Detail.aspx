<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeFile="DocumentMaintain_Detail.aspx.cs" Inherits="DocumentManagementM_DocumentMaintain_Detail"  MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
         <form id="MyForm" runat="server"  autocomplete="off">
        <div class="formBtn formBtnright">
            <% if (DeletePower.HasPower) { %>
                <asp:Button CssClass="btn" ID="btnRemove" Text="刪除" runat="server"  OnClick="btnRemove_Click" />
            <%} %>
        </div>
        <div class="formTb">
            <table>
                <tr>
                    <th scope="row"><span class="must">*</span>發佈日期：</th>
                    <td><asp:Literal ID="lblDate" ClientIDMode="Static"  runat="server"  /></td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>標題：</th>
                    <td>
                        <asp:TextBox ID="tbTitle" CssClass="text01" ClientIDMode="Static"  runat="server"  />
                    </td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>檔案名稱：</th>
                    <td>
                        <%--<%=FilesHtml %>--%>
                        <%--{{VM.fileList}}--%>
                        <div ng-repeat="item in VM.fileList track by $index">
                            <span class="blank" ng-bind='item["DF"]'></span>
                            <span class="blank"></span>
                             <% if (UpdatePower.HasPower) { %>
                                <span class="blank">
                                  <img style="cursor:pointer" ng-click="goDelete(item,$index)" src="/images/icon_del01.gif"  />
                                </span>
                             <%} %>
                            <br/>
                        </div>
                        <asp:FileUpload ID="tbFile" Multiple="Multiple"  CssClass="text01" ClientIDMode="Static"   runat="server"  />
                        <span class="wordred">(大小限3M以內，請勿傳空白檔案)</span>
                    </td>
                </tr>
                <tr>
                    <th scope="row">說明：</th>
                    <td>
                        <asp:TextBox ID="tbDesp" Rows="5" CssClass="text01" ClientIDMode="Static"   runat="server"  />
                    </td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>上架狀態：</th>
                    <td>
                        <asp:RadioButton GroupName="rbS" ID="rb1" ClientIDMode="Static" Text="上架" runat="server" CssClass="radio01" />
                        <asp:RadioButton GroupName="rbS" ID="rb2" ClientIDMode="Static" Text="下架" runat="server" CssClass="radio01" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formBtn">
            <% if (UpdatePower.HasPower) { %>
                <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server"  OnClick="btnSave_Click" />
            <%} %>
            <input type="button" id="lastBtn" value="回上一頁" class="btn" />
        </div>
        <input type="hidden" name="i" value="<%:ID%>" />
        <input name="hash" type="hidden" value="<%=GetString("hash") %>"/>
       </form>
    </section>
    <script>
        var i =<%:ID%>;
        var fileList = <%=FileList%>;
    </script>
</asp:Content>