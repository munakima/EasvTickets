using BLL.WebApi.Gateway.Core;

#if DEBUG
using BLL.WebApi.Gateway.TestHelper;
#endif

namespace BLL.WebApi.Gateway.Common
{
    public static class BLLGatewayFactory
    {
        private static IBLLGateway _bllGateway;

#if DEBUG
        public static IBLLGateway CreateOrGet()
        {
            if(_bllGateway == null)
            {
                _bllGateway = new FakeBLLGateway();
            }
            return _bllGateway;
        } 

        public static IBLLGateway CreateOrGetIgnoreDebug()
        {
            if (_bllGateway == null)
            {
                _bllGateway = new BLLGateway();
            }
            return _bllGateway;
        }
#else
        public static IBLLGateway CreateOrGet()
        {
            if(_bllGateway == null)
            {
                _bllGateway = new BLLGateway();
            }
            return _bllGateway;
        }
#endif
    }
}
