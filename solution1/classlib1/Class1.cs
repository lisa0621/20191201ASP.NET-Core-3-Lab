using System;

namespace classlib1
{
    public class Class1
    {
        public string Go(){
            ////return "Go!!!!!!!!";
            #if NET472
            return "NET 472";
            #else
            return ".NET Standand 2.0";
            #endif
        }
    }
}
