using PAET.Infraestructure;
using PAET.Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.Test.FakeContext
{
    public class FakePAETContext : DbContext, IFakeContext
    {
        public IDbSet<Preguntas> RPreguntas { get; set; }
    }
}
