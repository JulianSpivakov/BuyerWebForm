using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace BuyerWebForm.Models
{
    [Collection("Test")]
    public class WebFormFields
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
