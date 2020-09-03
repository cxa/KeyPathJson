# KeyPathJson

Safe and key-path-able JSON navigator for `System.Json`.

(For navigating `System.Text.Json`, use [JPath](https://github.com/cxa/JPath))

## Install

Simply drop `KeyPathJson.fsproj` or `KeyPathJson.fs` to your project, or search `KeyPathJson` on NuGet.

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

Say you need to access the second `menuitem`'s `value`, with `KeyPathJson` this is a piece of cake:

```fsharp
open KeyPathJson

// result is a Result<string, exn>
let result =
  Json.parse jsonStr
  |> Json.string "menu.popup.menuitem.1.value"

```

One thing to aware is that you must use a number as a key to access array, like `menuitem.1` in the above example.

Except accessing string, `KeyPathJson` also provides:

```fsharp
val value :
      keyPath:string ->
        jsonValue:JsonValue -> Result<JsonValue,exn>

val bool :
  keyPath:string -> jsonValue:JsonValue -> Result<bool,exn>

val byte :
  keyPath:string -> jsonValue:JsonValue -> Result<byte,exn>

val sbyte :
  keyPath:string -> jsonValue:JsonValue -> Result<sbyte,exn>

val int8 :
  keyPath:string -> jsonValue:JsonValue -> Result<int8,exn>

val uint8 :
  keyPath:string -> jsonValue:JsonValue -> Result<uint8,exn>

val int16 :
  keyPath:string -> jsonValue:JsonValue -> Result<int16,exn>

val uint16 :
  keyPath:string -> jsonValue:JsonValue -> Result<uint16,exn>

val int32 :
  keyPath:string -> jsonValue:JsonValue -> Result<int,exn>

val uint32 :
  keyPath:string -> jsonValue:JsonValue -> Result<uint32,exn>

val int64 :
  keyPath:string -> jsonValue:JsonValue -> Result<int64,exn>

val uint64 :
  keyPath:string -> jsonValue:JsonValue -> Result<uint64,exn>

val decimal :
  keyPath:string -> jsonValue:JsonValue -> Result<decimal,exn>

val double :
  keyPath:string -> jsonValue:JsonValue -> Result<double,exn>

val float32 :
  keyPath:string -> jsonValue:JsonValue -> Result<float32,exn>

val char :
  keyPath:string -> jsonValue:JsonValue -> Result<char,exn>

val string :
  keyPath:string -> jsonValue:JsonValue -> Result<string,exn>

val dateTime :
  keyPath:string ->
    jsonValue:JsonValue -> Result<System.DateTime,exn>

val dateTimeOffset :
  keyPath:string ->
    jsonValue:JsonValue -> Result<System.DateTimeOffset,exn>

val guid :
  keyPath:string ->
    jsonValue:JsonValue -> Result<System.Guid,exn>

val timeSpan :
  keyPath:string ->
    jsonValue:JsonValue -> Result<System.TimeSpan,exn>

val uri :
  keyPath:string ->
    jsonValue:JsonValue -> Result<System.Uri,exn>
```

That's all.

## Usage

Drag `src/KeyPathJson.fs` to your project or get it from NuGet: <https://www.nuget.org/packages/KeyPathJson/>

## License

MIT
