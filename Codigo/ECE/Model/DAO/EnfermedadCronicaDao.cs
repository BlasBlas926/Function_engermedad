using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ActivoFijoAPI.Util;
using ConnectionTools.DBTools;
using ECE.Entities;
using TsaakAPI.Entities;

namespace ECE.Model.DAO
{
    public class EnfermedadCronicaDao
    {
        private ISqlTools _sqlTools;

        public EnfermedadCronicaDao(string connectionString)
        {
            this._sqlTools = new SQLTools(connectionString);
        }
        public async Task<ResultOperation<VMCatalog>> GetByIdAsync(int id)
        {
            ResultOperation<VMCatalog> resultOperation = new ResultOperation<VMCatalog>();
            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.obtener_id_enfermedad", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cronica", NpgsqlTypes.NpgsqlDbType.Integer,id),
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
                        Id = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cronica"],
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
                resultOperation.AddErrorMessage($"Error al ejecutar la función admece.obtener_id_enfermedad_cronica. {respuestaBD.Detail}");
            }
            return resultOperation;
        }
        public async Task<ResultOperation<List<VMCatalog>>> GetAllAsync()
        {
            ResultOperation<List<VMCatalog>> resultOperation = new ResultOperation<List<VMCatalog>>();
            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.obtener_todos_enfermedades", null);
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    List<VMCatalog> aux = new List<VMCatalog>();
                    foreach (System.Data.DataRow item in respuestaBD.Data.Tables[0].Rows)
                    {
                        VMCatalog auxItem = new VMCatalog
                        {
                            Id = (int)item["id_enf_cronica"],
                            Nombre = item["nombre"].ToString(),
                            Descripcion = item["descripcion"].ToString(),
                            Estado = item["estado"] as bool?,
                        };
                        aux.Add(auxItem);
                    }
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
                resultOperation.AddErrorMessage($"Error al ejecutar la función admece.obtener_enfermedades_cronicas. {respuestaBD.Detail}");
            }
            return resultOperation;
        }
        public async Task<ResultOperation<DataTableView<VMCatalog>>> GetPaginacion(int page, int fetch)
        {
           ResultOperation<DataTableView<VMCatalog>> resultOperation = new ResultOperation<DataTableView<VMCatalog>>();

            // Crear los parámetros de la función PostgreSQL
            var parameters = new ConnectionTools.DBTools.ParameterPGsql[]
            {
                new ParameterPGsql("p_page", NpgsqlTypes.NpgsqlDbType.Integer, page),
                new ParameterPGsql("p_fetch", NpgsqlTypes.NpgsqlDbType.Integer, fetch)
            };

            // Llamar a la función con los parámetros correctos
            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.fn_get_all_page_fetch_cronica", parameters);

            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;

            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    List<VMCatalog> catalogos = respuestaBD.Data.Tables[0].AsEnumerable()
                        .Select(row => new VMCatalog
                        {
                            Id = (int)row["id_enf_cronica"],
                            Nombre = row["nombre"].ToString(),
                            Descripcion = row["descripcion"].ToString(),
                            Estado = (bool?)row["estado"]
                        }).ToList();

                    Pager pager = new Pager(page, fetch, respuestaBD.Data.Tables[0].Rows.Count);

                    DataTableView<VMCatalog> dataTableView = new DataTableView<VMCatalog>(pager, catalogos);

                    resultOperation.Result = dataTableView;
                }
                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage("No se encontraron registros en la tabla.");
                }
            }
            else
            {
                Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
                throw new Exception(respuestaBD.Mensaje);
            }

            return resultOperation;
        }
        ///EnfermedadCronica
        public async Task<ResultOperation<List<EnfermedadCronica>>> GetCatalogoCronica()
        {
            ResultOperation<List<EnfermedadCronica>> resultOperation = new ResultOperation<List<EnfermedadCronica>>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.obtener_todos_enfermedades", null);
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;

            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    List<EnfermedadCronica> lista = new List<EnfermedadCronica>();

                    foreach (System.Data.DataRow item in respuestaBD.Data.Tables[0].Rows)
                    {
                        EnfermedadCronica aux = new EnfermedadCronica
                        {
                            id_enf_cronica = (int)item["id_enf_cronica"],
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

        //Diccionario
        public async Task<ResultOperation<Dictionary<int, string>>> GetObtenerDiccionario()
        {
            // Crear una lista de diccionarios
            Dictionary<int, string> diccionario = new Dictionary<int, string>();

            ResultOperation<Dictionary<int, string>> resultOperation = new ResultOperation<Dictionary<int, string>>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.obtener_todos_enfermedades");
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;

            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < respuestaBD.Data.Tables[0].Rows.Count; i++)
                    {
                        var id = (int)respuestaBD.Data.Tables[0].Rows[i]["id_enf_cronica"];
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


        public async Task<ResultOperation<int>> InsertAsync(EnfermedadCronica enfermedadCronica)
        {
            ResultOperation<int> resultOperation = new ResultOperation<int>();
            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.agregar_enfermedad", new ParameterPGsql[]{
                    new ParameterPGsql("p_nombre", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedadCronica.nombre),
                    new ParameterPGsql("p_descripcion", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedadCronica.descripcion),
                    new ParameterPGsql("p_fecha_registro", NpgsqlTypes.NpgsqlDbType.Date,enfermedadCronica.fecha_registro2),
                    new ParameterPGsql("p_fecha_inicio", NpgsqlTypes.NpgsqlDbType.Date,enfermedadCronica.fecha_inicio2),
                      new ParameterPGsql("p_estado", NpgsqlTypes.NpgsqlDbType.Boolean,enfermedadCronica.estado),
                      new ParameterPGsql("p_fecha_actualizacion", NpgsqlTypes.NpgsqlDbType.Date,enfermedadCronica.fecha_actualizacion2),
                });
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    resultOperation.Result = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cronica"];
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
        public async Task<ResultOperation<int>> UpdateAsync(EnfermedadCronica enfermedadCronica)
        {
            ResultOperation<int> resultOperation = new ResultOperation<int>();
            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.actualizar_enfermedad", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cronica", NpgsqlTypes.NpgsqlDbType.Integer,enfermedadCronica.id_enf_cronica),
                    new ParameterPGsql("p_nombre", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedadCronica.nombre),
                    new ParameterPGsql("p_descripcion", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedadCronica.descripcion),
                    new ParameterPGsql("p_estado", NpgsqlTypes.NpgsqlDbType.Boolean,enfermedadCronica.estado),
                    new ParameterPGsql("p_fecha_actualizacion", NpgsqlTypes.NpgsqlDbType.Date,enfermedadCronica.fecha_actualizacion2),

                });
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    resultOperation.Result = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cronica"];
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
                resultOperation.AddErrorMessage($"Error al ejecutar la función admece.actualizar_enfermedad_cronica. {respuestaBD.Detail}");
            }
            return resultOperation;
        }

        public async Task<ResultOperation<VMCatalog>> DeleteAsync(int id)
        {
            ResultOperation<VMCatalog> resultOperation = new ResultOperation<VMCatalog>();
            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.borrar_enfermedad", new ParameterPGsql[]{
          new ParameterPGsql("p_id_enf_cronica", NpgsqlTypes.NpgsqlDbType.Integer,id),
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
                        Id = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cronica"],
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