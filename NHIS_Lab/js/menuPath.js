try {
    var myHtml = sessionStorage.getItem("menuPath");
    var parent = document.getElementsByClassName('path')[0];
    parent.innerHTML = myHtml;
}
catch (e) {
}