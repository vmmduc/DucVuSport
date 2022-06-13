$(document).ready(function () {
    let navbar = $('.header__menu');
    let searchForm = $('.header__search');
    let shoppingCart = $('.header__cart');
    let account = $('.header__account');

    $('.header__menu-item').click(function () {
        searchForm.removeClass('active');
        account.removeClass('active');
        shoppingCart.removeClass('active');
    });

    $('#search-btn').click(function () {
        if (!searchForm.hasClass('active'))
            searchForm.addClass('active');
        else
            searchForm.removeClass('active');

        shoppingCart.removeClass('active');
        account.removeClass('active');
        navbar.removeClass('active');
    })

    $('#cart-btn').click(function () {
        if (!shoppingCart.hasClass('active'))
            shoppingCart.addClass('active');
        else
            shoppingCart.removeClass('active');

        searchForm.removeClass('active');
        account.removeClass('active');
        navbar.removeClass('active');
    });

    $('#menu-btn').click(function () {
        if (!navbar.hasClass('active'))
            navbar.addClass('active');
        else
            navbar.removeClass('active');

        searchForm.removeClass('active');
        shoppingCart.removeClass('active');
        account.removeClass('active');
    });

    $('#account-btn').click(function () {
        if (!account.hasClass('active'))
            account.addClass('active');
        else account.removeClass('active');

        searchForm.removeClass('active');
        navbar.removeClass('active');
        shoppingCart.removeClass('active');
    });

    $('.body').click(function () {
        searchForm.removeClass('active');
        navbar.removeClass('active');
        shoppingCart.removeClass('active');
        account.removeClass('active');
    })

    /*
     * Account modal
     */
    $('#login_btn').click(function () {
        if ($('.modal-body__register').hasClass('active')) {
            $('.modal-header__register').removeClass('active');
            $('.modal-body__register').removeClass('active');
        }
        if (!$('.modal-body__login').hasClass('active')) {
            $('.modal-header__login').addClass('active');
            $('.modal-body__login').addClass('active');
        }
    });
    $('#register_btn').click(function () {
        if ($('.modal-body__login').hasClass('active')) {
            $('.modal-header__login').removeClass('active');
            $('.modal-body__login').removeClass('active');
        }
        if (!$('.modal-body__register').hasClass('active')) {
            $('.modal-header__register').addClass('active');
            $('.modal-body__register').addClass('active');
        }
    });

    /*
     * Show/hide password
     */
    $('.pwd-input').each(function (index, value) {
        let temp = $(this).find('.pw');
        $(this).find('i').click(function () {
            if ($(this).hasClass('fa-eye-slash')) {
                $(this).removeClass('fa-eye-slash');
                $(this).addClass('fa-eye');
                temp.attr('type', 'text');
            }
            else {
                $(this).removeClass('fa-eye');
                $(this).addClass('fa-eye-slash');
                temp.attr('type', 'password');
            }
        });
    });

    // Product slider
    $('#autoWidth').lightSlider({
        autoWidth: true,
        loop: true,
        onSliderLoad: function () {
            $('#autoWidth').removeClass('cs-hidden');
        }
    });
    $('#equipment-s').lightSlider({
        autoWidth: true,
        loop: true,
        onSliderLoad: function () {
            $('#autoWidth').removeClass('cs-hidden');
        }
    });

    $('#imageGallery').lightSlider({
        gallery: true,
        item: 1,
        loop: true,
        thumbItem: 9,
        slideMargin: 0,
        enableDrag: false,
        currentPagerPosition: 'left',
        onSliderLoad: function (el) {
            el.lightGallery({
                selector: '#imageGallery .lslide'
            });
        }
    });


    $('input.input-qty').each(function () {
        var $this = $(this),
            qty = $this.parent().find('.is-form'),
            min = Number($this.attr('min')),
            max = Number($this.attr('max'))
        if (min == 0) {
            var d = 0
        } else d = min
        $(qty).on('click', function () {
            if ($(this).hasClass('minus')) {
                if (d > min) d += -1
            } else if ($(this).hasClass('plus')) {
                var x = Number($this.val()) + 1
                if (x <= max) d += 1
            }
            $this.attr('value', d).val(d)
        })
    })
});

$('.desc-content').html($('.desc-content').text());