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

        public TareaService(PostgressqlConfiguration connection) => _connection = connection;
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

        #region FindAll

        public async Task<UserModel?> FindAll(int userID)
        {
            string sqlQuery = "Select * From view_usuario where idusuario = @ID";

            using NpgsqlConnection database = CreateConnection();

            Dictionary<int, List<TareaModel>> userTasks = [];

            try
            {
                await database.OpenAsync();

                IEnumerable<UserModel> result = await database.QueryAsync<UserModel, TareaModel, UserModel>(
                  sql: sqlQuery,
                  param: new
                  {
                      ID = userID
                  },
                  map: (user, tarea) => {
                      List<TareaModel> currentTasks = [];
                      userTasks.TryGetValue(user.IdUsuario, out currentTasks);

                      currentTasks ??= [];

                      if (currentTasks.Count == 0 && tarea != null)
                      {
                          currentTasks = [tarea];
                      }
                      else
                      {
                          if (currentTasks.Count > 0 && tarea != null)
                              currentTasks.Add(tarea);
                      }

                      userTasks[user.IdUsuario] = currentTasks;

                      return user;
                  },
                  splitOn: "idTarea"
                );

                await database.CloseAsync();


                IEnumerable<UserModel?> user = result.Distinct().ToList().Select(user => {
                    user.Tareas = userTasks[user.IdUsuario];
                    return user;
                });
                return user.FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Update
        public async Task<TareaModel?> Update(int idtask, UpdateTareaDto updateTareaDto)
        {
            using NpgsqlConnection database = CreateConnection();
            string sqlQuery = "Select * from fun_task_update (" +
                " p_idTarea := @ID," +
                " p_tarea := @task, " +
                " p_descripcion := @description, " +
                ")";
            try
            {
                await database.OpenAsync();

                var result = await database.QueryAsync<TareaModel>(
                    sqlQuery,
                    param: new
                    {
                        ID = idtask,
                        Task = updateTareaDto.Tarea,
                        description = updateTareaDto.Descripcion
                    });
                await database.CloseAsync();
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Remove
        public async Task<TareaModel?> Remove(int taskID)
        {
            using NpgsqlConnection database = CreateConnection();
            string sqlQuery = "Select * from fun_task_remove (" +
                " p_idTarea := @IDtarea)";
            try
            {
                await database.OpenAsync();

                var result = await database.QueryAsync<TareaModel>(
                    sqlQuery,
                    param: new
                    {
                        IDtarea = taskID
                    });
                await database.CloseAsync();
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Togglestatus
        public async Task<TareaModel?> Togglestatus(int taskID)
        {
            NpgsqlConnection database = CreateConnection();
            string sqlQuery = "Select * from fun_task_togglestatus(p_idTarea := @idtarea)";
            try
            {
                TareaModel? result = await database.QueryFirstOrDefaultAsync<TareaModel>(
                    sqlQuery,
                    param: new
                    {
                        idtarea = taskID
                    }
                    );
                await database.CloseAsync();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}

