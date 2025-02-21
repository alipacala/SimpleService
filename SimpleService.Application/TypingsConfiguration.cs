using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;

namespace SimpleService.Application;
public static class TypingsConfiguration
{
    public static void Configure(ConfigurationBuilder builder)
    {
        builder.Global(x =>
        {
            x.UseModules(true);
            x.AutoOptionalProperties(true);
            x.CamelCaseForProperties(true);
        });

        builder.Substitute(typeof(Guid), new RtSimpleTypeName("string"));
    }
}