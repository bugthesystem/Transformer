namespace Transformer
{
    using System;

    /// <summary>The TransformProfile interface.</summary>
    public interface ITransformProfile
    {
        /// <summary>Gets transform service provider.</summary>
        /// <param name="dependencyResolver">The dependency Resolver.</param>
        /// <returns>The <see cref="ITransformServiceProvider" />.</returns>
        ITransformServiceProvider GetTransformServiceProvider(Func<Type, object> dependencyResolver = null);
    }
}