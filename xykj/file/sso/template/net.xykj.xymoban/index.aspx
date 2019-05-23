<%@ Page Language="C#" AutoEventWireup="true" Inherits="net.xykj.moban.BulidPage.Page" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="http://172.168.1.111:9002/data/plugin/js/jquery.min.js"></script>
    <script type="text/javascript" src="http://172.168.1.111:9002/data/plugin/js/md5.js"></script>
</head>
<body>
    <p>信游-单点登录-注册机制-demo</p>
    <p>2019.5.22</p>
    <div>账号：<input type="text" id="uname" /></div>
    <div>密码：<input type="text" id="password" /></div>
    <p></p>
    <div>
        <button onclick="reg()">注册</button>
    </div>
    <div id="log">
        <p></p>
        <p>信游-单点登录-登录机制-demo</p>
        <div>账号：<input type="text" id="loguname" /></div>
        <div>密码：<input type="text" id="logpassword" /></div>
        <p></p>
        <div>
            <button onclick="login()">登录</button>
        </div>
    </div>
    <div id="off" hidden>
        <p>信游-单点登录-退出机制-demo</p>
        <div>
            <button onclick="logout()">退出</button>
        </div>
    </div>
    <p>信游-检测在线机制-demo</p>
    <div>
        <p>
            <button id="msg"></button>
        </p>
    </div>
    <script type="text/javascript">
        function reg() {
            var parm = {
                cmd: "reg",
                uname: $("#uname").val(),
                password: $("#password").val()
            };
            $.ajax({
                type: "GET",
                url: "/data/system/index.aspx",
                data: parm,
                dataType: "json",
                success: function (data) {
                    console.log(data);
                    if (data.code) {
                        location.reload();
                    }
                    console.log(111111)
                    $("#msg").html(data.url);
                },
            });
        }

        function login() {
            var parm = {
                cmd: "login",
                uname: $("#loguname").val(),
                password: $("#logpassword").val()
            };
            $.ajax({
                type: "GET",
                url: "/data/system/index.aspx",
                data: parm,
                dataType: "json",
                success: function (data) {
                    if (data.code) {
                        location.reload();
                    }
                    $("#msg").html(data.url);
                },
            });
        }

        function logout() {
            var parm = {
                cmd: "logout",
            };
            $.ajax({
                type: "GET",
                url: "/data/system/index.aspx",
                data: parm,
                dataType: "json",
                success: function (data) {
                    if (data.code) {
                        location.reload();
                    }
                    $("#msg").html(data.url);
                },
            });
        }


        $(function () {
            var parm = {
                cmd: "online",
            };
            $.ajax({
                type: "GET",
                url: "/data/system/index.aspx",
                data: parm,
                dataType: "json",
                success: function (data) {
                    if (data.code == 1) {
                        $("#msg").html("在线状态");
                        $("#log").hide();
                        $("#off").show();
                    } else {
                        $("#msg").html("离线状态");
                        $("#log").show();
                        $("#off").hide();
                    }

                },
            });
        })
    </script>
</body>
</html>
