namespace PetCareSystem
{
    // ABSTRACTION
    public abstract class Animal
    {
        // ENCAPSULATION
        private string petId;
        private string petName;
        private int age;
        private string ageUnit;

        public string PetId
        {
            get { return petId; }
            set { petId = value; }
        }

        public string PetName
        {
            get { return petName; }
            set { petName = value; }
        }

        public int Age
        {
            get { return age; }

            set
            {
                if (value >= 0)
                    age = value;
                else
                    age = 0;
            }
        }

        public string AgeUnit
        {
            get { return ageUnit; }
            set { ageUnit = value; }
        }

        protected Animal(
            string petId,
            string petName,
            int age,
            string ageUnit)
        {
            PetId = petId;
            PetName = petName;
            Age = age;
            AgeUnit = ageUnit;
        }

        // POLYMORPHISM
        public abstract string GetAnimalType();

        public virtual void DisplayAnimalDetails()
        {
            System.Console.WriteLine(
                "Pet ID      : " + PetId);

            System.Console.WriteLine(
                "Pet Name    : " + PetName);

            System.Console.WriteLine(
                "Animal Type : " + GetAnimalType());

            System.Console.WriteLine(
                "Age         : " +
                Age +
                " " +
                AgeUnit);
        }
    }
}