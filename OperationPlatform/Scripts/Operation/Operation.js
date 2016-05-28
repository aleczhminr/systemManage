var loginNum = [];
var activeNum = [];

function GetTendencyJson() {
    var url = "/Operation/TendencyJson";
    var postJson = {};

    var timeType = $(".tendencyWhere .btn-group .btn.btn-select").val();
    if (timeType == "other") {
        var statTime = $(".tendencyWhere .page-where-othertime input[name='start']").val();
        var endTime = $(".tendencyWhere .page-where-othertime input[name='end']").val();
        if (statTime.length > 0) {
            postJson["startTime"] = statTime;
        }
        if (endTime.length > 0) {
            postJson["endTime"] = endTime;
        }
    } else {
        var timeValue = $.timePeriod(0 - Number(timeType));
        postJson["startTime"] = timeValue.start.toLocaleDateString();
        postJson["endTime"] = timeValue.end.toLocaleDateString();
    }


    var columnList = $(".tendencyWhere .checkbox input:checkbox:checked");
    var column = new Array();
    $.each(columnList, function (i, n) {
        column.push(n.value);
    });
    if (column.length > 0) {
        postJson["columns"] = column.join(",");
        $.doAjax(url, postJson, function (msg) {

            if (msg != "") {

                var json = jQuery.parseJSON(msg);
                var XLable = new Array();
                var tbData = {};

                var series = new Array();

                option.title.text = json.captionTitle;
                $.each(json.DataList, function (i, n) {

                    XLable.push(i);

                    $.each(n.ItemList, function (itemI, itemN) {
                        if (itemI == "loginNum") {
                            loginNum.push(
                            {
                                "date": i,
                                "value": Number(itemN.Values)
                            });
                        } else if (itemI == "activeNum") {
                            activeNum.push(
                            {
                                "date": i,
                                "value": Number(itemN.Values)
                            });
                        } else {
                            var dataItemValue = {};
                            //if (itemI == "acc_Rep") {
                            //    if (n.weekend == 6 || n.weekend == 0) {
                            //        dataItemValue = {
                            //            value: Number(parseFloat(itemN.Values / n.ItemList.loginNum.Values).toFixed(2)),
                            //            symbol: 'emptycircle',
                            //            smooth: true,
                            //            symbolSize: 2,
                            //            itemStyle: {
                            //                normal: {
                            //                    color: 'red'
                            //                }
                            //            },
                            //            showAllSymbol: true
                            //        };
                            //    } else {
                            //        dataItemValue["value"] = Number(parseFloat(itemN.Values / n.ItemList.loginNum.Values).toFixed(2));
                            //    }
                            //} else {
                                if (n.weekend == 6 || n.weekend == 0) {
                                    dataItemValue = {
                                        value: Number(itemN.Values),
                                        symbol: 'emptycircle',
                                        smooth: true,
                                        symbolSize: 2,
                                        itemStyle: {
                                            normal: {
                                                color: 'red'
                                            }
                                        },
                                        showAllSymbol: true
                                    };
                                } else {
                                    dataItemValue["value"] = Number(itemN.Values);
                                }
                            //}
                            

                            if (!tbData.hasOwnProperty(itemN.series)) {
                                tbData[itemN.series] = new Array();
                            }
                            tbData[itemN.series].push(dataItemValue);
                        }
                        
                    });

                });
                var legend = new Array();
                $.each(tbData, function(i, n) {
                    var seriesItem = {
                        name: i + "数量",
                        type: 'line',
                        symbol: 'Circle',
                        smooth: true,
                        data: n,
                        markLine: { data: [{ type: "average", name: "平均值" }] }
                    }
                    series.push(seriesItem);
                    legend.push(seriesItem.name);
                });

                option.xAxis[0].data = XLable;
                option.legend.data = legend;
                option.series = series;
                myChart.clear().setOption(option);
            }

        }, true);
    }
}

//获取数据对比汇总信息
function GetSummary() {

    //订单分端（网站、Android、IOS对比）
    var column = new Array();
    var checkKey = $("#sectionCbx").attr("checked");
    if (checkKey=="checked") {
        GetSectionSummary();
        return;
    }
    var url = "/Operation/MonthSummary";
    var postJson = {};

    var timeType = $(".monthcomparisonWhere .btn-group .btn.btn-select").val();

    if (timeType == "other") {
        timeType = 0;
    }

    postJson["timeType"] = timeType;
    postJson["startMonth"] = $(".monthcomparisonWhere .page-where-othertime input[name='start']").val();
    postJson["endMonth"] = $(".monthcomparisonWhere .page-where-othertime input[name='end']").val();

    var dataType = $(".monthcomparisonWhere .radio input:radio:checked").val();
    postJson["dataType"] = dataType;
    $.doAjax(url, postJson, function (msg) {
        if (msg != "") {
            var json = jQuery.parseJSON(msg);
            var html = "";

            $.each(json, function (i, n) {
                html += "<span class='fs-12 hint-text'>" + n.Name + "总数：</span>";
                //if (n.FormerData !== 0) {
                //    var minusAnd = n.AfterData - n.FormerData;
                //    var perAnd = (minusAnd / n.FormerData) * 100;
                //    if (perAnd<0) {
                //        html += "<span class='text-success fs-12 m-r-10'>" + changeTwoDecimal(perAnd) + "%</span> ";
                //    } else {
                //        html += "<span class='text-danger fs-14 m-r-10'>" + changeTwoDecimal(perAnd) + "%</span> ";
                //    }
                //} else {
                    //html += "<span class='text-warning fs-12 m-r-10 bold'>-</span>";
                //}
                html += "<span class='text-success fs-12 m-r-14'>" + n.AfterData + "</span> ";
            });

            $("#summary").html(html);
        }
    }, true);
}


