module RestApiModule

open Nancy
open Nancy.ModelBinding

type ServiceInfo = {Name: string; Version : string; GitSha: string; ReleaseNotes: string} 

type ServiceInfoRequest = {Name: string; Environment: string}

type RestApiModule() as this =
    inherit NancyModule()
    do
        this.Get.["/serviceInfo/{serviceName}/{environment}/"] <- fun parameters ->
                    let serviceName = (parameters :?> Nancy.DynamicDictionary).["serviceName"]
                    let environment = (parameters :?> Nancy.DynamicDictionary).["environment"]
                    printfn "%O" serviceName
                    let response = {Name= "service1"; Version="version1"; GitSha="weasdfsd"; ReleaseNotes="notes"}
                    this.Response.AsJson(response) :> obj

