<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_CaseDataForVisit.ascx.cs" Inherits="CaseVisit_uc_CaseDataForVisit" %>

 
    <div class="formBtn formBtnleft">
      <input type="button" name="btnBack"  id ="btnBack" value="上一頁" class="btn" />
    </div>
    <div class="formTb formTb3">
      <table>
        <tr>
          <td width="33%"><table>
              <tr>
                <th scope="row">姓名：</th>
                <td><asp:Literal ID="ltName" runat="server"></asp:Literal></td>
              </tr>
              <tr>
                <th scope="row">身分證號：</th>
                <td><asp:Literal ID="ltIdNo" runat="server"></asp:Literal></td>
              </tr>
              <tr>
                <th scope="row">出生日期：</th>
                <td><asp:Literal ID="ltBirthDate" runat="server"></asp:Literal></td>
              </tr>
              <tr>
                <th scope="row">性別</th>
                <td><asp:Literal ID="ltGender" runat="server"></asp:Literal></td>
              </tr>
            </table></td>
          <td width="33%"><table>
              <tr>
                <th scope="row">戶號：</th>
                <td><asp:Literal ID="ltHouseNo" runat="server"></asp:Literal></td>
              </tr>
              <tr>
                <th scope="row">所屬轄區：</th>
                <td><asp:Literal ID="ltRegionName" runat="server"></asp:Literal></td>
              </tr>
              <tr>
                <th scope="row">身分別：</th>
                <td><asp:Literal ID="ltCap" runat="server"></asp:Literal></td>
              </tr>
              <tr>
                <th scope="row">語言：</th>
                <td><asp:Literal ID="ltLang" runat="server"></asp:Literal></td>
              </tr>
            </table></td>
          <td width="33%"><table>
              <tr>
                <th scope="row">母親姓名：</th>
                <td><asp:Literal ID="ltMotherName" runat="server"></asp:Literal></td>
              </tr>
              <tr>
                <th scope="row">母親身分證號：</th>
                <td><asp:Literal ID="ltMotherIdNo" runat="server"></asp:Literal></td>
              </tr>
              <tr>
                <th scope="row">母親出生日期：</th>
                <td><asp:Literal ID="ltMotherBirthDate" runat="server"></asp:Literal></td>
              </tr>
            </table></td>
        </tr>
        <tr>
          <td colspan="3"><table>
              <tr>
                <th scope="row">戶籍地址：</th>
                <td><asp:Literal ID="ltResAddr" runat="server"></asp:Literal></td>
              </tr>
              <tr>
                <th scope="row">通訊地址：</th>
                <td><asp:Literal ID="ltConAddr" runat="server"></asp:Literal></td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div> 
