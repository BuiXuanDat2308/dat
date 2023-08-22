using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web.Api.Model;
using MySqlConnector;
using Dapper;
using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MISA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //khởi tạo kết nối với database
                var connectionString = "Host=127.0.0.1;Port = 3306;Database= misa.web.cukcuk;User Id=root";
                //1.khởi tạo kết nối với mariadb
                var sqlConnnection = new MySqlConnection(connectionString);
                //Lấy dữ liệu:
                //Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = "SELECT*FROM Employee";
                // Truy vấn lấy dữ liệu:
                var employees = sqlConnnection.Query<Employee>(sql: sqlCommand);
                //trả về cho client
                return Ok(employees);
            }
            catch (Exception ex)
            {
                var error = new ErrorService();
                error.DevMeg = ex.Message;
                error.UserMsg = Resource.ResourceVN.Error_Exception;
                error.Data = ex.Data;    
                return StatusCode(500, error);  
                throw;
            }
        }
    
        [HttpGet("{employeeId}")]
        public IActionResult GetById( Guid employeeId)
        {
            try
            {
                //khởi tạo kết nối với database
                var connectionString = "Host=127.0.0.1;Port = 3306;Database= misa.web.cukcuk;User Id=root";
                //1.khởi tạo kết nối với mariadb
                var sqlConnnection = new MySqlConnection(connectionString);
                //Lấy dữ liệu:
                //Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = $"SELECT*FROM Employee WHERE EmployeeId = @EmployeeId";
                //nếu có tham số truyền cho câu lệnh mysql thì phải sử dụng DynamicParameter
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employeeId);
                // Truy vấn lấy dữ liệu:
                var employee = sqlConnnection.QueryFirstOrDefault<Employee>(sql: sqlCommand, param: parameters);
                //trả về cho client
                // var guid = Guid.NewGuid();
                return Ok(employee);
            }
            catch(Exception ex)
            {
                var error = new ErrorService();
                error.DevMeg = ex.Message;
                error.UserMsg = Resource.ResourceVN.Error_Exception;
                error.Data = ex.Data;
                return StatusCode(500, error);
                throw;
            }
        }
        //thêm mới nhân viên
        //201-thêm mới thành công 
        //400-dữ liệu đầu vào không hợp lệ
        //500 - có exception

        [HttpPost]
        public IActionResult Post(Employee employee) 
        {
            try
            {
                employee.EmployeeId = Guid.NewGuid();
                var error = new ErrorService();
                var errorData = new Dictionary<string, string>();
                var errorMsgs = new List<string>();
                //1 validate dữ liệu
                //1.1 mã nhân viên bắt buộc nhập
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    // error.UserMsg = "mã nhân viên không được phép để trống";
                    errorData.Add("EmployeeCode", "mã nhân viên không được phép để trống");
                    // return BadRequest(error);
                }
                if (string.IsNullOrEmpty(employee.FullName))
                {
                    // error.UserMsg = "họ và tên không được phép để trống";
                    errorData.Add("FullName", "họ và tên không được phép để trống");
                    //return BadRequest(error);
                }


                //email đúng định dạng
                if (!IsValidEmail(email: employee.Email))
                {
                    errorData.Add("Email", "Email không đúng định dạng");
                }
                //ngày sinh ko đc lớn hơn ngày hiện tại
                //2 khởi tạo kết nối
                var connectionString = "Host=127.0.0.1;Port = 3306;Database= misa.web.cukcuk;User Id=root";
                var mySqlConnection = new MySqlConnection(connectionString);
                //3 thực hiện thêm mới
                var sqlCommandText = "Proc_InsertEmployee";
                mySqlConnection.Open();
                var sqlCommand = mySqlConnection.CreateCommand() ;
                sqlCommand.CommandText = sqlCommandText;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlCommandBuilder.DeriveParameters(sqlCommand);
                var dynamicParam = new DynamicParameters();
                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    //thực hiện gán giá trị cho các param
                    var paramName = parameter.ParameterName;
                    var propName = paramName.Replace("@m_", "");
                    var entityProperty = employee.GetType().GetProperty(propName);
                    if( entityProperty != null )
                    {
                        var propValue = employee.GetType().GetProperty(propName).GetValue(employee);
                        dynamicParam.Add(paramName, propValue);
                    }
                    else
                    {
                        dynamicParam.Add(paramName, null);
                    }
                }
               // var dynamicParam = new DynamicParameters();
                //dynamicParam.Add("@m_EmployeeId",Guid.NewGuid());
                //dynamicParam.Add("m_EmployeeCode", employee.EmployeeCode);
                //dynamicParam.Add("@m_FullName", employee.FullName);
                //dynamicParam.Add("@m_DateOfBirth", employee.DateOfBirth);
                //dynamicParam.Add("@m_Gender", employee.Gender);
                //dynamicParam.Add("@m_IdentityNumber", employee.IdentityNumber);
                //dynamicParam.Add("@m_IdentityDate", employee.IdentityDate);
                //dynamicParam.Add("@m_IdentityPlace", employee.IdentityPlace);
                //dynamicParam.Add("@m_Email", employee.Email);
                //dynamicParam.Add("@m_TaxCode", employee.TaxCode);
                //dynamicParam.Add("@m_Salary", employee.Salary);
                //dynamicParam.Add("@m_JoinDate", employee.JoinDate);
                //dynamicParam.Add("@m_WorkStatus", employee.WorkStatus);
                //dynamicParam.Add("@m_PositionId", employee.PositionId);
                //dynamicParam.Add("@m_DepartmentId", employee.DepartmentId);
                //dynamicParam.Add("@m_CreateDate", DateTime.Now);
                //dynamicParam.Add("@m_CreateBy", employee.CreateBy);
                //dynamicParam.Add("@m_ModifiledDate", employee.ModifiledDate);
                //dynamicParam.Add("@m_ModifiledBy", employee.ModifiledBy);
                //dynamicParam.Add("@m_PhoneNumber", employee.PhoneNumber);
                
                var res = mySqlConnection.Execute(sql: sqlCommandText,param: employee, commandType: System.Data.CommandType.StoredProcedure);
                if (CheckEmployeeCode(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", "Mã nhân viên không được phép trùng");
                }
                if (errorData.Count > 0)
                {
                    error.UserMsg = "Dữ liệu đầu vào không hợp lệ";
                    return BadRequest(error);
                }
                //4 trả thông tin về cho client
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return Ok(res);
                }
                

                
            }
            catch(Exception ex)
            {
               return HandleException(ex);
            }
        }
        private bool CheckEmployeeCode(string employeeCode)
        {
            var connectionString = "Host=127.0.0.1;Port = 3306;Database= misa.web.cukcuk;User Id=root";
            var mySqlConnection = new MySqlConnection(connectionString);
            var sqlCheck = "SELECT EmployeeCode FROM Employee WHERE EmployeeCode=@EmployeeCode";
            var dynamicparam = new DynamicParameters();
            dynamicparam.Add("@EmployeeCode", employeeCode);
            var res = mySqlConnection.Query<string>(sqlCheck, dynamicparam);
            if(res != null)
            {
                return true;
            }
            else
            {
                return false;   
            }
        }
        private IActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.DevMeg = ex.Message;
            error.UserMsg = Resource.ResourceVN.Error_Exception;
            error.Data = ex.Data;
            return StatusCode(500, error);
           // throw;
        }
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
