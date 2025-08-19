using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDT.Attribute;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Service;

namespace QLDT.Controllers
{
    [Route("api/certificate")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly CertificateSer _service;

        public CertificateController(CertificateSer service)
        {
            _service = service;
        }

        [Authorize]
        [HasPermission("Report.ViewProcess")]
        [HttpGet("me")]
        public async Task<IActionResult> GetAllByCurrentUser()
        {
            var data = await _service.GetAllByUserAsync();
            return Ok(ApiResponse<IEnumerable<CertificateRes>>.SuccessResponse(
                data,
                "Fetched cetificates successfully"
            ));
        }

        [Authorize]
        [HasPermission("Report.ViewDetail")]
        [HttpGet("employee/{id}")]
        public async Task<IActionResult> GetAllByEmployee(long id)
        {
            var data = await _service.GetAllByUserAsync(id);
            return Ok(ApiResponse<IEnumerable<CertificateRes>>.SuccessResponse(
                data,
                "Fetched cetificates successfully"
            ));
        }

        [Authorize]
        [HasPermission("Certificate.Manage")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CertificateReq request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<string>.ErrorResponse(
                    "Validation failed",
                    errors
                ));
            }

            try
            {
                var cetificateRes = await _service.CreateAsync(request);

                return Ok(ApiResponse<CertificateRes>.SuccessResponse(
                    cetificateRes,
                    "Cetificate created successfully"
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.ErrorResponse(
                    "System error",
                    new[] { ex.Message }
                ));
            }
        }

        [Authorize]
        [HasPermission("Certificate.Manage")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(
                    "Cetificate not found",
                    new[] { $"No course with ID {id}" }
                ));
            }

            return Ok(ApiResponse<CertificateRes>.SuccessResponse(
                data,
                "Fetched Cetificate successfully"
            ));
        }

        [Authorize]
        [HasPermission("Certificate.Manage")]
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(long id, [FromForm] CertificateReq request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ApiResponse<string>.ErrorResponse(
                    "Validation failed",
                    errors
                ));
            }
            try
            {
                var updatedCetificate = await _service.UpdateAsync(id, request);
                return Ok(new
                {
                    success = true,
                    data = updatedCetificate,
                    message = "Cetificate updated successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
