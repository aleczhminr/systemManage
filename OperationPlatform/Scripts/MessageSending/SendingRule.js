//用于确认循环任务条件的Model
var recreateCondition = new Array();

function SetItemCondition(id, colName, colDesc, conditionType, conditionGroup, range, diffVal) {
    var model = {
        Id: id,
        ColName: colName,
        ColDesc: colDesc,
        ConditionType: conditionType,
        ConditionGroup: conditionGroup,
        DataRange: range,
        DiffVal: diffVal
    };

    recreateCondition.push(model);
}

function changeTrace() {
    if ($("#traceMark").prop('checked')) {
        $("#traceSetting").removeClass('hidden');
    } else {
        $("#traceSetting").addClass('hidden');
    }
}

//获取筛选器条件并输出到页面
function GetShowCondition() {
    var postJson = {};

    postJson["verif"] = $("#verif").val();
    var url = "/MessageSending/GetShowCondition";

    $.doAjax(url, postJson, function (msg) {
        if (msg != "") {
            var json = jQuery.parseJSON(msg);

            $.each(json.DataList, function (i, n) {
                var html = "";

                html += "<div rel='" + n.ConditionGroup + "' class='ConditionGroup'>" + n.GroupDesc + ":" + "<br>";

                $.each(n.GroupList, function (k, m) {
                    html += "<div class='ConditionItem' colName='" + m.ColName + "' ruleId='" + m.RuleId + "' " +
                        "group='" + m.ConditionGroup + "' type='" + m.ConditionType +
                        "' listId='" + m.Id + "' max='" + m.MaxValue + "' colDesc='" + m.ColDesc + "' " +
                        "min='" + m.MinValue + "' range='" + m.RangeData + "' diffVal='' ";

                    //if (m.ConditionType == "TimePair") {
                    //    html += " style='cursor:pointer' onclick='ShowDateDiffDay(this);'>" + m.Remark + "</div>";
                    //} else {
                    html += ">" + m.Remark + "</div>";
                    //}

                }); //" + m.Id + "" + n.ColDesc + "" + n.ConditionType + "Show" + m.ConditionType + "(this)

                html += "</div><br>";

                $("#conditionList").append(html);
            });

            $("#filterCondition").removeClass('hidden');
        }
    }, true);
}

//选择发送类型
//在选择发送类型的时候调整条件组的状态和点击事件
function SelectSendingType() {
    if ($("#sendType_1").prop('checked')) {
        $(".ConditionGroup .ConditionItem[type='TimePair']").each(function () {
            if ($(this).attr('fMark')) {
                $(this).html($(this).attr('fMark'));
            } else {
                //$(this).css('color', '');
            }
        });
    } else {
    }

    if ($("#sendType_2").prop('checked')) {
        $(".ConditionGroup .ConditionItem[type='TimePair']").each(function () {
            if ($(this).attr('fMark')) {
                $(this).html($(this).attr('fMark'));
            } else {
                //$(this).css('color', '');
            }
        });

        $("#setSpecTime").removeClass('hidden');

        $("#setSpecTime .specDate").datetimepicker();
    } else {
        $("#setSpecTime").addClass('hidden');
    }

    if ($("#sendType_3").prop('checked')) {
        $(".ConditionGroup .ConditionItem[type='TimePair']").attr('onclick', 'ShowDateDiffDay(this)');
        $(".ConditionGroup .ConditionItem[type='TimePair']").css('cursor', 'pointer');

        $("#setCronTime").removeClass('hidden');
        $("#sendMark").removeClass('hidden');
        
        //初始化Cron表达式设置
        if (!$("#btn-cron").length > 0) {

            $("#cron").cronGen();
        }
        
        $(".ConditionGroup .ConditionItem[type='TimePair']").each(function () {
            if ($(this).attr('diffVal') == '') {
                $(this).css('color', 'red');
            } else {
                $(this).css('color', '');
                $(this).html($(this).attr('colDesc') + " 据设定时间 【" + $(this).attr('diffVal') + "天前】");
            }
        });
        //$("#setSpecTime").removeClass('hidden');

        //$("#setSpecTime .specDate").datepicker({
        //    format: "yyyy-mm-dd"
        //});
    } else {
        $(".ConditionGroup .ConditionItem[type='TimePair']").removeAttr('onclick');
        $(".ConditionGroup .ConditionItem[type='TimePair']").css('cursor', '');
        $(".ConditionGroup .ConditionItem[type='TimePair']").css('color', '');

        $("#setCronTime").addClass('hidden');
        $("#sendMark").addClass('hidden');
        //$("#setSpecTime").addClass('hidden');
    }
}

function ShowDateDiffDay(obj) {
    var colDesc = $(obj).attr('colDesc');
    var colName = $(obj).attr('colName');

    dialog({
        id: "setDiffDialog",
        title: '设置循环时间条件',
        content: "<div><span id='desc'>" + colDesc + "</span> 据设定时间 <input type='text' name='timeDiff' style='width:30px' value='' id='timeDiff'/> 天前</div>",
        ok: function () {
            if ($("#timeDiff").val() != '') {
                $(".ConditionGroup .ConditionItem[colName='" + colName + "']").attr('diffVal', $("#timeDiff").val());
                if (!$(".ConditionGroup .ConditionItem[colName='" + colName + "']").attr('fMark')) {
                    $(".ConditionGroup .ConditionItem[colName='" + colName + "']").attr('fMark',
                    $(".ConditionGroup .ConditionItem[colName='" + colName + "']").html());
                }

                $(".ConditionGroup .ConditionItem[colName='" + colName + "']").css('color', '');
                //$(".ConditionGroup .ConditionItem[colName='" + colName + "']").css('cursor', '');

                $(".ConditionGroup .ConditionItem[colName='" + colName + "']").html(colDesc + " 据设定时间 【" + $("#timeDiff").val() + "天前】");
            }
        }
    }).showModal();
}

function SetConditionStr() {
    var recreateGroup = new Array();
    var modelStr = "";

    //循环条件组中带有特定时间的
    $(".ConditionGroup .ConditionItem[type='TimePair']").each(function () {
        var groupNum = parseInt($(this).attr('group'));
        if ($.inArray(groupNum, recreateGroup) < 0) {
            recreateGroup.push(groupNum);
        }
    });

    //生成产生变动的条件组
    if (recreateGroup.length > 0) {
        for (var i = 0; i < recreateGroup.length; i++) {
            $(".ConditionGroup .ConditionItem[group='" + recreateGroup[i] + "']").each(function () {
                var colName = $(this).attr("colname");
                var desc = $(this).attr("coldesc");
                //var tName = $(this).attr("tName");
                var id = $(this).attr("ruleid");
                var type = $(this).attr("type");
                var group = $(this).attr("group");
                var diffVal = $(this).attr("diffval");

                var dataRange = {
                    Max: $(this).attr("max"),
                    Min: $(this).attr("min"),
                    Range: $(this).attr("range")
                };

                SetItemCondition(id, colName, desc, type, group, dataRange, diffVal);

            });
        }

        modelStr = JSON.stringify(recreateCondition);
    }

    return modelStr;


}