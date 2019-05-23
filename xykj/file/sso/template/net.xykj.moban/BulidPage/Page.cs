using net.xykj.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace net.xykj.moban.BulidPage
{
    public class Page : XYPage
    {
        protected override void OnInit(EventArgs e)
        {
            var cmd = Request.Params["cmd"];
            var xmsg = new xy_code();
            switch (cmd)
            {
                case "reg":
                    var type = "xy.user.reg";
                    var regtype = "acc";
                    var uname = Request.Params["uname"];
                    var password = Request.Params["password"];
                    var source = "xykjtest";
                    var time = net.xykj.common.XY.TimeSpan;
                    var sign = net.xykj.common.XY.MD5(type + "#" + regtype + "#" + uname + "#" + password + "#" + source + "#" + time + "");
                    string url = string.Format("{0}?type={1}&regtype={2}&uname={3}&password={4}&source={5}&time={6}&sign={7}", "http://172.168.1.111:9003/", type, regtype, uname, password, source, time, sign);
                    //string url = string.Format("{0}?type={1}&regtype={2}&uname={3}&password={4}&source={5}&time={6}&sign={7}", "http://localhost:53769/index.aspx", type, regtype, uname, password, source, time, sign);
                    var msg = net.xykj.common.XY.UrlGet(url);
                    var data = common.XY.JToUnJson<xy_code>(msg);
                    if (data.code == 1)
                    {
                        common.XY.Cookie("token", data.data.ToString(), common.XY.NowTime.AddDays(1));
                        xmsg.data = data.data;
                        xmsg.code = 1;
                    }
                    break;
                case "online":
                    var col = common.XY.Cookie("token");
                    if (col != null)
                    {
                        type = "xy.user.token";
                        var token = col;
                        time = common.XY.TimeSpan;
                        sign = common.XY.MD5(string.Format("{0}#{1}#{2}", type, time, token));
                        url = string.Format("{0}?type={1}&token={2}&time={3}&sign={4}", "http://172.168.1.111:9003/", type, token, time, sign);
                        //url = string.Format("{0}?type={1}&token={2}&time={3}&sign={4}", "http://localhost:53769/index.aspx", type, token, time, sign);
                        xmsg = common.XY.JToUnJson<xy_code>(common.XY.UrlGet(url));
                        //xmsg = new xy_code() {  data = url };
                    }
                    break;
                case "logout":
                    var cols = common.XY.Cookie("token");
                    if (cols != null)
                    {
                        type = "xy.user.logout";
                        var token = cols;
                        time = common.XY.TimeSpan;
                        sign = common.XY.MD5(string.Format("{0}#{1}#{2}", type, time, token));
                        url = string.Format("{0}?type={1}&token={2}&time={3}&sign={4}", "http://172.168.1.111:9003/", type, token, time, sign);
                        //url = string.Format("{0}?type={1}&token={2}&time={3}&sign={4}", "http://localhost:53769/index.aspx", type, token, time, sign);
                        xmsg = common.XY.JToUnJson<xy_code>(common.XY.UrlGet(url));
                    }
                    break;
                default:
                    break;
            }
            Response.Clear();
            Response.Write(common.XY.JToJson(xmsg));
            base.OnInit(e);
        }
    }
}
