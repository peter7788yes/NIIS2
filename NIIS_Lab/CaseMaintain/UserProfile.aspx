<%@ Page Title="" Language="C#" enableEventValidation="false"  MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="CaseMantain_UserProfile" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register src="ucCaseRemark.ascx" tagname="ucCaseRemark" tagprefix="uc1" %>
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

  <form id="form1" runat="server">
 
 <section class="Content" ng-app="MyApp" ng-controller="MyController" id="MyController" ng-cloak>
  
    
       <div class="path" style="content: none;">
  <span class="pathReplaceA">個案管理</span><span class="pathReplaceA">個案資料維護</span>
  個案基本資料<img id="loading" src="/images/loading001.gif" style="display:none;margin-left:5px;">
</div>
  <!-- form start-->

    <div class="formBtn formBtnleft">
     

    </div>
    <div id="TabDIV" runat="server" class="tab">
      <ul>
        <li id="TabUserContent" class="here"><a href="javascript:void(0);" id="UserContentLink">基本資料</a>
          <ul>
            <li>   
            </li>
          </ul>
        </li>
        <li id="TabModifyLog" runat ="server" visible="false" ><a href="javascript:void(0);" id="ModifyLogLink">異動記錄</a>
          <ul>
            <li> 
             </li>
          </ul>
        </li>
      </ul>
    </div>
     <div id="DivUserContent">
               <div class="formTb btnblock2 button_floatleft" id="DivAgeTip" runat="server" visible ="false" > <asp:Literal ID="ltAgeTip" runat="server"></asp:Literal></div>
 
              <!--內容 start -->
              <div class="formTb formTb3">
            

                <table>
                  <tr>
                    <td width="33%"><table>
                        <tr>
                          <th scope="row"><span class="must">*</span>出生日期</th>
                          <td> 
                          <asp:TextBox ID="BirthDate" runat="server" ></asp:TextBox>
                          <a href="javascript:void(0);"><asp:Image ID="BirthDateImg" ImageUrl="../images/icon_calendar.png" runat="server"></asp:Image></a>
                             <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="BirthDate"   ID="rfvBirthDate" runat="server"  ErrorMessage="*必填"></asp:RequiredFieldValidator> 
                          

                          </td>
                        </tr>
                        <tr>
                          <th scope="row">身分證字號</th>  
                         <td> <asp:TextBox ID="tbIdNo"  MaxLength="10" runat="server" CssClass="text03 tbIdNo"></asp:TextBox>
                          <asp:CustomValidator ID="cvIdNo"  clientvalidationfunction="checkID" ControlToValidate="tbIdNo" runat="server" Display="Dynamic" ForeColor="Red"  ErrorMessage="*錯誤"></asp:CustomValidator>
   
                         <div id="CaseIDdiv" runat ="server" class="CaseID" style ="display:none"></div>
                         </td>
                        </tr>
                        <tr>
                          <th scope="row">居留證號</th>
                         <td> <asp:TextBox ID="tbResNo" MaxLength="10" runat="server" CssClass="text03"></asp:TextBox>
                           <asp:CustomValidator ID="cvResNo"  clientvalidationfunction="checkRes" ControlToValidate="tbResNo" runat="server" Display="Dynamic" ForeColor="Red"  ErrorMessage="*錯誤"></asp:CustomValidator>
   
                         
                         </td>
                        </tr>
                        <tr>
                          <th scope="row">護照號碼</th>
                         <td> <asp:TextBox ID="tbPassportNo" MaxLength="30" runat="server" CssClass="text03"></asp:TextBox></td>
                        </tr>
                        <tr>
                          <th scope="row">其他證號</th>
                       <td> <asp:TextBox ID="tbOtherNo" runat="server" CssClass="text03"></asp:TextBox></td>
                        </tr>
                        <tr>
                          <th scope="row">出入境狀況</th>
                           <td> <asp:TextBox ID="tbImmiType" runat="server" Enabled ="false" CssClass="text01"  ></asp:TextBox></td>
 
                        </tr>
                      </table></td>
                    <td width="33%"><table>
                        <tr>
                          <th scope="row"><span class="must">*</span>姓名</th>
                          <td> <asp:TextBox ID="tbName" runat="server" CssClass="text03"></asp:TextBox>
                              <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="tbName"   ID="rfvtbName" runat="server"  ErrorMessage="*必填"></asp:RequiredFieldValidator> 
                          
                          
                          </td>
                     
                        </tr>
                        <tr>
                          <th scope="row">英文姓名</th>
                      <td> <asp:TextBox ID="tbEngName" runat="server" CssClass="text01"></asp:TextBox>
                      
                      <asp:RegularExpressionValidator ID="revEngName" ForeColor ="Red" Display="Dynamic"  Font-Size="Medium" ValidationExpression="^[a-zA-Z_\s_\.]*$" ControlToValidate="tbEngName"  runat="server" ErrorMessage="*英文"></asp:RegularExpressionValidator>
                      </td>
                     
                        </tr>
                        <tr>
                          <th scope="row">性別</th>
                          <td> 
                             <asp:DropDownList ID="ddlGender" runat="server">
                             <asp:ListItem Value="0" Text ="請選擇"></asp:ListItem> 
                             </asp:DropDownList>
                             </td>
                        </tr>
                        <tr>
                          <th scope="row">戶號</th>
                      <td> <asp:TextBox ID="tbHouseNo" MaxLength="10" runat="server" CssClass="text03"></asp:TextBox></td>
                        </tr>
                        <tr>
                          <th scope="row">所屬轄區</th>
                          <td>
                          <asp:TextBox ID="tbArea" Enabled ="false"  CssClass="text01"  runat="server"></asp:TextBox>
                           
                          </td>
                        </tr>
                        <tr>
                          <th scope="row">原屬國籍</th>
                          <td> 
                            <asp:DropDownList ID="ddlONationality" runat="server">  <asp:ListItem Value="0" Text ="請選擇"></asp:ListItem></asp:DropDownList>
                            
                            </td>
                        </tr>
                      </table></td>
                    <td width="33%"><table>
                        <tr>
                          <th scope="row"><span class="must">*</span>主要聯絡人</th>
                          <td>  
                            <asp:DropDownList ID="ddlMainContact" runat="server"  >
                             
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvMainContact" InitialValue ="0" runat="server" ErrorMessage="*必填" ForeColor ="red"   ControlToValidate ="ddlMainContact"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            <div id="MainContactInfo"></div>  
                            
                            </td>
                        </tr>
                      </table></td>
                  </tr>
                  <tr> <td colspan="3">
                  <div id="CaseContactInfo">
                   <table  >
                        <tr>
                          <th scope="row">聯絡資料</th>
                          <td>
                         
                          <table>
                              <tr>
          <th scope="row">電話(日)：</th>
          <td >
          <asp:TextBox ID="tbTelDayArea" CssClass ="tbTelDayArea"   runat="server" Width="30px"></asp:TextBox>
            <asp:RangeValidator ID="rvTelDayArea" runat="server" ErrorMessage="*數字"  ControlToValidate ="tbTelDayArea"   ForeColor ="Red"  Font-Size="Medium"  MinimumValue="0" MaximumValue="999" Display ="Dynamic" Type="Integer"></asp:RangeValidator>
                      
         <asp:TextBox ID="tbTelDayNo"  CssClass ="tbTelDayNo" runat="server" Width="100px"></asp:TextBox>
            <asp:RangeValidator ID="rvTelDayNo" runat="server" ErrorMessage="*數字"  ControlToValidate ="tbTelDayNo"   ForeColor ="Red"  Font-Size="Medium"  MinimumValue="0" MaximumValue="999999999" Display ="Dynamic" Type="Integer"></asp:RangeValidator>
         
            分機
            <asp:TextBox ID="tbTelDayExt"  CssClass ="tbTelDayExt" runat="server" Width="50px"></asp:TextBox>
              <asp:RangeValidator ID="rvTelDayExt" runat="server" ErrorMessage="*數字"  ControlToValidate ="tbTelDayExt"   ForeColor ="Red"  Font-Size="Medium"  MinimumValue="0" MaximumValue="999999999" Display ="Dynamic" Type="Integer"></asp:RangeValidator>
         
            </td> 
          <th scope="row">電話(夜)：</th>
          <td  >
          <asp:TextBox ID="tbTelNightArea" CssClass ="tbTelNightArea" runat="server" Width="30px"></asp:TextBox>
         <asp:TextBox ID="tbTelNightNo" CssClass ="tbTelNightNo" runat="server" Width="100px"></asp:TextBox>
            分機
            <asp:TextBox ID="tbTelNightExt" CssClass ="tbTelNightExt" runat="server" Width="50px"></asp:TextBox>
            
            </td>
        </tr>
        <tr>
          <th scope="row">行動電話：</th>
          <td  > 
            <div id="MobileDIV" class="MobileDIV"  runat="server"  > </div> 
            <div id="MobileSample" style="display:none">
            <input name="tbMobileNo" type="text" class="text03 tbMobile" /><a onclick ="javascript:void(0);" class="DelMobile"><img src="/images/icon_del.png" /></a><a onclick ="javascript:void(0);" class="AddMobile"><img src="/images/icon_increase.png" /></a>
             </div>
          </td> 
          <th scope="row">電子郵件：</th>
          <td  >
           <div id="EmailDIV" class="EmailDIV"  runat="server"  > </div> 
            <div id="EmailSample" style="display:none">
            <input name="tbEmail" type="text" class="text03 tbEmail" /><a onclick ="javascript:void(0);" class="DelEmail"><img src="/images/icon_del.png" /></a><a onclick ="javascript:void(0);" class="AddEmail"><img src="/images/icon_increase.png" /></a>
             </div>
          </td>
        </tr>
      </table>





                          </td>
                          </tr>
                      </table>
                      </div>
                  </td>
                  </tr>

                  <tr>
                    <td colspan="3"><table>
                        <tr>
                          <th scope="row">語言</th>
                          <td>
                           <asp:CheckBoxList ID="cblLang"    RepeatDirection="Horizontal"  runat="server"  RepeatLayout="Flow"></asp:CheckBoxList>
                               
                               <div style="display:inline-block;"><asp:TextBox ID="TextBox1" runat="server"  Width ="40"   ></asp:TextBox></div>
                            
                            </td>
                        </tr>
                      </table></td>
                  </tr>
                  <tr>
                    <td colspan="3"><table>
                        <tr> 
                          <th  scope="row">身分別<img src="/images/icon_record.png" alt="身分別歷史" id="imgCapacity" /></th>
                          <td>    
                         <table  >
                         <tr id="tr_cblCapacity_1"  visible ="false" runat ="server"  ><td width="150px">區域相關</td><td><asp:CheckBoxList ID="cblCapacity_1"  RepeatDirection="Horizontal" runat="server"  RepeatLayout="Flow"></asp:CheckBoxList></td></tr>
                            <tr  id="tr_cblCapacity_2"  visible ="false" runat ="server"><td>疫苗特定實施對象</td><td><asp:CheckBoxList ID="cblCapacity_2"  RepeatDirection="Horizontal" runat="server"  RepeatLayout="Flow"></asp:CheckBoxList></td></tr>
                      <tr  id="tr_cblCapacity_3"  visible ="false"  runat ="server"><td>社經相關</td><td><asp:CheckBoxList ID="cblCapacity_3"  RepeatDirection="Horizontal" runat="server"  RepeatLayout="Flow"></asp:CheckBoxList></td></tr>
                     <tr  id="tr_cblCapacity_4" visible ="false"  runat ="server"><td>社政相關</td><td><asp:CheckBoxList ID="cblCapacity_4"  RepeatDirection="Horizontal" runat="server"  RepeatColumns="3" RepeatLayout="Flow"></asp:CheckBoxList></td></tr>
                    
                         </table>
                          
                           <asp:CheckBoxList ID="cblCapacity"  RepeatDirection="Horizontal" runat="server"  RepeatLayout="Flow"></asp:CheckBoxList>
                         

                             </td>
                        </tr>
                      </table></td>
                  </tr>
                  <tr>
                    <td colspan="3"><table>
                        <tr>
                          <th scope="row"><span class="must">*</span>戶籍地址</th>
                          <td>

                          <asp:DropDownList ID="ddlResCounty" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium"  Display ="Dynamic" ControlToValidate ="ddlResCounty"   ID="rfvddlResCounty" runat="server"  InitialValue ="0" ErrorMessage="*必填"></asp:RequiredFieldValidator> 
                          
                             <asp:DropDownList ID="ddlResTown" runat="server"></asp:DropDownList> 
                              <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" Display ="Dynamic" ControlToValidate ="ddlResTown"   ID="rfvddlResTown" runat="server"  InitialValue ="0" ErrorMessage="*必填"></asp:RequiredFieldValidator> 
                          
                            <asp:DropDownList ID="ddlResVillage" runat="server"></asp:DropDownList> 
                             <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium"  Display ="Dynamic" ControlToValidate ="ddlResVillage"   ID="rfvddlResVillage" runat="server"  InitialValue ="0" ErrorMessage="*必填"></asp:RequiredFieldValidator> 
                          
                           <asp:TextBox ID="tbResNei" CssClass ="ResNei"  Width ="30px"  runat="server"></asp:TextBox>
                           <asp:RangeValidator ID="rvResNei" runat="server" ErrorMessage="*數字"  ControlToValidate ="tbResNei"   ForeColor ="Red"  Font-Size="Medium"  MinimumValue="0" MaximumValue="999" Display ="Dynamic" Type="Integer"></asp:RangeValidator>鄰
                       <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" Display ="Dynamic" ControlToValidate ="tbResNei"   ID="rfvtbResNei" runat="server"   ErrorMessage="*必填"></asp:RequiredFieldValidator> 
                            <asp:TextBox ID="tbResAddr" CssClass ="ResAddr text02"    runat="server"></asp:TextBox>
                              <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" Display ="Dynamic" ControlToValidate ="tbResAddr"   ID="rfvtbResAddr" runat="server"   ErrorMessage="*必填"></asp:RequiredFieldValidator> 
                          
                            </td>
                        </tr>
                        <tr>
                          <th scope="row">通訊地址</th>
                          <td> 
                 
                          <asp:DropDownList ID="ddlConCounty" runat="server"></asp:DropDownList>
                             <asp:DropDownList ID="ddlConTown" runat="server"></asp:DropDownList> 
                            <asp:DropDownList ID="ddlConVillage"   runat="server">
                           
                            </asp:DropDownList> 
                           <asp:TextBox ID="tbConNei"  CssClass ="ConNei" Width ="30px"   runat="server"></asp:TextBox><asp:RangeValidator ID="rvConNei" runat="server" ErrorMessage="*數字"  ControlToValidate ="tbConNei"   ForeColor ="Red"  Font-Size="Medium"  MinimumValue="0" MaximumValue="999" Display ="Dynamic" Type="Integer"></asp:RangeValidator>鄰
                       
                          <asp:TextBox ID="tbConAddr" CssClass ="ConAddr text02"   runat="server"></asp:TextBox>
                            
                            <input id="btnSameRes" type="button"  value="同戶籍" />
                            </td>
                        </tr>
                      </table></td>
                  </tr>
                  <tr>
                    <td width="33%"><table>
                        <tr>
                          <th scope="row">懷孕週數</th>
                          <td> <asp:TextBox ID="tbPregWeek" runat="server" CssClass="text03"></asp:TextBox>
                          
                          <asp:RangeValidator ID="rvPregWeek" runat="server" ErrorMessage="*數字"  ControlToValidate ="tbPregWeek"   ForeColor ="Red"  Font-Size="Medium"  MinimumValue="0" MaximumValue="100" Display ="Dynamic" Type="Integer"></asp:RangeValidator>
                      
                          </td>
                     
                        </tr>
                        <tr>
                          <th scope="row">胎次</th>
                          <td> <asp:TextBox ID="tbBirthNum" runat="server" CssClass="text03"></asp:TextBox>
                              <asp:RangeValidator ID="rvBirthNum" runat="server" ErrorMessage="*數字"  ControlToValidate ="tbBirthNum"   ForeColor ="Red"  Font-Size="Medium"  MinimumValue="0" MaximumValue="100" Display ="Dynamic" Type="Integer"></asp:RangeValidator>
                      
                          </td>
                     
                        </tr>
                        <tr>
                          <th scope="row">胎別</th>
                      <td>  
                        <asp:DropDownList ID="ddlBirthMulti" runat="server">   
                        <asp:ListItem Value="0" Text ="請選擇"></asp:ListItem>
                             </asp:DropDownList>
                      
                      </td>
                     
                        </tr>
                        <tr>
                          <th scope="row"><span class="must">*</span>同胎次序</th>
                          <td> <asp:TextBox ID="tbBirthSeq" runat="server" CssClass="text03"></asp:TextBox>
                            <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="tbBirthSeq"   ID="rfvtbBirthSeq" runat="server"   ErrorMessage="*必填" Display ="Dynamic"></asp:RequiredFieldValidator> 
                         <asp:RangeValidator ID="rvBirthSeq" runat="server" ErrorMessage="*數字"  ControlToValidate ="tbBirthSeq"   ForeColor ="Red"  Font-Size="Medium"  MinimumValue="0" MaximumValue="100" Display ="Dynamic" Type="Integer"></asp:RangeValidator>
                          </td>
                   
                        </tr>
                      </table></td>
                    <td width="33%"><table>
                        <tr>
                          <th scope="row">出生體重 </th>
                            <td> <asp:TextBox ID="tbBirthWeight" runat="server" CssClass="text03"></asp:TextBox>
                               <asp:RangeValidator ID="rvBirthWeight" runat="server" ErrorMessage="*數字"  ControlToValidate ="tbBirthWeight"   ForeColor ="Red"  Font-Size="Medium"  MinimumValue="0" MaximumValue="99999" Display ="Dynamic" Type="Integer"></asp:RangeValidator>
                      
                            </td>
    
                        </tr>
                        <tr>
                          <th scope="row">出生場所</th>
                          <td> 
                            
                              <asp:DropDownList ID="ddlBirthPlace" runat="server">
                             <asp:ListItem Value ="0" Text="請選擇"></asp:ListItem>
                             <asp:ListItem  Value ="1" Text="醫院"></asp:ListItem>
                             <asp:ListItem  Value ="2" Text="診所"></asp:ListItem>
                              <asp:ListItem  Value ="3" Text="其他"></asp:ListItem>
                             </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                          <th scope="row">接生者</th>
                          <td>
                          
                             <asp:DropDownList ID="ddlDeliver" runat="server">
                             <asp:ListItem Value ="0" Text="請選擇"></asp:ListItem>
                             <asp:ListItem  Value ="1" Text="醫生"></asp:ListItem>
                             <asp:ListItem  Value ="2" Text="助產師"></asp:ListItem>
                              <asp:ListItem  Value ="3" Text="其他"></asp:ListItem>
                             </asp:DropDownList>
                          </td>
                        </tr>
                        <tr>
                          <th scope="row">接生單位</th>
                        <td> <asp:TextBox ID="tbDeliverOrg" runat="server" CssClass="text03"></asp:TextBox>
                         
                        
                        </td>
    
                        </tr>
                      </table></td>
                    <td width="33%"><table>
                        <tr>
                          <th scope="row">死亡原因</th>
                          <td>
                          <asp:TextBox ID="tbDeathReason" runat="server" Enabled ="false" ></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <th scope="row">死亡日期</th>
                          <td>
                          <asp:TextBox ID="tbDeathDate" runat="server"  Enabled ="false"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <th scope="row">死亡註記</th>
                          <td><asp:TextBox ID="tbDeathMark" runat="server"  Enabled ="false"></asp:TextBox>
                          </td>
                        </tr>
                      </table></td>
                  </tr>
                  <tr>
                    <td colspan="3"><table>
                        <tr>
                          <th scope="row">婚姻</th>
                          <td>
                          <asp:DropDownList ID="ddlMarryStatus" runat="server">
                          <asp:ListItem Value ="0" Text ="請選擇"></asp:ListItem>
                            <asp:ListItem Value ="1" Text ="未婚"></asp:ListItem>
                          <asp:ListItem Value ="2" Text ="已婚"></asp:ListItem>
                            <asp:ListItem Value ="3" Text ="離婚"></asp:ListItem>
                          </asp:DropDownList> 
                            
                            
                            </td>
                          <td scope="row" width="7%" class="aRight">學歷</td>
                             <td> <asp:TextBox ID="tbEduLevel" runat="server" CssClass="text01"></asp:TextBox></td>
                     
                          <td scope="row"  width="10%"  class="aRight">就讀國小</td>
                              <td> <asp:TextBox ID="tbElemSchool"  ToolTip="最新一筆國小資料" runat="server" CssClass="text01"></asp:TextBox></td>
                      
                          <td scope="row"  width="7%"  class="aRight">職業</td>
                            <td> <asp:TextBox ID="tbOccupation"  runat="server" CssClass="text01"></asp:TextBox></td>
                      
                        </tr>
                      </table></td>
                  </tr>
                  <tr>
                    <td colspan="3"><table>
                        <tr>
                          <th scope="row">備註</th>
                          <td>
                          <div id="CommentAreaDIV" class="CommentAreaDIV"  runat="server"  > 
                         
                           </div>
                           <div id="CommentSample" style="display:none"><asp:Literal ID="ltDDLCommentKind" runat="server"></asp:Literal><input name="tbComment"  type="text" class="text02" /><a onclick ="javascript:void(0);" class="DelPS"><img src="/images/icon_del.png" /></a><a onclick ="javascript:void(0);" class="AddPS"><img src="/images/icon_increase.png" /></a>
                           </div>
                           
                            <uc1:ucCaseRemark ID="ucCaseRemark1" runat="server" />
                           


                           </td>
                       
                           

                         </tr>
                      </table></td>
                  </tr>
                  <tr id="ContractTR" runat ="server" >
                    <td colspan="3">
                  <table>
                        <tr>
                          <th scope="row"><span class="must">*</span>聯絡人</th>
                          <td>
                    <asp:Button ID="btnAddContract" runat="server"   CssClass="btn AddContract" Text="新增"></asp:Button>
                    <input type="hidden" name="NewCaseUserContactIDs" id="NewCaseUserContactIDs" value="" />
                    <div id="CaseContactList"  class="formTb4"></div>
                  
                   
                     
                      </td>
                       
                           

                         </tr>
                      </table>
                     </td>
                  </tr>
                </table>
              </div>


              <div class="formBtn formBtncenter">
              <asp:Button ID="btnCheck" runat="server" OnClientClick="return CheckSubmit();" Text="儲存" CssClass="btn" 
                      onclick="btnCheck_Click" Visible ="false"  ></asp:Button>
                      <asp:Button ID="btnAdd" runat="server" OnClientClick="return CheckSubmit();" Text="新增" CssClass="btn" 
                      onclick="btnAdd_Click" Visible ="false" ></asp:Button>
      <asp:Button ID="btnSave" runat="server" Text="儲存"  OnClientClick="return CheckSubmit();" CssClass="btn" 
            onclick="btnSave_Click" Visible ="false" ></asp:Button>      <input type="button" name="BackToList"  id="BackToList"   value="回查詢結果" class="btn BackToList" />
            </div>
              <!--內容 end --> 
               </div>
     <div id="DivModifyLog" style="display:none">
              <!--內容 start -->
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
              <!-- page start -->
             
              <!--表格 start -->

               <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col" width="1%">序號</th>
                    <th scope="col" width="15%">異動時間</th>
                     <th scope="col" width="1%"">異動類別</th>
                  
                    <th scope="col">異動欄位</th>
                    <th scope="col">異動前資料</th>
                    <th scope="col">異動後資料</th>  
                      <th scope="col" width="15%">異動單位</th>
                    <th scope="col">異動人員</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' > </td>                   
                   <td class="aCenter" ng-bind='record["D"]'></td>
                      <td class="aCenter" ng-bind='record["K"]'></td>
                
                    <td class="aCenter" ng-bind='record["C"]'></td>
                   <td class="aCenter" ng-bind='record["B"]'></td>
                   <td class="aCenter" ng-bind='record["A"]'></td>   
                   <td class="aCenter" ng-bind='record["O"]'></td> 
                   <td class="aCenter" ng-bind='record["U"]'></td>
                </tr>
            </table>
        </div>
              <div class="listTb">
              
              </div>
              <!--表格 end --> 
              <!--條列list01 start -->
              <div class="remark wordred">P.S: 此處僅列出於NIIS系統中進行異動審該通過的紀錄</div>
