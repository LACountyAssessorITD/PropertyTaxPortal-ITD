﻿$(document).ready(function () {

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
            $('.js--wp-4').toggleClass('animated pulse')
        },
        function () {
            $('.c-1').toggleClass('animated flip')
        }, {
            offset: '50%'
        });
    $('.js--wp-5').hover(function () {
            $('.js--wp-5').toggleClass('animated pulse')
        },
        function () {
            $('.c-2').toggleClass('animated flip')
        }, {
            offset: '50%'
        });
    $('.js--wp-6').hover(function () {
            $('.js--wp-6').toggleClass('animated pulse')
        },
        function () {
            $('.c-3').toggleClass('animated flip')
        }, {
            offset: '50%'
        });
    $('.js--wp-7').hover(function () {
            $('.js--wp-7').toggleClass('animated pulse')
        },
        function () {
            $('.c-4').toggleClass('animated flip')
        }, {
            offset: '50%'
        });
    
});