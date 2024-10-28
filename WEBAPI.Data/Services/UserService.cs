﻿using Dapper;
using Npgsql;
using System.Collections;
using WEBAPI.Data.Interfaces;
using WEBAPI.DTOs.User;
using WEBAPI.Models;

namespace WEBAPI.Data.Services
{
    public class UserService : IUserService
    {
        private PostgressqlConfiguration _postgresConfig;
        public UserService(PostgressqlConfiguration postgresConfig) => _postgresConfig = postgresConfig;

        public NpgsqlConnection CreateConnection() => new NpgsqlConnection(_postgresConfig.Connection);

        #region Create
        public Task<UserModel?> Create(CreateUserDto CreateUserDto)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region FindAll
        //tipos genericos <>
        public async Task<IEnumerable<UserModel>> FindAll()
        {
            string sqlQuery = "Select * From view_usuario";

            using NpgsqlConnection database = CreateConnection();

            Dictionary<int, List<TareaModel>> userTasks = []; // O(1)
                                                              // 10 ==> [...] 
                                                              // [...] ===> 7
            try
            {
                await database.OpenAsync(); // ???

                IEnumerable<UserModel> result = await database.QueryAsync<UserModel, TareaModel, UserModel>(
                  sql: sqlQuery,
                  map: (user, tarea) => { // O(4n)
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
                              currentTasks.Add(tarea); // O(n)
                      }

                      userTasks[user.IdUsuario] = currentTasks; // O(n)

                      return user; // O(n)
                  },
                  splitOn: "idTarea"
                );

                await database.CloseAsync();

                //                                       O(n),     O(n),    O(n)
                IEnumerable<UserModel> users = result.Distinct().ToList().Select(user => {
                    user.Tareas = userTasks[user.IdUsuario]; // O(n)
                    return user; // O(n)
                });
                return users; 
            }
            catch (Exception e) {
                //[] esto representa una lista
                return[];
            }
        }
        #endregion

        #region FIndOne
        public async Task<UserModel?> FindOne(int userID)
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

        #region Remove
        public Task<UserModel?> Remove(int userId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public Task<UserModel?> Update(UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}