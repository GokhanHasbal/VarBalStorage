/*
**	Plugin for counter shortcode
*/
(function ($) {
	$.fn.countTo = function (options) {
		options = options || {};

		return $(this).each(function () {
			// set options for current element
			var settings = $.extend({}, $.fn.countTo.defaults, {
				from:            $(this).data('from'),
				to:              $(this).data('to'),
				speed:           $(this).data('speed'),
				refreshInterval: $(this).data('refresh-interval'),
				decimals:        $(this).data('decimals')
			}, options);

			// how many times to update the value, and how much to increment the value on each update
			var loops = Math.ceil(settings.speed / settings.refreshInterval),
				increment = (settings.to - settings.from) / loops;
			// references & variables that will change with each update
			var self = this,
				$self = $(this),
				loopCount = 0,
				value = settings.from,
				data = $self.data('countTo') || {};

			$self.data('countTo', data);
			// if an existing interval can be found, clear it first
			if (data.interval) {
				clearInterval(data.interval);
			}
			data.interval = setInterval(updateTimer, settings.refreshInterval);
			// initialize the element with the starting value
			render(value);
			function updateTimer() {
				value += increment;
				loopCount++;
				render(value);
				if (typeof(settings.onUpdate) == 'function') {
					settings.onUpdate.call(self, value);
				}
				if (loopCount >= loops) {
					// remove the interval
					$self.removeData('countTo');
					clearInterval(data.interval);
					value = settings.to;

					if (typeof(settings.onComplete) == 'function') {
						settings.onComplete.call(self, value);
					}
				}
			}
			function render(value) {
				var formattedValue = settings.formatter.call(self, value, settings);
				$self.html(formattedValue);
			}
		});
	};
	$.fn.countTo.defaults = {
		from: 0,               // the number the element should start at
		to: 0,                 // the number the element should end at
		speed: 1000,           // how long it should take to count between the target numbers
		refreshInterval: 100,  // how often the element should be updated
		decimals: 0,           // the number of decimal places to show
		formatter: formatter,  // handler for formatting the value before rendering
		onUpdate: null,        // callback method for every time the element is updated
		onComplete: null       // callback method for when the element finishes updating
	};
	function formatter(value, settings) {
		return value.toFixed(settings.decimals);
	}
}(jQuery));

function storeup_animation( items, trigger ) {
  	items.each( function() {
    	var aw_element = jQuery(this),
        	aw_animationclass = aw_element.attr('data-animation'),
        	aw_animationdelay = aw_element.attr('data-animation-delay');

		if( typeof( aw_animationclass ) !== 'undefined'){
	        aw_element.css({
		        '-webkit-animation-delay':  aw_animationdelay,
		        '-moz-animation-delay': aw_animationdelay,
		        'animation-delay': aw_animationdelay,
	        });
			var aw_trigger = ( trigger ) ? trigger : aw_element;
        	aw_trigger.waypoint(function() {
	        	aw_element.addClass('animate').addClass( 'animated  ' + aw_animationclass + '');
	        },{
	            triggerOnce: true,
	            offset: '90%'
	        });
		}
	});
}
jQuery(document).ready(function($){
	storeup_animation( jQuery('.iva_anim') );
});

/* Testimonials Slider */
function storeup_mySlider(interval, id) {
	var slides;
	var cnt;
	var amount;
	var i;

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
	slides = jQuery('#' + id + ' .testimonials > li');
	cnt = jQuery('#counter');
	amount = slides.length;
	i = 0;
	// updating counter
	cnt.text(i + 1 + ' / ' + amount);
	if (amount > 1) setTimeout(run, interval);
}

/* Progressbar */
function storeup_progressbar() {
    jQuery('.at-prgress-bar-color').each(function () {
        var percent = jQuery(this).attr('data-width');
		jQuery(this).animate({
            width: percent +'%'
        }, 2200);
		jQuery(this).parent().parent().find('.at-progress-num').countTo({
			from: 0,
			to: parseFloat( percent ),
			speed: 1500,
			refreshInterval: 50
		});
	});
}

/* Fun Fact */
function storeup_funfact() {
	jQuery('.funfact-number').each(function() {
		var $this = jQuery(this);
		var parts = $this.text().match(/^(\d+)(.*)/);
		if (parts.length < 2) return;
		var scale = 20;
		var delay = 50;
		var end = 0+parts[1];
		var next = 0;
		var suffix = parts[2];
		var runUp = function() {
			var show = Math.ceil(next);
			$this.text(''+show+suffix);
			if (show == end) return;
			next = next + (end - next) / scale;
			window.setTimeout(runUp, delay);
		}
		runUp();
	});
}

