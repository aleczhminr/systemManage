
//获取规则列表生成可选按钮
function GetItem() {
    var ur = "/FiltrateData/GetRuleCondition";

    $.doAjax(ur, null, function (msg) {
        if (msg != "[]") {
            var json = $.parseJSON(msg);

            $.each(json.DataList, function (i, n) {
                //如果存在该节点的目录
                if ($(".SelectWhereTitle[group='" + n.ConditionGroup + "']").length > 0) {
                    //alert("haha");
                } else {
                    var title = "";
                    switch (n.ConditionGroup) {
                        case 1:
                            title = "属性信息";
                            break;
                        case 2:
                            title = "订单信息";
                            break;
                        case 3:
                            title = "业务信息";
                            break;
                        case 4:
                            title = "登录信息";
                            break;
                        case 5:
                            title = "基础数据信息";
                            break;
                        case 6:
                            title = "用户标签";
                            break;
                        default:
                            break;
                    }
                    $(".SelectWhereBody").append("<div class='SelectWhereTitle' onclick='TriggerLabel(this)' group='" + n.ConditionGroup + "'><div>" + title + "<span class='pg-arrow_minimize_line' style='margin-left: 5px;margin-top: 3px'></span></div></div>" +
                        "<div class='SelectWhereItem'></div>");
                }

                $(".SelectWhereTitle[group='" + n.ConditionGroup + "']").next(".SelectWhereItem").append("<div class='SelectList' name='" + n.ColName + "' tName='" + n.TableName + "' cId='" + n.Id + "' desc='" + n.ColDesc + "' where='" + n.ConditionType + "' onclick='Show" + n.ConditionType + "(this)'>" + n.ColDesc + "</div>");
            });

            $(".SelectWhereItem").append("<div class='clear'></div>");
            $(".SelectWhereBody").append("<div class='AccomplishButtonList'>" +
                "<div class='AccomplishButton'><input type='button' onclick='SelectDataSetEx()' value='查询数据' class='btn btn-primary AButton' />" +
                "</div><div class='AddWhere' onclick='SelectWhereShow()'>新增</div></div>");


        }
    }, true);
}

var conditionList = new Array();
//生成一个条件备选项、
function SetItemCondition(id, colName, tabName, colDesc, conditionType, conditionGroup, range) {
    var model = {
        Id: id,
        ColName: colName,
        TableName: tabName,
        ColDesc: colDesc,
        ConditionType: conditionType,
        ConditionGroup: conditionGroup,
        DataRange: range
    };

    conditionList.push(model);
}

//移除一个条件
function RemoveItemCondition(id) {
    for (var i = 0; i < conditionList.length; i++) {
        if (id == conditionList[i].Id)
            conditionList.remove(conditionList[i]);
    }
}


//不同条件的条件类型处理方法
function ShowStrPair(obj) {
    var colName = $(obj).attr("name");
    var desc = $(obj).attr("desc");
    var tName = $(obj).attr("tName");
    var id = $(obj).attr("cId");
    var type = $(obj).attr("where");


    $("#TwoSelectNumber .TitleTime .Title").html(desc);
    $("#TwoSelectNumber input").val("");

    //var maxVal = dom.attr("max");
    //$("#TwoSelectNumber .MaxTime .MaxTimeValue").attr("placeholder", maxVal);
    dialog({
        id: "TwoSelectNumberDialog",
        title: "请选择" + desc,
        lock: true,
        background: '#000', // 背景色
        opacity: 0.5,	// 透明度
        content: document.getElementById("TwoSelectNumber"),
        ok: function () {
            var dataRange = {
                Max: 0,
                Min: 0,
                Range: 0
            };

            dataRange.Max = $("#TwoSelectNumber .MaxTimeValue").val();
            dataRange.Min = $("#TwoSelectNumber .MinTimeValue").val();

            SetItemCondition(id, colName, tName, desc, type, 0, dataRange);

            console.log(conditionList);

            $(obj).addClass("SelectListSelect");
            WhereItemOtherListShow();
        },
        cancelValue: "移除",
        cancel: function () {
            if ($(obj).hasClass("SelectListSelect")) {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + where + "']").remove();
                $(obj).removeClass("SelectListSelect");
                WhereItemOtherListShow();
            }
        }
    }).show();

}

function ShowIntPair(obj) {
    ShowStrPair(obj);
}

function ShowIntRange(obj) {

}

function ShowStrRange(obj) {

}

function ShowTimePair(obj) {
    var colName = $(obj).attr("name");
    var desc = $(obj).attr("desc");
    var tName = $(obj).attr("tName");
    var id = $(obj).attr("cId");
    var type = $(obj).attr("where");

    $("#TwoSelectTime .TitleTime .Title").html(desc);
    $("#TwoSelectTime input").val("");
    dialog({
        id: "TwoSelectTimeDialog",
        title: "请选择" + desc,
        lock: true,
        background: '#000', // 背景色
        opacity: 0.5,	// 透明度
        content: document.getElementById("TwoSelectTime"),
        ok: function () {
            var dataRange = {
                Max: 0,
                Min: 0,
                Range: 0
            };

            dataRange.Max = $("#TwoSelectTime .MaxTimeValue").val();
            dataRange.Min = $("#TwoSelectTime .MinTimeValue").val();

            SetItemCondition(id, colName, tName, desc, type, 0, dataRange);

            console.log(conditionList);
            //var html = '<div class="OtherList" wheretype="BuyOrderByTime" value="Two" max="' + maxValue + '" min="' + minValue + '">';

            //if (maxValue.length > 0 && minValue.length > 0) {
            //    html += "在 " + minValue + " 到 " + maxValue + " 之间购买订单";
            //} else if (maxValue.length > 0 && minValue == "") {
            //    html += "在" + maxValue + " 之前购买订单";
            //} else if (maxValue == "" && minValue.length > 0) {
            //    html += "在" + minValue + " 之后购买订单";
            //} else {
            //    return true;
            //}
            //html += "</div>";
            //if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='BuyOrderByTime']").size() > 0) {
            //    $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='BuyOrderByTime']").replaceWith(html);
            //} else {
            //    $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
            //}

            $(obj).addClass("SelectListSelect");
            WhereItemOtherListShow();
        },
        cancelVal: "移除",
        cancel: function () {
            if ($(obj).hasClass("SelectListSelect")) {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='BuyOrderByTime']").remove();
                $(obj).removeClass("SelectListSelect");
                //WhereItemOtherListShow();
            }
        }
    }).show();
}


