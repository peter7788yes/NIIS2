<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyAccount.aspx.cs" Inherits="ApplyAccount" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ PreviousPageType VirtualPath="~/Login.aspx" %> 
<%@ Reference VirtualPath="~/Login.aspx" %> 

<!doctype html>
<html lang="zh-TW">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <title>全國性預防接種資訊管理系統</title>
    <!--[if lt IE 9]>
        <script src="../bower_components/html5shiv/dist/html5shiv.min.js"></script>
    <![endif]-->
    <%:Styles.Render("~/bundles/Login_CSS")%>
</head>
<body>
<section class="wrap">
  <section class="login"> 
    <section class="login_block">
      <div class="title">帳號申請</div>
      <form id="form1" runat="server"  autocomplete="off">
        <div class="formTb">
          <table>
           <%-- <tr>
              <th scope="row"><span class="must">*</span>帳號：</th>
              <td>
                  <asp:TextBox ID="tbAccount" CssClass="text01" runat="server" />
                  <span style="display:none;" class="wordred">【此帳號已有人使用】</span></td>
            </tr>--%>
            <tr>
              <th scope="row"><span class="must">*</span>申請人姓名：</th>
              <td>
                   <asp:TextBox ID="tbName" CssClass="text01" ClientIDMode="Static" runat="server" />
              </td>
            </tr>
            <tr>
              <th scope="row"><span class="must">*</span>身分證號：</th>
              <td>
                <asp:TextBox ID="tbID" CssClass="text01" ClientIDMode="Static" runat="server" />
                <span style="display:none;" class="wordred">【身分證號錯誤】</span></td>
            </tr>
            <tr>
              <th scope="row"><span class="must">*</span>聯絡電話：</th>
              <td>
                    <asp:TextBox ID="tbTel" CssClass="text01" ClientIDMode="Static" runat="server" />
              </td>
            </tr>
            <tr>
              <th scope="row"><span class="must">*</span>電子信箱：</th>
              <td>
                  <asp:TextBox ID="tbEmail" CssClass="text02" ClientIDMode="Static" runat="server" />
                  <span style="display:none;" class="wordred">【電子信箱不合法】</span></td>
            </tr>
            <tr>
              <th scope="row"><span class="must">*</span>單位：</th>
              <td>
                   <asp:TextBox  ID="tbLocation"  CssClass="text02" ng-click="openOrgs()" ClientIDMode="Static" runat="server" />
                   <img style="cursor:pointer" ng-click="openOrgs()" src="/images/location.png"  />
              </td>
            </tr>
            <tr>
              <th scope="row">職稱：</th>
              <td>
                   <asp:TextBox ID="tbTitle" CssClass="text02" ClientIDMode="Static" runat="server" />
              </td>
            </tr>
            <tr>
              <th scope="row"><span class="must">*</span>申請事由：</th>
              <td>
                    <asp:TextBox ID="tbReason" TextMode="MultiLine" Columns="70" Rows="5" CssClass="text02" ClientIDMode="Static" runat="server" />
               </td>
            </tr>
            <tr>
              <th scope="row"><span class="must">*</span>角色：</th>
              <td>

              </td>
            </tr>
          </table>

          <div class="formBtn">
            <asp:Button ClientIDMode="Static" ID="btnGo" runat="server" Text="送出申請" CssClass="btn" PostBackUrl="~/ApplyAccount_Next.aspx" />
          </div>

        </div>
      </form>
    </section>
  </section>
</section>
    <script src="js/jq/jquery-2.1.4.js"></script>
    <script src="ApplyAccount.js"></script>
</body>
</html>

