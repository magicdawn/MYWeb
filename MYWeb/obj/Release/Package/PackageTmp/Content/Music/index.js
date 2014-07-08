/// <reference path="../../Scripts/jquery-1.8.2.intellisense.js" />
/// <reference path="../Qiniu/plupload/js/plupload.full.min.js" />
/// <reference path="../Qiniu/qiniu.js" />

function log(msg) {
    $("#result").html($("#result").html() + new Date().toLocaleString() + msg + "<br />");
};

$(function () {
    var domain = "http://ranger.qiniudn.com/";
    var uploader = new plupload.Uploader({
        browse_button: 'select_file',
        url: "http://up.qiniu.com/",
        filters: {
            mime_types: [
                { title: "mp3文件(mp3,wma,wav,ogg)", extensions: "mp3,wma,wav,ogg" }
            ]
        },
        init: {
            "FilesAdded": function (up, files) {
                var file = files[0];
                log(" [添加] " + file.name);
            },
            "BeforeUpload": function (up, file) {
                up.settings["multipart_params"] = up.settings["multipart_params"] || {};
                up.settings["multipart_params"]["key"] = "mp3/" + file.name;
            },
            "FileUploaded": function (up, file, info) {
                var data = JSON.parse(info.response);
                log(" [上传] " + file.name + "已成功上传");
                var link = domain + data.key;
                log(" [地址] " + link + "<br/>    Encoded : " + window.encodeURI(link));
            }
        }
    });
    uploader.init();

    //post数据
    $.get("/Music/Uptoken", function (token) {
        //1.set option
        //uploader.setOption("multipart_params", {
        //    "token": token
        //});
        //2.给setting复制
        uploader.settings["multipart_params"] = uploader.settings["multipart_params"] || {};
        uploader.settings["multipart_params"]["token"] = token;
    });

    $("#start_upload").click(function () {
        uploader.start();
    });
});

/** qiniu sdk
上传时不post token导致error
var uploader = Qiniu.uploader({
        browse_button: 'select_file',
        uptoken_url: '/Music/Uptoken',
        domain: 'http://ranger.qiniudn.com/',
        auto_start: true,                 //选择文件后自动上传，若关闭需要自己绑定事件触发上传
        init: {
            //注意是files
            "FilesAdded": function (up, files) {
                var file = files[0];
                alert(file.id + file.name);
            },
            'FileUploaded': function (up, file, info) {
                var domain = up.getOption('domain');
                var res = JSON.parse(info);
                var sourceLink = domain + res.key;
            },
            "UploadProgress": function (up,file) {
                console.log("id : %s ,name : %s , %s completed",file.id,file.name,file.percent);
            }
        }
    });
*/