//获取会员、销售、商品、订单、支出按端分类（网站、Android、IOS）数据对比汇总信息
function GetSectionSummary() {
    var url = "/Operation/SectionMonthSummary";
    var postJson = {};

    var timeType = $(".monthcomparisonWhere .btn-group .btn.btn-select").val();

    if (timeType == "other") {
        timeType = 0;
    }

    postJson["timeType"] = timeType;
    postJson["startMonth"] = $(".monthcomparisonWhere .page-where-othertime input[name='start']").val();
    postJson["endMonth"] = $(".monthcomparisonWhere .page-where-othertime input[name='end']").val();

    var dataType = $(".monthcomparisonWhere .radio input:radio:checked").val();
    var sourceType = $("#orderCategoryCbx input:checkbox:checked").val();
    postJson["dataType"] = dataType;
    postJson["sourceType"] = sourceType;
    $.doAjax(url, postJson, function (msg) {
        if (msg != "") {
            var json = jQuery.parseJSON(msg);
            var html = "";

            $.each(json, function (i, n) {
                html += "<span class='fs-12 hint-text'>" + n.Name + "总数：</span>";
                html += "<span class='text-success fs-12 m-r-14'>" + n.AfterData + "</span> ";
            });
            $("#summary").html(html);
        }
    }, true);
}


//获取平台趋势对比信息
function GetTendencySummary() {
    var url = "/Operation/TendencySummary";
    var postJson = {};

    var timeType = $(".tendencyWhere .btn-group .btn.btn-select").val();
    if (timeType == "other") {
        var statTime = $(".tendencyWhere .page-where-othertime input[name='start']").val();
        var endTime = $(".tendencyWhere .page-where-othertime input[name='end']").val();
        if (statTime.length > 0) {
            postJson["startTime"] = statTime;
        }
        if (endTime.length > 0) {
            postJson["endTime"] = endTime;
        }
    } else {
        var timeValue = $.timePeriod(0 - Number(timeType));
        postJson["startTime"] = timeValue.start.toLocaleDateString();
        postJson["endTime"] = timeValue.end.toLocaleDateString();
    }

    var columnList = $(".tendencyWhere .checkbox input:checkbox:checked");
    var column = new Array();
    $.each(columnList, function (i, n) {
        column.push(n.value);
    });
    if (column.length > 0) {
        postJson["columns"] = column.join(",");
        $.doAjax(url, postJson, function (msg) {

            if (msg != "") {

                var json = jQuery.parseJSON(msg);
                var html = "";

                $.each(json, function (i, n) {
                    if (n.ColName.indexOf("用户")>0) {
                        
                    } else {
                        html += "<span class='fs-12 hint-text'>" + n.ColName + "：</span>";
                        if (parseInt(n.Ratio)>0) {
                            html += "<span class='text-success fs-12 m-r-14'>" + n.NowNum + "(<span class='text-danger fs-12 m-r-14'>" + n.Ratio + "%</span>)</span> ";
                        } else if (n.Ratio!="-999") {
                            html += "<span class='text-success fs-12 m-r-14'>" + n.NowNum + "(" + n.Ratio + "%)</span> ";
                        } else {
                            html += "<span class='text-success fs-12 m-r-14'>" + n.NowNum + "(暂无周增长数据)</span> ";
                        }
                        
                    }
                    
                });

                $("#summaryTendency").html(html);
            }

        }, true);
    }
}

