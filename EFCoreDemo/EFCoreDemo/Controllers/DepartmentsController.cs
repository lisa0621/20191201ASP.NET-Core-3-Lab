﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase {
        private readonly ContosouniversityContext _context;

        public DepartmentsController (ContosouniversityContext context) {
            _context = context;
            // this.logger = logger;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment () {
            // logger.LogInformation("總筆數 {num}", _context.Department.Count());
            return await _context.Department.ToListAsync ();
        }

        // GET: api/Departments/5
        [HttpGet ("{id}")]
        public async Task<ActionResult<Department>> GetDepartment (int id) {
            var department = await _context.Department.FindAsync (id);

            if (department == null) {
                return NotFound ();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut ("{id}")]
        public async Task<IActionResult> PutDepartment (int id, Department department) {
            if (id != department.DepartmentId) {
                return BadRequest ();
            }

            _context.Entry (department).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                if (!DepartmentExists (id)) {
                    return NotFound ();
                } else {
                    throw;
                }
            }

            return NoContent ();
        }

        // POST: api/Departments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment (Department department) {
            _context.Department.Add (department);
            await _context.SaveChangesAsync ();

            return CreatedAtAction ("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete ("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment (int id) {
            var department = await _context.Department.FindAsync (id);
            if (department == null) {
                return NotFound ();
            }

            _context.Department.Remove (department);
            await _context.SaveChangesAsync ();

            return department;
        }

        private bool DepartmentExists (int id) {
            return _context.Department.Any (e => e.DepartmentId == id);
        }
    }
}