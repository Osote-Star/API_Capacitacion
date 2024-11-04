using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.DTOs.Tarea;
using WEBAPI.Models;

namespace WEBAPI.Data.Interfaces
{
    public interface ITareaService
    {
        public Task<TareaModel?> Create(CreateTareaDto createTareaDTO);
        public Task<UserModel?> FindAll(int userID);
        public Task<TareaModel?> Update(int idtask, UpdateTareaDto updateTareaDto);
        public Task<TareaModel?> Remove(int taskID);
        public Task<TareaModel?> Togglestatus(int taskID);
    }
}
