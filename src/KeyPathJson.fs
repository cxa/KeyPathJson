namespace KeyPathJson

module Json =
  open System
  open System.Json

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

  let value (keyPath:string) jsonValue =
    let keys = keyPath.Split '.' |> Array.toList
    loop keys <| Ok jsonValue

  let private value' keyPath jsonValue expectType converter =
    value keyPath jsonValue
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

  let bool keyPath jsonValue =
    let converter: JsonValue -> bool = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Boolean converter

  let byte keyPath jsonValue =
    let converter: JsonValue -> byte = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let sbyte keyPath jsonValue =
    let converter: JsonValue -> sbyte = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let int8 keyPath jsonValue =
    let converter: JsonValue -> int8 = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let uint8 keyPath jsonValue =
    let converter: JsonValue -> uint8 = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let int16 keyPath jsonValue =
    let converter: JsonValue -> int16 = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let uint16 keyPath jsonValue =
    let converter: JsonValue -> uint16 = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let int32 keyPath jsonValue =
    let converter: JsonValue -> int = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let uint32 keyPath jsonValue =
    let converter: JsonValue -> uint32 = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let int64 keyPath jsonValue =
    let converter: JsonValue -> int64 = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let uint64 keyPath jsonValue =
    let converter: JsonValue -> uint64 = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let decimal keyPath jsonValue =
    let converter: JsonValue -> decimal = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let double keyPath jsonValue =
    let converter: JsonValue -> double = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let float32 keyPath jsonValue =
    let converter: JsonValue -> float32 = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let char keyPath jsonValue =
    let converter: JsonValue -> char = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.String converter

  let string keyPath jsonValue =
    let converter: JsonValue -> string = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.String converter

  let dateTime keyPath jsonValue =
    let converter: JsonValue -> DateTime = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.String converter

  let dateTimeOffset keyPath jsonValue =
    let converter: JsonValue -> DateTimeOffset = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let guid keyPath jsonValue =
    let converter: JsonValue -> Guid = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let timeSpan keyPath jsonValue =
    let converter: JsonValue -> TimeSpan = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter

  let uri keyPath jsonValue =
    let converter: JsonValue -> Uri = JsonValue.op_Implicit
    value' keyPath jsonValue JsonType.Number converter
