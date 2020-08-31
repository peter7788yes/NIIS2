﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountInfo.aspx.cs" Inherits="System_AccountM_AccountInfo" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content">
        <div class="path"></div>
        <form id="myForm" ClientIDMode="Static" runat="server"  autocomplete="off">
        <div class="formBtn formBtnleft">
               <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" ClientIDMode="Static" OnClick="btnSave_Click" />
        </div>
        <div class="formTb">
             <table>
              <tr>
                <th scope="row"><span class="must">*</span>帳號:</th>
                <td colspan="2"><asp:Literal ID="lblAccount" ClientIDMode="Static" runat="server"/></td>
              </tr>
              <%--<tr>
                <th scope="row"><span class="must">*</span>密碼:</th>
                <td>
                     <asp:TextBox  ID="tbPWD"  TextMode="Password" value="*************************" Enabled="false"  CssClass="text01" ClientIDMode="Static"  runat="server" />
                </td>
                <td>
                      <asp:Button CssClass="btn" ID="btnChange" Text="變更密碼"  runat="server" OnClick="btnChange_Click" />
                </td>
              </tr>--%>
              <%--<tr>
                <th scope="row"><span class="must">*</span>確認密碼:</th>
                <td><asp:TextBox  ID="tbPWD2" TextMode="Password" Enabled="false" CssClass="text01" ClientIDMode="Static"  runat="server" /></td>
                <td><span id="notPWD"  style="display:none;color:crimson">密碼不相符</span></td>
              </tr>--%>
             <tr>
                <th scope="row"><span class="must">*</span>身分證號:</th>
                <td colspan="2"><asp:TextBox ID="tbIDF" CssClass="text01"  required="required" ClientIDMode="Static"  runat="server" /></td>
              </tr>
                 <tr>
                <th scope="row"><span class="must">*</span>姓名:</th>
                <td colspan="2"><asp:TextBox ID="tbName" CssClass="text01" required="required"  ClientIDMode="Static"  runat="server" /></td>
              </tr>
             <tr>
                <th scope="row"><span class="must">*</span>電話:</th>
                <td colspan="2"><asp:TextBox ID="tbTel" CssClass="text01" required="required"  ClientIDMode="Static"  runat="server" /></td>
              </tr>
             <tr>
                <th scope="row"><span class="must">*</span>電子信箱:</th>
                <td colspan="2"><asp:TextBox ID="tbEmail" CssClass="text01" required="required" type="email" ClientIDMode="Static"  runat="server" /></td>
              </tr>
              <tr>
                <th scope="row"><span class="must">*</span>單位:</th>
                <td colspan="2"><asp:TextBox ID="tbDept" Enabled="false" required="required"  CssClass="text01" ClientIDMode="Static"  runat="server" /></td>
              </tr>
               <tr>
                <th scope="row">職稱:</th>
                <td colspan="2"><asp:TextBox ID="tbTitle" CssClass="text01" ClientIDMode="Static"  runat="server" /></td>
              </tr>
             </table>
        </div>
       </form>
    </section>
</asp:Content>