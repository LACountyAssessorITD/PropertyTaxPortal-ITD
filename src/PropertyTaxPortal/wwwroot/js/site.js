$(document).ready(function () {

    //$('#gsc-i-id1').click(function () {
    //    $('.search-icon').toggleClass('search-hide');
    //});
 
    $('.search').click(function () {
        //$(this).toggleClass('icon-cross');
        

        $('.search-feature').toggleClass('down');
        $('.navbar-collapse').removeClass('show');
        $('.index-wrap').toggleClass('padding-down')
    });
    $('.close').click(function () {
        //$(this).toggleClass('icon-cross');
       
        
        $('.search-feature').removeClass('down');
        //$('.navbar-collapse').removeClass('show');
        $('.index-wrap').removeClass('padding-down')
    });
    var navBar = $(".tab-background");
    
    $(window).scroll(function () {
        //window.alert(hdrHeight);
        if ($(this).scrollTop() > 250) {
            navBar.addClass("navScrolled");
        } else {
            navBar.removeClass("navScrolled");
        }
    });

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
    $('.js--wp-10').hover(function () {
        $('.js--wp-10').addClass('animated pulse')
        $('.c-5').addClass('animated flip')
    },
        function () {
            $('.js--wp-10').removeClass('animated pulse')
            $('.c-5').removeClass('animated flip')
        });
    $('.floatbox').click(function () {
        $('.floatbox').removeClass('display-block')
    });

    $("input[type=text]").focusin(function () {
        $('.expand-test').addClass('textwidth');


    });
    $("input[type=text]").focusout(function () {
        $('.expand-test').removeClass('textwidth');


    });
});

function displayDescription(i) {
    //JH = document.body.clientHeight;
    //JW = document.body.clientWidth;
    //JX = document.body.scrollLeft;
    //JY = document.body.scrollTop;
    //var w = null;
    
    $('#div' + i).addClass('display-block')
    //w.style.right = 0;
    //w.style.top = document.body.scrollTop;
    //w.style.display = '';
}
function killDescription(i) {
    //document.getElementById("div" + i).style.display = 'none';
}
