<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UC_SelectOrgs.aspx.cs" Inherits="UC_SelectOrgs" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <link href="/css/tab.css" rel="stylesheet"/>
    <style>
        a:link {text-decoration: none;color:black;}
        a:visited {text-decoration: none;color:black;}
        a:hover {text-decoration: none;color:black;}
        a:active {text-decoration: none;color:black;}
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
<section class="Content2">
  <h2>選擇組織單位</h2>
    <div class="tab">
      <ul>
        <li class="tabBtn here" data-tab="A"><a href="javascript:void(0);">組織層級</a>
          <ul id="tabA" class="tabs" style="position: absolute;">
            <li> 
              <div id="div1" class="formTb formTb2">
                <table>
                  <tr>
                    <td colspan="4"><input type="button" value="全選" id="selectAllA" class="btn" /></td>
                  </tr>
                  <tr>
                    <%if(MyLevel<=1){ %>
                    <td width="20%">
                        <a href="javascript:void(0);">
                            <input name="cb1" type="checkbox" value="1" id="cb1" class="cbs"  >CDC
                        </a>
                    </td>
                    <%} %>
                    <%if(MyLevel<=2 && OrgArea){ %>
                    <td width="20%">
                        <a href="javascript:void(0);">
                            <input name="cb2" type="checkbox" value="2" id="cb2" class="cbs">區管中心
                        </a>
                    </td>
                    <%} %>
                    <%if(MyLevel<=3){ %>
                    <td width="20%">
                        <a href="javascript:void(0);">
                            <input name="cb3" type="checkbox" value="3" id="cb3" class="cbs">局
                        </a>
                    </td>
                    <%} %>
                    <%if(MyLevel<=4){ %>
                    <td width="20%">
                        <a href="javascript:void(0);">
                            <input name="cb4" type="checkbox" value="4" id="cb4" class="cbs">所
                        </a>
                    </td>
                    <%} %>
                    <%if(MyLevel<=5){ %>
                    <td width="20%">
                        <a href="javascript:void(0);">
                            <input name="cb5" type="checkbox" value="5" id="cb5" class="cbs">院
                        </a>
                    </td>
                    <%} %>
                  </tr>
                </table>
              </div>
              <div class="formBtn">
                <input type="button" id="saveBtn1" value="確認" class="btn" />
                <input type="button" id="cancelBtn1" value="取消" class="btn" />
              </div>
            </li>
          </ul>
        </li>
          <li class="tabBtn" data-tab="B"><a href="javascript:void(0);">組織瀏覽</a>
          <ul  id="tabB" class="tabs" style="position: absolute;">
            <li> 
              <div id="tab2TableDiv" class="formTb formTb2">
                <table>
                  <tr>
                    <td><input type="button" value="全選" id="selectAllB" class="btn"></td>
                  </tr>
                </table>
              </div>
              <section class="tree2">
                <ul id ="ulRoot">
                </ul>
              </section>
              <div class="formBtn">
               <input type="button" id="saveBtn2" value="確認" class="btn" />
                <input type="button" id="cancelBtn2" value="取消" class="btn" />
              </div>
            </li>
          </ul>
        </li>
      </ul>
    </div>
</section>
    <script>
           var data = '<%=MyTreeData %>';
    </script>
</asp:Content>