﻿namespace MedPlus.Web.Api.MedModel
{
    public class AppointmentModel
    {

        public class Appointment
        {
            public int AppointmentID { get; set; }

            public DateTime AppointmentDate { get; set; }

            public int AppointStatus { get; set; }

            public int PatientID { get; set; }
        }
    }
}
