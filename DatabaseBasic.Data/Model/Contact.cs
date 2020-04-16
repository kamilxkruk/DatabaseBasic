using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseBasic.Data.Model
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public SexEnum Sex { get; set; }
        public string PhoneNumber { get; set; }

    }

    public enum SexEnum
    {
        Male = 1,
        Female
    }
}
