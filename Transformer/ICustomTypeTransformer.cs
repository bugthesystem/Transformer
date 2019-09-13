namespace Transformer
{
    public interface ICustomTypeTransformer<in TSource, TDestination>
    {
        /// <summary>The map.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="TDestination" />.</returns>
        TDestination Map(ITransformContext<TSource, TDestination> context);
    }
}