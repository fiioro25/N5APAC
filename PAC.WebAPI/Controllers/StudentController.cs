using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PAC.Domain;
using PAC.IBusinessLogic;
using PAC.WebAPI.Filters;

namespace PAC.WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentLogic _studentLogic;

        public StudentController(IStudentLogic studentLogic)
        {
            this._studentLogic = studentLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var students = _studentLogic.GetStudents();
                if (students.Any())
                {
                    return Ok(students);
                }
                else
                {
                    return NotFound("No se encontraron estudiantes.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener la lista de estudiantes: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El identificador del estudiante debe ser mayor que cero.");
            }

            try
            {
                var student = _studentLogic.GetStudentById(id);
                if (student != null)
                {
                    return Ok(student);
                }
                else
                {
                    return NotFound($"No se encontró un estudiante con el ID {id}.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el estudiante: {ex.Message}");
            }
        }

        [AuthorizationFilter]
        [HttpPost]
        public IActionResult Post([FromBody] Student value)
        {
            if (value == null)
            {
                return BadRequest("El objeto Student no puede ser nulo.");
            }

            try
            {
                _studentLogic.InsertStudents(value);
                return Ok("Estudiante insertado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al insertar el estudiante: {ex.Message}");
            }
        }

    }
}
