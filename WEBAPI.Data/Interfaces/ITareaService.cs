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
    }
}
