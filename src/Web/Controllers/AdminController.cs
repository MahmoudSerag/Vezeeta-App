﻿using Application.Dtos;
using Application.Interfaces.Helpers;
using Application.Interfaces.Services;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IJwtHelpService jwtHelpService;
        private readonly IDoctorService doctorService;

        public AdminController(
            IPatientService patientService,
            IHttpContextAccessor httpContextAccessor,
            IJwtHelpService jwtHelpService,
            IDoctorService doctorService
        )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.patientService = patientService;
            this.jwtHelpService = jwtHelpService;
            this.doctorService = doctorService;
        }

        [HttpGet("patients/count")]
        public async Task<IActionResult> GetCountOfPatients()
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

                if (accessToken == null)
                    return Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "Unauthorized"
                        }
                    );

                var decodedToken = this.jwtHelpService.DecodeToken(accessToken);

                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Admin")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with admin account."
                        }
                    );
                }

                int numberOfPatients = await this.patientService.GetCountOfPatients("Patient");

                return Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        numberOfPatients
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        success = false,
                        statusCode = 500,
                        message = ex
                    }
                );
            }
        }

        [HttpGet("patients/{patientId}")]
        public async Task<IActionResult> GetPatientById([FromRoute] string patientId)
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

                if (accessToken == null)
                    return Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "Unauthorized"
                        }
                    );

                var decodedToken = this.jwtHelpService.DecodeToken(accessToken);

                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Admin")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with admin account."
                        }
                    );
                }

                var patient = await this.patientService.GetPatientById(patientId);

                if (patient == null)
                    return Ok(
                        new
                        {
                            success = true,
                            statusCdoe = 200,
                            message = "No patient found with the given Id"
                        }
                    );

                return Ok(
                    new
                    {
                        succuss = true,
                        statusCode = 200,
                        patient
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        sucess = false,
                        statusCode = 500,
                        message = ex.Message
                    }
                );
            }
        }

        [HttpGet("patients")]
        public async Task<IActionResult> GetAllPatients(
            [FromQuery] int page,
            [FromQuery] int size,
            [FromQuery] string searchQuery
        )
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

                if (accessToken == null)
                    return Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "Unauthorized"
                        }
                    );

                var decodedToken = this.jwtHelpService.DecodeToken(accessToken);

                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Admin")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with admin account."
                        }
                    );
                }

                var patients = await this.patientService.GetAllPatients(
                    page,
                    size,
                    searchQuery,
                    RolesEnum.Patient.ToString()
                );

                var totalPatientsCount = await this.patientService.GetCountOfPatients(
                    RolesEnum.Patient.ToString()
                );

                if (patients.Count == 0 || totalPatientsCount == 0)
                    return Ok(
                        new
                        {
                            success = true,
                            statusCdoe = 200,
                            message = "No patients found in this page."
                        }
                    );

                return Ok(
                    new
                    {
                        succuss = true,
                        statusCode = 200,
                        totalPtientsCount = totalPatientsCount,
                        maxPages = (int)Math.Ceiling((decimal)totalPatientsCount / size),
                        currentPage = page,
                        patientsPerPage = size,
                        patients
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        sucess = false,
                        statusCode = 500,
                        message = ex.Message
                    }
                );
            }
        }

        [HttpGet("doctors/count")]
        public async Task<IActionResult> GetDoctorsCount()
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

                if (accessToken == null)
                    return Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "Unauthorized"
                        }
                    );

                var decodedToken = this.jwtHelpService.DecodeToken(accessToken);

                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Admin")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with admin account."
                        }
                    );
                }

                var totalDoctorsCount = await this.doctorService.GetCountOfDoctors(
                    RolesEnum.Doctor.ToString()
                );

                if (totalDoctorsCount == 0)
                    return Ok(
                        new
                        {
                            success = true,
                            statusCdoe = 200,
                            message = "No doctors found in this page."
                        }
                    );

                return Ok(
                    new
                    {
                        succuss = true,
                        statusCode = 200,
                        totalDoctorsCount = totalDoctorsCount,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        sucess = false,
                        statusCode = 500,
                        message = ex.Message
                    }
                );
            }
        }

        [HttpGet("doctors/{doctorId}")]
        public async Task<IActionResult> GetDoctorById(string doctorId)
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

                if (accessToken == null)
                    return Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "Unauthorized"
                        }
                    );

                var decodedToken = this.jwtHelpService.DecodeToken(accessToken);

                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Admin")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with admin account."
                        }
                    );
                }

                var doctor = await this.doctorService.GetDoctorById(doctorId);

                if (doctor == null)
                    return Ok(
                        new
                        {
                            success = true,
                            statusCdoe = 200,
                            message = "No doctors found with the given Id."
                        }
                    );

                return Ok(
                    new
                    {
                        succuss = true,
                        statusCode = 200,
                        doctor,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        sucess = false,
                        statusCode = 500,
                        message = ex.Message
                    }
                );
            }
        }

        [HttpPost("doctors")]
        public async Task<IActionResult> CreateNewDoctor(DoctorDto doctorDto)
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

                if (accessToken == null)
                    return Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "Unauthorized"
                        }
                    );

                var decodedToken = this.jwtHelpService.DecodeToken(accessToken);

                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Admin")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with admin account."
                        }
                    );
                }

                if (!ModelState.IsValid)
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            message = ModelState.ValidationState
                        }
                    );

                if (doctorDto.Image == null)
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            messgae = "Image is required."
                        }
                    );

                if (doctorDto.Image.Length > 5 * 1024 * 1024)
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            messgae = "Invalid image size"
                        }
                    );
                }

                if (
                    doctorDto.Image.ContentType != "image/png"
                    && doctorDto.Image.ContentType != "image/jpg"
                    && doctorDto.Image.ContentType != "image/jpeg"
                )
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            messgae = "Invalid image type"
                        }
                    );

                var doctor = await this.doctorService.CreateNewDoctor(
                    doctorDto,
                    doctorDto.Password,
                    doctorDto.Specialization
                );

                if (!doctor.Succeeded)

                    return StatusCode(
                        500,
                        new
                        {
                            sucess = false,
                            statusCode = 500,
                            message = doctor.Errors
                        }
                    );

                return Ok(
                    new
                    {
                        succuss = true,
                        statusCode = 201,
                        messgae = "Doctor created successfully",
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        sucess = false,
                        statusCode = 500,
                        message = ex.Message
                    }
                );
            }
        }

        [HttpPut("doctors/{doctorId}")]
        public async Task<IActionResult> UpdateDoctorById(
            [FromRoute] string doctorId,
            UpdateDoctorDto updateDoctorDto
        )
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

                if (accessToken == null)
                    return Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "Unauthorized"
                        }
                    );

                var decodedToken = this.jwtHelpService.DecodeToken(accessToken);

                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Admin")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with admin account."
                        }
                    );
                }

                if (!ModelState.IsValid)
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            message = ModelState.ValidationState
                        }
                    );

                if (updateDoctorDto.Image == null)
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            messgae = "Image is required."
                        }
                    );

                if (updateDoctorDto.Image.Length > 5 * 1024 * 1024)
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            messgae = "Invalid image size"
                        }
                    );
                }

                if (
                    updateDoctorDto.Image.ContentType != "image/png"
                    && updateDoctorDto.Image.ContentType != "image/jpg"
                    && updateDoctorDto.Image.ContentType != "image/jpeg"
                )
                    return BadRequest(
                        new
                        {
                            success = false,
                            statusCode = 400,
                            messgae = "Invalid image type"
                        }
                    );

                var doctor = await this.doctorService.UpdateDoctorById(
                    updateDoctorDto,
                    doctorId,
                    updateDoctorDto.Specialization
                );

                if (!doctor.Succeeded)

                    return StatusCode(
                        500,
                        new
                        {
                            sucess = false,
                            statusCode = 500,
                            message = doctor.Errors
                        }
                    );

                return Ok(
                    new
                    {
                        succuss = true,
                        statusCode = 200,
                        messgae = "Doctor updated successfully",
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        sucess = false,
                        statusCode = 500,
                        message = ex.Message
                    }
                );
            }
        }

        [HttpGet("doctors")]
        public async Task<IActionResult> GetAllDoctors(
            [FromQuery] int page,
            [FromQuery] int limit,
            [FromQuery] string searchQuery
        )
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

                if (accessToken == null)
                    return Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "Unauthorized"
                        }
                    );

                var decodedToken = this.jwtHelpService.DecodeToken(accessToken);

                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Admin")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with admin account."
                        }
                    );
                }

                var doctors = await this.doctorService.GetAllDoctors(page, limit, searchQuery);
                var totalDoctorsCount = await this.doctorService.GetCountOfDoctors(
                    RolesEnum.Doctor.ToString()
                );

                if (doctors.Count == 0 || totalDoctorsCount == 0)
                    return Ok(
                        new
                        {
                            sucess = true,
                            statusCode = 200,
                            message = "No doctors found",
                        }
                    );

                return Ok(
                    new
                    {
                        succuss = true,
                        statusCode = 200,
                        totalDoctorsCount = totalDoctorsCount,
                        maxPages = (int)Math.Ceiling((decimal)totalDoctorsCount / limit),
                        currentPage = page,
                        doctorsPerPage = limit,
                        doctors,
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(
                    500,
                    new
                    {
                        sucess = false,
                        statusCode = 500,
                        message = ex.Message
                    }
                );
            }
        }

        [HttpDelete("doctors{doctorId}")]
        public async Task<IActionResult> DeleteDoctorById([FromRoute] string doctorId)
        {
            try
            {
                Console.WriteLine(true);
                var accessToken = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

                if (accessToken == null)
                    return Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "Unauthorized"
                        }
                    );

                var decodedToken = this.jwtHelpService.DecodeToken(accessToken);

                string roleName = decodedToken.Claims
                    .First(claim => claim.Type == "RoleName")
                    .Value;

                if (roleName != "Admin")
                {
                    return StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "Forbidden. Should log in with admin account."
                        }
                    );
                }

                var result = await this.doctorService.DeleteDoctorById(doctorId);

                if (result != "Succeeded")
                    return StatusCode(
                        500,
                        new
                        {
                            success = false,
                            statusCode = 500,
                            message = result
                        }
                    );

                return Ok(
                    new
                    {
                        succuss = true,
                        statusCode = 200,
                        message = "Doctor deleted successfully."
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(
                    500,
                    new
                    {
                        sucess = false,
                        statusCode = 500,
                        message = ex.Message
                    }
                );
            }
        }
    }
}
