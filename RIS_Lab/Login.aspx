<%--<%@ OutputCache Duration="1000" VaryByParam="none" %>--%>
<%@ Page Language="C#" ViewStateMode="Enabled"  AutoEventWireup="true"  CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="robots" content="noindex,nofollow" />
    <title>全國性預防接種資訊管理系統</title>
    <!--[if lt IE 9]>
        <script src="../bower_components/html5shiv/dist/html5shiv.min.js"></script>
    <![endif]-->
    <%:Styles.Render("~/bundles/Login_CSS")%>
    <style>
        #btnLogin:hover  {background-color: #dd7a77 !important;}
    </style>
</head>
<body>
    <form id="MyForm"  runat="server" defaultbutton="btnLogin" autocomplete="off">
        <section class="wrap">
  <section class="login"> 
    <section class="login_left">
      <table>
        <tr>
          <th scope="row"><label for="">使用者帳號：</label></th>
          <td><asp:TextBox required="required" ID="tbUser" ClientIDMode="Static" CssClass="text01" runat="server" /></td>
        </tr>
        <tr>
          <th scope="row"><label for="">使用者密碼：</label></th>
          <td><asp:TextBox required="required" ID="tbPassword" ClientIDMode="Static" TextMode="Password" CssClass="text01"  runat="server" /></td>
        </tr>
        <tr>
          <th scope="row"><label for="">圖片驗證碼：</label></th>
          <td><asp:TextBox required="required" ClientIDMode="Static" maxlength="4" ID="tbCode" CssClass="text02"  onfocus="javascript:this.select();" runat="server" />
            <img style="cursor:pointer" src="/Ashx/CheckCodeOP.ashx" width="107" height="29" alt="換一個" title="換一個" onclick="Change(this)" />
           </td>
        </tr>
        <tr>
          <th scope="row">憑證登入選項：</th>
          <td> <asp:RadioButton GroupName="rbGroup" ID="rbDoctor" Text="醫事人員卡" ClientIDMode="Static" runat="server" CssClass="radio01" />
              <asp:RadioButton GroupName="rbGroup" ID="rbCDC" Text="自然人憑證" ClientIDMode="Static" runat="server" CssClass="radio01" />
          </td>
        </tr>
      </table>
    </section>
    <section class="login_right">
      <div>
        <input  type="checkbox" value="" class="checkbox01"/>
        使用健保IC卡讀卡機
      </div>
      <ul>
        <li><asp:Button ID="btnLogin" runat="server" ClientIDMode="Static" Text="登入系統" OnClick="btnLogin_Click" 
            style="display: block;
            background-image: url(/images/login_icon02.png);
            background-position: 130px 12px;
            border: 1px solid #fff;
            background-repeat: no-repeat;
            padding: 6px 60px 6px 30px;
            width: 197px;
            font-size: 1.25em;
            color:#fff;
            text-decoration: none;
            background-color:transparent;
            cursor:pointer;
            text-align: left;"/></li>
        <li><asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" Text="取得OTP" OnClick="btnLogin_Click" /></li>
        <li><asp:LinkButton ID="LinkButton2" runat="server" ClientIDMode="Static" Text="忘記密碼" OnClick="btnLogin_Click" /></li>
        <li><asp:LinkButton ID="btnApplyAccount" runat="server" ClientIDMode="Static" Text="申請帳號" PostBackUrl="~/ApplyAccount.aspx" /></li>
      </ul>
    </section>
  </section>
  <footer>
    <div class="logo"><img src="images/footer_logo.png" width="43" height="43"></div>
    <p>10050台北市中正區林森南路6號　電話:02-2395-9825<br/>
      衛生福利部 疾病管制署Copyright: All right reserved. 2015</p>
    <div class="update">版本: 1.0.0 - 更新日期:105.1.1 </div>
  </footer>
</section>
</form>
    <%:Scripts.Render("~/bundles/Login_JS")%>
</body>
</html>
