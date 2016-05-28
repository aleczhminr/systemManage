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
                            title = "汇总数据信息";
                            break;
                        case 6:
                            title = "用户标签";
                            break;
                        case 7:
                            title = "自定义条件";
                            break;
                        case 8:
                            title = "自定义周期数据";
                            break;
                        default:
                            break;
                    }
                    $(".SelectWhereBody").append("<div class='SelectWhereTitle' onclick='TriggerLabel(this)' group='" + n.ConditionGroup + "'><div>" + title + "<span class='pg-arrow_minimize_line' style='margin-left: 5px;margin-top: 3px'></span></div></div>" +
                        "<div class='SelectWhereItem m-l-10 m-t-10'></div>");
                }

                $(".SelectWhereTitle[group='" + n.ConditionGroup + "']").next(".SelectWhereItem").append("<div class='SelectList' name='" + n.ColName + "' group='" + n.ConditionGroup + "' tName='" + n.TableName + "' cId='" + n.Id + "' desc='" + n.ColDesc + "' where='" + n.ConditionType + "' onclick='Show" + n.ConditionType + "(this)'>" + n.ColDesc + "</div>");
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
function SetItemCondition(id, colName, tabName, colDesc, conditionType, conditionGroup, range, remark) {
    var model = {
        Id: id,
        ColName: colName,
        TableName: tabName,
        ColDesc: colDesc,
        ConditionType: conditionType,
        ConditionGroup: conditionGroup,
        DataRange: range,
        Remark: remark
    };

    conditionList.push(model);
}

//移除一个条件
function RemoveItemCondition(id) {
    for (var i = 0; i < conditionList.length; i++) {
        if (id == conditionList[i].Id)
            conditionList.splice(i);
    }
}


//不同条件的条件类型处理方法
function ShowStrPair(obj) {
    var colName = $(obj).attr("name");
    var desc = $(obj).attr("desc");
    var tName = $(obj).attr("tName");
    var id = $(obj).attr("cId");
    var type = $(obj).attr("where");
    var group = $(obj).attr("group");

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

            var remark = "";

            dataRange.Max = $("#TwoSelectNumber .MaxTimeValue").val();
            dataRange.Min = $("#TwoSelectNumber .MinTimeValue").val();

            var html = '<div class="OtherList" wheretype="' + id + '">';

            if (dataRange.Max.length > 0 && dataRange.Min.length > 0) {
                html += desc + "在 <span style='color:red'>" + dataRange.Min + "</span> 到 <span style='color:red'>" + dataRange.Max + "</span> 之间";
                remark += desc + "在【" + dataRange.Min + "】到【" + dataRange.Max + "】之间";
            } else if (dataRange.Max.length > 0 && dataRange.Min == "") {
                html += desc + "小于等于<span style='color:red'>" + dataRange.Max + "</span>";
                remark += desc + "小于等于【" + dataRange.Max + "】";
            } else if (dataRange.Max == "" && dataRange.Min.length > 0) {
                html += desc + "大于等于<span style='color:red'>" + dataRange.Min + "</span>";
                remark += desc + "大于等于【" + dataRange.Min + "】";
            } else {
                return true;
            }
            html += "</div>";
            if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").size() > 0) {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").replaceWith(html);

                RemoveItemCondition(id);
            } else {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
            }
            //alert(html);
            SetItemCondition(id, colName, tName, desc, type, group, dataRange, remark);
            //console.log(conditionList);

            $(obj).addClass("SelectListSelect");
            WhereItemOtherListShowEx();
        },
        cancelValue: "移除",
        cancel: function () {
            if ($(obj).hasClass("SelectListSelect")) {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").remove();
                $(obj).removeClass("SelectListSelect");
                WhereItemOtherListShowEx();
            }
        }
    }).show();

}

function ShowStrRange(obj) {
    ShowIntRange(obj);
}

function ShowIntPair(obj) {
    ShowStrPair(obj);
}

