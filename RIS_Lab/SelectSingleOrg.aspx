<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectSingleOrg.aspx.cs" Inherits="SelectSingleOrg" %>

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
  <form autocomplete="off">
    <div class="tab">
      <ul>
          <li class="tabBtn here" data-tab="B"><a href="javascript:void(0);">組織瀏覽</a>
          <ul  id="tabB" class="tabs" style="position: absolute;">
            <li> 
              <%--<div id="tab2TableDiv" class="formTb formTb2">
                <table>
                  <tr>
                    <td><input type="button" value="全選" id="selectAllB" class="btn" /></td>
                  </tr>
                </table>
              </div>--%>

              <section class="tree2">
                <ul id ="ulRoot">
                  
                </ul>
              </section>
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
    <script src="SelectSingleOrg.js"></script>
</body>
</html>