function GetActiveSummary() {
    var url = "/Operation/ActiveSummary";
    var postJson = {};

    var timeType = $(".activeanalysisWhere .btn-group .btn.btn-select").val();
    if (timeType == "other") {
        var statTime = $(".activeanalysisWhere .page-where-othertime input[name='start']").val();
        var endTime = $(".activeanalysisWhere .page-where-othertime input[name='end']").val();
        if (statTime.length > 0) {
            postJson["startTime"] = statTime;
        }
        if (endTime.length > 0) {
            postJson["endTime"] = endTime;
        }
    } else {
        var timeValue = $.timePeriod(0 - Number(timeType));
        postJson["startTime"] = timeValue.start.toLocaleDateString();
        postJson["endTime"] = timeValue.end.toLocaleDateString();
    }

    var columnList = $(".activeanalysisWhere .checkbox input:checkbox:checked");
    var column = new Array();
    $.each(columnList, function (i, n) {
        column.push(n.value);
    });
    if (column.length > 0) {
        postJson["columns"] = column.join(",");
        $.doAjax(url, postJson, function (msg) {

            if (msg != "") {

                var json = jQuery.parseJSON(msg);
                var html = "";

                $.each(json, function (i, n) {

                    html += "<span class='fs-12 hint-text'>" + n.ColName + "：</span>";
                    if (parseInt(n.Ratio) >= 0) {
                        html += "<span class='text-danger fs-12 m-r-14'>" + n.Ratio + "%</span> ";
                    } else if (n.Ratio!="-999") {
                        html += "<span class='text-success fs-12 m-r-14'>" + n.Ratio + "%</span> ";
                    } else {
                        html += "<span class='text-success fs-12 m-r-14'>暂无周增长数据</span> ";
                    }
                });

                $("#summaryActive").html(html);
            }

        }, true);
    }
}

function GetMonthComparisonJson() {
    var url = "/Operation/MonthComparisonJson";
    var postJson = {};

    var timeType = $(".monthcomparisonWhere .btn-group .btn.btn-select").val();

    if (timeType == "other") {
        timeType = 0;
    }

    //var source = $("#source").val();

    //postJson["source"] = source;
    postJson["timeType"] = timeType;
    postJson["startMonth"] = $(".monthcomparisonWhere .page-where-othertime input[name='start']").val();
    postJson["endMonth"] = $(".monthcomparisonWhere .page-where-othertime input[name='end']").val();


    var dataType = $(".monthcomparisonWhere .radio input:radio:checked").val();
    var sectionResult = false;
    if (dataType != "shop" || dataType != "sms") {
       sectionResult = $("#sectionCbx").attr("checked");
    }
    postJson["sourceType"] = sectionResult;
    postJson["dataType"] = dataType;
    $.doAjax(url, postJson, function (msg) {

        if (msg != "") {

            var json = jQuery.parseJSON(msg);
            var XLable = new Array();
            var tbData = {};

            var series = new Array();

            option.title.text = json.captionTitle;
            $.each(json.DataList, function (i, n) {

                XLable.push(i);


                $.each(n.ItemList, function (itemI, itemN) {
                    if (!tbData.hasOwnProperty(itemN.series)) {
                        tbData[itemN.series] = new Array();
                    }
                    tbData[itemN.series].push(Number(itemN.Values));
                });

                //if (column.length <= 0) {

                if (n.thisMonth == 1) {
                    var nowDate = new Date(json.NowYear, (json.NowMonth - 1), 1);
                    var nextMonth = new Date(json.NowYear, (json.NowMonth), 1);
                    var MonthDayLength = (nextMonth - nowDate) / (1000 * 60 * 60 * 24);

                    var nowDay = json.NowDay;
                    if (nowDay > 1) {
                        nowDay = nowDay - 1;
                    }

                    //获取当前时间在一天时间内的占比
                    var myDate = new Date();
                    var hour = myDate.getHours();

                    var dayCnt = parseFloat(hour) / 24;

                    $.each(n.ItemList, function (itemI, itemN) {
                        var rVP = parseInt(Number(itemN.Values) / (nowDay + dayCnt) * MonthDayLength);
                        if (!tbData.hasOwnProperty(itemN.series)) {
                            tbData[itemN.series] = new Array();
                        }
                        tbData[itemN.series].push(rVP);
                    });

                    XLable.push("本月预测");
                 }
            });
            var legend = new Array();
            $.each(tbData, function (i, n) {
                // if (column.length > 0) {
                //   var  seriesItem = {};
                //    if (i=="网站") {
                //         seriesItem = {
                //            name: i,
                //            type: 'bar',
                //            stack: '网站查询分析',
                //            data: n
                //        }
                //    } else if (i == "iPhone") {
                //         seriesItem = {
                //            name: i,
                //            type: 'bar',
                //            stack: 'iPhone查询分析',
                //            data: n
                //        }
                //    }
                //    else if (i=="Android") {
                //         seriesItem = {
                //            name: i,
                //            type: 'bar',
                //            stack: 'Android查询分析',
                //            data: n
                //        }
                //    }
                //} else {
                //     seriesItem = {
                //        name: i,
                //        type: 'bar',
                //        stack: '查询分析',
                //        data: n
                //    }
                //}
                var seriesItem = {
                    name: i,
                    type: 'bar',
                    stack: '查询分析',
                    data: n
                }
                if (i == "旧用户" || i == "网站") {
                    seriesItem["itemStyle"] = {
                        normal: { color: "#87cefa", areaStyle: { type: 'default' } }
                    };
                } else if(i=="iPhone"){
                    seriesItem["itemStyle"] = {
                        normal: {
                            color: "#ff7f50",
                            areaStyle: { type: 'default' }
                        }
                    };
                }
                else if (i == "iPad") {
                    seriesItem["itemStyle"] = {
                        normal: {
                            color: "#CD2626",
                            areaStyle: { type: 'default' }
                        }
                    };
                }
                else {
                    seriesItem["itemStyle"] = {
                        normal: {
                            color: '#4a86e8',
                            areaStyle: { type: 'default' },
                            label: {
                                show: true,
                                position: 'top',
                                formatter: function (a, b, c) {
                                    //for (var i = 0, l = option.xAxis[0].data.length; i < l; i++) {
                                    //    if (option.xAxis[0].data[i] == b) {
                                    //        return option.series[0].data[i] + c;
                                    //    }
                                    //}
                                    for (var i = 0, l = option.xAxis[0].data.length; i < l; i++) {
                                        if (option.xAxis[0].data[i] == b) {
                                            var all = 0;
                                            for (var k = 0; k < option.series.length; k++) {
                                                all += option.series[k].data[i];
                                            }
                                            return parseInt(all);
                                        }
                                    }
                                },

                                textStyle: { color: '#000' }
                            }
                        }
                    };
                }

                series.push(seriesItem);
                legend.push(seriesItem.name);
            });

            option.xAxis[0].data = XLable;
            option.legend.data = legend;
            option.series = series;
   
            myChart.clear().setOption(option);
        }

    }, true);
}

