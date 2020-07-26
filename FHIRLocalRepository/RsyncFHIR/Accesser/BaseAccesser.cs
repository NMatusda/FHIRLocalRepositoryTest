using MongoDB.Bson.IO;
using Newtonsoft.Json.Linq;
using RsyncFHIR.Resource;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace RsyncFHIR.Accesser
{
    /// <summary>
    /// https://docs.microsoft.com/ja-jp/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-3.1&tabs=visual-studio
    /// </summary>
    public abstract class BaseAccesser
    {
        public abstract List<BaseResource> Get();
        public abstract BaseResource Get(string id);
        public abstract BaseResource Create(BaseResource resource);
        public abstract void Update(string id, BaseResource resource);
        public abstract void Remove(BaseResource resource);
        public abstract void Remove(string id);
    }
}
