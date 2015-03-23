open System
open Nancy

[<EntryPoint>]
let main argv = 
    let nancy = new Nancy.Hosting.Self.NancyHost(new Uri("http://localhost:" + "8100"))
    nancy.Start()
    while true do Console.ReadLine() |> ignore
    0
