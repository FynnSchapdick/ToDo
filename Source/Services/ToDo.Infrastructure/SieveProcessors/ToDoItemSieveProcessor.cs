using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using ToDo.Core.Domain;

namespace ToDo.Infrastructure.SieveProcessors;

public sealed class ToDoItemSieveProcessor : SieveProcessor
{
    public ToDoItemSieveProcessor(IOptions<SieveOptions> options) : base(options) { }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        mapper.Property<ToDoItem>(p => p.Text)
            .CanSort()
            .CanFilter();

        mapper.Property<ToDoItem>(p => p.Completed)
            .CanSort()
            .CanFilter();
        
        return base.MapProperties(mapper);
    }
}