<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_OpenSelectAgency.ascx.cs" Inherits="UC_OpenSelectAgency" %>

     <input id="<%=ucAgencyName %>" readonly="readonly" name="<%=ucAgencyName %>" class="text03" type="text"  />
     <img style="cursor:pointer" id="<%=unOpenSelectAgency %>" src="/images/icon_agency.png"  />
     <input id="<%=ucAgencyID %>" name="<%=ucAgencyID %>" type="hidden">

<script>
    //var unOpenSelectAgency = {};
    <% if(defaultAgencyID != 0) { %>
    document.querySelector("#<%=ucAgencyID %>").value = <%=defaultAgencyID%>;
    document.querySelector("#<%=ucAgencyName %>").value = '<%=defaultAgencyName%>';
    <% }%>
    (function (w) {
        var initUC<%=Suffix %> = function () {
            $(function () {                
                $(document).on("click", "#<%=unOpenSelectAgency %>,"+"#<%=ucAgencyName %>", function (e) {
                    var keys = [];
                    var values = [];
                    keys[0] = "p";
                    values[0] = "<%=EncryptPageUrl%>";
                    keys[1] = "hf";
                    values[1] = "<%=hasFilter%>";
                    keys[2] = "as";
                    values[2] = <%=agencyState%>;
                    w.callUC = "<%=Suffix %>";
                    openWindowWithPost("/UC/UC_SelectAgency.aspx", "UC_SelectAgency",  930, 450, keys, values);
                });
            });
        };

        w.fillValue<%=Suffix %> = function (code) {
            var element = document.querySelector('#<%=ucAgencyName %>');
            element.value = code.AN;
            element.focus();

            var element = document.querySelector('#<%=ucAgencyID %>');
            element.value = code.I;
            $('#<%=ucAgencyID %>').change();
        }

        w.getAgencyUC = function (code) {
        
            //console.log("fillValue" + window.callUC + ".apply(null, " + JSON.stringify(code) + ");");
            eval("fillValue" + window.callUC + "(" + JSON.stringify(code) + ")");
        };

   

        w.getAgencyID<%=Suffix %> = function () {
            return $("#<%=ucAgencyID %>").val();
        };

        w.getAgencyName<%=Suffix %> = function () {
            return $("#<%=ucAgencyName %>").val();
        };

        var popUpWindow = function (url, title, w, h) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
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
                initUC<%=Suffix %>();
            } else {
                var url = "/js/jq/jquery-2.1.4.js";
                $.getScript(url, function () {
                    initUC<%=Suffix %>();
                });

            }
        }, 1000);
    })(window);//(unOpenSelectAgency);
</script>