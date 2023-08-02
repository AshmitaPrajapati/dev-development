using MongoDB.Bson;

namespace DAL.Model.AzureCosmosMongoDB
{
    public class StudentModel
    {
        public ObjectId _Id { get; set; }
        public int? Id { get; set; }
        public string Name { get; set; }
        public Guid RegistrationNo { get; set; }
        public DateTime AddmissionDate { get; set; }
        public float Year { get; set; }
        public char BloodGroup { get; set; }
        public decimal GeneralKnowleg { get; set; }
        public bool IsActive { get; set; }
    }
}