/* Accordion */
function storeup_accordion() {
  	jQuery('.ac_wrap ').each(function () {
        tabid = jQuery(this).attr('id');
        jQuery("#" + tabid + " .ac_content:not('.active')").hide();
    });
	jQuery(".ac_wrap .ac_title").click(function () {
		jQuery(this).next(".ac_content").slideToggle(400, 'swing').siblings(".ac_content:visible").slideUp(400, 'swing');
		jQuery(this).toggleClass("active");
		jQuery(this).siblings(".ac_title").removeClass("active");
	});
}

/* Toggle */
function storeup_toggle() {
	jQuery(".toggle-title").click(function () {
		jQuery(this).next(".toggle_content").slideToggle({
			duration: 200
		});
	});

	jQuery(".toggle-title").click(function () {
		jQuery(this).toggleClass('active');
	});
}

/* Tabs */
function storeup_tabs(){
	jQuery('.systabspane ').each(function () {
		tabid = jQuery(this).attr('id');
		jQuery("#" + tabid + " .tab_content").hide(); // Hide all tab conten divs by default
		if(document.location.hash && jQuery(this).find("ul.iva-tabs li a[href='"+document.location.hash+"']").length >= 1) {
			jQuery(this).find("ul.iva-tabs li a[href='"+document.location.hash+"']").parent().addClass("current");
			jQuery(this).find(document.location.hash+".tab_content").show();
		}else{
			jQuery("#" + tabid + " .tab_content:first").show(); // Show the first div of tab content by default
			jQuery("#" + tabid + " ul.iva-tabs li:first").addClass("current"); // Show the current by default
		}
		jQuery("ul.iva-tabs li a").click(function(e) {
			e.preventDefault();
		});
	});
	jQuery('.systabspane').each(function(){
		jQuery("ul.iva-tabs li").click(function () { //Fire the click event
			tabid = jQuery(this).parents('.systabspane').attr("id");
			var activeTab = jQuery(this).attr("id"); // Catch the click link
			//window.location.hash = activeTab;
			jQuery('#' +tabid + " ul li").removeClass("current"); // Remove pre-highlighted link
			jQuery(this).addClass("current"); // set clicked link to highlight state
			jQuery('#' + tabid + " .tab_content").hide(); // hide currently visible tab content div
			jQuery('#' + tabid + " " + activeTab).fadeIn(); // show the target tab content div by matching clicked link.
		});
	});
}

