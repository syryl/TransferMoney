using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Application;
public class AppStartupOptions
{
    public bool UsingLoggingBehavior { get; private set; } = true;
    public bool UsingValidation { get; private set; } = true;
    public void UseLoggingBehavior(bool usingLog) => UsingLoggingBehavior = usingLog;
    public void UseValidation(bool usingVal) => UsingValidation = usingVal;
}
