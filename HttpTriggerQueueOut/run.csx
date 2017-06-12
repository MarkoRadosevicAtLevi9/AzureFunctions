#r "Newtonsoft.Json"
using System.Net;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log, IAsyncCollector<string> outputQueueItem)
{
    log.Info("C# HTTP trigger function processed a request.");

    // parse query parameter
    string name = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    name = name ?? data?.name;

    if (name == null)
    {
        req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
    }


    var exampleClass = new ExampleClass
    {
        Name = name
    };

    await outputQueueItem.AddAsync(JsonConvert.SerializeObject(exampleClass));

    return req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
}

public class ExampleClass
{
    public string Name { get; set; }
}