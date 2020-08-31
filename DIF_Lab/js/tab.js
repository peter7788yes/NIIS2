$( document ).ready( function() {
	var tabIndex = 0;
	
	$('.tab > ul > li').removeClass('here');
	
	$('.tab > ul > li > ul').css('position', 'absolute').hide();
	
	$('.tab > ul > li').click(function() {
		
		$(this).addClass('here').find('> ul').show();
		checkTabsHeight();
	});
	
	$('.tab > ul > li > a').focus(function() {
		$(this).closest(".tab").find("ul > li").removeClass('here').find('> ul').hide();
		$(this).parent().addClass('here').find('> ul').show();
		/*控制第二層ul*/
		$(this).parent().addClass('here').find('.tree2>ul>li>ul').show();
		$(this).parent().addClass('here').find('.tree2>ul>li>ul>li>ul').show();
		$(this).parent().addClass('here').find('.tree2>ul>li>ul>li>ul>li>ul').show();
		checkTabsHeight();
	});
	
	$(window).resize(function() {    
		checkTabsHeight();
	});
	
	$('.tab').find("ul li:eq(0)").click();
	
});
function checkTabsHeight(){
	$( '.tab' ).css('height',$($( '.here' ).children()[0]).height()+$($( '.here' ).children()[1]).height());
}