
var globalData = "";

//详情信息
function GetShopSummarize() {
    var accid = $("#ShopInfo").val();

    var url = "/shopinfo/GetShopSummarize/" + accid;
    $.doAjax(url, null, function (msg) {
        //msg 返回结果
        if (msg != null) {
            var json = $.parseJSON(msg);

            $("#regTimeDiv").html(template("regTime", json));
            $("#endTimeDiv").html(template("endTime", json));
            $("#orderNum").html(template("orderNumber", json));
            $("#AccountInfo").prepend(template("ShopSummarizeTemplate", json));
            $("#loginTimeDiv").html(template("loginTime", json));
            $("#smsSendAllNum").html(json.smsNum);
            $("#behaveIndex").html("(行为指数:" + json.behaveIndex + ")");

        }
    }, true);
}

//获取店铺登录端详情
function GetLogDetail() {
    var accid = $("#ShopInfo").val();

    var url = "/shopinfo/GetLatestLogClient/" + accid;
    $.doAjax(url, null, function (msg) {
        //msg 返回结果
        if (msg != null) {
            switch (msg) {
                case "10":
                    $("#lastClient").html("<i class='p-r-10 fa fa-apple fs-16' data-toggle='tooltip' data-placement='left'' title='iPhone登录'></i>");
                    break;
                case "11":
                    $("#lastClient").html("<i class='p-r-10 fa fa-android fs-16 android' data-toggle='tooltip' data-placement='left'' title='安卓登录'></i>");
                    break;
                case "13":
                    $("#lastClient").html("<i class='p-r-10 fa fa-apple fs-16' data-toggle='tooltip' data-placement='left'' title='iPad登录'></i>");
                    break;
                case "0":
                    $("#lastClient").html("<i class='p-r-10 fa pg-ui fs-16 text-primary' data-toggle='tooltip' data-placement='left'' title='网页登录'></i>");
                    break;
                case "8":
                    $("#lastClient").html("<i class='p-r-10 fa fa-desktop text-complete fs-16' data-toggle='tooltip' data-placement='left'' title='客户端登录'></i>");
                    break;
                default:
                    break;
            }
        }
    }, true);
}

function GetLastClient() {
    var accid = $("#ShopInfo").val();

    var url = "/shopinfo/GetLogDetail/" + accid;
    $.doAjax(url, null, function (msg) {
        //msg 返回结果
        if (msg != null) {

            $("#logDetail").attr('title', msg);

        }
    }, true);
}


// 用户回访反馈显示
function GetVisitStatus() {
    var accid = $("#ShopInfo").val();

    var url = "/shopinfo/GetVisitStatus/" + accid;
    $.doAjax(url, null, function (msg) {
        //msg 返回结果
        if (msg != null && msg != "") {
            var json = $.parseJSON(msg);
            var $lastTime = $("#userReturn .lastTime"),
				$period = $("#userReturn .period").html(json.Period),
				$visitWay = $("#userReturn .source");

            $lastTime.html(json.LastVisitTime.substr(0, 10));

            // 判断回访天数
            if (json.Period <= 7) {
                $lastTime.css("color", "#c64643");
            } else if (json.Period > 7 && json.Period <= 30) {
                $lastTime.addClass("text-danger");
            } else {
                $lastTime.css("color", "#f77975");
            }

            // 判断回访方式
            if (json.VisitWay != null) {
                var str = json.LastVisitTime + " (" + json.VisitWay + ")";
                if (json.VisitWay == "电话") {
                    $visitWay.html("<i class='fa fa-phone' title='" + str + "'>");
                } else if (json.VisitWay == "QQ") {
                    $visitWay.html("<i class='fa fa-qq' title='" + str + ">'");

                } else if (json.VisitWay == "邮件") {
                    $visitWay.html("<i class='fa pg-mail' title='" + str + "'>");

                } else if (json.VisitWay == "微信") {
                    $visitWay.html("<i class='fa pg-weixin' title='" + str + "'>");

                } else if (json.VisitWay == "短信") {
                    $visitWay.html("<i class='fa pg-phone' title='" + str + "'>");

                } else if (json.VisitWay == "论坛") {
                    $visitWay.html("<i class='fa pg-form' title='" + str + "'>");


                };
                // $visitWay.html(json.VisitWay);
            } else {
                $visitWay.html("-");
            }
        } else {
            $("#userReturn").html("<span class='fs-12 hint-text'>尚无回访记录</span>");
        }
    }, true);
}

function AppListShow() {
    var accid = $("#ShopInfo").val();

    var url = "/shopinfo/GetAppList/" + accid;
    $.doAjax(url, null, function (msg) {
        //msg 返回结果
        if (msg != null) {
            var json = $.parseJSON(msg);
            var html = template("AppList-Tpl", json);
            dialog({
                id: "addnewappkeydialog",
                title: 'App管理',
                content: html
            }).showModal();
        }
    }, true);


}

function SetAlipayActive(accId) {
    dialog({
        id: "alipayShowDivdialog",
        title: "支付宝补充信息",
        lock: true,
        background: '#fff', // 背景色
        opacity: 0.5, // 透明度
        content: document.getElementById("alipayShowDiv"),
        ok: function () {
            var ur = "/shopinfo/SetAlipay";

            var postJson = {};
            postJson["accId"] = accId;
            postJson["alipayAccount"] = $("#aliAccount").val();
            postJson["alipayPid"] = $("#aliPid").val();
            postJson["alipayKey"] = $("#aliKey").val();

            $.doAjax(ur, postJson, function (msg) {
                if (msg == "2") {
                    dialog({
                        content: '开通成功！',
                        quickClose: true // 点击空白处快速关闭
                    }).show();
                    dialog.get("addnewappkeydialog").close().remove();
                    AppListShow();
                } else {
                    alert("设置权限失败");
                }
            }, true);
        }
    }).showModal();
}

function SetAppActive(appKey) {
    var accid = $("#ShopInfo").val();
    var url = "/shopinfo/SetAppActive";

    $.doAjax(url, { "accid": accid, "appkey": appKey }, function (msg) {
        //msg 返回结果
        if (msg != "0") {
            dialog.get("addnewappkeydialog").close().remove();
            AppListShow();
        } else {
            alert("设置权限失败");
        }
    }, true);
}

function SetAppClose(appKey, appName) {

    dialog({
        content: '您确认要关闭此店铺【' + appName + '】的授权吗？',
        icon: 'question',
        okValue: "确定",
        ok: function () {
            var accid = $("#ShopInfo").val();
            var url = "/shopinfo/RemoveAccountApp";
            $.doAjax(url, { "accid": accid, "appkey": appKey }, function (msg) {
                //msg 返回结果
                if (msg == "1") {
                    dialog.get("addnewappkeydialog").close().remove();
                    AppListShow();
                } else {
                    alert("移除权限失败");
                }
            }, true);
            return true;
        },
        cancelValue: '关闭',
        cancel: true //为true等价于function(){}
    }).show();
}



function GetSubranchList() {
    var accid = $("#ShopInfo").val();
    var url = "/shopinfo/SubbranchList/" + accid;
    $.doAjax(url, null, function (msg) {
        //msg 返回结果
        if (msg != null) {
            var json = $.parseJSON(msg);

            var html = "";
            if (json.headquerters != null) {
                if (json.headquerters.ID == accid) {
                    html += '<div style="font-weight:bold;color:green;">总店：' + json.headquerters.CompanyName + '【' + json.headquerters.ID + '】</div>';
                } else {
                    html += '<div style="font-weight:bold;">总店：' + json.headquerters.CompanyName + '【' + json.headquerters.ID + '】</div>';
                }

            }
            if (json.subbranch != null) {
                $.each(json.subbranch, function (i, n) {
                    if (i == 0) {
                        html += '<span style="font-weight:bold;">分店列表：</span><br/>';
                    }
                    if (n.ID == accid) {
                        html += "<span style='color:green;'>" + n.CompanyName + "【" + n.ID + "】</span><br/>";
                    } else {
                        html += "<span>" + n.CompanyName + "【" + n.ID + "】</span><br/>";
                    }

                });
            }

            if (html == "") {
                html = "未查询到与此店相关的分店信息";
            }



            dialog({
                title: "分店信息",
                content: html
            }).showModal();
        }
    }, true);
}


