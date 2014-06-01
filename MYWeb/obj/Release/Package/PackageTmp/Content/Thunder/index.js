/// <reference path="../../Scripts/jquery-1.7.1.min.js" />

//用于 /Thunder/Index/的js

var $info = $("#info");//展示消息的div
var timeHide; //用于隐藏的settimeput
var className;//用于控制$info的颜色

//展示消息
function showInfo(text, type) {
    if (!type)
    {
        type = "info";//默认为info
    }
    //显示
    className = "alert-" + type;//alert类型
    $info.addClass(className);
    $info.html(text).slideDown("slow");
    //定时隐藏
    setInfoTimeout();
}

//定时隐藏$info
function setInfoTimeout() {
    timeHide = setTimeout(function () {
        $info.slideUp("slow", function () {
            //完成时 移除class
            $info.removeClass(className);
        });
    }, 2000);
}

//复制文本
function doCopy(text) {
    if (typeof (clipboardData) != "undefined")
    {
        clipboardData.setData("text", text);
        showInfo("[" + text + "] 已复制到剪贴板");
    }
    else
    {
        showInfo(
            "复制到剪贴板功能只支持<span style='font-size:18px;color:black;'>IE</span>浏览器 或 国产浏览器兼容核心<br /><a href='/Thunder/Help' target='_blank'>怎么设置兼容模式啊</a>", "danger");
    }
}

$(function () {
    //值初始化
    $info = $("#info");

    //复制到剪贴板链接的显示,隐藏
    //先隐藏掉
    $(".clip").hide();
    $("td").hover(function () {
        //移上去
        $(".clip", $(this)).show();

    }, function () {
        //移出
        $(".clip", $(this)).hide();
    });

    //复制功能
    $(".clip").click(function () {
        doCopy($(this).prev("span").text());
    });

    //双击复制
    $("#tbldata td").dblclick(function () {
        doCopy($("span:first", $(this)).text());
    });

    //将info固定在顶部
    $info.css("left", (window.innerWidth - $info.width()) / 2);

    //如果鼠标放在$info上,则暂时取消定时隐藏
    $info.hover(function () {
        //如果已经设置Timeout则清除
        if (timeHide)
        {
            clearTimeout(timeHide);
        }
    },
    function () {
        //移出去,加上timeout
        setInfoTimeout();
    });

    //点击行变色,除去header
    $("tr", $("#tbldata")).not("table-header").click(function () {
        //其他行 正常色
        $(this).siblings().children().removeClass("clickcyan");
        //当前行变cyan
        $(this).children().addClass("clickcyan");
    });
});