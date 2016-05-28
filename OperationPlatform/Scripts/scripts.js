(function($) {

    'use strict';


    $(document).ready(function() {
        // Initializes search overlay plugin.
        // Replace onSearchSubmit() and onKeyEnter() with 
        // your logic to perform a search and display results
        $(".list-view-wrapper").scrollbar();

        $('[data-pages="search"]').search({
            searchField: '#overlay-search',
            closeButton: '.overlay-close',
            suggestions: '#overlay-suggestions',
            brand: '.brand',
            onSearchSubmit: function(searchString) {
                console.log("Search for: " + searchString);
            },
            onKeyEnter: function(searchString) {
                console.log("Live search for: " + searchString);
                var searchField = $('#overlay-search');
                var searchResults = $('.search-results');

                clearTimeout($.data(this, 'timer'));
                searchResults.fadeOut("fast");
                var wait = setTimeout(function() {

                    searchResults.find('.result-name').each(function() {
                        if (searchField.val().length != 0) {
                            $(this).html(searchField.val());
                            searchResults.fadeIn("fast");
                        }
                    });
                }, 500);
                $(this).data('timer', wait);

            }
        })

    });

    // 菜单栏保持打开状态
    var $sideMenu = $('.sidebar-menu'),
        $pinBtn = $('.sidebar-header-controls>button[data-toggle-pin="sidebar"]');

    if (sessionStorage.pinSwitch == 2) {
        $('body').attr('class','fixed-header mac desktop dashboard windows menu-pin');
    } else {
        $('body').attr('class','fixed-header mac desktop dashboard windows');
    };

    if (sessionStorage.mainMenu) {
        $sideMenu.find('ul.menu-items > li:eq(' + sessionStorage.mainMenu + ')').addClass('open').find('span.arrow').addClass('open').parent().parent().find('.sub-menu').css('display', 'block');
    };

    if (sessionStorage.subMenu) {
        $sideMenu.find('ul.menu-items > li:eq(' + sessionStorage.mainMenu + ')').find('ul.sub-menu > li:eq(' + sessionStorage.subMenu + ')').addClass('open');
    }

    $pinBtn.on('click', function() {
        if ($('.menu-pin').size()) {
            sessionStorage.pinSwitch = 1;
        } else(
            sessionStorage.pinSwitch = 2
        );
    });

    $sideMenu.find('ul > li > a[href="javascript:;"]').on('click', function() {
        sessionStorage.mainMenu = $(this).parent().index();
    });

    $sideMenu.find('ul > li > ul.sub-menu > li > a').on('click', function() {
        sessionStorage.subMenu = $(this).parent().index();

    });

    $('.panel-collapse label').on('click', function(e) {
        e.stopPropagation();
    })

})(window.jQuery);