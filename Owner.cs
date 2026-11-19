namespace PetCareSystem
{
    public class Owner
    {
        // ENCAPSULATION
        private string ownerId;
        private string ownerName;
        private string phone;

        public string OwnerId
        {
            get { return ownerId; }
            set { ownerId = value; }
        }

        public string OwnerName
        {
            get { return ownerName; }
            set { ownerName = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public Owner(
            string ownerId,
            string ownerName,
            string phone)
        {
            OwnerId = ownerId;
            OwnerName = ownerName;
            Phone = phone;
        }

        public void DisplayOwnerDetails()
        {
            System.Console.WriteLine(
                "Owner ID    : " + OwnerId);

            System.Console.WriteLine(
                "Owner Name  : " + OwnerName);

            System.Console.WriteLine(
                "Phone       : " + Phone);
        }
    }
}