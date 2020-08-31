<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_OpenSelectSingleOrg.ascx.cs" Inherits="UC_UC_OpenSelectSingleOrg" %>

<input id="<%:ucOrgName %>" readonly="readonly" name="<%:ucOrgName %>"  value="<%:GetName() %>" type="text" class="text03"/>
<img style="cursor:pointer"  id="<%:unOpenSelectOrg %>" src="/images/location.png"  />
<input id="<%:ucOrgID %>" name="<%:ucOrgID %>" value="<%:DefaultID %>" type="hidden">

<script>
    (function (w) {
        var initUC<%=Suffix %> = function () {
             $(function () {
                $(document).on("click", "#<%:unOpenSelectOrg %>,"+"#<%:ucOrgName %>", function (e) {
                    var obj = {};
                    obj["p"] = "<%:EncryptPageUrl%>";
                    window.callOrgUC = "<%:Suffix %>";
                    openWindowWithPost("/UC/UC_SelectSingleOrg.aspx", "UC_SelectSingleOrg", 620, 450, obj);
                });
            });
        };

        w.fillOrgValue<%:Suffix %> = function (code) {
            var element = document.querySelector('#<%:ucOrgName %>');
            element.value = code.text;
            element.focus();

            var element = document.querySelector('#<%:ucOrgID %>');
            element.value = code.id;

            <%=callback%>;
        };

        w.getOrgUC = function (code) {
            //console.log("fillValue" + window.callUC + ".apply(null, " + JSON.stringify(code) + ");");
            eval("fillOrgValue" + window.callOrgUC + "(" + JSON.stringify(code) + ")");
        };

        w.getOrgID<%=Suffix %> = function () {
            return $("#<%:ucOrgID %>").val();
        };

        w.getOrgName<%=Suffix %> = function () {
            return $("#<%:ucOrgName %>").val();
        };

        w.setOrgID<%=Suffix %> = function (id) {
            $("#<%:ucOrgID %>").val(id);
        };

        w.setOrgName<%=Suffix %> = function (name) {
           $("#<%:ucOrgName %>").val(name);
        };

        var popUpWindow = function (url, title, w, h) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        }

        var openWindowWithPost = function (url, title, w, h, obj) {
            var newWindow = popUpWindow(url, title, w, h);
            if (!newWindow) return false;
            var html = "";
            html += "<html><head></head><body><form id='formid' method='post' action='" + url + "' autocomplete='off'>";
            for (var key in obj) {
                html += "<input type='hidden' name='" + key + "' value='" + obj[key] + "'/>";
            }
            html += "</form><script>document.getElementById('formid').submit();" + "</" + "script" + "></" + "body" + "></" + "html>";
            newWindow.document.write(html);
            return newWindow;
        };

        setTimeout(function () {
            if (jQuery) {
                initUC<%:Suffix %>();
            } else {
                var url = "/js/jq/jquery-2.1.4.js";
                $.getScript(url, function () {
                    initUC<%:Suffix %>();
                });
            }
        }, 1000);
     })(window);
</script>