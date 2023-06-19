using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_TT.Common
{
    public enum EnumErrCode
    {
        Error = -1, 
        Fail,
        Success,
        InvalidInput,
        NotYetLogin,
        EmptyData,
        ExistentUnique
    }
}