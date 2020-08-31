<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YellowCard_Add.aspx.cs" Inherits="Vaccination_RecordM_YellowCard_Add" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content2">
        <h2>申請黃卡紀錄</h2>
          <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
            <div class="formTb formTb2">
                <table>
                    <tr>
                        <th scope="row">核發日期：</th>
                        <td>
                            <asp:TextBox ID="tbDate" CssClass="text02" ClientIDMode="Static"   runat="server" />
                            <img style="cursor:pointer" onclick="WdatePicker({el:'tbDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>

                        </td>
                    </tr>
                    <tr>
                        <th scope="row">核發地點：</th>
                        <td>
                            <asp:TextBox ID="tbLocation" CssClass="text01" ClientIDMode="Static"  runat="server"  />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">承辦人：</th>
                        <td>
                            <asp:TextBox ID="tbUser" CssClass="text02" ClientIDMode="Static" Enabled="false" runat="server"  />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">核發格式：</th>
                        <td>
                            <asp:RadioButton GroupName="rbGroup" ID="rbYellowCard" Text="黃卡" ClientIDMode="Static" runat="server" CssClass="radio01" />
                            <asp:RadioButton GroupName="rbGroup" ID="rbRecord" Text="預防接種紀錄" ClientIDMode="Static" runat="server" CssClass="radio01" />
                        </td>
                    </tr>
                </table>
                <div class="wordred">(請於個案紙本黃卡加註更新日期)</div>
                <input type="hidden" name="c" value="<%=ID %>" />
            </div>
            <div class="formBtn">
                <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                <input type="button" name="closeBtn" value="取消" class="btn" onclick="window.close();" />
            </div>
        </form>
    </section>
</asp:Content>

<asp:Content ContentPlaceHolderID="jsCP" runat="Server">
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>

