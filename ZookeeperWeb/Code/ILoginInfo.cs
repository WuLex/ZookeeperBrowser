using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZookeeperBrowser.Code
{
    public interface ILoginInfo
    {
        string AccessToken { get; }
        string RefreshToken { get; }
        string ExpiresIn { get; }
    }
}
