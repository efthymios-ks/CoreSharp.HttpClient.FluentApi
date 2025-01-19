using AutoFixture;

namespace Tests.Common;

public abstract class TestsBase
{
    private readonly IFixture _fixture = null!;

    protected TestsBase()
    {
        _fixture = FixtureUtils.CreateFixture();

        ConfigureFixture(_fixture);
    }

    protected virtual void ConfigureFixture(IFixture fixture)
    {
    }

    protected TElement MockCreate<TElement>()
        => _fixture.Create<TElement>();

    protected TElement[] MockCreateMany<TElement>()
        => _fixture
            .CreateMany<TElement>()
            .ToArray();

    protected TElement[] MockCreateMany<TElement>(int count)
        => _fixture
            .CreateMany<TElement>(count)
            .ToArray();

    protected TElement MockFreeze<TElement>()
        => _fixture.Freeze<TElement>();
}
