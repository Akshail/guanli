jQuery(document).ready(function () {
    jQuery("#file_upload").uploadify({
        'buttonImage': '../../js/uploadify/browse-btn.png',
        'buttonText': '上传',
        'auto': true,
        'successTimeout': 99999,
        'swf': '../../js/uploadify/uploadify.swf',
        'queueID': 'uploadfileQueue',
        'uploader': '../../js/UploadHandler.ashx',
        'fileSizeLimit': '102400',
        'queueSizeLimit': 1,
        'progressData': 'speed',
        'overrideEvents': ['onDialogClose'],
        'fileTypeExts': '*.*;',
        'fileTypeDesc': '请选择 flv',
        'onSelectError': function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("上传的文件数量已经超出系统限制的" + jQuery('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
                    break;
                case -110:
                    alert("文件 [" + file.name + "] 大小超出系统限制的" + jQuery('#file_upload').uploadify('settings', 'fileSizeLimit') / 1024 + "M大小！");
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
            alert("文件" + file.name + "上传成功！");
            $("#LabelAccessoryName").html("已上传附件名称："+file.name.substring(0, file.name.length - file.type.length));
            $("#HiddenFieldAccessoryName").val(file.name);
            $("#HiddenFieldUrl").val(data);
        }
    });
});