function ShowSpecRange(obj) {
    var colName = $(obj).attr("name");
    var desc = $(obj).attr("desc");
    var tName = $(obj).attr("tName");
    var id = $(obj).attr("cId");
    var type = $(obj).attr("where");
    var group = $(obj).attr("group");

    if (!$(obj).hasClass("SelectListSelect")) {
        if (colName == "specificAccId") {
            dialog({
                id: "SpecRangeSelect",
                title: "请输入要查询的店铺ID",
                lock: true,
                background: '#000', // 背景色
                opacity: 0.5,	// 透明度
                content: "<div class='ExcludeContent'><textarea id='soleAccountText' cols='20' rows='2'></textarea></div><div class='ExcludeTitle'>店铺ID之间请用','分开</div>",
                ok: function () {
                    var dataRange = {
                        Max: 0,
                        Min: 0,
                        Range: new Array()
                    };

                    var list = $("#soleAccountText").val();

                    var remark = "";

                    if (list != "") {
                        list = $.trim(list.replace(/[^\d\,]/g, ''));
                        dataRange.Range = list.split(',');

                        var html = '<div class="OtherList" wheretype="' + id + '" value="' + list + '">只查询<span style="color:red;font-weight: bold;"> ' + dataRange.Range.length + ' </span>个店铺</div>';
                        remark += "只查询【" + dataRange.Range.length + "】个店铺";

                        if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").size() > 0) {
                            RemoveItemCondition(id);
                            SetItemCondition(id, colName, tName, desc, type, group, dataRange, remark);
                            $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").replaceWith(html);
                        } else {
                            SetItemCondition(id, colName, tName, desc, type, group, dataRange, remark);
                            $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
                        }

                        $(obj).addClass("SelectListSelect");
                        WhereItemOtherListShowEx();
                    } else {
                        return false;
                    }
                },
                cancelValue: "移除",
                cancel: function () {
                }
            }).show();
        } else if (colName == "specificEx") {    //排除以下店铺
            dialog({
                id: "SpecRangeExSelect",
                title: "请输入要排除的店铺ID",
                lock: true,
                background: '#000', // 背景色
                opacity: 0.5,	// 透明度
                content: "<div class='ExcludeContent'><textarea id='soleExAccountText' cols='20' rows='2'></textarea></div><div class='ExcludeTitle'>店铺ID之间请用','分开</div>",
                ok: function () {
                    var dataRange = {
                        Max: 0,
                        Min: 0,
                        Range: new Array()
                    };

                    var list = $("#soleExAccountText").val();

                    var remark = "";

                    if (list != "") {
                        list = $.trim(list.replace(/[^\d\,]/g, ''));
                        dataRange.Range = list.split(',');

                        var html = '<div class="OtherList" wheretype="' + id + '" value="' + list + '">排除<span style="color:red;font-weight: bold;"> ' + dataRange.Range.length + ' </span>个店铺</div>';
                        remark += "只查询【" + dataRange.Range.length + "】个店铺";

                        if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").size() > 0) {
                            RemoveItemCondition(id);
                            SetItemCondition(id, colName, tName, desc, type, group, dataRange, remark);
                            $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").replaceWith(html);
                        } else {
                            SetItemCondition(id, colName, tName, desc, type, group, dataRange, remark);
                            $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
                        }

                        $(obj).addClass("SelectListSelect");
                        WhereItemOtherListShowEx();
                    } else {
                        return false;
                    }
                },
                cancelValue: "移除",
                cancel: function () {
                }
            }).show();
        } else if (colName == "areaQuery") {    //地区筛选
            dialog({
                id: "SpecRangeExSelect",
                title: "请输入要查询的地区",
                lock: true,
                background: '#000', // 背景色
                opacity: 0.5,	// 透明度
                content: "<div class='ExcludeContent'><textarea id='soleExAccountText' cols='20' rows='2'></textarea></div><div class='ExcludeTitle'>地区之间请用','分开</div>",
                ok: function () {
                    var dataRange = {
                        Max: 0,
                        Min: 0,
                        Range: new Array()
                    };

                    var list = $("#soleExAccountText").val();

                    var remark = "";

                    if (list != "") {
                        //list = $.trim(list.replace(/[^\d\,]/g, ''));
                        dataRange.Range = list.split(',');

                        var tmpStr = "";
                        $.each(dataRange.Range, function (i, n) {
                            tmpStr += n + " ";
                        });

                        var html = '<div class="OtherList" wheretype="' + id + '" value="' + list + '">在<span style="color:red;font-weight: bold;"> ' + tmpStr + ' </span>的店铺</div>';
                        remark += "只查询在【" + tmpStr + "】的店铺";

                        if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").size() > 0) {
                            RemoveItemCondition(id);
                            SetItemCondition(id, colName, tName, desc, type, group, dataRange, remark);
                            $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").replaceWith(html);
                        } else {
                            SetItemCondition(id, colName, tName, desc, type, group, dataRange, remark);
                            $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
                        }

                        $(obj).addClass("SelectListSelect");
                        WhereItemOtherListShowEx();
                    } else {
                        return false;
                    }
                },
                cancelValue: "移除",
                cancel: function () {
                }
            }).show();
        }

    } else {
        $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='soleAccount']").remove();
        $(obj).removeClass("SelectListSelect");

        WhereItemOtherListShow();
    }
}

