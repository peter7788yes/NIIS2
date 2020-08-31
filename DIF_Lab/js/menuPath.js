try {
    //var myHtml = sessionStorage.getItem("menuPath");
    //var parent = document.getElementsByClassName('path')[0];
    //parent.innerHTML = myHtml;
    setTimeout(function () {
        window.parent.changeMenuPath(location.pathname);
    }, 0);
}
catch (e) {
}