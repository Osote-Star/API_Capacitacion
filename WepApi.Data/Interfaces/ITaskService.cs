using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTOs.Task;
using WebApi.Models;

namespace WebApi.Data.Interfaces
{
    public interface ITaskService
    {
        public Task<TaskModel?> Create(CreateTaskDTO createTaskDTO);
    }
}
