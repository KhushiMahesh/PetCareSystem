using System;

namespace PetCareSystem
{
    // CUSTOM EXCEPTION
    public class PetException : Exception
    {
        public PetException(string message)
            : base(message)
        {
        }
    }

    // CUSTOM EXCEPTION
    public class AppointmentException : Exception
    {
        public AppointmentException(string message)
            : base(message)
        {
        }
    }

    // CUSTOM EXCEPTION
    public class PetNotFoundException : Exception
    {
        public PetNotFoundException(string message)
            : base(message)
        {
        }
    }

    // CUSTOM EXCEPTION
    public class DoctorNotFoundException : Exception
    {
        public DoctorNotFoundException(string message)
            : base(message)
        {
        }
    }
}