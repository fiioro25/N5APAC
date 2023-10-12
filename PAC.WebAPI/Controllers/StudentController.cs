﻿using System;
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
        public IEnumerable<Student> Get()
        {
            return _studentLogic.GetStudents();
        }

        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _studentLogic.GetStudentById(id);
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
