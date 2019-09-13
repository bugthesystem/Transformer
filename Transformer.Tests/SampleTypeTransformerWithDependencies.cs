namespace Transformer.Tests
{
    /// <summary>The sample type converter without dependencies.</summary>
    public class
        SampleTypeTransformerWithDependencies : ICustomTypeTransformer<SampleClientRequest, SampleInternalRequest>
    {
        /// <summary>The dependency.</summary>
        private readonly ISampleComponent component;

        /// <summary>Initializes a new instance of the <see cref="SampleTypeTransformerWithDependencies" /> class.</summary>
        /// <param name="component">The dependency.</param>
        public SampleTypeTransformerWithDependencies(ISampleComponent component)
        {
            this.component = component;
        }

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