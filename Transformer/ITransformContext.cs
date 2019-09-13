namespace Transformer
{
    /// <summary>The Transform Context interface.</summary>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Destination type.</typeparam>
    public interface ITransformContext<out TSource, TDestination>
    {
        /// <summary>Gets or sets the destination.</summary>
        TDestination Destination { get; set; }

        /// <summary>Gets the source.</summary>
        TSource Source { get; }
    }
}