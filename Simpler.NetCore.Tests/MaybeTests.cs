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
      maybe.Get.Should().Be("Text");
    }

    [Fact]
    void NoValue() {
      var maybe = May.BeNot<Int32>();
      FluentActions.Invoking(() => maybe.Get).Should().Throw<NullReferenceException>();
    }

    [Theory]
    [InlineData("Yes")]
    [InlineData(null)]
    void Map(String value) {
      var maybe = May.Be(value);
      maybe.Map(_ => 1).GetOr(0).Should().Be(value == null ? 0 : 1);
    }

    [Fact]
    void Concat() {
      var values = new[] { "a", "b", "c" };
      var yes = May.Be("X");
      var no = May.Be((String)null!);
      String.Join("", yes.Concat(no).Concat(values)).Should().Be("Xabc");
    }
  }
}
