using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using Newtonsoft.Json;
using AppoinmentTestAPI.Models;
using System.Net;
using System.Data.SqlClient;
using MedPlus.Web.Api.Controllers;







namespace AppoinmentTestAPI.Controllers
{
    //[Route("api")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private IConfiguration _configuration;


        public PatientController( IConfiguration configuration)
        {

            _configuration = configuration;
        }
      
       // SqlConnection _connection = new SqlConnection("Data Source=AUTOHKQ2XGTH4GW;Initial Catalog=testdb;Persist Security Info=True; User ID=sa; Password=sa");
       
        //All Patient Details

        [HttpGet]
        [Route("AllPatient")]
        public string Get()
        {
            try
            {
                string connString = _configuration.GetConnectionString("WebApiDatabase");
                SqlConnection _connection = new SqlConnection(connString);
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Patient", _connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(dt);
                }
                else
                {
                    return "Data not found";
                }
            }
            catch (Exception ex)
            {
                return ("Error");
            }
        }
        // Create Patient details
        //
        [HttpPost]
        ////Post api/values
        [Route("CreatePatient")]
        public string Post([FromBody] Patient _patient)
        {
            try
            {
                string connString = _configuration.GetConnectionString("WebApiDatabase");
                SqlConnection _connection = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("insert into patient (Patient_ID,Patient_Name,Email_ID,Mobile) values" +
                    " ('" + _patient.Patient_ID + "','" + _patient.Patient_Name + "','" + _patient.Email_ID + "' ,'" + _patient.Mobile + "'   )", _connection);
                _connection.Open();
                int i = cmd.ExecuteNonQuery();
                _connection.Close();

                if (i == 1)
                {
                    return "Patient Insert Successfully ";
                }
                else
                {
                    return "Try again";
                }
            }
            catch (Exception ex)
            {
                return ("Failed to Insert");
            }
        }
        // Edit Patient details
        [HttpPut]
        [Route("UpdatePatientName")]
        public string Put(int Patient_ID, [FromBody] Patient _patientName)
        {
            try
            {
                string connString = _configuration.GetConnectionString("WebApiDatabase");
                SqlConnection _connection = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("update patient set Patient_Name = '" + _patientName.Patient_Name+ "',Email = '" 
                    + _patientName.Email_ID + "','" + _patientName.Mobile+ "' where Patient_ID =  '" + Patient_ID + "'", _connection);
                _connection.Open();
                int i = cmd.ExecuteNonQuery();
                _connection.Close();

                if (i == 1)
                {
                    return "Patient name  "  + _patientName + " updated successfully for Patient id"  + Patient_ID;
                }
                else
                {
                    return "Try again";
                }
            }
            catch (Exception ex)
            {
                return ("Failed to Insert");
            }
        }




    }
  }