function SelectDataSetEx() {
    HideSelectWhereBody();

    //SettingThConfigClick(true);

    //var json = SelectAjaxJson();
    var url = "/FiltrateData/GetFilterDataSet";

    $("#sk_loading").show();
    $.ajax({
        type: "POST",
        url: url,
        data: { "postJson": JSON.stringify(conditionList) },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(XMLHttpRequest.responseText);
            alert("查询数据出错");
        },
        success: function (msg) {

            $("#sk_loading").hide();
            var Column = GetShowColumn();
            if (msg == "nullJson") {

                var trHtml = '<tr ><td colspan="' + Column.length + '" class="DataNullJson">没有查到符合的店铺信息</td></tr>';
                $("#RequstDataTable tbody").html(trHtml);
            }
            if (msg != "error!") {
                $("#GetDataJson").html(msg);
                SetTableData();
            } else {
                alert("查询数据出错");
            }
        }
    });
}


















function GetTagInfo() {
    var ur = "/FiltrateData/TagList";
    $.doAjax(ur, null, function (msg) {
        if (msg != "[]") {
            var json = $.parseJSON(msg);
            $("#AccountTagInfo .SelectList").remove();
            $("#AccountTagInfo").prepend(template('SelectOtherTagScript', { "table": json }));
            if ($("#AccountTagInfo .SelectList").size() > 0) {
                $("#AccountTagInfo").show();
                $(".SelectWhereBody .SelectWhereTitle.tagList").show();

                $("#AccountTagInfo .SelectList").click(function () {
                    SelectTagInfoWhere(this);
                });

            } else {
                $("#AccountTagInfo").hide();
                $(".SelectWhereBody .SelectWhereTitle.tagList").hide();
            }

        }
    }, true);
}

/*
Sole  单一值
Two   二个值




*/

//短信小于会员数
function SmsLesserThanUser(obj) {


    var dom = $(obj);

    //WhereItemOtherListDiv

    if ($(obj).hasClass("SelectListSelect")) {
        $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='SmsLesserThanUser']").remove();
        $(obj).removeClass("SelectListSelect");
    } else {
        var html = '<div class="OtherList" wheretype="SmsLesserThanUser" value="Sole">短信小于会员数</div>';

        $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
        $(obj).addClass("SelectListSelect");
    }

    WhereItemOtherListShow();
}

function BuyOrderByTime(obj) {
    var dom = $(obj);
    $("#TwoSelectTime .TitleTime .Title").html("购买时间");
    $("#TwoSelectTime input").val("");
    dialog({
        id: "TwoSelectTimeDialog",
        title: "请选择订单的购买时间",
        lock: true,
        background: '#000', // 背景色
        opacity: 0.5,	// 透明度
        content: document.getElementById("TwoSelectTime"),
        ok: function () {

            var maxValue = $("#TwoSelectTime .MaxTimeValue").val();
            var minValue = $("#TwoSelectTime .MinTimeValue").val();

            var html = '<div class="OtherList" wheretype="BuyOrderByTime" value="Two" max="' + maxValue + '" min="' + minValue + '">';

            if (maxValue.length > 0 && minValue.length > 0) {
                html += "在 " + minValue + " 到 " + maxValue + " 之间购买订单";
            } else if (maxValue.length > 0 && minValue == "") {
                html += "在" + maxValue + " 之前购买订单";
            } else if (maxValue == "" && minValue.length > 0) {
                html += "在" + minValue + " 之后购买订单";
            } else {
                return true;
            }
            html += "</div>";
            if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='BuyOrderByTime']").size() > 0) {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='BuyOrderByTime']").replaceWith(html);
            } else {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
            }

            $(obj).addClass("SelectListSelect");
            WhereItemOtherListShow();
        },
        cancelVal: "移除",
        cancel: function () {
            if ($(obj).hasClass("SelectListSelect")) {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='BuyOrderByTime']").remove();
                $(obj).removeClass("SelectListSelect");
                WhereItemOtherListShow();
            }
        }
    }).show();

}
function RegTimeByTime(obj) {
    var dom = $(obj);
    $("#TwoSelectTime .TitleTime .Title").html("注册时间");
    $("#TwoSelectTime input").val("");
    dialog({
        id: "TwoSelectTimeDialog",
        title: "请选择注册时间",
        lock: true,
        background: '#000', // 背景色
        opacity: 0.5,	// 透明度
        content: document.getElementById("TwoSelectTime"),
        ok: function () {

            var maxValue = $("#TwoSelectTime .MaxTimeValue").val();
            var minValue = $("#TwoSelectTime .MinTimeValue").val();

            var html = '<div class="OtherList" wheretype="RegTimeByTime" value="Two" max="' + maxValue + '" min="' + minValue + '">';

            if (maxValue.length > 0 && minValue.length > 0) {
                html += "在 " + minValue + " 到 " + maxValue + " 之间注册";
            } else if (maxValue.length > 0 && minValue == "") {
                html += "在" + maxValue + " 之前注册";
            } else if (maxValue == "" && minValue.length > 0) {
                html += "在" + minValue + " 之后注册";
            } else {
                return true;
            }
            html += "</div>";
            if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='RegTimeByTime']").size() > 0) {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='RegTimeByTime']").replaceWith(html);
            } else {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
            }

            $(obj).addClass("SelectListSelect");
            WhereItemOtherListShow();
        },
        cancelVal: "移除",
        cancel: function () {
            if ($(obj).hasClass("SelectListSelect")) {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='RegTimeByTime']").remove();
                $(obj).removeClass("SelectListSelect");
                WhereItemOtherListShow();
            }
        }
    }).show();
}

