using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet]
        public async Task<IActionResult> GetEnfermedadCronica()
        {
            var result = await _enfermedadCronicaDao.GetAllAsync();
            if (result.Success)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest(new { message = result.Messages });
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
                return Ok(result.Result);
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
                return Ok(result.Result);
            }
            else
            {
                // Si no fue exitosa, devuelve un error con el detalle
                return BadRequest(new { message = result.Messages });
            }
        }
        [HttpPatch("id")]
        public async Task<IActionResult> UpdateEnfermedad(EnfermedadCronica enfermedad)
        {
            // Llamada al DAO para actualizar el registro
            var result = await _enfermedadCronicaDao.UpdateAsync(enfermedad);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnfermedad(int id)
        {
            // Llamada al DAO para eliminar el registro
            var result = await _enfermedadCronicaDao.DeleteAsync(id);
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
    }
}