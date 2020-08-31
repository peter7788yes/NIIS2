<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterData_Detail_StoolCard.aspx.cs" Inherits="Vaccination_RecordM_RegisterData_Detail_StoolCard" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content2">
        <h2>大便卡篩檢結果紀錄</h2>
          <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
            <div class="formTb formTb2">
                <table>
                    <tr>
                        <th scope="row">篩檢日期：</th>
                        <td>
                            <asp:TextBox ID="tbDate" CssClass="text02" ClientIDMode="Static"   runat="server"  />
                            <img style="cursor:pointer" onclick="WdatePicker({el:'tbDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                            <input type="hidden" name="c" value="<%=ID %>" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">篩檢地點：</th>
                        <td>
                            <asp:TextBox ID="tbLocation" CssClass="text01" ClientIDMode="Static"  runat="server"  />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">篩檢結果：</th>
                        <td>
                            <label for="rb1">
                                <asp:RadioButton GroupName="rbGroup" ID="rb1" ng-change="goStool(1)" ClientIDMode="Static"  runat="server"  />
                            正常
                            </label>
                          <label for="rb2">
                              <asp:RadioButton GroupName="rbGroup" ID="rb2" ng-change="goStool(2)" ClientIDMode="Static"  runat="server"  />
                            不正常
                          </label>
                          <label for="rb3">
                              <asp:RadioButton GroupName="rbGroup" ID="rb3" ng-change="goStool(3)" ClientIDMode="Static"  runat="server"  />
                            不確定或不知道
                          </label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="formBtn">
                <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                <input type="button" name="closeBtn" value="取消" class="btn" onclick="window.close();" />
                <input type="hidden" name="i"  value="<%=StoolCardID %>" />
            </div>
        </form>
    </section>
</asp:Content>

<asp:Content ContentPlaceHolderID="jsCP" runat="Server">
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>

