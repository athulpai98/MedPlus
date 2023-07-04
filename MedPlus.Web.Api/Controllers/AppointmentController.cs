using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static MedPlus.Web.Api.MedModel.AppointmentModel;

namespace MedPlus.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        SqlConnection _connection = new SqlConnection("Data Source = AUTOV99GRHTYSQC; Initial Catalog = localdb; Integrated Security = true");


        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(ILogger<AppointmentController> logger)
        {
            _logger = logger;
        }





        [HttpPost(Name = "GetWeatherForecastAthulHEalthcare")]
        public string GetWeatherForecastAthulHEalthcare([FromBody] Appointment _appointment)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into Appointment (AppointmentID,AppointmentDate,AppointStatus,PatientID) values" +
                    " ('" + _appointment.AppointmentID + "','" + _appointment.AppointmentDate + "','" + _appointment.AppointStatus + "','" + _appointment.PatientID + "'  )", _connection);
                _connection.Open();
                int i = cmd.ExecuteNonQuery();
                _connection.Close();


                //
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
    }
}