/* Button*/
function storeup_buttondata(){
	jQuery("a.btn").hover(function () {
		var hoverBg = jQuery(this).attr('data-btn-hoverBg');
		var hoverColor = jQuery(this).attr('data-btn-hoverColor');
		var borderhoverColor = jQuery(this).attr('data-btn-hoverborder');
		var iconhoverColor = jQuery(this).find('.btn-icon i').attr('data-btn-hovericon');

		if (hoverBg !== undefined) {
			jQuery(this).css('background-color', hoverBg);
		}
		if (borderhoverColor !== undefined) {
			jQuery(this).css('border-color', borderhoverColor);
		} else {}
		if (iconhoverColor !== undefined ) {
			jQuery(this).find('.btn-icon i').css('color', iconhoverColor );
		} else {}
		if (hoverColor !== undefined) {
			jQuery(this).css('color', hoverColor);
		} else {}
	}, function () {
		var btnbg = jQuery(this).attr('data-btn-bg');
		var btncolor = jQuery(this).attr('data-btn-color');
		var btnborder = jQuery(this).attr('data-btn-border');
		var btnicon = jQuery(this).find('.btn-icon i').attr('data-btn-icon');
		if (btnbg !== undefined) {
			jQuery(this).css('background-color', btnbg);
		} else {
				jQuery(this).css('background-color', 'transparent');
		}
		if (btncolor !== undefined) {
			jQuery(this).css('color', btncolor);
		}
		if (btnicon !== undefined) {
			jQuery(this).find('.btn-icon i').css('color', btnicon);
		}
		if (btnborder !== undefined) {
			jQuery(this).css('border-color', btnborder);
		}
	});
}
/* Message close*/
function storeup_messagebox_close(){
	jQuery("span.close").click(function () {
		jQuery(this).hide();
		jQuery(this).parent().parent().animate({
			opacity : '0'
		}).slideUp(400);
	});
}
function storeup_parallax_bg(){
	jQuery('.parallaxsection').each(function($){
		var id = jQuery(this).attr('id');
		jQuery('#'+id + ".parallaxsection").parallax("50%", 0.4);
	});
}
function storeup_hoverimage() {
	jQuery('.thumbs_hover').animate({opacity: 0});
	jQuery(".hoverimg").hover(function() {
		jQuery(this).find('.thumbs_hover').css({display:'block'}).animate({
			opacity: 1,
			bottom: (jQuery('.port_img, .hoverimg').height())/2 - 20+'px'}, 200, 'swing');
			jQuery(this).find('img').fadeTo(300,0.4);

	},function() {
		jQuery(this).find('.thumbs_hover').animate({
			opacity: 0,
			bottom: '100%'}, 200, 'swing', function() {
			jQuery(this).css({'bottom':'0'});
			});
			jQuery(this).find('img').fadeTo(300,1);
	});
}
function storeup_tabNav() {
	// Tabbed Section
	var $wpAdminBar = jQuery('#wpadminbar');
	var tabs_NavPosition;
	var	fixedHeader = jQuery('#fixedheader').outerHeight();
	if ( $wpAdminBar.length ) {
		fixedHeader = jQuery('#fixedheader').outerHeight() + $wpAdminBar.height();
	}

	if (jQuery('#fixedheader').length) {
		tabs_NavPosition = jQuery('#fixedheader').outerHeight() + jQuery('.iva_tabsWrap').outerHeight();
	} else {
		tabs_NavPosition = jQuery('.iva_tabsWrap').outerHeight();
	}
	jQuery('.iva_tabNav a').on('click', function( event ) {
		var $anchorid =jQuery(this);

		jQuery(".iva_tabsWrap ul li").removeClass("current");
		jQuery(this).parent('li').addClass("current");

		jQuery('html, body').stop().animate({
			scrollTop : jQuery(  $anchorid.attr( 'href' ) ).offset().top - tabs_NavPosition
		}, 500, '');
		event.preventDefault();
	});

	if (jQuery('#fixedheader').length) {
		jQuery(".iva_tabsWrap").sticky({ topSpacing: fixedHeader });
	} else {
		jQuery(".iva_tabsWrap").sticky({ topSpacing: 0 });
	}

}
function storeup_expandable(){
	jQuery(".at-expand-action-text").click(function () {
    $at_expand_label_text = jQuery(this);

    jQuery(".at-expand-content-outer").slideToggle(800, function () {
        $at_expand_label_text.text(function () {
            return jQuery(".at-expand-content-outer").is(":visible") ?jQuery(".at-expand-action-text").attr('data-less-label') : jQuery(".at-expand-action-text").attr('data-more-label');
        });
    });

});

}

jQuery(document).ready(function($){
	storeup_progressbar();
	storeup_tabs();
	storeup_accordion();
	storeup_mySlider();
	storeup_toggle();
	storeup_buttondata();
	storeup_messagebox_close();
  	storeup_parallax_bg();
	storeup_tabNav();
	storeup_hoverimage();
	storeup_funfact();
	storeup_expandable();

	/* Twenty Twenty Before After Image*/
	if ($.isFunction($.fn.twentytwenty)) {
		jQuery(window).on('load',function($){
		    jQuery(".twentytwenty-container[data-orientation!='vertical']").twentytwenty({default_offset_pct: 0.5});
		});
	}
	/* Table Sorter */
	if($.isFunction($.fn.tablesorter)) {
		jQuery("#at-vacant-table-104").tablesorter({sortList: [[0,0]]} );
	}
});

/**
 * jQuery.ScrollTo - Easy element scrolling using jQuery.
 * Copyright (c) 2007-2009 Ariel Flesler - aflesler(at)gmail(dot)com | http://flesler.blogspot.com
 * Dual licensed under MIT and GPL.
 * Date: 5/25/2009
 * @author Ariel Flesler
 * @version 1.4.2
 *
 * http://flesler.blogspot.com/2007/10/jqueryscrollto.html
 */