function BuyOrderProject(obj) {

    dialog({
        id: "BuyOrderProjectDialog",
        title: "请选择购买过的产品",
        lock: true,
        background: '#000', // 背景色
        opacity: 0.5,	// 透明度
        content: document.getElementById("SelectOrderProjectList"),
        ok: function () {
            $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='OrderProject']").remove();
            var objList = $("#SelectOrderProjectList .SelectList.SelectListSelect");
            if (objList.length > 0) {
                for (var i = 0; i < objList.length; i++) {
                    var list = $(objList[i]);

                    var html = '<div class="OtherList" wheretype="OrderProject" value="' + list.attr("value") + '">' + list.html() + '</div>';
                    $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
                }
                $(obj).addClass("SelectListSelect")
            } else {
                $(obj).removeClass("SelectListSelect");
            }

            WhereItemOtherListShow();
        },
        cancelVal: "移除",
        cancel: function () {
            $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='OrderProject']").remove();
            $(obj).removeClass("SelectListSelect");
            $("#SelectOrderProjectList .SelectListSelect").removeClass("SelectListSelect");
            WhereItemOtherListShow();
        }
    }).show();
}

function SearchByAgent(obj) {

    dialog({
        id: "SearchByAgentDialog",
        title: "请选择代理商",
        lock: true,
        background: '#000', // 背景色
        opacity: 0.5,	// 透明度
        content: document.getElementById("SelectAgentList"),
        ok: function () {
            $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='SearchByAgent']").remove();
            var objList = $("#SelectAgentList .SelectList.SelectListSelect");
            if (objList.length > 0) {
                for (var i = 0; i < objList.length; i++) {
                    var list = $(objList[i]);

                    var html = '<div class="OtherList" wheretype="SearchByAgent" value="' + list.attr("value") + '">' + list.html() + '</div>';
                    $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
                }
                $(obj).addClass("SelectListSelect");
            } else {
                $(obj).removeClass("SelectListSelect");
            }

            WhereItemOtherListShow();
        },
        cancelValue: "移除",
        cancel: function () {
            $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='OrderProject']").remove();
            $(obj).removeClass("SelectListSelect");
            $("#SelectAgentList .SelectListSelect").removeClass("SelectListSelect");
            WhereItemOtherListShow();
        }
    }).show();
}

function AccountActive(obj) {

    dialog({
        id: "SelectAccountActivetDialog",
        title: "请选择购买过的产品",
        lock: true,
        background: '#000', // 背景色
        opacity: 0.5,	// 透明度
        content: document.getElementById("SelectAccountActiveList"),
        ok: function () {
            $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='AccountActive']").remove();
            var objList = $("#SelectAccountActiveList .SelectList.SelectListSelect");
            if (objList.length > 0) {
                for (var i = 0; i < objList.length; i++) {
                    var list = $(objList[i]);

                    var html = '<div class="OtherList" wheretype="AccountActive" value="' + list.attr("value") + '">【' + list.html() + '】用户</div>';
                    $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
                }
                $(obj).addClass("SelectListSelect")
            } else {
                $(obj).removeClass("SelectListSelect");
            }

            WhereItemOtherListShow();
        },
        cancelValue: "移除",
        cancel: function () {
            $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='AccountActive']").remove();
            $(obj).removeClass("SelectListSelect");
            $("#SelectAccountActiveList .SelectListSelect").removeClass("SelectListSelect");
            WhereItemOtherListShow();
        }
    }).show();
}

function WhereSliderListClick(obj) {
    var where = $(obj).attr("where");
    var json = {};
    json["id"] = "";
    if (where == "MaxUserNum") {
        json["id"] = "userNumber";
        json["title"] = "会员数量";
        json["maxValue"] = $(obj).attr("max");
    } else if (where == "MaxSaleNum") {
        json["id"] = "saleNumber";
        json["title"] = "销售笔数";
        json["maxValue"] = $(obj).attr("max");
    } else if (where == "MaxLastLoginTime") {
        json["id"] = "LastLoginTime";
        json["title"] = "未登录天数";
        json["maxValue"] = $(obj).attr("max");
    } else if (where == "MaxOrderMoney") {
        json["id"] = "orderMoney";
        json["title"] = "订单总额";
        json["maxValue"] = $(obj).attr("max");
    } else if (where == "MaxOrderNum") {
        json["id"] = "orderNumber";
        json["title"] = "订单数量";
        json["maxValue"] = $(obj).attr("max");
    } else if (where == "MaxSmsNum") {
        json["id"] = "smsNumber";
        json["title"] = "发短信数";
        json["maxValue"] = $(obj).attr("max");
    } else if (where == "MaxGoodsNum") {
        json["id"] = "goodsNumber";
        json["title"] = "商品数量";
        json["maxValue"] = $(obj).attr("max");
    } else if (where == "MaxOutlayNum") {
        json["id"] = "outlayNumber";
        json["title"] = "支出数量";
        json["maxValue"] = $(obj).attr("max");
    } else if (where == "MaxAllLoginNum") {
        json["id"] = "loginTimes";
        json["title"] = "登录次数";
        json["maxValue"] = $(obj).attr("max");
    }
    if (json["id"] != "") {

        if ($(obj).hasClass("SelectListSelect")) {
            $('#' + json["id"]).parent().remove();
            $(obj).removeClass("SelectListSelect");
        } else {
            $("#WhereSliderListDiv .ItemSliderList").append(template('WhereSliderListScript', json));
            $('#' + json["id"] + ' .SliderBody').slider({
                range: true,
                values: [0, 100],
                slide: function (event, ui) {

                    var value = ui.values;

                    var dom = $(this).parent();

                    var maxValu = parseInt(dom.find(".SliderBody").attr("maxValue"));

                    var min = parseInt(maxValu * (value[0] / 100));
                    var max = parseInt(maxValu * (value[1] / 100));
                    dom.find(".MinValueText").val(min);
                    dom.find(".MaxValueText").val(max);
                },
                change: function (event, ui) {

                    SetWhereVlaue();
                }
            });
            $('#' + json["id"] + ' .MinValueText').keyup(function () {
                var objDom = $('#' + json["id"]);
                var maxValue = objDom.find(".SliderBody").attr("maxValue");
                var min = objDom.find(".MinValueText").val();
                if (min == "") {
                    min = "0";
                }
                var left = parseInt(Number(min) / Number(maxValue) * 100);
                objDom.find(".SliderBody").slider("values", 0, left)
            });

            $('#' + json["id"] + ' .MaxValueText').keyup(function () {
                var objDom = $('#' + json["id"]);
                var maxValue = objDom.find(".SliderBody").attr("maxValue");
                var max = objDom.find(".MaxValueText").val();
                if (max == "") {
                    max = maxValue;
                }
                var left = parseInt(Number(max) / Number(maxValue) * 100);
                objDom.find(".SliderBody").slider("values", 1, left)
            });


            $(obj).addClass("SelectListSelect");
        }
    }
}


