(function($) {

    'use strict';

    $(document).ready(function() {

        // Show Progress Pie
        $('#gaugePie').append(
            '<div id="gauge" style="width: 90%; height: 100%; position: absolute; top: 0;"></div>',
            '<div id="panel" style="width: 20%; text-align: left; margin-top: 20px; position: absolute; right: 0; top: 0;"></div>'
        );

        var products = [{
            name: '注册',
            count: 413
        }, {
            name: '登录',
            count: 923
        }, {
            name: '收入',
            count: 134
        }, {
            name: '访问',
            count: 33
        }];

        var gauge = $('#gauge').dxBarGauge({
            startValue: 0,
            endValue: 1000,
            label: {
                format: 'fixedPoint',
                precision: 0
            },
            geometry: {
                startAngle: 0,
                endAngle: 360
            },
        }).dxBarGauge('instance');

        $('#panel').append($.map(products, function(product) {
            return $('<div></div>').append(
                '<input type="checkbox" value="' + product.count + '" checked></input>',
                '<span style="margin-left: 10px;">' + product.name + ': ' + product.count + '</span>'
            );
        }));

        var $inputs = $('#panel input').change(handleChange);

        function handleChange() {
            var values = $.map($inputs, function(input) {
                return $(input).prop('checked') ? $(input).val() : null;
            });
            gauge.values(values);
        }

        handleChange();
    });

    // Show Progress bar

})(window.jQuery);