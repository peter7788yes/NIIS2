<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyAccount_Next.aspx.cs" Inherits="ApplyAccount_Next" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ PreviousPageType VirtualPath="~/Account/ApplyAccount.aspx" %> 
<%@ Reference VirtualPath="~/Account/ApplyAccount.aspx" %> 

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
        <div class="formTb">
          <p>您的帳號已送審, 請您下載以下申請單, 帳號申請通過後, 將以電子郵件通知您.</p>
          <div class="link"><a href="javascript:void(0)">帳號申請單</a></div>
          <div class="formBtn">
                <input type="button" id="lastBtn" value="回首頁" class="btn" />
          </div>
        </div>
    </section>
  </section>
</section>
    <script src="js/jq/jquery-2.1.4.js"></script>
    <script src="ApplyAccount_Next.js"></script>
</body>
</html>

