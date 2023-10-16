namespace PatternsSample.Patterns.Builder;

public class Worker
{
    public string? Name { get; set; }
    public string? SecondName { get; set; }
    public ulong Office { get; set; }
    public decimal Wage { get; set; }
}

public class WorkerBuilder
{
    private readonly Worker _worker = new();

    public Worker Build()
    {
        if (_worker.Wage <= 0)
            throw new InvalidDataException("Wage can't be zero or less");
        
        if (string.IsNullOrEmpty(_worker.Name))
            throw new InvalidDataException("Worker must have a name");

        if (_worker.Office == 0)
            _worker.Office = 1;
        
        return _worker;
    }
    
    public WorkerBuilder WithName(string name)
    {
        _worker.Name = name;
        return this;
    }
    
    public WorkerBuilder WithSecondsName(string secondName)
    {
        _worker.SecondName = secondName;
        return this;
    }
    
    public WorkerBuilder WithOfficeId(ulong id)
    {
        _worker.Office = id;
        return this;
    }
    
    public WorkerBuilder WithWage(decimal wage)
    {
        _worker.Wage = wage;
        return this;
    }
}