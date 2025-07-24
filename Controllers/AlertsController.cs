using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmberAlerting.Data;
using AmberAlerting.Models;

namespace AmberAlerting.Controllers
{
    [ApiController]
    [Route("alerts")]
    public class AlertsController : ControllerBase
    {
        private readonly AlertContext _context;

        public AlertsController(AlertContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alert>>> GetAllAlerts()
        {
            try
            {
                var alerts = await _context.Alerts.ToListAsync();
                return Ok(alerts);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alert>> GetAlertById(string id)
        {
            try
            {
                var alert = await _context.Alerts.FindAsync(id);
                
                if (alert == null)
                {
                    return NotFound();
                }

                return Ok(alert);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Alert>> CreateAlert([FromBody] NewAlert newAlert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var alert = new Alert
                {
                    Message = newAlert.Message,
                    OriginalCountdown = newAlert.Countdown,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Alerts.Add(alert);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAlertById), new { id = alert.Id }, alert);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(string id)
        {
            try
            {
                var alert = await _context.Alerts.FindAsync(id);
                
                if (alert == null)
                {
                    return NotFound();
                }

                _context.Alerts.Remove(alert);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}