function ShowTimePair(obj) {
    var colName = $(obj).attr("name");
    var desc = $(obj).attr("desc");
    var tName = $(obj).attr("tName");
    var id = $(obj).attr("cId");
    var type = $(obj).attr("where");
    var group = $(obj).attr("group");

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

            var remark = "";

            var html = '<div class="OtherList" wheretype="' + id + '">';
            if (dataRange.Max.length > 0 && dataRange.Min.length > 0) {
                html += desc + "在 <span style='color:red'>" + dataRange.Min + "</span> 到 <span style='color:red'>" + dataRange.Max + "</span> 之间";
                remark += desc + "在【" + dataRange.Min + "】到【" + dataRange.Max + "】之间";
            } else if (dataRange.Max.length > 0 && dataRange.Min == "") {
                html += desc + "在 <span style='color:red'>" + dataRange.Max + "</span> 之前";
                remark += desc + "在【" + dataRange.Max + "】之前";
            } else if (dataRange.Max == "" && dataRange.Min.length > 0) {
                html += desc + "在 <span style='color:red'>" + dataRange.Min + "</span> 之后";
                remark += desc + "在【" + dataRange.Min + "】之后";
            } else {
                return true;
            }

            SetItemCondition(id, colName, tName, desc, type, group, dataRange, remark);

            html += "</div>";
            if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").size() > 0) {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").replaceWith(html);

                RemoveItemCondition(id);
            } else {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
            }

            $(obj).addClass("SelectListSelect");
            WhereItemOtherListShowEx();
        },
        cancelVal: "移除",
        cancel: function () {
            if ($(obj).hasClass("SelectListSelect")) {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").remove();
                $(obj).removeClass("SelectListSelect");
                WhereItemOtherListShowEx();
            }
        }
    }).show();
}


function SelectDataSetEx() {
    HideSelectWhereBody();

    //SettingThConfigClick(true);

    //var json = SelectAjaxJson();
    var url = "/FiltrateData/GetFilterDataSet";

    //$("#sk_loading").show();
    $("#loading").removeClass("hidden");
    $.ajax({
        type: "POST",
        url: url,
        data: { "postJson": JSON.stringify(conditionList) },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(XMLHttpRequest.responseText);
            alert("查询数据出错");
        },
        success: function (msg) {

            $("#loading").addClass("hidden");
            var Column = GetShowColumn();
            if (msg == "nullJson" || msg == "" || msg == null || msg == "null") {

                var trHtml = '<tr ><td colspan="' + Column.length + '" class="DataNullJson">没有查到符合的店铺信息</td></tr>';
                $("#RequstDataTable tbody").html(trHtml);

                $("#SelectTbaleRowCount span").html("0");
            }
            else if (msg != "error!") {
                $("#GetDataJson").html(msg);
                SetTableData();
            } else {
                alert("查询数据出错");
            }
        }
    });
}

