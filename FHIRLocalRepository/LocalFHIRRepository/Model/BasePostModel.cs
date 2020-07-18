using LocalFHIRRepository.ParameterContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.Model
{
    public abstract class BasePostModel : IDisposable
    {
        /// <summary>
        /// POST処理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        abstract public bool TryPost(BaseParameterContainer value);
        public void Dispose()
        {
            this.OnDispose();
        }
        protected virtual void OnDispose() { }
    }
}
