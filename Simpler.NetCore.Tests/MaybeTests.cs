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
    [InlineData(data: null)]
    void Map(String? value) {
      Maybe<String> maybe = May.Be(value);
      maybe.Map(_ => 1).GetOr(fallback: 0).Should().Be(value == null ? 0 : 1);
    }

    [Theory]
    [InlineData("Yes")]
    [InlineData(data: null)]
    void MapGetOr(String? value) {
      May.Be(value).MapGetOr(_ => _ + _, "Nothing").Should().Be(value == null ? "Nothing" : "YesYes");
    }

    [Fact]
    void Concat() {
      var values = new[] { "a", "b", "c" };
      var yes = May.Be("X");
      var no = May.Be((String)null!);
      String.Join("", yes.Concat(no).Concat(values)).Should().Be("Xabc");
    }

    [Fact]
    void SetIfEmpty() {
      May.BeNot<String>().SetIfEmpty("full").Should().BeEquivalentTo(May.Be("full"));
    }
  }
}