function GetAccountUserList() {
    var accid = $("#ShopInfo").val();
    var url = "/shopinfo/UserList/" + accid;
    $.doAjax(url, null, function (msg) {
        //msg 返回结果
        if (msg != null) {
            var json = $.parseJSON(msg);
            var html = template("AccountUserListScript", { "list": json });
            dialog({
                id: "accountUserListDialog",
                title: '店员账号管理',
                content: html
            }).showModal();
        }
    }, true);
}
function ColumnTdClick(t, type) {
    var text = $(t).text();
    if ($("#sys_input_" + type).size() < 1) {
        var html = '<input id="sys_input_' + type + '" class="sysUpdateInput" onblur="ColumnSaving(this,\'' + type + '\')" type="text" value="' + text + '" />';
        $(t).html(html);
    }
    $("#sys_input_" + type).focus();
}

function ColumnSaving(t, type) {
    var accid = $("#ShopInfo").val();
    var ur = "/shopinfo/UpdateSysAccount/";
    $.ajax({
        type: "POST",
        url: ur,
        data: { "col": type, "val": $(t).val(), "accid": accid },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(XMLHttpRequest.responseText);
        },
        success: function (msg) {
            if (msg == "-1") {
                alert("异常错误，请联系技术人员");
            } else if (msg == "1") {
                var obj = $(t).parent();
                obj.html($(t).val());
            } else {
                alert("保存错误，请联系高级客服");
            }
        }
    });
}

function GetSysInfoList() {
    var accid = $("#ShopInfo").val();
    var url = "/shopinfo/SysAccountInfo/" + accid;
    $.doAjax(url, null, function (msg) {
        if (msg != null && msg != "" && msg != "null") {
            var json = jQuery.parseJSON(msg);

            var sys_qq = "";
            var feedbackQQ = "";
            var feedbackTel = "";
            var sys_tel = "";
            var aboutAddress = "";

            $.each(json, function (i, n) {
                if (n == null) {
                    n = "";
                }
                $("#" + i).html(n);
                if (i == "feedbackQQ") {
                    feedbackQQ = n;
                } else if (i == "feedbackTel") {
                    feedbackTel = n;
                } else if (i == "sys_qq") {
                    sys_qq = n;
                } else if (i == "sys_tel") {
                    sys_tel = n;
                } else if (i == "aboutAddress") {
                    aboutAddress = n;
                }
            });

            var HuiFangHtml = "";
            if (sys_qq != "") {
                HuiFangHtml += "系统QQ<span class='m-l-5 m-r-20 bold'>【" + sys_qq + "】</span>";
            }
            if (sys_tel != "") {
                HuiFangHtml += "系统电话<span class='m-l-5 m-r-20 bold'>【" + sys_tel + "】</span>";
            }
            if (feedbackQQ != "") {
                HuiFangHtml += "反馈QQ<span class='m-l-5 m-r-20 bold'>【" + feedbackQQ + "】</span>";
            }
            if (feedbackTel != "") {
                HuiFangHtml += "反馈电话<span class='m-l-5 m-r-20 bold'>【" + feedbackTel + "】</span>";
            } else {
                HuiFangHtml += "暂无信息";
            }
            $("#sysHuiFangHelp .txtContent").html(HuiFangHtml);

            if (aboutAddress != "") {
                $("#AccountAddress .aboutAddrees").html("【" + aboutAddress + "】");
            } else {
                $("#AccountAddress .aboutAddrees").html(" 未知 ");
            }
        }
    });


}
function SysInfoListButtonClick() {
    $("#SysShopInfoItemInfoDiv").toggle();
}


function PortraitInfo() {
    if ($("#userPortrait").hasClass("hidden")) {
        $("#userPortrait").removeClass("hidden");

        var url = "/UserPortrait/GetDicItem";

        for (var i = 1; i < 8; i++) {
            $("#type_" + i).html("<option value='0' rel=''>---</option>");
        }

        $.doAjax(url, null, function (msg) {
            if (msg != "") {
                var json = jQuery.parseJSON(msg);

                globalData = json;

                $.each(json.DataList, function (i, n) {
                    $("#type_" + n.ItemType).append("<option value='" + n.Id + "' rel='" + n.ParentId + "'>" + n.Keyword + "</option>");
                    //$("#prefixAgent").append("<option value='" + n.id + "' rel='" + n.remark + "'>" + n.preName + "[" + n.agentId + "]</option>");

                });
            }
        }, true);

        setTimeout(GetUserExtInfo, 800);

    } else {
        $("#userPortrait").addClass("hidden");
    }

}

