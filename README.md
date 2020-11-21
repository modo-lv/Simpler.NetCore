# Simpler.NetCore

![](https://github.com/modo-lv/Simpler.NetCore/workflows/Tests/badge.svg)

## Extension methods

### `.IfNotNull(Action)`

A fluent alternative to `if (obj != null) { /* process object */ }`. Mainly useful when modifying/assigning properties.

```cs
IList<Boolean>? n = null;
IList<Boolean>? s = new List<Boolean>();

n.IfNotNull(l => l!.Add(true)); // Returns `false`, `n` remains `null`
s.IfNotNull(l => l!.Add(true)); // Returns `true`, `s` now contains one boolean element: `true`
```


## `Maybe`, `May.Be`

A thin wrapper turning a nullable value into a single-element collection.
This allows to operate with null/non-null values as with empty/non-empty collection.

```cs
var yes = May.Be("yes");
var no = May.Be(null);

yes.Any(); // true
no.Any();  // false
```

```cs
var values = new[] { "a", "b", "c" };
var yes = May.Be("X");
var no = May.Be((String)null!);
String.Join("", yes.Concat(no).Concat(values)); // Xabc
```