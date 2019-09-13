namespace Transformer
{
    /// <summary>The Custom Type transformer interface.</summary>
    /// <typeparam name="TSource">Source type</typeparam>
    /// <typeparam name="TDestination">Destination type</typeparam>
    public interface ICustomTypeTransformer<in TSource, TDestination>
    {
        /// <summary>The map.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="TDestination" />.</returns>
        TDestination Map(ITransformContext<TSource, TDestination> context);
    }
}