function GetUserExtInfo() {
    var postJson = {};

    postJson["accid"] = $("#ShopInfo").val();
    var url = "/UserPortrait/GetUserExtInfo";

    $("#industryMain_Tag").html("");
    $("#industrySecondary_Tag").html("");
    $("#userSource_Tag").html("");
    $("#sourceKeyword_Tag").html("");
    $("#choiceReason_Tag").html("");
    $("#needType_Tag").html("");
    $("#attitude_Tag").html("");
    $("#businessExp_Tag").html("");
    $("#mainQuestion_Tag").html("");


    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);

            var text = "";
            var rel = "";

            var industryArr = json.Industry.split(",");
            var sourceArr = json.UserSourcePortrait.split(",");
            var choiceReasonArr = json.ChoiceReason.split(",");

            var potentialNeedArr = json.PotentialNeed.split(",");
            if (json.MainQuestion != null) {
                var mainQuestionArr = json.MainQuestion.split(",");
                $.each(mainQuestionArr, function (i, n) {
                    if (n !== "" && n !== "undefined") {
                        text = $(".select_area option").filter("[value='" + n + "']").text();
                        rel = $(".select_area option").filter("[value='" + n + "']").attr("rel");

                        if (rel != "" && rel != 0) {
                            $("#mainQuestion_Tag").append("<div class='bootstrap-tagsinput'><span class='tag label " +
                                "label-info no-padding p-l-10 p-r-10 m-r-10 inline' " +
                                "style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
                                "tagid='" + n + "'>" + text + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

                            $("#mainQuestion_val").val($("#mainQuestion_val").val() + n + ",");
                        } else {
                            $("#mainQuestion_Tag").append("<div class='bootstrap-tagsinput'><span class='tag label " +
                                "label-info no-padding p-l-10 p-r-10 m-r-10 inline' " +
                                "style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
                                "tagid='" + n + "'>" + text + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

                            $("#mainQuestion_val").val($("#mainQuestion_val").val() + n + ",");
                        }
                    }
                });
            }
            
            //大行业分类和小行业分类设置为单选
            if (industryArr.length >= 2) {
                $("#type_1").val(industryArr[0]);
                $("#type_2").val(industryArr[1]);
            }
            else if (industryArr.length == 1) {
                $("#type_1").val(industryArr[0]);
            }

            //var competingProductArr = json.CompetingProduct.split(",");
            //var remarkArr = json.RemarkId.split(",");
            //$.each(industryArr, function (i, n) {
                //if (n !== "" && n !== "undefined") {
                //        text = $(".select_area option").filter("[value='" + n + "']").text();
                //        rel = $(".select_area option").filter("[value='" + n + "']").attr("rel");
                //        if (rel != "" && rel != 0) {
                //            $("#industrySecondary_Tag").append("<div class='bootstrap-tagsinput'><span class='tag label " +
                //                "label-info no-padding p-l-10 p-r-10 m-r-10 inline' " +
                //                "style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
                //                "tagid='" + n + "'>" + text + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

                //            $("#industrySecondary_val").val($("#industrySecondary_val").val() + n + ",");
                //        } else {
                //            $("#industryMain_Tag").append("<div class='bootstrap-tagsinput'><span class='tag label " +
                //                "label-info no-padding p-l-10 p-r-10 m-r-10 inline' " +
                //                "style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
                //                "tagid='" + n + "'>" + text + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

                //            $("#industryMain_val").val($("#industryMain_val").val() + n + ",");
                //        }
                //    }
            //});

            $.each(sourceArr, function (i, n) {
                if (n !== "" && n !== "undefined") {
                    text = $(".select_area option").filter("[value='" + n + "']").text();
                    rel = $(".select_area option").filter("[value='" + n + "']").attr("rel");


                    if (rel != "" && rel != 0) {
                        $("#sourceKeyword_Tag").append("<div class='bootstrap-tagsinput'><span class='tag label " +
	                        "label-info no-padding p-l-10 p-r-10 m-r-10 inline' " +
	                        "style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
	                        "tagid='" + n + "'>" + text + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

                        $("#sourceKeyword_val").val($("#sourceKeyword_val").val() + n + ",");
                    } else {
                        $("#userSource_Tag").append("<div class='bootstrap-tagsinput'><span class='tag label " +
	                        "label-info no-padding p-l-10 p-r-10 m-r-10 inline' " +
	                        "style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
	                        "tagid='" + n + "'>" + text + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

                        $("#userSource_val").val($("#userSource_val").val() + n + ",");
                    }
                }
            });

            $.each(choiceReasonArr, function (i, n) {
                if (n !== "" && n !== "undefined") {

                    text = $(".select_area option").filter("[value='" + n + "']").text();

                    $("#choiceReason_Tag").append("<div class='bootstrap-tagsinput'><span class='tag label " +
	                    "label-info no-padding p-l-10 p-r-10 m-r-10 inline' " +
	                    "style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
	                    "tagid='" + n + "'>" + text + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

                    $("#choiceReason_val").val($("#choiceReason_val").val() + n + ",");

                }
            });

            $.each(potentialNeedArr, function (i, n) {
                if (n !== "" && n !== "undefined") {
                    text = $(".select_area option").filter("[value='" + n + "']").text();

                    $("#needType_Tag").append("<div class='bootstrap-tagsinput'><span class='tag label " +
	                    "label-info no-padding p-l-10 p-r-10 m-r-10 inline' " +
	                    "style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
	                    "tagid='" + n + "'>" + text + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

                    $("#needType_val").val($("#needType_val").val() + n + ",");

                }
            });

            //if (json.Attitude != 0) {
            //    text = $(".select_area option").filter("[value='" + json.Attitude + "']").text();

            //    $("#attitude_Tag").append("<div class='bootstrap-tagsinput'><span class='tag label " +
	        //        "label-info no-padding p-l-10 p-r-10 m-r-10 inline' " +
	        //        "style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
	        //        "tagid='" + json.Attitude + "'>" + text + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

            //    $("#attitude_val").val($("#attitude_val").val() + json.Attitude + ",");
            //}

            if (json.BusinessExp != 0) {
                text = $("#businessExp option").filter("[value='" + json.BusinessExp + "']").text();

                $("#businessExp_Tag").append("<div class='bootstrap-tagsinput'><span class='tag label " +
	                "label-info no-padding p-l-10 p-r-10 m-r-10 inline' " +
	                "style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
	                "tagid='" + json.BusinessExp + "'>" + text + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

                $("#businessExp_val").val($("#businessExp_val").val() + json.BusinessExp + ",");
            }

            $("#phone_Tag").val(json.PhoneNum);
            $("#qq_Tag").val(json.QQNum);
            $("#ageGrade").val(json.AgeGrade);
            $("#gender").val(json.Gender);
            $("#webExp").val(json.WebExpGrade);
            $("#shopNum").val(json.SalesmanNum);

            $("#realName").val(json.RealName);
            $("#email").val(json.Email);
            $("#weixin").val(json.Weixin);
            $("#idCardNo").val(json.IdCardNo);
            $("#invoiceTitle").val(json.InvoiceTitle);
            $("#shopAddress").val(json.ShopAddress);
            $("#branchNum").val(json.BranchNum);
            $("#type_7").val(json.Attitude);
            $("#compete").val(json.CompetingProduct);
            $("#remarkContent_1").val(json.RemarkId);

            //if (json.CompetingProduct == "" || json.CompetingProduct == null) {
            //    $("#compete").html("<input id='tagInput' type='text' value='' class='form-control tagsinput custom-tag-input' data-role='tagsinput' />");
            //} else
            //$("#compete").html("<input class='form-control tagsinput custom-tag-input' type='text' value='' />").find("input.custom-tag-input").attr("value", json.CompetingProduct).tagsinput({});
            // $("#tagInput").tagsinput({ itemValue: 'hao' });

            //$.each(competingProductArr, function(i, n) {
            //    $(".bootstrap-tagsinput").append("<span class='tag label label-info'>" + n + "<span data-role='remove'></span></span>");
            //});
            //json.Industry.split(',').each(function() {
            //    text = $("option").filter("value=[" + this.val() + "]").text();
            //    alert(text);
            //    //if (this.rel==0||this.rel=="0") {

            //    //    $("#industryMain").
            //    //} else {

            //    //}

            //});
            //if (json.RemarkId != null) {
            //    GetRemarkInfo(json.RemarkId);
            //}
        }
        //else {
        //    $("#compete").html("<input class='form-control tagsinput custom-tag-input' type='text' value='' />").find("input.custom-tag-input").attr("value", "").tagsinput({});
        //}
    }, true);
}

function RemoveTag(obj) {
    $(obj).parent('span').parent('div').remove();
}

function GetRemarkInfo(str) {
    var url = "/UserPortrait/GetRemarkInfo";
    var postJson = {};
    postJson["remarkIdStr"] = str;

    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);

            $.each(json.DataList, function (i, n) {
                var cnt = (i + 1).toString();
                $("#remarkType_" + cnt).val(n.ItemType);
                $("#remarkContent_" + cnt).val(n.Keyword);
            });
        }
    }, true);

}

//销售图表

function GetSaleChartJson() {
    var accid = $("#ShopInfo").val();
    var url = "/shopinfo/GetSaleChartData";
    var postData = {};
    postData["accid"] = accid;



    var timeType = $(".saleWhere .btn-group .btn.btn-select").val();
    if (timeType == "other") {
        var statTime = $(".saleWhere .page-where-othertime input[name='start']").val();
        var endTime = $(".saleWhere .page-where-othertime input[name='end']").val();
        if (statTime.length > 0) {
            postData["startTime"] = statTime;
        }
        if (endTime.length > 0) {
            postData["endTime"] = endTime;
        }
    } else {
        var timeValue = $.timePeriod(0 - Number(timeType));
        postData["startTime"] = timeValue.start.toLocaleDateString();
        postData["endTime"] = timeValue.end.toLocaleDateString();
    }

    var typeValue = $(".saleWhere .radio input:checked").val();
    postData["dataType"] = typeValue;


    $.doAjax(url, postData, function (msg) {
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
                            value: itemN.Values,
                            symbol: 'emptycircle',
                            symbolSize: 2,
                            itemStyle: {
                                normal: {
                                    color: 'red'
                                }
                            },
                            showAllSymbol: true
                        };
                    } else {
                        dataItemValue["value"] = itemN.Values;
                    }

                    if (!tbData.hasOwnProperty(itemN.series)) {
                        tbData[itemN.series] = new Array();
                    }
                    tbData[itemN.series].push(dataItemValue);
                });

            });
            var legend = new Array();
            $.each(tbData, function (i, n) {
                var seriesItem = {
                    name: i + "数量",
                    type: 'line',
                    symbol: 'Circle',
                    data: n
                }
                series.push(seriesItem);
                legend.push(seriesItem.name);
            })

            option.xAxis[0].data = XLable;
            option.legend.data = legend;
            option.series = series;
            myChart.clear().setOption(option);
        }
    });
}

