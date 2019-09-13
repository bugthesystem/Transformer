namespace Transformer.Tests
{
    using System;

    using Xunit;

    public class TransformServiceProviderTests
    {
        [Fact]
        public void Register_custom_mapping_func_and_map_test()
        {
            var provider = new TransformServiceProvider(null);

            provider.RegisterCustom<SampleClientRequest, SampleInternalRequest>(
                clientRequest => new SampleInternalRequest { Filter = clientRequest.Query });

            AssertMap(provider);
        }

        [Fact]
        public void Register_custom_type_converter_with_dependencies_and_map_test()
        {
            Func<Type, object> stubDependencyResolver = type =>
                {
                    if (type == typeof(SampleTypeTransformerWithDependencies))
                        return new SampleTypeTransformerWithDependencies(new SampleComponent());

                    return null;
                };

            var provider = new TransformServiceProvider(stubDependencyResolver);

            provider
                .RegisterCustom<SampleClientRequest, SampleInternalRequest, SampleTypeTransformerWithDependencies>();

            AssertMap(provider);
        }

        [Fact]
        public void Register_custom_type_converter_without_dependencies_and_map_test()
        {
            var provider = new TransformServiceProvider(null);

            provider
                .RegisterCustom<SampleClientRequest, SampleInternalRequest, SampleTypeTransformerWithoutDependencies>();

            AssertMap(provider);
        }

        private static void AssertMap(TransformServiceProvider provider)
        {
            var sampleClientRequest =
                new SampleClientRequest { Id = Guid.NewGuid().ToString(), Query = "Name eq 'Yo Rick!'" };
            var result = provider.Map<SampleClientRequest, SampleInternalRequest>(sampleClientRequest);

            Assert.NotNull(result);
            Assert.NotNull(result.Filter == sampleClientRequest.Query);
        }
    }
}