﻿using Tasklist.Adapters.DataAccess;
using Tasklist.Domain;
using Tasklist.Ports.Commands;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.timeoutpolicy.Attributes;

namespace Tasklist.Ports.Handlers
{
    public class AddTaskCommandHandler : RequestHandler<AddTaskCommand>
    {
        private readonly ITasksDAO tasksDAO;

        public AddTaskCommandHandler(ITasksDAO tasksDAO)
        {
            this.tasksDAO = tasksDAO;
        }

        [Trace(step:1, timing: HandlerTiming.Before)]
        [Validation(step: 2, timing: HandlerTiming.Before)]
        [TimeoutPolicy(step: 3, milliseconds: 300)]
        public override AddTaskCommand Handle(AddTaskCommand addTaskCommand)
        {
            var inserted = tasksDAO.Add(
                new Task(
                    taskName: addTaskCommand.TaskName, 
                    taskDecription: addTaskCommand.TaskDecription,
                    dueDate: addTaskCommand.TaskDueDate
                    )
                );

            addTaskCommand.TaskId = inserted.Id;

            return addTaskCommand;
        }
    }
}