//短信
function GetShopSmsList(page) {
    if (page == null || page < 1) {
        page = 1;
    }
    var postData = {};
    var accid = $("#ShopInfo").val();
    postData["accid"] = accid;
    postData["pageIndex"] = page;
    var smsStatus = $("#smsSendStatus").val();
    if (smsStatus != "all") {
        postData["status"] = smsStatus;
    }
    if ($.trim($("#smsPhoneNumber").val()) != "") {
        postData["phone"] = $.trim($("#smsPhoneNumber").val());
    }
    if ($.trim($("#startTime").val()) != "") {
        postData["starttime"] = $.trim($("#startTime").val());
    }
    if ($.trim($("#endTime").val()) != "") {
        postData["endtime"] = $.trim($("#endTime").val());
    }



    var url = "/shopinfo/SmsList";
    $.doAjax(url, postData, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#smsListTable tbody").html(template("ShopSmsListScript", json));
            if (page == 1) {
                $("#ListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#ListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#ListPageHtml").attr("maxpage"), "GetShopSmsList", 5);
            $("#ListPageHtml .dataTables_paginate").html(pageHtml);
        }
    }, true);


}
function SmsInputOnKeyUp(evt) {
    evt = (evt) ? evt : ((window.event) ? window.event : "");
    var currKey = 0;
    currKey = evt.keyCode || evt.which || evt.charCode;
    if (currKey == 13) {
        GetShopSmsList(1);
    }
}

function tagOnKey(evt) {
    evt = (evt) ? evt : ((window.event) ? window.event : "");
    var currKey = 0;
    currKey = evt.keyCode || evt.which || evt.charCode;
    // if (currKey == 40) {
    //     var selectTag = $("#shopTagAddP .SysTagListDiv .SelectTag");
    //     if (selectTag.size() > 0) {

    //         $(selectTag).removeClass("SelectTag").nextAll(".selectShow:first").addClass("SelectTag")


    //     } else {

    //         $("#shopTagAddP .SysTagListDiv li.tagItem.selectShow:first").addClass("SelectTag");
    //     }
    // } else if (currKey == 38) {
    // }
    if (currKey == 13) {
        AddShopNewTag();
    } else if (currKey == 27) {
        ShopAddTagInoutHide();
    };
    // var keyName = String.fromCharCode(currKey);
    // console.log("按键码: " + currKey + " 字符: " + keyName);

}

//商品
function GetShopGoodsList(page) {
    if (page == null || page < 1) {
        page = 1;
    }
    var postData = {};
    var accid = $("#ShopInfo").val();
    postData["accid"] = accid;
    postData["pageIndex"] = page;
    if ($.trim($("#goodsGoodsName").val()) != "") {
        postData["goodsname"] = $.trim($("#goodsGoodsName").val());
    }
    if ($.trim($("#goodsStartTime").val()) != "") {
        postData["starttime"] = $.trim($("#goodsStartTime").val());
    }
    if ($.trim($("#goodsEndTime").val()) != "") {
        postData["endtime"] = $.trim($("#goodsEndTime").val());
    }



    var url = "/shopinfo/GoodsList";
    $.doAjax(url, postData, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#goodsListTable tbody").html(template("ShopGoodsListScript", json));
            if (page == 1) {
                $("#goodsListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#goodsListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#goodsListPageHtml").attr("maxpage"), "GetShopGoodsList", 5);
            $("#goodsListPageHtml .dataTables_paginate").html(pageHtml);
        }
    }, true);

}
function GoodsInputOnKeyUp(evt) {
    evt = (evt) ? evt : ((window.event) ? window.event : "");
    var currKey = 0;
    currKey = evt.keyCode || evt.which || evt.charCode;
    if (currKey == 13) {
        GetShopGoodsList(1);
    }
}


//支出
function GetOutPayList(page) {
    if (page == null || page < 1) {
        page = 1;
    }
    var postData = {};
    var accid = $("#ShopInfo").val();
    postData["accid"] = accid;
    postData["pageIndex"] = page;
    if ($.trim($("#outPayName").val()) != "") {
        postData["name"] = $.trim($("#outPayName").val());
    }
    if ($.trim($("#outPayStartTime").val()) != "") {
        postData["starttime"] = $.trim($("#outPayStartTime").val());
    }
    if ($.trim($("#outPayEndTime").val()) != "") {
        postData["endtime"] = $.trim($("#outPayEndTime").val());
    }



    var url = "/shopinfo/GetPayList";
    $.doAjax(url, postData, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#outPayListTable tbody").html(template("ShopoutPayListScript", json));
            if (page == 1) {
                $("#outPayListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#outPayListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#outPayListPageHtml").attr("maxpage"), "GetOutPayList", 5);
            $("#outPayListPageHtml .dataTables_paginate").html(pageHtml);
        }
    }, true);
}
function OutPayInputOnKeyUp(evt) {
    evt = (evt) ? evt : ((window.event) ? window.event : "");
    var currKey = 0;
    currKey = evt.keyCode || evt.which || evt.charCode;
    if (currKey == 13) {
        GetOutPayList(1);
    }
}


function GetGrowUpChartJson() {
    var accid = $("#ShopInfo").val();
    var url = "/shopinfo/GetGrowUpChartData";
    var postData = {};
    postData["accid"] = accid;

    var timeType = $(".growUpWhere .btn-group .btn.btn-select").val();
    if (timeType == "other") {
        var statTime = $(".growUpWhere .page-where-othertime input[name='start']").val();
        var endTime = $(".growUpWhere .page-where-othertime input[name='end']").val();
        if (statTime.length > 0) {
            postData["startTime"] = statTime;
        }
        if (endTime.length > 0) {
            postData["endTime"] = endTime;
        }
    } else if (timeType != "all") {
        var timeValue = $.timePeriod(0 - (Number(timeType) * 30));
        postData["startTime"] = timeValue.start.toLocaleDateString();
        postData["endTime"] = timeValue.end.toLocaleDateString();
    } else {

    }

    var typeValue = $(".growUpWhere .checkbox input:checked");
    var dataType = "";
    $.each(typeValue, function (i, n) {
        if (i > 0) {
            dataType += ",";
        }
        dataType += n.value;
    });

    postData["dataType"] = dataType;


    $.doAjax(url, postData, function (msg) {
        if (msg != "") {

            var json = jQuery.parseJSON(msg);
            var XLable = new Array();
            var tbData = {};

            var series = new Array();

            growUpOption.title.text = json.captionTitle;
            $.each(json.DataList, function (i, n) {

                XLable.push(i);

                $.each(n.ItemList, function (itemI, itemN) {
                    var dataItemValue = {};
                    if (n.weekend == 6 || n.weekend == 0) {
                        dataItemValue = {
                            value: itemN.Values,
                            symbol: 'emptycircle',
                            symbolSize: 2,
                            itemStyle: {
                                normal: {
                                    color: 'red'
                                }
                            },
                            showAllSymbol: true
                        };
                    } else {
                        dataItemValue["value"] = itemN.Values;
                    }

                    if (!tbData.hasOwnProperty(itemN.series)) {
                        tbData[itemN.series] = new Array();
                    }
                    tbData[itemN.series].push(dataItemValue);
                });

            });
            var legend = new Array();
            $.each(tbData, function (i, n) {
                var seriesItem = {
                    name: i + "",
                    type: 'line',
                    symbol: 'Circle',
                    data: n
                }
                series.push(seriesItem);
                legend.push(seriesItem.name);
            })

            growUpOption.xAxis[0].data = XLable;
            growUpOption.legend.data = legend;
            growUpOption.series = series;
            myChart.clear().setOption(growUpOption);
        }
    });
}


//回访
function GetVisitTagList() {

    if ($.trim($("#SelectVisitTagDiv .TagJson").html()) == "") {
        var url = "/shopinfo/VisitTagList";
        $.doAjax(url, null, function (msg) {
            if (msg != "null" && msg != "") {
                $("#SelectVisitTagDiv .TagJson").html(msg);
                var json = $.parseJSON(msg);
                var html = template("VisitTagListScript", { "list": json });
                $("#SelectVisitTagDiv .panel").html(html);
                $("#SelectVisitTagDiv .panel ul li:first a").click();
            }
        });

    }
}

