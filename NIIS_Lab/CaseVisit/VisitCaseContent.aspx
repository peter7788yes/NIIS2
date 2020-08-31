<%@ Page Title="" Language="C#" enableEventValidation="false"  MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="VisitCaseContent.aspx.cs" Inherits="CaseVisit_VisitCaseContent" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
 <link href="/css/design.css" rel="stylesheet" type="text/css" />  
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" /> 
 <link href="/css/tab.css" rel="stylesheet" type="text/css" />

</asp:Content>
 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">


 <section class="Content"  >
        <div class="path"></div>
 
  <!-- form start-->
  <form runat ="server" id="form1" >    
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="formTb formTb2 formTb3">
      <table style="width:auto">
        <tr>
          <th scope="row" class="aRight" >身分證號：</th>
          <td><asp:Literal ID="ltIdNo" runat="server"></asp:Literal></td>
          <th scope="row" class="aRight" >姓名：</th>
          <td><asp:Literal ID="ltName" runat="server"></asp:Literal></td>
          <th scope="row" class="aRight">性別：</th>
          <td><asp:Literal ID="ltGender" runat="server"></asp:Literal></td>
          <th scope="row" class="aRight" >出生日期：</th>
          <td><asp:Literal ID="ltBirthDate" runat="server"></asp:Literal></td>
        </tr>
      </table>
    </div>
    
    <br/>
    <div class="formTb formTb2 formTb5 ">
      <table>
        <tr>
          <th scope="row" style="width:90pt"><span class="must">*</span>訪查日期：</th>
          <td align="left">
          <asp:TextBox ID="tbVisitDate" runat="server"></asp:TextBox>
          <a href="#"><asp:Image ID="VisitDateImg" ImageUrl="../images/icon_calendar.png" runat="server"></asp:Image></a>
           <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="tbVisitDate"   ID="rfvVisitDate" runat="server"  ErrorMessage="*必填"></asp:RequiredFieldValidator> 
                          

          </td>
          <th scope="row"><span class="must">*</span>訪查人員：</th>
          <td>
          <asp:DropDownList ID="ddlVisitMan" runat="server"></asp:DropDownList>
              <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="ddlVisitMan"   ID="rfvVisitMan" InitialValue ="" runat="server"  ErrorMessage="*必填"></asp:RequiredFieldValidator> 
         
            </td>
          <th scope="row"><span class="must">*</span>登錄人員：</th>
          <td>
          <asp:TextBox ID="tbKeyInMan" runat="server"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <th scope="row"><span class="must">*</span>訪查單位：</th>
          <td>
          <asp:TextBox ID="tbVisitOrgName" runat="server" ></asp:TextBox>
          
          </td>
          <th scope="row"><span class="must">*</span>訪查方式</th>
          <td colspan="3">

          <asp:DropDownList ID="ddlVisitType" runat="server"></asp:DropDownList>
              <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="ddlVisitType"   ID="rfvddlVisitType" InitialValue ="" runat="server"  ErrorMessage="*必填"></asp:RequiredFieldValidator> 
         
            </td>
        </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>
        <table>
        <tr>
          <th scope="row" style="width:90pt"><span class="must">*</span>疫苗別：</th>
          <td colspan="5"> 
          <asp:HiddenField ID="hdVac" runat="server"></asp:HiddenField>
           
          <asp:TextBox ID="tbVac" AutoPostBack="true" runat="server" CssClass="text03 tbVac" 
                  ontextchanged="tbVac_TextChanged"></asp:TextBox> 
            <a href="#" onclick="void(0);" class="AddVac"><img src="/images/icon_needle.png" ></a> (複選)<asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="tbVac"   ID="RequiredFieldValidator4"  runat="server"  ErrorMessage="*必填"></asp:RequiredFieldValidator> 
         </td>
        </tr>
        <tr>
          <th scope="row"><span class="must">*</span>訪查原因：</th>
          <td colspan="5">

            <asp:DropDownList ID="ddlVisitReason"  AutoPostBack="true"    runat="server" 
                  onselectedindexchanged="ddlVisitReason_SelectedIndexChanged"></asp:DropDownList>
              <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="ddlVisitReason"   ID="rfvddlVisitReason" InitialValue ="" runat="server"  ErrorMessage="*必填"></asp:RequiredFieldValidator> 
         
            </td>
        </tr>
        <tr>
          <th scope="row"><span class="must">*</span>訪查結果摘要：</th>
          <td colspan="5">
          
           <asp:DropDownList ID="ddlVisitResult"  AutoPostBack="true"   runat="server" 
                  onselectedindexchanged="ddlVisitResult_SelectedIndexChanged"   ></asp:DropDownList>
              <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="ddlVisitResult"   ID="rfvddlVisitResult" InitialValue ="" runat="server"  ErrorMessage="*必填"></asp:RequiredFieldValidator> 
          
            
            </td>
        </tr>
            <tr id="tr_Country" runat ="server" visible ="false"  >
          <th scope="row"><span class="must">*</span>國家：</th>
          <td colspan="5">
          
           <asp:DropDownList ID="ddlCountry" CssClass="Country" runat="server"  ></asp:DropDownList>
              <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="ddlCountry"   ID="rfvCountry" InitialValue ="" runat="server"  ErrorMessage="*必填"></asp:RequiredFieldValidator> 
          
            
            </td>
        </tr>
     
              <tr  id="tr_PreBackDate"  visible ="false"  runat ="server"  >
          <th scope="row"><span class="must">*</span>預定回國日期：</th>
          <td colspan="5"> 
         <asp:TextBox ID="tbPreBackDate" CssClass="PreBackDate" runat="server"></asp:TextBox>
          <a href="javascript:void(0);"><asp:Image ID="PreBackDateImg" ImageUrl="../images/icon_calendar.png" runat="server"></asp:Image></a>
           <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="tbPreBackDate"   ID="rfvPreBackDate" runat="server"  ErrorMessage="*必填"></asp:RequiredFieldValidator> 
          
        
        
           </td>
        </tr>

         <tr  id="tr_NotNeedOrReject"   visible ="false"    runat ="server"    >
          <th scope="row"><span class="must">*</span><asp:Literal ID="ltThTitle" runat="server"></asp:Literal>：</th>
          <td colspan="5"> 
         <div ID="phNotNeedOrReject" runat="server"></div>
        
        
           </td>
        </tr>
       

        <tr>
          <th scope="row">訪查紀錄：</th>
          <td colspan="5"> 
          <asp:TextBox ID="tbVisitComment" runat="server" TextMode="MultiLine" Columns ="70" Rows ="5"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <th scope="row">相關附件：</th>
          <td colspan="5">
          <asp:DropDownList ID="ddlVisitFileType" runat="server">
          <asp:ListItem Value="1" Text="拒絕接種證明"></asp:ListItem>
          <asp:ListItem Value="2" Text="國外接種證明"></asp:ListItem>
          <asp:ListItem Value="3" Text="病歷資料"></asp:ListItem>
          <asp:ListItem Value="4" Text="B型肝炎檢驗資料"></asp:ListItem>
          </asp:DropDownList> 
           <asp:FileUpload ID="fu_Visit" runat="server"></asp:FileUpload>
           <asp:RegularExpressionValidator ID="revfu_Visit" ValidationExpression="(.*?)\.(pdf)$"
    ControlToValidate="fu_Visit" runat="server" ForeColor="Red" ErrorMessage="*限PDF"
    Display="Dynamic" />
           (檔案格式限PDF，單一檔案大小限3M)

           <br/>
           <asp:Literal ID="ltFiles" runat="server"></asp:Literal>
           <br/>
          </td>
        </tr>
      </table>
      </ContentTemplate>
      </asp:UpdatePanel>
    </div>

