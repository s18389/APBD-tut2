using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace tut2.Models
{
    public class Student
    {
        [XmlAttribute(attributeName: "index")]
        public string IndexNumber { get; set; }
        [XmlAttribute(attributeName: "email")]
        public string Email { get; set; }
        public string FirstName { get; set; }

        private string _lastName;
        public string LastName 
        { 
            get { return _lastName; }
            set 
            {
                _lastName = value ?? throw new ArgumentException();
            }
        }
    }
}
