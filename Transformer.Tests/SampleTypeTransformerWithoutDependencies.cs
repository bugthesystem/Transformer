namespace Transformer.Tests
{
    public class
        SampleTypeTransformerWithoutDependencies : ICustomTypeTransformer<SampleClientRequest, SampleInternalRequest>
    {
        /// <summary>The map.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="SampleInternalRequest" />.</returns>
        public SampleInternalRequest Map(ITransformContext<SampleClientRequest, SampleInternalRequest> context)
        {
            return new SampleInternalRequest { Filter = context.Source.Query };
        }
    }
}