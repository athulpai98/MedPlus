using AppoinmentTestAPI.Models;
using MedPlus.Web.Api.MedModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using static MedPlus.Web.Api.MedModel.AppointmentModel;

namespace MedPlus.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {

     
       
        private readonly ILogger<AppointmentController> _logger;


        private IConfiguration _configuration;

        


        public AppointmentController(ILogger<AppointmentController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }



        [HttpPost(Name = "CreateAppoinment")]
        public string CreateAppoinment([FromBody] Appointment _appointment)
        {
            try
            {
                string connString = _configuration.GetConnectionString("WebApiDatabase");
                SqlConnection _connection = new SqlConnection(connString);

                SqlCommand cmd = new SqlCommand("insert into Appointment (AppointmentID,AppointmentDate,AppointStatus,PatientID) values" +
                    " ('" + _appointment.AppointmentID + "','" + _appointment.AppointmentDate + "','" + _appointment.AppointStatus + "','" + _appointment.PatientID + "'  )", _connection);
                _connection.Open();
                int i = cmd.ExecuteNonQuery();
                _connection.Close();


                if (i == 1)
                {
                    return "Appointment Created Successfully ";
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

        // Update appointment Status details
        //1  Waiting patient ,2  Current Patient , 3 Completed Patient
        [HttpPut]
        [Route("UpdateAppointmentStatus")]
        public string Put(int appointmentId, [FromBody] int  _AppointmentStatus)
        {
            try
            {
                string connString = _configuration.GetConnectionString("WebApiDatabase");
                SqlConnection _connection = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("update Appointment set AppointStatus = '" + _AppointmentStatus + "'" +
                    " where AppointmentID =  '" + appointmentId + "'", _connection);
                _connection.Open();
                int i = cmd.ExecuteNonQuery();
                _connection.Close();

                if (i == 1)
                {
                    return "Appoinment status is successfully updated";
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

        // Select all Waiting appoinmant
        //1  Waiting patient 

        [HttpGet]
        [Route("GetAllAppointmentPatient")]
        public string GetAllAppointmentPatient()
        {
            try
            {
                string connString = _configuration.GetConnectionString("WebApiDatabase");
                SqlConnection _connection = new SqlConnection(connString);
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Appointment " +
                    "inner join Patient on Patient_ID =PatientID ", _connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(dt);
                }
                else
                {
                    return "No All Patient";
                }
            }
            catch (Exception ex)
            {
                return ("Error");
            }
        }


        // Select all Waiting appoinmant
        //1  Waiting patient 

        [HttpGet]
        [Route("GetWaitingAppointmentPatient")]
        public string GetWaitingAppointmentPatient()
        {
            try
            {
                string connString = _configuration.GetConnectionString("WebApiDatabase");
                SqlConnection _connection = new SqlConnection(connString);
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Appointment " +
                    "inner join Patient on Patient_ID =PatientID where AppointmentID= 1", _connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(dt);
                }
                else
                {
                    return "No Waiting Patient";
                }
            }
            catch (Exception ex)
            {
                return ("Error");
            }
        }



        // Select  Current appoinmant
        //2  Current patient 

        [HttpGet]
        [Route("GetCurrentAppointmentPatient")]
        public string GetCurrentppointmentPatient()
        {
            try
            {
                string connString = _configuration.GetConnectionString("WebApiDatabase");
                SqlConnection _connection = new SqlConnection(connString);
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Appointment " +
                    "inner join Patient on Patient_ID =PatientID where AppointmentID= 2", _connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(dt);
                }
                else
                {
                    return "No Current Patient";
                }
            }
            catch (Exception ex)
            {
                return ("Error");
            }
        }


        // Select  Current appoinmant
        //2  Current patient 

        [HttpGet]
        [Route("GetCompletedAppointmentPatient")]
        public string GetCompletedPatient()
        {
            try
            {
                string connString = _configuration.GetConnectionString("WebApiDatabase");
                SqlConnection _connection = new SqlConnection(connString);
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Appointment " +
                    "inner join Patient on Patient_ID =PatientID where AppointmentID= 3", _connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(dt);
                }
                else
                {
                    return "No Completed Patient";
                }
            }
            catch (Exception ex)
            {
                return ("Error");
            }
        }
    }
  }
