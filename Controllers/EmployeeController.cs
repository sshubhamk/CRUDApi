using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApi.Controllers {
    // [EnableCors("AllowOrigin")]
    [Route ("Employee/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase {

        private readonly DB01Context _context;
        public List<Employee> lstEmployee = new List<Employee> ();
        public EmployeeController (DB01Context context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees () {
            lstEmployee = _context.Employee.ToList ();
            return Ok (lstEmployee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee (Employee employee) {
            try {
                employee.status = Status.StatusOfEmployee.Pending.ToString ();
                await _context.Employee.AddAsync (employee);
                await _context.SaveChangesAsync ();
            } catch (Exception exc) {
                return BadRequest ();
            }
            return Ok ();
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee (long id) {
            try {
                Employee eid = _context.Employee.Where (a => a.id == id).SingleOrDefault ();
                _context.Remove (eid);
                _context.SaveChanges ();
            } catch (Exception exc) {
                return BadRequest ();
            }
            return Ok ();
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> ModifyEmployee (Employee employee) {
            Employee objEmployee = new Employee ();
            try {
                objEmployee = employee;
                _context.Employee.Update (objEmployee);
                await _context.SaveChangesAsync ();
            } catch (Exception ex) {
                Console.WriteLine ("Exception", ex);
                return Conflict (ex);
            }
            return Ok ();
        }
    }
}