function WhereItemOtherListShowEx() {

    $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList").attr("onclick", "RemoveWhereItemEx(this)");
}


function RemoveWhereItemEx(obj) {
    var dom = $(obj);
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
            $(".SelectWhereBody .SelectList[cid='" + type + "']").removeClass("SelectListSelect");
            RemoveItemCondition(type);
            console.log(conditionList);
        }
    }

}

function SelectAllAgent(obj) {
    if (obj.value == "0") {
        $("#SelectItemList .SelectList").addClass("SelectListSelect");
        obj.value = "1";
    } else {
        $("#SelectItemList .SelectList").removeClass("SelectListSelect");
        obj.value = "0";
    }
}

function SelectCommonAgent(obj) {
    if (obj.value == "2") {
        $("#SelectItemList .Group").addClass("SelectListSelect");
        obj.value = "3";
    } else {
        $("#SelectItemList .Group").removeClass("SelectListSelect");
        obj.value = "2";
    }
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


function ShowIntRange(obj) {
    $("#SelectItemList").html("<div class='clear'></div>");

    var colName = $(obj).attr("name");
    var desc = $(obj).attr("desc");
    var tName = $(obj).attr("tName");
    var id = $(obj).attr("cId");
    var type = $(obj).attr("where");
    var group = $(obj).attr("group");

    var ur = "/FiltrateData/GetRangeItem";

    var postJson = [];
    postJson["colName"] = colName;

    $.doAjax(ur, { "colName": colName }, function (msg) {
        if (msg != "[]") {
            var json = $.parseJSON(msg);

            //关于代理商分组的操作
            if (colName == "AgentId") {
                var html = '<div class="m-l-5"><button type="button" value="0" class="btn btn-success " onclick="SelectAllAgent(this)">全选</button>' +
                '<button type="button" value="0" class="btn btn-danger m-l-5" onclick="SelectCommonAgent(this)">常用分组</button></div>';
                $("#SelectItemList .clear").before(html);

                $.each(json, function (i, n) {
                    var html = "";
                    if (n.extInfo == 1) {
                        html = '<div class="SelectList Group" value="' + n.id + '">' + n.t_Name + '</div>';
                    } else {
                        html = '<div class="SelectList " value="' + n.id + '">' + n.t_Name + '</div>';
                    }

                    $("#SelectItemList .clear").before(html);
                });
            }
            else if (colName == "industry") { //行业筛选条件
                var html = '<div class="m-l-5"><button type="button" value="0" rel=sale class="btn btn-success " onclick="SelectIndustry(this)">零售</button>' +
                '<button type="button" value="0" class="btn btn-danger m-l-5" rel=beauty onclick="SelectIndustry(this)">丽人</button>' +
                '<button type="button" value="0" class="btn btn-primary m-l-5" rel=life onclick="SelectIndustry(this)">生活服务</button>' +
                    '<button type="button" value="0" class="btn btn-info m-l-5" rel=enter onclick="SelectIndustry(this)">休闲娱乐</button>' +
                    '<button type="button" value="0" class="btn btn-warning m-l-5" rel=food onclick="SelectIndustry(this)">餐饮</button></div>';

                $("#SelectItemList .clear").before(html);

                $.each(json, function (i, n) {
                    var html = "";

                    html = '<div class="SelectList " rel="' + n.extInfo + '" value="' + n.id + '">' + n.t_Name + '</div>';

                    $("#SelectItemList .clear").before(html);
                });

            } else {
                $.each(json, function (i, n) {
                    var html = "";
                    //if (n.AgentGroup == 1) {
                    //    html = '<div class="SelectList Group" value="' + n.id + '">' + n.AgentName + '</div>';
                    //} else {
                    html = '<div class="SelectList " rel="' + n.extInfo + '" value="' + n.id + '">' + n.t_Name + '</div>';
                    //}

                    $("#SelectItemList .clear").before(html);
                });
            }


            $("#SelectItemList .SelectList").click(function () {
                SelectListClick(this);
            });
        }
    }, true);

    dialog({
        id: "SelectItemDialog",
        title: "请选择" + desc,
        lock: true,
        background: '#000', // 背景色
        opacity: 0.5,	// 透明度
        content: document.getElementById("SelectItemList"),
        ok: function () {
            var dataRange = {
                Max: 0,
                Min: 0,
                Range: new Array()
            };

            var strDesc = "";
            var remark = "";

            $("#SelectItemList .SelectListSelect").each(function () {
                dataRange.Range.push($(this).attr("value"));
                strDesc += $(this).html() + ",";
            });


            if (strDesc != "") {
                strDesc = strDesc.substring(0, strDesc.length - 1);
                remark = desc + "为【" + strDesc + "】";
                //alert(strDesc);                

                var html = '<div class="OtherList" wheretype="' + id + '">';

                html += desc + "为 <span style='color:red'>" + strDesc + "</span>";
                html += "</div>";
                if ($("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").size() > 0) {
                    $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").replaceWith(html);

                    RemoveItemCondition(id);
                    SetItemCondition(id, colName, tName, desc, type, group, dataRange, remark);
                } else {
                    $("#WhereListDiv .WhereLogicSelect .LogicContent .clear").before(html);
                    SetItemCondition(id, colName, tName, desc, type, group, dataRange, remark);
                }

                $(obj).addClass("SelectListSelect");
                WhereItemOtherListShowEx();
            } else {
                return false;
            }

        },
        cancelValue: "移除",
        cancel: function () {
            if ($(obj).hasClass("SelectListSelect")) {
                $("#WhereListDiv .WhereLogicSelect .LogicContent .OtherList[wheretype='" + id + "']").remove();
                $(obj).removeClass("SelectListSelect");
                WhereItemOtherListShowEx();
            }
        }
    }).show();
}

//选择大行业
function SelectIndustry(obj) {
    var keyword = $(obj).attr('rel');

    switch (keyword) {
        case "sale":
            if (obj.value == "0") {
                $("#SelectItemList [rel='零售']").addClass("SelectListSelect");
                obj.value = "1";
            } else {
                $("#SelectItemList [rel='零售']").removeClass("SelectListSelect");
                obj.value = "0";
            }
            break;
        case "beauty":
            if (obj.value == "0") {
                $("#SelectItemList [rel='丽人']").addClass("SelectListSelect");
                obj.value = "1";
            } else {
                $("#SelectItemList [rel='丽人']").removeClass("SelectListSelect");
                obj.value = "0";
            }
            break;
        case "life":
            if (obj.value == "0") {
                $("#SelectItemList [rel='生活服务']").addClass("SelectListSelect");
                obj.value = "1";
            } else {
                $("#SelectItemList [rel='生活服务']").removeClass("SelectListSelect");
                obj.value = "0";
            }
            break;
        case "enter":
            if (obj.value == "0") {
                $("#SelectItemList [rel='休闲娱乐']").addClass("SelectListSelect");
                obj.value = "1";
            } else {
                $("#SelectItemList [rel='休闲娱乐']").removeClass("SelectListSelect");
                obj.value = "0";
            }
            break;
        case "food":
            if (obj.value == "0") {
                $("#SelectItemList [rel='餐饮']").addClass("SelectListSelect");
                obj.value = "1";
            } else {
                $("#SelectItemList [rel='餐饮']").removeClass("SelectListSelect");
                obj.value = "0";
            }
            break;
        default:
    }
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