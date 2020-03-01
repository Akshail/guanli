//function alertmsg() {
//    //jQuery(document).ready(function (){

//    setTimeout("alert('waited 10s')", 10000)
//};

////$("#CPHMainContent_btn_alert").click(
////    setTimeout(
////   (function () {
////       alert("msg")
////    }),10000)
////)
//$("#CPHMainContent_btn_alert").click(function () {
//    setTimeout("alert('msg after 10s')", 10000)
//});
//jQuery(document).ready(){
window.onload = function () {
    //$(function () {
    //jQuery(document).ready(function () {

    //setTimeout(function () {

    $('#FileUpload_Paper').uploadify({

        'buttonImage': '../../js/uploadify/browse-btn.png',
        'buttonText': '浏览',
        'auto': true,
        'successTimeout': 99999,
        'swf': '../../js/uploadify/uploadify.swf',
        'queueID': 'uploadfileQueue',
        'uploader': '../../js/UploadHandler.ashx',
        //'uploader': '../../js/Test.aspx',

        'fileSizeLimit': '102400',
        //'queueSizeLimit': 0,
        'queueSizeLimit': 1,

        'progressData': 'speed',
        'overrideEvents': ['onDialogClose'],
        'fileTypeExts': '*.*;',
        'fileTypeDesc': '请选择 flv',
        'onSelectError': function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("上传的文件数量已经超出系统限制的" + jQuery('#FileUpload_Paper').uploadify('settings', 'queueSizeLimit') + "个文件！");
                    break;
                case -110:
                    alert("文件 [" + file.name + "] 大小超出系统限制的" + jQuery('#FileUpload_Paper').uploadify('settings', 'fileSizeLimit') / 1024 + "M大小！");
                    break;
                case -120:
                    alert("文件 [" + file.name + "] 大小异常！");
                    break;
                case -130:
                    alert("文件 [" + file.name + "] 类型不正确！");
                    break;
            }
        },
        'onCancel': function (queueItemCount) {
            alert("取消上传");
            return false;
        },
        'onUploadSuccess': function (file, data, response) {
            //if ($("#CPHMainContent_LabelAccessoryName").text() == "") {
            //    $("#CPHMainContent_LabelAccessoryName").append("已上传：" + file.name);
            //    $("#CPHMainContent_HiddenFieldAccessoryName").val(file.name);
            //    $("#CPHMainContent_HiddenFieldUrl").val(data);


            $("#CPHMainContent_tb_AttName").val(file.name);
            $("#CPHMainContent_tb_AttPath").val(data);

            $("#CPHMainContent_btn_Upload").click();

            //}
            //else {
            //    $("#CPHMainContent_LabelAccessoryName").append("," + file.name);
            //    $("#CPHMainContent_HiddenFieldAccessoryName").val($("#CPHMainContent_HiddenFieldAccessoryName").val() + "," + file.name);
            //    $("#CPHMainContent_HiddenFieldUrl").val($("#CPHMainContent_HiddenFieldUrl").val() + "," + data);
            //}

        }
    });
    //}, 10);

    //}


    jQuery("#FileUpload_Summary").uploadify({
        'buttonImage': '../../js/uploadify/browse-btn.png',
        'buttonText': '浏览',
        'auto': true,
        'successTimeout': 99999,
        'swf': '../../js/uploadify/uploadify.swf',
        'queueID': 'uploadfileQueue',
        'uploader': '../../js/UploadHandler.ashx',
        //'uploader': '../../js/Test.aspx',

        'fileSizeLimit': '102400',
        //'queueSizeLimit': 0,
        'queueSizeLimit': 1,

        'progressData': 'speed',
        'overrideEvents': ['onDialogClose'],
        'fileTypeExts': '*.*;',
        'fileTypeDesc': '请选择 flv',
        'onSelectError': function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("上传的文件数量已经超出系统限制的" + jQuery('#FileUpload_Summary').uploadify('settings', 'queueSizeLimit') + "个文件！");
                    break;
                case -110:
                    alert("文件 [" + file.name + "] 大小超出系统限制的" + jQuery('#FileUpload_Summary').uploadify('settings', 'fileSizeLimit') / 1024 + "M大小！");
                    break;
                case -120:
                    alert("文件 [" + file.name + "] 大小异常！");
                    break;
                case -130:
                    alert("文件 [" + file.name + "] 类型不正确！");
                    break;
            }
        },
        'onCancel': function (queueItemCount) {
            alert("取消上传");
            return false;
        },
        'onUploadSuccess': function (file, data, response) {
            //if ($("#CPHMainContent_LabelAccessoryName").text() == "") {
            //    $("#CPHMainContent_LabelAccessoryName").append("已上传：" + file.name);
            //    $("#CPHMainContent_HiddenFieldAccessoryName").val(file.name);
            //    $("#CPHMainContent_HiddenFieldUrl").val(data);


            $("#CPHMainContent_tb_AttName_summary").val(file.name);
            $("#CPHMainContent_tb_AttPath_summary").val(data);

            $("#CPHMainContent_btn_Upload_Summary").click();

            //}
            //else {
            //    $("#CPHMainContent_LabelAccessoryName").append("," + file.name);
            //    $("#CPHMainContent_HiddenFieldAccessoryName").val($("#CPHMainContent_HiddenFieldAccessoryName").val() + "," + file.name);
            //    $("#CPHMainContent_HiddenFieldUrl").val($("#CPHMainContent_HiddenFieldUrl").val() + "," + data);
            //}

        }
    });

    jQuery("#FileUpload_Reason").uploadify({
        'buttonImage': '../../js/uploadify/browse-btn.png',
        'buttonText': '浏览',
        'auto': true,
        'successTimeout': 99999,
        'swf': '../../js/uploadify/uploadify.swf',
        'queueID': 'uploadfileQueue_Reason',
        'uploader': '../../js/UploadHandler.ashx',
        //'uploader': '../../js/Test.aspx',

        'fileSizeLimit': '102400',
        //'queueSizeLimit': 0,
        'queueSizeLimit': 1,

        'progressData': 'speed',
        'overrideEvents': ['onDialogClose'],
        'fileTypeExts': '*.*;',
        'fileTypeDesc': '请选择 flv',
        'onSelectError': function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("上传的文件数量已经超出系统限制的" + jQuery('#FileUpload_Reason').uploadify('settings', 'queueSizeLimit') + "个文件！");
                    break;
                case -110:
                    alert("文件 [" + file.name + "] 大小超出系统限制的" + jQuery('#FileUpload_Reason').uploadify('settings', 'fileSizeLimit') / 1024 + "M大小！");
                    break;
                case -120:
                    alert("文件 [" + file.name + "] 大小异常！");
                    break;
                case -130:
                    alert("文件 [" + file.name + "] 类型不正确！");
                    break;
            }
        },
        'onCancel': function (queueItemCount) {
            alert("取消上传");
            return false;
        },
        'onUploadSuccess': function (file, data, response) {
            //if ($("#CPHMainContent_LabelAccessoryName").text() == "") {
            //    $("#CPHMainContent_LabelAccessoryName").append("已上传：" + file.name);
            //    $("#CPHMainContent_HiddenFieldAccessoryName").val(file.name);
            //    $("#CPHMainContent_HiddenFieldUrl").val(data);


            $("#CPHMainContent_tb_AttName_Reason").val(file.name);
            $("#CPHMainContent_tb_AttPath_Reason").val(data);

            $("#CPHMainContent_btn_Upload_Reason").click();

            //}
            //else {
            //    $("#CPHMainContent_LabelAccessoryName").append("," + file.name);
            //    $("#CPHMainContent_HiddenFieldAccessoryName").val($("#CPHMainContent_HiddenFieldAccessoryName").val() + "," + file.name);
            //    $("#CPHMainContent_HiddenFieldUrl").val($("#CPHMainContent_HiddenFieldUrl").val() + "," + data);
            //}

        }
    });

    jQuery("#FileUpload_ItemPlan").uploadify({
        'buttonImage': '../../js/uploadify/browse-btn.png',
        'buttonText': '浏览',
        'auto': true,
        'successTimeout': 99999,
        'swf': '../../js/uploadify/uploadify.swf',
        'queueID': 'uploadfileQueue_ItemPlan',
        'uploader': '../../js/UploadHandler.ashx',
        //'uploader': '../../js/Test.aspx',

        'fileSizeLimit': '102400',
        //'queueSizeLimit': 0,
        'queueSizeLimit': 1,

        'progressData': 'speed',
        'overrideEvents': ['onDialogClose'],
        'fileTypeExts': '*.*;',
        'fileTypeDesc': '请选择 flv',
        'onSelectError': function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("上传的文件数量已经超出系统限制的" + jQuery('#FileUpload_ItemPlan').uploadify('settings', 'queueSizeLimit') + "个文件！");
                    break;
                case -110:
                    alert("文件 [" + file.name + "] 大小超出系统限制的" + jQuery('#FileUpload_ItemPlan').uploadify('settings', 'fileSizeLimit') / 1024 + "M大小！");
                    break;
                case -120:
                    alert("文件 [" + file.name + "] 大小异常！");
                    break;
                case -130:
                    alert("文件 [" + file.name + "] 类型不正确！");
                    break;
            }
        },
        'onCancel': function (queueItemCount) {
            alert("取消上传");
            return false;
        },
        'onUploadSuccess': function (file, data, response) {
            //if ($("#CPHMainContent_LabelAccessoryName").text() == "") {
            //    $("#CPHMainContent_LabelAccessoryName").append("已上传：" + file.name);
            //    $("#CPHMainContent_HiddenFieldAccessoryName").val(file.name);
            //    $("#CPHMainContent_HiddenFieldUrl").val(data);


            $("#CPHMainContent_tb_AttName_ItemPlan").val(file.name);
            $("#CPHMainContent_tb_AttPath_ItemPlan").val(data);

            $("#CPHMainContent_btn_Upload_ItemPlan").click();

            //}
            //else {
            //    $("#CPHMainContent_LabelAccessoryName").append("," + file.name);
            //    $("#CPHMainContent_HiddenFieldAccessoryName").val($("#CPHMainContent_HiddenFieldAccessoryName").val() + "," + file.name);
            //    $("#CPHMainContent_HiddenFieldUrl").val($("#CPHMainContent_HiddenFieldUrl").val() + "," + data);
            //}

        }
    });


    jQuery("#FileUpload_Budget").uploadify({
        'buttonImage': '../../js/uploadify/browse-btn.png',
        'buttonText': '浏览',
        'auto': true,
        'successTimeout': 99999,
        'swf': '../../js/uploadify/uploadify.swf',
        'queueID': 'uploadfileQueue_Budget',
        'uploader': '../../js/UploadHandler.ashx',
        //'uploader': '../../js/Test.aspx',

        'fileSizeLimit': '102400',
        //'queueSizeLimit': 0,
        'queueSizeLimit': 1,

        'progressData': 'speed',
        'overrideEvents': ['onDialogClose'],
        'fileTypeExts': '*.*;',
        'fileTypeDesc': '请选择 flv',
        'onSelectError': function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("上传的文件数量已经超出系统限制的" + jQuery('#FileUpload_Budget').uploadify('settings', 'queueSizeLimit') + "个文件！");
                    break;
                case -110:
                    alert("文件 [" + file.name + "] 大小超出系统限制的" + jQuery('#FileUpload_Budget').uploadify('settings', 'fileSizeLimit') / 1024 + "M大小！");
                    break;
                case -120:
                    alert("文件 [" + file.name + "] 大小异常！");
                    break;
                case -130:
                    alert("文件 [" + file.name + "] 类型不正确！");
                    break;
            }
        },
        'onCancel': function (queueItemCount) {
            alert("取消上传");
            return false;
        },
        'onUploadSuccess': function (file, data, response) {
            //if ($("#CPHMainContent_LabelAccessoryName").text() == "") {
            //    $("#CPHMainContent_LabelAccessoryName").append("已上传：" + file.name);
            //    $("#CPHMainContent_HiddenFieldAccessoryName").val(file.name);
            //    $("#CPHMainContent_HiddenFieldUrl").val(data);


            $("#CPHMainContent_tb_AttName_Budget").val(file.name);
            $("#CPHMainContent_tb_AttPath_Budget").val(data);

            $("#CPHMainContent_btn_Budget").click();

            //}
            //else {
            //    $("#CPHMainContent_LabelAccessoryName").append("," + file.name);
            //    $("#CPHMainContent_HiddenFieldAccessoryName").val($("#CPHMainContent_HiddenFieldAccessoryName").val() + "," + file.name);
            //    $("#CPHMainContent_HiddenFieldUrl").val($("#CPHMainContent_HiddenFieldUrl").val() + "," + data);
            //}

        }
    })

    //});
}
//}, 10);



