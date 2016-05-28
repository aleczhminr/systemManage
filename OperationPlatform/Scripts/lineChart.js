(function($) {

    'use strict';

    $(document).ready(function() {

        /* ============================================================
         * NVD3 Charts
         * ============================================================ */

        // Load chart data
        d3.json('http://revox.io/json/charts.json', function(data) {

            // date
            (function() {
                nv.addGraph(function() {
                    var chart = nv.models.lineChart()
                        .x(function(d) {
                            return d[0]
                        })
                        .y(function(d) {
                            return d[1] / 100
                        })
                        .color([
                            $.Pages.getColor('complete'),
                            $.Pages.getColor('primary'),
                        ])
                        .useInteractiveGuideline(true);

                    chart.xAxis
                        .tickFormat(function(d) {
                            return d3.time.format('%x')(new Date(d))
                        });

                    chart.yAxis.tickFormat(d3.format(',.2f'));

                    d3.select('#nvd3-date svg')
                        .datum(data.nvd3.line)
                        .transition().duration(500)
                        .call(chart);

                    nv.utils.windowResize(chart.update);

                    $('#nvd3-date').data('chart', chart);

                    return chart;
                });

                // reg
                nv.addGraph(function() {
                    var chart = nv.models.lineChart()
                        .x(function(d) {
                            return d[0]
                        })
                        .y(function(d) {
                            return d[1]
                        })
                        .color([
                            $.Pages.getColor('complete'),
                            $.Pages.getColor('warning'),
                        ])
                        .useInteractiveGuideline(true);

                    chart.xAxis
                        .tickFormat(function(d) {
                            return d3.time.format('%x')(new Date(d))
                        });

                    chart.yAxis.tickFormat(d3.format(',.2f'));

                    d3.select('#nvd3-reg svg')
                        .datum(data.nvd3.line)
                        .transition().duration(500)
                        .call(chart);

                    nv.utils.windowResize(chart.update);

                    $('#nvd3-reg').data('chart', chart);

                    return chart;
                });

                // sales
                nv.addGraph(function() {
                    var chart = nv.models.lineChart()
                        .x(function(d) {
                            return d[0]
                        })
                        .y(function(d) {
                            return d[1] / 100
                        })
                        .color([
                            $.Pages.getColor('complete'),
                            $.Pages.getColor('warning'),
                        ])
                        .useInteractiveGuideline(true);

                    chart.xAxis
                        .tickFormat(function(d) {
                            return d3.time.format('%x')(new Date(d))
                        });

                    chart.yAxis.tickFormat(d3.format(',.2f'));

                    d3.select('#nvd3-sales svg')
                        .datum(data.nvd3.line)
                        .transition().duration(500)
                        .call(chart);

                    nv.utils.windowResize(chart.update);

                    $('#nvd3-sales').data('chart', chart);

                    return chart;
                });

                // mem
                nv.addGraph(function() {
                    var chart = nv.models.lineChart()
                        .x(function(d) {
                            return d[0]
                        })
                        .y(function(d) {
                            return d[1] / 100
                        })
                        .color([
                            $.Pages.getColor('complete'),
                            $.Pages.getColor('warning'),
                        ])
                        .useInteractiveGuideline(true);

                    chart.xAxis
                        .tickFormat(function(d) {
                            return d3.time.format('%x')(new Date(d))
                        });

                    chart.yAxis.tickFormat(d3.format(',.2f'));

                    d3.select('#nvd3-mem svg')
                        .datum(data.nvd3.line)
                        .transition().duration(500)
                        .call(chart);

                    nv.utils.windowResize(chart.update);

                    $('#nvd3-mem').data('chart', chart);

                    return chart;
                });

                // sku
                nv.addGraph(function() {
                    var chart = nv.models.lineChart()
                        .x(function(d) {
                            return d[0]
                        })
                        .y(function(d) {
                            return d[1] / 100
                        })
                        .color([
                            $.Pages.getColor('success'),
                            $.Pages.getColor('danger'),
                        ])
                        .useInteractiveGuideline(true);

                    chart.xAxis
                        .tickFormat(function(d) {
                            return d3.time.format('%x')(new Date(d))
                        });

                    chart.yAxis.tickFormat(d3.format(',.2f'));

                    d3.select('#nvd3-sku svg')
                        .datum(data.nvd3.line)
                        .transition().duration(500)
                        .call(chart);

                    nv.utils.windowResize(chart.update);

                    $('#nvd3-sku').data('chart', chart);

                    return chart;
                });

                // sms
                nv.addGraph(function() {
                    var chart = nv.models.lineChart()
                        .x(function(d) {
                            return d[0]
                        })
                        .y(function(d) {
                            return d[1] / 100
                        })
                        .color([
                            $.Pages.getColor('danger'),
                            $.Pages.getColor('master'),
                        ])
                        .useInteractiveGuideline(true);

                    chart.xAxis
                        .tickFormat(function(d) {
                            return d3.time.format('%x')(new Date(d))
                        });

                    chart.yAxis.tickFormat(d3.format(',.2f'));

                    d3.select('#nvd3-sms svg')
                        .datum(data.nvd3.line)
                        .transition().duration(500)
                        .call(chart);

                    nv.utils.windowResize(chart.update);

                    $('#nvd3-sms').data('chart', chart);

                    return chart;
                });

                // orders
                nv.addGraph(function() {
                    var chart = nv.models.lineChart()
                        .x(function(d) {
                            return d[0]
                        })
                        .y(function(d) {
                            return d[1] / 100
                        })
                        .color([
                            $.Pages.getColor('success'),
                            $.Pages.getColor('warning'),
                        ])
                        .useInteractiveGuideline(true);

                    chart.xAxis
                        .tickFormat(function(d) {
                            return d3.time.format('%x')(new Date(d))
                        });

                    chart.yAxis.tickFormat(d3.format(',.2f'));

                    d3.select('#nvd3-orders svg')
                        .datum(data.nvd3.line)
                        .transition().duration(500)
                        .call(chart);

                    nv.utils.windowResize(chart.update);

                    $('#nvd3-orders').data('chart', chart);

                    return chart;
                });
            })();
        });

        $('#tabs-nvd3 a[data-toggle="tab"]').on('shown.bs.tab', function(e) {
            var target = $(e.target).attr('href'),
                tabPane = $(target),
                chart = tabPane.find('.line-chart').data('chart');
            chart.update();

            setTimeout(function() {
                d3.selectAll('#nvd3-date .nvd3 circle.nv-point').attr("r", "4");
                d3.selectAll('#nvd3-reg .nvd3 circle.nv-point').attr("r", "4");
                d3.selectAll('#nvd3-sales .nvd3 circle.nv-point').attr("r", "4");
                d3.selectAll('#nvd3-mem .nvd3 circle.nv-point').attr("r", "4");
                d3.selectAll('#nvd3-sku .nvd3 circle.nv-point').attr("r", "4");
                d3.selectAll('#nvd3-sms .nvd3 circle.nv-point').attr("r", "4");
                d3.selectAll('#nvd3-orders .nvd3 circle.nv-point').attr("r", "4");
            }, 300);
        });

    });
})(window.jQuery);