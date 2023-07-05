using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static MedPlus.Web.Api.MedModel.AppointmentModel;

namespace MedPlus.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {

        //string connString = this.Configuration.GetConnectionString("MyConn");
      //  SqlConnection _connection = new SqlConnection("Data Source = AUTO6OMFSDDATPS; Initial Catalog = HealthCare; Integrated Security = true");


        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<AppointmentController> _logger;


        private IConfiguration _configuration;

        


        public AppointmentController(ILogger<AppointmentController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }





        [HttpPost(Name = "GetWeatherForecastAthulHEalthcare")]
        public string GetWeatherForecastAthulHEalthcare([FromBody] Appointment _appointment)
        {
            try
            {
                string connString = _configuration.GetConnectionString("WebApiDatabase");
                SqlConnection _connection = new SqlConnection(connString);

                SqlCommand cmd = new SqlCommand("insert into Appointment (AppointmentID,AppointmentDate,AppointStatus,Patient_ID) values" +
                    " ('" + _appointment.AppointmentID + "','" + _appointment.AppointmentDate + "','" + _appointment.AppointStatus + "','" + _appointment.PatientID + "'  )", _connection);
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
    }
}