function MonthIndexActive(obj) {
    var dom = $(obj);

    //WhereItemOtherListDiv

    if ($(obj).hasClass("SelectListSelect")) {
        $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='MonthIndexActive']").remove();
        $(obj).removeClass("SelectListSelect");
    } else {
        var html = '<div class="OtherList" wheretype="MonthIndexActive" value="Sole">本月首次活跃</div>';

        $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
        $(obj).addClass("SelectListSelect")
    }

    WhereItemOtherListShow();
}

function MonthActiveDrain(obj) {
    var dom = $(obj);

    //WhereItemOtherListDiv

    if ($(obj).hasClass("SelectListSelect")) {
        $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='MonthActiveDrain']").remove();
        $(obj).removeClass("SelectListSelect");
    } else {
        var html = '<div class="OtherList" wheretype="MonthActiveDrain" value="Sole">本月活跃进入流失</div>';

        $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
        $(obj).addClass("SelectListSelect")
    }
    WhereItemOtherListShow();
}


function SelectTagInfoWhere(obj) {
    var dom = $(obj);

    //WhereItemOtherListDiv

    if ($(obj).hasClass("SelectListSelect")) {
        $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='TagInfo'][value='" + dom.attr("value") + "']").remove();
        $(obj).removeClass("SelectListSelect");
    } else {

        var html = '<div class="OtherList" wheretype="TagInfo" value="' + dom.attr("value") + '">' + dom.html() + '</div>';
        $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
        $(obj).addClass("SelectListSelect")
    }

    WhereItemOtherListShow();
}

function WhereItemOtherListShow() {

    $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList").attr("onclick", "RemoveWhereItem(this)");
}


function RemoveWhereItem(obj) {
    var dom = $(obj)
    var type = dom.attr("wheretype");
    var val = dom.attr("value");
    dom.remove();

    if (type == "TagInfo") {
        $("#AccountTagInfo .SelectListSelect[value='" + val + "']").removeClass("SelectListSelect");
    }
    else {
        if (type == "AccountActive") {
            $("#SelectAccountActiveList .SelectList[value='" + val + "']").removeClass("SelectListSelect");

        }
        if (type == "OrderProject") {
            $("#SelectOrderProjectList .SelectList[value='" + val + "']").removeClass("SelectListSelect");

        }
        if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + type + "']").size() < 1) {
            $(".SelectWhereBody .SelectList[where='" + type + "']").removeClass("SelectListSelect");
        }
    }

}

function GetBuyOrderProject() {

    var ur = "/FiltrateData/OrderProjectList";
    $.doAjax(ur, null, function (msg) {
        if (msg != "[]") {
            var json = $.parseJSON(msg);
            $.each(json, function (i, n) {
                var html = '<div class="SelectList" value="' + n.busId + '">' + n.displayName + '【' + n.normalMoney + '】</div>';
                $("#SelectOrderProjectList .clear").before(html);
            });

            $("#SelectOrderProjectList .SelectList").click(function () {
                SelectListClick(this);
            });
        }
    }, true);

}

function GetAgent() {
    var ur = "/FiltrateData/AnentList";
    $.doAjax(ur, null, function (msg) {
        if (msg != "[]") {
            var json = $.parseJSON(msg);

            var html = '<div class="m-l-5"><button type="button" value="0" class="btn btn-success " onclick="SelectAllAgent(this)">全选</button>' +
                '<button type="button" value="0" class="btn btn-danger m-l-5" onclick="SelectCommonAgent(this)">常用分组</button></div>';
            $("#SelectAgentList .clear").before(html);

            $.each(json, function (i, n) {
                var html = "";
                if (n.AgentGroup == 1) {
                    html = '<div class="SelectList Group" value="' + n.ID + '">' + n.AgentName + '</div>';
                } else {
                    html = '<div class="SelectList " value="' + n.ID + '">' + n.AgentName + '</div>';
                }

                $("#SelectAgentList .clear").before(html);
            });

            $("#SelectAgentList .SelectList").click(function () {
                SelectListClick(this);
            });
        }
    }, true);
}

function SelectListClick(obj) {
    if ($(obj).hasClass("SelectListSelect")) {
        $(obj).removeClass("SelectListSelect");
    } else {
        $(obj).addClass("SelectListSelect");
    }

    //if ($(obj).hasClass("SelectListSelect") && $(obj).hasClass("SelectAll")) {
    //    $("#SelectAgentList .SelectList").addClass("SelectListSelect");
    //} else if (!($(obj).hasClass("SelectListSelect")) && $(obj).hasClass("SelectAll")) {
    //    $("#SelectAgentList .SelectList").removeClass("SelectListSelect");
    //}
}

function SelectAllAgent(obj) {
    if (obj.value == "0") {
        $("#SelectAgentList .SelectList").addClass("SelectListSelect");
        obj.value = "1";
    } else {
        $("#SelectAgentList .SelectList").removeClass("SelectListSelect");
        obj.value = "0";
    }
}

function SelectCommonAgent(obj) {
    if (obj.value == "2") {
        $("#SelectAgentList .Group").addClass("SelectListSelect");
        obj.value = "3";
    } else {
        $("#SelectAgentList .Group").removeClass("SelectListSelect");
        obj.value = "2";
    }
}

function ExcludeAccount(obj) {

    if (!$(obj).hasClass("SelectListSelect")) {
        dialog({
            id: "TwoSelectNumberDialog",
            title: "请输入要排除的店铺ID",
            lock: true,
            background: '#000', // 背景色
            opacity: 0.5,	// 透明度
            content: "<div class='ExcludeContent'><textarea id='ExcludeAccountText' cols='20' rows='2'></textarea></div><div class='ExcludeTitle'>店铺ID之间请用','分开</div>",
            ok: function () {
                var list = $("#ExcludeAccountText").val();
                list = $.trim(list.replace(/[^\d\,]/g, ''));
                var ItemList = list.split(',');
                var html = '<div class="OtherList" wheretype="excludeAccount" value="' + list + '">共排除<span style="color:red;font-weight: bold;"> ' + ItemList.length + ' </span>个店铺</div>';

                $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
                $(obj).addClass("SelectListSelect")
                WhereItemOtherListShow();
            },
            cancelValue: "移除",
            cancel: function () {
            }
        }).show();
    } else {
        $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='excludeAccount']").remove();
        $(obj).removeClass("SelectListSelect");

        WhereItemOtherListShow();
    }

}




