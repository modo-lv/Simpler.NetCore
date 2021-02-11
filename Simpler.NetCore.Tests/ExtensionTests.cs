using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Simpler.NetCore.Tests {
  public class ExtensionTests {
    [Theory]
    [InlineData(data: null)]
    [InlineData("Text")]
    void Maybe(String? value) {
      value.ToMaybe().IsEmpty.Should().Be(value == null);
    }

    [Theory]
    [InlineData(data: null)]
    [InlineData("Text")]
    void IfNotNullFuncNullable(String? value) {
      value.IfNotNull(_ => _ + _).Should().Be(value == null ? null : "TextText");
    }
    
    [Theory]
    [InlineData(data: null)]
    [InlineData("Text")]
    void IfNotNullFunc(String? value) {
      value.IfNotNull(_ => _ + _, "Nothing").Should().Be(value == null ? "Nothing" : "TextText");
    }

    [Fact]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    void IfNotNullAction() {
      IList<Boolean>? n = null;
      IList<Boolean>? s = new List<Boolean>();

      n.IfNotNull(l => l!.Add(true)).Should().BeFalse();
      s.IfNotNull(l => l!.Add(true)).Should().BeTrue();

      n.Should().BeNull();
      s.Should().ContainSingle(_ => _);
    }
  }
}
