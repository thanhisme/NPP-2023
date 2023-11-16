﻿using DomainService.Interfaces.NPP;
using Microsoft.AspNetCore.Mvc;
using Model.RequestModel;

namespace ABCAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IHttpContextAccessor httpContextAccessor, IDepartmentService departmentService) : base(httpContextAccessor)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult GetMany([FromQuery] PaginationRequest req)
        {
            var (totalRecords, users) = _departmentService.GetMany(req.Page, req.PageSize);

            return Ok(users, totalRecords);
        }
    }
}