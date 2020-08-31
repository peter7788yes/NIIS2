<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="CaseRemarkContent.aspx.cs" Inherits="CaseMaintain_CaseRemarkContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
<link href="/css/design.css" rel="stylesheet" type="text/css"/>
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
<section class="Content2" >
  <h2>個案備註</h2>
    <form id="form1" runat ="server" >
    <div class="formTb formTb2">
      <table>
       <tr>
          <th>類別：</th>
          <td>
               <asp:DropDownList ID="ddlRemarkType" runat="server" CssClass="ddlRemarkType"></asp:DropDownList>
                <asp:RequiredFieldValidator  ValidationGroup ="dl" ID="RequiredFieldValidator2" ControlToValidate ="ddlRemarkType" runat="server" InitialValue ="" ErrorMessage="*必填" ForeColor ="Red"></asp:RequiredFieldValidator>
              
              </td>   
              
              </tr>
        <tr id="trText" runat ="server" class="cTxt"  >
          <th>內容：</th>
          <td>
              <asp:TextBox ID="tbRemarkContent" runat="server" Height="104px" 
                  TextMode="MultiLine" Width="320px"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup ="t" ControlToValidate ="tbRemarkContent" runat="server" ErrorMessage="*必填" ForeColor ="Red"></asp:RequiredFieldValidator>
              
              </td>   
              </tr>
      
      <tr id="trFile" runat ="server"  class="cFile"   ><th>檔案：</th>
      <td><asp:FileUpload ID="fuRemark" runat="server" />
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3"   ValidationGroup ="f" ControlToValidate ="fuRemark" runat="server" ErrorMessage="*必填" ForeColor ="Red"></asp:RequiredFieldValidator>
            
             <asp:HyperLink ID="linkFiles" runat="server"></asp:HyperLink>
      </td>
        </tr>
      </table>
    </div>

      <div class="formBtn">
          <asp:Button ID="btnAdd" runat="server"  OnClientClick ="return CheckValid();" Text="新增" CssClass ="btn" 
              onclick="btnAdd_Click" />
      <input type="button" name="cancel" id="cancel" value="取消" class="btn" /> 
    </div>
  </form>
  <!-- form end--> 
  </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">

    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    
<script type="text/javascript">
    $(function () {
        $(document).on("change", ".ddlRemarkType", function (e) {

            if ($(this).val() == 3) {
                //FILE upload
                $(".cFile").show();
                $(".cTxt").hide();
            } else {
                $(".cFile").hide();
                $(".cTxt").show();
            }

            e.preventDefault();
            return false;
        });
        $(document).on("click", "#cancel", function (e) { 
           // window.opener.ReloadRemarkList();
            window.close();
            e.preventDefault();
            return false;
        });


    });

    function CheckValid() {
        var isValid = false;

        if ($(".ddlRemarkType").val() == 3) {

            if (Page_ClientValidate("f") && Page_ClientValidate("dl")) {
                isValid = true;
            }
     
        } else {

            if (Page_ClientValidate("t") && Page_ClientValidate("dl")) {
                isValid = true;
        }
     
        }

        if (!isValid) { 
            return false;
        }
        else
            return true;
    }
  
</script>
</asp:Content>

