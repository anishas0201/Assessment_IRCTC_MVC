using Assessment_IRCTC_Revervation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment_IRCTC_Revervation.Interface
{
    public interface RegisterInterface
    {
        ResponseModel SignUpAction(RegisterUser objmodel);
        ResponseModel CheckLogin(RegisterUser objmodel);
    }
}
