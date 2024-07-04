using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoManager.Common.Models;
using ToDoManager.Persistence.EfCore;

namespace ToDoManager.CQRS;

public sealed record SaveSingleToDoCommand(ToDo obj) : IRequest<Result<ToDo>>;

public sealed class SaveSingleToDoCommandHandler : IRequestHandler<SaveSingleToDoCommand, Result<ToDo>>
{
    private ApplicationDbContext context;

    public SaveSingleToDoCommandHandler(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Result<ToDo>> Handle(SaveSingleToDoCommand command, CancellationToken cancellationToken)
    {
        if (command.obj == null)
            return Result.Fail("Object is null!");
        
        if (command.obj.ID == Guid.Empty) 
        {
            context.ToDos.Add(command.obj);
            await context.SaveChangesAsync();
            context.Entry(command.obj).State = EntityState.Detached;
            return Result.Ok(command.obj);
        }
        else
        {
            context.ToDos.Update(command.obj);
            await context.SaveChangesAsync();
            context.Entry(command.obj).State = EntityState.Detached;
            return Result.Ok(command.obj);
        }
    }
}