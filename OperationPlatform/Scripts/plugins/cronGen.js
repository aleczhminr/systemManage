

(function ($) {

    var resultsName = "";
    var inputElement;
    var displayElement;

    $.fn.extend({
        cronGen: function () {
            //create top menu
            var cronContainer = $("<div/>", { id: "CronContainer", style: "display:none;width:450px;height:300px;" });
            var mainDiv = $("<div/>", { id: "CronGenMainDiv", style: "width:500px;height:270px;font-size:12px" });
            var topMenu = $("<ul/>", { "class": "nav nav-tabs", id: "CronGenTabs" });
            $('<li/>', { 'class': 'active' }).html($('<a id="MinutesTab" href="#Minutes">每分钟</a>')).appendTo(topMenu);
            $('<li/>').html($('<a id="HourlyTab" href="#Hourly">每小时</a>')).appendTo(topMenu);
            $('<li/>').html($('<a id="DailyTab" href="#Daily">每天</a>')).appendTo(topMenu);
            $('<li/>').html($('<a id="WeeklyTab" href="#Weekly">每周</a>')).appendTo(topMenu);
            $('<li/>').html($('<a id="MonthlyTab" href="#Monthly">每月</a>')).appendTo(topMenu);
            $('<li/>').html($('<a id="YearlyTab" href="#Yearly">每年</a>')).appendTo(topMenu);
            $(topMenu).appendTo(mainDiv);

            //create what's inside the tabs
            var container = $("<div/>", { "class": "container-fluid", "style": "margin-top: 10px" });
            var row = $("<div/>", { "class": "row-fluid" });
            var span12 = $("<div/>", { "class": "span12" });
            var tabContent = $("<div/>", { "class": "tab-content" });

            //creating the minutesTab
            var minutesTab = $("<div/>", { "class": "tab-pane active", id: "Minutes" });
            $(minutesTab).append("每&nbsp;");
            $("<input/>", { id: "MinutesInput", type: "text", value: "1", style: "width: 40px" }).appendTo(minutesTab);
            $(minutesTab).append("&nbsp;分钟");
            $(minutesTab).appendTo(tabContent);

            //creating the hourlyTab
            var hourlyTab = $("<div/>", { "class": "tab-pane", id: "Hourly" });

            var hourlyOption1 = $("<div/>", { "class": "well well-small" });
            $("<input/>", { type: "radio", value: "1", name: "HourlyRadio", checked: "checked" }).appendTo(hourlyOption1);
            $(hourlyOption1).append("&nbsp;每&nbsp;");
            $("<input/>", { id: "HoursInput", type: "text", value: "1", style: "width: 40px" }).appendTo(hourlyOption1);
            $(hourlyOption1).append("&nbsp;小时");
            $(hourlyOption1).appendTo(hourlyTab);

            var hourlyOption2 = $("<div/>", { "class": "well well-small" });
            $("<input/>", { type: "radio", value: "2", name: "HourlyRadio" }).appendTo(hourlyOption2);
            $(hourlyOption2).append("&nbsp;在&nbsp;");
            $(hourlyOption2).append('<select id="AtHours" class="hours" style="width: 60px"></select>');
            $(hourlyOption2).append('<select id="AtMinutes" class="minutes" style="width: 60px"></select>');
            $(hourlyOption2).appendTo(hourlyTab);

            $(hourlyTab).appendTo(tabContent);

            //craeting the dailyTab
            var dailyTab = $("<div/>", { "class": "tab-pane", id: "Daily" });

            var dailyOption1 = $("<div/>", { "class": "well well-small" });
            $("<input/>", { type: "radio", value: "1", name: "DailyRadio", checked: "checked" }).appendTo(dailyOption1);
            $(dailyOption1).append("&nbsp;每&nbsp;");
            $("<input/>", { id: "DaysInput", type: "text", value: "1", style: "width: 40px" }).appendTo(dailyOption1);
            $(dailyOption1).append("&nbsp;天");
            $(dailyOption1).appendTo(dailyTab);

            var dailyOption2 = $("<div/>", { "class": "well well-small" });
            $("<input/>", { type: "radio", value: "2", name: "DailyRadio" }).appendTo(dailyOption2);
            $(dailyOption2).append("&nbsp;每工作日&nbsp;");
            $(dailyOption2).appendTo(dailyTab);

            $(dailyTab).append("开始时间&nbsp;");
            $(dailyTab).append('<select id="DailyHours" class="hours" style="width: 60px"></select>');
            $(dailyTab).append('<select id="DailyMinutes" class="minutes" style="width: 60px"></select>');

            $(dailyTab).appendTo(tabContent);

            //craeting the weeklyTab
            var weeklyTab = $("<div/>", { "class": "tab-pane", id: "Weekly" });
            var weeklyWell = $("<div/>", { "class": "well well-small" });

            var span31 = $("<div/>", { "class": "span6 col-sm-6" });
            $("<input/>", { type: "checkbox", value: "MON" ,rel:"周一"}).appendTo(span31);
            $(span31).append("&nbsp;周一<br />");
            $("<input/>", { type: "checkbox", value: "WED", rel: "周三" }).appendTo(span31);
            $(span31).append("&nbsp;周三<br />");
            $("<input/>", { type: "checkbox", value: "FRI", rel: "周五" }).appendTo(span31);
            $(span31).append("&nbsp;周五<br />");
            $("<input/>", { type: "checkbox", value: "SUN", rel: "周日" }).appendTo(span31);
            $(span31).append("&nbsp;周日");

            var span32 = $("<div/>", { "class": "span6 col-sm-6" });
            $("<input/>", { type: "checkbox", value: "TUE", rel: "周二" }).appendTo(span32);
            $(span32).append("&nbsp;周二<br />");
            $("<input/>", { type: "checkbox", value: "THU", rel: "周四" }).appendTo(span32);
            $(span32).append("&nbsp;周四<br />");
            $("<input/>", { type: "checkbox", value: "SAT", rel: "周六" }).appendTo(span32);
            $(span32).append("&nbsp;周六");

            $(span31).appendTo(weeklyWell);
            $(span32).appendTo(weeklyWell);
            //Hack to fix the well box
            $("<br /><br /><br /><br />").appendTo(weeklyWell);

            $(weeklyWell).appendTo(weeklyTab);

            $(weeklyTab).append("开始时间&nbsp;");
            $(weeklyTab).append('<select id="WeeklyHours" class="hours" style="width: 60px"></select>');
            $(weeklyTab).append('<select id="WeeklyMinutes" class="minutes" style="width: 60px"></select>');

            $(weeklyTab).appendTo(tabContent);

            //craeting the monthlyTab
            var monthlyTab = $("<div/>", { "class": "tab-pane", id: "Monthly" });

            var monthlyOption1 = $("<div/>", { "class": "well well-small" });
            $("<input/>", { type: "radio", value: "1", name: "MonthlyRadio", checked: "checked" }).appendTo(monthlyOption1);
            $(monthlyOption1).append("&nbsp;每&nbsp");
            $("<input/>", { id: "MonthInput", type: "text", value: "1", style: "width: 40px" }).appendTo(monthlyOption1);
            $(monthlyOption1).append("&nbsp;个月第&nbsp;");
            $("<input/>", { id: "DayOfMOnthInput", type: "text", value: "1", style: "width: 40px" }).appendTo(monthlyOption1);
            $(monthlyOption1).append("&nbsp;天&nbsp;");

            $(monthlyOption1).appendTo(monthlyTab);

            var monthlyOption2 = $("<div/>", { "class": "well well-small" });
            $("<input/>", { type: "radio", value: "2", name: "MonthlyRadio" }).appendTo(monthlyOption2);
            $(monthlyOption2).append("&nbsp;每");
            $("<input/>", { id: "EveryMonthInput", type: "text", value: "1", style: "width: 40px" }).appendTo(monthlyOption2);
            $(monthlyOption2).append("&nbsp;个月");
            $(monthlyOption2).append('<select id="WeekDay" class="day-order-in-month" style="width: 80px"></select>');
            $(monthlyOption2).append('<select id="DayInWeekOrder" class="week-days" style="width: 100px"></select>');

            $(monthlyOption2).appendTo(monthlyTab);

            $(monthlyTab).append("开始时间&nbsp;");
            $(monthlyTab).append('<select id="MonthlyHours" class="hours" style="width: 60px"></select>');
            $(monthlyTab).append('<select id="MonthlyMinutes" class="minutes" style="width: 60px"></select>');

            $(monthlyTab).appendTo(tabContent);

            //craeting the yearlyTab
            var yearlyTab = $("<div/>", { "class": "tab-pane", id: "Yearly" });

            var yearlyOption1 = $("<div/>", { "class": "well well-small" });
            $("<input/>", { type: "radio", value: "1", name: "YearlyRadio", checked: "checked" }).appendTo(yearlyOption1);
            $(yearlyOption1).append("&nbsp;每当&nbsp");
            $(yearlyOption1).append('<select id="MonthsOfYear" class="months" style="width: 150px"></select>');
            $(yearlyOption1).append("&nbsp;第&nbsp;");
            $("<input/>", { id: "YearInput", type: "text", value: "1", style: "width: 40px" }).appendTo(yearlyOption1);
            $(yearlyOption1).append("&nbsp;天&nbsp;");
            $(yearlyOption1).appendTo(yearlyTab);

            var yearlyOption2 = $("<div/>", { "class": "well well-small" });
            $("<input/>", { type: "radio", value: "2", name: "YearlyRadio" }).appendTo(yearlyOption2);
            $(yearlyOption2).append("&nbsp;每当&nbsp;");
            $(yearlyOption2).append('<select id="MonthsOfYear2" class="months" style="width: 110px"></select>');
            $(yearlyOption2).append('<select id="DayOrderInYear" class="day-order-in-month" style="width: 80px"></select>');
            $(yearlyOption2).append('<select id="DayWeekForYear" class="week-days" style="width: 100px"></select>');

            $(yearlyOption2).appendTo(yearlyTab);

            $(yearlyTab).append("开始时间&nbsp;");
            $(yearlyTab).append('<select id="YearlyHours" class="hours" style="width: 60px"></select>');
            $(yearlyTab).append('<select id="YearlyMinutes" class="minutes" style="width: 60px"></select>');

            $(yearlyTab).appendTo(tabContent);
            $(tabContent).appendTo(span12);

            //creating the button and results input           
            resultsName = $(this).prop("id");
            $(this).prop("name", resultsName);

            $(span12).appendTo(row);
            $(row).appendTo(container);
            $(container).appendTo(mainDiv);
            $(cronContainer).append(mainDiv);

            var that = $(this);

            // Hide the original input
            that.hide();

            // Replace the input with an input group
            var $g = $("<div>").addClass("input-group");
            // Add an input
            var $i = $("<input>", { type: 'text', placeholder: '请选择发送规则', readonly: 'readonly', style: 'color:green;width:300px' }).addClass("form-control m-r-10").val('');
            $i.appendTo($g);
            // Add the button
            var $b = $("<button id='btn-cron' class=\"btn btn-sm btn-success m-l-10\">选择循环规则</button>");
            // Put button inside span
            var $s = $("<span>").addClass("input-group-btn");
            $b.appendTo($s);
            $s.appendTo($g);

            $(this).before($g);

            inputElement = that;
            displayElement = $i;

            $b.popover({
                html: true,
                content: function () {
                    return $(cronContainer).html();
                },
                template: '<div class="popover" style="max-width:700px !important; width:600px"><div class="arrow"></div><div class="popover-inner"><h3 class="popover-title"></h3><div class="popover-content"><p></p></div></div></div>',
                placement: 'bottom'

            }).on('click', function (e) {
                e.preventDefault();

                fillDataOfMinutesAndHoursSelectOptions();
                fillDayWeekInMonth();
                fillInWeekDays();
                fillInMonths();
                $('#CronGenTabs a').click(function (e) {
                    e.preventDefault();
                    $(this).tab('show');
                    //generate();
                });
                $("#CronGenMainDiv select,input").change(function (e) {
                    generate();
                });
                $("#CronGenMainDiv input").focus(function (e) {
                    generate();
                });
                //generate();
            });
            return;
        }
    });


    var fillInMonths = function () {
        var days = [
            { text: "一月", val: "1", rel: "一月" },
            { text: "二月", val: "2", rel: "二月" },
            { text: "三月", val: "3", rel: "三月" },
            { text: "四月", val: "4", rel: "四月" },
            { text: "五月", val: "5", rel: "五月" },
            { text: "六月", val: "6", rel: "六月" },
            { text: "七月", val: "7", rel: "七月" },
            { text: "八月", val: "8", rel: "八月" },
            { text: "九月", val: "9", rel: "九月" },
            { text: "十月", val: "10", rel: "十月" },
            { text: "十一月", val: "11", rel: "十一月" },
            { text: "十二月", val: "12", rel: "十二月" }
        ];
        $(".months").each(function () {
            fillOptions(this, days);
        });
    };

    var fillOptions = function (elements, options) {
        for (var i = 0; i < options.length; i++)
            $(elements).append("<option value='" + options[i].val + "' rel='" + options[i].rel + "'>" + options[i].text + "</option>");
    };
    var fillDataOfMinutesAndHoursSelectOptions = function () {
        for (var i = 0; i < 60; i++) {
            if (i < 24) {
                $(".hours").each(function () { $(this).append(timeSelectOption(i)); });
            }
            $(".minutes").each(function () { $(this).append(timeSelectOption(i)); });
        }
    };
    var fillInWeekDays = function () {
        var days = [
            { text: "周一", val: "MON", rel: "周一" },
            { text: "周二", val: "TUE", rel: "周二" },
            { text: "周三", val: "WED", rel: "周三" },
            { text: "周四", val: "THU", rel: "周四" },
            { text: "周五", val: "FRI", rel: "周五" },
            { text: "周六", val: "SAT", rel: "周六" },
            { text: "周日", val: "SUN", rel: "周日" }
        ];
        $(".week-days").each(function () {
            fillOptions(this, days);
        });

    };
    var fillDayWeekInMonth = function () {
        var days = [
            { text: "第一个", val: "1", rel: "第一个" },
            { text: "第二个", val: "2", rel: "第二个" },
            { text: "第三个", val: "3", rel: "第三个" },
            { text: "第四个", val: "4", rel: "第四个" }
        ];
        $(".day-order-in-month").each(function () {
            fillOptions(this, days);
        });
    };
    var displayTimeUnit = function (unit) {
        if (unit.toString().length == 1)
            return "0" + unit;
        return unit;
    };
    var timeSelectOption = function (i) {
        return "<option id='" + i + "'>" + displayTimeUnit(i) + "</option>";
    };

    var generate = function () {
        var strDesc = "";

        var activeTab = $("ul#CronGenTabs li.active a").prop("id");
        var results = "";
        switch (activeTab) {
            case "MinutesTab":
                results = "0/" + $("#MinutesInput").val() + " * 1/1 * *";
                strDesc += "每隔 " + $("#MinutesInput").val() + "分钟 发送";
                break;
            case "HourlyTab":
                switch ($("input:radio[name=HourlyRadio]:checked").val()) {
                    case "1":
                        results = "0 0/" + $("#HoursInput").val() + " 1/1 * *";
                        strDesc += "每隔 " + $("#HoursInput").val() + "小时 发送";
                        break;
                    case "2":
                        results = "" + Number($("#AtMinutes").val()) + " " + Number($("#AtHours").val()) + " 1/1 * *";
                        strDesc += "在每 " + Number($("#AtHours").val()) + "时" + +Number($("#AtMinutes").val()) + "分 发送";
                        break;
                }
                break;
            case "DailyTab":
                switch ($("input:radio[name=DailyRadio]:checked").val()) {
                    case "1":
                        results = "" + Number($("#DailyMinutes").val()) + " " + Number($("#DailyHours").val()) + " 1/" + $("#DaysInput").val() + " * *";
                        strDesc += "每隔 " + $("#DaysInput").val() + "天 在 " + Number($("#DailyHours").val()) + "时" + +Number($("#DailyMinutes").val()) + "分 发送";
                        break;
                    case "2":
                        results = "" + Number($("#DailyMinutes").val()) + " " + Number($("#DailyHours").val()) + " * * MON-FRI";
                        strDesc += "每周一到周五在 " + Number($("#DailyHours").val()) + "时" + +Number($("#DailyMinutes").val()) + "分 发送";
                        break;
                }
                break;
            case "WeeklyTab":
                var selectedDays = "";
                var selectDayDesc = "";

                $("#Weekly input:checkbox:checked").each(function () {
                    selectedDays += $(this).val() + ",";
                    selectDayDesc += $(this).attr('rel') + ",";
                });
                if (selectedDays.length > 0) {
                    selectedDays = selectedDays.substr(0, selectedDays.length - 1);
                    selectDayDesc = selectDayDesc.substr(0, selectDayDesc.length - 1);
                }

                results = "" + Number($("#WeeklyMinutes").val()) + " " + Number($("#WeeklyHours").val()) + " * * " + selectedDays + "";

                strDesc += "每个 " + selectDayDesc + " 在 " + Number($("#WeeklyHours").val()) + " 时" + +Number($("#WeeklyMinutes").val()) + "分 发送";
                break;
            case "MonthlyTab":
                switch ($("input:radio[name=MonthlyRadio]:checked").val()) {
                    case "1":
                        results = "" + Number($("#MonthlyMinutes").val()) + " " + Number($("#MonthlyHours").val()) + " " + $("#DayOfMOnthInput").val() + " 1/" + $("#MonthInput").val() + " *";
                        strDesc += "每隔 " + $("#MonthInput").val() + "个月 在第" + $("#DayOfMOnthInput").val() + "天 " + Number($("#MonthlyHours").val()) + " 时" + +Number($("#MonthlyMinutes").val()) + "分 发送";
                        break;
                    case "2":
                        results = "" + Number($("#MonthlyMinutes").val()) + " " + Number($("#MonthlyHours").val()) + " * 1/" + Number($("#EveryMonthInput").val()) + " " + $("#DayInWeekOrder").val() + "#" + $("#WeekDay").val() + "";
                        strDesc += "每隔 " + $("#EveryMonthInput").val() + "个月 在" + $("#WeekDay option:selected").text()+" " + $("#DayInWeekOrder option:selected").text() + " " + Number($("#MonthlyHours").val()) + "时" + +Number($("#MonthlyMinutes").val()) + "分 发送";
                        break;
                }
                break;
            case "YearlyTab":
                switch ($("input:radio[name=YearlyRadio]:checked").val()) {
                    case "1":
                        results = "" + Number($("#YearlyMinutes").val()) + " " + Number($("#YearlyHours").val()) + " " + $("#YearInput").val() + " " + $("#MonthsOfYear").val() + " * *";
                        strDesc += "每当 " + $("#MonthsOfYear option:selected").text() + " 第" + $("#YearInput").val() + "天 " + Number($("#YearlyHours").val()) + " 时" + +Number($("#YearlyMinutes").val()) + "分 发送";
                        break;
                    case "2":
                        results = "" + Number($("#YearlyMinutes").val()) + " " + Number($("#YearlyHours").val()) + " ? " + $("#MonthsOfYear2").val() + " " + $("#DayWeekForYear").val() + "#" + $("#DayOrderInYear").val() + " *";
                        strDesc += "每当 " + $("#MonthsOfYear2 option:selected").text() + " " + $("#DayOrderInYear option:selected").text() + " " + $("#DayWeekForYear option:selected").text() + " " + Number($("#YearlyHours").val()) + "时" + +Number($("#YearlyMinutes").val()) + "分 发送";
                        break;
                }
                break;
        }

        // Update original control
        inputElement.val(results);
        // Update display
        //displayElement.val(results);
        displayElement.val(strDesc);

        $("#cronExp").val(results);
    };

})(jQuery);

