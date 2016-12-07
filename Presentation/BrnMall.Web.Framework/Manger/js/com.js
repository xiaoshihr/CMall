function over() {
    window.location.href = "/login.aspx";
}

function clk(o) {
    var id = o.id;
    var url = o.tag;
    url += "?id=" + escape(id) + "&da=" + new Date();
    window.location.href = url;
}

//js去除空格函数
//此处为string类添加三个成员
String.prototype.Trim = function () { return Trim(this); }
String.prototype.LTrim = function () { return LTrim(this); }
String.prototype.RTrim = function () { return RTrim(this); }

//此处为独立函数
function LTrim(str) {
    var i;
    for (i = 0; i < str.length; i++) {
        if (str.charAt(i) != " " && str.charAt(i) != " ") break;
    }
    str = str.substring(i, str.length);
    return str;
}
function RTrim(str) {
    var i;
    for (i = str.length - 1; i >= 0; i--) {
        if (str.charAt(i) != " " && str.charAt(i) != " ") break;
    }
    str = str.substring(0, i + 1);
    return str;
}
function Trim(str) {
    return LTrim(RTrim(str));
}


function putput() {
    var pwd = document.getElementById("pwd").value;
    var names = document.getElementById("name").value;
    if (pwd == "") {
        alert("请输入独立密码！");
        return false;
    }
    else if (names == "") {
        alert("请输入授权人信息！");
        return false;
    }
    else {
        var temp = WeiBusiness.Ceack.outputs(pwd, names).value;
        if (temp == "OK") {
            alert("通过验证，请再次点击按钮进行相关操作！");
            return true;
        }
        else {
            alert("输入有误请重试！");
            return false;
        }
    }
}

var bol = false;
function mima() {
    if (bol == false) {
        $.ajax({ url: 'Ceack.aspx?da=' + new Date(), type: 'get', dataType: 'html', success: function (data) {
            art.dialog({
                title: '权限验证',
                content: data,
                icon: 'succeed',
                button: [
                {
                    name: '关闭我'
                }
              ],
                ok: function () {
                    bol = putput();
                    if (bol) {
                        mima();
                    }
                    return bol;
                }
            })
        }
        })
    }
    return bol;
}