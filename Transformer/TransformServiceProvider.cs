namespace Transformer
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    /// <summary>The mapping service provider.</summary>
    public class TransformServiceProvider : ITransformServiceProvider
    {
        /// <summary>The dependency resolver.</summary>
        private readonly Func<Type, object> dependencyResolver;

        /// <summary>The type converter registry.</summary>
        private readonly ConcurrentDictionary<MapKey, Func<object, object>> transformersMap =
            new ConcurrentDictionary<MapKey, Func<object, object>>();

        /// <summary>Initializes a new instance of the <see cref="TransformServiceProvider" /> class.</summary>
        /// <param name="dependencyResolver">The resolver.</param>
        public TransformServiceProvider(Func<Type, object> dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        /// <inheritdoc />
        public TDestination Map<TSource, TDestination>(TSource input)
        {
            var converter = this.transformersMap.SingleOrDefault(
                x => x.Key.Source == typeof(TSource) && x.Key.Destination == typeof(TDestination));

            if (converter.Value != null)
            {
                var converted = converter.Value.Invoke(input);
                return (TDestination)converted;
            }

            throw new Exception(
                $"There is no converter registered for Source type: {typeof(TSource).Name} and Destination type: {typeof(TDestination).Name}");
        }

        /// <summary>The register custom.</summary>
        /// <param name="transformFunc">The transform function.</param>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDestination">Destination type.</typeparam>
        public void RegisterCustom<TSource, TDestination>(Func<TSource, TDestination> transformFunc)
        {
            var registered = this.transformersMap.TryAdd(
                new MapKey(typeof(TSource), typeof(TDestination)),
                input => transformFunc((TSource)input));

            if (!registered)
                throw new Exception(
                    $"Registering converter for Source type: {typeof(TSource).Name} and Destination type: {typeof(TDestination).Name} is failed.");
        }

        /// <summary>The register custom.</summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDestination">Destination type.</typeparam>
        /// <typeparam name="TConverter">Custom type converter.</typeparam>
        public void RegisterCustom<TSource, TDestination, TConverter>()
            where TConverter : ICustomTypeTransformer<TSource, TDestination>
        {
            var registered = this.transformersMap.TryAdd(
                new MapKey(typeof(TSource), typeof(TDestination)),
                input =>
                    {
                        var transformer = this.ResolveTransformer<TSource, TDestination, TConverter>();
                        var context = new TransformContext<TSource, TDestination>((TSource)input);
                        return transformer.Map(context);
                    });

            if (!registered)
                throw new Exception(
                    $"Registering converter for Source type: {typeof(TSource).Name} and Destination type: {typeof(TDestination).Name} is failed.");
        }

        /// <summary>The resolve converter.</summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDestination">Destination type.</typeparam>
        /// <typeparam name="TTransformer">Custom type converter.</typeparam>
        /// <returns>The <see cref="ICustomTypeTransformer{TSource,TDestination}" /> converter.</returns>
        private ICustomTypeTransformer<TSource, TDestination> ResolveTransformer<TSource, TDestination, TTransformer>()
            where TTransformer : ICustomTypeTransformer<TSource, TDestination>
        {
            TTransformer converter;
            if (this.dependencyResolver != null) converter = (TTransformer)this.dependencyResolver(typeof(TTransformer));
            else

                // Converter has to have parameter-less constructor.
                converter = Activator.CreateInstance<TTransformer>();

            return converter;
        }

        /// <summary>The type map to hold source to destination types.</summary>
        private struct MapKey
        {
            /// <summary>Initializes a new instance of the <see cref="MapKey" /> struct.</summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            public MapKey(Type source, Type destination)
            {
                this.Source = source;
                this.Destination = destination;
            }

            /// <summary>Gets the source.</summary>
            public Type Source { get; }

            /// <summary>Gets the destination.</summary>
            public Type Destination { get; }
        }
    }
}