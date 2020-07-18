using LocalFHIRRepository.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.ParameterContainer
{
    public abstract class BaseParameterContainer
    {
        abstract public bool IsValid();
        abstract public string OutputJson();
        abstract public string SynchronizeFileName();
    }
}
