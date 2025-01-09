using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ECE.DTO;
using ECE.Entities;
using ECE.Model.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECE.Api.V1.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EnfermedadCronicaController : ControllerBase
    {
        private readonly EnfermedadCronicaDao _enfermedadCronicaDao;
        private readonly IConfiguration _configuration;
        public EnfermedadCronicaController(EnfermedadCronicaDao enfermedadCronicaDao, IConfiguration configuration)
        {
            _enfermedadCronicaDao = enfermedadCronicaDao;
            _configuration = configuration;
        }
        [HttpGet("Paginacion")]
        public async Task<IActionResult> GetEnfermedadCronica([FromQuery] int page, [FromQuery] int fetch)
        {
            var result = await _enfermedadCronicaDao.GetPaginacion(page, fetch);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { message = result.Messages });
            }
        }


        [HttpGet("ObtenerEnfermedadCronica")]
        public async Task<IActionResult> GetEnfermedades()
        {
            try
            {
                var result = await _enfermedadCronicaDao.GetAllAsync();

                if (result.Success && result.Result != null)
                {
                    return Ok(result);
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
        [HttpGet("ObtenerEnfermedadCronicaCompleta")]
        public async Task<IActionResult> GetEnfermedadesCompleta()
        {
            try
            {
                var result = await _enfermedadCronicaDao.GetCatalogoCronica();

                if (result.Success && result.Result != null)
                {
                    return Ok(result);
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

        // [HttpGet("diccionario")]
        // public async Task<IActionResult> GetObtenerDiccionario()
        // {
        //     return Ok();
        // }
        [HttpGet("diccionario")]
        public async Task<IActionResult> GetObtenerDiccionarios()
        {
            try
            {
                var result = await _enfermedadCronicaDao.GetObtenerDiccionario();
                if (result.Success && result.Result != null)
                {
                    return Ok(result);
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
            var result = await _enfermedadCronicaDao.GetByIdAsync(id);
            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertEnfermedad(EnfermedadCronica enfermedad)
        {
            // Llamada al DAO para insertar el registro
            var result = await _enfermedadCronicaDao.InsertAsync(enfermedad);
            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return NoContent();
            }

        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEnfermedad(EnfermedadCronica enfermedad)
        {
            // Llamada al DAO para actualizar el registro
            var result = await _enfermedadCronicaDao.UpdateAsync(enfermedad);
            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnfermedad(int id)
        {
            // Llamada al DAO para eliminar el registro
            var result = await _enfermedadCronicaDao.DeleteAsync(id);
            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Si es exitosa, devuelve el resultado con un estado 200 OK
                return Ok(result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }
    }
}