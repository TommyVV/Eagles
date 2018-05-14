using Eagles.Application.Model.Test;
using Eagles.Base;

namespace Eagles.Interface.Core.Test
{
    public interface ITestHandler:IInterfaceBase
    {
        TestResponse Porcess(TestRequest request);

    }
}
