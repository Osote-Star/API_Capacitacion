using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data.Interfaces;
using WebApi.DTOs.Task;
using WebApi.Models;

namespace WebApi.Data.Servicios
{
    public class TaskService : ITaskService
    {
        private PostgresssqlConfiguration _connection;

        public TaskService(PostgresssqlConfiguration connection) => _connection = connection;

        private NpgsqlConnection CreateConnection() => new(_connection.Connection);

        #region Create
        public async Task<TaskModel> Create(CreateTaskDTO createTaskDTO)
        {
            using NpgsqlConnection database = CreateConnection();
            string sqlQuery = "Select * from fun_task_create" +
                "(" +
                        "p_tarea := @task," +
                        "p_descripcion := @description," +
                        "p_idUsuario := @userId" +
                ");";

            try
            {
                await database.OpenAsync();

                var result = await database.QueryAsync
                    <TaskModel, UserModel, TaskModel>
                    (
                        sqlQuery,
                        param: new {
                            task = createTaskDTO.Tarea,
                            description = createTaskDTO.Descripcion,
                            userId = createTaskDTO.IdUsuario

                        },
                        map: (task_, user) => {
                            task_.Usuario = user;

                            return task_;
                        },
                        splitOn: "usuarioId"
                    );
                await database.CloseAsync();
                return result.FirstOrDefault();
            }

            catch (Exception Ex)
            {
                return null;
            }
        }
    }
}

         #endregion

