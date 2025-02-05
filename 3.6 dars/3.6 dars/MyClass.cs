using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._6_dars;

public class MyClass
{
    public  long id { get; set; }
    private static MyClass _instance;
    private MyClass()
    {
        
    }
    public static MyClass GetInstanse()
    {
       if (_instance == null)
        {
            _instance = new MyClass();
        }
        return _instance;
    }
}
