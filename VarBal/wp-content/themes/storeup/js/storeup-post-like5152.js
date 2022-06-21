// Aivah Post Like
jQuery(document).ready(function($){
	'use strict';
	$(document).on('click','.iva-love', function() {
		var $loveLink = $(this);
		var $id = $(this).attr('id');

		if($loveLink.hasClass('loved')) return false;

		var $sendTheData = {
			action: 'storeup-love',
			loves_id: $id
		}
		$.post(ivaLove.ajaxurl, $sendTheData, function(data){
			$loveLink.find('span').html(data);
			$loveLink.addClass('loved').attr('title', ivaLove.youLikedit);
			$loveLink.find('span').css({'opacity': 1,'width':'auto'});
		});
		return false;
	});
});