function VisitAddTag(obj) {
    var dom = $(obj);
    if ($("#addVisitList .tag[tagid='" + $.trim(dom.attr("tagid")) + "']").size() == 0) {
        var html = '<span class="tag label label-info" tagid="' + $.trim(dom.attr("tagid")) + '" >';

        html += $.trim(dom.html());

        html += '<span data-role="remove" onclick="VisitRemoveTag(this)" ></span></span>';

        $("#addVisitList .addTag").before(html);
    }
}
function VisitRemoveTag(obj) {
    $(obj).parent().remove();
}
function SelectVisitTagDivToggle(obj) {
    if ($("#SelectVisitTagDiv").css("display") == "none") {
        $("#SelectVisitTagDiv").slideDown();
        $(obj).html("隐藏标签");
    } else {
        $("#SelectVisitTagDiv").slideUp();
        $(obj).html("增加标签");
    }
}

function ShopInfoSavingHuiFang() {
    var postData = {};
    postData["accid"] = Number($("#ShopInfo").val());

    postData["vrOne"] = Number($("#visitReasonOneSelect").val());
    postData["vrTwo"] = Number($("#visitReasonTwoSelect").val());
    postData["vrThree"] = Number($("#visitReasonThreeSelect").val());

    var recordType = $(".sysAccounttable .radio input:radio[name='recordType']:checked");
    postData["recordType"] = recordType.get(0).value;

    var content = Visiteditor.getContent();
    postData["content"] = escape(content);

    postData["stat"] = $(".sysAccounttable  input:radio[name='HandleStat']:checked").val();
    //var visitManner = $(".sysAccounttable .radio input:radio[name='VisitManner']:checked");
    postData["vmanner"] = Number($("#VisitMannerSelect").val());

    postData["contact"] = $("#visitContact").val();

    if ($("#feedBack").prop('checked')) {
        postData["feedBack"] = 1;
    } else {
        postData["feedBack"] = 0;
    }

    var tdid = Number($("#taskDailyId").val());
    if (tdid > 0) {
        postData["taskid"] = tdid;
    }

    //验证咨询回访表单，并提交
    CheckPostInfo(postData);
}

//验证咨询回访表单，并提交
function CheckPostInfo(obj) {
    if (obj['vrOne'] == -1) {
        dialog({
            title: '错误提示',
            content: '请选择一级概要。'
        }).showModal();
    }
    else if (obj['vrTwo'] == -1) {
        dialog({
            title: '错误提示',
            content: '请选择二级概要。'
        }).showModal();

    }
    else if (obj['vrThree'] == -1) {
        dialog({
            title: '错误提示',
            content: '请选择三级概要。'
        }).showModal();

    }
    else if (obj['vmanner'] == -1) {
        dialog({
            title: '错误提示',
            content: '请选择咨询途径。'
        }).showModal();
    }
    else if (RemoveHTMLTag(unescape(obj["content"])).length < 5) {
        dialog({
            title: '错误提示',
            content: '请输入咨询内容。'
        }).showModal();
    }
    else {
        var url = "/shopinfo/addVisit";
        $.doAjax(url, obj, function (msg) {
            if (Number(msg) > 0) {
                dialog({
                    content: '回访记录成功',
                    quickClose: true// 点击空白处快速关闭
                }).showModal();
                Visiteditor.setContent("");
                GetNeedVisitList(1);
            } else {
                alert("回访记录失败");
            }
        }, true);
    }
}
//过滤回放内容中的html标签，空白 (待改进)
function RemoveHTMLTag(str) {
    str = str.replace(/<\/?[^>]*>/g, '');   //去除HTML tag
    str = str.replace(/[ | ]*\n/g, '\n');   //去除行尾空白
    str = str.replace(/(^\s*)|(\s*$)/g, ''); //去除两边空格 类似Trim()
    str = str.replace(/&nbsp;/ig, '');      //去掉&nbsp;
    return str;
}

function GetTaskDailyInfo() {
    var tdid = $("#taskDailyId").val();
    var url = "/shopinfo/TaskDailyInfo/" + tdid;
    $.doAjax(url, null, function (msg) {
        if (msg != "" && msg != "null") {
            var json = $.parseJSON(msg);
            var html = json.t_mk;
            if (json.dt_Source != null && json.dt_Source != "") {
                html += "【来源：" + json.dt_Source + "】";
            }
            if (json.dt_Source == "系统") {
                $("#addVisitList .addTag").before('<span class="tag label label-info" tagid="33" >系统推荐<span data-role="remove" onclick="VisitRemoveTag(this)" ></span></span>');
            } else if (json.dt_Source == "前台反馈") {
                $("#addVisitList .addTag").before('<span class="tag label label-info" tagid="32" >前台反馈<span data-role="remove" onclick="VisitRemoveTag(this)" ></span></span>');
            }
            else if (json.dt_Source == "安卓") {
                $("#addVisitList .addTag").before('<span class="tag label label-info" tagid="31" >安卓反馈<span data-role="remove" onclick="VisitRemoveTag(this)" ></span></span>');
            }
            else if (json.dt_Source == "IOS") {
                $("#addVisitList .addTag").before('<span class="tag label label-info" tagid="30" >IOS反馈<span data-role="remove" onclick="VisitRemoveTag(this)" ></span></span>');
            }


            $("#TaskDailyTitle").html(html);
            $("#TaskDailyTitle").parent().show();

            Visiteditor.setContent('<span style="font-weight: bold;">用户问题：｛</span>' + html + '<span style="font-weight: bold;">｝</span><br/><hr /><br/>');
        } else {
            $("#TaskDailyTitle").parent().hide();
        }
    });
}
function AddVisitInfoButtonClick() {



    $(".VisitInfo .ButtonList").hide();
    $(".VisitInfo .newVisitInfo").show();
    $(".VisitInfo .goOnVisitInfo").hide();

    setTimeout(GetVisitTagList, 1000);
    SysTagQuestionsShow();

    PortraitInfo();
}

function VisitMannerClick(obj) {
    var placeholder = "";
    switch (Number($(obj).val())) {
        case 1:
            placeholder = "请输入用户电话号码";
            break;
        case 2:
            placeholder = "请输入用户QQ号";
            break;
        case 3:
            placeholder = "请输入用户邮箱地址";
            break;
        case 4:
            placeholder = "请输入用户微信号或者名称";
            break;
        case 5:
            placeholder = "请输入用户电话号码";
            break;
        case 7:
            placeholder = "请输入用户论坛ID";
            break;
        case 8:
            placeholder = "请输入消息中心详情";
            break;
        default:
            placeholder = "请选择咨询途径";
            break;
    }
    $("#visitContact").attr("placeholder", placeholder);
}


function GetVisitReason(obj) {
    var vrid = 0;
    if (obj != null && obj != undefined) {
        vrid = $(obj).val();
        if (vrid == 0) {
            return "";
        }
    }

    var url = "/ShopInfo/VisitReason/" + vrid;

    $.doAjax(url, null, function (msg) {

        var html = "";
        if (msg != null && msg != "") {
            var json = $.parseJSON(msg);
            html += '<option value="-1">-------</option>';
            $.each(json, function (i, n) {
                html += "<option value=" + n.id + ">" + n.vr_name + "</option>";
            });
            if (vrid == 0) {
                $("#visitReasonOneSelect").html(html);
                $("#visitReasonOneSelect").change();
            } else if (obj.id == "visitReasonOneSelect") {
                $("#visitReasonTwoSelect").html(html);
                $("#visitReasonTwoSelect").change();
            } else if (obj.id == "visitReasonTwoSelect") {
                $("#visitReasonThreeSelect").html(html);
            }
        }

    }, true);

}

