using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IActive
{
    int MaxCharges
    {
        get; set;
    }

    int Charges
    {
        get; set;
    }

    string Type
    {
        get;
    }
    void ExecuteActive();
}
