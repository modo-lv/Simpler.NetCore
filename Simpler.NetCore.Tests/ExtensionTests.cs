using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Simpler.NetCore.Tests {
  public class ExtensionTests {
    [Fact]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    void IfNotNullAction() {
      IList<Boolean> n = null;
      IList<Boolean> s = new List<Boolean>();

      n.IfNotNull(l => l.Add(true)).Should().BeFalse();
      s.IfNotNull(l => l.Add(true)).Should().BeTrue();

      n.Should().BeNull();
      s.Should().ContainSingle(_ => _);
    }
  }
}
