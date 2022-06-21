(function ($) {
	"use strict";
	/** Define your library strictly.*/

	jQuery(document).ready(function($) {

		/** On change function start */
		jQuery( "#iva_cities" ).change(function() {

			var iva_city	= jQuery( '#iva_cities' ).val();

			/** ajax calling function start */
			$.ajax({
				url: storeup_localize_script_param.ajaxurl,
				type: 'post',
				dataType: 'json',
				data: {
					action   : 'storeup_get_locations_by_cat', //get cities dropdown list function
					cat_slug : iva_city,
				},

				/** success function start */
				success: function( response ) {
					var loc_details = '';
					jQuery('.iva_loc_display').html(loc_details).show();
					for ( var j in response ) {
						var p1 = response[j];
						var iva_protocol = 'http';

						loc_details = loc_details + '<div class="storeup-loc-addr" data-lat="'+ p1[0] +'" data-lng="'+ p1[1]+'" data-addr="'+ p1[3]+'">';
						loc_details = loc_details + '<span class="storeup-loc-addr-icon"><a href="'+iva_protocol+'://maps.google.com/maps?saddr=Current+Location&daddr='+p1[3]+'" target="_blank">Get Direction</a></span><div class="storeup-loc-addr-info">';

						if( p1[2] ){
							loc_details = loc_details + '<h2><a href="'+ p1[5] +'">' + p1[2] + '</a></h2>';
						}
						loc_details = loc_details + '<ul class="storeup-loc-addr-details">';
						if( p1[3] ){
							loc_details = loc_details + '<li>' + p1[3] + '</li>';
						}
						if( p1[4] ){
							loc_details = loc_details + '<li>' + p1[4]+ '</li>';
						}
						loc_details = loc_details +'</ul></div></div>';
					} /** for loop closing */

					jQuery('.iva_loc_display').html(loc_details).show();

					/** particular city click function */
					jQuery( ".storeup-loc-addr" ).click(function() {
						jQuery('.storeup-loc-addr').removeClass('storeup-active');
						var  lattitudes 		 =  jQuery(this).attr('data-lat');
						var  longitudes 		 =  jQuery(this).attr('data-lng');
						var  content 		     =  jQuery(this).attr('data-addr');
						var  wopen  			 =  'true';
						var  LocationData   	 =  [
							[lattitudes,longitudes,content,wopen ],
						];
						jQuery(this).addClass('storeup-active');
						initialize(LocationData);
					 }); /** click function closing */

					/** Initialize function start */
					function initialize(LocationData) {
						var length_array = LocationData.length;
						var map = new google.maps.Map(document.getElementById('storeup-googlemap'));
						map.setOptions({ draggable: true, zoomControl: true, scrollwheel: false, disableDoubleClickZoom: true, disableDefaultUI: true });
						var bounds = new google.maps.LatLngBounds();
						var infowindow = new google.maps.InfoWindow();
						if(length_array == 1){
							var zoom = 16;
						} else if (length_array >= 2) {
							var zoom = 6;
						}

						for (var i in LocationData){
							var p      =  LocationData[i];
							var latlng =  new google.maps.LatLng(p[0], p[1]);
							bounds.extend(latlng);
							var  address_title = '<div class="iva_address">'+p[2]+'</div>';
							var marker =  new google.maps.Marker({
								position: latlng,
								map: map,
								title:p[2],
								icon: storeup_localize_script_param.directory_uri+'/images/marker-icon.png'
							});
							if( p[3] == 'true') {

								infowindow = new google.maps.InfoWindow();
								infowindow.setContent(address_title);
								infowindow.open(map, marker);
							}else {
								google.maps.event.addListener(marker, 'click', function() {
								infowindow.setContent(this.title);
								infowindow.open(map, this);
							});
							}

						} // for loop closing
						map.fitBounds(bounds);
					} // initialize function closing

					initialize(response);// deafult city and selected city function
				} // success function closing

			}); // ajax calling function closing

		}).change(); // on change function closing

	}); // document  ready function closing

})();