using System;
using System.Linq;
using FluentAssertions;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Simpler.NetCore.Tests {
  public class MaybeTests {
    [Fact]
    void YesValue() {
      var maybe = May.Be("Text");
      maybe.Value.Should().Be("Text");
    }

    [Fact]
    void NoValue() {
      var maybe = May.Be<Int32?>(null);
      FluentActions.Invoking(() => maybe.Value).Should().Throw<NullReferenceException>();
    }

    [Theory]
    [InlineData("Yes")]
    [InlineData(null)]
    void SelectMaybe(String value) {
      var maybe = May.Be(value);
      maybe.ValueOr("No").Should().Be(value == null ? "No" : "Yes");
    }

    [Fact]
    void Concat() {
      var values = new[] { "a", "b", "c" };
      var yes = May.Be("X");
      var no = May.Be((String)null!);
      String.Join("", yes.Concat(no).Concat(values))
        .Should().Be("Xabc");
    }
  }
}
