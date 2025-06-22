using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagementService.Application.UsesCases;
using UserManagementService.Domain.entities;

namespace UserManagementService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController(
        CreateRoleUseCase createRoleUseCase,
        GetAllRolesUseCase getAllRolesUseCase
    ) : ControllerBase
    {
        private readonly CreateRoleUseCase _createRoleUseCase = createRoleUseCase;
        private readonly GetAllRolesUseCase _getAllRolesUseCase = getAllRolesUseCase;

        // POST: api/roles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
        {
            await _createRoleUseCase.ExecuteAsync(request.Name);
            return CreatedAtAction(nameof(GetAll), null);
        }

        // GET: api/roles
        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetAll()
        {
            var roles = await _getAllRolesUseCase.ExecuteAsync();
            return Ok(roles);
        }
    }

    public record CreateRoleRequest(string Name);
}