function GetVisitManner() {
    var url = "/ShopInfo/VisitManner";
    $.doAjax(url, null, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            var html = "";
            html += '<option value="-1" select="selected">----------</option>';
            $.each(json, function (i, n) {
                if (i == "电话" || i == "QQ" || i == "论坛" || i == "邮件" || i == "短信") {
                    html += "<option value=" + n + ">" + i + "</option>";;
                }

            });
            $("#VisitMannerSelect").html(html);
        }
    }, false);
}

//历史回访信息
function GetShopVisitList(page) {
    var jsonPost = {};
    jsonPost["pageIndex"] = page;

    var start = $("#visitStartTime").val();
    if (start.length > 0) {
        jsonPost["starttime"] = start;
    }
    var end = $("#visitEndTime").val();
    if (end.length > 0) {
        jsonPost["endtime"] = end;
    }
    jsonPost["accid"] = $("#ShopInfo").val();
    var insertName = $("#VisitInsertName").val();
    if (insertName.length > 0) {
        jsonPost["insertname"] = insertName;
    }

    var url = "/PlatformVisit/GetVisitList";
    $.doAjax(url, jsonPost, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#visitListTable tbody").html(template("ShopVisitListScript", json));
            if (page == 1) {
                $("#visitListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#visitListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#visitListPageHtml").attr("maxpage"), "GetShopVisitList", 5);
            $("#visitListPageHtml .dataTables_paginate").html(pageHtml);

        }

    }, true);
}

//<div class="panel-heading pull-top" style="position: absolute;">
//        <div class="panel-title" id="dataSummary"><span class="text-success fs-12 m-r-14 dataSummary"> </span></div>
//    </div> 
function GetShopOrder() {
    $("#ordSum").html = "";

    var accid = $("#ShopInfo").val();
    var url = "/shopinfo/GetOrder/" + accid;
    $.doAjax(url, null, function (msg) {
        if (msg != null && msg != "") {
            var json = $.parseJSON(msg);
            $("#orderDisplay table tbody").html(template("OrderListScript", { "list": json.DataList }));
            $("#ordSum").html("成本：" + "<span style='color:black'>" + json.SumInfo.Cost + "元</span>" + "；毛利：" + "<span style='color:green'>" + json.SumInfo.Profit + "元</span>" + "；净利：" + "<span style='color:red'>" + json.SumInfo.SumMoney + "元</span>");
            //$("#dataSummary .dataSummary").html("补贴：" + "<span style='color:black'>" + json.SumInfo.Cost + "元</span>" + "；毛利：" + "<span style='color:black'>" + json.SumInfo.Profit + "元</span>"+ "；净利：" + "<span style='color:black'>" + json.SumInfo.SumMoney + "元</span>");
            dialog({
                id: "orderlistdialog",
                title: '订单管理',
                fixed: false,
                content: document.getElementById("orderDiv")

            }).showModal();
        }
    }, true);
}

function GetShopCoupon() {
    var accid = $("#ShopInfo").val();
    var url = "/shopinfo/GetAccountCoupon/" + accid;
    $.doAjax(url, null, function (msg) {
        if (msg != null && msg != "") {
            var json = $.parseJSON(msg);
            var html = template("CouponListScript", { "list": json });
            dialog({
                id: "CouponListScriptdialog",
                title: '优惠券列表',
                content: html
            }).showModal();
        }
    }, true);
}




function bindingCoupon() {

    dialog({
        id: "bingdingCouponDialog",
        title: '绑定店铺',
        content: "<div>请输入要绑定的优惠码：</div><div><input type='text' value='' id='bingdingCouponText' /></div>",
        ok: function () {
            var url = "/OrderCoupon/BingCoupon";
            var postJson = {};
            var accid = $("#ShopInfo").val();
            postJson["accountid"] = accid;
            postJson["CouponID"] = $("#bingdingCouponText").val();
            $.doAjax(url, postJson, function (msg) {
                if (msg == "2") {
                    dialog({
                        content: '绑定成功',
                        quickClose: true// 点击空白处快速关闭
                    }).show();
                } else {
                    alert("绑定优惠券失败" + msg);
                }
            }, true);

        }
    }).showModal();
}



function GetShopTagJson() {
    ShopAddTagInoutHide();
    var accid = $("#ShopInfo").val();
    var perm = $("#perm").val();
    var url = "/shopinfo/ShopTagList/" + accid;
    $.doAjax(url, null, function (msg) {
        if (msg != "" && msg != "null") {
            $("#ShopTagList").append("");
            var json = $.parseJSON(msg);
            $("#ShopTagList").html("");
            $.each(json, function (i, n) {
                var html = '<span class="tag label label-info no-padding p-l-10 p-r-10 m-r-10 m-b-10 inline" tagid="' + n.id + '" tagtypeid="' + n.tagTypeid + '" ';
                html += "style='background-color:" + n.t_BgColor + ";color:" + n.t_Color + ";'";
                html += '>';
                html += n.t_Name;
                if ((n.timediff < 10 && n.timediff >= 0) || perm == 1) {
                    html += '<span data-role="remove" onclick="RemoveShopTag(this)"></span></span>';
                } else {
                    html += '</span>';
                }


                $("#ShopTagList").append(html);
            });
        }
        SysTagQuestions();
    });
}
//标签问题
function SysTagQuestions() {
    var accid = $("#ShopInfo").val();

    var tagTypeList = new Array();

    var domList = $("#ShopTagList .tag");
    $.each(domList, function (i, n) {
        tagTypeList.push($(n).attr("tagtypeid"));
    });
    var postJson = {};
    if (tagTypeList.length > 0) {
        postJson["tagType"] = tagTypeList.join(',');
    }
    postJson["accid"] = accid;
    var url = "/ShopInfo/SysTagQuestions";
    $.doAjax(url, postJson, function (msg) {
        if (msg != null && msg != "") {
            var json = $.parseJSON(msg);
            //SysTagQuestionsListScript
            $("#SysTagQuestionsDiv .selectSysTagQuestionsList").html(template("SysTagQuestionsListScript", { "list": json }));
            SysTagQuestionsShow();
        }


    }, false);
}

function SysTagQuestionsShow() {


    if ($("#newVisitInfo").css("display") == "none") {
        $("#goOnVisitInfo tr td.TagQuestionsTd").html($("#SysTagQuestionsDiv"));
    } else {
        $("#newVisitInfo tr td.TagQuestionsTd").html($("#SysTagQuestionsDiv"));
    }

    if ($("#SysTagQuestionsDiv ul li").size() > 0) {
        $("#SysTagQuestionsDiv .selectSysTagQuestionsList ul li:first a").click();
        $("#SysTagQuestionsDiv").closest("tr").show();
    } else {
        $("#SysTagQuestionsDiv").closest("tr").hide();
    }
}

function ShopAddTagInoutShow() {
    $("#shopTagAddP .tagAdd").hide();
    $("#shopTagAddP .tagAddInput").css("display", "inline-block");
    $("#SysShopTagNewInput").focus();
    GetSysShopTagList();
}
function ShopAddTagInoutHide() {
    $("#shopTagAddP .tagAdd").show();
    $("#shopTagAddP .tagAddInput").hide();
}
function RemoveShopTag(obj) {
    var accid = $("#ShopInfo").val();
    var url = "/shopinfo/RemoveShopTag";
    var postJson = {};
    postJson["accid"] = accid;
    postJson["tagid"] = $(obj).parent().attr("tagid");
    $.doAjax(url, postJson, function (msg) {
        if (msg == "0") {
            GetShopTagJson();
        } else {
            alert("移除标签失败");
        }
    });
}

function GetSysShopTagList() {
    var url = "/shopinfo/SysTagList/";
    $.doAjax(url, null, function (msg) {
        if (msg != null && msg != "") {
            var json = $.parseJSON(msg);
            $("#shopTagAddP .SysTagListDiv").html("");
            $.each(json, function (i, n) {

                var html = "<li class='tagItem m-l-5 m-r-5 m-b-5 m-b-10 pull-left'><a href='javascript:void(0)' class='btn btn-default' onclick='ShopAddTagClick(this)' tagid='" + n.id + "'>";
                html += n.t_Name;
                html += "</a></li>";
                $("#shopTagAddP .SysTagListDiv").append(html);

            });


        }
    });
}

