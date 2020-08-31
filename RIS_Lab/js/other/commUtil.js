// 利用post 到某頁面
function post_to_url(path, params, method) 
{
    method = method || "post"; // Set method to post by default, if not specified.

    var form = document.createElement("form");
    form.setAttribute("method", method);
    form.setAttribute("action", path);

    for (var key in params) 
    {
        var hiddenField = document.createElement("input");
        hiddenField.setAttribute("type", "hidden");
        hiddenField.setAttribute("name", key);
        hiddenField.setAttribute("value", params[key]);
        form.appendChild(hiddenField);
    }
    
    document.body.appendChild(form);
    form.submit();
}

function trim(s) {
    return s.replace(/^\s+|\s+$/, '');
}

function validateEmail(emailAddr, fldDescr, endChar) {
    var error = "";
    var emailFilter = /^[^@]+@[^@.]+\.[^@]*\w\w$/;
    var illegalChars = /[\(\)\<\>\,\;\:\\\"\[\]\'\%]/;
    if (emailAddr == "") {
        error = "請輸入" + fldDescr + "。" + endChar;
    }
    else if (!emailFilter.test(emailAddr) || emailAddr.match(illegalChars))
        error = "請輸入正確" + fldDescr + "。" + endChar;
    return error;
}

function validateLength(slen, min, max, fldDescr, endChar) {
    var error = "";
    if (min > -1 && slen < min)
        error = fldDescr + "必須大於" + min + "。" + endChar;
    else if (max > -1 && slen > max)
        error = fldDescr + "必須小於" + max + "。" + endChar;
    return error;
}

function isNumeric(str, type) {
    var regex = '';
    switch (type) {
        case 'int':
            regex = /^[+]?[0-9]+$/;
            break;
        case 'float':
            regex = /^[+]?[0-9]+\.?[0-9]*$/;
            break;
        default:
            //預設是int
            regex = /^[+]?[0-9]+$/;
            break;
    }
    return regex.test(trim(str)) ? true : false;
}

function isPhoneNumber(str) {
    var regex = /^(\(\d{2,3}\)|\d{2,3})-?\d{7,8}$/;
    return regex.test(trim(str)) ? true : false;
}

PostOpen = function (verb, url, data, target) {
    var form = document.createElement("form");
    form.action = url;
    form.method = verb;
    form.target = target || "_self";
    if (data) {
        for (var key in data) {
            var input = document.createElement("textarea");
            input.name = key;
            input.value = typeof data[key] === "object" ? JSON.stringify(data[key]) : data[key];
            form.appendChild(input);
        }
    }
    form.style.display = 'none';
    document.body.appendChild(form);
    form.submit();
};


function OpenWindowWithPostOptions(url, iWidth, iHeight, name, params) {

    var iTop = (window.screen.availHeight - 30 - iHeight) / 2;  //視窗的垂直位置;
    var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;   //視窗的水平位置;
    var windowoption = "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + ",left=" + iLeft + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes";
 
    OpenWindowWithPost(url, windowoption, name, params);

}

 
function OpenWindowWithPost(url, windowoption, name, params) {
    
 var form = document.createElement("form");
 form.setAttribute("method", "post");
 form.setAttribute("action", url);
 form.setAttribute("target", name);
 for (var i in params)
 {
   if (params.hasOwnProperty(i))
   {
     var input = document.createElement('input');
     input.type = 'hidden';
     input.name = i;
     input.value = params[i];
     form.appendChild(input);
   }
 }
 document.body.appendChild(form);
 //note I am using a post.htm page since I did not want to make double request to the page
 //it might have some Page_Load call which might screw things up.
 
 window.open("", name, windowoption);
 form.submit();
 document.body.removeChild(form);
}
