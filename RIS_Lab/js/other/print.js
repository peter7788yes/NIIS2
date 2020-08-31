var printScreen = function(printlist) {
    var value = printlist.innerHTML;
    var printPage = window.open("", "Printing...", "");
    printPage.document.open();
    printPage.document.write("<html><head>");
    printPage.document.write('<link href="/css/common.css" rel="stylesheet" />');
    printPage.document.write('<link href="/css/table.css" rel="stylesheet" />');
    printPage.document.write('<link href="/css/page.css" rel="stylesheet" />');
    printPage.document.write('<link href="/css/list.css" rel="stylesheet" />');
    printPage.document.write("<html><head>");
    printPage.document.write("<html><head>");
    printPage.document.write("</head><body class='bodybg' onload='window.print();window.close()'>");
    printPage.document.write("<section class='Content'>");
    //printPage.document.write("<PRE>");
    printPage.document.write(value);
    //printPage.document.write("</PRE>");
    printPage.document.write("</section>");
    printPage.document.close("</body></html>");
}
      
