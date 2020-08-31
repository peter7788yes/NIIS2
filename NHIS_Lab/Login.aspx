<%@ Page Language="C#" ViewStateMode="Enabled"  AutoEventWireup="true"  CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <title>全國性預防接種資訊管理系統</title>
     <%:Styles.Render("~/bundles/Login_CSS")%>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnLogin">
        <section class="wrap">
  <section class="login"> 
    <!--登入左欄 start -->
    <section class="login_left">
      <table>
        <tr>
          <th scope="row"><label for="">使用者帳號：</label></th>
          <td><asp:TextBox ID="tbUser" CssClass="text01" runat="server" value="system" /></td>
        </tr>
        <tr>
          <th scope="row"><label for="">使用者密碼：</label></th>
          <td><asp:TextBox ID="tbPassword" TextMode="Password" CssClass="text01"  runat="server" value="admin" /></td>
        </tr>
        <tr>
          <th scope="row"><label for="">圖片驗證碼：</label></th>
          <td><asp:TextBox ID="tbCode" CssClass="text02"  onfocus="javascript:this.select();" runat="server" />
            <img src="/Ashx/CheckCodeOP.ashx" width="107" height="29" alt="換一個" title="換一個" onclick="Change(this)" />
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
    
    <!--登入左欄 end --> 
    <!--登入右欄 start -->
    <section class="login_right">
      <!--div>
        <input name="" type="checkbox" value="" class="checkbox01"/>
        使用健保IC卡讀卡機</div--> 
      <ul>
	   
        <li><asp:LinkButton ID="btnLogin" CssClass="loginbtn" ClientIDMode="Static" runat="server" Text="登入系統" OnClick="btnLogin_Click" /></li>
        <li><asp:LinkButton ID="LinkButton2" runat="server" ClientIDMode="Static" Text="忘記密碼" OnClick="btnLogin_Click" /></li>
        <li><asp:LinkButton ID="LinkButton3" runat="server" ClientIDMode="Static" Text="申請帳號" OnClick="btnLogin_Click" /></li>
      </ul>
    </section>
    <!--登入右欄 end --> 
  
            
  </section>
  <footer>
    <div class="logo"><img src="images/footer_logo.png" width="43" height="43"></div>
    <p>10050台北市中正區林森南路6號　電話:02-2395-9825<br>
      衛生福利部 疾病管制署Copyright: All right reserved. 2015</p>
    <div class="update">版本: 1.0.0 - 更新日期:105.1.1 </div>
  </footer>
</section>
       
        
 <asp:Panel ID="Panel1" Visible="false" runat="server">
        <div class="moicalogin" style="display:none;"> 
			 <img src="images/moica.gif" alt="使用憑證登入" title="使用憑證登入" onclick="checkCDCkey()" onkeypress="checkCDCkey()" />
                <br />
			 <iframe frameborder="0" width="280" height="80" scrolling="no" id="CitizenDigital"></iframe>
		</div>   
        <input type="hidden" id="getKeyVal" name="getKeyVal" />
       錯誤訊息:
      <asp:Label ID="lblError" runat="server" Text="Label" />
</asp:Panel>

</form>

    <%:Scripts.Render("~/bundles/Login_JS")%>

    <script language="javascript" type="text/javascript">

        function checkCDCkey() {
            document.getElementById('CitizenDigital').src = "CitizenDigitalCertificateFirefoxCompatible/login.html";
            //newwin=window.open('/CitizenDigitalCertificateFirefoxCompatible/login.html','','height=315,width=350,status=no,toolbar=no,menubar=no,location=no,resizable=yes','');
        }
        function getCDKey(key) {
            document.getElementById("getKeyVal").value = key;
            // alert(document.getElementById("getKeyVal").value);
            document.form1.submit();
            //closeWindow();

        }

        function Change(sender) {
            sender.src = "/Ashx/CheckCodeOP.ashx?nocache=" + Math.random();
        }
    </script>

</body>
</html>
