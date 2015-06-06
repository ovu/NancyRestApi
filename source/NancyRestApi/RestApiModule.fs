module RestApiModule

open System
open Nancy
open Nancy.Bootstrapper
open Nancy.ModelBinding

type ServiceInfo = {Name: string; Version : string; GitSha: string; ReleaseNotes: string} 

type ServiceInfoRequest = {Name: string; Environment: string}

(* Enable Cors in the application startup *)
type AppStartup() =
    interface IApplicationStartup with 
            member this.Initialize pipelines = 
                    pipelines.AfterRequest.AddItemToEndOfPipeline( new Action<NancyContext>( fun ctx -> 
                                                                                          ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                                                                                           |> ignore
                                                                                 ));
let GetServiceInfo serviceName environment =
  match (serviceName, environment) with
    | ("service1", "Integration") -> { Name = "service1"; Version = "1.2.3"; GitSha = "TheGitSha"; ReleaseNotes = "notes"}
    | ("service2", "Integration") -> { Name = "service2"; Version = "4.5.6"; GitSha = "Service2GitSha"; ReleaseNotes = "NotesService2"}
    | ("service3", "Integration") -> { Name = "service3"; Version = "7.8.9"; GitSha = "Service3GitSha"; ReleaseNotes = "NotesService3"}
    | ("service1", "QA") -> { Name = "service1"; Version = "1.2.4"; GitSha = "TheGitSha"; ReleaseNotes = "notes"}
    | ("service2", "QA") -> { Name = "service2"; Version = "4.5.6"; GitSha = "Service2GitSha"; ReleaseNotes = "NotesService2"}
    | ("service3", "QA") -> { Name = "service3"; Version = "7.8.9"; GitSha = "Service3GitSha"; ReleaseNotes = "NotesService3"}

type RestApiModule() as this =
    inherit NancyModule()
    do
        this.Get.["/serviceInfo/{serviceName}/{environment}/"] <- fun parameters ->
                    let serviceName = (parameters :?> Nancy.DynamicDictionary).["serviceName"]
                    let environment = (parameters :?> Nancy.DynamicDictionary).["environment"]
                    printfn "%O" serviceName
                    let response = GetServiceInfo ( string serviceName ) ( string environment )
                    this.Response.AsJson(response) :> obj

