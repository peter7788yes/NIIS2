<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectOrgs.aspx.cs" Inherits="SelectOrgs" %>
<!doctype html>
<html lang="zh-TW">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<title>全國性預防接種資訊管理系統</title>
<!--[if lt IE 9]>
    <script src="js/other/html5.js"></script>
<![endif]-->
<link href="/css/design.css" rel="stylesheet">
<!--頁籤資料 -->
<link href="/css/tab.css" rel="stylesheet"/>
<style>
    a:link {
        text-decoration: none;
        color:black;
    }

    a:visited {
        text-decoration: none;
        color:black;
    }

    a:hover {
        text-decoration: none;
        color:black;
    }

    a:active {
        text-decoration: none;
        color:black;
    }
</style>
</head>
<body>
<section class="Content2">
  <h2>選擇組織單位</h2>
  <form>
    <div class="tab">
      <ul>
        <li class="tabBtn here" data-tab="A"><a href="javascript:void(0);">組織層級</a>
          <ul id="tabA" class="tabs" style="position: absolute;">
            <li> 
              <!--內容 start -->
              <div id="div1" class="formTb formTb2">
                <table>
                  <tr>
                    <td colspan="4"><input type="button" value="全選" id="selectAllA" class="btn" /></td>
                  </tr>
                  <tr>
                    <td width="20%"><a href="javascript:void(0);">
                        <input name="cb1" type="checkbox" value="1" id="cb1" class="cbs"  >
                        CDC</a></td>
                    <%if(OrgArea){ %>
                    <td width="20%"><a href="javascript:void(0);">
                        <input name="cb2" type="checkbox" value="2" id="cb2" class="cbs">
                        區管中心</a></td>
                    <%} %>
                    <td width="20%"><a href="javascript:void(0);">
                        <input name="cb3" type="checkbox" value="3" id="cb3" class="cbs">
                        局</a></td>
                    <td width="20%"><a href="javascript:void(0);">
                        <input name="cb4" type="checkbox" value="4" id="cb4" class="cbs">
                        所</a></td>
                    <td width="20%"><a href="javascript:void(0);">
                        <input name="cb5" type="checkbox" value="5" id="Checkbox1" class="cbs">
                        院</a></td>
                  </tr>
                </table>
              </div>
              <div class="formBtn">
                <input type="button" id="saveBtn1" value="確認" class="btn" />
                <input type="button" id="cancelBtn1" value="取消" class="btn" />
              </div>
              <!--內容 end --> 
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
              <!--內容 end --> 
            </li>
          </ul>
        </li>
      </ul>
    </div>
  </form>
</section>
    <script>
           var data = '<%=MyTreeData %>';
    </script>
    <script src="/js/jq/jquery-2.1.4.js"></script>
    <script src="SelectOrgs.js"></script>
</body>
</html>

