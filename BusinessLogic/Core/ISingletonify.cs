using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Core;

public interface ISingletonify
{
    SingletonType GetInstance<SingletonType>() { throw new NotImplementedException(); }
}
