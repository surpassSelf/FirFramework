using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirServer.Managers
{
    public interface IManager
    {
        void Initialize();
        void OnDispose();
    }
}
