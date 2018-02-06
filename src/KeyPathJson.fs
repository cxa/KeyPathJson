namespace KeyPathJson

module Json =
  open System
  open System.Json
  open FSharp.Core

  let load stream =
    try Ok <| JsonValue.Load(stream=stream)
    with exn -> Error exn

  let parse jsonString =
    try Ok <| JsonValue.Parse jsonString
    with exn -> Error exn

  let private getType (jval: JsonValue) = jval.JsonType

  let private valueFor' key jsonValue =
    match getType jsonValue with
    | JsonType.Object ->
      try Ok <| jsonValue.Item (key=key)
      with exn -> Error exn
    | JsonType.Array ->
      try
        let idx = int (key)
        Ok <| jsonValue.Item (index=idx)
      with
        | :? FormatException ->
          Error <| failwithf "Array access requires key %A must be an int" key
        | e -> Error e
    | t ->
      Error
      <| failwithf
         "Expect an Object or Array to access key %A, but actual a %A"
         key t

  let rec private loop keys result =
    match keys, result with
    | [], _
    | _, Error _ ->
      result
    | [k], Ok jval ->
      valueFor' k jval
    | k::rest, Ok jval ->
      loop rest <| valueFor' k jval

  let valueForKeyPath (keyPath:string) jsonValue =
    let keys = keyPath.Split '.' |> Array.toList
    loop keys <| Ok jsonValue

  let private valueForKeyPath' keyPath jsonValue expectType converter =
    valueForKeyPath keyPath jsonValue
    |> Result.bind (fun jval ->
      match getType jval with
      | t when t = expectType ->
        try
          Ok <| converter jval
        with exn ->
          let err =
            failwithf "Value for %A fail to convert: %A" keyPath exn.Message
          Error err
      | _ ->
        Error <| failwithf "Mismatch type for key %A" keyPath
    )

  let boolForKeyPath keyPath jsonValue =
    let converter: JsonValue -> bool = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Boolean converter

  let byteForKeyPath keyPath jsonValue =
    let converter: JsonValue -> byte = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let sbyteForKeyPath keyPath jsonValue =
    let converter: JsonValue -> sbyte = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let int8ForKeyPath keyPath jsonValue =
    let converter: JsonValue -> int8 = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let uint8ForKeyPath keyPath jsonValue =
    let converter: JsonValue -> uint8 = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let int16ForKeyPath keyPath jsonValue =
    let converter: JsonValue -> int16 = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let uint16ForKeyPath keyPath jsonValue =
    let converter: JsonValue -> uint16 = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let int32ForKeyPath keyPath jsonValue =
    let converter: JsonValue -> int = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let uint32ForKeyPath keyPath jsonValue =
    let converter: JsonValue -> uint32 = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let int64ForKeyPath keyPath jsonValue =
    let converter: JsonValue -> int64 = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let uint64ForKeyPath keyPath jsonValue =
    let converter: JsonValue -> uint64 = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let decimalForKeyPath keyPath jsonValue =
    let converter: JsonValue -> decimal = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let doubleForKeyPath keyPath jsonValue =
    let converter: JsonValue -> double = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let float32ForKeyPath keyPath jsonValue =
    let converter: JsonValue -> float32 = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let charForKeyPath keyPath jsonValue =
    let converter: JsonValue -> char = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.String converter

  let stringForKeyPath keyPath jsonValue =
    let converter: JsonValue -> string = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.String converter

  let dateTimeForKeyPath keyPath jsonValue =
    let converter: JsonValue -> DateTime = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.String converter

  let dateTimeOffsetForKeyPath keyPath jsonValue =
    let converter: JsonValue -> DateTimeOffset = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let guidForKeyPath keyPath jsonValue =
    let converter: JsonValue -> Guid = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter

  let timeSpanOffsetForKeyPath keyPath jsonValue =
    let converter: JsonValue -> TimeSpan = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.String converter

  let uriForKeyPath keyPath jsonValue =
    let converter: JsonValue -> Uri = JsonValue.op_Implicit
    valueForKeyPath' keyPath jsonValue JsonType.Number converter
