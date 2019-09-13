namespace Transformer
{
    /// <summary>The transform context.</summary>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Destination type.</typeparam>
    public class TransformContext<TSource, TDestination> : ITransformContext<TSource, TDestination>
    {
        /// <summary>Initializes a new instance of the <see cref="TransformContext{TSource,TDestination}" /> class.</summary>
        /// <param name="source">The source.</param>
        public TransformContext(TSource source)
        {
            this.Source = source;
        }

        /// <summary>Gets or sets the destination.</summary>
        public TDestination Destination { get; set; }

        /// <summary>Gets the source.</summary>
        public TSource Source { get; }
    }
}