function changeTwoDecimal(x) {
    var f_x = parseFloat(x);
    if (isNaN(f_x)) {
        return x;
    }
    var f_x = Math.round(x * 100) / 100;

    return f_x;
}
function GetRegTimeReportJson() {
    var url = "/RegTimeReport/RegisterSourceJson";
    var postJson = {};
    var timeType = $(".regTimeReportSourceWhere .btn-group .btn.btn-select").val();


    var columnList = $(".regTimeReportSourceWhere .checkbox input:checkbox:checked");
    var column = new Array();
    $.each(columnList, function (i, n) {
        column.push(n.value);
    });
    postJson["timeType"] = timeType;
    if (column.length > 0) {
        postJson["type"] = column.join(",");
        $.doAjax(url, postJson, function (msg) {

            if (msg != "null" && msg != "[]") {
                var json = jQuery.parseJSON(msg);
                console.log(json);
                var XLable = new Array();
                var tbData = new Array();

                for (var i = 0; i < 5; i++) {
                    tbData[i] = new Array();
                }

                if (option.series.length < json.count) {

                    for (var i = 0; i < json.count - option.series.length; i++) {
                        var item = {
                            name: "数量" + i.toString(),
                            type: 'line',
                            symbol: 'Circle',
                            data: [0]
                        }

                        option.series.push(item);
                    }
                }
                else if (option.series.length > json.count) {
                    option.series.length = json.count;
                    option.legend.data.length = json.count;
                }

                var cnt = 0;
                $.each(json.DataList, function (i, n) {

                    for (var k = 0; k <24; k++) {
                        tbData[cnt].push(n.ItemList[k].num);
                    }
                    option.series[cnt].data = tbData[cnt];

                    switch (n.type) {
                        case "loginnum":
                            option.legend.data[cnt] = "用户登录数";
                            option.series[cnt].name = "用户登录数";
                            break;
                        case "salenum":
                            option.legend.data[cnt] = "销售笔数";
                            option.series[cnt].name = "销售笔数";
                            break;
                        case "regnum":
                            option.legend.data[cnt] = "注册用户数";
                            option.series[cnt].name = "注册用户数";
                            break;
                        case "clientnum":
                            option.legend.data[cnt] = "客户端登录数";
                            option.series[cnt].name = "客户端登录数";
                            break;
                        default:
                            break;
                    }
                    cnt++;

                });
                myChart.clear().setOption(option);

            }
        }, true);
    }
}
function GetRegisterSourceJson() {
    var url = "/Operation/RegisterSourceJson";
    var postJson = {};

    var timeType = $(".registerSourceWhere .btn-group .btn.btn-select").val();
    if (timeType == "other") {
        var statTime = $(".registerSourceWhere .page-where-othertime input[name='start']").val();
        var endTime = $(".registerSourceWhere .page-where-othertime input[name='end']").val();
        if (statTime.length > 0) {
            postJson["startTime"] = statTime;
        }
        if (endTime.length > 0) {
            postJson["endTime"] = endTime;
        }
    } else {
        var timeValue = $.timePeriod(0 - Number(timeType));
        postJson["startTime"] = timeValue.start.toLocaleDateString();
        postJson["endTime"] = timeValue.end.toLocaleDateString();
    }


    postJson["dataType"] = "regtime";

    var columnList = $(".registerSourceWhere .checkbox input:checkbox:checked");
    var column = new Array();
    $.each(columnList, function (i, n) {
        column.push(n.value);
    });

    if (column.length > 0) {
        postJson["source"] = column.join(",");
        $.doAjax(url, postJson, function (msg) {

            if (msg != "null" && msg != "[]") {
                var json = jQuery.parseJSON(msg);
                $("#registerSummary").html("");
                var count = 0;
                var seriesData =
                {
                    "iPhone": 0,
                    "Android": 0,
                    "PC": 0,
                    "SEM": 0,
                    "WEB": 0,
                    "iPad": 0
                };

                $.each(json.data, function (i, n) {
                    if ('IOS' in n.itemList) {
                        seriesData["iPhone"] += parseInt(n.itemList["IOS"].CountValue);
                    }
                    if ('Android' in n.itemList) {
                        seriesData["Android"] += parseInt(n.itemList["Android"].CountValue);
                        
                    }
                    if ('PC' in n.itemList) {
                        seriesData["PC"] += parseInt(n.itemList["PC"].CountValue);
                        
                    }
                    if ('SEM' in n.itemList) {
                        seriesData["SEM"] += parseInt(n.itemList["SEM"].CountValue);
                    }
                    if ('WEB' in n.itemList) {
                        seriesData["WEB"] += parseInt(n.itemList["WEB"].CountValue);
                        
                    }
                    if ('iPad' in n.itemList) {
                        seriesData["iPad"] += parseInt(n.itemList["iPad"].CountValue);

                    }
                    count++;
                });

                var perIOS = 0;
                var perAndroid = 0;
                var perPC = 0;
                var perSEM = 0;
                var perWEB = 0;
                var periPad = 0;

                var allData = seriesData["iPhone"] + seriesData["Android"] + seriesData["PC"] + seriesData["SEM"] + seriesData["WEB"] + seriesData["iPad"];
                var html = "";

                if (allData !== 0) {
                    perIOS = seriesData["iPhone"] * 100 / allData;
                    perAndroid = seriesData["Android"] * 100 / allData;
                    perPC = seriesData["PC"] * 100 / allData;
                    perSEM = seriesData["SEM"] * 100 / allData;
                    perWEB = seriesData["WEB"] * 100 / allData;
                    periPad = seriesData["iPad"] * 100 / allData;
                }

                html += "<span class='fs-12 hint-text'>iPhone：</span>";
                if (perIOS < 20) {
                    html += "<span class='text-success fs-12 m-r-10'>" + seriesData["iPhone"] + "(" + changeTwoDecimal(perIOS) + "%)</span> ";
                } else {
                    html += "<span class='text-danger fs-14 m-r-10'>" + seriesData["iPhone"] + "(" + changeTwoDecimal(perIOS) + "%)</span> ";
                }

                html += "<span class='fs-12 hint-text'>Android：</span>";
                if (perAndroid < 20) {
                    html += "<span class='text-success fs-12 m-r-10'>" + seriesData["Android"] + "(" + changeTwoDecimal(perAndroid) + "%)</span> ";
                } else {
                    html += "<span class='text-danger fs-14 m-r-10'>" + seriesData["Android"] + "(" + changeTwoDecimal(perAndroid) + "%)</span> ";
                }

                html += "<span class='fs-12 hint-text'>PC：</span>";
                if (perPC < 20) {
                    html += "<span class='text-success fs-12 m-r-10'>" + seriesData["PC"] + "(" + changeTwoDecimal(perPC) + "%)</span> ";
                } else {
                    html += "<span class='text-danger fs-14 m-r-10'>" + seriesData["PC"] + "(" + changeTwoDecimal(perPC) + "%)</span> ";
                }

                html += "<span class='fs-12 hint-text'>SEM：</span>";
                if (perSEM < 20) {
                    html += "<span class='text-success fs-12 m-r-10'>" + seriesData["SEM"] + "(" + changeTwoDecimal(perSEM) + "%)</span> ";
                } else {
                    html += "<span class='text-danger fs-14 m-r-10'>" + seriesData["SEM"] + "(" + changeTwoDecimal(perSEM) + "%)</span> ";
                }

                html += "<span class='fs-12 hint-text'>WEB：</span>";
                if (perWEB < 20) {
                    html += "<span class='text-success fs-12 m-r-10'>" + seriesData["WEB"] + "(" + changeTwoDecimal(perWEB) + "%)</span> ";
                } else {
                    html += "<span class='text-danger fs-14 m-r-10'>" + seriesData["WEB"] + "(" + changeTwoDecimal(perWEB) + "%)</span> ";
                }

                html += "<span class='fs-12 hint-text'>iPad：</span>";
                if (periPad < 20) {
                    html += "<span class='text-success fs-12 m-r-10'>" + seriesData["iPad"] + "(" + changeTwoDecimal(periPad) + "%)</span> ";
                } else {
                    html += "<span class='text-danger fs-14 m-r-10'>" + seriesData["iPad"] + "(" + changeTwoDecimal(periPad) + "%)</span> ";
                }

                $("#registerSummary").html(html);

                var XLable = new Array();
                var tbData = {};

                var series = new Array();

                option.title.text = json.captionTitle;
                $.each(json.data, function (i, n) {

                    XLable.push(n.datetime);

                    $.each(n.itemList, function (itemI, itemN) {
                        var dataItemValue = {};
                        if (itemN.weekend == "1" ) {
                            dataItemValue = {
                                value: Number(itemN.CountValue),
                                symbol: 'emptycircle',
                                symbolSize: 2,
                                smooth: true,
                                itemStyle: {
                                    normal: {
                                        color: 'red'
                                    }
                                },
                                showAllSymbol: true
                            };
                        } else {
                            dataItemValue["value"] = Number(itemN.CountValue);
                        }

                        if (!tbData.hasOwnProperty(itemI)) {
                            tbData[itemI] = new Array();
                        }
                        tbData[itemI].push(dataItemValue);
                    });

                });
                var legend = new Array();

                var dataName = {
                    "Android": "安卓",
                    "IOS": "iPhone",
                    "PC": "客户端",
                    "SEM": "SEM",
                    "WEB": "网站",
                    "iPad": "iPad"
                    //"tAndroid": "安卓登录",
                    //"tIOS": "IOS登录",
                    //"tPC": "客户端登录",
                    //"tWEB": "网站登录"
                };

                $.each(tbData, function(i, n) {
                    var seriesItem = {
                        name: dataName[i] + "数量",
                        type: 'line',
                        symbol: 'circle',
                        smooth: true,
                        data: n,
                        markLine: { data: [{ type: "average", name: "平均值" }] }
                    }
                    series.push(seriesItem);
                    legend.push(seriesItem.name);
                });

                option.xAxis[0].data = XLable;
                option.legend.data = legend;
                option.series = series;
                myChart.clear().setOption(option);
  
            }
        }, true);
    }
}

