jQuery(document).ready(function ($) {
	$('.select2_dropdown[name="khoa-hoc"]').select2({
		placeholder: 'Chá»n khĂ³a há»c*',
		minimumResultsForSearch: -1,
		width: 'style'
	});
});
jQuery(document).ready(function ($) {
	$('.select2_dropdown.monday').select2({
		placeholder: 'Chá»n mĂ´n giáº£ng dáº¡y*',
		minimumResultsForSearch: -1,
		width: 'style'
	});
});
jQuery(document).ready(function ($) {
	$('.select2_dropdown.vi-tri').select2({
		placeholder: 'Chá»n vá»‹ trĂ­ á»©ng tuyá»ƒn *',
		minimumResultsForSearch: -1,
		width: 'style'
	});
});
document.addEventListener('wpcf7invalid', function (event) {
	setTimeout(function () {
		jQuery(".wpcf7 form .wpcf7-response-output").slideDown(300);
		jQuery(".wpcf7-not-valid-tip").slideDown(300);
	}, 2000);
	setTimeout(function () {
		jQuery(".wpcf7 form .wpcf7-response-output").delay(1000).slideUp(300);
		jQuery(".wpcf7-not-valid-tip").delay(1000).slideUp(300);
	}, 3000);
}, false);
