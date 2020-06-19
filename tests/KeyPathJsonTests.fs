module Tests

open System
open System.Json
open System.Collections.Generic
open Xunit
open KeyPathJson

let requal (r1: Result<'a, _>) (r2: Result<'a, _>) =
  match r1, r2 with
  | Ok o1, Ok o2 -> Assert.True ((o1 = o2))
  | _ -> printfn "Exn: %A" r1; Assert.False (true)

[<Fact>]
let ``Test bool for keypath`` () =
  let t = true
  sprintf """{ "a": {"b": %b}}""" t
  |> Json.parse
  |> Result.bind (Json.bool "a.b")
  |> requal <| Ok t

[<Fact>]
let ``Test bool for keypath with root array`` () =
  let t = true
  sprintf """[%b]""" t
  |> Json.parse
  |> Result.bind (Json.bool "0")
  |> requal <| Ok t

[<Fact>]
let ``Test byte for keypath`` () =
  let b = Byte.MaxValue
  sprintf """{ "a": {"b": [%i]}}""" b
  |> Json.parse
  |> Result.bind (Json.byte "a.b.0")
  |> requal <| Ok b

[<Fact>]
let ``Test sbyte for keypath`` () =
  let sb = SByte.MaxValue
  sprintf """{ "a": {"b": [%i]}}""" sb
  |> Json.parse
  |> Result.bind (Json.sbyte "a.b.0")
  |> requal <| Ok sb

[<Fact>]
let ``Test int16 for keypath`` () =
  let i = Int16.MaxValue
  sprintf """{ "a": {"b": %i}}""" i
  |> Json.parse
  |> Result.bind (Json.int16 "a.b")
  |> requal <| Ok i

[<Fact>]
let ``Test uint16 for keypath`` () =
  let ui = UInt16.MaxValue
  sprintf """{ "a": {"b": %i}}""" ui
  |> Json.parse
  |> Result.bind (Json.uint16 "a.b")
  |> requal <| Ok ui

[<Fact>]
let ``Test int32 for keypath`` () =
  let i = Int32.MaxValue
  sprintf """{ "a": {"b": %i}}""" i
  |> Json.parse
  |> Result.bind (Json.int32 "a.b")
  |> requal <| Ok i

[<Fact>]
let ``Test uint32 for keypath`` () =
  let ui = UInt32.MaxValue
  sprintf """{ "a": {"b": %i}}""" ui
  |> Json.parse
  |> Result.bind (Json.uint32 "a.b")
  |> requal <| Ok ui

[<Fact>]
let ``Test int64 for keypath`` () =
  let i = Int64.MaxValue
  sprintf """{ "a": {"b": %i}}""" i
  |> Json.parse
  |> Result.bind (Json.int64 "a.b")
  |> requal <| Ok i

[<Fact>]
let ``Test uint64 for keypath`` () =
  let ui = UInt64.MaxValue
  sprintf """{ "a": {"b": %i}}""" ui
  |> Json.parse
  |> Result.bind (Json.uint64 "a.b")
  |> requal <| Ok ui

[<Fact>]
let ``Test double for keypath`` () =
  let d = 3.14
  sprintf """{ "a": {"b": %f}}""" d
  |> Json.parse
  |> Result.bind (Json.double "a.b")
  |> requal <| Ok d

[<Fact>]
let ``Test float for keypath`` () =
  let f = 3.14f
  sprintf """{ "a": {"b": %f}}""" f
  |> Json.parse
  |> Result.bind (Json.float32 "a.b")
  |> requal <| Ok f

[<Fact>]
let ``Test decimal for keypath`` () =
  let d = 0.7833m
  sprintf """{ "a": {"b": %M}}""" d
  |> Json.parse
  |> Result.bind (Json.decimal "a.b")
  |> requal <| Ok d

[<Fact>]
let ``Test char for keypath`` () =
  let c = 'a'
  sprintf """{ "a": {"b": "%c"}}""" c
  |> Json.parse
  |> Result.bind (Json.char "a.b")
  |> requal <| Ok c


[<Fact>]
let ``Test string for keypath`` () =
  let s = "Test string for keypath"
  sprintf """{ "a": {"b": "%s"}}""" s
  |> Json.parse
  |> Result.bind (Json.string "a.b")
  |> requal <| Ok s

[<Fact>]
let ``Test date time for keypath`` () =
  let dt = DateTime.Now
  let obj = JsonObject ()
  let kv = new KeyValuePair<string, JsonValue>("key", JsonPrimitive(dt))
  obj.Add(kv)
  obj
  |> Json.dateTime "key"
  |> requal <| Ok dt

[<Fact>]
let ``Test date time offset for keypath`` () =
  let dto = DateTimeOffset.Now
  let obj = JsonObject ()
  let kv = new KeyValuePair<string, JsonValue>("key", JsonPrimitive(dto))
  obj.Add(kv)
  obj
  |> Json.dateTimeOffset "key"
  |> requal <| Ok dto

[<Fact>]
let ``Test time span for keypath`` () =
  let ts = TimeSpan(1000000000000L)
  let obj = JsonObject ()
  let kv = new KeyValuePair<string, JsonValue>("key", JsonPrimitive(ts))
  obj.Add(kv)
  obj
  |> Json.timeSpan "key"
  |> requal <| Ok ts

[<Fact>]
let ``Test uri for keypath`` () =
  let uri = Uri("http://github.com")
  let obj = JsonObject ()
  let kv = new KeyValuePair<string, JsonValue>("key", JsonPrimitive(uri))
  obj.Add(kv)
  obj
  |> Json.uri "key"
  |> requal <| Ok uri

[<Fact>]
let ``Test guid for keypath`` () =
  let guid = Guid.NewGuid ()
  let obj = JsonObject ()
  let kv = new KeyValuePair<string, JsonValue>("key", JsonPrimitive(guid))
  obj.Add(kv)
  obj
  |> Json.guid "key"
  |> requal <| Ok guid