function InputOnKeyUp(evt) {
    evt = (evt) ? evt : ((window.event) ? window.event : "");
    var currKey = 0;
    currKey = evt.keyCode || evt.which || evt.charCode;
    if (currKey == 13) {
        AddShopNewTag();
    } else if (currKey == 27) {
        ShopAddTagInoutHide();
    };
}

function ShopAddTagClick(obj) {
    var accid = $("#ShopInfo").val();
    var url = "/shopinfo/AddShopSysTag";
    var postJson = {};
    postJson["accid"] = accid;
    postJson["tagid"] = $(obj).attr("tagid");
    $.doAjax(url, postJson, function (msg) {
        if (msg == "0") {
            GetShopTagJson();
        } else {
            alert("增加标签失败");
        }
    });
}

function AddShopNewTag() {
    var url = "/shopinfo/AddShopSysNewTag";
    var accid = $("#ShopInfo").val();
    var postJson = {};
    postJson["accid"] = accid;

    var tagName = $("#SysShopTagNewInput").val();
    if (tagName.length < 1) {
        ShopAddTagInoutHide();
        return false;
    }

    postJson["tagName"] = tagName;

    $.doAjax(url, postJson, function (msg) {
        if (msg == "0") {
            GetShopTagJson();
        } else if (msg == "1") {
            alert("该标签已添加");
        }
        else {
            alert("新增标签失败 + " + msg);
        }
    });
    $("#SysShopTagNewInput").val("");
}


function GetLastLogInfo() {
    var accid = $("#ShopInfo").val();
    var url = "/shopinfo/LastLogInfo/" + accid;
    $.doAjax(url, null, function (msg) {
        if (msg != "" && msg != "null") {
            var json = $.parseJSON(msg);

            var html = "( " + json.OperDate;
            if (json.LogSource == "移动端") {
                html += '&nbsp;&nbsp;<i class="fs-14 fa fa-tablet" title="移动端"></i>&nbsp;&nbsp;';
            } else if (json.LogSource == "客户端") {
                html += '&nbsp;&nbsp;<i class="fs-14 pg-ui" title="客户端"></i>&nbsp;&nbsp;';
            } else {
                html += '&nbsp;&nbsp;<i class="fs-14 pg-desktop" title="网站"></i>&nbsp;&nbsp;';
            }
            html += ")"
            $("#lastLogInfo").html(html);
            if (json.IpAddressInfo != "" && json.IpAddressInfo != "Not Find Location") {
                $("#AccountAddress").append("<p>IP登录地址：<span class='bold'>" + json.IpAddressInfo + "</span></p>");
            }
        }
    });

}

function SetHtmlThemesVal() {
    var val = $("#HtmlThemesVal").val();
    var accid = $("#ShopInfo").val();
    if (confirm("您确认要修改此店铺的【显示模板】吗？")) {
        var url = "/shopinfo/UpdateHtmlTemes";
        var postJson = {};
        postJson["accid"] = accid;
        postJson["val"] = val;

        $.doAjax(url, postJson, function (msg) {
            if (msg == "1") {
                AppListShow();
            } else {
                alert("修改【模板】失败");
            }
        });
    }
}

function GetNeedVisitList(page) {
    //var accid = $("#ShopInfo").val();
    //var url = "/shopinfo/NeedVisitList/" + accid;

    //$.doAjax(url, null, function (msg) {
    //    if (msg != "" && msg != "null") {
    //        var json = $.parseJSON(msg);
    //        $("#needVisitTable tbody").html(template("needVisitTableScript", { "list": json }));

    //        var logVid = $("#LoadVisitInfoId").val();
    //        if (logVid != "0") {
    //            $("#needVisitTable tbody tr td a[visitinfoid='" + logVid + "']").click();
    //            $("#LoadVisitInfoId").val("0");
    //        }
    //    }
    //}, false);

    if (page == null || page == undefined) {
        page = 1;
    }

    var jsonPost = {};
    jsonPost["pageIndex"] = page;

    jsonPost["accid"] = $("#ShopInfo").val();

    var url = "/PlatformVisit/GetVisitList";
    $.doAjax(url, jsonPost, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#needVisitTable tbody").html(template("needVisitTableScript", json));
            if (page == 1) {
                $("#oldVisitInfo .visitListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#oldVisitInfo .visitListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#oldVisitInfo .visitListPageHtml").attr("maxpage"), "GetNeedVisitList", 5);
            $("#oldVisitInfo .visitListPageHtml .dataTables_paginate").html(pageHtml);
            $("#oldVisitInfo").show();
        } else {
            $("#oldVisitInfo").hide();
        }

    }, true);
}

function GetUnfinishedVisitList() {
    var jsonPost = {};


    jsonPost["accId"] = $("#ShopInfo").val();

    var url = "/ShopInfo/GetUnfinishedTask";
    $.doAjax(url, jsonPost, function (msg) {
        if (msg != "null" && msg != "" && msg != "[]") {
            var json = $.parseJSON(msg);

            $("#unfinished").html(template("UnfinishedTask", { "list": json }));

            $("#taskDiv").removeClass('hidden');
        } else {
            $("#taskDiv").addClass('hidden');
        }

    }, true);
}

function GetForumFeedback() {
    var jsonPost = {};


    jsonPost["accId"] = $("#ShopInfo").val();

    var url = "/ShopInfo/GetForumFeedBack";
    $.doAjax(url, jsonPost, function (msg) {
        if (msg != "null" && msg != "" && msg != "[]") {
            var json = $.parseJSON(msg);

            $("#forumFeedback").html(template("ForumFeedBack", { "list": json }));

            $("#forumDiv").removeClass('hidden');
        } else {
            $("#forumDiv").addClass('hidden');
        }

    }, true);
}

function VisitReplyShow(vid) {

    var url = "/PlatformVisit/GetVisitInfo/" + vid;
    $("#needVisitId").val(vid);
    $.doAjax(url, null, function (msg) {
        if (msg != "" && msg != "null") {
            var json = $.parseJSON(msg);
            $(".VisitInfo .goOnVisitInfo").hide().find("table.sysAccounttable tr.template").remove();
            $(".VisitInfo .goOnVisitInfo").show();
            $(".VisitInfo .newVisitInfo").hide();
            SysTagQuestionsShow();

            var html = template("GoOnVisitInfoTableScript", json);

            $(".VisitInfo .goOnVisitInfo table.sysAccounttable>tbody").prepend(html);


        }

    }, false);


}

function SaveVisitReply() {
    var postJson = {};

    postJson["vi_id"] = $("#needVisitId").val();
    postJson["accid"] = $("#ShopInfo").val();


    var content = needVisiteditor.getContent();
    postJson["vr_Content"] = escape(content);

    postJson["vr_Stat"] = $(".VisitInfo .goOnVisitInfo input:radio[name='HandleStat']:checked").val();

    var url = "/ShopInfo/AddVisitReply";
    $.doAjax(url, postJson, function (msg) {

        if (msg == 1) {
            dialog({
                content: '回访记录成功',
                quickClose: true// 点击空白处快速关闭
            }).showModal();
            GetNeedVisitList();
            $(".VisitInfo .goOnVisitInfo").hide().find("table tr.template").remove();
        } else {
            alert("新增回访错误");
        }
    }, true);
}

