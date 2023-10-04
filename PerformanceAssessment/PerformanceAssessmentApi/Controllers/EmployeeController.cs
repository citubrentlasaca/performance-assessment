﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PerformanceAssessmentApi.Dtos;
using PerformanceAssessmentApi.Services;

namespace PerformanceAssessmentApi.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new employee
        /// </summary>
        /// <param name="employee">Employee details</param>
        /// <returns>Returns the newly created employee</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/employees
        ///     {
        ///         "userId": 1,
        ///         "teamId": 1,
        ///         "status": "Active"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a new employee</response>
        /// <response code="400">Employee details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateEmployee")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmployeeCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreationDto employee)
        {
            try
            {
                // Create a new employee
                var newEmployee = await _employeeService.CreateEmployee(employee);

                return CreatedAtRoute("GetEmployeeById", new { id = newEmployee.Id }, newEmployee);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <returns>Returns all employees</returns>
        /// <response code="200">Employees found</response>
        /// <response code="204">No employees found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllEmployees")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployees();

                if (employees.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(employees);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Returns the details of the employee with id <paramref name="id"/></returns>
        /// <response code="200">Employee found</response>
        /// <response code="404">Employee not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetEmployeeById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                // Check if employee exists
                var foundEmployee = await _employeeService.GetEmployeeById(id);

                if (foundEmployee == null)
                {
                    return StatusCode(404, "Employee not found");
                }

                return Ok(foundEmployee);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the employee by user id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Returns the details of the employee with user id <paramref name="userId"/></returns>
        /// <response code="200">Employee found</response>
        /// <response code="404">Employee not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("users/{userId}", Name = "GetEmployeeByUserId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeByUserId(int userId)
        {
            try
            {
                // Check if employee exists
                var foundEmployee = await _employeeService.GetEmployeeByUserId(userId);

                if (foundEmployee == null)
                {
                    return StatusCode(404, "Employee not found");
                }

                return Ok(foundEmployee);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the employee by team id
        /// </summary>
        /// <param name="teamId">Team id</param>
        /// <returns>Returns the details of the employee with team id <paramref name="teamId"/></returns>
        /// <response code="200">Employee found</response>
        /// <response code="404">Employee not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("teams/{teamId}", Name = "GetEmployeeByTeamId")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeByTeamId(int teamId)
        {
            try
            {
                // Check if employee exists
                var foundEmployee = await _employeeService.GetEmployeeByTeamId(teamId);

                if (foundEmployee == null)
                {
                    return StatusCode(404, "Employee not found");
                }

                return Ok(foundEmployee);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing employee
        /// </summary>
        /// <param name="id">The id of the employee that will be updated</param>
        /// <param name="employee">New employee details</param>
        /// <returns>Returns the details of the employee with id <paramref name="id"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/employees
        ///     {
        ///         "userId": 2,
        ///         "teamId": 2,
        ///         "status": "Inactive"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated an employee</response>
        /// <response code="404">Employee not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateEmployee")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamUpdationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdationDto employee)
        {
            try
            {
                // Check if employee exists
                var foundEmployee = await _employeeService.GetEmployeeById(id);

                if (foundEmployee == null)
                {
                    return StatusCode(404, "Employee not found");
                }

                // Update the employee
                await _employeeService.UpdateEmployee(id, employee);
                return Ok("Employee updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an existing employee
        /// </summary>
        /// <param name="id">The id of the employee that will be deleted</param>
        /// <response code="200">Successfully deleted the employee</response>
        /// <response code="404">Employee not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteEmployee")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                // Check if employee exists
                var foundEmployee = await _employeeService.GetEmployeeById(id);

                if (foundEmployee == null)
                {
                    return StatusCode(404, "Employee not found");
                }

                await _employeeService.DeleteEmployee(id);
                return Ok("Employee deleted successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all details of an employee
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Returns the details of employee with id <paramref name="id"/></returns>
        /// <response code="200">Employee found</response>
        /// <response code="404">Employee not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/details", Name = "GetEmployeeDetailsById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeDetailsById(int id)
        {
            try
            {
                var employeeDetails = await _employeeService.GetEmployeeDetailsById(id);

                if (employeeDetails == null)
                {
                    return StatusCode(404, "Employee not found");
                }

                return Ok(employeeDetails);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}