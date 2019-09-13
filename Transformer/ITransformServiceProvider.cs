namespace Transformer
{
    /// <summary>The MappingServiceProvider interface.</summary>
    public interface ITransformServiceProvider
    {
        /// <summary>The map.</summary>
        /// <param name="input">The input.</param>
        /// <typeparam name="TSource">The source type,</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <returns>The <see cref="System.Type" /> destination type..</returns>
        TDestination Map<TSource, TDestination>(TSource input);
    }
}