;(function(d){var k=d.scrollTo=function(a,i,e){d(window).scrollTo(a,i,e)};k.defaults={axis:'xy',duration:parseFloat(d.fn.jquery)>=1.3?0:1};k.window=function(a){return d(window)._scrollable()};d.fn._scrollable=function(){return this.map(function(){var a=this,i=!a.nodeName||d.inArray(a.nodeName.toLowerCase(),['iframe','#document','html','body'])!=-1;if(!i)return a;var e=(a.contentWindow||a).document||a.ownerDocument||a;return d.browser.safari||e.compatMode=='BackCompat'?e.body:e.documentElement})};d.fn.scrollTo=function(n,j,b){if(typeof j=='object'){b=j;j=0}if(typeof b=='function')b={onAfter:b};if(n=='max')n=9e9;b=d.extend({},k.defaults,b);j=j||b.speed||b.duration;b.queue=b.queue&&b.axis.length>1;if(b.queue)j/=2;b.offset=p(b.offset);b.over=p(b.over);return this._scrollable().each(function(){var q=this,r=d(q),f=n,s,g={},u=r.is('html,body');switch(typeof f){case'number':case'string':if(/^([+-]=)?\d+(\.\d+)?(px|%)?$/.test(f)){f=p(f);break}f=d(f,this);case'object':if(f.is||f.style)s=(f=d(f)).offset()}d.each(b.axis.split(''),function(a,i){var e=i=='x'?'Left':'Top',h=e.toLowerCase(),c='scroll'+e,l=q[c],m=k.max(q,i);if(s){g[c]=s[h]+(u?0:l-r.offset()[h]);if(b.margin){g[c]-=parseInt(f.css('margin'+e))||0;g[c]-=parseInt(f.css('border'+e+'Width'))||0}g[c]+=b.offset[h]||0;if(b.over[h])g[c]+=f[i=='x'?'width':'height']()*b.over[h]}else{var o=f[h];g[c]=o.slice&&o.slice(-1)=='%'?parseFloat(o)/100*m:o}if(/^\d+$/.test(g[c]))g[c]=g[c]<=0?0:Math.min(g[c],m);if(!a&&b.queue){if(l!=g[c])t(b.onAfterFirst);delete g[c]}});t(b.onAfter);function t(a){r.animate(g,j,b.easing,a&&function(){a.call(this,n,b)})}}).end()};k.max=function(a,i){var e=i=='x'?'Width':'Height',h='scroll'+e;if(!d(a).is('html,body'))return a[h]-d(a)[e.toLowerCase()]();var c='client'+e,l=a.ownerDocument.documentElement,m=a.ownerDocument.body;return Math.max(l[h],m[h])-Math.min(l[c],m[c])};function p(a){return typeof a=='object'?a:{top:a,left:a}}})(jQuery);
/*
Plugin: jQuery Parallax
Version 1.1.3
Author: Ian Lunn
Twitter: @IanLunn
Author URL: http://www.ianlunn.co.uk/
Plugin URL: http://www.ianlunn.co.uk/plugins/jquery-parallax/

Dual licensed under the MIT and GPL licenses:
http://www.opensource.org/licenses/mit-license.php
http://www.gnu.org/licenses/gpl.html
*/

(function( $ ){
	var $window = $(window);
	var windowHeight = $window.height();

	$window.resize(function () {
		windowHeight = $window.height();
	});

	$.fn.parallax = function(xpos, speedFactor, outerHeight) {

		var $this = $(this);
		var getHeight;
		var firstTop;
		var paddingTop = 0;

		//get the starting position of each element to have parallax applied to it
		$this.each(function(){
			firstTop = $this.offset().top;
		});

		if (outerHeight) {
			getHeight = function(jqo) {
				return jqo.outerHeight(true);
			};
		} else {
			getHeight = function(jqo) {
				return jqo.height();
			};
		}

		// setup defaults if arguments aren't specified
		if (arguments.length < 1 || xpos === null) xpos = "50%";
		if (arguments.length < 2 || speedFactor === null) speedFactor = 0.1;
		if (arguments.length < 3 || outerHeight === null) outerHeight = true;

		// function to be called whenever the window is scrolled or resized
		function update(){
			var pos = $window.scrollTop();

			$this.each(function(){
				var $element = $(this);
				var top = $element.offset().top;
				var height = getHeight($element);

				// Check if totally above or totally below viewport
				if (top + height < pos || top > pos + windowHeight) {
					return;
				}

				$this.css('backgroundPosition', xpos + " " + Math.round((firstTop - pos) * speedFactor) + "px");
			});
		}
		$window.bind('scroll', update).resize(update);
		update();
	};
})(jQuery);
