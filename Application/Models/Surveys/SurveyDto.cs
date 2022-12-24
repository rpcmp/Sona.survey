using System;

namespace Application.Models.Surveys
{
    public class SurveyDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public bool IsFirstAppointment { get; set; }

        public bool MoreThen50 { get; set; }

        public bool CureType { get; set; }

        public bool SuccessTherapy { get; set; }
    }
}
