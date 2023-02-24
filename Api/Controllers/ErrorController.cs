using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ErrorController: BaseApiController
    {
        //400
        [HttpGet("badrequest")]
        public ActionResult BadRequestGet() {
            return BadRequest("Someting not right");
        }

        //400
        [HttpPost("badrequest")]
        public ActionResult BadRequestPost(RegisterDto dto) {
            return Ok();
        }

        //401
        [Authorize]
        [HttpGet("auth")]
        public ActionResult Auth() {
            return BadRequest();
        }

        //403
        [HttpGet("forbidden")]
        public ActionResult Status403() {
            return StatusCode(403);
        }

        //404
        [HttpGet("notfound")]
        public ActionResult Not404Found() {
            return NotFound();
        }

        //500
        [HttpGet("server")]
        public ActionResult Server() {
            throw new NullReferenceException();
        }
    }
}