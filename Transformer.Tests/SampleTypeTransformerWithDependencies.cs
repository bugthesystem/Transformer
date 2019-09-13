namespace Transformer.Tests
{
    public class SampleTypeTransformerWithDependencies 
        : ICustomTypeTransformer<SampleClientRequest, SampleInternalRequest>
    {
        /// <summary>The dependency.</summary>
        private readonly ISampleComponent component;

        public SampleTypeTransformerWithDependencies(ISampleComponent component) => this.component = component;

        /// <summary>The map.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="SampleInternalRequest" />.</returns>
        public SampleInternalRequest Map(ITransformContext<SampleClientRequest, SampleInternalRequest> context)
        {
            this.component.DoStuff();
            return new SampleInternalRequest { Filter = context.Source.Query };
        }
    }
}