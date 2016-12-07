


//发放红包
function sendBag(sendid) {
   
    $("#se_" + sendid).attr('disabled', "true");
    var sendid = sendid;
   

    if (sendid < 1) {
        alert("参数错误");
        return;
    }
    //if (!verifyShipAddress(consignee, mobile, regionId, address)) {
    //    return;
    //}

    $.post("/mob/weixinweb/SendBag",
            { 'sendid': sendid },
            GetsendBagResponse)
}
//处理发送红包反馈信息
function GetsendBagResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        var msg = "";
        //for (var i = 0; i < result.content.length; i++) {
        //    msg += result.content + "\n";
        //}
        alert(result.content)
        window.location.href = "/mob/weixinweb/SendBag?Sendtype=" + $("#stype").val();
        
    }
    //else if (result.state == "exception") {
    //    alert(result.content);
    //}
    else
    {

        alert(result.content)
        //$(this).attr('disabled', "false");
        window.location.href = "/mob/weixinweb/SendBag?Sendtype=" + $("#stype").val();
    }
}
//修改推荐人
function MyParentUp() {

    var MyParentForm = document.forms["MyParentForm"];

    var txtMyParent = MyParentForm.elements["txtMyParent"].value;
   

    $.post("/mob/weixinweb/MyParentUpdate",
            { 'txtMyParent': txtMyParent },
            GetMyParentUpResponse)
}
//处理修改推荐人反馈信息
function GetMyParentUpResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        var msg = "";
       
        alert(result.content)
        window.location.href = "/mob/weixinweb/MyParent";

    }
        
    else {

        alert(result.content)
      
    }
}

