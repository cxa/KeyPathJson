# KeyPathJson

Safe and key-path-able JSON navigator for `System.Json`.

## Example

Given a JSON like this

```json
{
  "menu": {
    "id": "file",
    "value": "File",
    "popup": {
      "menuitem": [
        { "value": "New", "onclick": "CreateNewDoc()" },
        { "value": "Open", "onclick": "OpenDoc()" },
        { "value": "Close", "onclick": "CloseDoc()" }
      ]
    }
  }
}
```

Say you need to acess the second `menuitem`'s `value`, with `KeyPathJson` this is a piece of cake:

```fsharp
open KeyPathJson

let result =
  Json.parse jsonStr
  |> Json.stringForKeyPath "menu.popup.menuitem.1.value"

// result is a Result<string, exn>
```

Onething to aware is that you must use number as key to access array, like `menuitem.1` in above example.

Except accessing string, `KeyPathJson` also provides:

```fsharp
val valueForKeyPath :
      keyPath:string ->
        jsonValue:JsonValue -> Result<JsonValue,exn>

val boolForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<bool,exn>

val byteForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<byte,exn>

val sbyteForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<sbyte,exn>

val int8ForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<int8,exn>

val uint8ForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<uint8,exn>

val int16ForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<int16,exn>

val uint16ForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<uint16,exn>

val int32ForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<int,exn>

val uint32ForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<uint32,exn>

val int64ForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<int64,exn>

val uint64ForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<uint64,exn>

val decimalForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<decimal,exn>

val doubleForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<double,exn>

val float32ForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<float32,exn>

val charForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<char,exn>

val stringForKeyPath :
  keyPath:string -> jsonValue:JsonValue -> Result<string,exn>

val dateTimeForKeyPath :
  keyPath:string ->
    jsonValue:JsonValue -> Result<System.DateTime,exn>

val dateTimeOffsetForKeyPath :
  keyPath:string ->
    jsonValue:JsonValue -> Result<System.DateTimeOffset,exn>

val guidForKeyPath :
  keyPath:string ->
    jsonValue:JsonValue -> Result<System.Guid,exn>

val timeSpanForKeyPath :
  keyPath:string ->
    jsonValue:JsonValue -> Result<System.TimeSpan,exn>

val uriForKeyPath :
  keyPath:string ->
    jsonValue:JsonValue -> Result<System.Uri,exn>
```

That's all.

## Usage

Drag `src/KeyPathJson.fs` to your project or get it from NuGet: <https://www.nuget.org/packages/KeyPathJson/>

## License

MIT