<div class="list01" runat ="server" id="CreateModifyInfo" visible ="false" >
<ul>
<li><span>建立者:</span><asp:Literal ID="ltCreateInfo" runat="server"></asp:Literal></li>
<li><span>異動者:</span><asp:Literal ID="ltModifyInfo" runat="server"></asp:Literal></li>
</ul>
</div>

      <div class="formBtn formBtncenter"> 
      <asp:Button ID="btnSave" runat="server" Text="儲存" CssClass="btn btnSave" 
         Visible ="false"   onclick="btnSave_Click"></asp:Button> 

      <input type="button" name="btnBack" id="btnBack" value="取消" class="btn" /> 
 
  <asp:Button ID="btnDel" runat="server" CssClass="btn " CausesValidation="false" Visible ="false"   Text="刪除" OnClientClick="return confirm('確定刪除?');" onclick="btnDel_Click"></asp:Button>
      </div>
  </form> 
  <!-- form end-->  
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server"> 
    <script>
        var HFID = '<%=hdVac.ClientID %>';
        var VCID = '<%=tbVac.ClientID %>';  
    </script>
   
   
    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js" type="text/javascript"></script>
     <script src="../js/other/tab.js" type="text/javascript"></script> 
    <script src="../js/other/commUtil.js" type="text/javascript"></script>
    <script src="VisitCaseContent.js" type="text/javascript"></script>
    
</asp:Content>

