(function ($) {
    "use strict";
    /** Define your library strictly.*/

	jQuery(document).ready( function ($) {

		/** Initializing the Flexslider */
		if ( $.isFunction($.fn.flexslider) && typeof flexslider_args !== 'undefined' ) {
			$(".flexslider").flexslider({
				animation: flexslider_args.slideeffect,
				controlsContainer: ".flex-container",
				slideshow: true,
				slideshowSpeed:flexslider_args.slidespeed,
				animationSpeed: 1200,
				directionNav: flexslider_args.slidednav,
				controlNav: false,
				mousewheel: false,
				smoothHeight :false,
				start: function(slider) {
					jQuery(".total-slides").text(slider.count);
					jQuery(".flex-active-slide").find(".flex-caption").addClass("flex-caption-anim");
				},
				before: function(slider){
					  jQuery(slider).find(".flex-active-slide").each(function(){
						jQuery(".flex-active-slide").find(".flex-caption").removeClass("flex-caption-anim");
					  });
				},
				after: function(slider){
				  jQuery(".flex-active-slide").find(".flex-caption").addClass("flex-caption-anim");
				}
			});
		}

		/** Owl Carousel */
		if ( $.isFunction($.fn.owlCarousel) && typeof owlslider_args !== 'undefined'  ) {
			var slider_navigation = owlslider_args.slidenavigation == 'true' ? true : false;
			$("#owlcarousel_slider").owlCarousel({
				navigation : true,
				navigationText: [
				  "<i class='fa fa-angle-up fa-2x fa-fw'></i>",
				  "<i class='fa fa-angle-down fa-2x fa-fw'></i>"
				  ],
				pagination : false,
				slideSpeed : owlslider_args.slidespeed,
				singleItem : true,
				theme : 'storeup-owl',
				autoPlay: false,
				// addClassActive : true,
				transitionStyle : "fade"
			});
		}

		/** Pretty Photo */
		if ( $.isFunction($.fn.prettyPhoto) ) {
			$("a[rel^='prettyPhoto']").prettyPhoto({
				theme: 'pp_default',
				social_tools: false,
				deeplinking : false
			});
		}

		/** Footer Branches  */
		$('.footer-branches').hide();
		$( ".at-footer-branches" ).on('click', function(e) {
			e.preventDefault();
				$(this).toggleClass('at-toggleOpen');
			$( ".footer-branches" ).slideToggle(500);
			$('html, body').animate({
				scrollTop: $(document).height()
			}, 500);
		});

		/** Tooltip */
		$('.storeup_tip,.iva_tip').hover(function () {
			var ivaWidth = $(this).outerWidth();
			var tooltipWidth = $(this).find('span.ttip').outerWidth();
			var left = (ivaWidth - tooltipWidth) / 2;
			$(this).find('span.ttip').css('left', left).fadeIn();
		}, function () {
			$(this).find('span.ttip').fadeOut();
		});

		/** Vidoe Resize Fitvids */
		if ( $.fn.fitVids) {
			$('.video-frame,.boxcontent,.video-stage,.video,.post,.iva_map').fitVids();
		}

		/** Staff Social Hover */
		$('.speaker-item').each(function () {
			$(".sociables", this).stop().animate({bottom:'-100'},{queue:false,duration:400});
			$(".sociables").hide();
			$(this).hover(function(){
				$(".sociables", this).stop().animate({bottom:'0'},{queue:false,duration:400});
				$(".sociables", this).fadeIn();
			}, function() {
				$(".sociables", this).stop().animate({bottom:'-100'},{queue:false,duration:400});
				$(".sociables", this).fadeOut();

			});
		});

		/** BacktoTop Scroll */
        // hide #back-top first
		$("#back-top").hide();
		// fade in #back-top
		$(function () {
			$(window).scroll(function () {
				if ($(this).scrollTop() > 100) {
					$('#back-top').fadeIn();
				} else {
					$('#back-top').fadeOut();
				}
			});

			// scroll body to 0px on click
			$('#back-top a').click(function () {
				$('body,html').animate({
					scrollTop: 0
				}, 800);

			   return false;
			});
		});

		/** Mobile Menu */
		$('#iva-mobile-nav-icon').click(function(){
			$(this).toggleClass('open');
			$('.iva-mobile-menu').slideToggle(500);
			return false;
		});

		/** Child Menu Toggle */
		$('.iva-children-indenter').click(function(){
			$(this).parent().parent().toggleClass('iva-menu-open');
			$(this).parent().parent().find('> ul').slideToggle();
			return false;
		});
		resizemobile();

		$('.search-close').on("click", function(e) {
			$('#ivaSearchbar').slideUp(300);
		});

		/** Fixed Header */
		var $aSelected = $('#wrapper').find('div');

		if( $aSelected.is('#fixedheader') ){

			var stickyHeaderTop = $('#fixedheader').offset().top;
			var $wpAdminBar = $('#wpadminbar');

			$( window ).scroll(function(){
				if( $( window ).scrollTop() > stickyHeaderTop ) {
					$('#fixedheader').addClass("fixed-header");
					if ( $(window).width() > 1024) {
						$('.logo img').css({'transform':'scale(0.72)'});
						if ( $wpAdminBar.length ) {
							$('.fixed-header').css('top',$wpAdminBar.height()+'px');
						}
					}
				} else {
					$('#fixedheader').removeClass("fixed-header");
					$('#fixedheader').css('top','0');
					if ( $(window).width() > 1024) {
						$('.logo img').css({'transform':'scale(1)'});
					}
				}
			});
		}

		/** Init*/
		$(window).resize(resizemobile);

		/** Wait for window load */
		$(window).on('load',function() {
			$('.storeup_page_loader').fadeOut(500,function(){
				$(this).remove();
			});
		});

		$("#iva_menu").superfish();

		/** Adds custom class to datepicker ui */
		$(".ui-datepicker").addClass("iva-date-ui");

		/** Hover image fade*/
		hoverimage();

		/** StickyBar Toggler */
		$("#trigger").click(function () {
			$(this).next("#sticky").slideToggle({
				duration: 300
			});
		});


		var $wpAdminBar = $('#wpadminbar');
		var tarrow, sticky_adminbar;

		if ( $wpAdminBar.length ) {
			sticky_adminbar = $wpAdminBar.height() + 5;
			tarrow = $('.tarrow').css({ top: sticky_adminbar });
		} else {
			sticky_adminbar = 5;
			tarrow = $('.tarrow').css({top: sticky_adminbar });
		}

		/** Stickybar Toggler */
		$("#trigger").toggle(function () {
			$(this)
				.animate({ top: sticky_adminbar }, 50)
				.animate({ top: sticky_adminbar }, 50)
				.animate({ top: sticky_adminbar }, 800).children().addClass("fa-arrow-circle-o-up");
		}, function () {
			$(this)
				.animate({ top: sticky_adminbar }, 50)
				.animate({ top: sticky_adminbar }, 50)
				.animate({ top: sticky_adminbar }, 800).children().removeClass("fa-arrow-circle-o-up");
		});

			/** Searchbar Pop-up */

		$('body').on("click", function(e){
			var target = $(e.target);
			if(!target.is(".ivaInput")) {
				jQuery('#ivaSearchbar').slideUp(300);
			}
			$('#ivaSearch').on("click", function(e) {
				if ($(e.srcElement).closest('#ivaSearchbar').length) return;
				$('#ivaSearchbar').slideDown(300);
				$('#ivaSearchbar input[type=text]').focus();
				return false;
			});
		});



	/** On Window Resize */
	function resizemobile(){
		// show meun starting from iPad Portrait
		if( window.innerWidth < 959 ){
			$('.header #menuwrap').hide();
			$('.header .iva-light-logo').hide();
			$('.header .iva-dark-logo').show();
		}else {
			$('.header #menuwrap').show();
			$('.iva-mobile-menu').hide();
			$('.header .iva-light-logo').show();
			$('.header .iva-dark-logo').hide();
		}
	}

	/** Hover Image */
	function hoverimage() {
		$('.hover_type').animate({opacity: 0});
		$(".port_img, .sort_img").hover(function() {
			$(this).find('.hover_type').css({display:'block'}).animate({
				opacity: 1,
				bottom: ($('.port_img, .sort_img').height())/2 - 20+'px'
			}, 200, 'swing');
			$(this).find('img').fadeTo(300,0.4);

		},function() {
			$(this).find('.hover_type').animate({
				opacity: 0,
				bottom: '100%'
			}, 200, 'swing', function() { $(this).css({'bottom':'0'});	});
			$(this).find('img').fadeTo(300,1);
		});
	}

	/** Testimonials Slider */
	function MySlider( interval, id ) {
		var slides,cnt,amount,i;

		function run() {
			// hiding previous image and showing next
			jQuery(slides[i]).fadeOut('slow', function () {
				// Animation complete.
				i++;
				if (i >= amount) i = 0;
				jQuery(slides[i]).fadeIn('slow');

				// updating counter
				cnt.text(i + 1 + ' / ' + amount);
			});
			// loop
			setTimeout(run, interval);
		}
		slides  = jQuery('#' + id + ' .testimonials > li');
		cnt 	= jQuery('#counter');
		amount  = slides.length;
		i = 0;

		// updating counter
		cnt.text(i + 1 + ' / ' + amount);
		if (amount > 1) setTimeout(run, interval);

	}
});//End of Document

})();
