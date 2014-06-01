/// <reference path="/Scripts/jquery-1.7.1.min.js" />

//小黄鸡

var $text;//文本框
var $result;
$(function () {
    $text = $("#text");
    $result = $("#result");
    //回车,发送
    $text.keydown(function (event) {
        if (event.keyCode == 13)
        {
            chat();
        }
        //alert(event.keyCode);
    })
    //发送文本框 焦点
    .focus();

    $("#btnSend").click(function () {
        chat();
    });
});

//开始聊天,主要判断是否为空
function chat() {
    if ($text.val())
    {//有值
        if ($text.val() == "cls")//清屏命令
        {
            $result.html("");
            $text.val("");
            return;
        }
        if (!$("#btnSend").prop("disabled"))//disable=false时发送
        {
            iSay($text.val());
        }
    }
    else
    {
        //给出提示
        //alert("不打字,肿么聊天呢~~~");
        //IE下经常乱弹框
    }
}

//自己说
function iSay(text) {
    //添加上 自己说的话
    var html = "<div class='me'><span class='me_img'></span><span class='me_msg'>" + text + "</span></div>";
    $(html).appendTo($result);

    //清空TextBox
    $text.val("");
    //禁用按钮
    $("#btnSend").val("小黄鸡思考中").prop("disabled", true).css("color","red");
    //将$result scroll到底部
    $resultScorll();

    //服务器代理,加上超时
    $.ajax({
        url:"/Simsimi/Agent",
        data:"chat=" + encodeURI(text),
        timeout:5*1000,//5秒超时
        success:function (data) {
            //获取成功 小黄鸡说
            simsimiSay(data);
        },
        error: function () {
            simsimiSay("抱歉,出错了~小黄鸡不能再陪你玩了~");
        }
    });
}

//小黄鸡说
function simsimiSay(text) {
    //加上服务器响应
    var html = "<div class='bot'><span class='bot_img'></span><span class='bot_msg'>" + text + "</span></div>";
    $(html).appendTo($result);
    $resultScorll();
    //切换按钮文字,激活按钮
    $("#btnSend").val("发送").prop("disabled", false).css("color","black");
}

function $resultScorll() {
    //scroll到底
    setTimeout(function () {
        var total = $result[0].scrollHeight;
        var fixedHeight = $result.height();
        $result.scrollTop(total - fixedHeight);
    }, 10);
}