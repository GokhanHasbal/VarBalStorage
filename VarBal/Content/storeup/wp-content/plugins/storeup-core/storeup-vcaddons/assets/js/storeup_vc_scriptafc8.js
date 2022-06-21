// Single location gmap and related locations gmap
jQuery(document).ready(function($) {
	var locations = null;
	if($('.storeup_vc_more_loc_data').length){
		 locations = $.parseJSON( $('.storeup_vc_more_loc_data').text() );
	}
	
	if ( locations != null ) { 
		at_loc_map( locations,'storeup_vc_more_loc_googlemap' );
	}
	var vc_gmap_locations = null;
	if( $('.storeup_vc_single_loc_data').length) {
		vc_gmap_locations = $.parseJSON( $('.storeup_vc_single_loc_data').text() );
	}
	if ( vc_gmap_locations != null ) { 
		at_loc_map( vc_gmap_locations,'storeup_vc_single_loc_googlemap' );
	}
	function at_loc_map( locations, gmap_id ){
		var loc_length = locations.length;
		if( loc_length >= 1 ){
			var loc_lat = locations[0][0];
			var loc_lng = locations[0][1];
			var map = new google.maps.Map(document.getElementById( gmap_id ), {
		      zoom: 10,
		      center: new google.maps.LatLng( loc_lat, loc_lng ),
		      mapTypeId: google.maps.MapTypeId.ROADMAP
		    });
		    var infowindow = new google.maps.InfoWindow();
		    var marker, i;
		    for (i = 0; i < locations.length; i++) {  
		    	marker = new google.maps.Marker({
		        	position: new google.maps.LatLng( locations[i][0], locations[i][1]),
		        	map: map
		      	});
		      	google.maps.event.addListener(marker, 'click', (function(marker, i) {
		        	return function() {
		          infowindow.setContent( locations[i][2] + '<br>' +  locations[i][3] );
		          infowindow.open(map, marker);
		        }
		      })(marker, i));
		    }
		}
	}
});
