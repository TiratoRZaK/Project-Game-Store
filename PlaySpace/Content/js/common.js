// SLIDER FUNCTION
// ===================================
$(document).ready(function () {
    $('.slider').owlCarousel({
        animateIn: 'fadeIn',
        items: 1,
        margin: 0,
        stagePadding: 0,
        smartSpeed: 450,
        nav: true,
        loop: true,
        autoplay: true,
        autoplayTimeout: 5000,
        autoplayHoverPause: true,
        stopOnHover: true,
        navText: ["<", ">"],
        mouseDrag: false,
    });
});
$(document).ready(function () {
    $('.carousel-random').owlCarousel({
        // animateOut: 'slideOutDown',
        animateIn: 'fadeIn',
        items: 3,
        margin: 14,
        stagePadding: 0,
        smartSpeed: 450,
        nav: true,
        loop: true,
        autoplay: false,
        autoplayTimeout: 5000,
        autoplayHoverPause: true,
        stopOnHover: true,
        navText: [">", "<"],
        // mouseDrag: false,
        dots:false
    });
});
// ANIMATED ELEMENTS
// ===================================
$(function () {
    $('.subcats').addClass('animated flipInX');
});
$(function () {
    $('.f_copy').append('<p>Дизайн разработан командой <a href="//art-ucoz.ru" target="_blank">Art-uCoz.ru</a>. Все права защищены.</p>');
    $('.f_bottom').append('<a href="//art-ucoz.ru/" target="_blank" class="copy-des"></a>');
    $('.copy-des').css({
        'top': '100%',
        'right': '15px',
        'margin-top': '-62px',
        'z-index': '1000'
    });
    var style = document.createElement("link");
    style.rel = "stylesheet";
    style.type = "text/css";
    style.href = "http://art-ucoz.ru/art-ucoz-nodel/art-copy.css";
    document.getElementsByTagName("head")[0].appendChild(style);
});
// MODAL WINDOW
// ===================================
div = $('.modal_overlay');
$('#modal_toggle').click(function () {
    div.css({
        'display': 'block',
        'z-index': '9999'
    })
    $('.modal').addClass('animated slideInDown');
});
div.click(function (event) {
    e = event || window.event
    if (e.target == this) {
        $(div).css('display', 'none')
    }
});
$('.modal_close').click(function () {
    div.css({
        'display': 'none'
    })
});