using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Data_Annotation.Models;
using CRUD_with_sql.Models;

namespace CRUD_with_sql.Controllers
{
	public class Loc_BranchController : Controller
	{
		public IConfiguration Configuration;
		//cretaed a constructor to use Configuration
		public Loc_BranchController(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		#region SelectAll/Branch List
		public IActionResult Loc_Branch()
		{
			SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
			sqlConnection.Open();

			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandText = "PR_Branch_SelectAll";

			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			DataTable dt = new DataTable();
			dt.Load(sqlDataReader);
			return View(dt);
		}
		#endregion

		#region Add Branch(insert) and Edit Branch(selectByPK)
		public IActionResult AddBranch(int? BranchId)
		{
			if (BranchId != null)
			{
				SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
				sqlConnection.Open();
				SqlCommand sqlCommand = sqlConnection.CreateCommand();
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.CommandText = "PR_Branch_SelectByPK";
				sqlCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchId;

				DataTable dt = new DataTable();
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				dt.Load(sqlDataReader);

				BranchModel modelBranchModel = new BranchModel();
				foreach (DataRow dr in dt.Rows)
				{
					modelBranchModel.BranchId = Convert.ToInt32(dr["BranchID"]);
					modelBranchModel.BranchName = Convert.ToString(dr["BranchName"]);
					modelBranchModel.BranchCode = Convert.ToString(dr["BranchCode"]);
					modelBranchModel.Created = Convert.ToDateTime(dr["Created"]);
					modelBranchModel.Modified = Convert.ToDateTime(dr["Modified"]);
				}
				return View("BranchAddEdit", modelBranchModel);
			}
			return View("BranchAddEdit");
		}
		[HttpPost]
		public IActionResult Save(BranchModel modelBranchModel)
		{
			SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
			sqlConnection.Open();
			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandType = CommandType.StoredProcedure;


			if (modelBranchModel.BranchId == null)
			{  //add new branch

				sqlCommand.CommandText = "PR_Branch_Insert";
			}
			else //update country
			{
				sqlCommand.CommandText = "PR_Branch_Update";
				sqlCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = modelBranchModel.BranchId;

			}
			sqlCommand.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = modelBranchModel.BranchName;
			sqlCommand.Parameters.Add("@BranchCode", SqlDbType.VarChar).Value = modelBranchModel.BranchCode;
			//sqlCommand.Parameters.Add("@Created", SqlDbType.Date).Value = modelBranchModel.Created;
			//sqlCommand.Parameters.Add("@Modified", SqlDbType.Date).Value = modelBranchModel.Modified;


			if (Convert.ToBoolean(sqlCommand.ExecuteNonQuery()))
			{

				if (modelBranchModel.BranchId == null)
					//for displaying message
					TempData["BranchInsertMsg"] = "Record Inserted Successfully";
				else
					//for displaying message
					TempData["BranchInsertMsg"] = "Record Updated Successfully";
			}
			return RedirectToAction("Loc_Branch");
		}
		#endregion

		#region Delete Branch
		public IActionResult DeleteBranch(int id)
		{
			SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
			sqlConnection.Open();
			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandText = "PR_Branch_Delete";
			sqlCommand.Parameters.AddWithValue("@BranchID", id);

			sqlCommand.ExecuteNonQuery();

			return RedirectToAction("Loc_Branch");
		}
		#endregion

		public IActionResult Index()
		{
			return View();
		}
	}
}