function GetRegisterDetailSourceJson() {
    var url = "/Operation/RegisterSourceJson";
    var postJson = {};

    var timeType = $(".registerSourceWhere .btn-group .btn.btn-select").val();
    if (timeType == "other") {
        var statTime = $(".registerSourceWhere .page-where-othertime input[name='start']").val();
        var endTime = $(".registerSourceWhere .page-where-othertime input[name='end']").val();
        if (statTime.length > 0) {
            postJson["startTime"] = statTime;
        }
        if (endTime.length > 0) {
            postJson["endTime"] = endTime;
        }
    } else {
        var timeValue = $.timePeriod(0 - Number(timeType));
        postJson["startTime"] = timeValue.start.toLocaleDateString();
        postJson["endTime"] = timeValue.end.toLocaleDateString();
    }


    postJson["dataType"] = "detailSource";

    var columnList = $(".registerSourceWhere .checkbox input:checkbox:checked");
    var column = new Array();
    $.each(columnList, function (i, n) {
        column.push(n.value);
    });

    if (column.length > 0) {
        postJson["source"] = column.join(",");
        $.doAjax(url, postJson, function (msg) {

            if (msg != "null" && msg != "[]") {
                var json = jQuery.parseJSON(msg);
                //$("#registerSummary").html("");
                var count = 0;
                var seriesData =
                {
                    "xiaomi": 0,
                    "huawei": 0,
                    "360": 0,
                    "baidu": 0,
                    "91_zs": 0,
                    "tengxun": 0,
                    "meizu": 0,
                    "oppo": 0
                };

                $.each(json.data, function (i, n) {
                    if ("'market_xiaomi'" in n.itemList) {                        
                        seriesData["xiaomi"] += parseInt(n.itemList["'market_xiaomi'"].CountValue);
                    }
                    if ("'market_huawei'" in n.itemList) {
                        seriesData["huawei"] += parseInt(n.itemList["'market_huawei'"].CountValue);

                    }
                    if ('market_360' in n.itemList) {
                        seriesData["360"] += parseInt(n.itemList["'market_360'"].CountValue);

                    }
                    if ("'market_baidu'" in n.itemList) {
                        seriesData["baidu"] += parseInt(n.itemList["'market_baidu'"].CountValue);
                    }
                    if ("'market_91zs'" in n.itemList) {
                        seriesData["91_zs"] += parseInt(n.itemList["'market_91zs'"].CountValue);
                    }
                    if ("'market_tengxun'" in n.itemList) {
                        seriesData["tengxun"] += parseInt(n.itemList["'market_tengxun'"].CountValue);
                    }
                    if ("'market_meizu'" in n.itemList) {
                        seriesData["meizu"] += parseInt(n.itemList["'market_meizu'"].CountValue);
                    }
                    if ("'market_oppo'" in n.itemList) {
                        seriesData["oppo"] += parseInt(n.itemList["'market_oppo'"].CountValue);
                    }
                    count++;
                });              

                var XLable = new Array();
                var tbData = {};

                var series = new Array();

                option.title.text = json.captionTitle;
                $.each(json.data, function (i, n) {

                    XLable.push(n.datetime);

                    $.each(n.itemList, function (itemI, itemN) {
                        var dataItemValue = {};
                        if (itemN.weekend == "1") {
                            dataItemValue = {
                                value: Number(itemN.CountValue),
                                symbol: 'emptycircle',
                                symbolSize: 2,
                                smooth: true,
                                itemStyle: {
                                    normal: {
                                        color: 'red'
                                    }
                                },
                                showAllSymbol: true
                            };
                        } else {
                            dataItemValue["value"] = Number(itemN.CountValue);
                        }

                        if (!tbData.hasOwnProperty(itemI)) {
                            tbData[itemI] = new Array();
                        }
                        tbData[itemI].push(dataItemValue);
                    });

                });
                var legend = new Array();

                var dataName = {
                    "'market_xiaomi'": "小米",
                    "'market_huawei'": "华为",
                    "'market_360'": "360",
                    "'market_baidu'": "百度",
                    "'market_91zs'": "91助手",
                    "'market_tengxun'": "腾讯",
                    "'market_meizu'": "魅族",
                    "'market_oppo'": "oppo"
                    //"tAndroid": "安卓登录",
                    //"tIOS": "IOS登录",
                    //"tPC": "客户端登录",
                    //"tWEB": "网站登录"
                };

                $.each(tbData, function (i, n) {
                    var seriesItem = {
                        name: dataName[i] + "数量",
                        type: 'line',
                        symbol: 'circle',
                        smooth: true,
                        data: n,
                        markLine: { data: [{ type: "average", name: "平均值" }] }
                    }
                    series.push(seriesItem);
                    legend.push(seriesItem.name);
                });

                option.xAxis[0].data = XLable;
                option.legend.data = legend;
                option.series = series;
                myChart.clear().setOption(option);

            }
        }, true);
    }
}


