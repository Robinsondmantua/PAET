using PAET.Infraestructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.Test.Interfaces
{
    public interface IFakeContext
    {
        IDbSet<Preguntas> RPreguntas { get; set; }
    }
}