function SoleAccount(obj) {

    if (!$(obj).hasClass("SelectListSelect")) {
        dialog({
            id: "TwoSelectNumberDialog",
            title: "请输入要查询的店铺ID",
            lock: true,
            background: '#000', // 背景色
            opacity: 0.5,	// 透明度
            content: "<div class='ExcludeContent'><textarea id='soleAccountText' cols='20' rows='2'></textarea></div><div class='ExcludeTitle'>店铺ID之间请用','分开</div>",
            ok: function () {
                var list = $("#soleAccountText").val();
                list = $.trim(list.replace(/[^\d\,]/g, ''));
                var ItemList = list.split(',');
                var html = '<div class="OtherList" wheretype="soleAccount" value="' + list + '">只查询<span style="color:red;font-weight: bold;"> ' + ItemList.length + ' </span>个店铺</div>';

                $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
                $(obj).addClass("SelectListSelect")
                WhereItemOtherListShow();
            },
            cancelValue: "移除",
            cancel: function () {
            }
        }).show();
    } else {
        $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='soleAccount']").remove();
        $(obj).removeClass("SelectListSelect");

        WhereItemOtherListShow();
    }

}



function SelectWhereShow() {

    $("#WhereListDiv .WhereLogicSelect").removeClass("WhereLogicSelect");

    var html = $("#WhereLogicHtml").html();
    $("#WhereListDiv .AddWhere").before(html);

    $(".SelectWhereBody").show();

    $(".SelectWhereBody .SelectListSelect").removeClass("SelectListSelect");
    $("#SelectAccountActiveList .SelectList").removeClass("SelectListSelect");
    $("#SelectOrderProjectList .SelectList").removeClass("SelectListSelect");

    var width = $(".WhereBody").width();
    var showWidth = $(".SelectWhereBody").width();

    if (width > showWidth) {
        width = width - showWidth;
        if (width < 500) {
            width = 500;
        }
    }
    if (width > 0) {
        $(".WhereBody .WhereItemList").width(width);
    }

    $("#tag").attr("rel", "on");
    $("#tag").click();
}
function HideSelectWhereBody() {
    $("#WhereListDiv .WhereLogicSelect").removeClass("WhereLogicSelect");

    var logicDom = $("#WhereListDiv .WhereLogic");
    $.each(logicDom, function (i, n) {
        if ($(n).find(".LogicContent .OtherList").size() < 1) {
            $(n).remove();
        }
    });


    $(".SelectWhereBody").hide();
    $(".WhereBody .WhereItemList").width("100%");
}

function SelectDataSet() {
    HideSelectWhereBody();

    SettingThConfigClick(true);

    var json = SelectAjaxJson();
    var url = "/FiltrateData/SelectData";

    $("#sk_loading").show();
    $.ajax({
        type: "POST",
        url: url,
        data: { "postJson": JSON.stringify(json) },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(XMLHttpRequest.responseText);
            alert("查询数据出错");
        },
        success: function (msg) {

            $("#sk_loading").hide();
            var Column = GetShowColumn();
            if (msg == "nullJson") {

                var trHtml = '<tr ><td colspan="' + Column.length + '" class="DataNullJson">没有查到符合的店铺信息</td></tr>';
                $("#RequstDataTable tbody").html(trHtml);
            }
            if (msg != "error!") {
                $("#GetDataJson").html(msg);
                SetTableData();
            } else {
                alert("查询数据出错");
            }
        }
    });
}


function SetTableData() {
    var Column = GetShowColumn();

    var msg = $.trim($("#GetDataJson").html());

    SetTableThead();

    if (msg != "") {
        var json = $.parseJSON(msg);

        if (json.count > 0 && json.data.length > 0) {

            var tdHtml = "";
            var trHtml = "";
            $.each(json.data, function (i, n) {
                tdHtml = "";
                trHtml += "<tr>";

                $.each(Column, function (Ci, Cn) {
                    tdHtml += "<td class='" + GetTableColumnClass(Cn) + "'><div  ";

                    if (Cn == "aotjb") {
                        if (n.aotjbEndtime != null && n.aotjbEndtime != undefined) {
                            tdHtml += "  class='Hint' title='" + n.aotjbEndtime + "' ";
                        }
                    } else if (Cn == "LoginTimeWeb") {
                        tdHtml += "  class='Hint' title='" + n.LoginTimeLast + "'";
                    } else if (Cn == "allCount") {
                        if (n.userCount != null) {
                            tdHtml += "  class='Hint' title='" + n.userCount + "' ";
                        }
                    }

                    tdHtml += ">";
                    if (Cn == "CompanyName") {
                        tdHtml += '<a href="javascript:void(0)" onclick="OpenCompnayUrl(this,' + n.ID + ')" >' + n[Cn];
                        tdHtml += "【" + n.ID + "】 </a>";
                    } else {
                        if (n[Cn] != null) {
                            if ((!isNaN(n[Cn])) && n[Cn] !== 0) {
                                tdHtml += "<span style='color:red'>" + n[Cn] + "</span>";
                            } else {
                                tdHtml += n[Cn];
                            }

                        }
                    }

                    tdHtml += "</div></td>";
                });




                trHtml += tdHtml + "</tr>";
            });

            $("#RequstDataTable tbody").html(trHtml);

        } else {
            var trHtml = '<tr ><td colspan="' + Column.length + '" class="DataNullJson">没有查到符合的店铺信息</td></tr>';
            $("#RequstDataTable tbody").html(trHtml);
        }

        $("#DwonExeclButton").attr("verifi", json.verifi);
        $("#DwonExeclButton").attr("count", json.count);

        $("#SelectTbaleRowCount span").html(json.count);

    } else {
        var trHtml = '<tr ><td colspan="' + Column.length + '" class="DataNullJson">没有查到符合的店铺信息</td></tr>';
        $("#RequstDataTable tbody").html(trHtml);
    }



}

