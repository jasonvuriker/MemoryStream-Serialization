

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

//using var memorySteam = new MemoryStream();

//var text = "Hello world my friend";

//var buffer = Encoding.UTF8.GetBytes(text);

//memorySteam.Write(buffer);

//memorySteam.Write(Encoding.UTF8.GetBytes("\n new line"));


//// read
//memorySteam.Position = 0;
//var bytes = new byte[memorySteam.Length];
//memorySteam.Read(bytes);

//var str = Encoding.UTF8.GetString(bytes);

//Console.WriteLine(str);

//using var fileStream = new FileStream("console.txt", FileMode.OpenOrCreate,FileAccess.ReadWrite);

//memorySteam.Position = 0;
//memorySteam.CopyTo(fileStream);


//var drive = new DriveInfo("D:\\");

//Console.WriteLine($"Drive name: {drive.Name}");
//Console.WriteLine($"Drive free size: {drive.AvailableFreeSpace}");
//Console.WriteLine($"Drive size: {drive.TotalSize}");
//Console.WriteLine($"Drive type: {drive.DriveType}");
//Console.WriteLine($"Drive volume label: {drive.VolumeLabel}");

var todoAction = new TodoAction()
{
    ActivityName = "Get up",
    DateTime = DateTime.Parse("06:00"),
    Status = Status.Done
};


var json = JsonSerializer.Serialize(todoAction);

//Console.WriteLine(json);

var activitiesJson = @"[
  {
    ""activity_name"": ""Have lunch"",
    ""date_time"": ""2024-01-01T06:00"",
    ""status"": 1,
    ""place"":{
            ""name"":""pdp""
    }
  },
  {
    ""activity_name"": ""Do homework"",
    ""date_time"": ""2024-01-01T09:00"",
    ""status"": 2,
""place"":{
        ""name"":""pdp1""
}
  }
]
";

var jsonOptions = new JsonSerializerOptions()
{
    AllowTrailingCommas = false,
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    WriteIndented = true,
    Converters = { new JsonStringEnumConverter() },
    ReferenceHandler = ReferenceHandler.Preserve
};

List<TodoAction> todoActions =
[
    new()
    {
        ActivityName = "Laundry",
        DateTime = DateTime.Now,
        Status = Status.Done,
        Place = new Place()
        {
            Name = "PDP",
        }
    },
    new()
    {
        ActivityName = "Ironing",
        DateTime = DateTime.Now,
        Status = Status.InProgress,
        Place = new Place()
        {
            Name = "PDP",
        }
    }
];



//var activities = JsonSerializer
//    .Deserialize<List<TodoAction>>(activitiesJson, jsonOptions);

var text = JsonSerializer.Serialize(todoActions,jsonOptions);

Console.WriteLine(text);


using var memoryStream = new MemoryStream();

JsonSerializer.Serialize(memoryStream, todoActions, jsonOptions);

using var fileStream = new FileStream("todo.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);

memoryStream.Position = 0;
memoryStream.CopyTo(fileStream);


public class TodoAction
{
    public string ActivityName { get; set; }

    public DateTime DateTime { get; set; }

    public Status Status { get; set; }

    public Place Place { get; set; }
}

public class Place
{
    public string Name { get; set; }

    public TodoAction TodoAction { get; set; }
}

public enum Status : int
{
    Done = 1,
    InProgress = 2
}