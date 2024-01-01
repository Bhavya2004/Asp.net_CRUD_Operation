using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CRUD_with_sql.Models;
using Data_Annotation.Models;
using CRUD_with_sql.Areas.Student.Models;

namespace CRUD_with_sql.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("/Student/Student")]
    public class StudentController : Controller
    {
        public IConfiguration Configuration;
        //cretaed a constructor to use Configuration
        public StudentController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [Route("/StudentList")]
        public IActionResult StudentList()
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            sqlConnection.Open();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Student_SelectAll";

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            return View(dt);
        }

        [Route("/DeleteStudent")]
        #region Delete Student
        //Delete
        public IActionResult DeleteStudent(int id)
        {

            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Student_DeleteByPK";
            sqlCommand.Parameters.AddWithValue("@StudentID", id);

            sqlCommand.ExecuteNonQuery();

            return RedirectToAction("StudentList");
        }
        #endregion

        [Route("/AddStudent")]
        #region Add student(insert) and Edit Student(SelectByPK)

        public IActionResult AddStudent(int? StudentId)
        {
            if (StudentId != null)
            {
                SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "PR_Student_SelectByPK";
                sqlCommand.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentId;

                DataTable dt = new DataTable();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dt.Load(sqlDataReader);

                StudentModel modelStudentModel = new StudentModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelStudentModel.StudentName = Convert.ToString(dr["StudentName"]);
                    modelStudentModel.MobileNoStudent = Convert.ToString(dr["MobileNoStudent"]);
                    modelStudentModel.Email = Convert.ToString(dr["Email"]);
                    modelStudentModel.MobileNoFather = Convert.ToString(dr["MobileNoFather"]);
                    modelStudentModel.Address = Convert.ToString(dr["Address"]);
                    modelStudentModel.BirthDate = Convert.ToString(dr["BirthDate"]);
                    modelStudentModel.Age = Convert.ToInt32(dr["Age"]);
                    modelStudentModel.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    modelStudentModel.Gender = Convert.ToString(dr["Gender"]);
                    modelStudentModel.Password = Convert.ToString(dr["Password"]);
                    modelStudentModel.BranchID = Convert.ToInt32(dr["BranchID"]);
                    modelStudentModel.CityID = Convert.ToInt32(dr["CityID"]);
                    
                }
                return View("studentAddEdit", modelStudentModel);
            }
            return View("StudentAddEdit");
        }
        [Route("/Save")]
        [HttpPost]
        public IActionResult Save(StudentModel modelStudentModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;


            if (modelStudentModel.StudentID == null)
            {  //add new student

                sqlCommand.CommandText = "PR_Student_Insert";
            }
            else //update student
            {
                sqlCommand.CommandText = "PR_Student_UpdateByPK";
                sqlCommand.Parameters.Add("@StudentID", SqlDbType.Int).Value = modelStudentModel.StudentID;

            }
            sqlCommand.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = modelStudentModel.StudentName;
            sqlCommand.Parameters.Add("@MobileNoStudent", SqlDbType.VarChar).Value = modelStudentModel.MobileNoStudent;
            sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = modelStudentModel.Email;
            sqlCommand.Parameters.Add("@MobileNoFather", SqlDbType.VarChar).Value = modelStudentModel.MobileNoFather;
            sqlCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = modelStudentModel.Address;
            sqlCommand.Parameters.Add("@BirthDate", SqlDbType.VarChar).Value = modelStudentModel.BirthDate;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = modelStudentModel.Age;
            sqlCommand.Parameters.AddWithValue("@IsActive", modelStudentModel.IsActive);
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = modelStudentModel.Gender;
            sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = modelStudentModel.Password;
            sqlCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = modelStudentModel.BranchID;
            sqlCommand.Parameters.Add("@CityID", SqlDbType.Int).Value = modelStudentModel.CityID;
            sqlCommand.ExecuteNonQuery();


            //    if (Convert.ToBoolean(sqlCommand.ExecuteNonQuery()))
            //    {

            //        if (modelStudentModel.StudentID == null)
            //            //for displaying message
            //            TempData["StudentInsertMsg"] = "Record Inserted Successfully";
            //        else
            //            //for displaying message
            //            TempData["StudentInsertMsg"] = "Record Updated Successfully";
            //    }
            return RedirectToAction("StudentList");
            }
            #endregion
            public IActionResult Index()
        {
            return View();
        }
    }
}