function OpenCompnayUrl(obj, id) {
    window.open("/shopinfo/index/" + id);
}

function SetTableThead() {
    var Column = GetShowColumn();
    var tdHtml = "<tr>";
    $.each(Column, function (i, n) {
        tdHtml += "<th class='" + GetTableColumnClass(n) + "'>" + GetTableThead(n) + "</th>";
    });
    tdHtml += "</tr>";
    $("#RequstDataTable thead").html(tdHtml);

    var trHtml = '<tr ><td colspan="' + Column.length + '" class="DataNullJson">请选择条件进行筛选</td></tr>';
    $("#RequstDataTable tbody").html(trHtml);
}

function GetTableThead(key) {
    var keyVal = {
        "CompanyName": "店铺名称",
        "UserRealName": "店主",
        "RegTime": "注册时间",
        "aotjb": "版本",
        "aotjbEndtime": "过期时间",
        "active": "店铺状态",
        "userNum": "会员数量",
        "dxunity": "短信数",
        "smsNum": "短信发送数量",
        "goodsNum": "商品数",
        "saleNum": "销售数量",
        "orderMoney": "订单金额",
        "outlayNum": "支出数量",
        "LoginTimeWeb": "登录次数",
        "allCount": "优惠券数",
        "userCount": "已使用优惠券",
        "LoginTimeLast": "最后登录时间",
        "returnInsertTime": "最后回访人",
        "PhoneNumber": "手机号",
        "UserEmail": "邮件",
        "AgentName": "代理商"
    };
    var val = keyVal[key];
    if (val != null && val != undefined) {
        return val;
    } else {
        return key;
    }
}

function GetTableColumnClass(key) {
    var keyVal = {
        "CompanyName": "TdCompanyName",
        "LoginTimeLast": "TdLoginTimeLast",
        "LoginTimeWeb": "TdLoginTimeWeb",
        "PhoneNumber": "TdPhoneNumber",
        "UserEmail": "TdUserEmail",
        "RegTime": "TdRegTime",
        "UserRealName": "TdUserRealName",
        "active": "Tdactive",
        "allCount": "TdallCount",
        "aotjb": "Tdaotjb",
        "aotjbEndtime": "TdaotjbEndtime",
        "dxunity": "Tddxunity",
        "goodsNum": "TdgoodsNum",
        "orderMoney": "TdorderMoney",
        "outlayNum": "TdoutlayNum",
        "returnInsertTime": "TdreturnInsertTime",
        "saleNum": "TdsaleNum",
        "smsNum": "TdsmsNum",
        "userCount": "TduserCount",
        "userNum": "TduserNum",
        "AgentName": "TdagentName"
    };
    var val = keyVal[key];
    if (val != null && val != undefined) {
        return val;
    } else {
        return key;
    }
}



function GetShowColumn() {
    var html = $.trim($("#TableShowItemList").html())
    var ShowColumn;

    if (html == null || html == "") {
        ShowColumn = ["CompanyName", "aotjb", "active", "userNum", "smsNum", "goodsNum", "saleNum", "orderMoney", "outlayNum", "LoginTimeLast"];
    } else {
        ShowColumn = $.parseJSON(html);
    }
    return ShowColumn;
}

var setTableDataTimeout = 0;

function SelectTableThList(obj) {
    clearTimeout(setTableDataTimeout);
    var inputList = $("#SelectTableList input:checkbox:checked");
    var valList = new Array();
    for (var i = 0; i < inputList.length; i++) {
        var val = inputList[i].value;
        valList.push(val);
    }
    $("#TableShowItemList").html(JSON.stringify(valList));

    setTableDataTimeout = setTimeout(SetTableData(), 1500);
}



function SelectAjaxJson() {

    var listJson = new Array();



    var logic = $("#WhereListDiv .WhereLogic");

    $.each(logic, function (x, n) {
        var json = {};
        var list_dom = $(n);
        var OrderProject = new Array();
        var TagInfo = new Array();
        var Active = new Array();
        var SearchByAgent = new Array();

        var list = list_dom.find(".LogicContent .OtherList");

        for (var i = 0; i < list.length; i++) {
            var item = $(list[i]);
            var type = item.attr("wheretype");
            var value = item.attr("value");
            if (type == "OrderProject") {
                //购买订单产品
                OrderProject.unshift(Number(value));
            } else if (type == "TagInfo") {
                //标签信息
                TagInfo.unshift(Number(value));
            } else if (type == "excludeAccount" || type == "soleAccount") {
                json[type] = value.split(',');
            } else if (type == "AccountActive") {
                Active.unshift(Number(value));
            } else if (type == "SearchByAgent") {
                //代理商信息
                SearchByAgent.unshift(Number(value));
            }
            else {
                if (value == "Sole") {
                    //普通 单选
                    json[type] = type;
                } else if (value == "Two") {
                    //选择范围的
                    var max = item.attr("max");
                    var min = item.attr("min");
                    json[type] = {};
                    if (max != "") {
                        json[type]["max"] = max;
                    }
                    if (min != "") {
                        json[type]["min"] = min;
                    }
                } else {
                    json[type] = value;
                }
            }
        }
        if (OrderProject.length > 0) {
            json["OrderProject"] = OrderProject;
        }
        if (TagInfo.length > 0) {
            json["TagInfo"] = TagInfo;
        }
        if (Active.length > 0) {
            json["AccountActive"] = Active;
        }
        if (SearchByAgent.length > 0) {
            json["SearchByAgent"] = SearchByAgent;
        }
        listJson.unshift(json);
    });

    return listJson;
}


function SettingThConfigClick(b) {
    if (b != undefined && b != null) {
        if (b == true) {
            $("#SelectTableList").fadeOut(function () {
                $("#SelectTbaleRowCount").fadeIn();
            });
        } else {

            $("#SelectTbaleRowCount").fadeOut(function () {
                $("#SelectTableList").fadeIn();
            });
        }
    } else {

        if ($("#SelectTbaleRowCount").css("display") == "none") {
            $("#SelectTableList").fadeToggle("slow", function () {
                $("#SelectTbaleRowCount").fadeToggle();
            });
        } else {
            $("#SelectTbaleRowCount").fadeToggle("slow", function () {
                $("#SelectTableList").fadeToggle();
            });
        }
    }
}

