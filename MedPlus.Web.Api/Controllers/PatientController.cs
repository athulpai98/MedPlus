using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using Newtonsoft.Json;
using AppoinmentTestAPI.Models;
using System.Net;
using System.Data.SqlClient;

namespace AppoinmentTestAPI.Controllers
{
    //[Route("api")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        SqlConnection _connection = new SqlConnection("Data Source=AUTOHKQ2XGTH4GW;Initial Catalog=testdb;Persist Security Info=True; User ID=sa; Password=sa");
       
        [HttpGet]
        [Route("AllPatient")]
        public string Get()
        {
            try
            {
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

        [HttpPost]
        ////Post api/values
        [Route("CreatePatient")]
        public string Post([FromBody] Patient _patient)
        {
            try
            {
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

        [HttpPut]
        [Route("UpdatePatientName")]
        public string Put(int Patient_ID, [FromBody] string _patientName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update patient set Patient_Name = '" + _patientName + "' where Patient_ID =  '" + Patient_ID + "'", _connection);
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

