(function ($) {
	"use strict";
	/** Define your library strictly.*/
	
	jQuery(document).ready(function($){

		jQuery(".at-unit-price-tab-content").hide();
		jQuery('.at-unit-price-tabs li:first').addClass('active'); // FIRST LI  ADD CLASS
		var selected_tabs = jQuery('.at-unit-price-tabs li:first').attr('data-tab');
		jQuery("." + selected_tabs).show();
		jQuery('.at-unit-price-tab-list').on('click',function(){
			var selected_tab = jQuery(this).attr('data-tab');
			jQuery(".at-unit-price-tab-content").hide();
			jQuery('.at-unit-price-tabs li.active').removeClass('active'); // LI REMOVE CLASS
			jQuery(this).closest('li').addClass('active');  // LI CLASS ADDED
			jQuery("." + selected_tab).show();
		});

		/** tablesorter */
		jQuery("#at-units-pricing-table").tablesorter({
			sortList: [[3, 1]]
		});
		/** Form */
		jQuery('#storeup-bk-form').submit(function(e){
			storeup_bk_validateform(e);
		});

		function storeup_bk_validateform(e){
			var nameReg      = /^[A-Za-z]+$/;
			var numberReg    = /^(?=.*[0-9])[- +()0-9]+$/;
			var emailReg     = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;

			var iva_fname    = jQuery('#storeup_bk_name').val();
			var iva_phone    = jQuery('#storeup_bk_phoneno').val();
			var iva_email    = jQuery('#storeup_bk_email').val();
			var iva_price 	 = '';

			if ( jQuery( '.storeup_bk_price' ).hasClass( 'is-selected' ) ) {
				var iva_price = jQuery( '.storeup_bk_price.is-selected' ).attr('data-units_pricing');
			}

			var iva_location = jQuery('#storeup_bk_location').val();
			var iva_size     = jQuery('#storeup_bk_size').val();
			var iva_address  = jQuery('#storeup_bk_address').val();
			var iva_zipcode  = jQuery('#storeup_bk_zipcode').val();
			var iva_captcha  = jQuery('#storeup_bk_captcha').val();
			var iva_captcha_correct  = jQuery('#storeup_bk_captcha_correct').val();
			var iva_payment_type = jQuery('#storeup_payment_type' ).val();

			var f_proceed = p_proceed = e_proceed = l_proceed = s_proceed = pri_proceed =  a_proceed = zip_proceed = cap_proceed = true;

			/** First name */
			if ( iva_fname == '' ) {
				jQuery("#storeup_bk_name").addClass("error");
				var f_proceed = false;
			}
			if ( iva_fname ){
				jQuery("#storeup_bk_name").removeClass("error");
				var f_proceed = true;
			}
			/** Phone number */
			if ( numberReg.test( iva_phone ) ) {
				jQuery("#storeup_bk_phoneno").removeClass("error");
				var p_proceed = true;
			} else {
				jQuery("#storeup_bk_phoneno").addClass("error");
				var p_proceed = false;
			}
			/** Email */
			if ( emailReg.test( iva_email ) ) {
				jQuery("#storeup_bk_email").removeClass("error");
				var e_proceed = true;
			} else {
				jQuery("#storeup_bk_email").addClass("error");
				var e_proceed = false;
			}
			/** location */
			if( iva_location == ''){
				jQuery("#storeup_bk_location").addClass("error");
				var l_proceed = false;
			}
			if ( iva_location ) {
				jQuery("#storeup_bk_location").removeClass("error");
				var l_proceed = true;
			}
			/** Size */
			if( iva_size == ""){
				jQuery("#storeup_bk_size").addClass("error");
				var s_proceed = false;
			}
			if( iva_size ){
				jQuery("#storeup_bk_size").removeClass("error");
				var s_proceed = true;
			}
			/** Price */
			if( iva_price == ""){
				jQuery(".iva_bk_pricings").addClass("error");
				var pri_proceed = false;
			}
			if( iva_price ){
				jQuery(".iva_bk_pricings").removeClass("error");
				var pri_proceed = true;
			}
			/** Address */
			if( iva_address == '' ) {
				jQuery("#storeup_bk_address").addClass("error");
				var a_proceed = false;
			}
			if ( iva_address ) {
				jQuery("#storeup_bk_address").removeClass("error");
				var a_proceed = true;
			}
			/** Zipcode */
			if ( iva_zipcode == '' ) {
				jQuery("#storeup_bk_zipcode").addClass("error");
				var zip_proceed = false;
			}
			if ( iva_zipcode ) {
				jQuery("#storeup_bk_zipcode").removeClass("error");
				var zip_proceed = true;
			}
			/** Captcha */
			if ( iva_captcha == '' ) {
				jQuery("#storeup_bk_captcha").addClass("error");
				var cap_proceed = false;
			}
			if ( iva_captcha != '' ) {
				if ( iva_captcha !== iva_captcha_correct ) {
					jQuery("#storeup_bk_captcha").addClass('error');
					var cap_proceed = false;
				} else if( iva_captcha === iva_captcha_correct ) {
					jQuery("#storeup_bk_captcha").removeClass('error');
					var cap_proceed = true;
				}
			}
			/** If  no error proceed */
			if ( f_proceed &&
					p_proceed &&
					e_proceed &&
					l_proceed &&
					s_proceed &&
					pri_proceed &&
					a_proceed &&
					zip_proceed &&
					cap_proceed
			){
					return true;
			} else {
					e.preventDefault();
			}
		}

		/** Location */
		jQuery( "#storeup_bk_location" ).change(function() {
			jQuery('.storeup_bk_size').attr( 'data-loc', jQuery(this).val() );
			var iva_loc_id = jQuery(this).val();
			var iva_loc_size = jQuery('.storeup_bk_size').val();
			var iva_loc_price = jQuery(this).attr('data-price');
			storeup_loc_pricings( iva_loc_id, iva_loc_price, iva_loc_size);
		}).change();

		jQuery(".storeup_bk_size").change(function() {
			var iva_loc_id = jQuery('.storeup_bk_size').attr('data-loc');
			if ( iva_loc_id == null ) {
				var iva_loc_id = jQuery('.storeup_bk_location').val();
			}
			var iva_loc_size = jQuery(this).val();
			var iva_loc_price = jQuery('.storeup_bk_location').attr('data-price');
			storeup_loc_pricings( iva_loc_id, iva_loc_price, iva_loc_size);
		}).change();

		/** Loads the units_pricing based on location selection on booking template */
		function storeup_loc_pricings( iva_loc_id, iva_loc_price, iva_loc_size ){
			jQuery.ajax({
				url: storeup_localize_script_param.ajaxurl,
				type: 'post',
				dataType: 'html',
				data: {
					action     : 'storeup_bk_loc_pricings',
					iva_loc_id : iva_loc_id,
					loc_price  : iva_loc_price,
					loc_size   : iva_loc_size,
				},
				success: function( response ){
					jQuery('.iva_bk_pricings').html(response).show();
					jQuery('.at-bk-units-item').click(function(){
						jQuery('.at-bk-units-item').removeClass('is-selected');
						jQuery(this).addClass('is-selected');
						var iva_bk_item_price = jQuery(this).data('units_pricing');
						var iva_bk_item_id = jQuery(this).data('item_id');
						jQuery('#storeup_bk_item_price').val(iva_bk_item_price);
						jQuery('#storeup_bk_item_id').val(iva_bk_item_id);
					});
			  }
			});
		}

		jQuery('#at-wg-loc-tabs').change(function(){
			var loc_opt = jQuery(this).val();
			jQuery( ".at-wg-loc-tab-list-content" ).hide();
			jQuery( "#" + loc_opt ).fadeIn(800);
			jQuery( ".at-wg-loc-tabs" ).fadeOut(800);
		}).change();
	});


})();