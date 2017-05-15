namespace METechs.Models
{
    public class Clients : BaseDataObject
    {
        public int Id_client { get; set;}
        public new string Name { get; set; }

        public new string Address1 { get; set; }

        public new string Address2 { get; set; }

        public new string City { get; set; }

        public new string State { get; set; }

        public new string Zip { get; set; }

        public new string Phone { get; set; }
    }
}
