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


## `Maybe`

A thin wrapper turning a nullable value into a single-element collection.
This allows to operate with null/non-null values as with empty/non-empty collection.

### `.Get()`, `.GetOr(..)`
Retrieve the value of the Maybe, throwing an exception or providing a fallback value if the Maybe doesn't have a value.  

```cs
var yes = May.Be("yes");
var no = May.Be<String>(null);

yes.Any(); // true
no.Any();  // false

yes.Get(); // "yes"
no.Get();  // NullReferenceException

no.GetOr("full");  // "full"
no.GetOr("empty"); // "empty"
```

```cs
var values = new[] { "a", "b", "c" };
var yes = May.Be("X");
var no = May.Be((String)null!);
String.Join("", yes.Concat(no).Concat(values)); // "Xabc"
```


### `.Set()`, `.SetIfEmpty()`
```cs
May.BeNot<Int32>().Set(1);              // Maybe<Int32>(1)
May.BeNot<String>().SetIfEmpty("full"); // Maybe<String>("full")
```


### `.Map`
Run a function on the value of `Maybe` if present.

```cs
var yes = May.Be("yes");
var no = May.BeNot<String>();
yes.Map(_ => true) // Maybe<Boolean>(true)
no.Map(_ => 1)     // Maybe<Int32>(null)
``` 