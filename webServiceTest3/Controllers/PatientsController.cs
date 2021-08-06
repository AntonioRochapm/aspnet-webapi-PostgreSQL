using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webServiceTest3.Models;

namespace webServiceTest3.Controllers
{  
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientContext _context;
        private readonly ILogger<PatientController> _logger;

        public PatientController(PatientContext context, ILogger<PatientController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Patient
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatient()
        {
            Serilog.Log.Information("Começo --Log de Gestão de Pacientes -->Get Method");
            //throw new ArgumentException("Aptece-me lanchar isto");
            try
            {
                var p = await _context.PatientData.ToListAsync();
                return p;
            }catch(Exception ex)
            {
                Serilog.Log.Error("Erro: Log de Gestão de Pacientes --> Get Method error", ex);
                throw;
            }
             
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            Serilog.Log.Information("Começo --Log de Gestão de Pacientes -->Get bt Id Method");
            return await _context.PatientData.FindAsync(id);
        }

        // PUT: api/Patient/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(long id, Patient Patient)
        {
            Serilog.Log.Information("Começo --Log de Gestão de Pacientes -->Put Method");
            if (id != Patient.Id)
            {
                return BadRequest();
            }
            _context.Entry(Patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    Serilog.Log.Error("Erro: Log de Gestão de Pacientes -->Put Method error");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patient
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient Patient)
        {
            Serilog.Log.Information("Começo --Log de Gestão de Pacientes -->Post Method");
            //if (!ModelState.IsValid)
            //{
            //    return NoContent();
            //}
            try
            {                
                _context.PatientData.Add(Patient);
                await _context.SaveChangesAsync();

                //return CreatedAtAction("GetPatient", new { id = Patient.Id }, Patient);
                return CreatedAtAction(nameof(GetPatient), new { id = Patient.Id }, Patient);
            }catch(Exception ex)
            {
                //Serilog.Log.Error("Erro: Log de Gestão de Pacientes -->Post Method error", ex);
                throw;
            }
            
        }

        // DELETE: api/Patient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(long id)
        {
            Serilog.Log.Information("Começo --Log de Gestão de Pacientes -->Delete Method");
            try
            {
                
                var Patient = await _context.PatientData.FindAsync(id);
                if (Patient == null)
                {
                    return NotFound();
                }

                _context.PatientData.Remove(Patient);
                await _context.SaveChangesAsync();

                return NoContent();
            }catch(Exception ex)
            {
                Serilog.Log.Error("Erro: Log de Gestão de Pacientes -->Delete Method error", ex);
                throw;
            }
            
        }

        private bool PatientExists(long id)
        {
            return _context.PatientData.Any(e => e.Id == id);
        }
    }
}
