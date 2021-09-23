using MCVTask.Models;
using MCVTask.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MCVTask.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MCVContext _context;

        public EmployeesController(MCVContext context)
        {
            _context = context;
        }

        public ActionResult Index(int? pageIndex)
        {
            int pageSize = 12;
            IPagedList<EmployeeViewModel> employees = _context.Employees.Include(x => x.Department).Select(x => new EmployeeViewModel { Id = x.Id, Birthday = x.Birthday, HiringDate = x.HiringDate, Name = x.Name, Title = x.Title, Department = x.Department.Name }).ToPagedList((pageIndex.HasValue) ? pageIndex.Value : 1, pageSize);

            List<Department> departments = new List<Department> { new Department { Name = "All", Id = 0 } };
            departments.AddRange(_context.Departments.ToList());

            ViewBag.departments = departments;

            return View(employees);
        }

        public ActionResult CreateOrUpdate(int? id)
        {
            ViewBag.departments = _context.Departments.ToList();
            if(id.HasValue)
            {
                Employee employee = _context.Employees.FirstOrDefault(x => x.Id == id);
                if(employee != null)
                {
                    EmployeeEditorViewModel employeeEditor = new EmployeeEditorViewModel { Id = employee.Id, Birthday = employee.Birthday, DepartmentId = employee.DepartmentId, HiringDate = employee.HiringDate, Name = employee.Name, Title = employee.Title };
                    return View("Views/Employees/Editer.cshtml", employeeEditor);
                }
            }
            return View("Views/Employees/Editer.cshtml");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateOrUpdate(EmployeeEditorViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.departments = _context.Departments.ToList();
                return View("Views/Employees/Editer.cshtml", employee);
            }

            if (!employee.Id.HasValue)
            {
                Employee newEmployee = new Employee
                {
                    Name = employee.Name,
                    Title = employee.Title,
                    HiringDate = employee.HiringDate,
                    Birthday = employee.Birthday,
                    DepartmentId = employee.DepartmentId
                };
                _context.Employees.Add(newEmployee);
            }
            else
            {
                Employee oldEmployee = _context.Employees.FirstOrDefault(x => x.Id == employee.Id);
                if(oldEmployee != null)
                {
                    oldEmployee.Name = employee.Name;
                    oldEmployee.Title = employee.Title;
                    oldEmployee.HiringDate = employee.HiringDate;
                    oldEmployee.Birthday = employee.Birthday;
                    oldEmployee.DepartmentId = employee.DepartmentId;
                    _context.Employees.Update(oldEmployee);
                }
            }
            _context.SaveChanges();
            return Ok();
        }


        public ActionResult Delete(int id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(EmployeeSearchViewMode employeeSearch)
        {
            int pageSize = 12;
            var employees = _context.Employees.AsQueryable();

            if(string.IsNullOrEmpty(employeeSearch.Name) == false)
            {
                employees = employees.Where(x => x.Name.Contains(employeeSearch.Name));
            }

            if (string.IsNullOrEmpty(employeeSearch.Title) == false)
            {
                employees = employees.Where(x => x.Title.Contains(employeeSearch.Title));
            }

            if (employeeSearch.MinHiringDate.HasValue)
            {
                employees = employees.Where(x => x.HiringDate >= employeeSearch.MinHiringDate);
            }

            if (employeeSearch.MaxHiringDate.HasValue)
            {
                employees = employees.Where(x => x.HiringDate <= employeeSearch.MinHiringDate);
            }


            if (employeeSearch.MinBirthday.HasValue)
            {
                employees = employees.Where(x => x.HiringDate >= employeeSearch.MinBirthday);
            }


            if (employeeSearch.MaxBirthday.HasValue)
            {
                employees = employees.Where(x => x.Birthday <= employeeSearch.MaxBirthday);
            }

            if(employeeSearch.Department > 0)
            {
                employees = employees.Where(x => x.DepartmentId == employeeSearch.Department);
            }

            IPagedList<EmployeeViewModel> employeesModel = employees.Include(x => x.Department).Select(x => new EmployeeViewModel { Id = x.Id, Birthday = x.Birthday, HiringDate = x.HiringDate, Name = x.Name, Title = x.Title, Department = x.Department.Name }).ToPagedList(1, pageSize);

            List<Department> departments = new List<Department> { new Department { Name = "All", Id = 0 } };
            departments.AddRange(_context.Departments.ToList());

            ViewBag.departments = departments;

            return View("Index", employeesModel);
        }


    }
}
