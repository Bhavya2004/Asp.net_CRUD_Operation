using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Data_Annotation.Models;

namespace CRUD_with_sql.Controllers
{
    public class Loc_StateController : Controller
    {
        public IConfiguration Configuration;
        //cretaed a constructor to use Configuration
        public Loc_StateController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #region State List/SelectAll
        public IActionResult Loc_State()
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));

            sqlConnection.Open();
            #region Country Dropdown
            SqlCommand objCmd1 = sqlConnection.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_Country_SelectComboBox";
            SqlDataReader reader1 = objCmd1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(reader1);
            

            List<CountryModel> list = new List<CountryModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                CountryModel countryModel = new CountryModel();
                countryModel.CountryId = Convert.ToInt32(dr["CountryID"]);
                countryModel.CountryName = dr["CountryName"].ToString();
                list.Add(countryModel);
            }
            ViewBag.CountryList = list;

            #endregion
            
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_LOC_State_SelectAll";

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            return View(dt);
        }
        #endregion

        #region Delete State
        //Delete
        public IActionResult DeleteState(int id) {

            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_LOC_State_Delete";
            sqlCommand.Parameters.AddWithValue("@StateID", id);

            sqlCommand.ExecuteNonQuery();

            return RedirectToAction("Loc_State");
        }
        #endregion

        #region Add State/Insert
        public IActionResult AddState(int? StateId)
        {
            #region CountryCombobox
            SqlConnection sqlConnection1 = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            DataTable dt1 = new DataTable();
            sqlConnection1.Open();
            SqlCommand sqlCommand1 = sqlConnection1.CreateCommand();
            sqlCommand1.CommandType = CommandType.StoredProcedure;
            sqlCommand1.CommandText = "PR_Country_SelectComboBox";
            SqlDataReader sqlDataReader1 = sqlCommand1.ExecuteReader();
            dt1.Load(sqlDataReader1);
            sqlConnection1.Close();

            List<CountryDropDownModel> list = new List<CountryDropDownModel>();
            foreach(DataRow dr in dt1.Rows)
            {
                CountryDropDownModel cdd = new CountryDropDownModel();
                cdd.CountryId = Convert.ToInt32(dr["CountryId"]);
                cdd.CountryName = dr["CountryName"].ToString();
                list.Add(cdd);
            }
            ViewBag.CountryList = list;
			#endregion

			#region record select by primary key
			if (StateId != null)
            {
                SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "PR_LOC_State_SelectByPK";
                sqlCommand.Parameters.Add("@StateID", SqlDbType.Int).Value = StateId;

                DataTable dt = new DataTable();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dt.Load(sqlDataReader);

                StateModel m = new StateModel();
                foreach (DataRow dr in dt.Rows)
                {
                    m.StateID = Convert.ToInt32(dr["StateID"]);
                    m.StateName = Convert.ToString(dr["StateName"]);
                    m.StateCode = Convert.ToString(dr["StateCode"]);
                    m.CountryId = Convert.ToInt32(dr["CountryID"]);
                    m.Created = Convert.ToDateTime(dr["Created"]);
                    m.Modified = Convert.ToDateTime(dr["Modified"]);
                }
                return View("StateAddEdit", m);
            }
            #endregion
            return View("StateAddEdit");
        }

        #region Save
        [HttpPost]
        public IActionResult Save(StateModel m)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;

            if (m.StateID == null)
            {  //add new State

                sqlCommand.CommandText = "PR_LOC_State_Insert";
                //sqlCommand.Parameters.Add("@Created", SqlDbType.Date).Value = m.Created;
            }
            else //update country
            {
                sqlCommand.CommandText = "PR_LOC_State_Update";
                sqlCommand.Parameters.Add("@StateID", SqlDbType.Int).Value = m.StateID;

            }
            sqlCommand.Parameters.Add("@StateName", SqlDbType.VarChar).Value = m.StateName;
            sqlCommand.Parameters.Add("@StateCode", SqlDbType.VarChar).Value = m.StateCode;
            sqlCommand.Parameters.Add("@CountryId", SqlDbType.Int).Value = m.CountryId;
    

            if (Convert.ToBoolean(sqlCommand.ExecuteNonQuery()))
            {

                if (m.StateID == null)
                    //for displaying message
                    TempData["StateInsertMsg"] = "Record Inserted Successfully";
                else
                    //for displaying message
                    TempData["StateInsertMsg"] = "Record Updated Successfully";
            }

            return RedirectToAction("Loc_State");
        }
        #endregion

        #endregion
        #region Filter

        public IActionResult LOC_StateFilter(LOC_StateFilterModel filterModel)
        {
            string connectionStr = this.Configuration.GetConnectionString("my_cstring");
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_filter";
            objCmd.Parameters.AddWithValue("@CountryID", filterModel.CountryID);
            objCmd.Parameters.AddWithValue("@StateName", filterModel.StateName);
            objCmd.Parameters.AddWithValue("@StateCode", filterModel.StateCode);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            Console.WriteLine(filterModel.CountryID + " " + "hello");
            Console.WriteLine(filterModel.StateName + " " + filterModel.StateCode);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["CountryName"]);
                Console.WriteLine(dr["StateName"]);
                Console.WriteLine(dr["StateCode"]);
            }
            ModelState.Clear();

            #region Country DropDown
            SqlConnection connection1 = new SqlConnection(connectionStr);
            connection1.Open();
            SqlCommand objCmd1 = connection1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_Country_SelectComboBox";
            SqlDataReader reader1 = objCmd1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(reader1);
            connection1.Close();

            List<CountryModel> list = new List<CountryModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                CountryModel countryModel = new CountryModel();
                countryModel.CountryId = Convert.ToInt32(dr["CountryID"]);
                countryModel.CountryName = dr["CountryName"].ToString();
                list.Add(countryModel);
            }
            ViewBag.CountryList = list;

            #endregion

            return View("Loc_State", dt);
        }

        #endregion

        public IActionResult LOC_StateFilterByCountry(int? CountryID)
        {
            if (CountryID.HasValue)
            {
                string connectionStr = this.Configuration.GetConnectionString("my_cstring");
                DataTable dt = new DataTable();
                SqlConnection connection = new SqlConnection(connectionStr);
                connection.Open();
                SqlCommand objCmd = connection.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_State_SelectByCountryID";
                objCmd.Parameters.AddWithValue("@CountryID", CountryID.Value);
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);
                connection.Close();

                // Populate ViewBag.CountryList
                SqlConnection connection1 = new SqlConnection(connectionStr);
                connection1.Open();
                SqlCommand objCmd1 = connection1.CreateCommand();
                objCmd1.CommandType = CommandType.StoredProcedure;
                objCmd1.CommandText = "PR_Country_SelectComboBox";
                SqlDataReader reader1 = objCmd1.ExecuteReader();
                DataTable dt1 = new DataTable();
                dt1.Load(reader1);
                connection1.Close();

                List<CountryModel> list = new List<CountryModel>();

                foreach (DataRow dr in dt1.Rows)
                {
                    CountryModel countryModel = new CountryModel();
                    countryModel.CountryId = Convert.ToInt32(dr["CountryID"]);
                    countryModel.CountryName = dr["CountryName"].ToString();
                    list.Add(countryModel);
                }
                ViewBag.CountryList = list;

                // Set the selected country ID
                ViewBag.SelectedCountryID = CountryID.Value;

                return View("Loc_State", dt);
            }

            // If no CountryID is provided, redirect to the LOC_StateFilter view
            return RedirectToAction("LOC_StateFilter");
        }

    }
}