function AddTagItem(keyword) {
    var itemVal = $("#" + keyword + " select").val();

    if (itemVal == 0) {
        alert("请选择内容！");
        return false;
    }

    var itemName = "";

    if (keyword=='isSolved') {
        itemName = $("#isSolved select option:selected").text();
        keyword = 'mainQuestion';
    } else {
        itemName = $("#" + keyword + " select option:selected").text();
    }
    

    if ($("#" + keyword + "_Tag").html().indexOf(itemName) <= 0) {
        if (keyword == "attitude" || keyword == "businessExp") {
            $("#" + keyword + "_Tag").html("<div class='bootstrap-tagsinput'><span class='tag label " +
			"label-info no-padding p-l-10 p-r-10 m-r-10 m-b-10 inline' " +
			"style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
			"tagid='" + itemVal + "'>" + itemName + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

            $("#" + keyword + "_val").val(itemVal);
        } else {
            $("#" + keyword + "_Tag").append("<div class='bootstrap-tagsinput'><span class='tag label " +
			"label-info no-padding p-l-10 p-r-10 m-r-10 m-b-10 inline' " +
			"style='background-color:#de6739;color:#fff;' tagtypeid='0' " +
			"tagid='" + itemVal + "'>" + itemName + "<span onclick='RemoveTag(this)' data-role='remove'></span></span></div>");

            $("#" + keyword + "_val").val($("#" + keyword + "_val").val() + itemVal + ",");
        }

    } else {
        alert("已经存在该信息！");
    }

}

function SecondaryList(num) {
    var typeNum = num + 1;
    var pId = $("#type_" + num).val();

    $("#type_" + typeNum + " option").remove();

    $("#type_" + typeNum + " option").filter("[value='0']").show();
    //$("#type_" + typeNum + " option:eq(0)").attr('selected','selected');

    $("#type_" + typeNum + " option").filter("[rel='" + pId + "']").show();

    if (pId != 0) {
        $.each(globalData.DataList, function (i, n) {
            if (n.ParentId == pId) {
                $("#type_" + typeNum).append("<option value='" + n.Id + "' rel='" + n.ParentId + "'>" + n.Keyword + "</option>");
            }

            //$("#prefixAgent").append("<option value='" + n.id + "' rel='" + n.remark + "'>" + n.preName + "[" + n.agentId + "]</option>");

        });
    }

}

function SaveExtInfo() {
    //var industryMainVal = ",";
    //for (var i = 0; i < $("#industryMain_Tag .bootstrap-tagsinput").size() ; i++) {
    //    industryMainVal += $("#industryMain_Tag .bootstrap-tagsinput span.tag").eq(i).attr("tagid") + ",";
    //}

    var industryMain = $("#type_1").val()+",";

    //var industrySecondaryVal = ",";
    //for (var i = 0; i < $("#industrySecondary_Tag .bootstrap-tagsinput").size() ; i++) {
    //    industrySecondaryVal += $("#industrySecondary_Tag .bootstrap-tagsinput span.tag").eq(i).attr("tagid") + ",";
    //}
    var industrySecondary = $("#type_2").val();

    var industry = industryMain + industrySecondary;

    var sourceKeywordVal = ",";
    for (var i = 0; i < $("#sourceKeyword_Tag .bootstrap-tagsinput").size() ; i++) {
        sourceKeywordVal += $("#sourceKeyword_Tag .bootstrap-tagsinput span.tag").eq(i).attr("tagid") + ",";
    }
    var sourceKeyword = sourceKeywordVal;

    var userSourceVal = ",";
    for (var i = 0; i < $("#userSource_Tag .bootstrap-tagsinput").size() ; i++) {
        userSourceVal += $("#userSource_Tag .bootstrap-tagsinput span.tag").eq(i).attr("tagid") + ",";
    }

    var userSourcePortrait = userSourceVal + sourceKeyword;


    var choiceReasonVal = ",";
    for (var i = 0; i < $("#choiceReason_Tag .bootstrap-tagsinput").size() ; i++) {
        choiceReasonVal += $("#choiceReason_Tag .bootstrap-tagsinput span.tag").eq(i).attr("tagid") + ",";
    }
    var choiceReason = choiceReasonVal;

    var mainQuestionVal = ",";
    for (var i = 0; i < $("#mainQuestion_Tag .bootstrap-tagsinput").size() ; i++) {
        mainQuestionVal += $("#mainQuestion_Tag .bootstrap-tagsinput span.tag").eq(i).attr("tagid") + ",";
    }
    var mainQuestion = mainQuestionVal;

    var needTypeVal = ",";
    for (var i = 0; i < $("#needType_Tag .bootstrap-tagsinput").size() ; i++) {
        needTypeVal += $("#needType_Tag .bootstrap-tagsinput span.tag").eq(i).attr("tagid") + ",";
    }
    var needType = needTypeVal;

    //var attitude = $("#attitude_Tag .bootstrap-tagsinput span.tag").attr("tagid");
    var attitude = $("#type_7").val();
    var businessExp = $("#businessExp_Tag .bootstrap-tagsinput span.tag").attr("tagid");

    var phoneNum = $("#phone_Tag").val();
    var qqNum = $("#qq_Tag").val();
    //var competingProduct = $("#compete").find("input.custom-tag-input").val(); 
    var competingProduct = $("#compete").val();

    var ageGrade = $("#ageGrade").val();
    var gender = $("#gender").val();
    var webExp = $("#webExp").val();
    var shopNum = $("#shopNum").val();

    var remarkType1 = $("#remarkType_1").val();
    var remarkType2 = $("#remarkType_2").val();

    var remarkContent1 = $("#remarkContent_1").val();
    var remarkContent2 = $("#remarkContent_2").val();

    var postJson = {};

    postJson["Industry"] = industry;
    postJson["UserSourcePortrait"] = userSourcePortrait;
    postJson["ChoiceReason"] = choiceReason;
    postJson["MainQuestion"] = mainQuestion;
    postJson["CompetingProduct"] = competingProduct;
    postJson["PotentialNeed"] = needType;
    postJson["Attitude"] = attitude;
    postJson["BusinessExp"] = businessExp;
    postJson["PhoneNum"] = phoneNum;
    postJson["QQNum"] = qqNum;
    postJson["AgeGrade"] = ageGrade;
    postJson["Gender"] = gender;
    postJson["WebExpGrade"] = webExp;
    postJson["SalesmanNum"] = shopNum;

    postJson["RealName"] = $("#realName").val();
    postJson["Email"] = $("#email").val();
    postJson["Weixin"] = $("#weixin").val();
    postJson["IdCardNo"] = $("#idCardNo").val();
    postJson["InvoiceTitle"] = $("#invoiceTitle").val();
    postJson["ShopAddress"] = $("#shopAddress").val();
    postJson["BranchNum"] = $("#branchNum").val();
    postJson["IsSolveProblem"] = $("#type_10").val();

    postJson["RemarkType1"] = remarkType1;
    postJson["RemarkType2"] = remarkType2;
    postJson["RemarkContent1"] = remarkContent1;
    postJson["RemarkContent2"] = remarkContent2;

    postJson["AccId"] = $("#ShopInfo").val();

    var url = "/UserPortrait/AddUserPortrait";
    $.doAjax(url, postJson, function (msg) {

        if (msg == "") {
            alert("保存成功！");
        } else {
            alert("新增出错！");
        }
    }, true);
}

function SendSms() {
    
    txt_length();
    
    dialog({
        id: "sendSms",
        title: "发送短信",
        lock: true,
        opacity: 0.57, // 透明度
        content: document.getElementById("addSmsText"),
        ok: function () {
            if ($("#smscon").val().length<=0) {
                alert("短信内容不可以为空！");
                return false;
            }
            
            var ur = "/MessageSending/SendingVisitSms";

            var postJson = {};
            postJson["smsContent"] = $("#smscon").val();
            postJson["accid"] = $("#ShopInfo").val();

            $.doAjax(ur, postJson, function (msg) {
                //alert(msg);
                if (msg == "1") {
                    alert("发送成功！");                    
                } else {
                    alert("发送失败！");
                }
            }, true);
        },
        cancel: true
    }).show();
}

function txt_length() {
    var txtLength = parseInt($("#smscon").val().length);

    $("#wordcnt").html(txtLength).css("color", "red");

}