function GetActiveAnalysisChart() {
    var url = "/Operation/ActiveAnalysisJson";
    var postJson = {};

    var timeType = $(".activeanalysisWhere .btn-group .btn.btn-select").val();
    if (timeType == "other") {
        var statTime = $(".activeanalysisWhere .page-where-othertime input[name='start']").val();
        var endTime = $(".activeanalysisWhere .page-where-othertime input[name='end']").val();
        if (statTime.length > 0) {
            postJson["startTime"] = statTime;
        }
        if (endTime.length > 0) {
            postJson["endTime"] = endTime;
        }
    } else {
        var timeValue = $.timePeriod(0 - Number(timeType));
        postJson["startTime"] = timeValue.start.toLocaleDateString();
        postJson["endTime"] = timeValue.end.toLocaleDateString();
    }


    var columnList = $(".activeanalysisWhere .checkbox input:checkbox:checked");
    var column = new Array();
    $.each(columnList, function (i, n) {
        column.push(n.value);
    });
    if (column.length > 0) {
        postJson["columns"] = column.join(",");
        $.doAjax(url, postJson, function (msg) {

            if (msg != "") {

                var json = jQuery.parseJSON(msg);
                var XLable = new Array();
                var tbData = {};

                var series = new Array();

                option.title.text = json.captionTitle;
                $.each(json.DataList, function (i, n) {

                    XLable.push(i);

                    $.each(n.ItemList, function (itemI, itemN) {
                        var dataItemValue = {};
                        if (n.weekend == 6 || n.weekend == 0) {
                            dataItemValue = {
                                value: Number(itemN.Values),
                                symbol: 'emptycircle',
                                symbolSize: 2,
                                smooth:true,
                                itemStyle: {
                                    normal: {
                                        color: 'red'
                                    }
                                },
                                showAllSymbol: true
                            };
                        } else {
                            dataItemValue["value"] = Number(itemN.Values);
                        }

                        if (!tbData.hasOwnProperty(itemN.series)) {
                            tbData[itemN.series] = new Array();
                        }
                        tbData[itemN.series].push(dataItemValue);
                    });

                });
                var legend = new Array();
                $.each(tbData, function(i, n) {
                    var seriesItem = {
                        name: i + "数量",
                        type: 'line',
                        symbol: 'Circle',
                        smooth: true,
                        data: n,
                        markLine: { data: [{ type: "average", name: "平均值" }] }
                    }
                    series.push(seriesItem);
                    legend.push(seriesItem.name);
                });

                option.xAxis[0].data = XLable;
                option.legend.data = legend;
                option.series = series;
                myChart.clear().setOption(option);
                ActiveAnlysisButtonClick(1);
            }

        }, true);
    }


}

