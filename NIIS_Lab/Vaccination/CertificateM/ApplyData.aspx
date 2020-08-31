<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyData.aspx.cs" Inherits="Vaccination_CertificateM_ApplyData"  MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content3">
            <h2>接種證明書</h2>
         <form id="MyForm" runat="server"  autocomplete="off">
            <div class="formTb formTb2">
                        <table>
                            <tr>
                                <th scope="row"><span class="must">*</span>個案姓名:</th>
                                <td>
                                     <asp:TextBox ID="tbName" required="required" Enabled="false" CssClass="text02" ClientIDMode="Static" ng-click="openOrgs()" runat="server" />
                                    <input type="hidden" name="i" value="<%:CaseUserID %>" />
                                </td>
                            </tr>
                            <tr>
                                <th scope="row"><span class="must">*</span>個案英文姓名:</th>
                                <td>
                                     <asp:TextBox ID="tbE" required="required" CssClass="text02" ClientIDMode="Static" ng-click="openOrgs()" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2"><hr/></td>
                                </tr>
                            <tr>
                                <th scope="row"><span class="must">*</span>申請人姓名:</th>
                                <td>
                                   <asp:TextBox ID="tbA" required="required" CssClass="text02" ClientIDMode="Static" ng-click="openOrgs()" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th scope="row"><span class="must">*</span>關係:</th>
                                <td>
                                   <asp:TextBox ID="tbR" required="required" CssClass="text02" ClientIDMode="Static" ng-click="openOrgs()" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th scope="row"><span class="must">*</span>檢附文件:</th>
                                <td>
                                    <asp:FileUpload required="required" ID="tbFile" CssClass="text01" ClientIDMode="Static"   runat="server" 
                                         accept="application/pdf,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" />
                                    <span class="wordblue">(限3M)</span>
                                </td>
                            </tr>
                            <tr>
                                <th scope="row"><span class="must">*</span>證明書格式:</th>
                                <td>
                                    <asp:RadioButton GroupName="rbA" ID="rb1" Text="橫式格式" ClientIDMode="Static" runat="server" CssClass="radio01" />
                                    <asp:RadioButton GroupName="rbA" ID="rb2" Text="直式格式" ClientIDMode="Static" runat="server" CssClass="radio01" />
                                </td>
                            </tr>
                        </table>
            </div>
            <div class="formBtn formBtncenter">
                  <asp:Button CssClass="btn" ID="btnSave" Text="確定" ClientIDMode="Static" runat="server" OnClick="btnSave_Click" />
            </div>
             </form>
    </section>
</asp:Content>

<asp:Content ContentPlaceHolderID="jsCP" runat="Server">
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>