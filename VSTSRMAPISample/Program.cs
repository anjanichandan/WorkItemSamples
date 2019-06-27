using System;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Newtonsoft.Json;

namespace VSTSRMAPISample
{ 
    class Program
    {
        public static string vstsUrl = "http://localhost/DefaultCollection";
        public static string projectName = "MyAgile2";
        public static int releaseDefinitionId = 1;

        static void Main(string[] args)
        {
            Uri serverUrl = new Uri(vstsUrl);
            VssCredentials credentials = new VssClientCredentials();
            credentials.Storage = new VssClientCredentialStorage();

            VssConnection connection = new VssConnection(serverUrl, credentials);

            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            JsonPatchDocument patchDocument = new JsonPatchDocument();

            patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Tags",
                    Value = "tag1"
                }
            );

            patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Title",
                    Value = "Sample task  with tags"
                }
            );

            var newWorkItem = workItemTrackingClient.CreateWorkItemAsync(patchDocument, "MyAgile2", "Task").Result;

        }
    }
}
