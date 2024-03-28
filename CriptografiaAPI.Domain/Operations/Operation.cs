using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptografiaAPI.Domain.Operations
{
    public class Operation
    {
        public int Id { get; set; }
        public string UserDocument {  get; set; }
        public string CreditCard {  get; set; }
        public int Value { get; set; }

        public Operation(string userDocument, string creditCard, int value) 
        {
            UserDocument = userDocument;
            CreditCard = creditCard;
            Value = value;
        }

        public Operation(int id, string userDocument, string creditCard, int value)
        {
            Id = id;
            UserDocument = userDocument;
            CreditCard = creditCard;
            Value = value;
        }
    }
}
