<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_OpenSelectOrgs.ascx.cs" Inherits="UC_UC_OpenSelectOrgs" %>

<input id="<%:ucOrgsName %>" readonly="readonly" name="<%:ucOrgsName %>"type="text" class="text03"/>
<img style="cursor:pointer"  id="<%:unOpenSelectOrgs %>"  src="/images/location.png"  />
<input id="<%:ucOrgsID %>_Area" name="<%:ucOrgsID %>_Area" type="hidden">
<input id="<%:ucOrgsID %>" name="<%:ucOrgsID %>" type="hidden">

<script>
    var initUC<%:Suffix %> = function () {
         $(function () {
            $(document).on("click", "#<%=unOpenSelectOrgs %>,"+"#<%=ucOrgsName %>", function (e) {
                var keys = [];
                var values = [];
                keys[0] = "p";
                values[0] = "<%:EncryptPageUrl%>";
                window.callUC = "<%:Suffix %>";
                openWindowWithPost("/UC/UC_SelectSingleOrg.aspx", "UC_SelectSingleOrg", 620, 450, keys, values);
            });
        });
    };

     var fillValue<%:Suffix %> = function (code) {
        var element = document.querySelector('#<%:ucOrgsName %>');
        element.value = code.text;
        element.focus();

        var element = document.querySelector('#<%:ucOrgsID %>');
        element.value = code.id;
     }

    var getOrgUC = function (code) {
        
        //console.log("fillValue" + window.callUC + ".apply(null, " + JSON.stringify(code) + ");");
        eval("fillValue" + window.callUC + "(" + JSON.stringify(code) + ")");
    };

    var getOrgID<%:Suffix %> = function () {
        return $("#<%:ucOrgsID %>").val();
    };

    var getOrgName<%:Suffix %> = function () {
        return $("#<%:ucOrgsName %>").val();
    };

    var popUpWindow = function (url, title, w, h) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
    }

    var openWindowWithPost = function (url, title, w, h, keys, values) {
                 var newWindow = popUpWindow(url, title, w, h);
                 if (!newWindow) return false;
                 var html = "";
                 html += "<html><head></head><body><form id='formid' method='post' action='" + url + "' autocomplete='off'>";
                 keys = keys || [];
                 values = values || [];
                 if (keys && values && (keys.length == values.length))
                     for (var i = 0; i < keys.length; i++)
                         html += "<input type='hidden' name='" + keys[i] + "' value='" + values[i] + "'/>";
                 html += "</form><script>document.getElementById('formid').submit();"+"</"+"script"+"></"+"body"+"></"+"html>";
                 newWindow.document.write(html);
                 return newWindow;
   }

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
</script>