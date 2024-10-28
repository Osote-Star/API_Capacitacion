using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.Data.Interfaces;
using WEBAPI.DTOs.Tarea;
using WEBAPI.Models;

namespace WEBAPI.Data.Services
{
    public class TareaService : ITareaService
    {
        private PostgressqlConfiguration _connection;

        public TareaService (PostgressqlConfiguration connection) => _connection = connection;
        private NpgsqlConnection CreateConnection() => new(_connection.Connection);

        #region Create
        public async Task<TareaModel?> Create(CreateTareaDto CreateTareaDto) {
            using NpgsqlConnection database = CreateConnection();
            string sqlQuery = "Select * from fun_task_create (" + 
                " p_tarea := @task, " + 
                " p_descripcion := @descripcion, " + 
                " p_idUsuario := @userId " + 
                ")";

            try
            {
                await database.OpenAsync();

                var result = await database.QueryAsync<TareaModel, UserModel, TareaModel>(
                    sqlQuery,
                    param: new
                    {
                        task = CreateTareaDto.Tarea,
                        descripcion = CreateTareaDto.Descripcion,
                        userId = CreateTareaDto.IdUsuario
                    },
                    map: (tarea, usuario) => {
                        tarea.Usuario = usuario;
                        return tarea;
                    },
                    splitOn: "UsuarioId"
                    );
                await database.CloseAsync();
                return result.FirstOrDefault();
            }
            catch (Exception ex) 
            {
                return null;
            }
        }
        #endregion
    }
}
