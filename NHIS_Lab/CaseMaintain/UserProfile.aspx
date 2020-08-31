<%@ Page Title="" Language="C#" enableEventValidation="false"  MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="CaseMantain_UserProfile" %>
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
 <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
  <!-- form start-->
  <form id="form1" runat="server">
    <div class="formBtn formBtnleft">
     
      <input type="button" name="BackToList"  id="BackToList" value="回查詢結果" class="btn" />
    </div>
    <div class="tab">
      <ul>
        <li class="here"><a href="#" id="a">基本資料</a>
          <ul>
            <li> 
              <!--內容 start -->
              <div class="formTb formTb3">
                <table>
                  <tr>
                    <td width="33%"><table>
                        <tr>
                          <th scope="row"><span class="must">*</span>出生日期</th>
                          <td> 
                          <asp:TextBox ID="BirthDate" runat="server" ></asp:TextBox>
                          <a href="#"><asp:Image ID="BirthDateImg" ImageUrl="../images/icon_calendar.png" runat="server"></asp:Image></a>
                             <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="BirthDate"   ID="rfvBirthDate" runat="server"  ErrorMessage="*必填"></asp:RequiredFieldValidator> 
                          

                          </td>
                        </tr>
                        <tr>
                          <th scope="row">身分證字號</th>  
                         <td> <asp:TextBox ID="tbIdNo" runat="server" CssClass="text01"></asp:TextBox>
                         <div id="CaseIDdiv" runat ="server" class="CaseID" style ="display:none"></div>
                         </td>
                        </tr>
                        <tr>
                          <th scope="row">居留證號</th>
                         <td> <asp:TextBox ID="tbResNo" runat="server" CssClass="text01"></asp:TextBox></td>
                        </tr>
                        <tr>
                          <th scope="row">護照號碼</th>
                         <td> <asp:TextBox ID="tbPassportNo" runat="server" CssClass="text01"></asp:TextBox></td>
                        </tr>
                        <tr>
                          <th scope="row">其他證號</th>
                       <td> <asp:TextBox ID="tbOtherNo" runat="server" CssClass="text01"></asp:TextBox></td>
                        </tr>
                        <tr>
                          <th scope="row">出入境狀況</th>
                           <td> <asp:TextBox ID="tbImmiType" runat="server" Enabled ="false" CssClass="text01"  ></asp:TextBox></td>
 
                        </tr>
                      </table></td>
                    <td width="33%"><table>
                        <tr>
                          <th scope="row">姓名</th>
                          <td> <asp:TextBox ID="tbName" runat="server" CssClass="text01"></asp:TextBox></td>
                     
                        </tr>
                        <tr>
                          <th scope="row">英文姓名</th>
                      <td> <asp:TextBox ID="tbEngName" runat="server" CssClass="text01"></asp:TextBox></td>
                     
                        </tr>
                        <tr>
                          <th scope="row"><span class="must">*</span>性別</th>
                          <td> 
                             <asp:DropDownList ID="ddlGender" runat="server">
                             <asp:ListItem Value="0" Text ="請選擇"></asp:ListItem>
                             <asp:ListItem Value="1" Text ="男"></asp:ListItem>
                             <asp:ListItem Value="2" Text ="女"></asp:ListItem>
                             </asp:DropDownList>
                             <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="ddlGender"   ID="rfvGender" runat="server" InitialValue ="0" ErrorMessage="*必填"></asp:RequiredFieldValidator> 
                            </td>
                        </tr>
                        <tr>
                          <th scope="row">戶號</th>
                      <td> <asp:TextBox ID="tbHouseNo" runat="server" CssClass="text01"></asp:TextBox></td>
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
                            <asp:DropDownList ID="ddlONationality" runat="server"></asp:DropDownList>
                            
                            </td>
                        </tr>
                      </table></td>
                    <td width="33%"><table>
                        <tr>
                          <th scope="row">主要聯絡人</th>
                          <td>
                          
                          <select id="MainContact">
                            <option value="0">本人</option>
                            <option ng-repeat="record in TM2.data track by $index" value ="{{record['CC']}}"  >{{record['RS']}}</option>  
                            </select>
                            
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
          <td  colspan="5">
          <asp:TextBox ID="tbTelDayArea" CssClass ="tbTelDayArea"   runat="server" Width="50px"></asp:TextBox>
         <asp:TextBox ID="tbTelDayNo"  CssClass ="tbTelDayNo" runat="server"></asp:TextBox>
            分機
            <asp:TextBox ID="tbTelDayExt"  CssClass ="tbTelDayExt" runat="server"></asp:TextBox>
            
            </td>
        </tr>
        <tr>
          <th scope="row">電話(夜)：</th>
          <td  colspan="5">
          <asp:TextBox ID="tbTelNightArea" CssClass ="tbTelNightArea" runat="server" Width="50px"></asp:TextBox>
         <asp:TextBox ID="tbTelNightNo" CssClass ="tbTelNightNo" runat="server"></asp:TextBox>
            分機
            <asp:TextBox ID="tbTelNightExt" CssClass ="tbTelNightExt" runat="server"></asp:TextBox>
            
            </td>
        </tr>
        <tr>
          <th scope="row">行動電話：</th>
          <td  colspan="5"> 
            <div id="MobileDIV" class="MobileDIV"  runat="server"  > </div> 
            <div id="MobileSample" style="display:none">
            <input name="tbMobileNo" type="text" class="text02 tbMobile" /><a onclick ="javascript:void(0);" class="DelMobile"><img src="/images/icon_del.png" /></a><a onclick ="javascript:void(0);" class="AddMobile"><img src="/images/icon_increase.png" /></a>
             </div>
          </td>
        </tr>
        <tr>
          <th scope="row">電子郵件：</th>
          <td  colspan="5">
           <div id="EmailDIV" class="EmailDIV"  runat="server"  > </div> 
            <div id="EmailSample" style="display:none">
            <input name="tbEmail" type="text" class="text02 tbEmail" /><a onclick ="javascript:void(0);" class="DelEmail"><img src="/images/icon_del.png" /></a><a onclick ="javascript:void(0);" class="AddEmail"><img src="/images/icon_increase.png" /></a>
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
                           <asp:CheckBoxList ID="cblLang"  RepeatDirection="Horizontal" runat="server"  RepeatLayout="Flow"></asp:CheckBoxList>
                               
                               <asp:TextBox ID="TextBox1" runat="server"  Width ="40"   ></asp:TextBox>
                            
                            </td>
                        </tr>
                      </table></td>
                  </tr>
                  <tr>
                    <td colspan="3"><table>
                        <tr> 
                          <th  scope="row">身分別</th>
                          <td>     <asp:CheckBoxList ID="cblCapacity"  RepeatDirection="Horizontal" runat="server"  RepeatLayout="Flow"></asp:CheckBoxList>
                        
                               <asp:TextBox ID="TextBox2" runat="server"   Width ="40"  ></asp:TextBox>
                             </td>
                        </tr>
                      </table></td>
                  </tr>
                  <tr>
                    <td colspan="3"><table>
                        <tr>
                          <th scope="row">戶籍地址</th>
                          <td>

                          <asp:DropDownList ID="ddlResCounty" runat="server"></asp:DropDownList>
                             <asp:DropDownList ID="ddlResTown" runat="server"></asp:DropDownList> 
                            <asp:DropDownList ID="ddlResVillage" runat="server"></asp:DropDownList> 
                          
                            <asp:TextBox ID="tbResAddr" CssClass ="text02"    runat="server"></asp:TextBox>
                            
                            </td>
                        </tr>
                        <tr>
                          <th scope="row">通訊地址</th>
                          <td> 
                 
                          <asp:DropDownList ID="ddlConCounty" runat="server"></asp:DropDownList>
                             <asp:DropDownList ID="ddlConTown" runat="server"></asp:DropDownList> 
                            <asp:DropDownList ID="ddlConVillage"   runat="server">
                           
                            </asp:DropDownList> 
                          
                          <asp:TextBox ID="tbConAddr" CssClass ="text02"   runat="server"></asp:TextBox>
                            
                            
                            </td>
                        </tr>
                      </table></td>
                  </tr>
                  <tr>
                    <td width="33%"><table>
                        <tr>
                          <th scope="row">懷孕週數</th>
                          <td> <asp:TextBox ID="tbPregWeek" runat="server" CssClass="text01"></asp:TextBox></td>
                     
                        </tr>
                        <tr>
                          <th scope="row">胎次</th>
                          <td> <asp:TextBox ID="tbBirthNum" runat="server" CssClass="text01"></asp:TextBox></td>
                     
                        </tr>
                        <tr>
                          <th scope="row">胎別</th>
                      <td> <asp:TextBox ID="tbBirthMulti" runat="server" CssClass="text01"></asp:TextBox></td>
                     
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
                            <td> <asp:TextBox ID="tbBirthWeight" runat="server" CssClass="text01"></asp:TextBox></td>
    
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
                          <th scope="row"><span class="must">*</span>接生單位</th>
                        <td> <asp:TextBox ID="tbDeliverOrg" runat="server" CssClass="text03"></asp:TextBox>
                          <asp:RequiredFieldValidator  ForeColor ="Red"  Font-Size="Medium" ControlToValidate ="tbDeliverOrg"   ID="rfvtbDeliverOrg" runat="server"   ErrorMessage="*必填"></asp:RequiredFieldValidator> 
                          
                        
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
                              <td> <asp:TextBox ID="tbElemSchool" ToolTip="最新一筆國小資料" runat="server" CssClass="text01"></asp:TextBox></td>
                      
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
                           <div id="CommentSample" style="display:none">
                          <select  name="ddlCommentKindddlCommentKind">
                              <option value="1" selected>個案姓名、生日、戶籍地址等備註：</option>
                              <option value="2" >聯絡人資料備註：</option>
                              <option value="3" >身分備註：(上傳附件)</option>
                              <option value="4" >父母新資料備註：</option>
                              <option value="5" >死亡資料備註：</option>
                              <option value="6" >其他：</option>
                            </select><input name="tbComment"  type="text" class="text02" /><a onclick ="javascript:void(0);" class="DelPS"><img src="/images/icon_del.png" /></a><a onclick ="javascript:void(0);" class="AddPS"><img src="/images/icon_increase.png" /></a>
                           </div>
                           
                           </td>
                       
                           

                         </tr>
                      </table></td>
                  </tr>
                  <tr id="ContractTR" runat ="server" >
                    <td colspan="3">
                  
                    <asp:Button ID="btnAddContract" runat="server"   CssClass="btn AddContract" Text="新增連絡人"></asp:Button>
                   <input type="button" ng-click="changePage2()" style="display:none" value="更新列表" id="UpdateContactList" class="btn"  >
                   
                     <div  id="tmBlock2" class="formTb4"  ng-cloak >
                        <table>
                          <tr>
                            <th scope="col">關係</th>
                            <th scope="col">姓名</th>
                            <th scope="col">身分證號</th>
                            <th scope="col">出生日期</th>
                            <th scope="col">主要聯絡人</th>
                            <th scope="col">維護</th>
                            <th scope="col">移除</th>
                          </tr>
                        
                             <tr ng-repeat="record in TM2.data track by $index">
                    <td class="aCenter" ng-bind='record["RS"]' > </td>
                   <td class="aCenter" ng-bind='record["N"]'></td>
                   <td class="aCenter" ng-bind='record["I"]'></td> 
                 <td class="aCenter" ng-bind='record["BD"]'></td> 
                            <td class="aCenter">
                            <div ng-if='record["M"] == 1'><img src="/images/icon_tick.png" ></div>
                            </td>
                            <td class="aCenter" ng-click="goDetail2(record)"><a onclick ="javascript:void(0);"><img src="/images/icon_maintain.png"></a></td>
                            <td class="aCenter"><a href="#"><img src="/images/icon_del01.gif"></a></td>
                          </tr>
                        </table>
                      </div>
                     </td>
                  </tr>
                </table>
              </div>


              <div class="formBtn formBtncenter">

      <asp:Button ID="Button1" runat="server" Text="儲存" CssClass="btn" 
            onclick="Button1_Click"></asp:Button>
            </div>
              <!--內容 end --> 
            </li>
          </ul>
        </li>
        <li id="ModifyLogTab" style="display:none"><a href="#">異動記錄</a>
          <ul>
            <li> 
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
                    <th scope="col" width="15%">異動單位</th>
                    <th scope="col">異動人員</th>
                    <th scope="col">異動欄位</th>
                    <th scope="col">異動前資料</th>
                    <th scope="col">異動後資料</th>  
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' > </td>
                   <td class="aCenter" ng-bind='record["D"]'></td>
                   <td class="aCenter" ng-bind='record["O"]'></td> 
                   <td class="aCenter" ng-bind='record["U"]'></td>
                    <td class="aCenter" ng-bind='record["C"]'></td>
                   <td class="aCenter" ng-bind='record["B"]'></td>
                   <td class="aCenter" ng-bind='record["A"]'></td>
                </tr>
            </table>
        </div>
              <div class="listTb">
              
              </div>
              <!--表格 end --> 
              <!--條列list01 start -->
              <div class="remark wordred">P.S: 此處僅列出於NIIS系統中進行修改的紀錄</div>
<div class="list01">
<ul>
<li><span>建立者：</span>李小明-衛生福利部疾病管制署-2015/5/19 14:23:45</li>
<li><span>建立者：</span>李小明-衛生福利部疾病管制署-2015/5/19 14:23:45</li>
</ul>
</div>
<!--條列list01 end -->
              <!--內容 end --> 
            </li>
          </ul>
        </li>
      </ul>
    </div>
  </form>
  <!-- form end-->  
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

     </script>
   
   
    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>
     <script src="../js/other/tab.js" type="text/javascript"></script>
  
    <script src="UserProfile.js" type="text/javascript"></script>
    
</asp:Content>