function GetActiveAnalysisList(page) {
    var url = "/Operation/ActiveAnalysisList";
    var postJson = {};
    postJson["page"] = page;
    var timeType = $(".activeanalysisWhere .btn-group .btn.btn-select").val();
    if (timeType == "other") {
        var statTime = $(".activeanalysisWhere .page-where-othertime input[name='start']").val();
        var endTime = $(".activeanalysisWhere .page-where-othertime input[name='end']").val();
        if (statTime.length > 0) {
            postJson["startTime"] = statTime;
        }
        if (endTime.length > 0) {
            postJson["endTime"] = endTime;
        }
    } else {
        var timeValue = $.timePeriod(0 - Number(timeType));
        postJson["startTime"] = timeValue.start.toLocaleDateString();
        postJson["endTime"] = timeValue.end.toLocaleDateString();
    }

    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#actvieanalysisTableList table tbody").html(template("ActiveAnalysisTableTrScript", json));

            var html = "<tr><td align='center' class='help'>均值</td>";
            var valM = new Array();
            var count = 0;
            var allUsr = 0;
            for (var j = 0; j < 8; j++) {
                valM.push(0);
            }

            $.each(json.listData, function(i, n) {
                valM[0] += n.LoginUsr;
                if (i!=0) {
                    valM[1] += n.RegUsr;
                }                
                valM[2] += n.NewReg;
                valM[3] += n.RegAttention + n.Attention;
                valM[4] += n.ActiveUsr;
                valM[5] += n.FaithUsr;
                valM[6] += n.SleepUsr;
                valM[7] += n.OutUsr;
                count++;
                allUsr += n.AllUsr;
            });

            for (var k = 0; k < 8; k++) {
                if (k == 1) {
                    html += "<td align='center' class='help'>" + changeTwoDecimal(parseInt(valM[k] / (count - 1))) + "</td>";
                } else {
                    html += "<td align='center' class='help'>" + changeTwoDecimal(valM[k] * 100 / allUsr) + "%</td>";
                }
            }
            html += "</tr>";
            $("#actvieanalysisTableList table tbody").prepend(html);

            if (page == 1) {
                $("#ListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#ListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#ListPageHtml").attr("maxpage"), "GetActiveAnalysisList", 5);
            $("#ListPageHtml .dataTables_paginate").html(pageHtml);


        }

    }, true);

}

