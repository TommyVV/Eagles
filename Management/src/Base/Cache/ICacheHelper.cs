using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Base.Cache
{
    public interface ICacheHelper:IInterfaceBase
    {
        void SetData<T>(string key, T data);

        T GetData<T>(string key) where T : class;
    }
}
