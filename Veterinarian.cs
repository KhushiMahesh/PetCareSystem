namespace PetCareSystem
{
    public class Veterinarian
    {
        // ENCAPSULATION
        private string doctorId;
        private string doctorName;
        private string specialization;
        private int experience;
        private double consultationFee;

        public string DoctorId
        {
            get { return doctorId; }
            set { doctorId = value; }
        }

        public string DoctorName
        {
            get { return doctorName; }
            set { doctorName = value; }
        }

        public string Specialization
        {
            get { return specialization; }
            set { specialization = value; }
        }

        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }

        public double ConsultationFee
        {
            get { return consultationFee; }
            set { consultationFee = value; }
        }

        public Veterinarian(
            string doctorId,
            string doctorName,
            string specialization,
            int experience,
            double consultationFee)
        {
            DoctorId = doctorId;
            DoctorName = doctorName;
            Specialization = specialization;
            Experience = experience;
            ConsultationFee = consultationFee;
        }

        public void DisplayDoctorDetails()
        {
            System.Console.WriteLine(
                "Doctor ID       : " + DoctorId);

            System.Console.WriteLine(
                "Doctor Name     : " + DoctorName);

            System.Console.WriteLine(
                "Specialization  : " + Specialization);

            System.Console.WriteLine(
                "Experience      : " +
                Experience +
                " years");

            System.Console.WriteLine(
                "Consultation Fee: ₹" +
                ConsultationFee);
        }
    }
}