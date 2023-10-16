using PatternsSample.Patterns.Builder;

var workerBuilder = new WorkerBuilder()
    .WithName("Roman")
    .WithWage(100)
    .WithOfficeId(100)
    .WithSecondsName("Oq");

var worker = workerBuilder.Build();

Console.WriteLine(worker.Name);