function NumberVerification(obj) {
    var value = $(obj).val();
    var newvalue = $.trim(value.replace(/[^\d\.]/g, ''));
    if (value != newvalue) {
        $(obj).val(newvalue);
    }
}
function SetNumberScreenMaxVal() {
    var maxJson = $.parseJSON($("#MaxValueJson").html());
    $.each(maxJson, function (i, n) {
        $(".SelectList[other='" + i + "']").attr("max", Number(n));
    });
}


function NumberScreenCondition(key) {

    var AllJson = {};

    AllJson["RegTimeByTime"] = {
        "name": "注册时间",
        "title": "注册时间",
        "type": "bytime",
        "maxmin": "在 {0} 到 {1} 之间注册",
        "max": "在 {0} 之前注册",
        "min": "在 {0} 之后注册",
        "other": ""
    };
    AllJson["BuyOrderByTime"] = {
        "name": "订单购买时间",
        "title": "购买时间",
        "type": "bytime",
        "maxmin": "在 {0} 到 {1} 之间购买订单",
        "max": "在 {0} 之前购买订单",
        "min": "在 {0} 之后购买订单",
        "other": ""
    };
    AllJson["userNumber"] = {
        "name": "会员数量",
        "title": "会员数量",
        "type": "bynum",
        "maxmin": "会员数在 {0} 到 {1} 之间",
        "max": "会员数小于 {0} ",
        "min": "会员数大于 {0} ",
        "other": "MaxUserNum"
    };
    AllJson["saleNumber"] = {
        "name": "销售笔数",
        "title": "销售笔数",
        "type": "bynum",
        "maxmin": "销售笔数在 {0} 到 {1} 之间",
        "max": "销售数小于 {0} 笔",
        "min": "销售数大于 {0} 笔",
        "other": "MaxSaleNum"
    };
    AllJson["LastLoginTime"] = {
        "name": "连续未登录天数",
        "title": "未登录",
        "type": "bynum",
        "maxmin": "未登录天数在 {0} 到 {1} 之间",
        "max": "未登录小于 {0} 天",
        "min": "未登录大于 {0} 天",
        "other": "MaxLastLoginTime"
    };
    AllJson["orderMoney"] = {
        "name": "订单金额",
        "title": "订单金额",
        "type": "bynum",
        "maxmin": "订单金额在 {0} 到 {1} 之间",
        "max": "订单金额小于 {0} 元",
        "min": "订单金额大于 {0} 元",
        "other": "MaxOrderMoney"
    };
    AllJson["smsNumber"] = {
        "name": "发短信数",
        "title": "发短信数",
        "type": "bynum",
        "maxmin": "短信发送数在 {0} 到 {1} 之间",
        "max": "短信发送小于 {0} 条",
        "min": "短信发送大于 {0} 条",
        "other": "MaxSmsNum"
    };
    AllJson["goodsNumber"] = {
        "name": "商品数量",
        "title": "商品数",
        "type": "bynum",
        "maxmin": "商品数在 {0} 到 {1} 之间",
        "max": "商品数小于 {0} 件",
        "min": "商品数大于 {0} 件",
        "other": "MaxGoodsNum"
    };
    AllJson["outlayNumber"] = {
        "name": "支出数量",
        "title": "支出笔数",
        "type": "bynum",
        "maxmin": "支出笔数在 {0} 到 {1} 之间",
        "max": "支出小于 {0} 笔",
        "min": "支出大于 {0} 笔",
        "other": "MaxOutlayNum"
    };
    AllJson["loginTimes"] = {
        "name": "登录次数",
        "title": "登录次数",
        "type": "bynum",
        "maxmin": "登录次数在 {0} 到 {1} 之间",
        "max": "登录小于 {0} 次",
        "min": "登录大于 {0} 次",
        "other": "MaxAllLoginNum"
    };

    AllJson["smsResidue"] = {
        "name": "剩余短信数",
        "title": "短信数",
        "type": "bynum",
        "maxmin": "短信剩余 {0} 到 {1} 条",
        "max": "短信剩余小于 {0} 条",
        "min": "短信剩余大于 {0} 条",
        "other": ""
    };

    AllJson["useVoucher"] = {
        "name": "使用代金券数",
        "title": "代金券",
        "type": "bynum",
        "maxmin": "使用代金券 {0} 到 {1} 张",
        "max": "使用代金券小于 {0} 张",
        "min": "使用代金券大于 {0} 张",
        "other": ""
    };

    AllJson["accAllIntegral"] = {
        "name": "店铺总积分",
        "title": "积分",
        "type": "bynum",
        "maxmin": "店铺总积分在 {0} 到 {1} 之间",
        "max": "店铺总积分小于 {0} 分",
        "min": "店铺总积分大于 {0} 分",
        "other": ""
    };
    AllJson["accIntegral"] = {
        "name": "店铺积分",
        "title": "积分",
        "type": "bynum",
        "maxmin": "店铺积分在 {0} 到 {1} 之间",
        "max": "店铺积分小于 {0} 分",
        "min": "店铺积分大于 {0} 分",
        "other": ""
    };
    AllJson["accUseIntegral"] = {
        "name": "店铺已使用积分",
        "title": "积分",
        "type": "bynum",
        "maxmin": "店铺已使用积分在 {0} 到 {1} 之间",
        "max": "店铺已使用积分小于 {0} 分",
        "min": "店铺已使用积分大于 {0} 分",
        "other": ""
    };

    AllJson["shopAssistant"] = {
        "name": "店员数量",
        "title": "店员数",
        "type": "bynum",
        "maxmin": "店员数量在 {0} 到 {1} 之间",
        "max": "店员数小于 {0} 人",
        "min": "店员数大于 {0} 人",
        "other": ""
    };

    AllJson["subbranch"] = {
        "name": "分店数",
        "title": "分店数",
        "type": "bynum",
        "maxmin": "分店数量在 {0} 到 {1} 之间",
        "max": "分店数小于 {0} ",
        "min": "分店数大于 {0} ",
        "other": ""
    };

    AllJson["subbranch"] = {
        "name": "分店数",
        "title": "分店数",
        "type": "bynum",
        "maxmin": "分店数量在 {0} 到 {1} 之间",
        "max": "分店数小于 {0} ",
        "min": "分店数大于 {0} ",
        "other": ""
    };
    AllJson["serviceNumber"] = {
        "name": "客服次数",
        "title": "客服次数",
        "type": "bynum",
        "maxmin": "客服次数在 {0} 到 {1} 之间",
        "max": "客服次数小于 {0} 次",
        "min": "客服次数大于 {0} 次",
        "other": ""
    };

    AllJson["continuousLogin"] = {
        "name": "连续登录天数",
        "title": "登录天数",
        "type": "bynum",
        "maxmin": "连续登录天数在 {0} 到 {1} 之间",
        "max": "连续登录小于 {0} 天",
        "min": "连续登录大于 {0} 天",
        "other": ""
    };

    AllJson["finallyLoginTime"] = {
        "name": "最后登录时间",
        "title": "登录时间",
        "type": "bytime",
        "maxmin": "最后登录在 {0} 到 {1} 之间",
        "max": "在 {0} 以后进行登录的",
        "min": "在 {0} 以前进行登录的",
        "other": ""
    };
    AllJson["WhileLoggedOn"] = {
        "name": "某段时间登录过",
        "title": "登录时间",
        "type": "bytime",
        "maxmin": "曾经在 {0} 到 {1} 之间登录过",
        "max": "曾经在 {0} 之前登录过",
        "min": "曾经在 {0} 之后登录过",
        "other": ""
    };


    if (key != null && key != undefined && key != "") {
        var json = AllJson[key];
        if (json != undefined && json != null) {
            return json;
        } else {
            return null;
        }
    } else {
        return AllJson;
    }

}