<div class="list01">
<ul>
<li><span>建立者：</span><asp:Literal ID="ltCreateInfo" runat="server"></asp:Literal></li>
<li><span>異動者：</span><asp:Literal ID="ltModifyInfo" runat="server"></asp:Literal></li>
</ul>
</div>
     <div class="formBtn formBtncenter">
  <input type="button" name="BackToList2"  id="BackToList2" value="回查詢結果" class="btn BackToList" />
  </div>
<!--條列list01 end -->
              <!--內容 end --> 

                </div>
    
  <!-- form end--> 
  
  </section>  </form>
</asp:Content> 
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server"> 
   
    <script>
         var Countydata=<%=CountyAry%>;
         var Towndata=<%=TownAry%>;
         var Villagedata=<%=VillageAry%>;

         var CountyInival='<%=CountyInival%>';
         var TownInival='<%=TownInival%>';
         var VillageInival='<%=VillageInival%>';


         var ResCountydata=<%=ResCountyAry%>;
         var ResTowndata=<%=ResTownAry%>;
         var ResVillagedata=<%=ResVillageAry%>;

         var ResCountyInival='<%=ResCountyInival%>';
         var ResTownInival='<%=ResTownInival%>';
         var ResVillageInival='<%=ResVillageInival%>';


         var MainContactInival='<%=MainContactInival%>';
         var CaseID ='<%=CaseID.ToString() %>';
     </script>
   
   
    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <!--script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script--> 
        <script src="/js/ang/angular-1.4.8.min.js" type="text/javascript"></script> 
    <script src="/js/ang/PageM.js" type="text/javascript"></script>
    <script src="/js/ang/TableM.js" type="text/javascript"></script>
     <script src="/js/ang/FilterM.js" type="text/javascript"></script> 
    <script src="/js/date/WdatePicker.js"></script>
     <script src="/js/other/tab.js" type="text/javascript"></script>
     <script src="/js/other/commUtil.js" type="text/javascript"></script>
    <script src="UserProfile.js" type="text/javascript"></script>
    
</asp:Content>

