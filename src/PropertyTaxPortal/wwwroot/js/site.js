$(document).ready(function () {

    $('.section-news .carousel').carousel({
        interval: 4000
    })

    $('.js--wp-1').waypoint(function (direction) {
        $('.js--wp-1').addClass('animated fadeIn');
    }, {
            offset: '50%'
        });

    $('.js--wp-2').waypoint(function (direction) {
        $('.js--wp-2').addClass('animated fadeInUp');
    }, {
            offset: '50%'
        });

    $('.js--wp-3').waypoint(function (direction) {
        $('.js--wp-3').addClass('animated bounceInLeft');
    }, {
            offset: '50%'
        });

    $('.js--wp-8').waypoint(function (direction) {
        $('.js--wp-8').addClass('animated fadeInLeft');
    }, {
            offset: '50%'
        });
    $('.js--wp-9').waypoint(function (direction) {
        $('.js--wp-9').addClass('animated fadeInRight');
    }, {
            offset: '50%'
        });

    $('.js--wp-4').hover(function () {
        $('.js--wp-4').addClass('animated pulse')
        $('.c-1').addClass('animated flip')
    },
        function () {
            $('.js--wp-4').removeClass('animated pulse')
            $('.c-1').removeClass('animated flip')
        });
    $('.js--wp-5').hover(function () {
        $('.js--wp-5').addClass('animated pulse')
        $('.c-2').addClass('animated flip')
    },
        function () {
            $('.js--wp-5').removeClass('animated pulse')
            $('.c-2').removeClass('animated flip')
        });
    $('.js--wp-6').hover(function () {
        $('.js--wp-6').addClass('animated pulse')
        $('.c-3').addClass('animated flip')
    },
        function () {
            $('.js--wp-6').removeClass('animated pulse')
            $('.c-3').removeClass('animated flip')
        });
    $('.js--wp-7').hover(function () {
        $('.js--wp-7').addClass('animated pulse')
        $('.c-4').addClass('animated flip')
    },
        function () {
            $('.js--wp-7').removeClass('animated pulse')
            $('.c-4').removeClass('animated flip')
        });

});