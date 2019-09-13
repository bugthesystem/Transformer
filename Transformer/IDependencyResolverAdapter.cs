namespace Transformer
{
    using System;

    public interface IDependencyResolverAdapter
    {
        object GetService(Type arg);
    }
}