function ActiveAnlysisButtonClick(type) {
    if (type == 1) {
        $("#ActiveAnlysisButton").show();
        $("#actvieanalysisTableList").hide();
    } else {
        $("#ActiveAnlysisButton").hide();
        $("#actvieanalysisTableList").show();
        GetActiveAnalysisList(1);
    }
}



function GetShopActiveList(page) {
    var url = "/Operation/ShopActiveList";
    var postJson = {};
    postJson = GetShopActiveListPostJson();
    postJson["pageIndex"] = page;

    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#AccountActiveListTable table").replaceWith(template("AccountActiveListScript", json));
            if (page == 1) {
                $("#ListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#ListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#ListPageHtml").attr("maxpage"), "GetShopActiveList", 5);
            $("#ListPageHtml .dataTables_paginate").html(pageHtml);


        }
    }, true);

}


function GetShopActiveListPostJson() {

    var post = {};

    var mainStart = $("#mainStartTime").val();
    var mainEnd = $("#mainEndTime").val();

    post["mainstart"] = mainStart;
    post["mainend"] = mainEnd;


    var selectActive = "";
    var checkList = $("#mainActiveCheckBoxList input:checkbox:checked");

    if (checkList.length > 0) {
        $.each(checkList, function (i, n) {
            if (i > 0) {
                selectActive += ',';
            }
            selectActive += n.value;
        });
    }
    if (selectActive.length > 0) {
        post["mainactive"] = selectActive;
    }

    var isContinue = 0;
    if ($("#isContinue").prop("checked")) {
        isContinue = 1;
    }
    post["maincontinue"] = isContinue;
    return post;
}


function GetGroupShopActiveList(page) {
    var url = "/Operation/GroupShopActiveList";
    var postJson = {};
    postJson = GetGroupShopActiveListPostJson();
    postJson["pageIndex"] = page;

    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#AccountActiveListTable table").replaceWith(template("AccountActiveListScript", json));
            if (page == 1) {
                $("#ListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#ListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#ListPageHtml").attr("maxpage"), "GetShopActiveList", 5);
            $("#ListPageHtml .dataTables_paginate").html(pageHtml);


        }
    }, true);
}


function GetGroupShopActiveListPostJson() {
    var post = {};

    var mainStart = $("#mainStartTime").val();
    var mainEnd = $("#mainEndTime").val();

    post["mainstart"] = mainStart;
    post["mainend"] = mainEnd;


    var selectActive = "";
    var checkList = $("#mainActiveCheckBoxList input:checkbox:checked");

    if (checkList.length > 0) {
        $.each(checkList, function (i, n) {
            if (i > 0) {
                selectActive += ',';
            }
            selectActive += n.value;
        });
    }
    if (selectActive.length > 0) {
        post["mainactive"] = selectActive;
    }

    var isContinue = 0;
    if ($("#isContinue").prop("checked")) {
        isContinue = 1;
    }
    post["maincontinue"] = isContinue;





    post["followstart"] = $("#followStartTime").val();
    post["followend"] = $("#followEndTime").val();



    var isFollowContinue = 0;
    if ($("#isfollowContinue").prop("checked")) {
        isFollowContinue = 1;
    }
    post["followcontinue"] = isFollowContinue;


    var followSelectActive = "";
    var followCheckList = $("#followActiveCheckBoxList input:checkbox:checked");

    if (followCheckList.length > 0) {
        $.each(followCheckList, function (i, n) {
            if (i > 0) {
                followSelectActive += ',';
            }
            followSelectActive += n.value;
        });
    }
    if (followSelectActive.length > 0) {
        post["followactive"] = followSelectActive;
    }





    return post;
}