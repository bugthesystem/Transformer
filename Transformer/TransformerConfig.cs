namespace Transformer
{
    using System;
    using System.Threading;

    public class TransformerConfig
    {
        /// <summary>The instance.</summary>
        private static readonly Lazy<TransformerConfig> LazyInstance =
            new Lazy<TransformerConfig>(LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>The dependency resolver.</summary>
        private IDependencyResolverAdapter resolver;

        /// <summary>The instance.</summary>
        public static TransformerConfig Instance => LazyInstance.Value;

        /// <summary>The get dependency resolver.</summary>
        /// <returns>The <see cref="Func{Type, Object}" />.</returns>
        public Func<Type, object> GetCurrentDependencyResolver() => this.resolver.GetService;

        /// <summary>The set dependency resolver.</summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        public void SetDependencyResolver(IDependencyResolverAdapter dependencyResolver) => this.resolver = dependencyResolver;
    }
}