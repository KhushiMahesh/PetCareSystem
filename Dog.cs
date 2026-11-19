namespace PetCareSystem
{
    // INHERITANCE
    public class Dog : Animal
    {
        private string breed;

        public string Breed
        {
            get { return breed; }
            set { breed = value; }
        }

        public Dog(
            string petId,
            string petName,
            int age,
            string ageUnit,
            string breed)
            : base(
                petId,
                petName,
                age,
                ageUnit)
        {
            Breed = breed;
        }

        // POLYMORPHISM
        public override string GetAnimalType()
        {
            return "Dog";
        }

        public override void DisplayAnimalDetails()
        {
            base.DisplayAnimalDetails();

            System.Console.WriteLine(
                "Breed       : " + Breed);
        }
    }
}