function TwoSelectTimeShow() {
    var AllJson = NumberScreenCondition();

    $.each(AllJson, function (i, n) {
        $(".SelectWhereBody .SelectOtherWhere .clear").before('<div class="SelectList" where="' + i + '"  dialog="' + n.type + '" other="' + n.other + '">' + n.name + '</div>');
    });

    $(".SelectWhereBody .SelectOtherWhere div[dialog='bytime']").click(function () {
        TwoSelectTimeDiaLog(this);
    });
    $(".SelectWhereBody .SelectOtherWhere div[dialog='bynum']").click(function () {
        TwoSelectNumberDiaLog(this);
    });

    SetNumberScreenMaxVal();


}

function TwoSelectTimeDiaLog(obj) {
    var dom = $(obj);

    var where = dom.attr("where");
    var titleJson = NumberScreenCondition(where);
    if (titleJson != null) {
        $("#TwoSelectTime .TitleTime .Title").html(titleJson.title);
        $("#TwoSelectTime input").val("");
        dialog({
            id: "TwoSelectTimeDialog",
            title: "请选择" + titleJson.name,
            lock: true,
            background: '#000', // 背景色
            opacity: 0.5,	// 透明度
            content: document.getElementById("TwoSelectTime"),
            ok: function () {

                var maxValue = $("#TwoSelectTime .MaxTimeValue").val();
                var minValue = $("#TwoSelectTime .MinTimeValue").val();

                var html = '<div class="OtherList" wheretype="' + where + '" value="Two" max="' + maxValue + '" min="' + minValue + '">';

                if (maxValue.length > 0 && minValue.length > 0) {
                    html += titleJson.maxmin.replace("{0}", minValue).replace("{1}", maxValue);
                } else if (maxValue.length > 0 && minValue == "") {
                    html += titleJson.max.replace("{0}", maxValue);
                } else if (maxValue == "" && minValue.length > 0) {
                    html += titleJson.min.replace("{0}", minValue);
                } else {
                    return true;
                }
                html += "</div>";
                if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + where + "']").size() > 0) {
                    $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + where + "']").replaceWith(html);
                } else {
                    $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
                }

                $(obj).addClass("SelectListSelect");
                WhereItemOtherListShow();
            },
            cancelValue: "移除",
            cancel: function () {
                if ($(obj).hasClass("SelectListSelect")) {
                    $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + where + "']").remove();
                    $(obj).removeClass("SelectListSelect");
                    WhereItemOtherListShow();
                }
            }
        }).show();
    } else {
        dom.remove();
    }

}



function TwoSelectNumberDiaLog(obj) {
    var dom = $(obj);

    var where = dom.attr("where");
    var titleJson = NumberScreenCondition(where);
    if (titleJson != null) {
        $("#TwoSelectNumber .TitleTime .Title").html(titleJson.title);
        $("#TwoSelectNumber input").val("");

        var maxVal = dom.attr("max");
        $("#TwoSelectNumber .MaxTime .MaxTimeValue").attr("placeholder", maxVal);


        dialog({
            id: "TwoSelectNumberDialog",
            title: "请选择" + titleJson.name,
            lock: true,
            background: '#000', // 背景色
            opacity: 0.5,	// 透明度
            content: document.getElementById("TwoSelectNumber"),
            ok: function () {

                var maxValue = $("#TwoSelectNumber .MaxTimeValue").val();
                var minValue = $("#TwoSelectNumber .MinTimeValue").val();

                var html = '<div class="OtherList" wheretype="' + where + '" value="Two" max="' + maxValue + '" min="' + minValue + '">';

                if (maxValue.length > 0 && minValue.length > 0) {
                    html += titleJson.maxmin.replace("{0}", minValue).replace("{1}", maxValue);
                } else if (maxValue.length > 0 && minValue == "") {
                    html += titleJson.max.replace("{0}", maxValue);
                } else if (maxValue == "" && minValue.length > 0) {
                    html += titleJson.min.replace("{0}", minValue);
                } else {
                    return true;
                }
                html += "</div>";
                if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + where + "']").size() > 0) {
                    $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + where + "']").replaceWith(html);
                } else {
                    $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
                }

                $(obj).addClass("SelectListSelect");
                WhereItemOtherListShow();
            },
            cancelValue: "移除",
            cancel: function () {
                if ($(obj).hasClass("SelectListSelect")) {
                    $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + where + "']").remove();
                    $(obj).removeClass("SelectListSelect");
                    WhereItemOtherListShow();
                }
            }
        }).show();
    } else {
        dom.remove();
    }

}