using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace PetCareSystem
{
    class Program
    {
        // OBJECT COLLECTIONS
        static List<Animal> pets = new List<Animal>();
        static List<Owner> owners = new List<Owner>();
        static List<Veterinarian> doctors = new List<Veterinarian>();
        static List<Appointment> appointments = new List<Appointment>();

        static void Main(string[] args)
        {
            LoadDoctors();

            bool running = true;

            while (running)
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine("==========================================");
                    Console.WriteLine("       PET CARE & VETERINARY SYSTEM");
                    Console.WriteLine("==========================================");
                    Console.WriteLine();
                    Console.WriteLine("1. Register Pet and Owner");
                    Console.WriteLine("2. View Pet Details");
                    Console.WriteLine("3. View All Doctors");
                    Console.WriteLine("4. Book Appointment");
                    Console.WriteLine("5. View Pet Appointments");
                    Console.WriteLine("6. Cancel Appointment");
                    Console.WriteLine("7. Exit");
                    Console.WriteLine();
                    Console.WriteLine("==========================================");

                    Console.Write("Enter your choice: ");

                    string choice =
                        Console.ReadLine() ?? "";

                    switch (choice)
                    {
                        case "1":
                            RegisterPetAndOwner();
                            break;

                        case "2":
                            ViewPetDetails();
                            break;

                        case "3":
                            ViewAllDoctors();
                            break;

                        case "4":
                            BookAppointment();
                            break;

                        case "5":
                            ViewPetAppointments();
                            break;

                        case "6":
                            CancelAppointment();
                            break;

                        case "7":
                            running = false;

                            Console.WriteLine();
                            Console.WriteLine(
                                "Thank you for using Pet Care System!");

                            break;

                        default:
                            throw new PetException(
                                "Invalid menu choice. Please select 1 to 7.");
                    }
                }
                catch (PetException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: " + ex.Message);
                }
                catch (AppointmentException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: " + ex.Message);
                }
                catch (PetNotFoundException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: " + ex.Message);
                }
                catch (DoctorNotFoundException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input format.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Unexpected error: " + ex.Message);
                }

                if (running)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Press Enter to continue...");

                    Console.ReadLine();
                }
            }
        }

        // ==========================================
        // PREDEFINED DOCTORS
        // ==========================================

        static void LoadDoctors()
        {
            doctors.Add(
                new Veterinarian(
                    "D101",
                    "Dr. Ananya",
                    "Veterinary Surgeon",
                    8,
                    500));

            doctors.Add(
                new Veterinarian(
                    "D102",
                    "Dr. Rahul",
                    "Pet Dermatologist",
                    6,
                    600));

            doctors.Add(
                new Veterinarian(
                    "D103",
                    "Dr. Priya",
                    "Veterinary Physician",
                    10,
                    700));
        }

        // ==========================================
        // REGISTER PET AND OWNER
        // ==========================================

        static void RegisterPetAndOwner()
        {
            Console.WriteLine();
            Console.WriteLine(
                "========== REGISTER PET ==========");

            Console.Write("Enter Pet ID: ");

            string petId =
                Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(petId))
            {
                throw new PetException(
                    "Pet ID cannot be empty.");
            }

            if (FindPet(petId) != null)
            {
                throw new PetException(
                    "Pet ID already exists.");
            }

            Console.Write("Enter Pet Name: ");

            string petName =
                Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(petName))
            {
                throw new PetException(
                    "Pet name cannot be empty.");
            }

            Console.Write(
                "Enter Animal Type (Dog/Cat): ");

            string type =
                Console.ReadLine() ?? "";

            type = type.Trim().ToLower();

            if (type != "dog" && type != "cat")
            {
                throw new PetException(
                    "Only Dog or Cat is supported.");
            }

            Console.Write("Enter Breed: ");

            string breed =
                Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(breed))
            {
                throw new PetException(
                    "Breed cannot be empty.");
            }

            // ==================================
            // AGE
            // ==================================

            Console.Write("Enter Age: ");

            int age;

            try
            {
                age = Convert.ToInt32(
                    Console.ReadLine());

                if (age <= 0)
                {
                    throw new PetException(
                        "Age must be greater than zero.");
                }
            }
            catch (FormatException)
            {
                throw new PetException(
                    "Age must be a whole number.");
            }

            Console.Write(
                "Enter Age Unit (Years/Months): ");

            string ageUnit =
                Console.ReadLine() ?? "";

            ageUnit =
                ageUnit.Trim().ToLower();

            if (ageUnit == "year")
            {
                ageUnit = "years";
            }
            else if (ageUnit == "month")
            {
                ageUnit = "months";
            }
            else if (
                ageUnit != "years" &&
                ageUnit != "months")
            {
                throw new PetException(
                    "Age unit must be Years or Months.");
            }

            Console.WriteLine();
            Console.WriteLine(
                "========== OWNER DETAILS ==========");

            Console.Write("Enter Owner ID: ");

            string ownerId =
                Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(ownerId))
            {
                throw new PetException(
                    "Owner ID cannot be empty.");
            }

            foreach (Owner existingOwner in owners)
            {
                if (existingOwner.OwnerId == ownerId)
                {
                    throw new PetException(
                        "Owner ID already exists.");
                }
            }

            Console.Write("Enter Owner Name: ");

            string ownerName =
                Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(ownerName))
            {
                throw new PetException(
                    "Owner name cannot be empty.");
            }

            // ==================================
            // PHONE NUMBER
            // ==================================

            Console.Write(
                "Enter Phone Number: ");

            string phone =
                Console.ReadLine() ?? "";

            phone = phone.Trim();

            if (phone.Length != 10 ||
                !phone.All(char.IsDigit))
            {
                throw new PetException(
                    "Phone number must contain exactly 10 digits.");
            }

            // ==================================
            // OBJECT CREATION
            // ==================================

            Owner owner =
                new Owner(
                    ownerId,
                    ownerName,
                    phone);

            Animal pet;

            // POLYMORPHISM
            if (type == "dog")
            {
                pet =
                    new Dog(
                        petId,
                        petName,
                        age,
                        ageUnit,
                        breed);
            }
            else
            {
                pet =
                    new Cat(
                        petId,
                        petName,
                        age,
                        ageUnit,
                        breed);
            }

            pets.Add(pet);
            owners.Add(owner);

            Console.WriteLine();
            Console.WriteLine(
                "Pet and Owner registered successfully!");

            Console.WriteLine();
            Console.WriteLine(
                "---------- PET DETAILS ----------");

            pet.DisplayAnimalDetails();

            Console.WriteLine();
            Console.WriteLine(
                "---------- OWNER DETAILS ----------");

            owner.DisplayOwnerDetails();
        }

        // ==========================================
        // VIEW PET DETAILS
        // ==========================================

        static void ViewPetDetails()
        {
            Console.WriteLine();
            Console.WriteLine(
                "========== PET DETAILS ==========");

            Console.Write("Enter Pet ID: ");

            string petId =
                Console.ReadLine() ?? "";

            Animal pet =
                FindPet(petId);

            if (pet == null)
            {
                throw new PetNotFoundException(
                    "Pet with ID " + petId +
                    " was not found.");
            }

            pet.DisplayAnimalDetails();

            Owner owner =
                FindOwnerForPet(petId);

            if (owner != null)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "---------- OWNER DETAILS ----------");

                owner.DisplayOwnerDetails();
            }
        }

        // ==========================================
        // VIEW ALL DOCTORS
        // ==========================================

        static void ViewAllDoctors()
        {
            Console.WriteLine();
            Console.WriteLine(
                "========== AVAILABLE DOCTORS ==========");

            if (doctors.Count == 0)
            {
                throw new DoctorNotFoundException(
                    "No doctors are available.");
            }

            foreach (Veterinarian doctor in doctors)
            {
                doctor.DisplayDoctorDetails();

                Console.WriteLine();
            }
        }

        // ==========================================
        // BOOK APPOINTMENT
        // ==========================================

        static void BookAppointment()
        {
            Console.WriteLine();
            Console.WriteLine(
                "========== BOOK APPOINTMENT ==========");

            Console.Write("Enter Pet ID: ");

            string petId =
                Console.ReadLine() ?? "";

            Animal pet =
                FindPet(petId);

            if (pet == null)
            {
                throw new PetNotFoundException(
                    "Pet with ID " + petId +
                    " was not found.");
            }

            Console.WriteLine();
            Console.WriteLine(
                "Pet: " + pet.PetName);

            Console.WriteLine(
                "Type: " + pet.GetAnimalType());

            Console.WriteLine();

            // SHOW DOCTORS
            Console.WriteLine(
                "---------- DOCTORS ----------");

            foreach (Veterinarian doctor in doctors)
            {
                Console.WriteLine(
                    doctor.DoctorId +
                    " - " +
                    doctor.DoctorName +
                    " - " +
                    doctor.Specialization +
                    " - ₹" +
                    doctor.ConsultationFee);
            }

            Console.WriteLine();

            Console.Write("Enter Doctor ID: ");

            string doctorId =
                Console.ReadLine() ?? "";

            Veterinarian doctorSelected =
                FindDoctor(doctorId);

            if (doctorSelected == null)
            {
                throw new DoctorNotFoundException(
                    "Doctor with ID " +
                    doctorId +
                    " was not found.");
            }

            // ==================================
            // DATE VALIDATION
            // ==================================

            Console.Write(
                "Enter Appointment Date (DD/MM/YYYY): ");

            string date =
                Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(date))
            {
                throw new AppointmentException(
                    "Appointment date cannot be empty.");
            }

            date = date.Trim();

            DateTime parsedDate;

            bool validDate =
                DateTime.TryParseExact(
                    date,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out parsedDate);

            if (!validDate)
            {
                throw new AppointmentException(
                    "Invalid date. Please use DD/MM/YYYY format.");
            }

            if (parsedDate.Date < DateTime.Today)
            {
                throw new AppointmentException(
                    "Appointment date cannot be in the past.");
            }

            // ==================================
            // TIME VALIDATION
            // ==================================

            Console.Write(
                "Enter Time Slot (e.g., 8:30 AM / 2:30 PM): ");

            string time =
                Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(time))
            {
                throw new AppointmentException(
                    "Time slot cannot be empty.");
            }

            time = time.Trim();

            DateTime parsedTime;

            bool validTime =
                DateTime.TryParseExact(
                    time,
                    new string[]
                    {
                        "h:mm tt",
                        "hh:mm tt",
                        "H:mm",
                        "HH:mm"
                    },
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out parsedTime);

            if (!validTime)
            {
                throw new AppointmentException(
                    "Invalid time. Please enter like 8:30 AM or 2:30 PM.");
            }

            // Store time consistently
            time =
                parsedTime.ToString(
                    "hh:mm tt",
                    CultureInfo.InvariantCulture);

            // Store date consistently
            date =
                parsedDate.ToString(
                    "dd/MM/yyyy");

            // ==================================
            // CHECK AVAILABILITY
            // ==================================

            foreach (Appointment existing
                     in appointments)
            {
                if (
                    existing.Doctor.DoctorId ==
                    doctorId
                    &&
                    existing.AppointmentDate ==
                    date
                    &&
                    existing.TimeSlot ==
                    time
                    &&
                    existing.IsBooked
                )
                {
                    throw new AppointmentException(
                        "This doctor is NOT AVAILABLE at this date and time.");
                }
            }

            // ==================================
            // CREATE APPOINTMENT
            // ==================================

            string appointmentId =
                "A" +
                (appointments.Count + 1)
                .ToString("D3");

            Appointment appointment =
                new Appointment(
                    appointmentId,
                    pet,
                    doctorSelected,
                    date,
                    time);

            // METHOD OVERLOADING
            appointment.BookAppointment(
                date,
                time);

            appointments.Add(appointment);

            Console.WriteLine();
            Console.WriteLine(
                "==========================================");

            Console.WriteLine(
                "       APPOINTMENT BOOKED SUCCESSFULLY");

            Console.WriteLine(
                "==========================================");

            appointment.DisplayAppointmentDetails();
        }

        // ==========================================
        // VIEW PET APPOINTMENTS
        // ==========================================

        static void ViewPetAppointments()
        {
            Console.WriteLine();
            Console.WriteLine(
                "========== PET APPOINTMENTS ==========");

            Console.Write("Enter Pet ID: ");

            string petId =
                Console.ReadLine() ?? "";

            Animal pet =
                FindPet(petId);

            if (pet == null)
            {
                throw new PetNotFoundException(
                    "Pet with ID " +
                    petId +
                    " was not found.");
            }

            bool found = false;

            // SHOW ONLY ACTIVE / BOOKED APPOINTMENTS
            foreach (Appointment appointment
                     in appointments)
            {
                if (
                    appointment.Pet.PetId.Equals(
                        petId,
                        StringComparison.OrdinalIgnoreCase)
                    &&
                    appointment.IsBooked
                )
                {
                    appointment.DisplayAppointmentDetails();

                    found = true;
                }
            }

            // NO ACTIVE APPOINTMENTS
            if (!found)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "No appointments found for this pet.");
            }
        }

        // ==========================================
        // CANCEL APPOINTMENT
        // ==========================================

        static void CancelAppointment()
        {
            Console.WriteLine();
            Console.WriteLine(
                "========== CANCEL APPOINTMENT ==========");

            Console.Write("Enter Pet ID: ");

            string petId =
                Console.ReadLine() ?? "";

            Animal pet =
                FindPet(petId);

            if (pet == null)
            {
                throw new PetNotFoundException(
                    "Pet with ID " +
                    petId +
                    " was not found.");
            }

            Console.WriteLine();
            Console.WriteLine(
                "Active appointments for " +
                pet.PetName +
                ":");

            bool found = false;

            foreach (Appointment appointment
                     in appointments)
            {
                if (
                    appointment.Pet.PetId.Equals(
                        petId,
                        StringComparison.OrdinalIgnoreCase)
                    &&
                    appointment.IsBooked
                )
                {
                    appointment.DisplayAppointmentDetails();

                    found = true;
                }
            }

            if (!found)
            {
                throw new AppointmentException(
                    "No active appointments found for this pet.");
            }

            Console.WriteLine();

            Console.Write(
                "Enter Appointment ID to cancel: ");

            string appointmentId =
                Console.ReadLine() ?? "";

            Appointment selectedAppointment = null;

            foreach (Appointment appointment
                     in appointments)
            {
                if (
                    appointment.AppointmentId ==
                    appointmentId
                    &&
                    appointment.Pet.PetId.Equals(
                        petId,
                        StringComparison.OrdinalIgnoreCase)
                )
                {
                    selectedAppointment =
                        appointment;

                    break;
                }
            }

            if (selectedAppointment == null)
            {
                throw new AppointmentException(
                    "Appointment not found.");
            }

            if (!selectedAppointment.IsBooked)
            {
                throw new AppointmentException(
                    "This appointment is already cancelled.");
            }

            // INTERFACE METHOD
            selectedAppointment.CancelAppointment();

            Console.WriteLine();
            Console.WriteLine(
                "Appointment cancelled successfully!");
        }

        // ==========================================
        // FIND PET
        // ==========================================

        static Animal FindPet(string petId)
        {
            foreach (Animal pet in pets)
            {
                if (
                    pet.PetId.Equals(
                        petId,
                        StringComparison.OrdinalIgnoreCase)
                )
                {
                    return pet;
                }
            }

            return null;
        }

        // ==========================================
        // FIND OWNER
        // ==========================================

        static Owner FindOwnerForPet(string petId)
        {
            int index = 0;

            foreach (Animal pet in pets)
            {
                if (
                    pet.PetId.Equals(
                        petId,
                        StringComparison.OrdinalIgnoreCase)
                )
                {
                    if (index < owners.Count)
                    {
                        return owners[index];
                    }
                }

                index++;
            }

            return null;
        }

        // ==========================================
        // FIND DOCTOR
        // ==========================================

        static Veterinarian FindDoctor(string doctorId)
        {
            foreach (Veterinarian doctor in doctors)
            {
                if (
                    doctor.DoctorId.Equals(
                        doctorId,
                        StringComparison.OrdinalIgnoreCase)
                )
                {
                    return doctor;
                }
            }

            return null;
        }
    }
}
