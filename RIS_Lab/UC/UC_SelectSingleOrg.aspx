﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UC_SelectSingleOrg.aspx.cs" Inherits="UC_SelectSingleOrg" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
     <link href="/css/design.min.css" rel="stylesheet"/>
     <link href="/css/tab.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
<section class="Content2">
  <h2>選擇組織單位</h2>
    <div class="tab">
      <ul>
          <li class="tabBtn here" data-tab="B"><a href="javascript:void(0);">組織瀏覽</a>
          <ul  id="tabB" class="tabs" style="position: absolute;">
            <li> 
              <section class="tree2">
                <ul id ="ulRoot">
                  
                </ul>
              </section>
            </li>
          </ul>
        </li>
      </ul>
    </div>
</section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
           var data = '<%=MyTreeData %>';
    </script>
    <%:Scripts.Render("~/bundles/UC_SelectSingleOrg_JS")%>
</asp:Content>