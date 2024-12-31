using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivoFijoAPI.Util;
using Npgsql;
using TsaakAPI.Entities;

using ConnectionTools.DBTools;
using System.Security.Cryptography.X509Certificates;

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