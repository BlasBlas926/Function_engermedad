using System;
using System.Collections.Generic;
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
        public async Task<ResultOperation<Dictionary<int, Tuple<string, string, bool>>>> GetObtenerDiccionario()
        {
            ResultOperation<Dictionary<int, Tuple<string, string, bool>>> resultOperation = new ResultOperation<Dictionary<int, Tuple<string, string, bool>>>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.obtener_todos_enfermedades", null);
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;

            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    Dictionary<int, Tuple<string, string, bool>> dictionary = new Dictionary<int, Tuple<string, string, bool>>();

                    foreach (System.Data.DataRow item in respuestaBD.Data.Tables[0].Rows)
                    {
                        int id = (int)item["id_enf_cronica"];
                        string nombre = item["nombre"].ToString();
                        string descripcion = item["descripcion"].ToString();
                        bool estado = (bool)item["estado"];

                        var value = new Tuple<string, string, bool>(nombre, descripcion, estado);
                        dictionary.Add(id, value);
                    }

                    resultOperation.Result = dictionary;
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