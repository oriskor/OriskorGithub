using EDI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDI.Controllers
{
    public class EmployeeController : Controller
    {
        
        private EDIEntities db = new EDIEntities();
        // GET: Employee
        #region Example with Form Post method  
        public ActionResult Employee()
        {
            var roleModel = new RoleModel();
            roleModel.listRole = GetRoleDataFromDB();
            //Set default employee records  
            //employeeModel.EmployeeId = employeeModel.listEmp.First().EmployeeId;
            //employeeModel.EmpName = employeeModel.listEmp.First().EmpName;
            //employeeModel.Salary = employeeModel.listEmp.First().Salary;
            return View(roleModel);
        }
        [HttpPost]
        public ActionResult Employee(int model)
        {
            var roleModel = new RoleModel();
            roleModel.listRole = GetRoleDataFromDB();
            roleModel.listMenu = GetMenuDataFromDB(model);
            //Filter employeeData based on EmployeeId  
            //This filter part you can do through sql queries also.  
            //Here model.EmployeeId is Dropdownlist selected EmployeeId.  
           // var emp = employeeModel.listEmp.Where(e => e.EmployeeId == model.EmployeeId).FirstOrDefault();
            //Set default emp records  
            //employeeModel.EmployeeId = emp.EmployeeId;
            //employeeModel.EmpName = emp.EmpName;
            //employeeModel.Salary = emp.Salary;
            return View(roleModel);
        }
        private List<RoleModel> GetRoleDataFromDB()
        {
            //Here you can write your query to fetch data from db  
            var listEmp = new List<RoleModel>();
            var shapeItems = from x in db.role_master select new RoleModel { RoleName = x.roll_name, RoleId = x.role_id };
           return listEmp = shapeItems.ToList();
            //var emp1 = new EmployeeModel();
            //emp1.EmployeeId = 1;
            //emp1.EmpName = "Employee1";
            //emp1.Salary = 50000;
            //var emp2 = new EmployeeModel();
            //emp2.EmployeeId = 2;
            //emp2.EmpName = "Employee2";
            //emp2.Salary = 56000;
            //var emp3 = new EmployeeModel();
            //emp3.EmployeeId = 3;
            //emp3.EmpName = "Employee3";
            //emp3.Salary = 60000;
            //listEmp.Add(emp1);
            //listEmp.Add(emp2);
            //listEmp.Add(emp3);
            //return listEmp;
        }
        private List<RoleModel> GetMenuDataFromDB(int model)
        {
            FormCollection form = new FormCollection();
            int RoleId = model;
            return Comman.getAllMenuWhichNotAssignedToTheRole1(Convert.ToInt32(RoleId));
        }
        #endregion
    }
}