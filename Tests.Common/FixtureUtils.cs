using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace Tests.Common;

public static class FixtureUtils
{
    public static IFixture CreateFixture()
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoNSubstituteCustomization()
        {
            ConfigureMembers = true,
        });

        fixture.Register<IFixture>(() => fixture);
        fixture.Register(() => false);

        return fixture;
    }
}
