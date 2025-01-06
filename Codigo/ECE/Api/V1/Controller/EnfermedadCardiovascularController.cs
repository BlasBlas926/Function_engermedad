using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivoFijoAPI.Util;
using ECE.DTO;
using ECE.Util;
using Microsoft.AspNetCore.Mvc;
using TsaakAPI.Entities;
using TsaakAPI.Model.DAO;

namespace TsaakAPI.Api.V1.Controller
{
    [ApiController]
    [Route("tsaak/api/v1/[controller]")]
    public class EnfermedadCardiovascularController : ControllerBase
    {
        private readonly EnfermedadCardiovascularDao _enfermedadCardiovascularDao;
        private readonly IConfiguration _configuration;

        public EnfermedadCardiovascularController(EnfermedadCardiovascularDao enfermedadCardiovascularDao, IConfiguration configuration)
        {
            _enfermedadCardiovascularDao = enfermedadCardiovascularDao;
            _configuration = configuration;

        }

        // [HttpGet]
        // public async Task<IActionResult> GetEnfermedadCardiovascular([FromQuery] PaginacionDTO paginacion)
        // {
        //     try
        //     {
        //         var result = await _enfermedadCardiovascularDao.GetAllAsync();

        //         if (result.Success && result.Result != null)
        //         {
        //             var queryable = result.Result.AsQueryable();
        //             await HttpContext.InsertPaginationHeader(queryable);

        //             return Ok(queryable.Paginate(paginacion));

        //         }

        //         return BadRequest(new { message = result.Messages });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { message = "Ocurrió un error inesperado.", details = ex.Message });
        //     }
        // }

        [HttpGet("ObtenerTodos")]
        public async Task<IActionResult> GetEnfermedades()
        {
            try
            {
                var result = await _enfermedadCardiovascularDao.GetAllAsync();

                if (result.Success && result.Result != null)
                {
                    return Ok(result.Result);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", details = ex.Message });
            }
        }

        [HttpGet("Paginacion")]
        public async Task<IActionResult> GetEnfermedadCardiovascular([FromQuery] PaginacionDTO paginacion)
        {
            try
            {
                var result = await _enfermedadCardiovascularDao.GetAllAsync();

                if (result.Success && result.Result != null)
                {
                    var totalItems = result.Result.Count();
                    var pagedItems = result.Result
                        .Skip((paginacion.Pagina - 1) * paginacion.RecordsPorPagina)
                        .Take(paginacion.RecordsPorPagina)
                        .ToList();

                    var pager = new ActivoFijoAPI.Util.Pager(
                        paginacion.Pagina,
                        paginacion.RecordsPorPagina,
                        totalItems
                    );

                    var response = new ActivoFijoAPI.Util.DataTableView<TsaakAPI.Entities.VMCatalog>(
                        pager,
                        pagedItems
                    );

                    return Ok(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error inesperado.",
                    details = ex.Message
                });
            }
        }

        ///Diccionario de datos 
        [HttpGet("diccionario")]
        public async Task<IActionResult> GetEnfermedadCardiovascular()
        {
            try
            {
                var result = await _enfermedadCardiovascularDao.GetObtener();
                if (result.Success && result.Result != null)
                {
                    return Ok(result.Result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", details = ex.Message });
            }
        }

        [HttpGet("ObtenerCatalogoCardiovascular")]
        public async Task<IActionResult> GetCatalogoCardiovascular()
        {
            try
            {
                var result = await _enfermedadCardiovascularDao.GetCatalogoCardiovasculares();
                if (result.Success && result.Result != null)
                {
                    return Ok(result.Result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", details = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Llamada al DAO para obtener el registro
            var result = await _enfermedadCardiovascularDao.GetByIdAsync(id);

            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result.Result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertEnfermedad(EnfermedadCardiovascular enfermedad)
        {
            // Llamada al DAO para insertar el registro
            var result = await _enfermedadCardiovascularDao.InsertAsync(enfermedad);

            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve un estado 200 OK
                return Ok();
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ActualizarEnfermedades(EnfermedadCardiovascular enfermedad)
        {
            var result = await _enfermedadCardiovascularDao.UpdateAsync(enfermedad);
            if (result.Success)
            {

                return Ok();
            }
            else
            {
                return BadRequest(new { message = result.Messages });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEnfermedades(int id)
        {
            var result = await _enfermedadCardiovascularDao.DeleteAsync(id);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }
    }
}