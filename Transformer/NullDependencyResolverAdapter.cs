namespace Transformer
{
    using System;

    public class NullDependencyResolverAdapter : IDependencyResolverAdapter
    {
        public object GetService(Type arg) => null;
    }
}