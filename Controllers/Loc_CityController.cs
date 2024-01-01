using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CRUD_with_sql.Models;

namespace CRUD_with_sql.Controllers
{
    public class Loc_CityController : Controller
    {
        public IConfiguration Configuration;
        //cretaed a constructor to use Configuration
        public Loc_CityController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region City List/SelectAll
        public IActionResult Loc_City()
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            sqlConnection.Open();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_LOC_City_SelectAll";

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            return View(dt);
        }
        #endregion

        #region Delete City
        //Delete
        public IActionResult DeleteCity(int id)
        {

            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_LOC_City_Delete";
            sqlCommand.Parameters.AddWithValue("@CityID", id);

            sqlCommand.ExecuteNonQuery();

            return RedirectToAction("Loc_City");
        }
        #endregion

        #region Add City/Insert
        public IActionResult AddCity(int? CityId)
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
            

            List<CountryDropDownModel> list = new List<CountryDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                CountryDropDownModel cdd = new CountryDropDownModel();
                cdd.CountryId = Convert.ToInt32(dr["CountryId"]);
                cdd.CountryName = dr["CountryName"].ToString();
                list.Add(cdd);
            }
            ViewBag.CountryList = list;
			#endregion

            List<StateDropDownModel> list1 = new List<StateDropDownModel>();
            ViewBag.StateList = list1;

			//#region StateCombobox
			//DataTable dt2 = new DataTable();
   //         SqlCommand sqlCommand2 = sqlConnection1.CreateCommand();
   //         sqlCommand2.CommandType = CommandType.StoredProcedure;
   //         sqlCommand2.CommandText = "PR_State_SelectComboBox";
   //         SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader();
   //         dt2.Load(sqlDataReader2);
   //         sqlConnection1.Close();

   //         List<StateDropDownModel> list1 = new List<StateDropDownModel>();
   //         foreach (DataRow dr in dt2.Rows)
   //         {
   //             StateDropDownModel sdd= new StateDropDownModel();
   //             sdd.StateId = Convert.ToInt32(dr["StateID"]);
   //             sdd.StateName = dr["StateName"].ToString();
   //             list1.Add(sdd);
   //         }
   //         ViewBag.StateList = list1;
			//#endregion

			#region record select by primary key
			if (CityId != null)
            {
                SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "PR_LOC_City_SelectByPK";
                sqlCommand.Parameters.Add("@CityID", SqlDbType.Int).Value = CityId;

                DataTable dt = new DataTable();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dt.Load(sqlDataReader);

                CityModel m = new CityModel();
                foreach (DataRow dr in dt.Rows)
                {
                    m.CityId = Convert.ToInt32(dr["CityID"]);
                    m.CityName = Convert.ToString(dr["CityName"]);
                    m.CityCode = Convert.ToString(dr["CityCode"]);
                    m.CountryID = Convert.ToInt32(dr["CountryID"]);
                    m.StateID = Convert.ToInt32(dr["StateID"]);
                    m.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    m.Modified = Convert.ToDateTime(dr["Modified"]);
                }
                return View("CityAddEdit", m);
            }
            #endregion
            return View("CityAddEdit");
        }

        #region Save
        [HttpPost]
        public IActionResult Save(CityModel m)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;

            if (m.CityId == null)
            {  //add new State

                sqlCommand.CommandText = "PR_LOC_City_Insert";
               
            }
            else //update country
            {
                sqlCommand.CommandText = "PR_LOC_City_Update";
                sqlCommand.Parameters.Add("@CityID", SqlDbType.Int).Value = m.CityId;

            }
			sqlCommand.Parameters.Add("@StateID", SqlDbType.Int).Value = m.StateID;
			sqlCommand.Parameters.Add("@CountryId", SqlDbType.Int).Value = m.CountryID;
			sqlCommand.Parameters.Add("@CityName", SqlDbType.VarChar).Value = m.CityName;
            sqlCommand.Parameters.Add("@CityCode", SqlDbType.VarChar).Value = m.CityCode;


            if (Convert.ToBoolean(sqlCommand.ExecuteNonQuery()))
            {

                if (m.CityId == null)
                    //for displaying message
                    TempData["CityInsertMsg"] = "Record Inserted Successfully";
                else
                    //for displaying message
                    TempData["CityInsertMsg"] = "Record Updated Successfully";
            }

            return RedirectToAction("Loc_City");
        }
        #endregion

        #endregion


        #region Cascade StateCombobox
        public IActionResult DropdownByCountry(int countryId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("my_cstring"));
            DataTable dt = new DataTable();
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_LOC_City_StateByCountryID_Combo";
            sqlCommand.Parameters.AddWithValue("@CountryID", countryId);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            dt.Load(sqlDataReader);
            sqlConnection.Close();

            List<StateDropDownModel> list = new List<StateDropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                StateDropDownModel sdd = new StateDropDownModel();
                sdd.StateId = Convert.ToInt32(dr["StateID"]);
                sdd.StateName = dr["StateName"].ToString();
                list.Add(sdd);
            }
            var vModel = list;
            return Json(vModel);
        }
        #endregion

        #region FILTER
        public IActionResult LOC_CityFilter(LOC_CityFilterModel FilterModel)
        {
            string connectionStr = this.Configuration.GetConnectionString("my_cstring");

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

            List<Data_Annotation.Models.CountryModel> list = new List<Data_Annotation.Models.CountryModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                Data_Annotation.Models.CountryModel countryModel = new Data_Annotation.Models.CountryModel();
                countryModel.CountryId = Convert.ToInt32(dr["CountryID"]);
                countryModel.CountryName = dr["CountryName"].ToString();
                list.Add(countryModel);
            }
            ViewBag.CountryList = list;

            #endregion

            #region State DropDown

            SqlConnection connection2 = new SqlConnection(connectionStr);
            connection2.Open();
            SqlCommand objCmd2 = connection2.CreateCommand();
            objCmd2.CommandType = CommandType.StoredProcedure;
            objCmd2.CommandText = "PR_State_ComboBox2";
            SqlDataReader reader2 = objCmd2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(reader2);
            connection2.Close();

            List<StateDropDownModel> list2 = new List<StateDropDownModel>();

            foreach (DataRow dr in dt2.Rows)
            {
                StateDropDownModel stateModel = new StateDropDownModel();
                stateModel.StateId = Convert.ToInt32(dr["StateID"]);
                stateModel.StateName = dr["stateName"].ToString();
                list2.Add(stateModel);
            }
            ViewBag.StateList = list2;

            #endregion

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_City_filter";
            objCmd.Parameters.AddWithValue("@CountryID", FilterModel.CountryID);
            objCmd.Parameters.AddWithValue("@StateID", FilterModel.StateID);
            objCmd.Parameters.AddWithValue("@CityName", FilterModel.CityName);
            objCmd.Parameters.AddWithValue("@CityCode", FilterModel.CityCode);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);

            ModelState.Clear();
            return View("LOC_City", dt);
        }
        #endregion

    }
}
