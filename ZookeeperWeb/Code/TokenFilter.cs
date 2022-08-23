using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.AuthTokens;
using WebApiClient.Contexts;

namespace ZookeeperBrowser.Code
{
    /// <summary>
    /// JWT Headers中加入AccessToken
    /// </summary>
    public class TokenFilter : ApiActionFilterAttribute
    {
        public override Task OnBeginRequestAsync(ApiActionContext context)
        {
            if(context.Exception != null)
            {
                return Task.CompletedTask;
            }
            try
            {
                var _loginInfo = context.GetService<ILoginInfo>();
                context.RequestMessage.Headers.Add("Token", _loginInfo.AccessToken);
            }
            catch (System.Exception)
            {
                return Task.CompletedTask;
            }
            return base.OnBeginRequestAsync(context);
        }
    }

}
