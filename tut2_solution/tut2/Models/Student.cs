using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace tut2.Models
{
    public class Student
    {
        private string _firstName;
        private string _lastName;
        private string _indexNumber;
        private DateTime _BirthDate;
        private string _Email;
        private string _mothersName;
        private string _fathersName;
        private string _studiesName;
        private string _studiesMode;

      
        public string fname
        { 
            get { return _firstName; }
            set { _firstName = value ?? throw new ArgumentException(); }
        }

        public string lname
        {
            get { return _lastName; }
            set { _lastName = value ?? throw new ArgumentException(); }
        }

        [XmlAttribute(attributeName: "indexNumber")]
        public string IndexNumber
        {
            get { return _indexNumber; }
            set { _indexNumber = value ?? throw new ArgumentException(); }
        }

        public DateTime? birthdate
        {
            get { return _BirthDate; }
            set { _BirthDate = value ?? throw new ArgumentException(); }
        }

        public string email
        {
            get { return _Email; }
            set { _Email = value ?? throw new ArgumentException(); }
        }

        public string mothersName
        {
            get { return _mothersName; }
            set { _mothersName = value ?? throw new ArgumentException(); }
        }

        public string fathersName
        {
            get { return _fathersName; }
            set { _fathersName = value ?? throw new ArgumentException(); }
        }
        public string StudiesName
        {
            get { return _studiesName; }
            set { _studiesName = value ?? throw new ArgumentException(); }
        }

        public string StudiesMode
        {
            get { return _studiesMode; }
            set { _studiesMode = value ?? throw new ArgumentException(); }
        }
    }
}
