namespace PetCareSystem
{
    // INTERFACE IMPLEMENTATION
    public class Appointment : IAppointable
    {
        // ENCAPSULATION
        private string appointmentId;
        private Animal pet;
        private Veterinarian doctor;
        private string appointmentDate;
        private string timeSlot;
        private bool isBooked;

        public string AppointmentId
        {
            get { return appointmentId; }
            set { appointmentId = value; }
        }

        public Animal Pet
        {
            get { return pet; }
            set { pet = value; }
        }

        public Veterinarian Doctor
        {
            get { return doctor; }
            set { doctor = value; }
        }

        public string AppointmentDate
        {
            get { return appointmentDate; }
            set { appointmentDate = value; }
        }

        public string TimeSlot
        {
            get { return timeSlot; }
            set { timeSlot = value; }
        }

        public bool IsBooked
        {
            get { return isBooked; }
            set { isBooked = value; }
        }

        public Appointment(
            string appointmentId,
            Animal pet,
            Veterinarian doctor,
            string appointmentDate,
            string timeSlot)
        {
            AppointmentId = appointmentId;
            Pet = pet;
            Doctor = doctor;
            AppointmentDate = appointmentDate;
            TimeSlot = timeSlot;
            IsBooked = false;
        }

        // INTERFACE METHOD
        public void BookAppointment()
        {
            IsBooked = true;
        }

        // METHOD OVERLOADING
        public void BookAppointment(
            string date,
            string time)
        {
            AppointmentDate = date;
            TimeSlot = time;
            IsBooked = true;
        }

        // INTERFACE METHOD
        public void CancelAppointment()
        {
            IsBooked = false;
        }

        public void DisplayAppointmentDetails()
        {
            System.Console.WriteLine(
                "------------------------------------------");

            System.Console.WriteLine(
                "Appointment ID : " +
                AppointmentId);

            System.Console.WriteLine(
                "Pet Name       : " +
                Pet.PetName);

            System.Console.WriteLine(
                "Doctor         : " +
                Doctor.DoctorName);

            System.Console.WriteLine(
                "Date           : " +
                AppointmentDate);

            System.Console.WriteLine(
                "Time           : " +
                TimeSlot);

            System.Console.WriteLine(
                "Status         : " +
                (IsBooked ? "Booked" : "Cancelled"));

            System.Console.WriteLine(
                "------------------------------------------");
        }
    }
}