using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivoFijoAPI.Util;
using Npgsql;
using TsaakAPI.Entities;

using ConnectionTools.DBTools;
using System.Security.Cryptography.X509Certificates;
using System.Data;

namespace TsaakAPI.Model.DAO
{
    public class EnfermedadCardiovascularDao
    {
        private ISqlTools _sqlTools;

        public EnfermedadCardiovascularDao(string connectionString)
        {
            this._sqlTools = new SQLTools(connectionString);

        }

        public async Task<ResultOperation<VMCatalog>> GetByIdAsync(int id)
        {
            ResultOperation<VMCatalog> resultOperation = new ResultOperation<VMCatalog>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.obtener_id_enfermedad_cardiovascular", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cardiovascular", NpgsqlTypes.NpgsqlDbType.Integer,id),
                });
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {

                    VMCatalog aux = new VMCatalog
                    {
                        Id = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cardiovascular"],
                        Nombre = respuestaBD.Data.Tables[0].Rows[0]["nombre"].ToString(),
                        Descripcion = respuestaBD.Data.Tables[0].Rows[0]["descripcion"].ToString(),
                        Estado = respuestaBD.Data.Tables[0].Rows[0]["estado"] as bool?,

                    };

                    resultOperation.Result = aux;
                }
                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
                }

            }
            else
            {
                //TODO Agregar error en el log             
                if (respuestaBD.ExisteError)
                    Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
                throw new Exception(respuestaBD.Mensaje);
            }
            return resultOperation;


        }
        public async Task<ResultOperation<List<VMCatalog>>> GetAllAsync()
        {
            ResultOperation<List<VMCatalog>> resultOperation = new ResultOperation<List<VMCatalog>>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.obtener_enfermedad_cardiovascular", null);
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    List<VMCatalog> lista = new List<VMCatalog>();
                    foreach (System.Data.DataRow item in respuestaBD.Data.Tables[0].Rows)
                    {
                        VMCatalog aux = new VMCatalog
                        {
                            Id = (int)item["id_enf_cardiovascular"],
                            Nombre = item["nombre"].ToString(),
                            Descripcion = item["descripcion"].ToString(),
                            Estado = item["estado"] as bool?,

                        };
                        lista.Add(aux);
                    }
                    resultOperation.Result = lista;
                }
                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
                }

            }
            else
            {
                //TODO Agregar error en el log             
                if (respuestaBD.ExisteError)
                    Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
                throw new Exception(respuestaBD.Mensaje);
            }
            return resultOperation;
        }

        public async Task<ResultOperation<DataTableView<VMCatalog>>> GetObtenerEnfermedad(int page, int fetch)
        {
            ResultOperation<DataTableView<VMCatalog>> resultOperation = new ResultOperation<DataTableView<VMCatalog>>();

            var ParameterPGsql = new ConnectionTools.DBTools.ParameterPGsql[]{
            new ParameterPGsql("p_pagina",NpgsqlTypes.NpgsqlDbType.Integer, page),
            new ParameterPGsql("p_records_por_pagina",NpgsqlTypes.NpgsqlDbType.Integer,fetch)
            };
            Task<RespuestaBD> respuestaBD = _sqlTools.ExecuteFunctionAsync("admece.obtener_enfermedades_cardiovasculares_con_paginacion");
            RespuestaBD respuesta = await respuestaBD;
            resultOperation.Success = !respuesta.ExisteError;
            if (!respuesta.ExisteError)
            {
                if (respuesta.Data.Tables.Count > 0 && respuesta.Data.Tables[0].Rows.Count > 0)
                {
                    List<VMCatalog> lista = respuesta.Data.Tables[0].AsEnumerable().
                    Select(row => new VMCatalog
                    {
                        Id = (int)row["id_enf_cardiovascular"],
                        Nombre = row["nombre"].ToString(),
                        Descripcion = row["descripcion"].ToString(),
                        Estado = (bool?)row["estado"]
                    }).ToList();

                    Pager pager = new Pager(page, fetch, respuesta.Data.Tables[0].Rows.Count);
                    DataTableView<VMCatalog> dataTableView = new DataTableView<VMCatalog>(pager, lista);
                    resultOperation.Result = dataTableView;
                }
                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuesta.Detail}");
                }

            }
            return resultOperation;

        }
        ///Lista de enfermedades cardiovasculares
        // public async Task<ResultOperation<List<VMCatalog>>> GetObtenerEnfermedad()
        // {
        //     ResultOperation<List<VMCatalog>> resultOperation = new ResultOperation<List<VMCatalog>>();

        //     Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.obtener_enfermedad_cardiovascular", null);
        //     RespuestaBD respuestaBD = await respuestaBDTask;
        //     resultOperation.Success = !respuestaBD.ExisteError;
        //     if (!respuestaBD.ExisteError)
        //     {
        //         if (respuestaBD.Data.Tables.Count > 0
        //          && respuestaBD.Data.Tables[0].Rows.Count > 0)
        //         {
        //             List<VMCatalog> lista = new List<VMCatalog>();
        //             foreach (System.Data.DataRow item in respuestaBD.Data.Tables[0].Rows)
        //             {
        //                 VMCatalog aux = new VMCatalog
        //                 {
        //                     Id = (int)item["id_enf_cardiovascular"],
        //                     Nombre = item["nombre"].ToString(),
        //                     Descripcion = item["descripcion"].ToString(),
        //                     Estado = item["estado"] as bool?,

        //                 };
        //                 lista.Add(aux);
        //             }
        //             resultOperation.Result = lista;
        //         }
        //         else
        //         {
        //             resultOperation.Result = null;
        //             resultOperation.Success = false;
        //             resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
        //         }

        //     }
        //     else
        //     {
        //         //TODO Agregar error en el log             
        //         if (respuestaBD.ExisteError)
        //             Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
        //         throw new Exception(respuestaBD.Mensaje);
        //     }
        //     return resultOperation;
        // }

        ///Obtener catalogo de enfermedades cardiovasculares
        public async Task<ResultOperation<List<EnfermedadCardiovascular>>> GetCatalogoCardiovasculares()
        {
            ResultOperation<List<EnfermedadCardiovascular>> resultOperation = new ResultOperation<List<EnfermedadCardiovascular>>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.obtener_enfermedad_cardiovascular", null);
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;

            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    List<EnfermedadCardiovascular> lista = new List<EnfermedadCardiovascular>();

                    foreach (System.Data.DataRow item in respuestaBD.Data.Tables[0].Rows)
                    {
                        EnfermedadCardiovascular aux = new EnfermedadCardiovascular
                        {
                            id_enf_cardiovascular = (int)item["id_enf_cardiovascular"],
                            nombre = item["nombre"].ToString(),
                            descripcion = item["descripcion"].ToString(),
                            estado = (bool)item["estado"],
                            fecha_registro2 = (DateTime)item["fecha_registro"],
                            fecha_inicio2 = (DateTime)item["fecha_inicio"],
                            fecha_actualizacion2 = (DateTime)item["fecha_actualizacion"],
                        };
                        lista.Add(aux);
                    }

                    resultOperation.Result = lista;
                }
                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
                }
            }
            else
            {
                if (respuestaBD.ExisteError)
                    Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
                throw new Exception(respuestaBD.Mensaje);
            }

            return resultOperation;
        }
         public async Task<ResultOperation<Dictionary<int, string>>> GetDiccionario()
        {
            // Crear una lista de diccionarios
            Dictionary<int, string> diccionario = new Dictionary<int, string>();

            ResultOperation<Dictionary<int, string>> resultOperation = new ResultOperation<Dictionary<int, string>>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.fn_get_all");
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;

            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < respuestaBD.Data.Tables[0].Rows.Count; i++)
                    {
                        var id = (int)respuestaBD.Data.Tables[0].Rows[i]["id_enf_cardiovascular"];
                        var nombre = respuestaBD.Data.Tables[0].Rows[i]["nombre"].ToString();

                        diccionario.Add(id, nombre);
                    }
                    resultOperation.Result = diccionario;
                }


                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No se encontraron registros en la tabla.");
                }
            }
            else
            {
                // Manejo de errores (log, excepciones, etc.)
                Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
                throw new Exception(respuestaBD.Mensaje);
            }

            return resultOperation;
        }
        ///Diccionario de enfermedades cardiovasculares

        // public async Task<ResultOperation<Dictionary<int, Tuple<string, string, bool>>>> GetObtener()
        // {
        //     ResultOperation<Dictionary<int, Tuple<string, string, bool>>> resultOperation = new ResultOperation<Dictionary<int, Tuple<string, string, bool>>>();

        //     Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.obtener_enfermedad_cardiovascular", null);
        //     RespuestaBD respuestaBD = await respuestaBDTask;
        //     resultOperation.Success = !respuestaBD.ExisteError;

        //     if (!respuestaBD.ExisteError)
        //     {
        //         if (respuestaBD.Data.Tables.Count > 0 && respuestaBD.Data.Tables[0].Rows.Count > 0)
        //         {
        //             Dictionary<int, Tuple<string, string, bool>> dictionary = new Dictionary<int, Tuple<string, string, bool>>();

        //             foreach (System.Data.DataRow item in respuestaBD.Data.Tables[0].Rows)
        //             {
        //                 int id = (int)item["id_enf_cardiovascular"];
        //                 string nombre = item["nombre"].ToString();
        //                 string descripcion = item["descripcion"].ToString();
        //                 bool estado = (bool)item["estado"];

        //                 var value = new Tuple<string, string, bool>(nombre, descripcion, estado);
        //                 dictionary.Add(id, value);
        //             }

        //             resultOperation.Result = dictionary;
        //         }
        //         else
        //         {
        //             resultOperation.Result = null;
        //             resultOperation.Success = false;
        //             resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
        //         }
        //     }
        //     else
        //     {
        //         if (respuestaBD.ExisteError)
        //             Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
        //         throw new Exception(respuestaBD.Mensaje);
        //     }

        //     return resultOperation;
        // }

        public async Task<ResultOperation<Dictionary<int, string>>> GetObtenerDiccionario()
        {
            // Crear una lista de diccionarios
            Dictionary<int, string> diccionario = new Dictionary<int, string>();

            ResultOperation<Dictionary<int, string>> resultOperation = new ResultOperation<Dictionary<int, string>>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.obtener_enfermedad_cardiovascular");
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;

            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < respuestaBD.Data.Tables[0].Rows.Count; i++)
                    {
                        var id = (int)respuestaBD.Data.Tables[0].Rows[i]["id_enf_cardiovascular"];
                        var nombre = respuestaBD.Data.Tables[0].Rows[i]["nombre"].ToString();

                        diccionario.Add(id, nombre);
                    }
                    resultOperation.Result = diccionario;
                }


                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No se encontraron registros en la tabla.");
                }
            }
            else
            {
                // Manejo de errores (log, excepciones, etc.)
                Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
                throw new Exception(respuestaBD.Mensaje);
            }

            return resultOperation;
        }


        //Falta el de agregar, actualizar
        public async Task<ResultOperation<int>> InsertAsync(EnfermedadCardiovascular enfermedad)
        {
            ResultOperation<int> resultOperation = new ResultOperation<int>();
            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.agregar_enfermedad_cardiovascular", new ParameterPGsql[]{
                    new ParameterPGsql("p_nombre", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedad.nombre),
                    new ParameterPGsql("p_descripcion", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedad.descripcion),
                    new ParameterPGsql("p_fecha_registro", NpgsqlTypes.NpgsqlDbType.Date,enfermedad.fecha_registro2),
                    new ParameterPGsql("p_fecha_inicio", NpgsqlTypes.NpgsqlDbType.Date,enfermedad.fecha_inicio2),
                      new ParameterPGsql("p_estado", NpgsqlTypes.NpgsqlDbType.Boolean,enfermedad.estado),
                      new ParameterPGsql("p_fecha_actualizacion", NpgsqlTypes.NpgsqlDbType.Date,enfermedad.fecha_actualizacion2),
                });
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    resultOperation.Result = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cardiovascular"];
                }
                else
                {
                    resultOperation.Result = 0;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
                }
            }
            else
            {
                resultOperation.AddErrorMessage($"Error al ejecutar la función admece.insertar_enfermedad_cardiovascular. {respuestaBD.Detail}");
            }
            return resultOperation;
        }


        public async Task<ResultOperation<int>> UpdateAsync(EnfermedadCardiovascular enfermedad)
        {
            ResultOperation<int> resultOperation = new ResultOperation<int>();
            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.actualizar_enfermedad_cardiovascular", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cardiovascular", NpgsqlTypes.NpgsqlDbType.Integer,enfermedad.id_enf_cardiovascular),
                    new ParameterPGsql("p_nombre", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedad.nombre),
                    new ParameterPGsql("p_descripcion", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedad.descripcion),
                    new ParameterPGsql("p_estado", NpgsqlTypes.NpgsqlDbType.Boolean,enfermedad.estado),
                    new ParameterPGsql("p_fecha_actualizacion", NpgsqlTypes.NpgsqlDbType.Date,enfermedad.fecha_actualizacion2),
                });
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    resultOperation.Result = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cardiovascular"];
                }
                else
                {
                    resultOperation.Result = 0;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
                }
            }
            else
            {
                resultOperation.AddErrorMessage($"Error al ejecutar la función admece.actualizar_enfermedad_cardiovascular. {respuestaBD.Detail}");
            }
            return resultOperation;
        }

        public async Task<ResultOperation<VMCatalog>> DeleteAsync(int id)
        {
            ResultOperation<VMCatalog> resultOperation = new ResultOperation<VMCatalog>();
            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.borrar_enfermedad_cardiovascular", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cardiovascular", NpgsqlTypes.NpgsqlDbType.Integer,id),
                });
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {

                    VMCatalog aux = new VMCatalog
                    {
                        Id = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cardiovascular"],
                        Nombre = respuestaBD.Data.Tables[0].Rows[0]["nombre"].ToString(),
                        Descripcion = respuestaBD.Data.Tables[0].Rows[0]["descripcion"].ToString(),
                        Estado = respuestaBD.Data.Tables[0].Rows[0]["estado"] as bool?,

                    };

                    resultOperation.Result = aux;
                }
                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
                }

            }
            else
            {
                //TODO Agregar error en el log             
                if (respuestaBD.ExisteError)
                    Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
                throw new Exception(respuestaBD.Mensaje);
            }
            return resultOperation;
        }
    }
}