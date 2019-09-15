using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Models.Interfaces
{
    public interface IModel : IMake
    {

        int MakeId { get; set; }
        IMake Make { get; set; }
    }
}
