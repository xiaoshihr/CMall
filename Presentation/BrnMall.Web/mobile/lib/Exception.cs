using System;
using System.Collections.Generic;
using System.Web;

namespace BrnMall.WxPayAPI
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}