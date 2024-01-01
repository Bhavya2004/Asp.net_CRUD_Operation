using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Data_Annotation.Models;
using System.Diagnostics;

namespace CRUD_with_sql.Controllers
{
	public class Loc_CountryController : Controller
	{
		public IConfiguration Configuration;
		//cretaed a constructor to use Configuration
		public Loc_CountryController(IConfiguration configuration)
		{
			Configuration = configuration;
		}

        #region SelectAll / CountryList
        public IActionResult Loc_Country()
		{
			SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
			sqlConnection.Open();

			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandText = "PR_Country_SelectAll_WithStateCount";

			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			DataTable dt = new DataTable();
			dt.Load(sqlDataReader);
			return View(dt);
		}
        #endregion

        #region Add Country(insert) and Edit Country(SelectByPK)

        public IActionResult AddCountry(int? CountryId)
        {
            if(CountryId != null)
            {
                SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "PR_Country_SelectByPK";
                sqlCommand.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryId;

                DataTable dt = new DataTable();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dt.Load(sqlDataReader);

                CountryModel modelCountryModel = new CountryModel();
                foreach (DataRow dr in dt.Rows)
                { 
                    modelCountryModel.CountryId = Convert.ToInt32(dr["CountryID"]);
                    modelCountryModel.CountryName = Convert.ToString(dr["CountryName"]);
                    modelCountryModel.CountryCode = Convert.ToString(dr["CountryCode"]);
                   
                }
                return View("CountryAddEdit", modelCountryModel);
            }
            return View("CountryAddEdit");
        }
        [HttpPost]   
        public IActionResult Save(CountryModel modelCountryModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            

            if (modelCountryModel.CountryId == null) {  //add new country

                sqlCommand.CommandText = "PR_Country_Insert";
            }
            else //update country
            {
                sqlCommand.CommandText = "PR_Country_Update";
                sqlCommand.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelCountryModel.CountryId;

            }
            sqlCommand.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = modelCountryModel.CountryName;
            sqlCommand.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = modelCountryModel.CountryCode;
          


            if (Convert.ToBoolean(sqlCommand.ExecuteNonQuery())){

                if(modelCountryModel.CountryId == null)
                    //for displaying message
                    TempData["CountryInsertMsg"] = "Record Inserted Successfully";
                else
                    //for displaying message
                    TempData["CountryInsertMsg"] = "Record Updated Successfully";
            }
            return RedirectToAction("Loc_Country");
        }
        #endregion

        #region Delete Country
        // Delete action
        public IActionResult DeleteCountry(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Country_Delete";
            sqlCommand.Parameters.AddWithValue("@CountryID", id);

            sqlCommand.ExecuteNonQuery();

            return RedirectToAction("Loc_Country");
        }
        #endregion

  

        public IActionResult Index()
		{
			return View();
		}
	}
}
